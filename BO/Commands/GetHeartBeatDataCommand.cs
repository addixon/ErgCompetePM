﻿using BO.Enums;
using BO.Interfaces;
using System.Collections.Generic;

namespace BO.Commands
{
    public class GetHeartBeatDataCommand: LongGetCommand
    {
        public override byte Code => (byte)PM3Command.GET_HEARTBEATDATA;
        public override ushort? ResponseSize => 33;
        
        public override bool IsProprietary => true;

        public new string Resolution = "1 ms";

        protected override uint[]? Data { get; }


        public GetHeartBeatDataCommand(ushort blockLengthInBytes) : base(null)
        {
            Data = new uint[] { blockLengthInBytes };
        }

        protected override void ReadImplementation(IResponseReader responseReader, ushort _)
        {
            byte size = (byte) responseReader.ReadByte();
            List<ushort> heartbeats = new List<ushort>();

            for (int i = 0; i < size; i += 2)
            {
                heartbeats.Add((ushort) responseReader.ReadUShort());
            }

            Value = (ushort[]?) heartbeats.ToArray();
        }
    }
}
