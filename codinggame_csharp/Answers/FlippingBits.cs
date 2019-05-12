using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

public class FlippingBits
{

    // Complete the flippingBits function below.
    public static long flippingBits(long n)
    {
            var i = Convert.ToUInt32(n);
            var bytes = BitConverter.GetBytes(i);
            var ba = new BitArray(bytes);
            var b1 = new BitArray(Enumerable.Range(0, 32).Select(x => true).ToArray());
            var ba_xor = ba.Xor(b1);
            var ba_xor_long = BitArrayUtils. BitArrayToByteArray(ba_xor);
            var n_xor = BitConverter.ToUInt32(ba_xor_long,0);
            return n_xor;
    }
}
