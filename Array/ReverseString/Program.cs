using System;

namespace ReverseString
{
    class Program
    {
        /*


                344. Reverse String
                Easy
                Manipulating Strings
                        https://leetcode.com/problems/reverse-string/

                Write a function that reverses a string. The input string is given as an array of characters s.

                Recusive Demonstration of the reversing of a string in place memory allocation. 

                    Example 1:

                    Input: s = ["h","e","l","l","o"]
                    Output: ["o","l","l","e","h"]
                    Example 2:

                    Input: s = ["H","a","n","n","a","h"]
                    Output: ["h","a","n","n","a","H"]
                    

                    Constraints:

                    1 <= s.length <= 105
                    s[i] is a printable ascii character.
                    

                    Follow up: Do not allocate extra space for another array. You must do this by modifying the input array in-place with O(1) extra memory.

        */
        static void Main(string[] args)
        {
            //LeetCode compliant solution: iterative, in-place, O(1) extra memory
            char[] s = new char[]{'h','e','l','l','o'};
            ReverseString(s);
            Console.WriteLine($"Iterative: {new string(s)}");

            //Recursive demonstration of winding in and winding out
            char[] w = new char[]{'h','e','l','c','l','o','s'};
            ReverseStringRecursive(w, 0, w.Length - 1);
            Console.WriteLine($"Recursive: {new string(w)}");
        }

        //////////////////
        // LeetCode 344 solution. Matches the LeetCode signature ReverseString(char[] s).
        // Two pointers start at each end of the array and walk toward the center,
        // swapping as they go. Only two int indexes are used, so extra memory is O(1)
        // and the array is reversed in place. Time is O(n), n/2 swaps.
        //
        //  0[h]1[e]2[l]3[l]4[o]
        //  left = 0, right = 4   [h]ell[o] -> oellh
        //  left = 1, right = 3   o[e]l[l]h -> olleh
        //  left = 2, right = 2   pointers meet, center is not swapped, stop
        //////////////////
        public static void ReverseString(char[] s)
        {
            //the start of the string
            int left  = 0;
            //the end of the string
            int right = s.Length - 1;

            //loop while the left value is less than the right value
            while(left < right)
            {   
                //swap the values at the left and right indexes, then move the indexes toward each other
                (s[left], s[right]) = (s[right], s[left]);
                //move the left index up and the right index down, so they move toward each other
                left++;
                right--;
            }
        }

        //////////////////
        // Basic Recursive Function to demonstrate the winding in, and then winding out of
        // a recursive call to a function.
        // The function can be reused to reverse words in a sentence of a string.
        // Note: this is not the LeetCode answer, each recursive call uses a stack frame
        // so extra memory is O(n). See ReverseString(char[] s) above for the O(1) version.
        //////////////////
        public static void ReverseStringRecursive(char[] w, int startIndex, int endIndex)
        {
            //base case to stop the recursive call into the array.
            //startIndex and endIndex move toward each other on every call. Once they meet
            //(odd length, the center character) or cross (even length) there is nothing left to swap.
            if (startIndex >= endIndex) return;

            //our winding recursive call, moving both ends one step toward the center.
            //Because both indexes move, (startIndex, endIndex) is always a mirror pair within
            //the range we were given, so this works for any sub-range of the array
            //(e.g. a single word inside a sentence), not just a range starting at 0.
            ReverseStringRecursive(w, startIndex + 1, endIndex - 1);

            //as our recursive call is exited, we swap the mirror pair held by this call.
            //The operation will replace values starting from the center of the string winding out to the outside of the string
            //Example for index value of 0[h]1[e]2[l]3[c]4[l]5[o]6[s] helclos
            //  winding in:  (0,6) -> (1,5) -> (2,4) -> (3,3) base case, return
            //  winding out:
            //      startIndex = 2, endIndex = 4
            //      he[l]c[l]os
            //      startIndex = 1, endIndex = 5
            //      h[e]lcl[o]s
            //      startIndex = 0, endIndex = 6
            //      [h]elclo[s]
            Console.WriteLine($"Swapping index (startIndex){startIndex}({w[startIndex]}) for index (endIndex){endIndex}({w[endIndex]})");
            (w[startIndex], w[endIndex]) = (w[endIndex], w[startIndex]);

            /*
            On the winding out action, the order of index switching is reflected below.

                helclos   becomes  solcleh
                Swapping index (startIndex)2(l) for index (endIndex)4(l)
                Swapping index (startIndex)1(e) for index (endIndex)5(o)
                Swapping index (startIndex)0(h) for index (endIndex)6(s)

            Notice the center index of the string is not swapped, it is the base case.

            Recursion depth is n/2 calls. This still uses O(n) stack space, so it demonstrates
            recursion but does not meet the O(1) extra memory follow-up; ReverseString(char[] s)
            does the same swaps with a loop and meets it.

            */
        }
    }
}
