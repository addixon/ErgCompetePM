﻿using BO.Enums;
using BO.Interfaces;
using System;

namespace BO.Commands
{
    public class GetCadenceCommand: ShortGetCommand
    {
        public override byte Code => (byte) CSAFECommand.GET_CADENCE;
        public override ushort? ResponseSize => 3;
        
        public override bool IsProprietary => false;

        private const ushort _refreshRate = 2;

        public new string Units = "Strokes/Min";
        public new string Resolution = "1 stroke";

        public GetCadenceCommand() : base(_refreshRate)
        {

        }

        protected override void ReadImplementation(IResponseReader responseReader, ushort _)
        {
            Value = (ushort?) responseReader.ReadUShort();
            responseReader.ReadByte(); // TODO: Units
        }
    }
}
