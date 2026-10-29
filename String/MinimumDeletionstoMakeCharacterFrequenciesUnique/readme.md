# Minimum Deletions to Make Character Frequencies Unique

[1647. Minimum Deletions to Make Character Frequencies Unique](https://leetcode.com/problems/minimum-deletions-to-make-character-frequencies-unique/description/)

**Difficulty:** Medium

A string `s` is called **good** if there are no two different characters in `s` that have the same **frequency**.

Given a string `s`, return the **minimum** number of characters you need to delete to make `s` **good**.

The **frequency** of a character in a string is the number of times it appears in the string. For example, in the string `"aab"`, the frequency of `'a'` is `2`, while the frequency of `'b'` is `1`.

Example 1:
```
Input: s = "aab"
Output: 0
Explanation: s is already good.
```

Example 2:
```
Input: s = "aaabbbcc"
Output: 2
Explanation: You can delete two 'b's resulting in the good string "aaabcc".
Another way is to delete one 'b' and one 'c' resulting in the good string "aaabbc".
```

Example 3:
```
Input: s = "ceabaacb"
Output: 2
Explanation: You can delete both 'c's resulting in the good string "eabaab".
Note that we only care about characters that are still in the string at the end (i.e. frequency of 0 is ignored).
```

Constraints:
```
1 <= s.length <= 10^5
s contains only lowercase English letters.
```

## What the Problem Is Asking

The problem has two steps:

1. Count how many times **each character** appears.
2. Delete as few characters as possible so that **no two characters share the same count**.

The order of the characters in the string doesn't matter. Only the counts do.

## Walkthrough

Example 2: `s = "aaabbbcc"`

**Step 1: Count each character**

| Character | Count |
|---|---|
| a | **3** |
| b | **3** |
| c | **2** |

**Step 2: Find the clashes**

`a` and `b` both have a count of 3, so one of them has to lose a character.

**Step 3: Fix the clashes one at a time**

```
Delete one 'b'  →  a: 3, b: 2, c: 2    (now b and c clash)
Delete one 'c'  →  a: 3, b: 2, c: 1    (all different)
```

**Step 4: Count the deletions → 2**

## Common Points of Confusion

- **You return the number of deletions, not the new string.** For `"aaabbbcc"` the answer is `2`, not `"aaabbc"`.
- **A count of 0 is allowed more than once.** Deleting every copy of a character removes it from the string, so it no longer clashes with anything. In Example 3 both `'c'`s are deleted and `c` simply disappears.
- **Fixing one clash can cause another.** Dropping `b` from 3 to 2 made it clash with `c`. Each character has to keep dropping until it reaches a count nobody else is using.
- **There can be more than one correct set of deletions.** Example 2 lists two different ways. Both use 2 deletions, and only the number is returned.

## Approach: Count Frequencies, Then a Greedy Pass

1. Count each character with an `int[26]` array. `s[i] - 'a'` turns `'a'` into 0, `'b'` into 1, and so on.
2. Keep a HashSet of the counts that are **already taken**.
3. For each character's count:
   - While the count is above 0 **and** already taken, delete one (subtract 1 from the count, add 1 to the deletions).
   - If the count is still above 0, add it to the HashSet.
4. Return the number of deletions.

This is **greedy** because each character takes the closest free count below it and never looks back. Moving a count down by the smallest possible amount is always the cheapest fix.

### Approach Walkthrough

`s = "ceabaacb"`, counts: `a: 3, b: 2, c: 2, e: 1`

| Character | Starting count | Taken? | Count kept | Deletions | Taken counts after |
|---|---|---|---|---|---|
| a | 3 | no | 3 | 0 | {3} |
| b | 2 | no | 2 | 0 | {3, 2} |
| c | 2 | **yes**, drop to 1 | 1 | **1** | {3, 2, 1} |
| e | 1 | **yes**, drop to 0 | 0 (removed) | **2** | {3, 2, 1} |

**Result: 2**

## Complexity

| | Cost | Why |
|---|---|---|
| **Time** | O(N) | Counting visits each character once. The greedy pass only looks at 26 counts, and each one drops at most 25 times, so it counts as constant time |
| **Space** | O(1) | The count array is always 26 long, and the HashSet never holds more than 26 counts |

## Code

[Program.cs](Program.cs)
