# Largest Positive Integer That Exists With Its Negative

[2441. Largest Positive Integer That Exists With Its Negative](https://leetcode.com/problems/largest-positive-integer-that-exists-with-its-negative/description/)

**Difficulty:** Easy

Given an integer array nums that does not contain any zeros, find the largest positive
integer k such that -k also exists in the array.

Return the positive integer k. If there is no such integer, return -1.

Example 1:
```
Input: nums = [-1,2,-3,3]
Output: 3
Explanation: 3 is the only valid k we can find in the array.
```

Example 2:
```
Input: nums = [-1,10,6,7,-7,1]
Output: 7
Explanation: Both 1 and 7 have their corresponding negative values in the array. 7 has a larger value.
```

Example 3:
```
Input: nums = [-10,8,6,7,-2,-3]
Output: -1
Explanation: There is no a single valid k, we return -1.
```

Constraints:
```
1 <= nums.length <= 1000
-1000 <= nums[i] <= 1000
nums[i] != 0
```

## What the Problem Is Asking

Find a number that has its "mirror" in the array: `7` and `-7`, or `1` and `-1`. If more than one pair exists, return the biggest positive one. If there are no pairs, return `-1`.

## Approach: HashSet, Single Pass

Walk through the array once. For each number, ask: **"Have I already seen its opposite?"**

- If **no**, remember the number by adding it to a `HashSet`.
- If **yes**, a pair exists. Keep the larger of the pair's positive value and the best answer so far.

The order of the pair doesn't matter. If `-10` comes first, it is stored, and when `10` arrives its opposite is found.

```csharp
public static int FindMaxK(int[] nums)
{
    HashSet<int> seen = new HashSet<int>();
    int largest = -1;

    foreach (int i in nums)
    {
        if (seen.Contains(i * -1))
        {
            largest = Math.Max(largest, Math.Abs(i));
        }
        seen.Add(i);
    }

    return largest;
}
```

`largest` starts at `-1`, so if no pair is ever found, `-1` is returned without any extra check.

## Walkthrough

Example 2: `nums = [-1, 10, 6, 7, -7, 1]`

| num | Opposite already seen? | largest | seen after |
|---|---|---|---|
| -1 | no (1) | -1 | {-1} |
| 10 | no (-10) | -1 | {-1, 10} |
| 6 | no (-6) | -1 | {-1, 10, 6} |
| 7 | no (-7) | -1 | {-1, 10, 6, 7} |
| -7 | **yes (7)** | **7** | {-1, 10, 6, 7, -7} |
| 1 | **yes (-1)** | 7 (1 is smaller) | {-1, 10, 6, 7, -7, 1} |

**Output: 7**

## Complexity

| | Cost | Why |
|---|---|---|
| **Time** | O(N) | Every number is visited once, and HashSet `Contains`/`Add` are O(1) on average |
| **Space** | O(N) | In the worst case every number is stored in the HashSet |

## Where Log(N) Fits In

Log(N) means each step cuts the work in half (see [O(log N)](../../docs/logN/readme.md)). This solution never halves anything, so there is **no Log(N) in it**. Log(N) shows up in the other ways to solve the problem:

| Approach | Time | Space | Where the Log(N) comes from |
|---|---|---|---|
| **HashSet** (this solution) | O(N) | O(N) | None. Each lookup is O(1) |
| Sort + Two Pointers | O(N Log N) | O(1) | Sorting splits the array in half about Log N times, doing N work at each level |
| Sort + Binary Search | O(N Log N) | O(1) | Each binary search halves the range: O(Log N), done N times |
| SortedSet instead of HashSet | O(N Log N) | O(N) | A SortedSet is a balanced tree, so each lookup is O(Log N) |

For N = 1000 (the max in the constraints):

```
O(N)        ~  1,000 steps
O(N Log N)  ~ 10,000 steps   (Log 1000 is about 10)
```

The HashSet wins because an O(1) lookup beats an O(Log N) lookup. Sort + Two Pointers is the choice only when memory is tight, since it needs no extra space.

## Code

[Program.cs](Program.cs)
