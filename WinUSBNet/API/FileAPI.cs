/*  WinUSBNet library
 *  (C) 2010 Thomas Bleeker (www.madwizard.org)
 *
 *  Licensed under the MIT license, see license.txt or:
 *  http://www.opensource.org/licenses/mit-license.php
 */

/* NOTE: Parts of the code in this file are based on the work of Jan Axelson
 * See http://www.lvr.com/winusb.htm for more information
 */

using System;

namespace Nefarius.Drivers.WinUSB.API;

/// <summary>
///     API declarations relating to file I/O (and used by WinUsb).
/// </summary>
internal sealed class FileIO
{
    public const Int32 ERROR_IO_PENDING = 997;
    public static readonly IntPtr INVALID_HANDLE_VALUE = new(-1);
}
