# USBTransferType

Namespace: Nefarius.Drivers.WinUSB

USB transfer type enumeration

```csharp
public enum USBTransferType
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [Enum](https://docs.microsoft.com/en-us/dotnet/api/system.enum) → [USBTransferType](./nefarius.drivers.winusb.usbtransfertype.md)<br>
Implements [IComparable](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable), [IFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.iformattable), [IConvertible](https://docs.microsoft.com/en-us/dotnet/api/system.iconvertible)

## Fields

| Name | Value | Description |
| --- | --: | --- |
| Control | 0 | The pipe is a control transfer pipe |
| Isochronous | 1 | The pipe is an isochronous transfer pipe |
| Bulk | 2 | The pipe is a bulk transfer pipe |
| Interrupt | 3 | The pipe is an interrupt transfer pipe |
