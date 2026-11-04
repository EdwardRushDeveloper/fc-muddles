# Coin Change

[322. Coin Change](https://leetcode.com/problems/coin-change/description/)

**Difficulty:** Medium

You are given an integer array `coins` representing coins of different denominations and an integer `amount` representing a total amount of money.

Return the fewest number of coins that you need to make up that amount. If that amount of money cannot be made up by any combination of the coins, return `-1`.

You may assume that you have an infinite number of each kind of coin.

Example 1:
```
Input: coins = [1,2,5], amount = 11
Output: 3
Explanation: 11 = 5 + 5 + 1
```

Example 2:
```
Input: coins = [2], amount = 3
Output: -1
```

Example 3:
```
Input: coins = [1], amount = 0
Output: 0
```

Constraints:
```
1 <= coins.length <= 12
1 <= coins[i] <= 2^31 - 1
0 <= amount <= 10^4
```

## What the Problem Is Asking

- **Count the coins, not the combinations.** The answer is a single number: the fewest coins that add up to `amount`. You don't have to say which coins they are.
- **Coins can be reused.** Every coin value can be used as many times as you like.
- **Some amounts can't be made.** With only `2` coins, any odd amount is impossible, so the answer is `-1`.
- **An amount of 0 needs 0 coins.**

## Why Picking the Biggest Coin First Doesn't Work

The obvious idea is "greedy": keep taking the biggest coin that fits. That happens to work for `[1,2,5]`, but it fails for other coin sets.

`coins = [1,3,4], amount = 6`

```
Greedy:   4 + 1 + 1  = 3 coins
Best:     3 + 3      = 2 coins
```

Taking the `4` first looked like the best move, but it left `2`, which can only be made with two `1`s. To be sure of the best answer, every coin has to be tried at every step. Trying every combination directly would take far too long, which is why this problem uses **dynamic programming**.

## The Key Idea: Build Up From Smaller Amounts

Suppose you already know the fewest coins for every amount smaller than `11`. To make `11`, the **last coin** you add has to be one of `1`, `2` or `5`:

```
last coin is 1  →  fewest coins for 10, plus this 1 coin
last coin is 2  →  fewest coins for  9, plus this 1 coin
last coin is 5  →  fewest coins for  6, plus this 1 coin
```

The answer for `11` is the smallest of those three. That gives the rule for **any** amount `i`:

```
fewest(i) = smallest of ( fewest(i - coin) + 1 )  for every coin that fits (coin <= i)
```

Each answer depends only on smaller amounts. So if you work out the amounts in order, `0, 1, 2, ...`, everything you need is already known by the time you reach it. This is called a **bottom-up** solution.

## Approach: Bottom-Up Dynamic Programming

### Step 1: Create the `dp` array

```csharp
int[] dp = new int[amount + 1];
```

`dp[i]` will hold the fewest coins needed to make amount `i`. The array has `amount + 1` slots so that index `amount` exists. Index `0` stands for 0 cents, index `1` for 1 cent, and so on.

### Step 2: Fill it with a "not possible yet" value

```csharp
System.Array.Fill(dp, amount + 2);
```

The most coins any amount could ever need is `amount` itself, using all `1`s. Any number bigger than `amount` can therefore never be a real answer, so it's used to mean "this amount can't be made yet". The code uses `amount + 2`; `amount + 1` would work just as well.

Why not `int.MaxValue`? The rule adds `1` to `dp` values, and `int.MaxValue + 1` overflows to a negative number, which `Math.Min` would wrongly pick as the smallest.

### Step 3: Set the starting point

```csharp
dp[0] = 0;
```

It takes 0 coins to make 0 cents. Every other answer is built from this one.

### Step 4: Work out each amount from smaller amounts

```csharp
for (int i = 0; i <= amount; i++)
{
    for (int c = 0; c < coins.Length; c++)
    {
        if (coins[c] <= i)
        {
            dp[i] = System.Math.Min(dp[i], 1 + dp[i - coins[c]]);
        }
    }
}
```

| Part | What it does |
|---|---|
| Outer loop `i` | Visits each amount from `0` up to `amount` |
| Inner loop `c` | Tries each coin as the last coin used to make `i` |
| `coins[c] <= i` | Skips a coin that's bigger than the amount. You can't use a 5 to make 3 |
| `dp[i - coins[c]]` | The fewest coins for what's left after using this coin |
| `1 + ...` | Adds the coin just used |
| `Math.Min(dp[i], ...)` | Keeps whichever is smaller: the best answer found so far for `i`, or this new one |

### Step 5: Return the answer

```csharp
return dp[amount] > amount ? -1 : dp[amount];
```

If `dp[amount]` is still bigger than `amount`, no combination of coins ever reached it, so return `-1`. Otherwise it holds the fewest coins.

## Walkthrough

Example 1: `coins = [1,2,5], amount = 11`

The array starts as:

```
amount:  0   1   2   3   4   5   6   7   8   9  10  11
dp:      0  13  13  13  13  13  13  13  13  13  13  13      (13 = amount + 2, "not possible yet")
```

Each row below works out one amount. For every coin that fits, it looks back at `dp[i - coin]` and adds 1. The smallest result is kept.

| i | coin 1: dp[i-1] + 1 | coin 2: dp[i-2] + 1 | coin 5: dp[i-5] + 1 | dp[i] |
|---|---|---|---|---|
| 0 | | | | **0** (starting point) |
| 1 | dp[0] + 1 = 1 | doesn't fit | doesn't fit | **1** |
| 2 | dp[1] + 1 = 2 | dp[0] + 1 = 1 | doesn't fit | **1** |
| 3 | dp[2] + 1 = 2 | dp[1] + 1 = 2 | doesn't fit | **2** |
| 4 | dp[3] + 1 = 3 | dp[2] + 1 = 2 | doesn't fit | **2** |
| 5 | dp[4] + 1 = 3 | dp[3] + 1 = 3 | dp[0] + 1 = 1 | **1** |
| 6 | dp[5] + 1 = 2 | dp[4] + 1 = 3 | dp[1] + 1 = 2 | **2** |
| 7 | dp[6] + 1 = 3 | dp[5] + 1 = 2 | dp[2] + 1 = 2 | **2** |
| 8 | dp[7] + 1 = 3 | dp[6] + 1 = 3 | dp[3] + 1 = 3 | **3** |
| 9 | dp[8] + 1 = 4 | dp[7] + 1 = 3 | dp[4] + 1 = 3 | **3** |
| 10 | dp[9] + 1 = 4 | dp[8] + 1 = 4 | dp[5] + 1 = 2 | **2** |
| 11 | dp[10] + 1 = 3 | dp[9] + 1 = 4 | dp[6] + 1 = 3 | **3** |

The finished array:

```
amount:  0   1   2   3   4   5   6   7   8   9  10  11
dp:      0   1   1   2   2   1   2   2   3   3   2   3
```

**Result: `dp[11] = 3`**

### Reading One Row Closely: Amount 7

```
coin 1:  7 - 1 = 6,  dp[6] = 2,  2 + 1 = 3     (best for 6, then a 1)
coin 2:  7 - 2 = 5,  dp[5] = 1,  1 + 1 = 2     (a 5, then a 2)
coin 5:  7 - 5 = 2,  dp[2] = 1,  1 + 1 = 2     (a 2, then a 5)
```

The smallest is `2`, so `dp[7] = 2`. The array doesn't record that the coins are `5 + 2`, only that **2 coins** is the fewest. That's all the problem asks for.

### Finding the Coins Behind the Answer (Optional)

The problem doesn't ask for this, but you can follow the table backwards to see which coins were used. From `11`, look for a coin where `dp[11 - coin] + 1` gives `dp[11]`:

```
11:  coin 1 → dp[10] + 1 = 3  ✓   use a 1, 10 left
10:  coin 5 → dp[5]  + 1 = 2  ✓   use a 5,  5 left
 5:  coin 5 → dp[0]  + 1 = 1  ✓   use a 5,  0 left
```

`11 = 1 + 5 + 5`, which is 3 coins.

## Walkthrough: An Amount That Can't Be Made

Example 2: `coins = [2], amount = 3`

```
start:   amount:  0   1   2   3
         dp:      0   5   5   5      (5 = amount + 2)
```

| i | coin 2: dp[i-2] + 1 | dp[i] |
|---|---|---|
| 0 | | **0** |
| 1 | doesn't fit | **5** (unchanged) |
| 2 | dp[0] + 1 = 1 | **1** |
| 3 | dp[1] + 1 = 6 | min(5, 6) = **5** (unchanged) |

`dp[3] = 5`, which is bigger than `amount` (3), so amount 3 was never reached.

**Result: -1**

`dp[1]` was never reachable, so building `3` from it (`dp[1] + 1 = 6`) is also too big. Impossible amounts stay at the "not possible" value, and `Math.Min` never picks them over a real answer.

## Common Points of Confusion

- **`dp[i]` is a count of coins, not an amount of money.** `dp[7] = 2` means "7 cents needs 2 coins".
- **The loops can be swapped.** Looping over coins on the outside and amounts on the inside gives the same answer. Each `dp[i]` still sees every coin before the final answer is read.
- **Why `amount + 1` slots?** Arrays start at index 0, so an array of length `amount` would stop at `amount - 1`. The extra slot lets `dp[amount]` exist.
- **The "not possible" value must be bigger than `amount`.** The return check `dp[amount] > amount` depends on this.
- **Don't confuse this with 518. Coin Change II.** That problem asks how many different combinations make the amount. This one asks for the fewest coins.

## Complexity

| | Cost | Why |
|---|---|---|
| **Time** | O(A × C) | The outer loop runs `A + 1` times (A is `amount`), and the inner loop tries all `C` coins each time |
| **Space** | O(A) | The `dp` array has `A + 1` slots |

For the largest input (`amount = 10^4`, 12 coins) that's about 120,000 steps.

## Running the Code

`Main` runs the solution with `coins = [1, 2, 5]`. The `amount` there is currently set to `12`, not the `11` from Example 1. The answer for 12 is also 3 (`5 + 5 + 2`).

After each update, `CoinChange` calls `printAmount`, which prints the coin being tried followed by the whole `dp` array. This lets you watch the table fill in one step at a time.

## Code

[Program.cs](Program.cs)
