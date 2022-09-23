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
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Windows.Win32;
using Windows.Win32.Devices.Usb;
using Windows.Win32.Foundation;
using Windows.Win32.Storage.FileSystem;
using Microsoft.Win32.SafeHandles;

namespace Nefarius.Drivers.WinUSB.API;

/// <summary>
///     Wrapper for a WinUSB device dealing with the WinUSB and additional interface handles
/// </summary>
internal partial class WinUSBDevice : IDisposable
{
    private IntPtr[] _addInterfaces;
    private SafeFileHandle _deviceHandle;
    private bool _disposed;
    private IntPtr _winUsbHandle = IntPtr.Zero;

    public int InterfaceCount => 1 + (_addInterfaces == null ? 0 : _addInterfaces.Length);

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~WinUSBDevice()
    {
        Dispose(false);
    }

    private void CheckNotDisposed()
    {
        if (_disposed)
            throw new ObjectDisposedException("USB device object has been disposed.");
    }

    // TODO: check if not disposed on methods (although this is already checked by USBDevice)

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        if (disposing)
        {
            // Dispose managed resources
            if (_deviceHandle != null && !_deviceHandle.IsInvalid)
                _deviceHandle.Dispose();
            _deviceHandle = null;
        }

        // Dispose unmanaged resources
        FreeWinUSB();
        _disposed = true;
    }

    private void FreeWinUSB()
    {
        if (_addInterfaces != null)
        {
            for (var i = 0; i < _addInterfaces.Length; i++) WinUsb_Free(_addInterfaces[i]);
            _addInterfaces = null;
        }

        if (_winUsbHandle != IntPtr.Zero)
            WinUsb_Free(_winUsbHandle);
        _winUsbHandle = IntPtr.Zero;
    }

    public USB_DEVICE_DESCRIPTOR GetDeviceDescriptor()
    {
        USB_DEVICE_DESCRIPTOR deviceDesc;
        uint transfered;
        var size = (uint)Marshal.SizeOf(typeof(USB_DEVICE_DESCRIPTOR));
        var success = WinUsb_GetDescriptor(_winUsbHandle, USB_DEVICE_DESCRIPTOR_TYPE,
            0, 0, out deviceDesc, size, out transfered);
        if (!success)
            throw APIException.Win32("Failed to get USB device descriptor.");

        if (transfered != size)
            throw APIException.Win32("Incomplete USB device descriptor.");

        return deviceDesc;
    }

    private int ReadStringDescriptor(byte index, ushort languageID, byte[] buffer)
    {
        uint transfered;
        var success = WinUsb_GetDescriptor(_winUsbHandle, USB_STRING_DESCRIPTOR_TYPE,
            index, languageID, buffer, (uint)buffer.Length, out transfered);
        if (!success)
            throw APIException.Win32("Failed to get USB string descriptor (" + index + ").");

        if (transfered == 0)
            throw new APIException("No data returned when reading USB descriptor.");

        int length = buffer[0];
        if (length != transfered)
            throw new APIException("Unexpected length when reading USB descriptor.");
        return length;
    }

    public ushort[] GetSupportedLanguageIDs()
    {
        var buffer = new byte[256];
        var length = ReadStringDescriptor(0, 0, buffer);
        length -= 2; // Skip length byte and descriptor type
        if (length < 0 || length % 2 != 0)
            throw new APIException("Unexpected length when reading supported languages.");

        var langIDs = new ushort[length / 2];
        Buffer.BlockCopy(buffer, 2, langIDs, 0, length);
        return langIDs;
    }

    public string GetStringDescriptor(byte index, ushort languageID)
    {
        var buffer = new byte[256];
        var length = ReadStringDescriptor(index, languageID, buffer);
        length -= 2; // Skip length byte and descriptor type
        if (length < 0)
            return null;
        var chars = Encoding.Unicode.GetChars(buffer, 2, length);
        return new string(chars);
    }

    public int ControlTransfer(byte requestType, byte request, ushort value, ushort index, ushort length, byte[] data)
    {
        uint bytesReturned = 0;
        WINUSB_SETUP_PACKET setupPacket;

        setupPacket.RequestType = requestType;
        setupPacket.Request = request;
        setupPacket.Value = value;
        setupPacket.Index = index;
        setupPacket.Length = length;

        var success = WinUsb_ControlTransfer(_winUsbHandle, setupPacket, data, length, ref bytesReturned, IntPtr.Zero);
        if (!success) // todo check bytes returned?
            throw APIException.Win32("Control transfer on WinUSB device failed.");
        return (int)bytesReturned;
    }
    
    public unsafe void OpenDevice(string devicePathName)
    {
        try
        {
            _deviceHandle = PInvoke.CreateFile(
                devicePathName,
                FILE_ACCESS_FLAGS.FILE_GENERIC_READ | FILE_ACCESS_FLAGS.FILE_GENERIC_WRITE,
                FILE_SHARE_MODE.FILE_SHARE_READ | FILE_SHARE_MODE.FILE_SHARE_WRITE,
                null,
                FILE_CREATION_DISPOSITION.OPEN_EXISTING,
                FILE_FLAGS_AND_ATTRIBUTES.FILE_ATTRIBUTE_NORMAL
                | FILE_FLAGS_AND_ATTRIBUTES.FILE_FLAG_OVERLAPPED,
            null
            );

            if (_deviceHandle.IsInvalid)
                throw APIException.Win32("Failed to open WinUSB device handle.");
            InitializeDevice();
        }
        catch (Exception)
        {
            if (_deviceHandle != null)
            {
                _deviceHandle.Dispose();
                _deviceHandle = null;
            }

            FreeWinUSB();
            throw;
        }
    }

    private IntPtr InterfaceHandle(int index)
    {
        if (index == 0)
            return _winUsbHandle;
        return _addInterfaces[index - 1];
    }

    public void GetInterfaceInfo(int interfaceIndex, out USB_INTERFACE_DESCRIPTOR descriptor,
        out WINUSB_PIPE_INFORMATION[] pipes)
    {
        var pipeList = new List<WINUSB_PIPE_INFORMATION>();
        var success = WinUsb_QueryInterfaceSettings(InterfaceHandle(interfaceIndex), 0, out descriptor);
        if (!success)
            throw APIException.Win32("Failed to get WinUSB device interface descriptor.");

        var interfaceHandle = InterfaceHandle(interfaceIndex);
        for (byte pipeIdx = 0; pipeIdx < descriptor.bNumEndpoints; pipeIdx++)
        {
            WINUSB_PIPE_INFORMATION pipeInfo;
            success = WinUsb_QueryPipe(interfaceHandle, 0, pipeIdx, out pipeInfo);

            pipeList.Add(pipeInfo);
            if (!success)
                throw APIException.Win32("Failed to get WinUSB device pipe information.");
        }

        pipes = pipeList.ToArray();
    }

    private void InitializeDevice()
    {
        bool success;

        success = WinUsb_Initialize(_deviceHandle, ref _winUsbHandle);

        if (!success)
            throw APIException.Win32("Failed to initialize WinUSB handle. Device might not be connected.");

        var interfaces = new List<IntPtr>();
        byte numAddInterfaces = 0;
        byte idx = 0;

        try
        {
            while (true)
            {
                var ifaceHandle = IntPtr.Zero;
                success = WinUsb_GetAssociatedInterface(_winUsbHandle, idx, out ifaceHandle);
                if (!success)
                {
                    if (Marshal.GetLastWin32Error() == ERROR_NO_MORE_ITEMS)
                        break;

                    throw APIException.Win32("Failed to enumerate interfaces for WinUSB device.");
                }

                interfaces.Add(ifaceHandle);
                idx++;
                numAddInterfaces++;
            }
        }
        finally
        {
            // Save interface handles (will be cleaned by Dispose)
            // also in case of exception (which is why it is in finally block),
            // because some handles might have already been opened and need
            // to be disposed.
            _addInterfaces = interfaces.ToArray();
        }

        // Bind handle (needed for overlapped I/O thread pool)
        ThreadPool.BindHandle(_deviceHandle);
        // TODO: bind interface handles as well? doesn't seem to be necessary
    }

    public void ReadPipe(int ifaceIndex, byte pipeID, byte[] buffer, int offset, int bytesToRead, out uint bytesRead)
    {
        bool success;
        unsafe
        {
            fixed (byte* pBuffer = buffer)
            {
                success = WinUsb_ReadPipe(InterfaceHandle(ifaceIndex), pipeID, pBuffer + offset, (uint)bytesToRead,
                    out bytesRead, IntPtr.Zero);
            }
        }

        if (!success)
            throw APIException.Win32("Failed to read pipe on WinUSB device.");
    }

    private unsafe void HandleOverlappedAPI(bool success, string errorMessage, NativeOverlapped* pOverlapped,
        USBAsyncResult result, int bytesTransferred)
    {
        if (!success)
        {
            if (Marshal.GetLastWin32Error() != (int)WIN32_ERROR.ERROR_IO_PENDING)
            {
                Overlapped.Unpack(pOverlapped);
                Overlapped.Free(pOverlapped);
                throw APIException.Win32(errorMessage);
            }
        }
        else
        {
            // Immediate success!
            Overlapped.Unpack(pOverlapped);
            Overlapped.Free(pOverlapped);

            result.OnCompletion(true, null, bytesTransferred, false);
            // is the callback still called in this case?? todo
        }
    }

    public void ReadPipeOverlapped(int ifaceIndex, byte pipeID, byte[] buffer, int offset, int bytesToRead,
        USBAsyncResult result)
    {
        var overlapped = new Overlapped();

        overlapped.AsyncResult = result;

        unsafe
        {
            NativeOverlapped* pOverlapped = null;
            uint bytesRead;

            pOverlapped = overlapped.Pack(PipeIOCallback, buffer);
            bool success;
            // Buffer is pinned already by overlapped.Pack
            fixed (byte* pBuffer = buffer)
            {
                success = WinUsb_ReadPipe(InterfaceHandle(ifaceIndex), pipeID, pBuffer + offset, (uint)bytesToRead,
                    out bytesRead, pOverlapped);
            }

            HandleOverlappedAPI(success, "Failed to asynchronously read pipe on WinUSB device.", pOverlapped, result,
                (int)bytesRead);
        }
    }

    public void WriteOverlapped(int ifaceIndex, byte pipeID, byte[] buffer, int offset, int bytesToWrite,
        USBAsyncResult result)
    {
        var overlapped = new Overlapped();
        overlapped.AsyncResult = result;

        unsafe
        {
            NativeOverlapped* pOverlapped = null;

            uint bytesWritten;
            pOverlapped = overlapped.Pack(PipeIOCallback, buffer);

            bool success;
            // Buffer is pinned already by overlapped.Pack
            fixed (byte* pBuffer = buffer)
            {
                success = WinUsb_WritePipe(InterfaceHandle(ifaceIndex), pipeID, pBuffer + offset, (uint)bytesToWrite,
                    out bytesWritten, pOverlapped);
            }

            HandleOverlappedAPI(success, "Failed to asynchronously write pipe on WinUSB device.", pOverlapped, result,
                (int)bytesWritten);
        }
    }


    public void ControlTransferOverlapped(byte requestType, byte request, ushort value, ushort index, ushort length,
        byte[] data, USBAsyncResult result)
    {
        uint bytesReturned = 0;
        WINUSB_SETUP_PACKET setupPacket;

        setupPacket.RequestType = requestType;
        setupPacket.Request = request;
        setupPacket.Value = value;
        setupPacket.Index = index;
        setupPacket.Length = length;

        var overlapped = new Overlapped();
        overlapped.AsyncResult = result;

        unsafe
        {
            NativeOverlapped* pOverlapped = null;
            pOverlapped = overlapped.Pack(PipeIOCallback, data);
            var success =
                WinUsb_ControlTransfer(_winUsbHandle, setupPacket, data, length, ref bytesReturned, pOverlapped);
            HandleOverlappedAPI(success, "Asynchronous control transfer on WinUSB device failed.", pOverlapped, result,
                (int)bytesReturned);
        }
    }

    private unsafe void PipeIOCallback(uint errorCode, uint numBytes, NativeOverlapped* pOverlapped)
    {
        try
        {
            Exception error = null;
            if (errorCode != 0)
                error = APIException.Win32("Asynchronous operation on WinUSB device failed.", (int)errorCode);
            var overlapped = Overlapped.Unpack(pOverlapped);
            var result = (USBAsyncResult)overlapped.AsyncResult;
            Overlapped.Free(pOverlapped);
            pOverlapped = null;

            result.OnCompletion(false, error, (int)numBytes, true);
        }
        finally
        {
            if (pOverlapped != null)
            {
                Overlapped.Unpack(pOverlapped);
                Overlapped.Free(pOverlapped);
            }
        }
    }

    public void AbortPipe(int ifaceIndex, byte pipeID)
    {
        var success = WinUsb_AbortPipe(InterfaceHandle(ifaceIndex), pipeID);
        if (!success)
            throw APIException.Win32("Failed to abort pipe on WinUSB device.");
    }

    public void WritePipe(int ifaceIndex, byte pipeID, byte[] buffer, int offset, int length)
    {
        uint bytesWritten;
        bool success;
        unsafe
        {
            fixed (byte* pBuffer = buffer)
            {
                success = WinUsb_WritePipe(InterfaceHandle(ifaceIndex), pipeID, pBuffer + offset, (uint)length,
                    out bytesWritten, IntPtr.Zero);
            }
        }

        if (!success || bytesWritten != length)
            throw APIException.Win32("Failed to write pipe on WinUSB device.");
    }

    public void FlushPipe(int ifaceIndex, byte pipeID)
    {
        var success = WinUsb_FlushPipe(InterfaceHandle(ifaceIndex), pipeID);
        if (!success)
            throw APIException.Win32("Failed to flush pipe on WinUSB device.");
    }

    public void SetPipePolicy(int ifaceIndex, byte pipeID, POLICY_TYPE policyType, bool value)
    {
        var byteVal = (byte)(value ? 1 : 0);
        var success = WinUsb_SetPipePolicy(InterfaceHandle(ifaceIndex), pipeID, (uint)policyType, 1, ref byteVal);
        if (!success)
            throw APIException.Win32("Failed to set WinUSB pipe policy.");
    }


    public void SetPipePolicy(int ifaceIndex, byte pipeID, POLICY_TYPE policyType, uint value)
    {
        var success = WinUsb_SetPipePolicy(InterfaceHandle(ifaceIndex), pipeID, (uint)policyType, 4, ref value);

        if (!success)
            throw APIException.Win32("Failed to set WinUSB pipe policy.");
    }


    public bool GetPipePolicyBool(int ifaceIndex, byte pipeID, POLICY_TYPE policyType)
    {
        byte result;
        uint length = 1;

        var success =
            WinUsb_GetPipePolicy(InterfaceHandle(ifaceIndex), pipeID, (uint)policyType, ref length, out result);
        if (!success || length != 1)
            throw APIException.Win32("Failed to get WinUSB pipe policy.");
        return result != 0;
    }


    public uint GetPipePolicyUInt(int ifaceIndex, byte pipeID, POLICY_TYPE policyType)
    {
        uint result;
        uint length = 4;
        var success =
            WinUsb_GetPipePolicy(InterfaceHandle(ifaceIndex), pipeID, (uint)policyType, ref length, out result);

        if (!success || length != 4)
            throw APIException.Win32("Failed to get WinUSB pipe policy.");
        return result;
    }
}
