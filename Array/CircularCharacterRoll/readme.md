# Circular Character Roll

[GeeksforGeeks: Roll Characters of String](https://www.geeksforgeeks.org/problems/roll-the-characters-of-a-string2127/1)

**Source:** HackerRank, as a coding assessment question. It is also known as "Roll the String".

**Difficulty:** Medium

You are given a string `s` made of **lowercase** English letters and an integer array `roll`. For each value `roll[i]`, roll the **first `roll[i]` characters** of `s` forward by one letter in the alphabet.

The alphabet is **circular**, so rolling `'z'` gives `'a'`.

The rolls are applied one after another, and each roll works on the string produced by the previous rolls. Return the final string after every roll has been applied.

Example 1:
```
Input: s = "abz", roll = [3,2,1]
Output: "dda"
Explanation:
- roll 3: "abz" -> "bca"
- roll 2: "bca" -> "cda"
- roll 1: "cda" -> "dda"
```

Example 2:
```
Input: s = "bca", roll = [1,2,3]
Output: "eeb"
Explanation:
- roll 1: "bca" -> "cca"
- roll 2: "cca" -> "dda"
- roll 3: "dda" -> "eeb"
```

Constraints:
```
1 <= s.length <= 10^5
1 <= roll.length <= 10^5
1 <= roll[i] <= s.length
s consists of lowercase English letters only.
```

## What the Problem Is Asking

- A roll of `3` changes only the **first 3 characters**. Everything after them stays the same.
- Rolling a character moves it **one letter forward**: `a -> b`, `b -> c`, ... `y -> z`, and then `z -> a`.
- The rolls **add up**. A character rolled 3 times moves 3 letters forward.

## Common Points of Confusion

- **Wrap-around:** `'z'` rolled once becomes `'a'`, not a character past `'z'`. A character can be rolled more than 26 times, and it keeps going around the alphabet.
- **The roll value is a length, not an index.** `roll[i] = 2` means characters at index `0` and `1`.
- **Order doesn't change the final answer.** Each roll adds +1 to a prefix, so applying `[3, 2, 1]` or `[1, 2, 3]` to the same string gives the same result.

## Approach: Difference Array

The walkthrough below uses `s = "zcza"` and `roll = [1, 1, 3, 4]`. It does the problem the slow way first, then shows where the difference array comes from, then traces the code line by line.

### Step 1: The Slow Way, One Roll at a Time

Change the actual string after each roll:

```
start:      z  c  z  a

roll 1:     a  c  z  a      ← first 1 letter moves:  z→a
roll 1:     b  c  z  a      ← first 1 letter moves:  a→b
roll 3:     c  d  a  a      ← first 3 letters move:  b→c, c→d, z→a
roll 4:     d  e  b  b      ← all 4 letters move:    c→d, d→e, a→b, a→b
```

**Answer: "debb"**

This works, but every roll touches every letter it covers. With up to 10^5 letters and 10^5 rolls, that's up to 10,000,000,000 steps.

### Step 2: How Far Did Each Letter Move in Total?

```
index:      0   1   2   3
letter:     z   c   z   a
moved:      4   2   2   1
```

- Index 0 was in every roll (1, 1, 3, 4) → moved **4**
- Index 1 was only in the rolls of 3 and 4 → moved **2**
- Index 2 was only in the rolls of 3 and 4 → moved **2**
- Index 3 was only in the roll of 4 → moved **1**

If we know the list **`[4, 2, 2, 1]`**, the problem is basically solved: move each letter that many places along the alphabet.

The question is how to get `[4, 2, 2, 1]` **quickly**.

### Step 3: Where the Name "Difference" Comes From

Take `[4, 2, 2, 1]` and write down **how much each number changes from the one before it**:

```
shift:          4      2      2      1
change:        +4     -2      0     -1
               ↑      ↑      ↑      ↑
             (0→4)  (4→2)  (2→2)  (2→1)
```

That bottom row, **`[+4, -2, 0, -1]`**, is the **difference array**. It holds the differences between neighbours.

You can always go back the other way by adding up as you go:

```
change:        +4     -2      0     -1
running total:  4      2      2      1     ← the shifts again
```

So the shifts and the difference array carry the same information, written two ways.

### Step 4: Why Build the Difference Array Instead of the Shifts?

Because **one roll changes only two numbers in the difference array**, however long the roll is.

Take a roll of 3 on a 4-letter string. It adds 1 to the shifts at indexes 0, 1 and 2:

```
shift change from this roll:      1   1   1   0
neighbour differences:           +1   0   0  -1
```

Inside the roll, neighbours both went up by 1, so the gap between them didn't change (**0**). The difference only changes at:
- **the start** of the roll, where it goes up: **+1 at index 0**
- **just past the end**, where it drops back: **-1 at index 3**

That's why the code only touches `diff[0]` and `diff[r]`. A roll of 3 or a roll of 100,000 costs the same: two updates.

### Picture It: Each Roll Is a Brick

A roll of `r` is a brick that covers the first `r` letters. Stack all the bricks for `zcza` / `[1, 1, 3, 4]`:

```
roll 1:   ██
roll 1:   ██
roll 3:   ██   ██   ██
roll 4:   ██   ██   ██   ██
         ─────────────────────
           z    c    z    a
height:    4    2    2    1
```

**The height of the stack above each letter is how far that letter moves.** `z` has 4 bricks on it, so it moves 4. `a` has 1, so it moves 1.

#### Walk Across the Top of the Wall, Left to Right

Start on the ground (height 0) and write down each step up or down as you move from letter to letter:

```
ground → z :  climb up 4      (+4)   all 4 bricks start here
z → c      :  drop down 2     (-2)   both "roll 1" bricks ended
c → z      :  stay level      ( 0)   no brick ends here
z → a      :  drop down 1     (-1)   the "roll 3" brick ended
```

Those steps, **`[+4, -2, 0, -1]`**, are exactly the `diff` array.

**`diff` doesn't store the heights. It stores the climbs and drops.**

#### Why the Running Total Gives Each Height

If you start at 0 and add up every climb and drop as you walk, you always know your current height:

```
start at 0
+4  → height 4    (z)
-2  → height 2    (c)
 0  → height 2    (z)
-1  → height 1    (a)
```

That's `currentShift += diff[i]`. It's the same as a bank balance: if you add up every deposit and withdrawal in order, you get the balance at each point.

#### Why Each Roll Is `+1` at 0 and `-1` at r

Look at a **single brick**, say roll 3:

```
          ██   ██   ██
           z    c    z    a
index:     0    1    2    3
```

Walking across it, you climb **1** when you step onto it at index 0, and drop **1** when you step off at index 3. Nothing happens in between, because the top is flat. That's all the code writes:

```csharp
diff[0] += 1;   // step onto this brick
diff[r] -= 1;   // step off it at index r
```

When bricks are stacked, their climbs and drops just add together. Four bricks start at index 0, so `diff[0]` becomes `+4`. Two bricks end at index 1, so `diff[1]` becomes `-2`.

#### The Whole Idea

1. **The shift for each letter** is the height of the brick stack above it.
2. **`diff`** is a list of the climbs and drops as you walk across the top.
3. **Each brick** adds exactly one climb (`+1` at the start) and one drop (`-1` where it ends). That's why the code is so short.
4. **Adding up the climbs and drops** as you walk gives the height at each letter. That's `currentShift`.

It's fast because adding a brick only means writing down where it starts and ends, however long it is. You never have to visit every letter it covers.

### Step 5: Building `diff` in the Code

```csharp
int n = s.Length;              // n = 4
int[] diff = new int[n + 1];   // [0, 0, 0, 0, 0]

foreach (int r in roll)
{
    diff[0] += 1;              // this roll starts at index 0
    if (r < n)
    {
        diff[r] -= 1;          // and stops before index r
    }
}
```

The extra slot at the end is room for a "-1 just past the end" marker. The `if (r < n)` check skips that marker when the roll covers the whole string, which is also fine.

Tracing each roll:

**r = 1**
```
diff[0] += 1        →  [1,  0, 0, 0, 0]
1 < 4, diff[1] -= 1 →  [1, -1, 0, 0, 0]
```
Meaning: one roll starts at index 0 and stops before index 1.

**r = 1**
```
diff[0] += 1        →  [2, -1, 0, 0, 0]
1 < 4, diff[1] -= 1 →  [2, -2, 0, 0, 0]
```
Meaning: two rolls start at index 0, and both stop before index 1.

**r = 3**
```
diff[0] += 1        →  [3, -2, 0,  0, 0]
3 < 4, diff[3] -= 1 →  [3, -2, 0, -1, 0]
```
Meaning: a third roll starts at index 0 and stops before index 3.

**r = 4**
```
diff[0] += 1        →  [4, -2, 0, -1, 0]
4 < 4 is false      →  nothing to undo; this roll runs to the end
```

**Final `diff` = `[4, -2, 0, -1]`**. That's the difference array from Step 3.

### Step 6: Running Total and New Letters

```csharp
int currentShift = 0;
for (int i = 0; i < n; i++)
{
    currentShift += diff[i];
    int originalPos = s[i] - 'a';                     // a=0, b=1, ... z=25
    int newPos = (originalPos + currentShift) % 26;   // % 26 wraps z back to a
    result.Append((char)('a' + newPos));
}
```

**i = 0**, letter `z`
```
currentShift = 0 + 4      = 4
originalPos  = 'z' - 'a'  = 25
newPos       = (25 + 4) % 26 = 29 % 26 = 3
'a' + 3      = 'd'
```

**i = 1**, letter `c`
```
currentShift = 4 + (-2)   = 2
originalPos  = 'c' - 'a'  = 2
newPos       = (2 + 2) % 26 = 4
'a' + 4      = 'e'
```

**i = 2**, letter `z`
```
currentShift = 2 + 0      = 2
originalPos  = 'z' - 'a'  = 25
newPos       = (25 + 2) % 26 = 27 % 26 = 1
'a' + 1      = 'b'
```

**i = 3**, letter `a`
```
currentShift = 2 + (-1)   = 1
originalPos  = 'a' - 'a'  = 0
newPos       = (0 + 1) % 26 = 1
'a' + 1      = 'b'
```

**Result: "debb"**, the same as the slow way in Step 1.

### Summary

| | Slow way (Step 1) | Difference array (Steps 5–6) |
|---|---|---|
| What each roll does | changes every letter it covers | changes 2 numbers in `diff` |
| Turning that into an answer | already done | one pass with a running total |
| Work for 100,000 letters and 100,000 rolls | up to 10,000,000,000 steps | about 200,000 steps |

1. The answer only depends on **how far each letter moves**: `[4, 2, 2, 1]`.
2. Neighbouring letters move the same amount, **except where a roll starts or stops**.
3. So we only record **where the amount changes** (the difference array), which is cheap.
4. A running total turns those changes back into the amount each letter moves.

## Complexity

| | Cost | Why |
|---|---|---|
| **Time** | O(N + M) | N = length of `s`, M = length of `roll`. Each roll is two O(1) updates, then one pass over the string |
| **Space** | O(N) | The `diff` array and the `StringBuilder` for the result |

## Code

[Program.cs](Program.cs)
