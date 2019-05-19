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

public class Triangle
{
    public enum TriangleType
    {
        INVALID_OR_NOTTRIANGLE = 0,
        EQUILATERAL = 1,
        SIMPLE = 2,
    }
    // Complete the triangle function below.
    public static int triangle(int a, int b, int c)
    {
        if (a <= 0 || b <= 0 || c <= 0)
            return (int)TriangleType.INVALID_OR_NOTTRIANGLE;

        if (a == b && b == c)
            return (int)TriangleType.EQUILATERAL;

        var sides = new int[] { a, b, c }.OrderByDescending(x => x).ToArray();
        if (sides[1] + sides[2] > sides[0])
            return (int)TriangleType.SIMPLE;

        return (int)TriangleType.INVALID_OR_NOTTRIANGLE;

    }
}
