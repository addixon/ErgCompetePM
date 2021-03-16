using LibUsbDotNet.LibUsb;
using System.Collections.Generic;

namespace PM.BO.Comparers
{
    public class IUsbDeviceComparer : EqualityComparer<IUsbDevice>
    {
        public override bool Equals(IUsbDevice? device1, IUsbDevice? device2)
        {
            if (device1 == null && device2 == null)
            {
                return true;
            }
            
            if (device1 == null || device2 == null)
            {
                return false;
            }

            UsbDevice usbDevice1 = (UsbDevice)device1;
            UsbDevice usbDevice2 = (UsbDevice)device2;

            if (usbDevice1.BusNumber == usbDevice2.BusNumber && usbDevice1.Address == usbDevice2.Address )
            {
                return true;
            }

            return false;
        }

        public override int GetHashCode(IUsbDevice device)
        {
            UsbDevice usbDevice = (UsbDevice)device;

            (int BusNumber, int Address) location = (usbDevice.BusNumber, usbDevice.Address);
            return location.GetHashCode();
        }
    }
}
