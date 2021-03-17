using PM.BO.Interfaces;
using System;

namespace PM.BO
{
    public class PM : IErg
    {
        public IErgProperties Properties { get; set; }

        public DateTime? LastCommunication { get; set; }

        public IErgData Data { get; set; }

        public PM(IErgProperties properties)
        {
            Properties = properties;
            Data = new PMData();
        }

        public PM()
        {
            Properties = new PMProperties();
            Data = new PMData();
        }
    }
}