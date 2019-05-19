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
using System.Numerics;

public class FunWithStrings
{

    /*
     * Complete the 'countPerms' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts INTEGER n as parameter.

    public static int countPerms(int n)
    {
        return
            countPerms('a', n )
            + countPerms('e', n )
            + countPerms('i', n )
            + countPerms('o', n )
            + countPerms('u', n )
        ;
    }

    public static int countPerms(char c, int n)
    {
        if (n <= 0)
            return 0;
        if (n == 1)
            return 1;

        switch (c)
        {
            case 'a':
                return countPerms('e', n - 1);
            case 'e':
                return countPerms('a', n - 1) + countPerms('i', n - 1);
            case 'i':
                return countPerms('a', n - 1) + countPerms('e', n - 1) + countPerms('o', n - 1) + countPerms('u', n - 1);
            case 'o':
                return countPerms('i', n - 1) + countPerms('u', n - 1);
            case 'u':
                return countPerms('a', n - 1);
        }
        throw new ArgumentException($"char={c} is not a valid vowyel");
    }

     */

    class R
    {
        public BigInteger A = 0;
        public BigInteger E = 0;
        public BigInteger I = 0;
        public BigInteger O = 0;
        public BigInteger U = 0;

        public BigInteger Sum
        {
            get
            {
                return this.A + this.E + this.I + this.O + this.U;
            }
        }
    }
    public static int countPerms(int n)
    {
        if (n <= 0)
            return 0;

        const int M = 1000000007;
        var currentPossibilities = new R { A = 1, E = 1, I = 1, O = 1, U = 1 };
        for (int i = 1; i < n; i++)
        {
            var nextPossibilities = new R(); //  currentPossibilities.SelectMany(t => Tuple.Create() getNext(t.Item1)).ToList();
            nextPossibilities.A = currentPossibilities.E + currentPossibilities.I + currentPossibilities.U;
            nextPossibilities.E = currentPossibilities.A + currentPossibilities.I;
            nextPossibilities.I = currentPossibilities.E + currentPossibilities.O;
            nextPossibilities.O = currentPossibilities.I;
            nextPossibilities.U = currentPossibilities.I + currentPossibilities.O;

            if (nextPossibilities.A > M && nextPossibilities.E > M && nextPossibilities.I > M && nextPossibilities.O > M && nextPossibilities.U > M)
            {
                nextPossibilities.A = nextPossibilities.A % M;
                nextPossibilities.E = nextPossibilities.E % M;
                nextPossibilities.I = nextPossibilities.I % M;
                nextPossibilities.O = nextPossibilities.O % M;
                nextPossibilities.U = nextPossibilities.U % M;
            }


            currentPossibilities = nextPossibilities;
        }
        return (int)(currentPossibilities.Sum % M);
    }
    public static char[] getNext(char c)
    {
        switch (c)
        {
            case 'a':
                return new char[] { 'e' };
            case 'e':
                return new char[] { 'a', 'i' };
            case 'i':
                return new char[] { 'a', 'e', 'o', 'u' };
            case 'o':
                return new char[] { 'i', 'u' };
            case 'u':
                return new char[] { 'a' };
        }
        throw new ArgumentException($"char={c} is not a valid vowyel");
    }
}

