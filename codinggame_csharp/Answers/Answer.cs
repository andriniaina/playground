using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

class Solution
{

    /*
     * Complete the function below.
     */
    static int[] jobOffers(int[] scores, int[] lowerLimits, int[] upperLimits)
    {

        if (scores == null) return new int[0];
        if (lowerLimits == null) return new int[0];
        if (upperLimits == null) return new int[0];
        Array.Sort(scores);

        var result = lowerLimits;

        for (int i = 0; i < lowerLimits.Length; i++)
        {
            var min = lowerLimits[i];
            var max = upperLimits[i];
            // find min
            var min_i = 0;
            var max_i = scores.Length / 2;
            if (scores[0] >= min)
            {
                min_i = 0;
            }
            else
            {
                min_i = scores.Length / 2;
                var min_i_a = 0;
                var min_i_b = scores.Length;
                var shouldGoRight = false;
                var shouldGoLeft = false;
                do
                {
                    shouldGoRight = scores[min_i] < min;
                    shouldGoLeft = scores[min_i] > min;
                    if (shouldGoRight)
                    {
                        min_i_a = min_i;
                        min_i = (min_i + min_i_b) / 2;
                    }
                    else if(shouldGoLeft)
                    {
                        min_i_b = min_i;
                        min_i = (min_i + min_i_a) / 2;
                    }

                    if(min_i_a+1>=min_i_b) break;
                } while (shouldGoRight || shouldGoLeft);
            }
            if (scores[scores.Length - 1] <= max)
            {
                max_i = scores.Length;
            }
            else
            {
                max_i = scores.Length / 2;
                var max_i_a = 0;
                var max_i_b = scores.Length;
                var shouldGoLeft = false;
                var shouldGoRight = false;
                do
                {
                    shouldGoRight = scores[max_i] < max;
                    shouldGoLeft = scores[max_i] > max;
                    if (shouldGoRight)
                    {
                        max_i_a = max_i;
                        max_i = (max_i + max_i_b) / 2;
                    }
                    else if(shouldGoLeft)
                    {
                        max_i_b = max_i;
                        max_i = (max_i + max_i_a) / 2;
                    }
                    if(max_i_a+1>=max_i_b) break;
                } while (shouldGoRight || shouldGoLeft);
            }
            if(min_i==max_i)
            result[i] = 1;
            else
            result[i] = max_i - min_i;
        }
        return result;
    }


    /*
     * Complete the function below. 
     * Base url: https://jsonmock.hackerrank.com/api/movies/search/?Title=
     */
    static string[] getMovieTitles(string substr)
    {
        return HttpGetTitles(substr).OrderBy(x => x).ToArray();
    }
    static IEnumerable<string> HttpGetTitles(string substr)
    {
        Response response;
        var i = 1;
        do
        {
            var url = $"https://jsonmock.hackerrank.com/api/movies/search/?Title={substr}&page={i}";
            Console.WriteLine($"url={url}");
            var sResponse = new HttpClient().GetAsync(url).Result.Content.ReadAsStringAsync().Result;

            response = JsonConvert.DeserializeObject<Response>(sResponse);
            foreach (var movie in response.Data)
            {
                yield return movie.Title;
            }
            ++i;
        } while (response.Page < response.Total_Pages);
    }

    public class Response
    {
        public int Page { get; set; }
        public int PerPage { get; set; }
        public int Total { get; set; }
        public int Total_Pages { get; set; }
        public List<Movie> Data { get; set; }

    }
    public class Movie
    {
        public string Title { get; set; }
    }
    /*
    public static void Main()
    {
        var x1 = jobOffers(new int[]{1,3,5,6,8}, new int[]{2}, new int[]{6}); // 3
        var x2 = jobOffers(new int[]{4,8,7}, new int[]{2,4}, new int[]{8,4}); //3,1
    }
    */
}