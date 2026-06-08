# USBTransferType

Namespace: Nefarius.Drivers.WinUSB

USB transfer type enumeration

```csharp
public enum USBTransferType
```

Inheritance [Object](https://learn.microsoft.com/dotnet/api/system.object) → [ValueType](https://learn.microsoft.com/dotnet/api/system.valuetype) → [Enum](https://learn.microsoft.com/dotnet/api/system.enum) → [USBTransferType](./nefarius.drivers.winusb.usbtransfertype.md)<br>
Implements [IComparable](https://learn.microsoft.com/dotnet/api/system.icomparable), [ISpanFormattable](https://learn.microsoft.com/dotnet/api/system.ispanformattable), [IFormattable](https://learn.microsoft.com/dotnet/api/system.iformattable), [IConvertible](https://learn.microsoft.com/dotnet/api/system.iconvertible)

## Fields

| Name | Value | Description |
| --- | --: | --- |
| Control | 0 | The pipe is a control transfer pipe |
| Isochronous | 1 | The pipe is an isochronous transfer pipe |
| Bulk | 2 | The pipe is a bulk transfer pipe |
| Interrupt | 3 | The pipe is an interrupt transfer pipe |
