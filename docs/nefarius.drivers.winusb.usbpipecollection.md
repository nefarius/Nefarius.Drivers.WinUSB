# USBPipeCollection

Namespace: Nefarius.Drivers.WinUSB

Collection of UsbPipe objects

```csharp
public sealed class USBPipeCollection : System.Collections.Generic.IEnumerable<Nefarius.Drivers.WinUSB.USBPipe>, System.Collections.IEnumerable
```

Inheritance [Object](https://learn.microsoft.com/dotnet/api/system.object) → [USBPipeCollection](./nefarius.drivers.winusb.usbpipecollection.md)<br>
Implements [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)<[USBPipe](./nefarius.drivers.winusb.usbpipe.md)>, [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.ienumerable)<br>
Attributes [DefaultMemberAttribute](https://learn.microsoft.com/dotnet/api/system.reflection.defaultmemberattribute)

## Properties

### <a id="properties-item"/>**Item**

```csharp
public USBPipe Item { get; }
```

#### Property Value

[USBPipe](./nefarius.drivers.winusb.usbpipe.md)<br>

## Methods

### <a id="methods-getenumerator"/>**GetEnumerator()**

Returns a typed enumerator that iterates through a collection.

```csharp
public IEnumerator<USBPipe> GetEnumerator()
```

#### Returns

The enumerator object that can be used to iterate through the collection.
