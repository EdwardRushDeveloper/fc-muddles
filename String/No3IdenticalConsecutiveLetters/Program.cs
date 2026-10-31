using System.Text;

namespace No3IdenticalConsecutiveLetters;

/*

1957. Delete Characters to Make Fancy String
Difficulty: Easy
Leet Code Problem

A fancy string is a string where no three consecutive characters are equal.
Given a string s, delete the minimum possible number of characters from s to make it fancy.
Return the final string after the deletion. It can be shown that the answer will always be unique.


https://leetcode.com/problems/delete-characters-to-make-fancy-string/description/

*/


/// <summary>
/// Solves LeetCode 1957. Deletes the fewest characters from a string so that
/// no three consecutive characters are the same.
/// </summary>
class Program
{
    /// <summary>
    /// Runs <see cref="RemoveCharacters"/> on a test string.
    /// Swap the commented lines to try the LeetCode examples.
    /// </summary>
    static void Main(string[] args)
    {

        /*

            Example 1:
            Input: s = "leeetcode"
            Output: "leetcode"
            Explanation:
            Remove an 'e' from the first group of 'e's to create "leetcode".
            No three consecutive characters are equal, so return "leetcode".

            Example 2:
            Input: s = "aaabaaaa"
            Output: "aabaa"
            Explanation:
            Remove an 'a' from the first group of 'a's to create "aabaaaa".
            Remove two 'a's from the second group of 'a's to create "aabaa".
            No three consecutive characters are equal, so return "aabaa".

            Example 3:
            Input: s = "aab"
            Output: "aab"
            Explanation: No three consecutive characters are equal, so return "aab".

        */

        //string input = "aaabaaaa";
        string input = "leeetcode";
        string result = RemoveCharacters(input);


    }

    /// <summary>
    /// Walks the string once and counts how many times the current character has appeared in a row.
    /// Each character is copied into a <see cref="StringBuilder"/> until its run reaches 2;
    /// any further repeats in that run are skipped.
    /// </summary>
    /// <param name="input">The string to make fancy.</param>
    /// <returns>
    /// <paramref name="input"/> with the fewest characters removed so that
    /// no three consecutive characters are equal.
    /// </returns>
    /// <remarks>
    /// Time O(N). Space O(N) for the <see cref="StringBuilder"/>.
    /// 12 ms execution, not as good as <see cref="MakeFancyString"/> below from LeetCode.
    /// </remarks>
    protected static string RemoveCharacters(string input)
    {
        /*
            We need this to look like this aaabaaaa,  aabaa
        */

        //sized for the worst case, where no characters are removed
        StringBuilder sb = new StringBuilder(input.Length);

        //counter is how many times tracker has been appended in the current run
        //tracker is the character of the current run
        int counter = 1;
        char tracker = default;


        for (int i = 0; i < input.Length; i++)
        {
            char c = input[i];

            //first iteration initialize
            //the first character always starts a run of 1, so it is always kept
            if (tracker == default)
            {
                tracker = c;
                sb.Append(c);
                continue;
            }

            //if we have reached our max for the current character tracker
            //just throw the index away and increment
            if (tracker == c && counter >= 2)
            {
                counter += 1;
                continue;
            }

            //a different character starts a new run
            //counter is reset to 0 because it is incremented right after the append below
            if (tracker != c)
            {
                tracker = c;
                counter = 0;
            }

            //keep the character and count it in the current run
            sb.Append(c);
            counter++;

        }

        return sb.ToString();
    }

    /// <summary>
    /// Leet Code high efficiency and robust example 7ms execution.
    /// Keeps the last two kept characters in <c>prev2</c> and <c>prev1</c> and writes each
    /// character into a stack buffer unless it matches both of them.
    /// </summary>
    /// <param name="s">The string to make fancy.</param>
    /// <returns>
    /// <paramref name="s"/> with the fewest characters removed so that
    /// no three consecutive characters are equal. Returns <paramref name="s"/> itself
    /// when nothing had to be removed.
    /// </returns>
    /// <remarks>
    /// Time O(N). Space O(N) for the buffer, which is on the stack instead of the heap,
    /// so no extra allocation is made unless characters are removed.
    /// </remarks>
    protected static string MakeFancyString(string s)
    {
        int length = s.Length;

        //a string of 0, 1 or 2 characters can't have three in a row
        if (length <= 2) { return s; }

        //span gives fast, bounds-checked access to s without copying it
        ReadOnlySpan<char> span = s;
        Span<char> buf = stackalloc char[length];  // s in test suite is small enough for stack

        //the first two characters are always kept
        //prev2 is the second to last kept character, prev1 is the last kept character
        char prev2 = buf[0] = s[0];
        char prev1 = buf[1] = s[1];

        //written is how many characters are in buf so far
        int written = 2;

        for (int i = written; i < length; i++)
        {
            char ch = span[i];

            //x ^ y is 0 only when x == y, so this is non-zero unless ch equals both prev2 and prev1
            //in other words, keep ch unless it would make three in a row
            if (((prev2 ^ ch) | (prev1 ^ ch)) != 0)
            {
                buf[written++] = ch;
                prev2 = prev1;
                prev1 = ch;
            }
        }

        //if nothing was removed, return the original string instead of allocating a new one
        return written == length ? s : new string(buf[..written]);
    }

}
