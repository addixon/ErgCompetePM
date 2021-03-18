using Microsoft.Extensions.Logging;
using PM.BO;
using PM.BO.Enums;
using PM.BO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Communication
{
    /// <summary>
    /// Holds commands to be sent to the PM
    /// </summary>
    public class CommandList : List<ICommand>, ICommandList
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<CommandList> _logger;

        /// <summary>
        /// The frame start prefix
        /// </summary>
        /// <remarks>
        /// { 0xF1 }: Standard Frame
        /// { 0xF0, 0xFD, 0x00 }: Extended Frame communicating from Master to Slave
        /// </remarks>
        private static readonly uint[] _prefix;

        /// <summary>
        /// The frame stop suffix
        /// </summary>
        private static readonly uint[] _suffix;

        /// <summary>
        /// The command writer
        /// </summary>
        private readonly CommandWriter _commandWriter;

        /// <summary>
        /// Flag to determine if the frame has already been formed or if more commands can be added
        /// </summary>
        private bool IsOpen = false;

        private static readonly IList<uint> _wrapperCommands;

        /// <inheritdoc />
        public uint[] Buffer
        {
            get
            {
                if (CanSend)
                {
                    return _commandWriter.ToArray();
                }

                throw new InvalidOperationException("Buffer is not available until CommandList has been prepared.");
            }
        }

        /// <inheritdoc />
        public bool CanSend => !IsOpen;

        /// <summary>
        /// Static constructor
        /// </summary>
        static CommandList()
        {
            _prefix = new uint[] { 0xF1 };// { 0xF0, 0xFD, 0x00 };
            _suffix = new uint[] { 0xF2 };
            _wrapperCommands = new List<uint>()
            {
                (uint) CSAFECommand.SET_USERCFG1,
                (uint) CSAFECommand.SET_PMCFG,
                (uint) CSAFECommand.SET_PMDATA,
                (uint) CSAFECommand.GET_PMCFG,
                (uint) CSAFECommand.GET_PMDATA
            };
        }

        /// <summary>
        /// DI Constructor
        /// </summary>
        /// <param name="logger">The logger</param>
        public CommandList(ILogger<CommandList> logger)
        {
            _commandWriter = new CommandWriter();
            _logger = logger;
            IsOpen = true;
        }

        /// <inheritdoc />
        public void Reset()
        {
            // Clear the command list
            Clear();

            // Clear the command buffer
            _commandWriter.Reset();

            // Reopen the command list
            IsOpen = true;
        }

        /// <inheritdoc />
        public new void Add(ICommand command)
        {
            if (IsOpen)
            {
                command.Order = (ushort)Count;
                base.Add(command);
            }
            else
            {
                throw new InvalidOperationException("Commands cannot be added to already prepared list");
            }
        }

        /// <inheritdoc />
        public new void AddRange(IEnumerable<ICommand> commands)
        {
            if (IsOpen)
            {
                ushort order = (ushort)Count;
                foreach (ICommand command in commands)
                {
                    command.Order = order++;
                }
                base.AddRange(commands);
            }
            else
            {
                throw new InvalidOperationException("Commands cannot be added to already prepared list");
            }
        }

        /// <inheritdoc />
        public void Prepare()
        {
            if (IsOpen)
            {
                _commandWriter.Reset();

                FormatCommandData();
                FormatFrame();

                // Ensure no more commands are added
                IsOpen = false;
            }
        }

        /// <inheritdoc />
        public bool Read(IResponseReader reader)
        {
            if (IsOpen)
            {
                throw new InvalidOperationException("Attempting to read set before it has been prepared.");
            }

            if (reader.Count == 0)
            {
                throw new InvalidOperationException("Invalid response. Response was completely empty.");
            }

            uint reportId = reader.ReadByte();

            if (reader.Count < 1)
            {
                throw new InvalidOperationException("No data was returned");
            }

            uint startFlag = reader.ReadByte();

            uint destination;
            uint source;

            if (startFlag == (byte)FrameCommands.EXTENDED_START_FLAG)
            {
                if (reader.Count < 4)
                {
                    throw new InvalidOperationException("Improperly formatted frame.");
                }

                destination = reader.ReadByte();
                source = reader.ReadByte();
            }
            else if (startFlag != (byte)FrameCommands.STANDARD_START_FLAG)
            {
                throw new InvalidOperationException("No start flag was found");
            }

            int stopFrameFlagIndex = reader.Position;
            uint checksum;

            stopFrameFlagIndex = ((List<uint>)reader).IndexOf((byte)FrameCommands.STOP_FRAME_FLAG, stopFrameFlagIndex + 1);

            if (stopFrameFlagIndex == -1)
            {
                InvalidOperationException exception = new ("Improperly formatted frame. No stop flag was found.");
                _logger.LogError(exception, "No stop flag found.");
                throw exception;
            }

            // Unstuff bytes
            for (int index = reader.Position; index < stopFrameFlagIndex - 1; index++)
            {
                if (reader[index] == 0xF3)
                {
                    reader.RemoveAt(index);
                    stopFrameFlagIndex--;
                    reader[index] += 0xF0;
                }
            }

            checksum = reader.Skip(reader.Position).Take(stopFrameFlagIndex -reader.Position - 1).Aggregate<uint, uint>(0, _calculateChecksum);
            
            if (checksum != reader[stopFrameFlagIndex-1])
            {
                InvalidOperationException exception = new("Improperly formatted frame. Checksum did not match.");
                _logger.LogError(exception, "Checksum did not match.");
                throw exception;
            }

            // Remove checksum and end flag
            reader.Truncate(stopFrameFlagIndex - 1);

            if (reader.Position == reader.Size)
            {
                return true;
            }

            uint status = reader.ReadByte();
            int listHighWaterMark = 0;
            do
            {
                uint commandCode = reader.ReadByte();

                if (_wrapperCommands.Contains(commandCode))
                {
                    uint size = reader.ReadByte();
                    int startIndex = reader.Position;

                    while (reader.Position < startIndex + size)
                    {
                        uint proprietaryCommandCode = reader.ReadByte();

                        ICommand wrappedCommand = this.Skip(listHighWaterMark).First(command => command.Code == proprietaryCommandCode && command.ProprietaryWrapper == commandCode);
                        listHighWaterMark = IndexOf(wrappedCommand)+1;

                        if (wrappedCommand is GetCommand)
                        {
                            try
                            {
                                wrappedCommand.Read(reader);
                            }
                            catch (Exception e)
                            {
                                _logger.LogError(e, "Exception occurred while reading command [{CommandName}]", wrappedCommand.Name);

                                throw;
                            }
                        }
                    }

                    continue;
                }

                ICommand command = this.Skip(listHighWaterMark).First(command => command.Code == commandCode && command.ProprietaryWrapper == null);
                listHighWaterMark = IndexOf(command)+1;

                if (command is GetCommand)
                {
                    try
                    {
                        command.Read(reader);
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e, "Exception occurred while reading command [{CommandName}]", command.Name);

                        throw;
                    }
                }
            } while (reader.Position < reader.Size);

            // Ensure whole response has been read
            bool success = reader.Position == reader.Size;
            return success;
        }

        /// <summary>
        /// Function to calculate the checksum value
        /// </summary>
        private readonly Func<uint, uint, uint> _calculateChecksum = (source, accumulate) =>
        {
            return accumulate ^ source;
        };

        /// <summary>
        /// Converts the commands from objects into bytecode for sending to PM
        /// </summary>
        private void FormatCommandData()
        {
            // Group as many proprietary commands together as possible, but allow them to be not first in the list
            Dictionary<int, List<ICommand>> commandGroups = new();
            uint? current = null;
            int size = 0;

            foreach(ICommand command in this)
            {
                // First command, get group started
                if (!commandGroups.Any())
                {
                    current = command.ProprietaryWrapper;
                    commandGroups.Add(0, new List<ICommand> { command });
                    continue;
                }

                if (current == command.ProprietaryWrapper)
                {
                    commandGroups[size].Add(command);
                    continue;
                }

                current = command.ProprietaryWrapper;
                commandGroups.Add(++size, new List<ICommand> { command });
            }

            foreach(List<ICommand> commandGroup in commandGroups.Values)
            {
                uint? wrapper = commandGroup.First().ProprietaryWrapper;

                if (wrapper != null)
                {
                    // Add wrapped commands
                    _commandWriter.WriteByte(wrapper.Value);
                    _commandWriter.WriteByte((uint)(commandGroup.Where(command => command is SetCommand).Sum(command => command.Size + 1)+commandGroup.Count));
                }

                foreach (ICommand command in commandGroup)
                {
                    command.Write(_commandWriter);
                }
            }
        }

        /// <summary>
        /// Creates the frame, inserting the command data and generating checksum
        /// </summary>
        private void FormatFrame()
        {
            // Apply prefix
            _commandWriter.PrependRange(_prefix);

            uint checksum = 0x0;

            //checksum
            for (int index = _prefix.Length; index < _commandWriter.Size; index++)
            {
                uint currentByte = _commandWriter[index];

                //calculate checksum
                checksum ^= currentByte;
            }

            // Append CheckSum
            _commandWriter.WriteByte(checksum);

            // Append suffix
            _commandWriter.AppendRange(_suffix);

            uint reportId;
            int length = _commandWriter.Length;

            if (length <= 121)
            {
                reportId = 0x02;
                _commandWriter.AddRange(Enumerable.Repeat<uint>(0, 121 - length));
            }
            else
            {
                throw new InvalidOperationException("Buffer is too long!");
            }

            _commandWriter.Insert(0, reportId);
        }
    }
}
