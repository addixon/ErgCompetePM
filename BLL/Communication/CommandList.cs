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
            _prefix = new uint[] { 0xF0, 0xFD, 0x00 };
            _suffix = new uint[] { 0xF2 };
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

            do
            {
                stopFrameFlagIndex = ((List<uint>)reader).IndexOf((byte)FrameCommands.STOP_FRAME_FLAG, stopFrameFlagIndex + 1);

                if (stopFrameFlagIndex == -1)
                {
                    throw new InvalidOperationException("Improperly formatted frame. No stop flag was found or incorrect checksum.");
                }

                checksum = reader.Skip(reader.Position).Take(stopFrameFlagIndex - reader.Position - 1).Aggregate<uint, uint>(0, _calculateChecksum);
            } while (checksum != reader[stopFrameFlagIndex - 1]);

            // Remove checksum and end flag
            reader.Truncate(stopFrameFlagIndex - 1);

            if (reader.Position == reader.Size)
            {
                return true;
            }

            uint status = reader.ReadByte();

            do
            {
                uint commandCode = reader.ReadByte();

                if (commandCode == (uint)CSAFECommand.SET_USERCFG1)
                {
                    uint size = reader.ReadByte();
                    int startIndex = reader.Position;

                    while (reader.Position < startIndex + size)
                    {
                        uint proprietaryCommandCode = reader.ReadByte();

                        ICommand wrappedCommand = this.First(command => command.Code == proprietaryCommandCode && command.IsProprietary);
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

                ICommand command = this.First(command => command.Code == commandCode && !command.IsProprietary);
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
        /// Converts the commands from objects into bytecode
        /// </summary>
        private void FormatCommandData()
        {
            // Group as many PM3 commands together as possible, but allow them to be not first in the list
            Dictionary<int, List<ICommand>> commandGroups = new();
            bool? current = null;
            int size = 0;

            foreach(ICommand command in this)
            {
                if (current == null)
                {
                    current = command.IsProprietary;
                    commandGroups.Add(0, new List<ICommand> { command });
                    continue;
                }

                if (current == command.IsProprietary)
                {
                    commandGroups[size].Add(command);
                    continue;
                }

                current = command.IsProprietary;
                commandGroups.Add(++size, new List<ICommand> { command });
            }

            foreach(List<ICommand> commandGroup in commandGroups.Values)
            {
                bool isPM3 = commandGroup.First().IsProprietary;

                if (isPM3)
                {
                    // Add PM3 commands
                    _commandWriter.WriteByte((uint)CSAFECommand.SET_USERCFG1);
                    _commandWriter.WriteByte((uint)(commandGroup.Sum(command => command.Size)+commandGroup.Count+commandGroup.Count(command => command is SetCommand)));
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

            //checksum and byte stuffing
            for (int index = _prefix.Length; index < _commandWriter.Size; index++)
            {
                uint currentByte = _commandWriter[index];

                //calculate checksum
                checksum ^= currentByte;

                // byte stuffing
                if (0xF0 <= currentByte && currentByte <= 0xF3)
                {
                    uint[] byteStuffedValue = GetByteStuffingValue(currentByte);
                    _commandWriter.RemoveAt(index);
                    _commandWriter.InsertRange(index, byteStuffedValue);
                    index += byteStuffedValue.Length - 1;
                }
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

        /// <summary>
        /// Gets the equatable byte stuffed value
        /// </summary>
        /// <param name="frameByte">The byte to convert</param>
        /// <returns>The equatable value</returns>
        private static uint[] GetByteStuffingValue(uint frameByte)
        {
            return frameByte switch
            {
                0xF0 => new uint[] { 0xF3, 0x00 },
                0xF1 => new uint[] { 0xF3, 0x01 },
                0xF2 => new uint[] { 0xF3, 0x02 },
                0xF3 => new uint[] { 0xF3, 0x03 },
                _ => throw new InvalidOperationException("Frame byte to be stuffed was unknown: " + frameByte),
            };
        }
    }
}
