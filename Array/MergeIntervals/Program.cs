namespace MergeIntervals;

/// <summary>
/// Merge intervals main program class
/// </summary>
class Program
{

    /// <summary>
    /// Main entry point of the program
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        //provided intervals out of order
        int[][] intervals = new int[][]
        {
            new int[] { 1, 3 },
            new int[] { 8, 10 },
            new int[] { 2, 6 },
            new int[] { 15, 18 }
        };

        for(int i = 0; i < intervals.Length; i++)
        {
            Console.WriteLine($"[{intervals[i][0]}, {intervals[i][1]}]");
        }
  
    
        int[][] merged = Merge(intervals);
        Console.WriteLine("Merged Intervals:********************************************");

        for(int i = 0; i < merged.Length; i++)
        {
            Console.WriteLine($"[{merged[i][0]}, {merged[i][1]}]");
        }
    }
/// <summary>
/// Merges overlapping intervals in a given array of intervals.
/// </summary>
/// <param name="intervals">Array of intervals to merge</param>
/// <returns>Array of merged intervals</returns>
    public static int[][] Merge(int[][] intervals)
    {
        // Sort the intervals based on the starting point
        Array.Sort(intervals, (a, b) => a[0].CompareTo(b[0]));

        Console.WriteLine("Sorted Intervals:********************************************");

        for(int i = 0; i < intervals.Length; i++)
        {
            Console.WriteLine($"[{intervals[i][0]}, {intervals[i][1]}]");
        }
        //create a list to hold the merged intervals. We initialize this with the first interval sinse we have sorted.
        List<int[]> mergedIntervals = new List<int[]>{
            intervals[0]
        };

        //begin the merging operation
        for(int i = 0; i < intervals.Length; i++)
        {
            //get the the very last element in the mergedIntervals lisg
            int[] lastMergedInterval = mergedIntervals[mergedIntervals.Count - 1];

            Console.WriteLine($"Last Merged Interval: [{intervals[i][0]}, {intervals[i][1]}]");
            //now get the current interval element
            int[] currentInterval = intervals[i];

            //now compare the current interval first element, against the mergedIntervals
            //last element to see if they overlap.
            if(currentInterval[0] <= lastMergedInterval[1])
            {
                //compare the second element of each of the intervals and return the Max of the two.
                lastMergedInterval[1] = Math.Max(lastMergedInterval[1], currentInterval[1]);
            }
            else
            {
                //if they don't overlap, we can add the current interval to the mergedIntervals list
                mergedIntervals.Add(currentInterval);
            }
        }
        return mergedIntervals.ToArray();
    }
}
