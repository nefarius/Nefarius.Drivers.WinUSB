using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Windows.Win32;
using Windows.Win32.Devices.Usb;
using Windows.Win32.Storage.FileSystem;
using JetBrains.Annotations;

namespace Nefarius.Drivers.WinUSB.API;

internal partial class WinUSBDevice
{
    [UsedImplicitly]
    public unsafe USB_DEVICE_DESCRIPTOR GetDeviceDescriptor()
    {
        USB_DEVICE_DESCRIPTOR deviceDesc;
        var size = (uint)Marshal.SizeOf(typeof(USB_DEVICE_DESCRIPTOR));

        uint transferred = 0;
        var success = PInvoke.WinUsb_GetDescriptor(
            _winUsbHandle.ToPointer(),
            (byte)PInvoke.USB_DEVICE_DESCRIPTOR_TYPE,
            0,
            0,
            (byte*)&deviceDesc,
            size,
            &transferred
        );

        if (!success)
            throw APIException.Win32("Failed to get USB device descriptor.");

        if (transferred != size)
            throw APIException.Win32("Incomplete USB device descriptor.");

        return deviceDesc;
    }

    [UsedImplicitly]
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

    [UsedImplicitly]
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

    [UsedImplicitly]
    public unsafe int ControlTransfer(byte requestType, byte request, ushort value, ushort index, ushort length,
        byte[] data)
    {
        uint bytesReturned = 0;
        WINUSB_SETUP_PACKET setupPacket;

        setupPacket.RequestType = requestType;
        setupPacket.Request = request;
        setupPacket.Value = value;
        setupPacket.Index = index;
        setupPacket.Length = length;

        fixed (byte* dataPtr = data)
        {
            var success = PInvoke.WinUsb_ControlTransfer(_winUsbHandle.ToPointer(), setupPacket, dataPtr, length,
                &bytesReturned);
            if (!success) // todo check bytes returned?
                throw APIException.Win32("Control transfer on WinUSB device failed.");
            return (int)bytesReturned;
        }
    }

    [UsedImplicitly]
    // ReSharper disable once RedundantUnsafeContext
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

    [UsedImplicitly]
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

    [UsedImplicitly]
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

    [UsedImplicitly]
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

    [UsedImplicitly]
    public unsafe void ControlTransferOverlapped(byte requestType, byte request, ushort value, ushort index,
        ushort length,
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

        fixed (byte* pBuffer = data)
        {
            NativeOverlapped* pOverlapped = null;
            pOverlapped = overlapped.Pack(PipeIOCallback, data);
            var success =
                PInvoke.WinUsb_ControlTransfer(_winUsbHandle.ToPointer(), setupPacket, pBuffer, length, &bytesReturned,
                    pOverlapped);
            HandleOverlappedAPI(success, "Asynchronous control transfer on WinUSB device failed.", pOverlapped, result,
                (int)bytesReturned);
        }
    }

    [UsedImplicitly]
    public void AbortPipe(int ifaceIndex, byte pipeID)
    {
        var success = WinUsb_AbortPipe(InterfaceHandle(ifaceIndex), pipeID);
        if (!success)
            throw APIException.Win32("Failed to abort pipe on WinUSB device.");
    }

    [UsedImplicitly]
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

    [UsedImplicitly]
    public void FlushPipe(int ifaceIndex, byte pipeID)
    {
        var success = WinUsb_FlushPipe(InterfaceHandle(ifaceIndex), pipeID);
        if (!success)
            throw APIException.Win32("Failed to flush pipe on WinUSB device.");
    }

    [UsedImplicitly]
    public void SetPipePolicy(int ifaceIndex, byte pipeID, POLICY_TYPE policyType, bool value)
    {
        var byteVal = (byte)(value ? 1 : 0);
        var success = WinUsb_SetPipePolicy(InterfaceHandle(ifaceIndex), pipeID, (uint)policyType, 1, ref byteVal);
        if (!success)
            throw APIException.Win32("Failed to set WinUSB pipe policy.");
    }

    [UsedImplicitly]
    public void SetPipePolicy(int ifaceIndex, byte pipeID, POLICY_TYPE policyType, uint value)
    {
        var success = WinUsb_SetPipePolicy(InterfaceHandle(ifaceIndex), pipeID, (uint)policyType, 4, ref value);

        if (!success)
            throw APIException.Win32("Failed to set WinUSB pipe policy.");
    }

    [UsedImplicitly]
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

    [UsedImplicitly]
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
