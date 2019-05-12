using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Solution
{
    static void Main(string[] args)
    {
        int N = int.Parse(Console.ReadLine());
        var strengths = ReadStrength(N).OrderBy(x=>x).ToList();
        var min = ComputeDiffs(strengths).Min();

        Console.WriteLine(min);
    }

    private static IEnumerable<int> ComputeDiffs(List<int> strengths)
    {
        var previous = strengths.First();
        for (int i = 1; i < strengths.Count; i++)
        {
            var current = strengths[i];
            var D = Math.Abs(previous - current);
            yield return D;
            previous = current;
        }
    }

    private static IEnumerable<int> ReadStrength(int n)
    {
        for (int i = 0; i < n; i++)
        {
            yield return int.Parse(Console.ReadLine());
        }
    }
}
