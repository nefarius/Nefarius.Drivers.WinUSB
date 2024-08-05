# USBInterface

Namespace: Nefarius.Drivers.WinUSB

Represents a single USB interface from a USB device

```csharp
public sealed class USBInterface
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [USBInterface](./nefarius.drivers.winusb.usbinterface.md)

## Properties

### <a id="properties-alternatesetting"/>**AlternateSetting**

Interface alternate setting

```csharp
public byte AlternateSetting { get; set; }
```

#### Property Value

[Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

### <a id="properties-baseclass"/>**BaseClass**

Interface class code. If the interface class does
 not match any of the USBBaseClass enumeration values
 the value will be USBBaseClass.Unknown

```csharp
public USBBaseClass BaseClass { get; }
```

#### Property Value

[USBBaseClass](./nefarius.drivers.winusb.usbbaseclass.md)<br>

### <a id="properties-classvalue"/>**ClassValue**

Interface class code as defined in the interface descriptor
 This property can be used if the class type is not defined
 int the USBBaseClass enumeration

```csharp
public byte ClassValue { get; }
```

#### Property Value

[Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

### <a id="properties-device"/>**Device**

USB device associated with this interface

```csharp
public USBDevice Device { get; }
```

#### Property Value

[USBDevice](./nefarius.drivers.winusb.usbdevice.md)<br>

### <a id="properties-inpipe"/>**InPipe**

First IN direction pipe on this interface

```csharp
public USBPipe InPipe { get; }
```

#### Property Value

[USBPipe](./nefarius.drivers.winusb.usbpipe.md)<br>

### <a id="properties-number"/>**Number**

Interface number from the interface descriptor

```csharp
public int Number { get; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### <a id="properties-outpipe"/>**OutPipe**

First OUT direction pipe on this interface

```csharp
public USBPipe OutPipe { get; }
```

#### Property Value

[USBPipe](./nefarius.drivers.winusb.usbpipe.md)<br>

### <a id="properties-pipes"/>**Pipes**

Collection of pipes associated with this interface

```csharp
public USBPipeCollection Pipes { get; }
```

#### Property Value

[USBPipeCollection](./nefarius.drivers.winusb.usbpipecollection.md)<br>

### <a id="properties-protocol"/>**Protocol**

Interface protocol code

```csharp
public byte Protocol { get; }
```

#### Property Value

[Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

### <a id="properties-subclass"/>**SubClass**

Interface subclass code

```csharp
public byte SubClass { get; }
```

#### Property Value

[Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
