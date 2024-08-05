# USBPipePolicy

Namespace: Nefarius.Drivers.WinUSB

Describes the policy for a specific USB pipe

```csharp
public sealed class USBPipePolicy
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [USBPipePolicy](./nefarius.drivers.winusb.usbpipepolicy.md)

## Properties

### <a id="properties-allowpartialreads"/>**AllowPartialReads**

When false, read requests fail when the device returns more data than requested. When true, extra data is
 saved and returned on the next read. Default value is true. Only available on IN direction pipes.

```csharp
public bool AllowPartialReads { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### <a id="properties-autoclearstall"/>**AutoClearStall**

When true, the driver fails stalled data transfers, but the driver clears the stall condition automatically.
 Default
 value is false.

```csharp
public bool AutoClearStall { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### <a id="properties-autoflush"/>**AutoFlush**

If both AllowPartialReads and AutoFlush are true, when the device returns more data than requested by the client it
 will discard the remaining data. Default value is false. Only available on IN direction pipes.

```csharp
public bool AutoFlush { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### <a id="properties-ignoreshortpackets"/>**IgnoreShortPackets**

When true, read operations are completed only when the number of bytes requested by the client has been received.
 Default value is false.
 Only available on IN direction pipes.

```csharp
public bool IgnoreShortPackets { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### <a id="properties-maximumpacketsize"/>**MaximumPacketSize**

Gets the maximum size of a USB transfer supported by WinUSB.

```csharp
public int MaximumPacketSize { get; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### <a id="properties-pipetransfertimeout"/>**PipeTransferTimeout**

Specifies the timeout in milliseconds for pipe operations. If an operation does not finish within the specified
 time it will fail.
 When set to zero, no timeout is used. Default value is zero.

```csharp
public int PipeTransferTimeout { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### <a id="properties-rawio"/>**RawIO**

When true, read and write operations to the pipe must have a buffer length that is a multiple of the maximum
 endpoint packet size,
 and the length must be less than the maximum transfer size. With these conditions met, data is sent directly to the
 USB driver stack,
 bypassing the queuing and error handling of WinUSB.
 Default value is false.

```csharp
public bool RawIO { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### <a id="properties-shortpacketterminate"/>**ShortPacketTerminate**

When true, every write request that is a multiple of the maximum packet size for the endpoint is terminated with a
 zero-length packet.
 Default value is false. Only available on OUT direction pipes.

```csharp
public bool ShortPacketTerminate { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
