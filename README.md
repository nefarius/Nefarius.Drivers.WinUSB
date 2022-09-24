<img src="assets/NSS-128x128.png" align="right" />

# Nefarius.Drivers.WinUSB

> *This is a fork of the fantastic [`MadWizard.WinUSBNet`](https://github.com/snikeguo/winusbnet) project by Thomas Bleeker and contributors.*

Managed wrapper for the [WinUSB APIs](https://learn.microsoft.com/en-us/windows-hardware/drivers/usbcon/winusb) on Microsoft Windows.

## Changes of this fork

- Replaced P/Invoke code with [source generators](https://github.com/microsoft/CsWin32)
- Changed namespace to `Nefarius.Drivers.WinUSB` to avoid conflicts with the origin library
- Removed device notification listener as my other lib [`Nefarius.Utilities.DeviceManagement`](https://github.com/nefarius/Nefarius.Utilities.DeviceManagement) provides a drop-in replacement without depending on WinForms or WPF
- Added `USBDevice::GetSingleDeviceByPath` to allow opening a WinUSB device via device path (symbolic link)

## Features

> *Taken verbatim from [the source repository](https://github.com/snikeguo/winusbnet/blob/master/README.md).*

- MIT licensed with C# source code available (free for both personal and commercial use)
- CLS compliant library (usable from all .NET languages such as C#, VB.NET and C++.NET)
- Synchronous and asynchronous data transfers
- Support for 32-bit and 64-bit Windows versions
- Support for multiple interfaces and endpoints
- Intellisense documentation

## Related documentation

- [Library reference online](http://madwizard-thomas.github.io/winusbnet/docs/)
- [Online wiki with short howto](https://github.com/madwizard-thomas/winusbnet/wiki)
- [WinUSB overview](https://docs.microsoft.com/en-us/windows-hardware/drivers/usbcon/winusb)
- [How to Access a USB Device by Using WinUSB Functions](https://docs.microsoft.com/en-us/windows-hardware/drivers/usbcon/-using-winusb-api-to-communicate-with-a-usb-device)
- [Jan Axelson's page on WinUSB](http://janaxelson.com/winusb.htm)
