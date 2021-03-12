using BO.Enums;
using BO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Communication
{
    public class CommandList : List<ICommand>, IList<ICommand>, ICommandList
    {
        private const ushort _maxBufferSize = 96;
        private readonly CommandWriter _commandWriter;

        private bool IsOpen = false;

        public uint[] Buffer => _commandWriter.Buffer;
        public int Size => _commandWriter.Size;
        public bool CanSend => !IsOpen;

        public ushort ExpectedResponseSize
        {
            get
            {
                ushort responseSize = 0;

                foreach (ICommand command in this)
                {
                    // Add the expected response size
                    responseSize += command.ResponseSize ?? 0;

                    // Add for the code byte and size byte
                    responseSize += 2;
                }

                if (GetPM3Commands().Any())
                {
                    // Add for the UsrCfg1 Byte and size byte
                    responseSize += 2;
                }

                // Increase size by factor of two to account for worst-case frame stuffing
                return (ushort)Math.Min(responseSize * 2 + 1, _maxBufferSize);
            }
        }

        public CommandList()
        {
            _commandWriter = new CommandWriter(_maxBufferSize);
            IsOpen = true;
        }

        public void Reset()
        {
            // Clear the command list
            base.Clear();

            // Clear the command buffer
            _commandWriter.Reset(_maxBufferSize);

            // Reopen the command list
            IsOpen = true;
        }

        public new void Add(ICommand command)
        {
            if (IsOpen)
            {
                command.Order = (ushort)base.Count;
                base.Add(command);
            }
            else
            {
                throw new InvalidOperationException("Commands cannot be added to already prepared list");
            }
        }

        public new void AddRange(IEnumerable<ICommand> commands)
        {
            if (IsOpen)
            {
                ushort order = (ushort)base.Count;
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

        public void Prepare()
        {
            if (IsOpen)
            {
                // Get size of commands
                //int commandSize = this.Sum(command => command.)
                _commandWriter.Reset(_maxBufferSize);

                ushort size = 0;
                IList<ICommand> commands = GetPM3Commands();

                if (commands.Any())
                {
                    // Add PM3 commands
                    _commandWriter.WriteByte((byte)CSAFECommand.SET_USERCFG1);
                    _commandWriter.WriteByte((byte)commands.Sum(command => command.Size));
                    size += 2;

                    foreach (ICommand command in commands)
                    {
                        command.Write(_commandWriter);
                        size += command.Size;
                    }
                }

                // Add CSAFE commands
                foreach (ICommand command in GetCSafeCommands())
                {
                    command.Write(_commandWriter);
                    size += command.Size;
                }

                _commandWriter.Resize(size);

                // Ensure no more commands are added
                IsOpen = false;
            }
        }

        public bool Read(IResponseReader reader)
        {
            if (IsOpen)
            {
                throw new InvalidOperationException("Attempting to read set before it has been prepared.");
            }

            if (GetPM3Commands().Any())
            {
                ushort size = 0;

                // Read the PM3 custom command marker and size
                if (reader.ReadByte() == (uint)CSAFECommand.SET_USERCFG1)
                {
                    // Read the size
                    size = (ushort)reader.ReadByte();
                }

                if (size > 0)
                {
                    // Read PM3 commands
                    foreach (ICommand cmd in GetPM3Commands())
                    {
                        cmd.Read(reader);
                    }
                }
            }

            // Read CSAFE commands
            foreach (ICommand cmd in GetCSafeCommands())
            {
                cmd.Read(reader);
            }

            // Ensure whole response has been read
            bool success = reader.Position == reader.Size;
            return success;
        }

        private IList<ICommand> GetPM3Commands()
        {
            return base.FindAll(command => command.IsProprietary).OrderBy(command => command.Order).ToList();
        }

        private IList<ICommand> GetCSafeCommands()
        {
            return base.FindAll(command => !command.IsProprietary).OrderBy(command => command.Order).ToList();
        }
    }
}
