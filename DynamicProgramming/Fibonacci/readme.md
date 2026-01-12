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


# Code Outputs will look like this for a Fibonacci call for 9 numbers
```

Computation for each side of the  calculation
Recusive side left
F(n-1) + F(n-2)



InvocationId 68762DAAB51F4F128E5139A82EBA3378: Base Case Reached Fib(1) = 1

**********************InvocationId EDCF4F6640BF4E21ACEC21B5B9CBD1C2: Winding out to call Right Side Fib(0) after Left Side Fib(1) = Left Result is 1 ************************

InvocationId DD610A882BD943B3B8F75BEACCD9AF28: Base Case Reached Fib(0) = 0

InvocationId EDCF4F6640BF4E21ACEC21B5B9CBD1C2: Winding Out Results for Number(2) = Fib(1) + Fib(0) OR 1 + 0 = 1 


Storing Computed Value for Number(2) = 1 in Dictionary

**********************InvocationId F9CD0E6BFC1344D49EC7563B12C26CF6: Winding out to call Right Side Fib(1) after Left Side Fib(2) = Left Result is 1 ************************

InvocationId 4B8C8AAD81374D739CE6E7E20998E6D2: Base Case Reached Fib(1) = 1

InvocationId F9CD0E6BFC1344D49EC7563B12C26CF6: Winding Out Results for Number(3) = Fib(2) + Fib(1) OR 1 + 1 = 2 


Storing Computed Value for Number(3) = 2 in Dictionary

**********************InvocationId C230E2579F534A77A45E9464EA34F910: Winding out to call Right Side Fib(2) after Left Side Fib(3) = Left Result is 2 ************************

Already Computed for Number 2: Value is 1: InvoicationId 7A44023D999644F7A6BD51266E4A6A68

InvocationId C230E2579F534A77A45E9464EA34F910: Winding Out Results for Number(4) = Fib(3) + Fib(2) OR 2 + 1 = 3 


Storing Computed Value for Number(4) = 3 in Dictionary

**********************InvocationId 2096D76AF8A347C08230135F856440DF: Winding out to call Right Side Fib(3) after Left Side Fib(4) = Left Result is 3 ************************

Already Computed for Number 3: Value is 2: InvoicationId DEF9660C534C4DB2BF9C52B01A98B779

InvocationId 2096D76AF8A347C08230135F856440DF: Winding Out Results for Number(5) = Fib(4) + Fib(3) OR 3 + 2 = 5 


Storing Computed Value for Number(5) = 5 in Dictionary

**********************InvocationId 229D465DC9294310906EB14E3EC1258A: Winding out to call Right Side Fib(4) after Left Side Fib(5) = Left Result is 5 ************************

Already Computed for Number 4: Value is 3: InvoicationId 59AC1B536C634F02B73448156A22F6BA

InvocationId 229D465DC9294310906EB14E3EC1258A: Winding Out Results for Number(6) = Fib(5) + Fib(4) OR 5 + 3 = 8 


Storing Computed Value for Number(6) = 8 in Dictionary

**********************InvocationId FE20E528E05E4D0D95EDAA2ACD57A3FE: Winding out to call Right Side Fib(5) after Left Side Fib(6) = Left Result is 8 ************************

Already Computed for Number 5: Value is 5: InvoicationId E5281BA05DB34A14B5BA49C5FCB871F4

InvocationId FE20E528E05E4D0D95EDAA2ACD57A3FE: Winding Out Results for Number(7) = Fib(6) + Fib(5) OR 8 + 5 = 13 


Storing Computed Value for Number(7) = 13 in Dictionary

**********************InvocationId 617B5FFFC5D64EB6B2A49F2B1BE05E3B: Winding out to call Right Side Fib(6) after Left Side Fib(7) = Left Result is 13 ************************

Already Computed for Number 6: Value is 8: InvoicationId 670F6EF84AE44BECB468D9B5011D76B3

InvocationId 617B5FFFC5D64EB6B2A49F2B1BE05E3B: Winding Out Results for Number(8) = Fib(7) + Fib(6) OR 13 + 8 = 21 


Storing Computed Value for Number(8) = 21 in Dictionary

**********************InvocationId FB67D432427E4C889D4DCCEE1177A15E: Winding out to call Right Side Fib(7) after Left Side Fib(8) = Left Result is 21 ************************

Already Computed for Number 7: Value is 13: InvoicationId 0930C6D39AD24ECEB0FD6C6B63AE6FCA

InvocationId FB67D432427E4C889D4DCCEE1177A15E: Winding Out Results for Number(9) = Fib(8) + Fib(7) OR 21 + 13 = 34 


Storing Computed Value for Number(9) = 34 in Dictionary

**********************InvocationId 2727FE0DE5E84DCA810F830180636C3A: Winding out to call Right Side Fib(8) after Left Side Fib(9) = Left Result is 34 ************************

Already Computed for Number 8: Value is 21: InvoicationId F5D25A02E3E8469F8F5FF44DB0337F51

InvocationId 2727FE0DE5E84DCA810F830180636C3A: Winding Out Results for Number(10) = Fib(9) + Fib(8) OR 34 + 21 = 55 


Storing Computed Value for Number(10) = 55 in Dictionary

**********************InvocationId D23FE095420E4CE8B4DB90DC12309507: Winding out to call Right Side Fib(9) after Left Side Fib(10) = Left Result is 55 ************************

Already Computed for Number 9: Value is 34: InvoicationId 6CCC7B796B6A411DBE8F6A7CA2A4FC27

InvocationId D23FE095420E4CE8B4DB90DC12309507: Winding Out Results for Number(11) = Fib(10) + Fib(9) OR 55 + 34 = 89 


Storing Computed Value for Number(11) = 89 in Dictionary

**********************InvocationId 9B94758047C0467FA90C3AD46369469A: Winding out to call Right Side Fib(10) after Left Side Fib(11) = Left Result is 89 ************************

Already Computed for Number 10: Value is 55: InvoicationId 8641B08D639A4769B1820A9104D624A9

InvocationId 9B94758047C0467FA90C3AD46369469A: Winding Out Results for Number(12) = Fib(11) + Fib(10) OR 89 + 55 = 144 


Storing Computed Value for Number(12) = 144 in Dictionary

Final Number 144
Fib(2) = 1
Fib(3) = 2
Fib(4) = 3
Fib(5) = 5
Fib(6) = 8
Fib(7) = 13
Fib(8) = 21
Fib(9) = 34
Fib(10) = 55
Fib(11) = 89
Fib(12) = 144
```