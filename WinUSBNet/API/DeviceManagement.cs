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
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace MadWizard.WinUSBNet.API;

/// <summary>
///     Routines for detecting devices and receiving device notifications.
/// </summary>
internal static partial class DeviceManagement
{
    private static byte[] GetProperty(IntPtr deviceInfoSet, SP_DEVINFO_DATA deviceInfoData, SPDRP property,
        out int regType)
    {
        uint requiredSize;

        if (!SetupDiGetDeviceRegistryProperty(deviceInfoSet, ref deviceInfoData, property, IntPtr.Zero, IntPtr.Zero, 0,
                out requiredSize))
            if (Marshal.GetLastWin32Error() != ERROR_INSUFFICIENT_BUFFER)
                throw APIException.Win32("Failed to get buffer size for device registry property.");

        var buffer = new byte[requiredSize];

        if (!SetupDiGetDeviceRegistryProperty(deviceInfoSet, ref deviceInfoData, property, out regType, buffer,
                (uint)buffer.Length, out requiredSize))
            throw APIException.Win32("Failed to get device registry property.");

        return buffer;
    }

    // todo: is the queried data always available, or should we check ERROR_INVALID_DATA?
    private static string GetStringProperty(IntPtr deviceInfoSet, SP_DEVINFO_DATA deviceInfoData, SPDRP property)
    {
        int regType;
        var buffer = GetProperty(deviceInfoSet, deviceInfoData, property, out regType);
        if (regType != (int)RegTypes.REG_SZ)
            throw new APIException("Invalid registry type returned for device property.");

        // sizof(char), 2 bytes, are removed to leave out the string terminator
        return Encoding.Unicode.GetString(buffer, 0, buffer.Length - sizeof(char));
    }

    private static string[] GetMultiStringProperty(IntPtr deviceInfoSet, SP_DEVINFO_DATA deviceInfoData, SPDRP property)
    {
        int regType;
        var buffer = GetProperty(deviceInfoSet, deviceInfoData, property, out regType);
        if (regType != (int)RegTypes.REG_MULTI_SZ)
            throw new APIException("Invalid registry type returned for device property.");

        var fullString = Encoding.Unicode.GetString(buffer);

        return fullString.Split(new[] { '\0' }, StringSplitOptions.RemoveEmptyEntries);
    }

    private static DeviceDetails GetDeviceDetails(string devicePath, IntPtr deviceInfoSet,
        SP_DEVINFO_DATA deviceInfoData)
    {
        var details = new DeviceDetails();
        details.DevicePath = devicePath;
        details.DeviceDescription = GetStringProperty(deviceInfoSet, deviceInfoData, SPDRP.SPDRP_DEVICEDESC);
        details.Manufacturer = GetStringProperty(deviceInfoSet, deviceInfoData, SPDRP.SPDRP_MFG);
        var hardwareIDs = GetMultiStringProperty(deviceInfoSet, deviceInfoData, SPDRP.SPDRP_HARDWAREID);

        var regex = new Regex("^USB\\\\VID_([0-9A-F]{4})&PID_([0-9A-F]{4})", RegexOptions.IgnoreCase);
        var foundVidPid = false;
        foreach (var hardwareID in hardwareIDs)
        {
            var match = regex.Match(hardwareID);
            if (match.Success)
            {
                details.VID = ushort.Parse(match.Groups[1].Value, NumberStyles.AllowHexSpecifier);
                details.PID = ushort.Parse(match.Groups[2].Value, NumberStyles.AllowHexSpecifier);
                foundVidPid = true;
                break;
            }
        }

        if (!foundVidPid)
            throw new APIException("Failed to find VID and PID for USB device. No hardware ID could be parsed.");

        return details;
    }


    public static DeviceDetails[] FindDevicesFromGuid(Guid guid)
    {
        var deviceInfoSet = IntPtr.Zero;
        var deviceList = new List<DeviceDetails>();
        try
        {
            deviceInfoSet = SetupDiGetClassDevs(ref guid, IntPtr.Zero, IntPtr.Zero,
                DIGCF_PRESENT | DIGCF_DEVICEINTERFACE);
            if (deviceInfoSet == FileIO.INVALID_HANDLE_VALUE)
                throw APIException.Win32("Failed to enumerate devices.");
            var memberIndex = 0;
            while (true)
            {
                // Begin with 0 and increment through the device information set until
                // no more devices are available.
                var deviceInterfaceData = new SP_DEVICE_INTERFACE_DATA();

                // The cbSize element of the deviceInterfaceData structure must be set to
                // the structure's size in bytes.
                // The size is 28 bytes for 32-bit code and 32 bytes for 64-bit code.
                deviceInterfaceData.cbSize = Marshal.SizeOf(deviceInterfaceData);

                bool success;

                success = SetupDiEnumDeviceInterfaces(deviceInfoSet, IntPtr.Zero, ref guid, memberIndex,
                    ref deviceInterfaceData);

                // Find out if a device information set was retrieved.
                if (!success)
                {
                    var lastError = Marshal.GetLastWin32Error();
                    if (lastError == ERROR_NO_MORE_ITEMS)
                        break;

                    throw APIException.Win32("Failed to get device interface.");
                }
                // A device is present.

                var bufferSize = 0;

                success = SetupDiGetDeviceInterfaceDetail
                (deviceInfoSet,
                    ref deviceInterfaceData,
                    IntPtr.Zero,
                    0,
                    ref bufferSize,
                    IntPtr.Zero);

                if (!success)
                    if (Marshal.GetLastWin32Error() != ERROR_INSUFFICIENT_BUFFER)
                        throw APIException.Win32("Failed to get interface details buffer size.");

                var detailDataBuffer = IntPtr.Zero;
                try
                {
                    // Allocate memory for the SP_DEVICE_INTERFACE_DETAIL_DATA structure using the returned buffer size.
                    detailDataBuffer = Marshal.AllocHGlobal(bufferSize);

                    // Store cbSize in the first bytes of the array. The number of bytes varies with 32- and 64-bit systems.

                    Marshal.WriteInt32(detailDataBuffer, IntPtr.Size == 4 ? 4 + Marshal.SystemDefaultCharSize : 8);

                    // Call SetupDiGetDeviceInterfaceDetail again.
                    // This time, pass a pointer to DetailDataBuffer
                    // and the returned required buffer size.

                    // build a DevInfo Data structure
                    var da = new SP_DEVINFO_DATA();
                    da.cbSize = Marshal.SizeOf(da);


                    success = SetupDiGetDeviceInterfaceDetail
                    (deviceInfoSet,
                        ref deviceInterfaceData,
                        detailDataBuffer,
                        bufferSize,
                        ref bufferSize,
                        ref da);

                    if (!success)
                        throw APIException.Win32("Failed to get device interface details.");


                    // Skip over cbsize (4 bytes) to get the address of the devicePathName.

                    var pDevicePathName = new IntPtr(detailDataBuffer.ToInt64() + 4);
                    var pathName = Marshal.PtrToStringUni(pDevicePathName);

                    // Get the String containing the devicePathName.

                    var details = GetDeviceDetails(pathName, deviceInfoSet, da);


                    deviceList.Add(details);
                }
                finally
                {
                    if (detailDataBuffer != IntPtr.Zero)
                    {
                        Marshal.FreeHGlobal(detailDataBuffer);
                        detailDataBuffer = IntPtr.Zero;
                    }
                }

                memberIndex++;
            }
        }
        finally
        {
            if (deviceInfoSet != IntPtr.Zero && deviceInfoSet != FileIO.INVALID_HANDLE_VALUE)
                SetupDiDestroyDeviceInfoList(deviceInfoSet);
        }

        return deviceList.ToArray();
    }
}
