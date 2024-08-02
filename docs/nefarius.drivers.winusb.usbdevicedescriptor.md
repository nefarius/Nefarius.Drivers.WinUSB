# USBDeviceDescriptor

Namespace: Nefarius.Drivers.WinUSB

USB device details

```csharp
public sealed class USBDeviceDescriptor
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [USBDeviceDescriptor](./nefarius.drivers.winusb.usbdevicedescriptor.md)

## Properties

### <a id="properties-baseclass"/>**BaseClass**

Device class code. If the device class does
 not match any of the USBBaseClass enumeration values
 the value will be USBBaseClass.Unknown

```csharp
public USBBaseClass BaseClass { get; }
```

#### Property Value

[USBBaseClass](./nefarius.drivers.winusb.usbbaseclass.md)<br>

### <a id="properties-classvalue"/>**ClassValue**

Device class code as defined in the interface descriptor
 This property can be used if the class type is not defined
 int the USBBaseClass enumeration

```csharp
public byte ClassValue { get; }
```

#### Property Value

[Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

### <a id="properties-fullname"/>**FullName**

Friendly device name, or path name when no
 further device information is available

```csharp
public string FullName { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### <a id="properties-manufacturer"/>**Manufacturer**

Manufacturer name, or null if not available

```csharp
public string Manufacturer { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### <a id="properties-pathname"/>**PathName**

Windows path name for the USB device

```csharp
public string PathName { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### <a id="properties-pid"/>**PID**

USB product ID (PID) of the device

```csharp
public int PID { get; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### <a id="properties-product"/>**Product**

Product name, or null if not available

```csharp
public string Product { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### <a id="properties-protocol"/>**Protocol**

Device protocol code

```csharp
public byte Protocol { get; }
```

#### Property Value

[Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

### <a id="properties-serialnumber"/>**SerialNumber**

Device serial number, or null if not available

```csharp
public string SerialNumber { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### <a id="properties-subclass"/>**SubClass**

Device subclass code

```csharp
public byte SubClass { get; }
```

#### Property Value

[Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

### <a id="properties-vid"/>**VID**

USB vendor ID (VID) of the device

```csharp
public int VID { get; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

## Methods

### <a id="methods-tostring"/>**ToString()**

```csharp
public string ToString()
```

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)
