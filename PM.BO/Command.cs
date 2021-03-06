using PM.BO.Enums;
using PM.BO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PM.BO
{
    public abstract class Command : ICommand
    {
        public abstract string Name { get; }

        public abstract byte Code { get; }

        protected IEnumerable<uint>? Data { get; set; }

        public abstract PMCommandType CommandType { get; }

        public bool IsShortCommand => CommandType == PMCommandType.Short;

        public bool IsLongCommand => CommandType == PMCommandType.Long;

        public virtual string Units { get; } = string.Empty;

        public virtual string Resolution { get; } = string.Empty;

        public abstract ushort TotalSize { get; }

        public virtual uint? Wrapper { get; } = null;

        public virtual IEnumerable<uint>? ParentTo { get; } = null;

        public virtual int? MaximumOccurrenceInParent { get; } = null;

        public ushort Size => (ushort) (Data?.Count() ?? 0);

        public abstract ushort? ResponseSize { get; }

        public dynamic? Value { get; protected set; } = null;

        public virtual uint[] GetBytes()
        {
            List<uint> bytes = new();

            bytes.Add(Code);

            if (Data != null && Data.Any())
            {
                if (typeof(SetCommand).IsAssignableFrom(GetType()))
                {
                    bytes.Add((uint)Data.Count());
                }

                bytes.AddRange(Data);
            }

            return bytes.ToArray();
        }

        public virtual void Write(ICommandWriter commandWriter)
        {
            commandWriter.WriteBytes(GetBytes());
        }

        public void Read(IResponseReader responseReader)
        {
            if (ResponseSize == 0)
            {
                return;
            }

            ushort size = (ushort)responseReader.ReadByte();

            if (size == ResponseSize)
            {
                ReadImplementation(responseReader, size);
            }
            else
            {
                throw new InvalidOperationException($"Unexpected size. Encountered size [{size}] and expected [{ResponseSize}].");
            }
        }

        protected virtual void ReadImplementation(IResponseReader responseReader, ushort size)
        {
            throw new NotSupportedException("Read is not supported for this command.");
        }

        protected virtual void PerformByteStuffing()
        {
            if (Data == null)
            {
                return;
            }

            bool dataModified = false;
            List<uint> data = new(Data);
            for (int index = 0; index <= Data.Count(); index++)
            {
                uint currentByte = data[index];

                // byte stuffing
                if (0xF0 <= currentByte && currentByte <= 0xF3)
                {
                    dataModified = true;
                    uint[] byteStuffedValue = GetByteStuffingValue(currentByte);
                    data.RemoveAt(index);
                    data.InsertRange(index, byteStuffedValue);
                    index += byteStuffedValue.Length - 1;
                }
            }

            if (dataModified)
            {
                // Replace data
                Data = data;
            }
        }

        /// <summary>
        /// Gets the equatable byte stuffed value
        /// </summary>
        /// <param name="frameByte">The byte to convert</param>
        /// <returns>The equatable value</returns>
        protected static uint[] GetByteStuffingValue(uint frameByte)
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
