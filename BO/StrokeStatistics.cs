namespace BO
{
    public class StrokeStatistics
    {
        public ushort Distance { get; set; }
        public byte DriveTime { get; set; }
        public ushort RecoveryTime { get; set; }
        public byte Length { get; set; }
        public ushort Count { get; set; }
        public ushort Peak { get; set; }
        public ushort ImpulseForce { get; set; }
        public ushort AverageForce { get; set; }
        public ushort WorkPerStroke { get; set; }
    }
}
