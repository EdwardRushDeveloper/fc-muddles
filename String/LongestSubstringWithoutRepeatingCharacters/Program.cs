namespace LongestSubstringWithoutRepeatingCharacters;

/*

3. Longest Substring Without Repeating Characters
Difficulty: Medium
Leet Code Problem

Given a string s, find the length of the longest substring without duplicate characters.

https://leetcode.com/problems/longest-substring-without-repeating-characters/description/

*/


/// <summary>
/// LeetCode 3. Finds the length of the longest substring that has no repeating characters.
/// </summary>
class Program
{
    /// <summary>
    /// Runs <see cref="LengthOfLongestSubstring"/> on a test string.
    /// Swap the commented lines to try the LeetCode examples.
    /// </summary>
    static void Main(string[] args)
    {

        /*

            Example 1:
            Input: s = "abcabcbb"
            Output: 3
            Explanation: The answer is "abc", with the length of 3.

            Example 2:
            Input: s = "bbbbb"
            Output: 1
            Explanation: The answer is "b", with the length of 1.

            Example 3:
            Input: s = "pwwkew"
            Output: 3
            Explanation: The answer is "wke", with the length of 3.
            Notice that the answer must be a substring, "pwke" is a subsequence and not a substring.

            Constraints:
            0 <= s.length <= 5 * 10^4
            s consists of English letters, digits, symbols and spaces.

        */
        Console.Clear();
        string input = "abcabcbb";
        //string input = "bbbbb";
        //string input = "pwwkew";

        int result = LengthOfLongestSubstring(input);

        Console.WriteLine($"Input: \"{input}\"  Output: {result}");
    }

    /// <summary>
    /// Returns the length of the longest substring of <paramref name="s"/>
    /// that contains no repeating characters.
    /// Uses a sliding window: the characters between <c>a_pointer</c> (inclusive) and
    /// <c>b_pointer</c> (exclusive), held in a <see cref="HashSet{T}"/>.
    /// Each loop either grows the window on the right by one character, or
    /// shrinks it on the left by one character.
    /// </summary>
    /// <param name="s">The string to search.</param>
    /// <returns>The length of the longest substring without repeating characters.</returns>
    /// <remarks>
    /// <para>
    /// The window always holds unique characters. When the next character (<c>s[b_pointer]</c>)
    /// is already in the window, the left side is moved forward, one character at a time,
    /// until the duplicate falls out. Then the right side can move again.
    /// </para>
    /// <para>
    /// Time O(N): each pointer only moves forward and moves at most N times, so the loop runs at most 2N times.
    /// Space O(min(N, K)), where K is the number of distinct characters the string can hold.
    /// </para>
    /// </remarks>
    protected static int LengthOfLongestSubstring(string s)
    {

        //pwwkew : result should be wke

        //a_pointer is the left side of the window, the first character in it
        //b_pointer is the right side, the next character to try to add
        //the window is s[a_pointer..b_pointer], so it starts out empty
        int a_pointer = 0; int b_pointer = 0;

        //the characters currently in the window, for O(1) "have I seen this?" checks
        //set.Count is always b_pointer - a_pointer, the window's length
        HashSet<char> set = new HashSet<char>();

        //the longest window seen so far
        int max = 0;

        //keep going until the left side reaches the end
        //once b_pointer hits the end, the else branch just empties the window
        while(a_pointer < s.Length)
        {
            //grow: if there is a next character and it is not already in the window,
            //set.Add puts it in and returns true
            //(the b_pointer check comes first, so s[b_pointer] is never read past the end)
            if(b_pointer < s.Length && set.Add(s[b_pointer]))
            {
               //the window now includes s[b_pointer], move the right side past it
               b_pointer++;
               //the window just got bigger, so check if it is the new longest
               max = Math.Max(max, set.Count);
            }
            //shrink: s[b_pointer] is a duplicate (or we are at the end of the string),
            //drop the leftmost character and try again on the next loop
            //this repeats until the copy of s[b_pointer] has been removed
            else
            {
                set.Remove(s[a_pointer]);
                a_pointer++;
            }

        }

        //max, not set.Count, because the window is empty by the time the loop ends
        return max;

    }

}
