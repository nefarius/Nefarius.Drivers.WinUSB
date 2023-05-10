# USBDeviceDescriptor

Namespace: Nefarius.Drivers.WinUSB

USB device details

```csharp
public sealed class USBDeviceDescriptor
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [USBDeviceDescriptor](./nefarius.drivers.winusb.usbdevicedescriptor.md)

## Properties

### **PathName**

Windows path name for the USB device

```csharp
public string PathName { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **VID**

USB vendor ID (VID) of the device

```csharp
public int VID { get; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **PID**

USB product ID (PID) of the device

```csharp
public int PID { get; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Manufacturer**

Manufacturer name, or null if not available

```csharp
public string Manufacturer { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Product**

Product name, or null if not available

```csharp
public string Product { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **SerialNumber**

Device serial number, or null if not available

```csharp
public string SerialNumber { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **FullName**

Friendly device name, or path name when no
 further device information is available

```csharp
public string FullName { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **ClassValue**

Device class code as defined in the interface descriptor
 This property can be used if the class type is not defined
 int the USBBaseClass enumeration

```csharp
public byte ClassValue { get; }
```

#### Property Value

[Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

### **SubClass**

Device subclass code

```csharp
public byte SubClass { get; }
```

#### Property Value

[Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

### **Protocol**

Device protocol code

```csharp
public byte Protocol { get; }
```

#### Property Value

[Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

### **BaseClass**

Device class code. If the device class does
 not match any of the USBBaseClass enumeration values
 the value will be USBBaseClass.Unknown

```csharp
public USBBaseClass BaseClass { get; }
```

#### Property Value

[USBBaseClass](./nefarius.drivers.winusb.usbbaseclass.md)<br>
