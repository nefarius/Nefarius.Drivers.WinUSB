# USBPowerPolicy

Namespace: Nefarius.Drivers.WinUSB

Describes the power policy for a USB device

```csharp
public sealed class USBPowerPolicy
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [USBPowerPolicy](./nefarius.drivers.winusb.usbpowerpolicy.md)

## Properties

### <a id="properties-autosuspend"/>**AutoSuspend**

When true, the device is auto-suspended when either no transfers are pending, or only In transfers on an
 interrupt or bulk endpoint are pending.
 Default value is determined by the DefaultIdleState registry value.

```csharp
public bool AutoSuspend { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### <a id="properties-suspenddelay"/>**SuspendDelay**

The minimum amount of milliseconds that must pass before the device can be suspended.

```csharp
public int SuspendDelay { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
