using PM.BO.Enums;

namespace PM.BO
{
    public record PMProperties
    {
        public string? SerialNumber { get; }
        public byte? MfgId { get; }
        public byte? Model { get; }
        public ushort? HwVersion { get; }
        public ushort? SwVersion { get; }
        public DisplayUnitsType? Units { get; }

        public PMProperties(string? serialNumber, byte? mfgId, byte? model, ushort? hwVersion, ushort? swVersion, DisplayUnitsType? units) => (SerialNumber, MfgId, Model, HwVersion, SwVersion, Units) = (serialNumber, mfgId, model, hwVersion, swVersion, units);
    }
}
