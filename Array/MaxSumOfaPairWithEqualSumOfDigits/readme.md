# Max Sum of a Pair With Equal Sum of Digits

[2342. Max Sum of a Pair With Equal Sum of Digits](https://leetcode.com/problems/max-sum-of-a-pair-with-equal-sum-of-digits/description/)

**Difficulty:** Medium

You are given a **0-indexed** array `nums` consisting of **positive** integers. You can choose two indices `i` and `j`, such that `i != j`, and the sum of digits of the number `nums[i]` is equal to that of `nums[j]`.

Return the **maximum** value of `nums[i] + nums[j]` that you can obtain over all possible indices `i` and `j` that satisfy the conditions. If no such pair of indices exists, return `-1`.

Example 1:
```
Input: nums = [18,43,36,13,7]
Output: 54
Explanation: The pairs (i, j) that satisfy the conditions are:
- (0, 2), both numbers have a sum of digits equal to 9, and their sum is 18 + 36 = 54.
- (1, 4), both numbers have a sum of digits equal to 7, and their sum is 43 + 7 = 50.
So the maximum sum that we can obtain is 54.
```

Example 2:
```
Input: nums = [10,12,19,14]
Output: -1
Explanation: There are no two numbers that satisfy the conditions, so we return -1.
```

Constraints:
```
1 <= nums.length <= 10^5
1 <= nums[i] <= 10^9
```

## What the Problem Is Asking

The problem has two steps:

1. Pair up numbers whose **digits add up to the same total**.
2. Out of those pairs, return the one whose **numbers themselves add up to the most**.

## Walkthrough

Example 1: `nums = [18, 43, 36, 13, 7]`

**Step 1: Add up the digits of each number**

| Number | Its digits added | Digit sum |
|---|---|---|
| 18 | 1 + 8 | **9** |
| 43 | 4 + 3 | **7** |
| 36 | 3 + 6 | **9** |
| 13 | 1 + 3 | **4** |
| 7 | 7 | **7** |

**Step 2: Group the numbers by digit sum**

```
Digit sum 9  →  18, 36
Digit sum 7  →  43, 7
Digit sum 4  →  13        (alone, nothing to pair with)
```

You're only allowed to pair numbers **from the same group**. 13 is the only number with a digit sum of 4, so it can't be used.

**Step 3: Add the actual numbers in each pair**

```
Group 9:  18 + 36 = 54
Group 7:  43 + 7  = 50
```

**Step 4: Take the biggest total → 54**

## Common Points of Confusion

- **The digit sum only decides who can pair up.** The answer adds the original numbers: 18 + 36 = 54, not 9 + 9.
- **The answer can be -1.** In Example 2, `[10, 12, 19, 14]` has digit sums 1, 3, 10 and 5. They're all different, so no pair exists and the answer is `-1`.
- **A group can have more than two numbers.** If it does, pick the **two largest** in that group. For `[18, 36, 27, 9]`, every digit sum is 9, so the answer is 36 + 27 = 63.

## Approach: Dictionary, Single Pass

Use a Dictionary that maps each **digit sum → the largest number seen with that digit sum**, plus a `best` variable that holds the largest pair total so far.

For each number:
1. Work out its digit sum.
2. If that digit sum is already in the Dictionary, add this number to the stored number and see whether the total beats `best`.
3. Keep the **larger** of the two numbers in the Dictionary for that digit sum.

At the end, return `best`. It starts at `-1`, so if no pair is ever found, `-1` is returned.

```csharp
if (largestByDigitSum.TryGetValue(digitSum, out int stored))
{
    best = Math.Max(best, num + stored);                  // is this the best pair so far?
    largestByDigitSum[digitSum] = Math.Max(stored, num);  // keep the bigger number
}
else
{
    largestByDigitSum[digitSum] = num;
}
```

### Getting the Digit Sum

```csharp
int temp = num;
int digitSum = 0;
while (temp > 0)
{
    digitSum += temp % 10;   // add the last digit
    temp /= 10;              // remove the last digit
}
```

- `temp % 10` gives the remainder after dividing by 10, which is always the last digit: `43 % 10 = 3`.
- `temp /= 10` drops the last digit, because integer division throws away the decimal: `43 / 10 = 4`.
- The loop stops when `temp` reaches 0, meaning every digit has been added.
- `temp` is a copy, so the original number is kept for the pair total.

### Approach Walkthrough

`nums = [18, 43, 36, 13, 7]`

| Number | Digit sum | Already in Dictionary? | Pair total | best | Dictionary after |
|---|---|---|---|---|---|
| 18 | 9 | no | | -1 | {9: 18} |
| 43 | 7 | no | | -1 | {9: 18, 7: 43} |
| 36 | 9 | **yes, 18** | 36 + 18 = **54** | **54** | {9: **36**, 7: 43} |
| 13 | 4 | no | | 54 | {9: 36, 7: 43, 4: 13} |
| 7 | 7 | **yes, 43** | 7 + 43 = 50 | 54 (50 is smaller) | {9: 36, 7: **43**, 4: 13} |

**Result: 54**

### Why Keep Only the Largest Number?

The best pair in a group is always its two largest numbers. When a new number arrives, pairing it with the largest one already seen is the best it can do, so smaller numbers never need to be stored.

## Complexity

| | Cost | Why |
|---|---|---|
| **Time** | O(N) | Each number is visited once. The digit loop runs at most 10 times (numbers are at most 10^9), so it counts as constant time |
| **Space** | O(1) in practice | The largest possible digit sum is 81 (999,999,999), so the Dictionary never holds more than 82 entries. An `int[82]` array could replace it |

Sorting the array first also works, but costs O(N Log N) (see [O(log N)](../../docs/logN/readme.md)), so the single pass is faster.

## Code

[Program.cs](Program.cs)
