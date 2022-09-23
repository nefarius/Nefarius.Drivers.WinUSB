/*  WinUSBNet library
 *  (C) 2010 Thomas Bleeker (www.madwizard.org)
 *
 *  Licensed under the MIT license, see license.txt or:
 *  http://www.opensource.org/licenses/mit-license.php
 */

/* NOTE: Parts of the code in this file are based on the work of Jan Axelson
 * See http://www.lvr.com/winusb.htm for more information
 */

using System;
using System.Runtime.InteropServices;
using System.Threading;
using Windows.Win32.Devices.Usb;
using Microsoft.Win32.SafeHandles;

namespace Nefarius.Drivers.WinUSB.API;

internal enum POLICY_TYPE
{
    SHORT_PACKET_TERMINATE = 1,
    AUTO_CLEAR_STALL,
    PIPE_TRANSFER_TIMEOUT,
    IGNORE_SHORT_PACKETS,
    ALLOW_PARTIAL_READS,
    AUTO_FLUSH,
    RAW_IO
}

internal partial class WinUSBDevice
{
    [DllImport("winusb.dll", SetLastError = true)]
    private static extern bool WinUsb_Free(IntPtr InterfaceHandle);

    [DllImport("winusb.dll", SetLastError = true)]
    private static extern bool WinUsb_Initialize(SafeFileHandle DeviceHandle, ref IntPtr InterfaceHandle);
}
