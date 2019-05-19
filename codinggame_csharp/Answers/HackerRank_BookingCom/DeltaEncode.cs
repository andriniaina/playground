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



public class DeltaEncode
{

    /*
     * Complete the 'delta_encode' function below.
     *
     * The function is expected to return an INTEGER_ARRAY.
     * The function accepts INTEGER_ARRAY numbers as parameter.
     */

    public static List<int> delta_encode(List<int> numbers)
    {
        if (numbers.Count == 0)
            return new List<int>();
        if (numbers.Count == 1)
            return numbers;

        var result = new List<int>();

        var previousNumber = numbers[0];
        result.Add(previousNumber);

        for (int i = 1; i < numbers.Count; i++)
        {
            var n = numbers[i];

            var diff = n - previousNumber;
            if (diff < SByte.MinValue || SByte.MaxValue < diff)
            {
                result.Add(-128);
            }
            result.Add(diff);
            previousNumber = n;
        }
        return result;
    }
}
