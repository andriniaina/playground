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

public class MinimumStrokes
{

    public static int strokesRequired(List<string> lpicture)
    {
        var w = lpicture.First().Length;
        var nbStrokes = 0;
        var p = lpicture.SelectMany(x => x).ToList();
        while (true)
        {
            var i = p.FindIndex(0, p.Count, x => x != '?');
            if (i < 0)
                return nbStrokes;

            propagate('?', w, i, p);
            nbStrokes++;

        }
    }

    private static void propagate(char c, int w, int i, List<char> p)
    {
        var current = p[i];
        p[i] = c;
        var top = i - w;
        var bottom = i + w;
        var left = (i % w) == 0 ? -1 : i - 1;
        var right = (i % w) == w - 1 ? p.Count : i + 1;

        if (top >= 0 && current == p[top]) propagate(c, w, top, p);
        if (bottom < p.Count && current == p[bottom]) propagate(c, w, bottom, p);
        if (left >= 0 && current == p[left]) propagate(c, w, left, p);
        if (right < p.Count && p[right] == current) propagate(c, w, right, p);
    }
}