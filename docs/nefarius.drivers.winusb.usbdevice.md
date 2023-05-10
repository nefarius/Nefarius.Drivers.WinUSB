# USBDevice

Namespace: Nefarius.Drivers.WinUSB

The UsbDevice class represents a single WinUSB device.

```csharp
public class USBDevice : System.IDisposable
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [USBDevice](./nefarius.drivers.winusb.usbdevice.md)<br>
Implements [IDisposable](https://docs.microsoft.com/en-us/dotnet/api/system.idisposable)

## Properties

### **Pipes**

Collection of all pipes available on the USB device

```csharp
public USBPipeCollection Pipes { get; private set; }
```

#### Property Value

[USBPipeCollection](./nefarius.drivers.winusb.usbpipecollection.md)<br>

### **Interfaces**

Collection of all interfaces available on the USB device

```csharp
public USBInterfaceCollection Interfaces { get; private set; }
```

#### Property Value

[USBInterfaceCollection](./nefarius.drivers.winusb.usbinterfacecollection.md)<br>

### **Descriptor**

Device descriptor with information about the device

```csharp
public USBDeviceDescriptor Descriptor { get; }
```

#### Property Value

[USBDeviceDescriptor](./nefarius.drivers.winusb.usbdevicedescriptor.md)<br>

### **PowerPolicy**

The power policy settings for this device

```csharp
public USBPowerPolicy PowerPolicy { get; }
```

#### Property Value

[USBPowerPolicy](./nefarius.drivers.winusb.usbpowerpolicy.md)<br>

### **ControlPipeTimeout**

Specifies the timeout in milliseconds for control pipe operations. If a control transfer does not finish within the
 specified time it will fail.
 When set to zero, no timeout is used. Default value is 5000 milliseconds.

```csharp
public int ControlPipeTimeout { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

## Constructors

### **USBDevice(USBDeviceInfo)**

Constructs a new USB device

```csharp
public USBDevice(USBDeviceInfo deviceInfo)
```

#### Parameters

`deviceInfo` [USBDeviceInfo](./nefarius.drivers.winusb.usbdeviceinfo.md)<br>
USB device info of the device to create

### **USBDevice(String)**

Constructs a new USB device

```csharp
public USBDevice(string devicePathName)
```

#### Parameters

`devicePathName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Device path name of the USB device to create

## Methods

### **Finalize()**

Finalizer for the UsbDevice. Disposes all unmanaged handles.

```csharp
protected void Finalize()
```

### **Dispose(Boolean)**

Disposes the object

```csharp
protected void Dispose(bool disposing)
```

#### Parameters

`disposing` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

                Indicates whether Dispose was called manually (true) or by
                the garbage collector (false) via the destructor.

### **Dispose()**

Disposes the UsbDevice including all unmanaged WinUSB handles. This function
 should be called when the UsbDevice object is no longer in use, otherwise
 unmanaged handles will remain open until the garbage collector finalizes the
 object.

```csharp
public void Dispose()
```

### **ControlTransfer(Byte, Byte, Int32, Int32, Span&lt;Byte&gt;, Int32)**

Initiates a control transfer over the default control endpoint. This method allows both IN and OUT direction
 transfers, depending
 on the highest bit of the  parameter. Alternatively,
 [USBDevice.ControlIn(Byte, Byte, Int32, Int32, Span&lt;Byte&gt;, Int32)](./nefarius.drivers.winusb.usbdevice.md#controlinbyte-byte-int32-int32-spanbyte-int32) and
 [USBDevice.ControlOut(Byte, Byte, Int32, Int32, Span&lt;Byte&gt;, Int32)](./nefarius.drivers.winusb.usbdevice.md#controloutbyte-byte-int32-int32-spanbyte-int32) can be used for control transfers in a specific direction,
 which is the recommended way because
 it prevents using the wrong direction accidentally. Use the ControlTransfer method when the direction is not known
 at compile time.

```csharp
public int ControlTransfer(byte requestType, byte request, int value, int index, Span<byte> buffer, int length)
```

#### Parameters

`requestType` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
The setup packet request type.

`request` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
The setup packet device request.

`value` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                The value member in the setup packet. Its meaning depends on the request. Value should be between
                zero and 65535 (0xFFFF).

`index` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                The index member in the setup packet. Its meaning depends on the request. Index should be between
                zero and 65535 (0xFFFF).

`buffer` [Span&lt;Byte&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.span-1)<br>

                The data to transfer in the data stage of the control. When the transfer is in the IN direction the data received
                will be
                written to this buffer. For an OUT direction transfer the contents of the buffer are written sent through the pipe.

`length` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                Length of the data to transfer. Must be equal to or less than the length of .
                The setup packet's length member will be set to this length.

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The number of bytes received from the device.

### **BeginControlTransfer(Byte, Byte, Int32, Int32, Byte[], Int32, AsyncCallback, Object)**

Initiates an asynchronous control transfer over the default control endpoint. This method allows both IN and OUT
 direction transfers, depending
 on the highest bit of the  parameter. Alternatively,
 [USBDevice.BeginControlIn(Byte, Byte, Int32, Int32, Byte[], Int32, AsyncCallback, Object)](./nefarius.drivers.winusb.usbdevice.md#begincontrolinbyte-byte-int32-int32-byte-int32-asynccallback-object) and
 [USBDevice.BeginControlIn(Byte, Byte, Int32, Int32, Byte[], Int32, AsyncCallback, Object)](./nefarius.drivers.winusb.usbdevice.md#begincontrolinbyte-byte-int32-int32-byte-int32-asynccallback-object) can be used for asynchronous
 control transfers in a specific direction, which is
 the recommended way because it prevents using the wrong direction accidentally. Use the BeginControlTransfer method
 when the direction is not
 known at compile time.

```csharp
public IAsyncResult BeginControlTransfer(byte requestType, byte request, int value, int index, Byte[] buffer, int length, AsyncCallback userCallback, object stateObject)
```

#### Parameters

`requestType` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
The setup packet request type.

`request` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
The setup packet device request.

`value` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                The value member in the setup packet. Its meaning depends on the request. Value should be between
                zero and 65535 (0xFFFF).

`index` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                The index member in the setup packet. Its meaning depends on the request. Index should be between
                zero and 65535 (0xFFFF).

`buffer` [Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

                The data to transfer in the data stage of the control. When the transfer is in the IN direction the data received
                will be
                written to this buffer. For an OUT direction transfer the contents of the buffer are written sent through the pipe.
                Note: This buffer is not allowed
                to change for the duration of the asynchronous operation.

`length` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                Length of the data to transfer. Must be equal to or less than the length of
                . The setup packet's length member will be set to this length.

`userCallback` [AsyncCallback](https://docs.microsoft.com/en-us/dotnet/api/system.asynccallback)<br>

                An optional asynchronous callback, to be called when the control transfer is complete. Can
                be null if no callback is required.

`stateObject` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

                A user-provided object that distinguishes this particular asynchronous operation. Can be null
                if not required.

#### Returns

[IAsyncResult](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncresult)<br>

                An  object representing the asynchronous control transfer, which could still be
                pending.

**Remarks:**

This method always completes immediately even if the operation is still pending. The [IAsyncResult](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncresult)
 object returned represents the operation
 and must be passed to [USBDevice.EndControlTransfer(IAsyncResult)](./nefarius.drivers.winusb.usbdevice.md#endcontroltransferiasyncresult) to retrieve the result of the operation. For every call to
 this method a matching call to
 [USBDevice.EndControlTransfer(IAsyncResult)](./nefarius.drivers.winusb.usbdevice.md#endcontroltransferiasyncresult) must be made. When  specifies a callback
 function, this function will be called when the operation is completed. The optional
  parameter can be used to pass user-defined information to this callback or the
 [IAsyncResult](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncresult). The [IAsyncResult](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncresult)
 also provides an event handle ([IAsyncResult.AsyncWaitHandle](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncresult.asyncwaithandle)) that will be triggered when the
 operation is complete as well.

### **BeginControlTransfer(Byte, Byte, Int32, Int32, Byte[], AsyncCallback, Object)**

Initiates an asynchronous control transfer over the default control endpoint. This method allows both IN and OUT
 direction transfers, depending
 on the highest bit of the  parameter. Alternatively,
 [USBDevice.BeginControlIn(Byte, Byte, Int32, Int32, Byte[], Int32, AsyncCallback, Object)](./nefarius.drivers.winusb.usbdevice.md#begincontrolinbyte-byte-int32-int32-byte-int32-asynccallback-object) and
 [USBDevice.BeginControlIn(Byte, Byte, Int32, Int32, Byte[], Int32, AsyncCallback, Object)](./nefarius.drivers.winusb.usbdevice.md#begincontrolinbyte-byte-int32-int32-byte-int32-asynccallback-object) can be used for asynchronous
 control transfers in a specific direction, which is
 the recommended way because it prevents using the wrong direction accidentally. Use the BeginControlTransfer method
 when the direction is not
 known at compile time.

```csharp
public IAsyncResult BeginControlTransfer(byte requestType, byte request, int value, int index, Byte[] buffer, AsyncCallback userCallback, object stateObject)
```

#### Parameters

`requestType` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
The setup packet request type.

`request` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
The setup packet device request.

`value` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                The value member in the setup packet. Its meaning depends on the request. Value should be between
                zero and 65535 (0xFFFF).

`index` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                The index member in the setup packet. Its meaning depends on the request. Index should be between
                zero and 65535 (0xFFFF).

`buffer` [Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

                The data to transfer in the data stage of the control. When the transfer is in the IN direction the data received
                will be
                written to this buffer. For an OUT direction transfer the contents of the buffer are written sent through the pipe.
                The setup packet's length member will
                be set to the length of this buffer. Note: This buffer is not allowed to change for the duration of the
                asynchronous operation.

`userCallback` [AsyncCallback](https://docs.microsoft.com/en-us/dotnet/api/system.asynccallback)<br>

                An optional asynchronous callback, to be called when the control transfer is complete. Can
                be null if no callback is required.

`stateObject` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

                A user-provided object that distinguishes this particular asynchronous operation. Can be null
                if not required.

#### Returns

[IAsyncResult](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncresult)<br>

                An  object representing the asynchronous control transfer, which could still be
                pending.

**Remarks:**

This method always completes immediately even if the operation is still pending. The [IAsyncResult](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncresult)
 object returned represents the operation
 and must be passed to [USBDevice.EndControlTransfer(IAsyncResult)](./nefarius.drivers.winusb.usbdevice.md#endcontroltransferiasyncresult) to retrieve the result of the operation. For every call to
 this method a matching call to
 [USBDevice.EndControlTransfer(IAsyncResult)](./nefarius.drivers.winusb.usbdevice.md#endcontroltransferiasyncresult) must be made. When  specifies a callback
 function, this function will be called when the operation is completed. The optional
  parameter can be used to pass user-defined information to this callback or the
 [IAsyncResult](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncresult). The [IAsyncResult](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncresult)
 also provides an event handle ([IAsyncResult.AsyncWaitHandle](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncresult.asyncwaithandle)) that will be triggered when the
 operation is complete as well.

### **EndControlTransfer(IAsyncResult)**

Waits for a pending asynchronous control transfer to complete.

```csharp
public int EndControlTransfer(IAsyncResult asyncResult)
```

#### Parameters

`asyncResult` [IAsyncResult](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncresult)<br>

                The  object representing the asynchronous operation,
                as returned by one of the ControlIn, ControlOut or ControlTransfer methods.

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The number of bytes transferred during the operation.

**Remarks:**

Every asynchronous control transfer must have a matching call to [USBDevice.EndControlTransfer(IAsyncResult)](./nefarius.drivers.winusb.usbdevice.md#endcontroltransferiasyncresult) to dispose
 of any resources used and to retrieve the result of the operation. When the operation was successful the method
 returns the number
 of bytes that were transferred. If an error occurred during the operation this method will throw the exceptions that
 would
 otherwise have occurred during the operation. If the operation is not yet finished EndControlTransfer will wait for
 the
 operation to finish before returning.

### **ControlTransfer(Byte, Byte, Int32, Int32, Span&lt;Byte&gt;)**

Initiates a control transfer over the default control endpoint. This method allows both IN and OUT direction
 transfers, depending
 on the highest bit of the  parameter). Alternatively,
 [USBDevice.ControlIn(Byte, Byte, Int32, Int32, Int32)](./nefarius.drivers.winusb.usbdevice.md#controlinbyte-byte-int32-int32-int32) and
 [USBDevice.ControlOut(Byte, Byte, Int32, Int32, Span&lt;Byte&gt;)](./nefarius.drivers.winusb.usbdevice.md#controloutbyte-byte-int32-int32-spanbyte) can be used for control transfers in a specific direction,
 which is the recommended way because
 it prevents using the wrong direction accidentally. Use the ControlTransfer method when the direction is not known
 at compile time.

```csharp
public int ControlTransfer(byte requestType, byte request, int value, int index, Span<byte> buffer)
```

#### Parameters

`requestType` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
The setup packet request type.

`request` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
The setup packet device request.

`value` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                The value member in the setup packet. Its meaning depends on the request. Value should be between
                zero and 65535 (0xFFFF).

`index` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                The index member in the setup packet. Its meaning depends on the request. Index should be between
                zero and 65535 (0xFFFF).

`buffer` [Span&lt;Byte&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.span-1)<br>

                The data to transfer in the data stage of the control. When the transfer is in the IN direction the data received
                will be
                written to this buffer. For an OUT direction transfer the contents of the buffer are written sent through the pipe.
                The length of this
                buffer is used as the number of bytes in the control transfer. The setup packet's length member will be set to this
                length as well.

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The number of bytes received from the device.

### **ControlTransfer(Byte, Byte, Int32, Int32)**

Initiates a control transfer without a data stage over the default control endpoint. This method allows both IN and
 OUT direction transfers, depending
 on the highest bit of the  parameter). Alternatively,
 [USBDevice.ControlIn(Byte, Byte, Int32, Int32)](./nefarius.drivers.winusb.usbdevice.md#controlinbyte-byte-int32-int32) and
 [USBDevice.ControlOut(Byte, Byte, Int32, Int32)](./nefarius.drivers.winusb.usbdevice.md#controloutbyte-byte-int32-int32) can be used for control transfers in a specific direction, which is
 the recommended way because
 it prevents using the wrong direction accidentally. Use the ControlTransfer method when the direction is not known
 at compile time.

```csharp
public void ControlTransfer(byte requestType, byte request, int value, int index)
```

#### Parameters

`requestType` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
The setup packet request type.

`request` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
The setup packet device request.

`value` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                The value member in the setup packet. Its meaning depends on the request. Value should be between
                zero and 65535 (0xFFFF).

`index` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                The index member in the setup packet. Its meaning depends on the request. Index should be between
                zero and 65535 (0xFFFF).

### **ControlIn(Byte, Byte, Int32, Int32, Int32)**

Initiates a control transfer over the default control endpoint. The request should have an IN direction (specified
 by the highest bit
 of the  parameter). A buffer to receive the data is automatically created by this
 method.

```csharp
public Byte[] ControlIn(byte requestType, byte request, int value, int index, int length)
```

#### Parameters

`requestType` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

                The setup packet request type. The request type must specify the IN direction (highest bit
                set).

`request` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
The setup packet device request.

`value` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                The value member in the setup packet. Its meaning depends on the request. Value should be between
                zero and 65535 (0xFFFF).

`index` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                The index member in the setup packet. Its meaning depends on the request. Index should be between
                zero and 65535 (0xFFFF).

`length` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                Length of the data to transfer. A buffer will be created with this length and the length member of the setup packet
                will be set to this length.

#### Returns

[Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
A buffer containing the data transferred.

**Remarks:**

This routine initially allocates a buffer to hold the  bytes of data expected from the
 device.
 If the device responds with less data than expected, this routine will allocate a smaller buffer to copy and return
 only the bytes actually received.

### **ControlIn(Byte, Byte, Int32, Int32, Span&lt;Byte&gt;, Int32)**

Initiates a control transfer over the default control endpoint. The request should have an IN direction (specified
 by the highest bit
 of the  parameter).

```csharp
public int ControlIn(byte requestType, byte request, int value, int index, Span<byte> buffer, int length)
```

#### Parameters

`requestType` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

                The setup packet request type. The request type must specify the IN direction (highest bit
                set).

`request` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
The setup packet device request.

`value` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                The value member in the setup packet. Its meaning depends on the request. Value should be between
                zero and 65535 (0xFFFF).

`index` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                The index member in the setup packet. Its meaning depends on the request. Index should be between
                zero and 65535 (0xFFFF).

`buffer` [Span&lt;Byte&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.span-1)<br>
The buffer that will receive the data transferred.

`length` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                Length of the data to transfer. The length member of the setup packet will be set to this length. The buffer
                specified
                by the  parameter should have at least this length.

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The number of bytes received from the device.

### **ControlIn(Byte, Byte, Int32, Int32, Span&lt;Byte&gt;)**

Initiates a control transfer over the default control endpoint. The request should have an IN direction (specified
 by the highest bit
 of the  parameter). The length of buffer given by the 
 parameter will dictate
 the number of bytes that are transferred and the value of the setup packet's length member.

```csharp
public int ControlIn(byte requestType, byte request, int value, int index, Span<byte> buffer)
```

#### Parameters

`requestType` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

                The setup packet request type. The request type must specify the IN direction (highest bit
                set).

`request` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
The setup packet device request.

`value` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                The value member in the setup packet. Its meaning depends on the request. Value should be between
                zero and 65535 (0xFFFF).

`index` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                The index member in the setup packet. Its meaning depends on the request. Index should be between
                zero and 65535 (0xFFFF).

`buffer` [Span&lt;Byte&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.span-1)<br>

                The buffer that will receive the data transferred. The length of this buffer will be the number of
                bytes transferred.

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The number of bytes received from the device.

### **ControlIn(Byte, Byte, Int32, Int32)**

Initiates a control transfer without a data stage over the default control endpoint. The request should have an IN
 direction (specified by the highest bit
 of the  parameter). The setup packets' length member will be set to zero.

```csharp
public void ControlIn(byte requestType, byte request, int value, int index)
```

#### Parameters

`requestType` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

                The setup packet request type. The request type must specify the IN direction (highest bit
                set).

`request` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
The setup packet device request.

`value` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                The value member in the setup packet. Its meaning depends on the request. Value should be between
                zero and 65535 (0xFFFF).

`index` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                The index member in the setup packet. Its meaning depends on the request. Index should be between
                zero and 65535 (0xFFFF).

### **ControlOut(Byte, Byte, Int32, Int32, Span&lt;Byte&gt;, Int32)**

Initiates a control transfer over the default control endpoint. The request should have an OUT direction (specified
 by the highest bit
 of the  parameter).

```csharp
public void ControlOut(byte requestType, byte request, int value, int index, Span<byte> buffer, int length)
```

#### Parameters

`requestType` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

                The setup packet request type. The request type must specify the OUT direction (highest bit
                cleared).

`request` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
The setup packet device request.

`value` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                The value member in the setup packet. Its meaning depends on the request. Value should be between
                zero and 65535 (0xFFFF).

`index` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                The index member in the setup packet. Its meaning depends on the request. Index should be between
                zero and 65535 (0xFFFF).

`buffer` [Span&lt;Byte&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.span-1)<br>
A buffer containing the data to transfer in the data stage.

`length` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                Length of the data to transfer. Only the first  bytes of  will
                be transferred.
                The setup packet's length parameter is set to this length.

### **ControlOut(Byte, Byte, Int32, Int32, Span&lt;Byte&gt;)**

Initiates a control transfer over the default control endpoint. The request should have an OUT direction (specified
 by the highest bit
 of the  parameter).

```csharp
public void ControlOut(byte requestType, byte request, int value, int index, Span<byte> buffer)
```

#### Parameters

`requestType` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

                The setup packet request type. The request type must specify the OUT direction (highest bit
                cleared).

`request` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
The setup packet device request.

`value` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                The value member in the setup packet. Its meaning depends on the request. Value should be between
                zero and 65535 (0xFFFF).

`index` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                The index member in the setup packet. Its meaning depends on the request. Index should be between
                zero and 65535 (0xFFFF).

`buffer` [Span&lt;Byte&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.span-1)<br>

                A buffer containing the data to transfer in the data stage. The complete buffer is transferred. The setup packet's
                length
                parameter is set to the length of this buffer.

### **ControlOut(Byte, Byte, Int32, Int32)**

Initiates a control transfer without a data stage over the default control endpoint. The request should have an OUT
 direction (specified by the highest bit
 of the  parameter. The setup packets' length member will be set to zero.

```csharp
public void ControlOut(byte requestType, byte request, int value, int index)
```

#### Parameters

`requestType` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

                The setup packet request type. The request type must specify the OUT direction (highest bit
                cleared).

`request` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
The setup packet device request.

`value` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                The value member in the setup packet. Its meaning depends on the request. Value should be between
                zero and 65535 (0xFFFF).

`index` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                The index member in the setup packet. Its meaning depends on the request. Index should be between
                zero and 65535 (0xFFFF).

### **BeginControlTransfer(Byte, Byte, Int32, Int32, AsyncCallback, Object)**

Initiates an asynchronous control transfer without a data stage over the default control endpoint. This method
 allows both IN and OUT direction transfers, depending
 on the highest bit of the  parameter. Alternatively,
 [USBDevice.BeginControlIn(Byte, Byte, Int32, Int32, Byte[], Int32, AsyncCallback, Object)](./nefarius.drivers.winusb.usbdevice.md#begincontrolinbyte-byte-int32-int32-byte-int32-asynccallback-object) and
 [USBDevice.BeginControlIn(Byte, Byte, Int32, Int32, Byte[], Int32, AsyncCallback, Object)](./nefarius.drivers.winusb.usbdevice.md#begincontrolinbyte-byte-int32-int32-byte-int32-asynccallback-object) can be used for asynchronous
 control transfers in a specific direction, which is
 the recommended way because it prevents using the wrong direction accidentally. Use the BeginControlTransfer method
 when the direction is not
 known at compile time.

```csharp
public IAsyncResult BeginControlTransfer(byte requestType, byte request, int value, int index, AsyncCallback userCallback, object stateObject)
```

#### Parameters

`requestType` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
The setup packet request type.

`request` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
The setup packet device request.

`value` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                The value member in the setup packet. Its meaning depends on the request. Value should be between
                zero and 65535 (0xFFFF).

`index` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                The index member in the setup packet. Its meaning depends on the request. Index should be between
                zero and 65535 (0xFFFF).

`userCallback` [AsyncCallback](https://docs.microsoft.com/en-us/dotnet/api/system.asynccallback)<br>

                An optional asynchronous callback, to be called when the control transfer is complete. Can
                be null if no callback is required.

`stateObject` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

                A user-provided object that distinguishes this particular asynchronous operation. Can be null
                if not required.

#### Returns

[IAsyncResult](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncresult)<br>

                An  object representing the asynchronous control transfer, which could still be
                pending.

**Remarks:**

This method always completes immediately even if the operation is still pending. The [IAsyncResult](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncresult)
 object returned represents the operation
 and must be passed to [USBDevice.EndControlTransfer(IAsyncResult)](./nefarius.drivers.winusb.usbdevice.md#endcontroltransferiasyncresult) to retrieve the result of the operation. For every call to
 this method a matching call to
 [USBDevice.EndControlTransfer(IAsyncResult)](./nefarius.drivers.winusb.usbdevice.md#endcontroltransferiasyncresult) must be made. When  specifies a callback
 function, this function will be called when the operation is completed. The optional
  parameter can be used to pass user-defined information to this callback or the
 [IAsyncResult](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncresult). The [IAsyncResult](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncresult)
 also provides an event handle ([IAsyncResult.AsyncWaitHandle](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncresult.asyncwaithandle)) that will be triggered when the
 operation is complete as well.

### **BeginControlIn(Byte, Byte, Int32, Int32, Byte[], Int32, AsyncCallback, Object)**

Initiates an asynchronous control transfer over the default control endpoint. The request should have an IN
 direction (specified by the highest bit
 of the  parameter).

```csharp
public IAsyncResult BeginControlIn(byte requestType, byte request, int value, int index, Byte[] buffer, int length, AsyncCallback userCallback, object stateObject)
```

#### Parameters

`requestType` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

                The setup packet request type. The request type must specify the IN direction (highest bit
                set).

`request` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
The setup packet device request.

`value` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                The value member in the setup packet. Its meaning depends on the request. Value should be between
                zero and 65535 (0xFFFF).

`index` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                The index member in the setup packet. Its meaning depends on the request. Index should be between
                zero and 65535 (0xFFFF).

`buffer` [Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
The buffer that will receive the data transferred.

`length` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                Length of the data to transfer. Must be equal to or less than the length of
                . The setup packet's length member will be set to this length.

`userCallback` [AsyncCallback](https://docs.microsoft.com/en-us/dotnet/api/system.asynccallback)<br>

                An optional asynchronous callback, to be called when the control transfer is complete. Can
                be null if no callback is required.

`stateObject` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

                A user-provided object that distinguishes this particular asynchronous operation. Can be null
                if not required.

#### Returns

[IAsyncResult](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncresult)<br>

                An  object representing the asynchronous control transfer, which could still be
                pending.

**Remarks:**

This method always completes immediately even if the operation is still pending. The [IAsyncResult](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncresult)
 object returned represents the operation
 and must be passed to [USBDevice.EndControlTransfer(IAsyncResult)](./nefarius.drivers.winusb.usbdevice.md#endcontroltransferiasyncresult) to retrieve the result of the operation. For every call to
 this method a matching call to
 [USBDevice.EndControlTransfer(IAsyncResult)](./nefarius.drivers.winusb.usbdevice.md#endcontroltransferiasyncresult) must be made. When  specifies a callback
 function, this function will be called when the operation is completed. The optional
  parameter can be used to pass user-defined information to this callback or the
 [IAsyncResult](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncresult). The [IAsyncResult](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncresult)
 also provides an event handle ([IAsyncResult.AsyncWaitHandle](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncresult.asyncwaithandle)) that will be triggered when the
 operation is complete as well.

### **BeginControlIn(Byte, Byte, Int32, Int32, Byte[], AsyncCallback, Object)**

Initiates an asynchronous control transfer over the default control endpoint. The request should have an IN
 direction (specified by the highest bit
 of the  parameter).

```csharp
public IAsyncResult BeginControlIn(byte requestType, byte request, int value, int index, Byte[] buffer, AsyncCallback userCallback, object stateObject)
```

#### Parameters

`requestType` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

                The setup packet request type. The request type must specify the IN direction (highest bit
                set).

`request` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
The setup packet device request.

`value` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                The value member in the setup packet. Its meaning depends on the request. Value should be between
                zero and 65535 (0xFFFF).

`index` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                The index member in the setup packet. Its meaning depends on the request. Index should be between
                zero and 65535 (0xFFFF).

`buffer` [Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

                The buffer that will receive the data transferred. The setup packet's length member will be set to
                the length of this buffer.

`userCallback` [AsyncCallback](https://docs.microsoft.com/en-us/dotnet/api/system.asynccallback)<br>

                An optional asynchronous callback, to be called when the control transfer is complete. Can
                be null if no callback is required.

`stateObject` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

                A user-provided object that distinguishes this particular asynchronous operation. Can be null
                if not required.

#### Returns

[IAsyncResult](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncresult)<br>

                An  object representing the asynchronous control transfer, which could still be
                pending.

**Remarks:**

This method always completes immediately even if the operation is still pending. The [IAsyncResult](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncresult)
 object returned represents the operation
 and must be passed to [USBDevice.EndControlTransfer(IAsyncResult)](./nefarius.drivers.winusb.usbdevice.md#endcontroltransferiasyncresult) to retrieve the result of the operation. For every call to
 this method a matching call to
 [USBDevice.EndControlTransfer(IAsyncResult)](./nefarius.drivers.winusb.usbdevice.md#endcontroltransferiasyncresult) must be made. When  specifies a callback
 function, this function will be called when the operation is completed. The optional
  parameter can be used to pass user-defined information to this callback or the
 [IAsyncResult](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncresult). The [IAsyncResult](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncresult)
 also provides an event handle ([IAsyncResult.AsyncWaitHandle](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncresult.asyncwaithandle)) that will be triggered when the
 operation is complete as well.

### **BeginControlIn(Byte, Byte, Int32, Int32, AsyncCallback, Object)**

Initiates an asynchronous control transfer without a data stage over the default control endpoint.
 The request should have an IN direction (specified by the highest bit of the 
 parameter).
 The setup packets' length member will be set to zero.

```csharp
public IAsyncResult BeginControlIn(byte requestType, byte request, int value, int index, AsyncCallback userCallback, object stateObject)
```

#### Parameters

`requestType` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

                The setup packet request type. The request type must specify the IN direction (highest bit
                set).

`request` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
The setup packet device request.

`value` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                The value member in the setup packet. Its meaning depends on the request. Value should be between
                zero and 65535 (0xFFFF).

`index` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                The index member in the setup packet. Its meaning depends on the request. Index should be between
                zero and 65535 (0xFFFF).

`userCallback` [AsyncCallback](https://docs.microsoft.com/en-us/dotnet/api/system.asynccallback)<br>

                An optional asynchronous callback, to be called when the control transfer is complete. Can
                be null if no callback is required.

`stateObject` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

                A user-provided object that distinguishes this particular asynchronous operation. Can be null
                if not required.

#### Returns

[IAsyncResult](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncresult)<br>

                An  object representing the asynchronous control transfer, which could still be
                pending.

**Remarks:**

This method always completes immediately even if the operation is still pending. The [IAsyncResult](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncresult)
 object returned represents the operation
 and must be passed to [USBDevice.EndControlTransfer(IAsyncResult)](./nefarius.drivers.winusb.usbdevice.md#endcontroltransferiasyncresult) to retrieve the result of the operation. For every call to
 this method a matching call to
 [USBDevice.EndControlTransfer(IAsyncResult)](./nefarius.drivers.winusb.usbdevice.md#endcontroltransferiasyncresult) must be made. When  specifies a callback
 function, this function will be called when the operation is completed. The optional
  parameter can be used to pass user-defined information to this callback or the
 [IAsyncResult](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncresult). The [IAsyncResult](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncresult)
 also provides an event handle ([IAsyncResult.AsyncWaitHandle](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncresult.asyncwaithandle)) that will be triggered when the
 operation is complete as well.

### **BeginControlOut(Byte, Byte, Int32, Int32, Byte[], Int32, AsyncCallback, Object)**

Initiates an asynchronous control transfer over the default control endpoint. The request should have an OUT
 direction (specified by the highest bit
 of the  parameter).

```csharp
public IAsyncResult BeginControlOut(byte requestType, byte request, int value, int index, Byte[] buffer, int length, AsyncCallback userCallback, object stateObject)
```

#### Parameters

`requestType` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

                The setup packet request type. The request type must specify the OUT direction (highest bit
                cleared).

`request` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
The setup packet device request.

`value` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                The value member in the setup packet. Its meaning depends on the request. Value should be between
                zero and 65535 (0xFFFF).

`index` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                The index member in the setup packet. Its meaning depends on the request. Index should be between
                zero and 65535 (0xFFFF).

`buffer` [Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
The buffer that contains the data to be transferred.

`length` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                Length of the data to transfer. Must be equal to or less than the length of
                . The setup packet's length member will be set to this length.

`userCallback` [AsyncCallback](https://docs.microsoft.com/en-us/dotnet/api/system.asynccallback)<br>

                An optional asynchronous callback, to be called when the control transfer is complete. Can
                be null if no callback is required.

`stateObject` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

                A user-provided object that distinguishes this particular asynchronous operation. Can be null
                if not required.

#### Returns

[IAsyncResult](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncresult)<br>

                An  object representing the asynchronous control transfer, which could still be
                pending.

**Remarks:**

This method always completes immediately even if the operation is still pending. The [IAsyncResult](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncresult)
 object returned represents the operation
 and must be passed to [USBDevice.EndControlTransfer(IAsyncResult)](./nefarius.drivers.winusb.usbdevice.md#endcontroltransferiasyncresult) to retrieve the result of the operation. For every call to
 this method a matching call to
 [USBDevice.EndControlTransfer(IAsyncResult)](./nefarius.drivers.winusb.usbdevice.md#endcontroltransferiasyncresult) must be made. When  specifies a callback
 function, this function will be called when the operation is completed. The optional
  parameter can be used to pass user-defined information to this callback or the
 [IAsyncResult](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncresult). The [IAsyncResult](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncresult)
 also provides an event handle ([IAsyncResult.AsyncWaitHandle](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncresult.asyncwaithandle)) that will be triggered when the
 operation is complete as well.

### **BeginControlOut(Byte, Byte, Int32, Int32, Byte[], AsyncCallback, Object)**

Initiates an asynchronous control transfer over the default control endpoint. The request should have an OUT
 direction (specified by the highest bit
 of the  parameter).

```csharp
public IAsyncResult BeginControlOut(byte requestType, byte request, int value, int index, Byte[] buffer, AsyncCallback userCallback, object stateObject)
```

#### Parameters

`requestType` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

                The setup packet request type. The request type must specify the OUT direction (highest bit
                cleared).

`request` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
The setup packet device request.

`value` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                The value member in the setup packet. Its meaning depends on the request. Value should be between
                zero and 65535 (0xFFFF).

`index` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                The index member in the setup packet. Its meaning depends on the request. Index should be between
                zero and 65535 (0xFFFF).

`buffer` [Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

                The buffer that contains the data to be transferred. The setup packet's length member will be set
                to the length of this buffer.

`userCallback` [AsyncCallback](https://docs.microsoft.com/en-us/dotnet/api/system.asynccallback)<br>

                An optional asynchronous callback, to be called when the control transfer is complete. Can
                be null if no callback is required.

`stateObject` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

                A user-provided object that distinguishes this particular asynchronous operation. Can be null
                if not required.

#### Returns

[IAsyncResult](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncresult)<br>

                An  object representing the asynchronous control transfer, which could still be
                pending.

**Remarks:**

This method always completes immediately even if the operation is still pending. The [IAsyncResult](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncresult)
 object returned represents the operation
 and must be passed to [USBDevice.EndControlTransfer(IAsyncResult)](./nefarius.drivers.winusb.usbdevice.md#endcontroltransferiasyncresult) to retrieve the result of the operation. For every call to
 this method a matching call to
 [USBDevice.EndControlTransfer(IAsyncResult)](./nefarius.drivers.winusb.usbdevice.md#endcontroltransferiasyncresult) must be made. When  specifies a callback
 function, this function will be called when the operation is completed. The optional
  parameter can be used to pass user-defined information to this callback or the
 [IAsyncResult](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncresult). The [IAsyncResult](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncresult)
 also provides an event handle ([IAsyncResult.AsyncWaitHandle](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncresult.asyncwaithandle)) that will be triggered when the
 operation is complete as well.

### **BeginControlOut(Byte, Byte, Int32, Int32, AsyncCallback, Object)**

Initiates an asynchronous control transfer without a data stage over the default control endpoint.
 The request should have an OUT direction (specified by the highest bit of the 
 parameter).
 The setup packets' length member will be set to zero.

```csharp
public IAsyncResult BeginControlOut(byte requestType, byte request, int value, int index, AsyncCallback userCallback, object stateObject)
```

#### Parameters

`requestType` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

                The setup packet request type. The request type must specify the OUT direction (highest bit
                cleared).

`request` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
The setup packet device request.

`value` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                The value member in the setup packet. Its meaning depends on the request. Value should be between
                zero and 65535 (0xFFFF).

`index` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                The index member in the setup packet. Its meaning depends on the request. Index should be between
                zero and 65535 (0xFFFF).

`userCallback` [AsyncCallback](https://docs.microsoft.com/en-us/dotnet/api/system.asynccallback)<br>

                An optional asynchronous callback, to be called when the control transfer is complete. Can
                be null if no callback is required.

`stateObject` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

                A user-provided object that distinguishes this particular asynchronous operation. Can be null
                if not required.

#### Returns

[IAsyncResult](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncresult)<br>

                An  object representing the asynchronous control transfer, which could still be
                pending.

**Remarks:**

This method always completes immediately even if the operation is still pending. The [IAsyncResult](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncresult)
 object returned represents the operation
 and must be passed to [USBDevice.EndControlTransfer(IAsyncResult)](./nefarius.drivers.winusb.usbdevice.md#endcontroltransferiasyncresult) to retrieve the result of the operation. For every call to
 this method a matching call to
 [USBDevice.EndControlTransfer(IAsyncResult)](./nefarius.drivers.winusb.usbdevice.md#endcontroltransferiasyncresult) must be made. When  specifies a callback
 function, this function will be called when the operation is completed. The optional
  parameter can be used to pass user-defined information to this callback or the
 [IAsyncResult](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncresult). The [IAsyncResult](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncresult)
 also provides an event handle ([IAsyncResult.AsyncWaitHandle](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncresult.asyncwaithandle)) that will be triggered when the
 operation is complete as well.

### **GetDevices(String)**

Finds WinUSB devices with a GUID matching the parameter guidString

```csharp
public static USBDeviceInfo[] GetDevices(string guidString)
```

#### Parameters

`guidString` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

                The GUID string that the device should match.
                The format of this string may be any format accepted by the constructor
                of the System.Guid class

#### Returns

[USBDeviceInfo[]](./nefarius.drivers.winusb.usbdeviceinfo.md)<br>

                An array of USBDeviceInfo objects representing the
                devices found. When no devices are found an empty array is
                returned.

### **GetDevices(Guid)**

Finds WinUSB devices with a GUID matching the parameter guid

```csharp
public static USBDeviceInfo[] GetDevices(Guid guid)
```

#### Parameters

`guid` [Guid](https://docs.microsoft.com/en-us/dotnet/api/system.guid)<br>
The GUID that the device should match.

#### Returns

[USBDeviceInfo[]](./nefarius.drivers.winusb.usbdeviceinfo.md)<br>

                An array of USBDeviceInfo objects representing the
                devices found. When no devices are found an empty array is
                returned.

### **GetSingleDevice(Guid)**

Finds the first WinUSB device with a GUID matching the parameter guid.
 If multiple WinUSB devices match the GUID only the first one is returned.

```csharp
public static USBDevice GetSingleDevice(Guid guid)
```

#### Parameters

`guid` [Guid](https://docs.microsoft.com/en-us/dotnet/api/system.guid)<br>
The GUID that the device should match.

#### Returns

[USBDevice](./nefarius.drivers.winusb.usbdevice.md)<br>

                An UsbDevice object representing the device if found. If
                no device with the given GUID could be found null is returned.

### **GetSingleDevice(String)**

Finds the first WinUSB device with a GUID matching the parameter guidString.
 If multiple WinUSB devices match the GUID only the first one is returned.

```csharp
public static USBDevice GetSingleDevice(string guidString)
```

#### Parameters

`guidString` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The GUID string that the device should match.

#### Returns

[USBDevice](./nefarius.drivers.winusb.usbdevice.md)<br>

                An UsbDevice object representing the device if found. If
                no device with the given GUID could be found null is returned.

### **GetSingleDeviceByPath(String)**

Opens a WinUSB device by provided path (symbolic link).

```csharp
public static USBDevice GetSingleDeviceByPath(string path)
```

#### Parameters

`path` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The device path (symbolic link) to open.

#### Returns

[USBDevice](./nefarius.drivers.winusb.usbdevice.md)<br>
a  object.

### **ControlTransferAsync(Byte, Byte, Int32, Int32, Byte[], Int32)**

Asynchronously issue a sequence of bytes IO over the default control endpoint.
 This method allows both IN and OUT direction transfers, depending on the highest bit of the
  parameter.
 Alternatively, [USBDevice.ControlInAsync(Byte, Byte, Int32, Int32, Byte[], Int32)](./nefarius.drivers.winusb.usbdevice.md#controlinasyncbyte-byte-int32-int32-byte-int32)
 and [USBDevice.ControlOutAsync(Byte, Byte, Int32, Int32, Byte[], Int32)](./nefarius.drivers.winusb.usbdevice.md#controloutasyncbyte-byte-int32-int32-byte-int32) can be used for asynchronous control transfers in
 a specific direction,
 which is the recommended way because it prevents using the wrong direction accidentally.
 Use the BeginControlTransfer method when the direction is not known at compile time.

```csharp
public Task<int> ControlTransferAsync(byte requestType, byte request, int value, int index, Byte[] buffer, int length)
```

#### Parameters

`requestType` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
The setup packet request type.

`request` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
The setup packet device request.

`value` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                The value member in the setup packet. Its meaning depends on the request. Value should be between
                zero and 65535 (0xFFFF).

`index` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                The index member in the setup packet. Its meaning depends on the request. Index should be between
                zero and 65535 (0xFFFF).

`buffer` [Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

                The data to transfer in the data stage of the control. When the transfer is in the IN direction the data received
                will be
                written to this buffer. For an OUT direction transfer the contents of the buffer are written sent through the pipe.
                Note: This buffer is not allowed
                to change for the duration of the asynchronous operation.

`length` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                Length of the data to transfer. Must be equal to or less than the length of
                . The setup packet's length member will be set to this length.

#### Returns

[Task&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

                A task that represents the asynchronous read operation.
                The value of the TResult parameter contains the total number of bytes that has been transferred.
                The result value can be less than the number of bytes requested if the number of bytes currently available is less
                than the requested number,
                or it can be 0 (zero) if the end of the stream has been reached.

### **ControlTransferAsync(Byte, Byte, Int32, Int32, Byte[])**

Asynchronously issue a sequence of bytes IO over the default control endpoint.
 This method allows both IN and OUT direction transfers, depending on the highest bit of the
  parameter.
 Alternatively, [USBDevice.ControlInAsync(Byte, Byte, Int32, Int32, Byte[])](./nefarius.drivers.winusb.usbdevice.md#controlinasyncbyte-byte-int32-int32-byte)
 and [USBDevice.ControlOutAsync(Byte, Byte, Int32, Int32, Byte[])](./nefarius.drivers.winusb.usbdevice.md#controloutasyncbyte-byte-int32-int32-byte) can be used for asynchronous control transfers in a
 specific direction,
 which is the recommended way because it prevents using the wrong direction accidentally.
 Use the BeginControlTransfer method when the direction is not known at compile time.

```csharp
public Task<int> ControlTransferAsync(byte requestType, byte request, int value, int index, Byte[] buffer)
```

#### Parameters

`requestType` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
The setup packet request type.

`request` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
The setup packet device request.

`value` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                The value member in the setup packet. Its meaning depends on the request. Value should be between
                zero and 65535 (0xFFFF).

`index` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                The index member in the setup packet. Its meaning depends on the request. Index should be between
                zero and 65535 (0xFFFF).

`buffer` [Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

                The data to transfer in the data stage of the control. When the transfer is in the IN direction the data received
                will be
                written to this buffer. For an OUT direction transfer the contents of the buffer are written sent through the pipe.
                Note: This buffer is not allowed
                to change for the duration of the asynchronous operation.

#### Returns

[Task&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

                A task that represents the asynchronous read operation.
                The value of the TResult parameter contains the total number of bytes that has been transferred.
                The result value can be less than the number of bytes requested if the number of bytes currently available is less
                than the requested number,
                or it can be 0 (zero) if the end of the stream has been reached.

### **ControlTransferAsync(Byte, Byte, Int32, Int32)**

Asynchronously issue a sequence of bytes IO without a data stage over the default control endpoint.
 This method allows both IN and OUT direction transfers, depending on the highest bit of the
  parameter.
 Alternatively, [USBDevice.ControlInAsync(Byte, Byte, Int32, Int32)](./nefarius.drivers.winusb.usbdevice.md#controlinasyncbyte-byte-int32-int32)
 and [USBDevice.ControlOutAsync(Byte, Byte, Int32, Int32)](./nefarius.drivers.winusb.usbdevice.md#controloutasyncbyte-byte-int32-int32) can be used for asynchronous control transfers in a specific
 direction,
 which is the recommended way because it prevents using the wrong direction accidentally.
 Use the BeginControlTransfer method when the direction is not known at compile time.

```csharp
public Task<int> ControlTransferAsync(byte requestType, byte request, int value, int index)
```

#### Parameters

`requestType` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
The setup packet request type.

`request` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
The setup packet device request.

`value` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                The value member in the setup packet. Its meaning depends on the request. Value should be between
                zero and 65535 (0xFFFF).

`index` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                The index member in the setup packet. Its meaning depends on the request. Index should be between
                zero and 65535 (0xFFFF).

#### Returns

[Task&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

                A task that represents the asynchronous read operation.
                The value of the TResult parameter contains the total number of bytes that has been transferred.
                The result value can be less than the number of bytes requested if the number of bytes currently available is less
                than the requested number,
                or it can be 0 (zero) if the end of the stream has been reached.

### **ControlInAsync(Byte, Byte, Int32, Int32, Byte[], Int32)**

Asynchronously issue a sequence of bytes input operation over the default control endpoint.
 The request should have an IN direction (specified by the highest bit of the 
 parameter).

```csharp
public Task<int> ControlInAsync(byte requestType, byte request, int value, int index, Byte[] buffer, int length)
```

#### Parameters

`requestType` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
The setup packet request type.

`request` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
The setup packet device request.

`value` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                The value member in the setup packet. Its meaning depends on the request. Value should be between
                zero and 65535 (0xFFFF).

`index` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                The index member in the setup packet. Its meaning depends on the request. Index should be between
                zero and 65535 (0xFFFF).

`buffer` [Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
The buffer that will receive the data transferred.

`length` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                Length of the data to transfer. Must be equal to or less than the length of
                . The setup packet's length member will be set to this length.

#### Returns

[Task&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

                A task that represents the asynchronous input operation.
                The value of the TResult parameter contains the total number of bytes that has been transferred.
                The result value can be less than the number of bytes requested if the number of bytes currently available is less
                than the requested number,
                or it can be 0 (zero) if the end of the stream has been reached.

### **ControlInAsync(Byte, Byte, Int32, Int32, Byte[])**

Asynchronously issue a sequence of bytes input operation over the default control endpoint.
 The request should have an IN direction (specified by the highest bit of the 
 parameter).

```csharp
public Task<int> ControlInAsync(byte requestType, byte request, int value, int index, Byte[] buffer)
```

#### Parameters

`requestType` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
The setup packet request type.

`request` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
The setup packet device request.

`value` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                The value member in the setup packet. Its meaning depends on the request. Value should be between
                zero and 65535 (0xFFFF).

`index` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                The index member in the setup packet. Its meaning depends on the request. Index should be between
                zero and 65535 (0xFFFF).

`buffer` [Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
The buffer that will receive the data transferred.

#### Returns

[Task&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

                A task that represents the asynchronous input operation.
                The value of the TResult parameter contains the total number of bytes that has been transferred.
                The result value can be less than the number of bytes requested if the number of bytes currently available is less
                than the requested number,
                or it can be 0 (zero) if the end of the stream has been reached.

### **ControlInAsync(Byte, Byte, Int32, Int32)**

Asynchronously issue a sequence of bytes input operation without a data stage over the default control endpoint.
 The request should have an IN direction (specified by the highest bit of the 
 parameter).

```csharp
public Task<int> ControlInAsync(byte requestType, byte request, int value, int index)
```

#### Parameters

`requestType` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
The setup packet request type.

`request` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
The setup packet device request.

`value` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                The value member in the setup packet. Its meaning depends on the request. Value should be between
                zero and 65535 (0xFFFF).

`index` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                The index member in the setup packet. Its meaning depends on the request. Index should be between
                zero and 65535 (0xFFFF).

#### Returns

[Task&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

                A task that represents the asynchronous input operation.
                The value of the TResult parameter contains the total number of bytes that has been transferred.
                The result value can be less than the number of bytes requested if the number of bytes currently available is less
                than the requested number,
                or it can be 0 (zero) if the end of the stream has been reached.

### **ControlOutAsync(Byte, Byte, Int32, Int32, Byte[], Int32)**

Asynchronously issue a sequence of bytes output operation over the default control endpoint.
 The request should have an OUT direction (specified by the highest bit of the 
 parameter).

```csharp
public Task<int> ControlOutAsync(byte requestType, byte request, int value, int index, Byte[] buffer, int length)
```

#### Parameters

`requestType` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
The setup packet request type.

`request` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
The setup packet device request.

`value` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                The value member in the setup packet. Its meaning depends on the request. Value should be between
                zero and 65535 (0xFFFF).

`index` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                The index member in the setup packet. Its meaning depends on the request. Index should be between
                zero and 65535 (0xFFFF).

`buffer` [Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
The buffer that contains the data to be transferred.

`length` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                Length of the data to transfer. Must be equal to or less than the length of
                . The setup packet's length member will be set to this length.

#### Returns

[Task&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

                A task that represents the asynchronous output operation.
                The value of the TResult parameter contains the total number of bytes that has been transferred.
                The result value can be less than the number of bytes requested if the number of bytes currently available is less
                than the requested number,
                or it can be 0 (zero) if the end of the stream has been reached.

### **ControlOutAsync(Byte, Byte, Int32, Int32, Byte[])**

Asynchronously issue a sequence of bytes output operation over the default control endpoint.
 The request should have an OUT direction (specified by the highest bit of the 
 parameter).

```csharp
public Task<int> ControlOutAsync(byte requestType, byte request, int value, int index, Byte[] buffer)
```

#### Parameters

`requestType` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
The setup packet request type.

`request` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
The setup packet device request.

`value` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                The value member in the setup packet. Its meaning depends on the request. Value should be between
                zero and 65535 (0xFFFF).

`index` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                The index member in the setup packet. Its meaning depends on the request. Index should be between
                zero and 65535 (0xFFFF).

`buffer` [Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
The buffer that contains the data to be transferred.

#### Returns

[Task&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

                A task that represents the asynchronous output operation.
                The value of the TResult parameter contains the total number of bytes that has been transferred.
                The result value can be less than the number of bytes requested if the number of bytes currently available is less
                than the requested number,
                or it can be 0 (zero) if the end of the stream has been reached.

### **ControlOutAsync(Byte, Byte, Int32, Int32)**

Asynchronously issue a sequence of bytes output operation without a data stage over the default control endpoint.
 The request should have an OUT direction (specified by the highest bit of the 
 parameter).

```csharp
public Task<int> ControlOutAsync(byte requestType, byte request, int value, int index)
```

#### Parameters

`requestType` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
The setup packet request type.

`request` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
The setup packet device request.

`value` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                The value member in the setup packet. Its meaning depends on the request. Value should be between
                zero and 65535 (0xFFFF).

`index` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                The index member in the setup packet. Its meaning depends on the request. Index should be between
                zero and 65535 (0xFFFF).

#### Returns

[Task&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

                A task that represents the asynchronous output operation.
                The value of the TResult parameter contains the total number of bytes that has been transferred.
                The result value can be less than the number of bytes requested if the number of bytes currently available is less
                than the requested number,
                or it can be 0 (zero) if the end of the stream has been reached.

### **GetSupportedLanguageIDs()**

Gets available language IDs from the device.

```csharp
public Int32[] GetSupportedLanguageIDs()
```

#### Returns

[Int32[]](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **GetStringDescriptor(Byte, Int32)**

Synchronously reads the string descriptor.

```csharp
public string GetStringDescriptor(byte index, int languageId)
```

#### Parameters

`index` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

                The descriptor index. For an explanation of the descriptor index, see the Universal Serial Bus
                specification ().

`languageId` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                A value that specifies the language identifier. languageID should be between zero and 65535
                (0xFFFF).

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The string descriptor content.
