# USBBaseClass

Namespace: Nefarius.Drivers.WinUSB

USB base class code enumeration, as defined in the USB specification

```csharp
public enum USBBaseClass
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [Enum](https://docs.microsoft.com/en-us/dotnet/api/system.enum) → [USBBaseClass](./nefarius.drivers.winusb.usbbaseclass.md)<br>
Implements [IComparable](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable), [IFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.iformattable), [IConvertible](https://docs.microsoft.com/en-us/dotnet/api/system.iconvertible)

## Fields

| Name | Value | Description |
| --- | --: | --- |
| Unknown | -1 | Unknown non-zero class code. Used when the actual class code does not match any of the ones defined in this enumeration. |
| None | 0 | Base class defined elsewhere (0x00) |
| Audio | 1 | Audio base class (0x01) |
| CommCDC | 2 | Communications and CDC control base class (0x02) |
| HID | 3 | HID base class (0x03) |
| Physical | 5 | Physical base class (0x05) |
| Image | 6 | Image base class (0x06) |
| Printer | 7 | Printer base class (0x07) |
| MassStorage | 8 | Mass storage base class (0x08) |
| Hub | 9 | Hub base class (0x09) |
| CDCData | 10 | CDC data base class (0x0A) |
| SmartCard | 11 | Smart card base class (0x0B) |
| ContentSecurity | 13 | Content security base class (0x0D) |
| Video | 14 | Video base class (0x0E) |
| PersonalHealthcare | 15 | Personal health care base class (0x0F) |
| DiagnosticDevice | 220 | Diagnostic device base class (0xDC) |
| WirelessController | 224 | Wireless controller base class (0xE0) |
| Miscellaneous | 239 | Miscellaneous base class (0xEF) |
| ApplicationSpecific | 254 | Application specific base class (0xFE) |
| VendorSpecific | 255 | Vendor specific base class (0xFF) |
