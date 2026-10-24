using System;
using System.Collections.Generic;

namespace MaxSumOfaPairWithEqualSumOfDigits
{
    /*
        2342. Max Sum of a Pair With Equal Sum of Digits
        Difficulty : Medium

        https://leetcode.com/problems/max-sum-of-a-pair-with-equal-sum-of-digits/

        You are given a 0-indexed array nums consisting of positive integers. You can choose two
        indices i and j, such that i != j, and the sum of digits of the number nums[i] is equal
        to that of nums[j].

        Return the maximum value of nums[i] + nums[j] that you can obtain over all possible
        indices i and j that satisfy the conditions. If no such pair of indices exists, return -1.

        Example 1:

        Input: nums = [18,43,36,13,7]
        Output: 54
        Explanation: The pairs (i, j) that satisfy the conditions are:
        - (0, 2), both numbers have a sum of digits equal to 9, and their sum is 18 + 36 = 54.
        - (1, 4), both numbers have a sum of digits equal to 7, and their sum is 43 + 7 = 50.
        So the maximum sum that we can obtain is 54.

        Example 2:

        Input: nums = [10,12,19,14]
        Output: -1
        Explanation: There are no two numbers that satisfy the conditions, so we return -1.

        Constraints:

        1 <= nums.length <= 10^5
        1 <= nums[i] <= 10^9
    */

    /// <summary>
    /// Demonstrates a solution for the LeetCode problem "Max Sum of a Pair With Equal Sum of Digits".
    /// The program calculates the maximum sum of two numbers whose individual digits have the same total.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Runs a small sample case to illustrate the algorithm and print the result.
        /// </summary>
        public static void Main()
        {
            Console.WriteLine("Max Sum of a Pair With Equal Sum of Digits");

            int[] nums = { 18, 43, 36, 13, 7 };
            int result = MaximumSum(nums);
            Console.WriteLine($"Input: nums = [{string.Join(", ", nums)}]");
            Console.WriteLine($"Output: {result}");
        }

        /// <summary>
        /// Finds the largest possible sum of two numbers whose digit sums are equal.
        /// </summary>
        /// <param name="nums">The array of positive integers to evaluate.</param>
        /// <returns>
        /// The maximum value of <c>nums[i] + nums[j]</c> for any pair with matching digit sums;
        /// otherwise, <c>-1</c> if no valid pair exists.
        /// </returns>
        public static int MaximumSum(int[] nums)
        {
            // Stores the best value seen for each digit-sum total.
            Dictionary<int, int> digitSums = new Dictionary<int, int>();

            // Tracks the maximum valid pair sum discovered so far.
            int bestReturn = -1;

            foreach (int i in nums)
            {
                // Reduce the current number to the sum of its digits.
                int temp = i;
                int digitSum = 0;
                while (temp > 0)
                {
                    // Extract the last digit and add it to the running total.
                    digitSum += temp % 10;
                    // Remove the last digit before repeating the process.
                    temp /= 10;
                }
                int output = 0;

                Console.WriteLine($"Current Number {i}, Current digitSum {digitSum}");

                // If another value with the same digit sum has already been seen,
                // we have a valid pair and can update the best answer.
                if (digitSums.TryGetValue(digitSum, out output))
                {
                    Console.WriteLine($"Digit Sum Found {output}");
                    digitSums[i] = i + output;

                    bestReturn = Math.Max(bestReturn, i + output);
                }
                else
                {
                    // This is the first number encountered for this digit-sum value.
                    digitSums.TryAdd(digitSum, i);
                }
            }

            // This loop prints the stored values for each digit-sum group for debugging.
            foreach (int k in digitSums.Keys)
            {
                Console.WriteLine(digitSums[k]);
            }

            return bestReturn;
        }
    }
}
