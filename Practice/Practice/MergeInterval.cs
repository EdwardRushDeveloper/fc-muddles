using System;

namespace Practice;

public class MergeInterval
{

    public static void Execute()
    {
        Console.WriteLine("******************** MergeInterval");
        int[][] intervals = new int[][]
            {
                new int[] { 1, 3 },
                new int[] { 8, 10 },
                new int[] { 2, 6 },
                new int[] { 15, 18 }
            };

         int[][] result = Merge(intervals);
         for(int i = 0; i < result.Length; i++)
        {
            Console.WriteLine($"Interval {result[i][0]} : {result[i][1]}  ");
        }

    }
    public static int[][] Merge(int[][] intervals)
    {
        Array.Sort(intervals, (a, b) => a[0].CompareTo(b[0]));

        List<int[]> mergedIntervals = new List<int[]>{
            intervals[0]
        };


        for(int i = 1; i < intervals.Length; i++)
        {
            //Console.WriteLine($"Interval {intervals[i][0]}, {intervals[i][1]}");
            int[] listItem = mergedIntervals[mergedIntervals.Count - 1];

            if(listItem[1] >= intervals[i][0])
            {
                listItem[1] = Math.Max(listItem[1], intervals[i][1]);
            }
            else
            {
                mergedIntervals.Add(intervals[i]);
            }
        }

        return mergedIntervals.ToArray();
    }

}
