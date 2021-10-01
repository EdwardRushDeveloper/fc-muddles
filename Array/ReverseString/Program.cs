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
            char[] w = new char[]{'h','e','l','c','l','o','s'};
            
            ReverseString(w, 0, w.Length - 1);

            for(int i = 0; i < w.Length; i++)
            {
                System.Console.WriteLine(w[i]);
            }
        }

        //////////////////
        // Basic Recursive Function to demonstrate the winding in, and then winding out of 
        // a recursive call to a function.
        // The function can be reused to reverse words in a sentence of a string.
        //////////////////
        public static void ReverseString(char[] w, int startIndex, int endIndex)
        {
            //base case to stop the recursive call into the array.
            //when our startIndex recursive increment finally is greater than we 
            //are at the end of our string so just exit.
            if(startIndex > endIndex)
            {
                return;
            }
            //our winding recursive call using our incrementing start index
            ReverseString(w, startIndex + 1, endIndex);
            
            //as our recursive call is exited, we will swap the values of the startIndex value, and the calculated
            //endIndex value. As long as startIndex is less than the endIndex minus our startIndex
            //we want to swap our startIndex position with our endIndex position
            //The operation will replace values starting from the center of the string winding out to the outside of the string
            //Example for index value of 0[h]1[e]2[l]3[c]4[l]5[0]6[s] helclos
            //      startIndex = 2
            //      endInex    = 4 or (6-2)
            // we will swap index value of 2 and 4
            // he[l]c[l]os
            // and then as the operation winds out, 
            //      startIndex = 1
            //      endIndex   = 5 or (6-1)
            // h[e]lcl[o]s
            if(startIndex < (endIndex - startIndex))
            {
                System.Console.WriteLine($"Swapping index (startIndex){startIndex}({w[startIndex]}) for index (endIndex-startIndex){endIndex-startIndex}({w[endIndex - startIndex]})");
                //holding spot for our current StartIndex value....we are going to replace the current location.
                char current    = w[startIndex];
                //replace the start index with the calculated index value 
                w[startIndex]   = w[endIndex - startIndex] ;
                //now replace the calcualed endIndex with the holding variable value.
                w[endIndex - startIndex] = current;
            }

            /*
            On the winding out action, the order of index switching is reflected below.

                helclos   becomes  solcleh
                Swapping index (startIndex)2(l) for index (endIndex-startIndex)4(l)
                Swapping index (startIndex)1(e) for index (endIndex-startIndex)5(o)
                Swapping index (startIndex)0(h) for index (endIndex-startIndex)6(s)

            Notice the center index of the string is not swapped.

            */
        }
    }
}
