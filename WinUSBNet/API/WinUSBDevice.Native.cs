/*  WinUSBNet library
 *  (C) 2010 Thomas Bleeker (www.madwizard.org)
 *
 *  Licensed under the MIT license, see license.txt or:
 *  http://www.opensource.org/licenses/mit-license.php
 */

/* NOTE: Parts of the code in this file are based on the work of Jan Axelson
 * See http://www.lvr.com/winusb.htm for more information
 */

using Windows.Win32;

namespace Nefarius.Drivers.WinUSB.API;

internal enum POLICY_TYPE : uint
{
    SHORT_PACKET_TERMINATE = PInvoke.SHORT_PACKET_TERMINATE,
    AUTO_CLEAR_STALL = PInvoke.AUTO_CLEAR_STALL,
    PIPE_TRANSFER_TIMEOUT = PInvoke.PIPE_TRANSFER_TIMEOUT,
    IGNORE_SHORT_PACKETS = PInvoke.IGNORE_SHORT_PACKETS,
    ALLOW_PARTIAL_READS = PInvoke.ALLOW_PARTIAL_READS,
    AUTO_FLUSH = PInvoke.AUTO_FLUSH,
    RAW_IO = PInvoke.RAW_IO
}
