# USBPipeCollection

Namespace: Nefarius.Drivers.WinUSB

Collection of UsbPipe objects

```csharp
public sealed class USBPipeCollection : System.Collections.Generic.IEnumerable`1[[Nefarius.Drivers.WinUSB.USBPipe, Nefarius.Drivers.WinUSB, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null]], System.Collections.IEnumerable
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [USBPipeCollection](./nefarius.drivers.winusb.usbpipecollection.md)<br>
Implements [IEnumerable&lt;USBPipe&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1), [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/system.collections.ienumerable)

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
