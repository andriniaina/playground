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


// 15h45
class ShortestSubString
{
    public static void shortestSubstring(string s)
    {
        /*
        var allChars = s.Distinct().ToList();
        var size = allChars.Count;
        for (int i = 0; i < s.Length; i += size)
        {
            var ss = s.Substring(i, size);
            if (ss.Distinct().Count() >= allChars.Count)
                yield return ;

                size = size *2;
        }
         */

        /*
        var (compressed, allChars) = compress(s);
        var maxSpan = allChars.Max()-allChars.Min();
         */
    }
/*
    private static (IList<KV<char, int>>, HashSet<char>) compress(string s)
    {
        var result = new List<KV<char, int>>();
        var hs = new HashSet<char>();
        char previousChar = '?';
        foreach (var c in s)
        {
            if (previousChar != c)
            {
                result.Add(new KV<char, int>(c, 1));
                hs.Add(c);
            }
            else
            {
                result[result.Count - 1].V++;
            }
        }
        return (result, hs);
    }
 */
}