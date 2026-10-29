// See https://aka.ms/new-console-template for more information


/*
https://youtu.be/GAUHXWjRawU?si=pGhYuCcWlPnQOzwZ
1647. Minimum Deletions to Make Character Frequencies Unique

Difficulty: Medium
Leet Code Problem

A string s is called good if there are no two different characters in s that have the same frequency.
Given a string s, return the minimum number of characters you need to delete to make s good.
The frequency of a character in a string is the number of times it appears in the string.
For example, in the string "aab", the frequency of 'a' is 2, while the frequency of 'b' is 1.

Example 1:
Input: s = "aab"
Output: 0
Explanation: s is already good.


Example 2:
Input: s = "aaabbbcc"
Output: 2
Explanation: You can delete two 'b's resulting in the good string "aaabcc".
Another way is to delete one 'b' and one 'c' resulting in the good string "aaabbc".

Example 3:
Input: s = "ceabaacb"
Output: 2
Explanation: You can delete both 'c's resulting in the good string "eabaab".
Note that we only care about characters that are still in the string at the end (i.e. frequency of 0 is ignored).

Constraints:
1 <= s.length <= 10^5
s contains only lowercase English letters.

https://leetcode.com/problems/minimum-deletions-to-make-character-frequencies-unique/

*/
using System.Collections;

/// <summary>
/// Solves LeetCode 1647. Finds the fewest characters to delete from a string so that
/// no two different characters appear the same number of times.
/// Both methods count the characters first, then make a greedy pass over the counts.
/// </summary>
public class FrequencyDeletions
{

    /// <summary>
    /// Runs both solutions against the same test string so their results can be compared.
    /// </summary>
    static void Main(string[] args)
    {

        //Console.WriteLine(FrequencyDeletions.MinDeletions("aaabbbcc"));
        //string d = "ceabaacb";
        //string d = "aaabbbcc";
        string d = "accdcdadddbaadbc";
        int minDeletions = MinDeletions(d);

        Console.WriteLine($"Minimum Deletions {minDeletions}");

        minDeletions = MinDeletionsPerformance(d);
        Console.WriteLine($"Minimum Deletions {minDeletions}");
    }

    /// <summary>
    /// Dictionary version. Counts each character, then lowers any count that is already
    /// taken until it reaches a free count or 0.
    /// </summary>
    /// <param name="s">The string to make good. Only lowercase English letters.</param>
    /// <returns>The minimum number of characters to delete.</returns>
    /// <remarks>Time O(N). Space O(1), because there are at most 26 distinct characters.</remarks>
    public static int MinDeletions(string s)
    {
        //count how many times each character appears, e.g. "aab" gives {a: 2, b: 1}
        var charFreq = new Dictionary<char, int>();
        foreach (char c in s)
            charFreq[c] = charFreq.GetValueOrDefault(c) + 1;

        //ret is the running total of deletions
        //freqSet holds the counts that another character already owns
        int ret = 0;
        var freqSet = new HashSet<int>();

        foreach (int f in charFreq.Values)
        {
            //copy the count so it can be lowered
            int freq = f;

            //while another character owns this count, delete one more of this character
            //stop at 0, because a character with a count of 0 is gone and can't clash
            while (freqSet.Contains(freq) && freq > 0)
            {
                freq -= 1;
                ret += 1;
            }

            //claim the free count. If freq is 0 this adds 0, which is harmless because
            //the while loop never lowers a count below 0
            freqSet.Add(freq);
        }
        return ret;
    }

    /// <summary>
    /// Faster version of <see cref="MinDeletions"/>. Uses a fixed array of 26 counts on the
    /// stack instead of a Dictionary, which avoids hashing and heap allocation for the counts.
    /// </summary>
    /// <param name="s">The string to make good. Only lowercase English letters.</param>
    /// <returns>The minimum number of characters to delete.</returns>
    /// <remarks>Time O(N). Space O(1): 26 counts and at most 26 entries in the HashSet.</remarks>
    public static int MinDeletionsPerformance(string s)
    {

        int returnValue = 0;
        //if only one item is in the string, return 0 immediately
        if (s.Length == 1) return 0;

        //now using ascii, for every character in the alphabet, we will do a count
        //use a span so that stack memory is used
        Span<int> count = stackalloc int[26];

        //for every letter in the string, we want to add a count 
        foreach (char l in s)
        {
            //26 character alphabet, minus the a value. We will count them
            count[l - 'a']++;
        }
        //holds the counts that another character already owns
        HashSet<int> set = new();

        //letters that never appear have a count of 0 and skip straight past the while loop
        foreach (var i in count)
        {
            //copy the count so it can be lowered
            int current = i;

            //while another character owns this count, delete one more of this character
            while (current > 0 && set.Contains(current))
            {
                current--;
                returnValue++;
            }
            //only claim counts above 0. A count of 0 means the letter was removed entirely
            if (current > 0)
                set.Add(current);
        }
        return returnValue;
    }
}
