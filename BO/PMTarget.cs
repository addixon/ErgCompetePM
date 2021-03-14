using LibUsbDotNet.LibUsb;
using System;

namespace PM.BO
{
    public record PMTarget
    {
        public Func<UsbEndpointReader>? Reader { get; set; }
        public Func<UsbEndpointWriter>? Writer { get; set; }

        public (int BusNumber, int Address)Location { get; }

        public PMTarget((int BusNumber, int Address) location)
        {
            Location = location;
        }
    }
}
