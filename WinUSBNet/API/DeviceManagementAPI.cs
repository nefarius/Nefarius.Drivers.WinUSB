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

namespace MadWizard.WinUSBNet.API
{
    /// <summary>
    /// API declarations relating to device management (SetupDixxx and
    /// RegisterDeviceNotification functions).
    /// </summary>
    internal static partial class DeviceManagement
    {
        // from setupapi.h

        private const Int32 DIGCF_PRESENT = 2;
        private const Int32 DIGCF_DEVICEINTERFACE = 0X10;


        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        internal struct WNDCLASS
        {
            //[MarshalAs(UnmanagedType.U4)]
            public uint style;
            public IntPtr lpfnWndProc;
            public int cbClsExtra;
            public int cbWndExtra;
            public IntPtr hInstance;
            public IntPtr hIcon;
            public IntPtr hCursor;
            public IntPtr hbrBackground;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string lpszMenuName;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string lpszClassName;
        }

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
        [StructLayout(LayoutKind.Sequential)]
        internal struct POINT
        {
            long x;
            long y;
        }
        [StructLayout(LayoutKind.Sequential)]
        internal struct MSG
        {
            IntPtr hwnd;
            uint message;
            UIntPtr wParam;
            IntPtr lParam;
            int time;
            POINT pt;
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

        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern IntPtr RegisterDeviceNotification(IntPtr hRecipient, IntPtr NotificationFilter, Int32 Flags);

        //[DllImport("setupapi.dll", SetLastError = true)]
        //internal static extern Int32 SetupDiCreateDeviceInfoList(ref System.Guid ClassGuid, Int32 hwndParent);

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

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool UnregisterDeviceNotification(IntPtr Handle);


        [DllImport("user32.dll", SetLastError = true, EntryPoint = "RegisterClass", CharSet = CharSet.Unicode)]
        internal static extern System.UInt16 RegisterClass([In] ref WNDCLASS lpWndClass);

        [DllImport("kernel32.dll")]
        internal static extern uint GetLastError();

        [DllImport("user32.dll")]
        internal static extern IntPtr DefWindowProc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        internal static extern void PostQuitMessage(int nExitCode);

        [DllImport("user32.dll")]
        internal static extern sbyte GetMessage(out MSG lpMsg, IntPtr hWnd, uint wMsgFilterMin,
           uint wMsgFilterMax);

        [DllImport("user32.dll")]
        internal static extern sbyte TranslateMessage(out MSG lpMsg);

        [DllImport("user32.dll")]
        internal static extern IntPtr DispatchMessage(out MSG lpMsg);

        [DllImport("user32.dll")]
        internal static extern IntPtr LoadCursor(IntPtr hInstance, int lpCursorName);

        [UnmanagedFunctionPointer(CallingConvention.Winapi, SetLastError = true)]
        internal delegate IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll")]
        internal static extern bool UpdateWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        internal static extern bool DestroyWindow(IntPtr hWnd);


        [DllImport("user32.dll", SetLastError = true, EntryPoint = "CreateWindowEx", CharSet = CharSet.Unicode)]
        internal static extern IntPtr CreateWindowEx(
           int dwExStyle,
          //UInt16 regResult,
          [MarshalAs(UnmanagedType.LPWStr)]
          string lpClassName,
        [MarshalAs(UnmanagedType.LPWStr)]
        string lpWindowName,
           UInt32 dwStyle,
           int x,
           int y,
           int nWidth,
           int nHeight,
           IntPtr hWndParent,
           IntPtr hMenu,
           IntPtr hInstance,
           IntPtr lpParam);

        private const int ERROR_NO_MORE_ITEMS = 259;
        private const int ERROR_INSUFFICIENT_BUFFER = 122;
    }
}
