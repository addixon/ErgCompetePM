﻿// Copyright © 2006-2010 Travis Robinson. All rights reserved.
//
// website: http://sourceforge.net/projects/libusbdotnet
// e-mail:  libusbdotnet@gmail.com
//
// This program is free software; you can redistribute it and/or modify it
// under the terms of the GNU General Public License as published by the
// Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful, but
// WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
// or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License
// for more details.
//
// You should have received a copy of the GNU General Public License along
// with this program; if not, write to the Free Software Foundation, Inc.,
// 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA. or
// visit www.gnu.org.
//
//
using System;

namespace LibUsbDotNet.LibUsb
{
    /// <summary>
    /// Represents a device which is managed by libusb. Use <see cref="UsbContext.List"/>
    /// to get a list of devices which are available for use.
    /// </summary>
    public partial class UsbDevice : IUsbDevice, IDisposable, ICloneable
    {
        private bool disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsbDevice"/> class.
        /// </summary>
        /// <param name="device">
        /// A device handle for this device. In most cases, you will want to use the
        /// <see cref="UsbContext.List()"/> methods to list all devices.
        /// </param>
        public UsbDevice(Device device)
        {
            if (device == null)
            {
                throw new ArgumentNullException(nameof(device));
            }

            if (device == Device.Zero || device.IsClosed || device.IsInvalid)
            {
                throw new ArgumentOutOfRangeException(nameof(device));
            }

            this.device = device;
        }

        /// <summary>
        /// Creates a clone of this device.
        /// </summary>
        /// <returns>
        /// A new <see cref="UsbDevice"/> which represents a clone of this device.
        /// </returns>
        public IUsbDevice Clone()
        {
            return new UsbDevice(NativeMethods.RefDevice(this.device));
        }

        /// <inheritdoc/>
        object ICloneable.Clone()
        {
            return this.Clone();
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            if (!this.disposed)
            {
                // Close the libusb_device_handle if required.
                this.Close();

                // Close the libusb_device handle.
                this.device.Dispose();

                this.disposed = true;
            }
        }

        public Error DetachFromKernel(int interfaceNumber)
        {
            return NativeMethods.DetachKernelDriver(this.DeviceHandle, interfaceNumber);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            if (this.IsOpen)
            {
                return this.Descriptor.ToString();
            }
            else
            {
                return $"PID 0x{this.ProductId:X} - VID: 0x{this.VendorId:X}";
            }
        }

        /// <summary>
        /// Throws a <see cref="ObjectDisposedException"/> if this device has been disposed of.
        /// </summary>
        protected void EnsureNotDisposed()
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(nameof(UsbDevice));
            }
        }
    }
}
