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

public class CustomerCapa
{

    // Complete the howManyAgentsToAdd function below.
    public static int howManyAgentsToAdd(int noOfCurrentAgents, List<List<int>> callsTimes)
    {
        if (callsTimes == null || callsTimes.Count == 0)
            return 0;

        //callsTimes = callsTimes.OrderBy(x => x[0]).ToList();
        int nb = 0;
        for (int j = 0; j < callsTimes.Count; j++)
        {
            for (int i = 0; i < callsTimes.Count; i++)
            {
                if (i != j)
                {
                    var start_i = callsTimes[i][0];
                    var end_i = callsTimes[i][1];
                    var start_j = callsTimes[j][0];
                    var end_j = callsTimes[j][1];

                    if (end_i >= start_j && end_i < end_j)
                    {
                        nb++;
                    }
                }
            }
        }
        return noOfCurrentAgents-nb;
    }
}