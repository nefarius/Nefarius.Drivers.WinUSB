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

namespace Nefarius.Drivers.WinUSB.API
{
    /// <summary>
    /// API declarations relating to device management (SetupDixxx and
    /// RegisterDeviceNotification functions).
    /// </summary>
    internal static partial class DeviceManagement
    {
        // from setupapi.h

        private struct SP_DEVICE_INTERFACE_DATA
        {
            internal Int32 cbSize;
            internal Guid InterfaceClassGuid;
            internal Int32 Flags;
            internal IntPtr Reserved;
        }
        private struct SP_DEVINFO_DATA
        {
            internal Int32 cbSize;
            internal Guid ClassGuid;
            internal Int32 DevInst;
            internal IntPtr Reserved;
        }
       
        // from pinvoke.net
        private enum SPDRP : uint
        {
            SPDRP_DEVICEDESC = 0x00000000,
            SPDRP_HARDWAREID = 0x00000001,
            SPDRP_COMPATIBLEIDS = 0x00000002,
            SPDRP_NTDEVICEPATHS = 0x00000003,
            SPDRP_SERVICE = 0x00000004,
            SPDRP_CONFIGURATION = 0x00000005,
            SPDRP_CONFIGURATIONVECTOR = 0x00000006,
            SPDRP_CLASS = 0x00000007,
            SPDRP_CLASSGUID = 0x00000008,
            SPDRP_DRIVER = 0x00000009,
            SPDRP_CONFIGFLAGS = 0x0000000A,
            SPDRP_MFG = 0x0000000B,
            SPDRP_FRIENDLYNAME = 0x0000000C,
            SPDRP_LOCATION_INFORMATION = 0x0000000D,
            SPDRP_PHYSICAL_DEVICE_OBJECT_NAME = 0x0000000E,
            SPDRP_CAPABILITIES = 0x0000000F,
            SPDRP_UI_NUMBER = 0x00000010,
            SPDRP_UPPERFILTERS = 0x00000011,
            SPDRP_LOWERFILTERS = 0x00000012,
            SPDRP_MAXIMUM_PROPERTY = 0x00000013,

            SPDRP_ENUMERATOR_NAME = 0x16,
        }

        private enum RegTypes : int
        {
            // incomplete list, these are just the ones used.
            REG_SZ = 1,
            REG_MULTI_SZ = 7
        }

        [DllImport("setupapi.dll", SetLastError = true)]
        private static extern Int32 SetupDiDestroyDeviceInfoList(IntPtr DeviceInfoSet);

        [DllImport("setupapi.dll", SetLastError = true)]
        private static extern bool SetupDiEnumDeviceInterfaces(IntPtr DeviceInfoSet, IntPtr DeviceInfoData, ref System.Guid InterfaceClassGuid, Int32 MemberIndex, ref SP_DEVICE_INTERFACE_DATA DeviceInterfaceData);

        [DllImport("setupapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern bool SetupDiGetDeviceRegistryProperty(IntPtr DeviceInfoSet, ref SP_DEVINFO_DATA DeviceInfoData, SPDRP Property, out int PropertyRegDataType, byte[] PropertyBuffer, uint PropertyBufferSize, out UInt32 RequiredSize);

        [DllImport("setupapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern bool SetupDiGetDeviceRegistryProperty(IntPtr DeviceInfoSet, ref SP_DEVINFO_DATA DeviceInfoData, SPDRP Property, IntPtr PropertyRegDataType, IntPtr PropertyBuffer, uint PropertyBufferSize, out UInt32 RequiredSize);

        [DllImport("setupapi.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern IntPtr SetupDiGetClassDevs(ref System.Guid ClassGuid, IntPtr Enumerator, IntPtr hwndParent, Int32 Flags);

        [DllImport("setupapi.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern bool SetupDiGetDeviceInterfaceDetail(IntPtr DeviceInfoSet, ref SP_DEVICE_INTERFACE_DATA DeviceInterfaceData, IntPtr DeviceInterfaceDetailData, Int32 DeviceInterfaceDetailDataSize, ref Int32 RequiredSize, ref SP_DEVINFO_DATA DeviceInfoData);

        [DllImport("setupapi.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern bool SetupDiGetDeviceInterfaceDetail(IntPtr DeviceInfoSet, ref SP_DEVICE_INTERFACE_DATA DeviceInterfaceData, IntPtr DeviceInterfaceDetailData, Int32 DeviceInterfaceDetailDataSize, ref Int32 RequiredSize, IntPtr DeviceInfoData);
    }
}
