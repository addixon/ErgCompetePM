using System;

namespace PM.BO.Interfaces
{
    public interface IErg
    {
        IErgProperties Properties { get; set; }

        DateTime? LastCommunication { get; set; }

        IErgData Data { get; set; }
    }
}
