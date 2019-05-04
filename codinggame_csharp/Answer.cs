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
class SortAlgorithms
{
    static void BubbleSort<T>(T[] a) where T : IComparable
    {
        var hasSwapped = false;
        do
        {
            hasSwapped = false;
            for (int i = 0; i < a.Length - 1; i++)
            {
                if (a[i].CompareTo(a[i + 1]) > 0)
                {
                    var tmp = a[i + 1];
                    a[i + 1] = a[i];
                    a[i] = tmp;
                    hasSwapped = true;
                }
            }
        } while (hasSwapped);
    }

    static void HeapSort<T>(T[] a) where T : IComparable
    {
        bool swapIfNecessary2(ref T v1, ref T v2)
        {
            if (v2.CompareTo(v1) > 0)
            {
                var tmp = v2; v2 = v1; v1 = tmp;
                return true;
            }
            return false;
        }
        bool swapIfNecessary3(ref T v1, ref T v2, ref T v3)
        {
            return
                swapIfNecessary2(ref v1, ref v2)
                |
                swapIfNecessary2(ref v1, ref v3);
        }
        int getParent(int pi)
        {
            return pi % 2 == 0 ? pi / 2 - 1 : pi / 2;
        }
        (int, int) getChildren(int i)
        {
            var c1 = 2 * i + 1;
            var c2 = 2 * i + 2;
            return (c1, c2);
        }
        // heapify
        for (int i = 0; i < (a.Length); i++)
        {
            var (c1, c2) = getChildren(i);
            if (c2 < a.Length)
                swapIfNecessary3(ref a[i], ref a[c1], ref a[c2]);
            else if (c1 < a.Length)
                swapIfNecessary2(ref a[i], ref a[c1]);
            stdout(join(a));
            // bubble up the parent i node if necessary
            var pi = i;
            while (pi > 0)
            {
                var parent = getParent(pi);
                if(!swapIfNecessary2(ref a[parent], ref a[pi]))
                    break;
                stdout(join(a));
                pi = parent;
            }
        }

        // sort: extract roots
        for (int i = 0; i < a.Length; i++)
        {
            var rightBoundary = a.Length - 1 - i;
            // extract root
            swapIfNecessary2(ref a[rightBoundary], ref a[0]);
            stdout(join(a));

            // fix the tree: bubble down the wrong root
            var c = 0;
            while (2 * c + 1 < rightBoundary)
            {
                var (c1, c2) = getChildren(c);
                if (c2 >= a.Length || a[c1].CompareTo(a[c2]) > 0)
                {
                    if(!swapIfNecessary2(ref a[c], ref a[c1]))
                        break;
                    stdout(join(a));
                    c = c1;
                }
                else// 
                {
                    if (c2 < rightBoundary)
                    {
                        if(!swapIfNecessary2(ref a[c], ref a[c2]))
                            break;
                        stdout(join(a));
                    }
                    c = c2;
                }
            }
        }
    }

    /*
67
34                   6
8          9         5       6
7    7     8    8    4    4  5 5
1 3  3 3   4 1  4 2  2 2  3
     */


    static void Main(string[] args)
    {
        var array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 1, 2, 3, 4, 5, 6, 7, 8, 3, 4, 67, 8, 34, 2, 2, 4, 5 };
        stdout(join(array));
        SortAlgorithms.HeapSort(array);
        stdout(join(array));
    }

    [System.Diagnostics.Conditional("DEBUG")]
    private static void stdout(string v)
    {
        Console.WriteLine(v);
    }

    private static string join<T>(IEnumerable<T> array)
    {
        return string.Join(",", array);
    }
}
