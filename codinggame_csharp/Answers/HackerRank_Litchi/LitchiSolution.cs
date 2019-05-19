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



public class LitchiSolution
{
    public static string betterCompression(string s)
    {
        if (s == null)
            return null;
            
        var r = new Regex(@"([a-z])(\d+)");
        var matches = r.Matches(s);

        var d = new Dictionary<char, int>();
        foreach (var m in matches.Cast<Match>())
        {
            var l = m.Groups[1].Value[0];
            var n = int.Parse(m.Groups[2].Value);
            var found = d.TryGetValue(l, out int value);
            d[l] = value + n;
        }

        var sb = new StringBuilder();
        foreach (var kv in d.OrderBy(kv => kv.Key))
        {
            sb.Append(kv.Key);
            sb.Append(kv.Value);
        }
        return sb.ToString();
    }

}
