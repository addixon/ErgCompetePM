namespace PM.BO
{
    public class ProductVersion
    {
        public byte ManufacturerId { get; set; }
        public byte CID { get; set; }
        public byte Model { get; set; }
        public ushort HardwareVersion { get; set; }
        public ushort SoftwareVersion { get; set; }
    }
}
