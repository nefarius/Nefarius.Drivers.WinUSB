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
    private const int ERROR_NO_MORE_ITEMS = 259;

    [DllImport("winusb.dll", SetLastError = true)]
    private static extern bool WinUsb_Free(IntPtr InterfaceHandle);

    [DllImport("winusb.dll", SetLastError = true)]
    private static extern bool WinUsb_Initialize(SafeFileHandle DeviceHandle, ref IntPtr InterfaceHandle);

    [DllImport("winusb.dll", SetLastError = true)]
    private static extern bool WinUsb_QueryInterfaceSettings(IntPtr InterfaceHandle, Byte AlternateInterfaceNumber,
        out USB_INTERFACE_DESCRIPTOR UsbAltInterfaceDescriptor);

    [DllImport("winusb.dll", SetLastError = true)]
    private static extern bool WinUsb_QueryPipe(IntPtr InterfaceHandle, Byte AlternateInterfaceNumber, Byte PipeIndex,
        out WINUSB_PIPE_INFORMATION PipeInformation);

    [DllImport("winusb.dll", SetLastError = true)]
    private static extern bool WinUsb_AbortPipe(IntPtr InterfaceHandle, byte PipeID);

    //  Two declarations for WinUsb_SetPipePolicy.
    //  Use this one when the returned Value is a Byte (all except PIPE_TRANSFER_TIMEOUT):

    [DllImport("winusb.dll", SetLastError = true)]
    private static extern bool WinUsb_SetPipePolicy(IntPtr InterfaceHandle, Byte PipeID, UInt32 PolicyType,
        UInt32 ValueLength, ref byte Value);

    [DllImport("winusb.dll", SetLastError = true)]
    private static extern bool WinUsb_GetPipePolicy(IntPtr InterfaceHandle, Byte PipeID, UInt32 PolicyType,
        ref UInt32 ValueLength, out byte Value);

    //  Use this alias when the returned Value is a UInt32 (PIPE_TRANSFER_TIMEOUT only):

    [DllImport("winusb.dll", SetLastError = true)]
    private static extern bool WinUsb_SetPipePolicy(IntPtr InterfaceHandle, Byte PipeID, UInt32 PolicyType,
        UInt32 ValueLength, ref UInt32 Value);

    [DllImport("winusb.dll", SetLastError = true)]
    private static extern bool WinUsb_GetPipePolicy(IntPtr InterfaceHandle, Byte PipeID, UInt32 PolicyType,
        ref UInt32 ValueLength, out UInt32 Value);

    [DllImport("winusb.dll", SetLastError = true)]
    private static extern bool WinUsb_FlushPipe(IntPtr InterfaceHandle, byte PipeID);
}
