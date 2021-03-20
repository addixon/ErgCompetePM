using Microsoft.Extensions.Logging;
using PM.BLL.Extensions;
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

        /// <summary>
        /// Special wrapper commands
        /// </summary>
        private static readonly IList<uint> _wrapperCommands;

        /// <summary>
        /// Formatted frames
        /// </summary>
        private readonly IList<uint[]> _frames;

        /// <inheritdoc />
        public int TotalFrames => _frames.Count;

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

            _frames = new List<uint[]>();
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
                base.Add(command);
                return;
            }

            throw new InvalidOperationException("Commands cannot be added to already prepared list");
        }

        /// <inheritdoc />
        public new void AddRange(IEnumerable<ICommand> commands)
        {
            if (IsOpen)
            {
                base.AddRange(commands);
                return;
            }
           
            throw new InvalidOperationException("Commands cannot be added to already prepared list");
        }

        /// <inheritdoc />
        public void Prepare()
        {
            if (IsOpen)
            {
                IEnumerable<PreparedCommand> preparedCommands = PrepareCommands();
                PrepareFrames(preparedCommands);

                // Ensure no more commands are added
                IsOpen = false;
            }
        }

        /// <inheritdoc />
        public uint[] GetFrame(int frameNumber)
        {
            if (CanSend)
            {
                return _frames[frameNumber];
            }

            throw new InvalidOperationException("Frames are not available until CommandList has been prepared.");
        }

        // TODO: Refactor read into something smaller
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

                        ICommand wrappedCommand = this.Skip(listHighWaterMark).First(command => command.Code == proprietaryCommandCode && command.Wrapper == commandCode);
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

                ICommand command = this.Skip(listHighWaterMark).First(command => command.Code == commandCode && command.Wrapper == null);
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

        private IEnumerable<PreparedCommand> PrepareCommands()
        {
            IList<PreparedCommand> preparedCommands = new List<PreparedCommand>();

            int parentCommandsFound = 0;
            for (int commandIndex = 0; commandIndex < Count;)
            {
                ICommand command = this[commandIndex++];
                IList<ICommand>? childCommands = new List<ICommand>();
                IList<uint>? parentTo = command.ParentTo?.ToList();

                if (parentTo == null)
                {
                    preparedCommands.Add(new PreparedCommand(command, null));
                    continue;
                }

                // if there are commands after this one
                if (commandIndex < Count)
                {
                    parentCommandsFound++;
                    ICommand childCommand;
                    int targetIndex = commandIndex;
                    while (targetIndex < Count && parentTo.Contains((childCommand = this[targetIndex]).Code))
                    {
                        if (parentCommandsFound > 1 && this[targetIndex].Code == (byte)PM3Command.SET_WORKOUTTYPE)
                        {
                            // Special case where WorkoutType would exist outside an interval
                            break;
                        }

                        // collect next record as a child
                        childCommands.Add(childCommand);
                        targetIndex++;
                    }

                    commandIndex = targetIndex;
                }

                preparedCommands.Add(new PreparedCommand(command, childCommands));
            }

            return preparedCommands;
        }

        private void PrepareFrames(IEnumerable<PreparedCommand> preparedCommands)
        {
            IList<IGrouping<uint?, PreparedCommand>> groups = preparedCommands.ChunkBy(command => command.Wrapper).ToList();

            int frameSize = 0;
            int maxFrameSize = _commandWriter.MaxSize;
            int totalGroups = groups.Count;

            if (!groups.Any())
            {
                throw new InvalidOperationException("Group count was unexpectedly 0.");
            }

            int maxFrameContentsSize = maxFrameSize - _prefix.Length - _suffix.Length - 1;

            _commandWriter.Reset();
            foreach (IGrouping<uint?, PreparedCommand> currentGroup in groups)
            {
                if (preparedCommands.Any(command => command.TotalSize > maxFrameContentsSize))
                {
                    throw new InvalidOperationException("Frame creation impossible. A single group is too large for the frame.");
                }

                IList<PreparedCommand> groupCommands = currentGroup.ToList();

                for(int commandIndex = 0; commandIndex < groupCommands.Count;)
                {
                    int frameSizeCheck = frameSize;
                    int iterationElements = 0;

                    // Determine the commands that can fit in this frame
                    for(int index = commandIndex; index < groupCommands.Count; index++, iterationElements++)
                    {
                        int groupSize = groupCommands[index].TotalSize;
                        frameSize += groupSize;
                        frameSizeCheck += groupSize;

                        if (frameSizeCheck > maxFrameContentsSize)
                        {
                            // this command made it go over, so stop checking
                            break;
                        }
                    }

                    IEnumerable<PreparedCommand> iterationCommands = groupCommands.Skip(commandIndex).Take(iterationElements);

                    // write the wrapper, if there is one and hasn't been written yet
                    if (currentGroup.Key != null)
                    {
                        _commandWriter.Add(currentGroup.Key.Value);
                        _commandWriter.Add((uint)iterationCommands.Sum(c => c.TotalSize));
                    }

                    foreach (PreparedCommand command in iterationCommands)
                    { 
                        // write all commands
                        _commandWriter.AddRange(command.GetBytes());
                        commandIndex++;

                        continue;
                    }
                    
                    // Frame was going to be too big
                    FinalizeFrame();
                    frameSize = 0;

                    string frame = string.Join(' ', _frames[_frames.Count-1].Select(b => b.ToString("X2")));
                }
            }
        }

        /// <summary>
        /// Creates the frame, inserting the command data and generating checksum
        /// </summary>
        private void FinalizeFrame()
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

            //reportId = 0x04;
            //_commandWriter.AddRange(Enumerable.Repeat<uint>(0, 64 - length - 1));


            if (length <= 121)
            {
                reportId = 0x02;
                _commandWriter.AddRange(Enumerable.Repeat<uint>(0, 121 - length - 1));
            }
            else
            {
                throw new InvalidOperationException("Buffer is too long!");
            }

            _commandWriter.Insert(0, reportId);

            _frames.Add(_commandWriter.ToArray());
            _commandWriter.Reset();
        }
    }
}
