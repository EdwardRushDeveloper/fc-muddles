using System;
using System.Text;

namespace CircularCharacterRoll
{
    /*
        Circular Character Roll (also known as "Roll the String")
        Source : HackerRank (coding assessment question)

        You are given a string s made of lowercase English letters and an integer array roll.
        For each value roll[i], "roll" the first roll[i] characters of s forward by one letter
        in the alphabet. The alphabet is circular, so rolling 'z' gives 'a'.

        The rolls are applied one after another, and each roll works on the string produced
        by the previous rolls. Return the final string after every roll has been applied.

        Example 1:

        Input: s = "abz", roll = [3,2,1]
        Output: "dda"
        Explanation:
        - roll 3: "abz" -> "bca"
        - roll 2: "bca" -> "cda"
        - roll 1: "cda" -> "dda"

        Example 2:

        Input: s = "bca", roll = [1,2,3]
        Output: "eeb"
        Explanation:
        - roll 1: "bca" -> "cca"
        - roll 2: "cca" -> "dda"
        - roll 3: "dda" -> "eeb"

        Constraints:

        1 <= s.length <= 10^5
        1 <= roll.length <= 10^5
        1 <= roll[i] <= s.length
        s consists of lowercase English letters only.
    */

    /// <summary>
    /// Console app for the HackerRank problem "Circular Character Roll".
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Runs the sample cases and prints the results.
        /// </summary>
        public static void Main()
        {
            Console.Clear();
            Console.WriteLine("Circular Character Roll");

            // string s = "abz";
            // string s = "bca";
            string s = "zcza";
            // int[] roll = [3, 2, 1];
            // int[] roll = [1, 2, 3];
            int[] roll = [1, 1, 3, 4];
            string result = RollTheString(s, roll);
            Console.WriteLine($"Input: s = \"{s}\", roll = [{string.Join(", ", roll)}]");
            // Console.WriteLine($"Output: \"{result}\" (expected \"dda\")");
            Console.WriteLine($"Output: \"{result}\" (expected \"eeb\")");
        }

        /// <summary>
        /// Rolls the first <c>roll[i]</c> characters of <paramref name="s"/> forward by one letter
        /// for each value in <paramref name="roll"/>, wrapping 'z' around to 'a'.
        /// </summary>
        /// <param name="s">The lowercase string to roll.</param>
        /// <param name="roll">The number of leading characters to roll on each step.</param>
        /// <returns>The string after every roll has been applied.</returns>
        public static string RollTheString(string s, int[] roll)
        {
            /*
                [b, c, a]
                [1, 2, 3]

                b + 1 + 1 + 1 = e
                c + 0 + 1 + 1 = e
                a + 0 + 0 + 1 = b

                [z, c, z, a]
                [1, 1, 3, 4]

                 1  [1            ]
                 1  [1            ]
                 3  [1   1   1    ]
                 4  [1   1   1   1]
                 z   1   1   1   1 = d
                 c   0   0   1   1 = e
                 z   0   0   1   1 = b
                 a   0   0   0   1 = b

                 1 - Increment only index 0
                 1 - Increment only index 0
                 3 - Increment 0 - 2
                 4 - Increment 0 - 4
            */

            int lengthOfString = s.Length;

            // Step 1: Create a difference array initialized to 0
            int[] diff = new int[lengthOfString + 1];

            // Step 2: Mark the roll operations in O(1) time
            /*
                1 - Increment only index 0
                1 - Increment only index 0
                3 - Increment 0 - 2
                4 - Increment 0 - 4

                Explaining the DIF problem
                We are looking at changes made from one number to the next based on the
                roll values.
                For example : [1, 1, 3, 4]
                       (these are the sequence where the rolling increment must stop on each iteration of the array)
                1 = only increment the first character
                1 = only increment the first character
                3 = only increment characters 1, 2, 3
                4 = only increment characters 1, 2, 3, 4

                So what does this mean as the DIFF is built:

                [0] [int] = 4   : incremented by 1 every loop
                [1] [int] = -2  :
                [2] [int] = 0
                [3] [int] = -1
                [4] [int] = 0
            */

            // So what does building the DIFF do
            // conceptualizing the step concept
            foreach (int indexToRoll in roll)
            {
                diff[0] += 1;

                if (indexToRoll < lengthOfString)
                {
                    diff[indexToRoll] -= 1;
                }
            }

            // Step 3: Compute cumulative rolling shifts and build the result string
            StringBuilder result = new StringBuilder();
            int currentShift = 0;

            for (int i = 0; i < lengthOfString; i++)
            {
                currentShift += diff[i];

                // Calculate the new cyclic character position
                int originalPos = s[i] - 'a';
                int newPos = (originalPos + currentShift) % 26;

                // Handle negative bounds if any negative modifications exist
                if (newPos < 0) newPos += 26;

                result.Append((char)('a' + newPos));
            }

            return result.ToString();

        }
    }
}
