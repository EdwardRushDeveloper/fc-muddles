// See https://aka.ms/new-console-template for more information

/*

125. Valid Palindrome
Difficulty: Easy
Leet Code Problem

A phrase is a palindrome if, after converting all uppercase letters into lowercase letters and removing all non-alphanumeric characters, it reads the same forward and backward. Alphanumeric characters include letters and numbers.
Given a string s, return true if it is a palindrome, or false otherwise.


https://leetcode.com/problems/valid-palindrome/




*/


/// <summary>
/// Solves LeetCode 125. Checks whether a string reads the same forward and backward
/// once it is lowercased and everything except letters and digits is removed.
/// </summary>
public class Program
{
    /// <summary>
    /// Runs <see cref="IsPalindrome"/> on a test string and prints the result.
    /// Swap the commented lines to try the LeetCode examples.
    /// </summary>
    static void Main(string[] args)
    {
        Console.Clear();
        //string p = "A man, a plan, a canal: Panama";
        //string p = "race a car";
        string p = "P0";
        bool isp = IsPalindrome(p);
        Console.WriteLine(isp);


    }
    /// <summary>
    /// Copies only the letters and digits of <paramref name="s"/>, lowercased, into a new array,
    /// then compares that array from both ends toward the middle.
    /// </summary>
    /// <param name="s">The phrase to check. Printable ASCII characters.</param>
    /// <returns>
    /// true if the cleaned phrase reads the same forward and backward, otherwise false.
    /// A phrase with no letters or digits returns true.
    /// </returns>
    /// <remarks>Time O(N). Space O(N) for the cleaned copy.</remarks>
    public static bool IsPalindrome(string s)
    {
        //assume it is a palindrome until a mismatch is found
        bool returnValue = true;

        //addLocation is the index of the last character copied into ns
        //it starts at -1 because nothing has been copied yet
        int addLocation = -1;
        //ns is sized for the worst case, where every character is kept
        string[] ns = new string[s.Length];
        Console.WriteLine(ns.Length);

        //step 1: copy only letters and digits, lowercased, into ns
        for(int i = 0; i < s.Length; i++)
        {
            char c = s[i];
            //keep a-z, A-Z and 0-9. Spaces and punctuation are skipped
            if( (c>= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9'))
            {
                //Console.WriteLine(c);
                addLocation++;
                ns[addLocation] = c.ToString().ToLower();
            }
        }

        //step 2: two pointers, left starts at the first kept character and right at the last
        //if nothing was kept (addLocation is -1), right is 0, the loop never runs, and the result is true
        int left = 0;
        int right = addLocation == -1 ? 0 : addLocation;
        //stop when the pointers meet or cross. A middle character always matches itself
        while(left < right)
        {
            Console.WriteLine($"{ns[left]} - {ns[right]}");
            //one mismatch is enough to know it is not a palindrome
            if(ns[left] != ns[right]) {returnValue = false; break;}
            //move both pointers one step toward the middle
            left++;
            right--;
        }
        return returnValue;
    }
}


