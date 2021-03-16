namespace PM.BO
{
    public class Capabilities
    {
        public byte MaxRxFrame { get; set; }
        public byte MaxTxFrame { get; set; }
        public byte MinInterframe { get; set; }

        public byte[] Code0x01 { get; set; } = new byte[2];
        public byte[] Code0x02 { get; set; } = new byte[11];
    }
}
