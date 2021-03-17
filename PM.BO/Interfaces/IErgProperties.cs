using PM.BO.Enums;

namespace PM.BO.Interfaces
{
    public interface IErgProperties
    {
        string? SerialNumber { get; }
        byte? MfgId { get; }
        byte? Model { get; }
        ushort? HwVersion { get; }
        ushort? SwVersion { get; }
        DisplayUnitsType? Units { get; }
    }
}
