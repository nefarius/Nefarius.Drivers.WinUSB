using Nefarius.Drivers.WinUSB;
using Nefarius.Utilities.DeviceManagement.PnP;

// DualSense under WinUSB via Zadig
Guid guid = Guid.Parse("{BF36F972-4F08-4AC9-A1B1-74D6BF7402E0}");

do
{
    while (!Console.KeyAvailable)
    {
        int instance = 0;

        while (Devcon.FindByInterfaceGuid(guid, out string path, out _, instance++))
        {
            Console.WriteLine($"Opening device {path}");
            USBDevice? device = USBDevice.GetSingleDeviceByPath(path);

            Console.WriteLine($"Disposing device {device}");
            device?.Dispose();
        }
    }
} while (Console.ReadKey(true).Key != ConsoleKey.Escape);