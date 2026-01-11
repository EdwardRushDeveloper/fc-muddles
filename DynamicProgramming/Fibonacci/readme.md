# Fibonacci

Without Memoization, the recursive tree will have to calculate for N mulitple times.

![Recusive Tree](fibonacci-recursive-tree.png)


## What is Fibonacci?

The Fibonacci sequence is a series of numbers where each number is the sum of the two preceding ones, starting from 0 and 1. Mathematically, it is defined as:

```
F(0) = 0  
F(1) = 1  
F(n) = F(n-1) + F(n-2) for n > 1
```

The sequence begins: 0, 1, 1, 2, 3, 5, 8, 13, ...

## Solving Fibonacci with Dynamic Programming

Dynamic programming is an optimization technique that solves complex problems by breaking them down into simpler subproblems and storing the results of these subproblems to avoid redundant computations.

For Fibonacci, dynamic programming can be applied in two main ways:

- **Top-Down (Memoization):** Store the results of each Fibonacci calculation in a table (usually an array or dictionary) as you compute them recursively. When a value is needed again, retrieve it from the table instead of recalculating.
- **Bottom-Up (Tabulation):** Build up the solution iteratively from the base cases, filling in a table from the bottom up until you reach the desired value.

Both approaches reduce the time complexity from exponential (`O(2^n)`) in the naive recursive solution to linear (`O(n)`), making the computation much more efficient.

## Links
[Fibonacci - MIT OpenCourseWare 15. Dynamic Programming, Part 1: SRTBOT, Fib, DAGs, Bowling](https://youtu.be/r4-cftqTcdI?si=QDe0a5-pPXcfZTl_)
