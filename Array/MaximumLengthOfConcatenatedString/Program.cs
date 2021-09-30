using System;
using System.Collections.Generic;

namespace MaximumLengthOfConcatenatedString
{
    class Program
    {
        /*

                1239. Maximum Length of a Concatenated String with Unique Characters
                Medium
                Manipulating Strings
                    https://www.youtube.com/watch?v=pD3cHFNyW2I
                    https://leetcode.com/problems/maximum-length-of-a-concatenated-string-with-unique-characters/

                We will address this problem with Recursion

                You are given an array of strings arr. A string s is formed by the concatenation of a subsequence of arr that has unique characters.
                Return the maximum possible length of s.
                A subsequence is an array that can be derived from another array by deleting some or no elements without changing the order of the remaining elements.


                Example 1:
                Input: arr = ["un","iq","ue"]
                Output: 4
                Explanation: All the valid concatenations are "","un","iq","ue","uniq" and "ique".
                Maximum length is 4.

                Example 2:
                Input: arr = ["cha","r","act","ers"]
                Output: 6
                Explanation: Possible solutions are "chaers" and "acters".
                
                Example 3:
                Input: arr = ["abcdefghijklmnopqrstuvwxyz"]
                Output: 26
                
                Constraints:

                1 <= arr.length <= 16
                1 <= arr[i].length <= 26
                arr[i] contains only lower case English letters.


        */
        static void Main(string[] args)
        {
            IList<string> arr = new List<string>() { "un", "iq", "ue" };
            int result = MaxLength(arr);

            System.Console.WriteLine($"The result {result}");

        }

        public static int MaxLength(IList<string> arr)
        {

            //start by creating an array object to hold our most current value
            //we create this so we can pass by reference to our string comparison function
            int[] result = new int[1]{0};
            
            //start recursive call
            MaximumCount(arr, 0, "", result, "");

            return result[0];
        }

        /////////////////////
        // Function that will be called recursively through the process of concatenating the strings
        // and checking lengths
        // The stack variable is used for output to help understand how the recursive calls are made.
        // That for each element of the array, the winding action will occur into the last element of the array.
        /////////////////////
        /*
        The call stack will look like this
                Current String: : Stack StackOne
                Current String: ue: Stack StackTwo
                Current String: iq: Stack StackOne
                Current String: ique: Stack StackTwo
                Current String: un: Stack StackOne
                Current String: unue: Stack StackTwo
                Current String: uniq: Stack StackOne
                Current String: unique: Stack StackTwo

        */
        public static void MaximumCount(IList<string> arr, int currentIndex , string currentString, int[] result, string stack)
        {

                //Our base case check
                //if we hit the end of the list, and the current length of the of the string is greater than what 
                //we have already put into our result array.
                //This base case check is when the recursive calls have wound all the way into the index of the array
                //and when it hits the count of the array, not the highest index of the array, but the count
                //we then know it is time to start winding out with our check.
                if(
                        arr.Count == currentIndex 
                        && 
                        CountCharacters(currentString, stack) > result[0]
                  )
                  {
                      result[0] = currentString.Length;
                      return;
                  }

                  //now just check to see if we have reached the end of the List if we make past the first check above.
                  if(arr.Count == currentIndex)
                  {
                      return;
                  }

                  //now stack our calls
                  //1. Recursively call all the way into the last index item of the list. With the current index + 1 and the current string
                  //2. Recursively call all the way into the list with concatenating the current string plus the next string index. With the current index + 1 and the current string plus the next string added to it.
                
                  //1. calls into the array all the way to the end, then reverses out looking for 
                  //   a string value that is the longest
                  MaximumCount(arr, currentIndex + 1, currentString, result, "StackOne");
                  //2. continue winding inward on the next call, this call will enter where the last call exited
                  MaximumCount(arr, currentIndex + 1, currentString + arr[currentIndex], result, "StackTwo");



        }

        ////////////////
        //will detect the characters in the current string to verify the uniqueness.
        ////////////////
        public static int CountCharacters(string current, string stack)
        {

            System.Console.WriteLine($"Current String: {current}: Stack {stack}");
            //create an array to hold counts related to characters in the alphabet
            int[] alphabet = new int[26];

            
            //iterate through our string characters, and see if the character has been seen before.
            foreach(char c in current.ToCharArray())            
            {
                //using our ascii character math, we can increment and check the position 
                //of the character in our array in this check. If the value is ever incremented once
                //then the value in the array index will be 1. this means that the character has
                //been seen before and we want to just exit returning -1
                if(alphabet[c -'a']++ > 0)
                {
                    return -1;
                }

            }
            //if we never run into duplicates then we just return the length of the current string.
            return current.Length;

        }



    }
}
