# Valid Palindrome

[125. Valid Palindrome](https://leetcode.com/problems/valid-palindrome/description/)

**Difficulty:** Easy

A phrase is a **palindrome** if, after converting all uppercase letters into lowercase letters and removing all non-alphanumeric characters, it reads the same forward and backward. Alphanumeric characters include letters and numbers.

Given a string `s`, return `true` if it is a **palindrome**, or `false` otherwise.

Example 1:
```
Input: s = "A man, a plan, a canal: Panama"
Output: true
Explanation: "amanaplanacanalpanama" is a palindrome.
```

Example 2:
```
Input: s = "race a car"
Output: false
Explanation: "raceacar" is not a palindrome.
```

Example 3:
```
Input: s = " "
Output: true
Explanation: s is an empty string "" after removing non-alphanumeric characters.
Since an empty string reads the same forward and backward, it is a palindrome.
```

Constraints:
```
1 <= s.length <= 2 * 10^5
s consists only of printable ASCII characters.
```

## What the Problem Is Asking

The problem has two steps:

1. **Clean the string.** Keep only letters and digits, and make every letter lowercase.
2. **Check the cleaned string.** Return `true` if it reads the same from the front and from the back.

## Walkthrough

Example 2: `s = "race a car"`

**Step 1: Clean the string**

```
"race a car"  →  remove the spaces      →  "raceacar"
              →  make letters lowercase →  "raceacar"   (already lowercase)
```

**Step 2: Compare from both ends toward the middle**

```
r a c e a c a r
^             ^    r = r  ✓
  ^         ^      a = a  ✓
    ^     ^        c = c  ✓
      ^ ^          e ≠ a  ✗  → not a palindrome
```

**Step 3: A mismatch was found → `false`**

## Common Points of Confusion

- **Digits count.** `"P0"` cleans to `"p0"`. `p` and `0` are different, so the answer is `false`. Digits are kept, not removed.
- **Case doesn't matter.** `'A'` and `'a'` are treated as the same character.
- **Spaces and punctuation are ignored completely.** They don't break the palindrome. They aren't part of it at all.
- **A string with no letters or digits is a palindrome.** `" "` and `",."` both clean to an empty string, which reads the same in both directions, so the answer is `true`.

## Approach: Clean, Then Two Pointers

1. Walk through `s` and copy each letter or digit, lowercased, into a new array.
2. Put a `left` pointer on the first kept character and a `right` pointer on the last one.
3. While `left` is before `right`:
   - If the two characters differ, return `false`.
   - Otherwise move `left` one step right and `right` one step left.
4. If the pointers meet without a mismatch, return `true`.

The loop stops when the pointers meet or cross. In an odd-length string the middle character is compared with nothing, which is fine because it always matches itself.

### Approach Walkthrough

`s = "A man, a plan, a canal: Panama"` cleans to `amanaplanacanalpanama` (21 characters, index 0 to 20).

| left | right | Characters | Match? |
|---|---|---|---|
| 0 | 20 | a, a | ✓ |
| 1 | 19 | m, m | ✓ |
| 2 | 18 | a, a | ✓ |
| ... | ... | ... | ✓ |
| 9 | 11 | a, a | ✓ |
| 10 | 10 | pointers meet, stop | |

**Result: true**

## Complexity

| | Cost | Why |
|---|---|---|
| **Time** | O(N) | One pass to clean the string, then at most N / 2 comparisons |
| **Space** | O(N) | The cleaned copy of the string can be as long as the original |

### Doing It Without the Copy

The pointers can also work directly on the original string. Each pointer skips over anything that isn't a letter or digit, and the two characters are lowercased only when they're compared. This gives the same O(N) time with O(1) space, because no copy is made.

## Code

[Program.cs](Program.cs)
