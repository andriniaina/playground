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

public class SortHotel
{



    // Complete the sort_hotels function below.
    public static List<int> sort_hotels(string keywords, List<int> hotel_ids, List<string> reviews)
    {
        if (keywords == null || hotel_ids == null || reviews == null)
            return new List<int>();
        var lowerCaseKeywords = keywords
            .Split(' ')
            .Select(s => s.Replace(".", string.Empty))
            .Select(s => s.Replace(",", string.Empty))
            .Where(k => k.Length > 0)
            .Distinct()
            .ToList();
        if (lowerCaseKeywords.Count == 0)
            return new List<int>();

        var counts = hotel_ids.Zip(reviews.Select(r => r.ToLowerInvariant()), (id, lowerCaseReview) =>
        {
            var count = lowerCaseKeywords.Count(k => lowerCaseReview.IndexOf(k) >= 0);
            return new { Id = id, Count = count };
        })
        .GroupBy(t => t.Id, t => t.Count)
        .Select(g => new { Id = g.Key, Sum = g.Sum() })
        .OrderByDescending(kv => kv.Sum)
        .ThenBy(kv => kv.Id)
        .Select(kv => kv.Id)
        .ToList()
        ;

        return counts;
    }
}