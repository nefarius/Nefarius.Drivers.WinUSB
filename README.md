# <img src="assets/NSS-128x128.png" align="left" />Nefarius.Drivers.WinUSB

> *This is a fork of the fantastic [`MadWizard.WinUSBNet`](https://github.com/snikeguo/winusbnet) project by Thomas
Bleeker and contributors.*

[![.NET](https://github.com/nefarius/Nefarius.Drivers.WinUSB/actions/workflows/build.yml/badge.svg)](https://github.com/nefarius/Nefarius.Drivers.WinUSB/actions/workflows/build.yml)
![.NET 4.7.2](https://img.shields.io/badge/.NET-4.7.2-blue)
![.NET 4.8](https://img.shields.io/badge/.NET-4.8-blue)
![.NET 6/7/8](https://img.shields.io/badge/.NET-6%2F7%2F8-blue)
[![NuGet Version](https://img.shields.io/nuget/v/Nefarius.Drivers.WinUSB)](https://www.nuget.org/packages/Nefarius.Drivers.WinUSB/)
[![NuGet](https://img.shields.io/nuget/dt/Nefarius.Drivers.WinUSB)](https://www.nuget.org/packages/Nefarius.Drivers.WinUSB/)

Managed wrapper for the [WinUSB APIs](https://learn.microsoft.com/en-us/windows-hardware/drivers/usbcon/winusb) on
Microsoft Windows.

## Changes of this fork

- Replaced P/Invoke code with [source generators](https://github.com/microsoft/CsWin32)
- Changed namespace to `Nefarius.Drivers.WinUSB` to avoid conflicts with the origin library
- Removed device notification listener as my other
  lib [`Nefarius.Utilities.DeviceManagement`](https://github.com/nefarius/Nefarius.Utilities.DeviceManagement) provides
  a drop-in replacement without depending on WinForms or WPF
- Added `USBDevice::GetSingleDeviceByPath` to allow opening a WinUSB device via device path (symbolic link)

## Documentation

[Link to API docs](docs/index.md).

### Generating documentation

- `dotnet build -c:Release`
- `dotnet tool install -g Nefarius.Tools.XMLDoc2Markdown`
- `xmldoc2md .\bin\net8.0-windows\Nefarius.Drivers.WinUSB.dll .\docs\`

## TO-DOs

- Migrate all buffers to Spans where possible
- Migrate the old asynchronous APIs to `async`/`await` and Tasks
- More/better tests 😇

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
- [How to Access a USB Device by Using WinUSB Functions](https://learn.microsoft.com/en-us/windows-hardware/drivers/usbcon/using-winusb-api-to-communicate-with-a-usb-device)
- [Jan Axelson's page on WinUSB](http://janaxelson.com/winusb.htm)
- [C#/Win32 P/Invoke Source Generator](https://github.com/microsoft/CsWin32)
