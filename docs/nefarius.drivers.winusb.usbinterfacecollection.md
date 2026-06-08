# USBInterfaceCollection

Namespace: Nefarius.Drivers.WinUSB

Collection of UsbInterface objects

```csharp
public sealed class USBInterfaceCollection : System.Collections.Generic.IEnumerable<Nefarius.Drivers.WinUSB.USBInterface>, System.Collections.IEnumerable
```

Inheritance [Object](https://learn.microsoft.com/dotnet/api/system.object) → [USBInterfaceCollection](./nefarius.drivers.winusb.usbinterfacecollection.md)<br>
Implements [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)<[USBInterface](./nefarius.drivers.winusb.usbinterface.md)>, [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.ienumerable)<br>
Attributes [DefaultMemberAttribute](https://learn.microsoft.com/dotnet/api/system.reflection.defaultmemberattribute)

## Properties

### <a id="properties-item"/>**Item**

```csharp
public USBInterface Item { get; }
```

#### Property Value

[USBInterface](./nefarius.drivers.winusb.usbinterface.md)<br>

## Methods

### <a id="methods-find"/>**Find(USBBaseClass)**

Finds the first interface with that matches the device class
 given by the `interfaceClass` parameter.

```csharp
public USBInterface Find(USBBaseClass interfaceClass)
```

#### Parameters

`interfaceClass` [USBBaseClass](./nefarius.drivers.winusb.usbbaseclass.md)<br>
The device class the interface should match

#### Returns

The first interface with the given interface class, or null
 if no such interface exists.

### <a id="methods-findall"/>**FindAll(USBBaseClass)**

Finds all interfaces matching the device class given by the
 `interfaceClass` parameter.

```csharp
public USBInterface[] FindAll(USBBaseClass interfaceClass)
```

#### Parameters

`interfaceClass` [USBBaseClass](./nefarius.drivers.winusb.usbbaseclass.md)<br>
The device class the interface should match

#### Returns

An array of USBInterface objects matching the device class, or an empty
 array if no interface matches.

### <a id="methods-getenumerator"/>**GetEnumerator()**

Returns a typed enumerator that iterates through a collection.

```csharp
public IEnumerator<USBInterface> GetEnumerator()
```

#### Returns

The enumerator object that can be used to iterate through the collection.
