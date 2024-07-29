# USBPipe

Namespace: Nefarius.Drivers.WinUSB

UsbPipe represents a single pipe on a WinUSB device. A pipe is connected
 to a certain endpoint on the device and has a fixed direction (IN or OUT)

```csharp
public sealed class USBPipe
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [USBPipe](./nefarius.drivers.winusb.usbpipe.md)

## Properties

### <a id="properties-address"/>**Address**

Endpoint address including the direction in the most significant bit

```csharp
public byte Address { get; }
```

#### Property Value

[Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

### <a id="properties-device"/>**Device**

The USBDevice this pipe is associated with

```csharp
public USBDevice Device { get; }
```

#### Property Value

[USBDevice](./nefarius.drivers.winusb.usbdevice.md)<br>

### <a id="properties-interface"/>**Interface**

The interface associated with this pipe

```csharp
public USBInterface Interface { get; private set; }
```

#### Property Value

[USBInterface](./nefarius.drivers.winusb.usbinterface.md)<br>

### <a id="properties-isin"/>**IsIn**

True if the pipe has direction IN (device to host), false otherwise.

```csharp
public bool IsIn { get; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### <a id="properties-isout"/>**IsOut**

True if the pipe has direction OUT (host to device), false otherwise.

```csharp
public bool IsOut { get; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### <a id="properties-maximumpacketsize"/>**MaximumPacketSize**

Maximum packet size for transfers on this endpoint

```csharp
public int MaximumPacketSize { get; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### <a id="properties-policy"/>**Policy**

The pipe policy settings for this pipe

```csharp
public USBPipePolicy Policy { get; private set; }
```

#### Property Value

[USBPipePolicy](./nefarius.drivers.winusb.usbpipepolicy.md)<br>

### <a id="properties-transfertype"/>**TransferType**

The transfer method used for this pipe

```csharp
public USBTransferType TransferType { get; }
```

#### Property Value

[USBTransferType](./nefarius.drivers.winusb.usbtransfertype.md)<br>

## Methods

### <a id="methods-abort"/>**Abort()**

Aborts all pending transfers for this pipe.

```csharp
public void Abort()
```

### <a id="methods-attachinterface"/>**AttachInterface(USBInterface)**

```csharp
internal void AttachInterface(USBInterface usbInterface)
```

#### Parameters

`usbInterface` [USBInterface](./nefarius.drivers.winusb.usbinterface.md)<br>

### <a id="methods-beginread"/>**BeginRead(Byte[], Int32, Int32, AsyncCallback, Object)**

Initiates an asynchronous read operation on the pipe.

```csharp
public IAsyncResult BeginRead(Byte[] buffer, int offset, int length, AsyncCallback userCallback, object stateObject)
```

#### Parameters

`buffer` [Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
Buffer that will receive the data read from the pipe.

`offset` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
Byte offset within the buffer at which to begin writing the data received.

`length` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
Length of the data to transfer.

`userCallback` [AsyncCallback](https://docs.microsoft.com/en-us/dotnet/api/system.asynccallback)<br>
An optional asynchronous callback, to be called when the operation is complete. Can be null
 if no callback is required.

`stateObject` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>
A user-provided object that distinguishes this particular asynchronous operation. Can be null
 if not required.

#### Returns

An [IAsyncResult](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncresult) object representing the asynchronous operation, which could still be pending.

**Remarks:**

This method always completes immediately even if the operation is still pending. The [IAsyncResult](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncresult)
 object returned represents the operation
 and must be passed to [USBPipe.EndRead(IAsyncResult)](./nefarius.drivers.winusb.usbpipe.md#endreadiasyncresult) to retrieve the result of the operation. For every call to this method
 a matching call to
 [USBPipe.EndRead(IAsyncResult)](./nefarius.drivers.winusb.usbpipe.md#endreadiasyncresult) must be made. When  specifies a callback function, this
 function will be called when the operation is completed. The optional
  parameter can be used to pass user-defined information to this callback or the
 [IAsyncResult](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncresult). The [IAsyncResult](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncresult)
 also provides an event handle ([IAsyncResult.AsyncWaitHandle](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncresult.asyncwaithandle)) that will be triggered when the
 operation is complete as well.

### <a id="methods-beginwrite"/>**BeginWrite(Byte[], Int32, Int32, AsyncCallback, Object)**

Initiates an asynchronous write operation on the pipe.

```csharp
public IAsyncResult BeginWrite(Byte[] buffer, int offset, int length, AsyncCallback userCallback, object stateObject)
```

#### Parameters

`buffer` [Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
Buffer that contains the data to write to the pipe.

`offset` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
Byte offset within the buffer from which to begin writing.

`length` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
Length of the data to transfer.

`userCallback` [AsyncCallback](https://docs.microsoft.com/en-us/dotnet/api/system.asynccallback)<br>
An optional asynchronous callback, to be called when the operation is complete. Can be null
 if no callback is required.

`stateObject` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>
A user-provided object that distinguishes this particular asynchronous operation. Can be null
 if not required.

#### Returns

An [IAsyncResult](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncresult) object representing the asynchronous operation, which could still be pending.

**Remarks:**

This method always completes immediately even if the operation is still pending. The [IAsyncResult](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncresult)
 object returned represents the operation
 and must be passed to [USBPipe.EndWrite(IAsyncResult)](./nefarius.drivers.winusb.usbpipe.md#endwriteiasyncresult) to retrieve the result of the operation. For every call to this
 method a matching call to
 [USBPipe.EndWrite(IAsyncResult)](./nefarius.drivers.winusb.usbpipe.md#endwriteiasyncresult) must be made. When  specifies a callback function, this
 function will be called when the operation is completed. The optional
  parameter can be used to pass user-defined information to this callback or the
 [IAsyncResult](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncresult). The [IAsyncResult](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncresult)
 also provides an event handle ([IAsyncResult.AsyncWaitHandle](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncresult.asyncwaithandle)) that will be triggered when the
 operation is complete as well.

### <a id="methods-endread"/>**EndRead(IAsyncResult)**

Waits for a pending asynchronous read operation to complete.

```csharp
public int EndRead(IAsyncResult asyncResult)
```

#### Parameters

`asyncResult` [IAsyncResult](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncresult)<br>
The [IAsyncResult](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncresult) object representing the asynchronous operation,
 as returned by [USBPipe.BeginRead(Byte[], Int32, Int32, AsyncCallback, Object)](./nefarius.drivers.winusb.usbpipe.md#beginreadbyte-int32-int32-asynccallback-object).

#### Returns

The number of bytes transferred during the operation.

**Remarks:**

Every call to [USBPipe.BeginRead(Byte[], Int32, Int32, AsyncCallback, Object)](./nefarius.drivers.winusb.usbpipe.md#beginreadbyte-int32-int32-asynccallback-object) must have a matching call to [USBPipe.EndRead(IAsyncResult)](./nefarius.drivers.winusb.usbpipe.md#endreadiasyncresult) to dispose
 of any resources used and to retrieve the result of the operation. When the operation was successful the method
 returns the number
 of bytes that were transferred. If an error occurred during the operation this method will throw the exceptions
 that would
 otherwise have occurred during the operation. If the operation is not yet finished EndWrite will wait for the
 operation to finish before returning.

### <a id="methods-endwrite"/>**EndWrite(IAsyncResult)**

Waits for a pending asynchronous write operation to complete.

```csharp
public int EndWrite(IAsyncResult asyncResult)
```

#### Parameters

`asyncResult` [IAsyncResult](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncresult)<br>
The [IAsyncResult](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncresult) object representing the asynchronous operation,
 as returned by [USBPipe.BeginWrite(Byte[], Int32, Int32, AsyncCallback, Object)](./nefarius.drivers.winusb.usbpipe.md#beginwritebyte-int32-int32-asynccallback-object).

#### Returns

The number of bytes transferred during the operation.

**Remarks:**

Every call to [USBPipe.BeginWrite(Byte[], Int32, Int32, AsyncCallback, Object)](./nefarius.drivers.winusb.usbpipe.md#beginwritebyte-int32-int32-asynccallback-object) must have a matching call to [USBPipe.EndWrite(IAsyncResult)](./nefarius.drivers.winusb.usbpipe.md#endwriteiasyncresult) to dispose
 of any resources used and to retrieve the result of the operation. When the operation was successful the method
 returns the number
 of bytes that were transferred. If an error occurred during the operation this method will throw the exceptions
 that would
 otherwise have occurred during the operation. If the operation is not yet finished EndWrite will wait for the
 operation to finish before returning.

### <a id="methods-flush"/>**Flush()**

Flushes the pipe, discarding any data that is cached. Only available on IN direction pipes.

```csharp
public void Flush()
```

### <a id="methods-read"/>**Read(Span&lt;Byte&gt;)**

Reads data from the pipe into a buffer.

```csharp
public int Read(Span<Byte> buffer)
```

#### Parameters

`buffer` [Span&lt;Byte&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.span-1)<br>
The buffer to read data into. The maximum number of bytes that will be read is specified by the
 length of the buffer.

#### Returns

The number of bytes read from the pipe.

### <a id="methods-read"/>**Read(Span&lt;Byte&gt;, Int32, Int32)**

Reads data from the pipe into a buffer.

```csharp
public int Read(Span<Byte> buffer, int offset, int length)
```

#### Parameters

`buffer` [Span&lt;Byte&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.span-1)<br>
The buffer to read data into.

`offset` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The byte offset in  from which to begin writing data read from the pipe.

`length` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The maximum number of bytes to read, starting at offset

#### Returns

The number of bytes read from the pipe.

### <a id="methods-readasync"/>**ReadAsync(Byte[], Int32, Int32)**

Asynchronously reads a sequence of bytes from the USB pipe.

```csharp
public Task<Int32> ReadAsync(Byte[] buffer, int offset, int length)
```

#### Parameters

`buffer` [Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
Buffer that will receive the data read from the pipe.

`offset` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
Byte offset within the buffer at which to begin writing the data received.

`length` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
Length of the data to transfer.

#### Returns

A task that represents the asynchronous read operation.
 The value of the TResult parameter contains the total number of bytes that has been transferred.
 The result value can be less than the number of bytes requested if the number of bytes currently available is less
 than the requested number,
 or it can be 0 (zero) if the end of the stream has been reached.

### <a id="methods-reset"/>**Reset()**

Resets the pipe to clear a stall condition.

```csharp
public void Reset()
```

### <a id="methods-write"/>**Write(Span&lt;Byte&gt;)**

Writes data from a buffer to the pipe.

```csharp
public void Write(Span<Byte> buffer)
```

#### Parameters

`buffer` [Span&lt;Byte&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.span-1)<br>
The buffer to write data from. The complete buffer will be written to the device.

### <a id="methods-write"/>**Write(Span&lt;Byte&gt;, Int32, Int32)**

Writes data from a buffer to the pipe.

```csharp
public void Write(Span<Byte> buffer, int offset, int length)
```

#### Parameters

`buffer` [Span&lt;Byte&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.span-1)<br>
The buffer to write data from.

`offset` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The byte offset in  from which to begin writing.

`length` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The number of bytes to write, starting at offset

### <a id="methods-writeasync"/>**WriteAsync(Byte[], Int32, Int32)**

Asynchronously write a sequence of bytes from the USB pipe.

```csharp
public Task<Int32> WriteAsync(Byte[] buffer, int offset, int length)
```

#### Parameters

`buffer` [Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
Buffer that will receive the data read from the pipe.

`offset` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
Byte offset within the buffer at which to begin writing the data received.

`length` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
Length of the data to transfer.

#### Returns

A task that represents the asynchronous read operation.
 The value of the TResult parameter contains the total number of bytes that has been transferred.
 The result value can be less than the number of bytes requested if the number of bytes currently available is less
 than the requested number,
 or it can be 0 (zero) if the end of the stream has been reached.
