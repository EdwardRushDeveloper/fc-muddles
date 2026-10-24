using System;
using System.Collections.Generic;

namespace LargestPossibleIntegerWithNegative
{
    /*
        2441. Largest Positive Integer That Exists With Its Negative
        Difficulty : Easy

        https://leetcode.com/problems/largest-positive-integer-that-exists-with-its-negative/

        Given an integer array nums that does not contain any zeros, find the largest positive
        integer k such that -k also exists in the array.

        Return the positive integer k. If there is no such integer, return -1.

        Example 1:

        Input: nums = [-1,2,-3,3]
        Output: 3
        Explanation: 3 is the only valid k we can find in the array.
        Example 2:

        Input: nums = [-1,10,6,7,-7,1]
        Output: 7
        Explanation: Both 1 and 7 have their corresponding negative values in the array. 7 has a larger value.

        Example 3:
        Input: nums = [-10,8,6,7,-2,-3]
        Output: -1
        Explanation: There is no a single valid k, we return -1.

        Constraints:

        1 <= nums.length <= 1000
        -1000 <= nums[i] <= 1000
        nums[i] != 0

        Approach : HashSet, single pass

            For each number, check if its opposite (-number) was already seen.
            If it was, the pair exists, so keep the larger of the two positives.

        Time  : O(N)   - every number is visited once, and HashSet Contains/Add are O(1) on average
        Space : O(N)   - in the worst case every number is stored in the HashSet

        Where Log(N) fits in  (see docs/logN/readme.md)

            Log(N) means each step cuts the work in half. This solution never halves anything,
            so there is no Log(N) in it. It shows up in the other ways this problem can be solved:

            1. Sort + Two Pointers : O(N Log N) time, O(1) space
                 Sorting costs O(N Log N): the sort splits the array in half about Log N times,
                 and does N work at each level. After sorting, a pointer at the most negative
                 number and one at the most positive walk toward each other in O(N).
                 Slower than the HashSet, but uses no extra memory.

            2. Sort + Binary Search : O(N Log N) time
                 Sort, then for each positive number, binary search for its negative.
                 Each binary search is O(Log N) because it halves the search range every step,
                 and doing it for N numbers gives N * Log N.

            3. SortedSet instead of HashSet : O(N Log N) time
                 A SortedSet is a balanced tree, so Contains/Add are O(Log N) instead of O(1).
                 Same code, N lookups at Log N each.

            Compare for N = 1000 (the max in the constraints):
                 O(N)       ~  1,000 steps
                 O(N Log N) ~ 10,000 steps   (Log 1000 is about 10)

            The HashSet wins because an O(1) lookup beats an O(Log N) lookup.
    */
    public class Program
    {
        public static void Main()
        {
            //int[] numbers = {1,2,3,-3,4,5,-1,-10,10,6,7,-7,-2,-3};
            //Example 3 with a 10 added, so -10 now has a pair. Expected output: 10
            int[] numbers = { -10, 8, 6, 7, -2, -3, 10 };
            int largest = FindMaxK(numbers);
            Console.WriteLine(largest);
        }


        //returns the largest k where both k and -k are in nums, or -1 if there is none
        //Time O(N), Space O(N)
        public static int FindMaxK(int[] nums)
        {

            //create our store
            //HashSet lookups are O(1). A SortedSet would be O(Log N) per lookup, making the whole method O(N Log N)
            HashSet<int> seen = new HashSet<int>();
            int largest = -1;

            //loop through all of the numbers just once, and record the postive numbers to the HashSet
            foreach (int i in nums)
            {

                //we will check each number and see if its inverse exists. If id does not, then we 
                //add the number (positive or negative) to the HashSet. 
                //Example: 
                // 1. We encounter -10, but we check the inverse 10 in the HashSet.
                // 2. If the inverse is not found (10), then we add -10 to the HashSet.
                // 3. When we encounter 10, we check the inverse -10 in the HashSet.
                // 4. If is found because we stored it in step 2.
                // 5. We then use the Math.Max function to see if the Absolute value of the current number is larger than the largest variable. If so, largest is replaced.
                if (seen.Contains(i * -1))
                {
                    largest = Math.Max(largest, Math.Abs(i));
                }
                seen.Add(i);
            }


            return largest;

        }
    }
}
