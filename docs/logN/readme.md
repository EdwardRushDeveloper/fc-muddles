# O(log N)

**O(log N) means: every step cuts the problem in half.**

Ask "how many times can I halve N before only 1 is left?" The answer is log N.

## The Guessing Game

I'm thinking of a number between 1 and 100. After each guess I tell you "higher" or "lower".

The smart move is to always guess the middle:

```
1 - 100   guess 50  → "higher"
51 - 100  guess 75  → "lower"
51 - 74   guess 62  → "higher"
63 - 74   guess 68  → "lower"
63 - 67   guess 65  → "higher"
66 - 67   guess 66  → "correct!"
```

Each guess throws away half of what is left, so you will **never need more than 7 guesses** for 100 numbers.

## Why It Is So Fast

Doubling the input only adds **one** more step:

| N (items) | Steps (log N) |
|---|---|
| 8 | 3 |
| 100 | 7 |
| 1,000 | 10 |
| 1,000,000 | 20 |
| 1,000,000,000 | 30 |

A billion items takes about 30 steps. Checking them one by one, O(N), could take a billion.

## What "log" Means

Here, log is base 2. It is the opposite of doubling:

```
2 × 2 × 2 = 8     so   log 8 = 3
```

"How many times do I multiply 2 to reach N?" is the same question as "how many times do I halve N to reach 1?"

## Where You See It

- **Binary search** on a sorted array
- **Balanced binary search trees**: each level down skips half the tree
- **Heaps**: insert and remove move up or down one level at a time

## Quick Way to Spot It

If a loop **halves** (or doubles) something each time, it is O(log N):

```csharp
while (n > 1)
{
    n = n / 2;   // cut in half every step
}
```
