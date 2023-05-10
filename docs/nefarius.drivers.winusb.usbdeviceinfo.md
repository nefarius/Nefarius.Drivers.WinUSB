# USBDeviceInfo

Namespace: Nefarius.Drivers.WinUSB

Gives information about a device. This information is retrieved using the setup API, not the
 actual device descriptor. Device description and manufacturer will be the strings specified
 in the .inf file. After a device is opened the actual device descriptor can be read as well.

```csharp
public sealed class USBDeviceInfo
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [USBDeviceInfo](./nefarius.drivers.winusb.usbdeviceinfo.md)

## Properties

### **VID**

Vendor ID (VID) of the USB device

```csharp
public int VID { get; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **PID**

Product ID (VID) of the USB device

```csharp
public int PID { get; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Manufacturer**

Manufacturer of the device, as specified in the INF file (not the device descriptor)

```csharp
public string Manufacturer { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **DeviceDescription**

Description of the device, as specified in the INF file (not the device descriptor)

```csharp
public string DeviceDescription { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **DevicePath**

Device pathname

```csharp
public string DevicePath { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
