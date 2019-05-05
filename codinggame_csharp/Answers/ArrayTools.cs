using System.Collections.Generic;

public class ArrayTools
{

    public static string join<T>(IEnumerable<T> array)
    {
        return string.Join(",", array);
    }
    /// <exception>Can return an index bigger than a.Length if no element found</exception>
    public static int FindIndexLesserOrEqualThan(int[] a, int upperBound)
    {
        var left = 0;
        var right = a.Length - 1;
        var i = a.Length / 2;
        while (true)
        {
            if (a[i] > upperBound) // must go left
            {
                right = i;
            }
            else if (a[i] < upperBound) // must go right
            {
                left = i;
            }
            else // it's right there!
            {
                return i;
            }
            i = (left + right) / 2;

            if (left + 1 >= right)
            {
                if (a[left] > upperBound)
                    return -1;
                else if (a[right] <= upperBound)
                    return right;
                else
                    return left;
            }
        }
    }
    /// <exception>Can return an index bigger than a.Length if no element found</exception>
    public static int FindIndexGreaterOrEqualThan(int[] a, int lowerBound)
    {
        var left = 0;
        var right = a.Length - 1;
        var i = a.Length / 2;
        while (true)
        {
            if (a[i] > lowerBound) // must go left
            {
                right = i;
            }
            else if (a[i] < lowerBound) // must go right
            {
                left = i;
            }
            else // it's right there!
            {
                return i;
            }
            i = (left + right) / 2;

            if (left + 1 >= right)
            {
                if (a[right] < lowerBound)
                    return -1;
                if (a[left] >= lowerBound)
                    return left;
                else //if (a[left]>=lowerBound)
                    return right;
            }
        }
    }
}