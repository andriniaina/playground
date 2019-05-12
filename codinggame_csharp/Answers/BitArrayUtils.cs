using System;
using System.Collections;
using System.Linq;

public class BitArrayUtils
{


    public static string ToString(long l)
    {
        var ba = new BitArray(BitConverter.GetBytes(l));
        return ToString(ba);
    }
    public static string ToString(BitArray ba)
    {
        var chars = ba.Cast<bool>().Select(x => (x ? '1' : '0')).ToArray();
        var s = new String(chars);
        return s;
    }
    public static byte[] BitArrayToByteArray(BitArray bits)
    {
        byte[] ret = new byte[(bits.Length - 1) / 8 + 1];
        bits.CopyTo(ret, 0);
        return ret;
    }
}