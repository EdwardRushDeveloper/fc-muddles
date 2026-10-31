

# Problems to be aware of 

What the test usually looks like
2–3 problems in about 60–90 minutes

Mostly easy to medium, focused on arrays, strings, hash maps and greedy logic
Graded on hidden test cases, so handling edge cases and writing efficient code both matter

Usually sent for entry to mid-level SWE roles. Senior and rehire candidates often skip it.
# Problems candidates commonly report

**Problem Main idea**
- Largest K where both K and -K are in the array |	Hash set
- Max sum of two numbers with the same digit sum |	Hash map keyed by digit sum
- Min deletions so every character’s count is unique |	Count frequencies, then a greedy pass
- Maximal network rank (two cities with the most roads touching them) |	Count connections per node
- Min swaps to group all the same letters/1s together |	Sliding window
- Longest substring with no 3 identical letters in a row | 	Scan the string once
- Min steps/moves to make an array valid or balanced | 	Greedy or prefix sums
- Lexicographically smallest string after removing one character. Remove the first character that’s bigger than the one after it

# Recent assessment reports (2025–2026)

Microsoft doesn’t publish which problems it uses, so there’s no official “most recent testing” list. What exists are candidate reports collected by prep sites, mostly from intern and new-grad assessments in 2025–2026. Those reports repeat the same few problems.

**Problems candidates report most often (HackerRank, sometimes Codility)**

| Problem | Description | Approach |
|---|---|---|
| Circular Character Roll | For each `roll[i]`, shift the first `roll[i]` characters of a string forward one letter, wrapping `z → a`. | Difference (prefix-sum) array instead of simulating each roll |
| No 3 identical consecutive letters | Remove the fewest characters so no 3 identical letters are consecutive (strings of only `a` and `b`). | Greedy single pass |
| Stacks/Blocks Conversion | Every 2 blocks in a stack merge into 1 that carries to the next stack, like binary carry. Return the leftover blocks. | Carry forward stack by stack |
| Subarray Permutation Check | In a permutation of `1..N`, decide whether any length-K subarray is a permutation of `1..K`. | Sliding window |
| Dual Intern Task Allocation | Assign exactly `k` tasks to one person and the rest to another to maximize total reward. | Greedy: sort or heap on the gain from moving each task |




# Links

[Hacker Rank](https://www.hackerrank.com/dashboard)

[DotNet Fiddle](https://dotnetfiddle.net/jKqpjy)






# Helpful Links


Platform was Hackerrank : 

Problem Statement: An e-book provider offers a variety of programming articles, each with a distinct intellectual value. Readers earn points equal to an article's intellectual value for each article read twice. Given a list of articles with their page lengths, intellectual
value coefficients, and a daily page limit, find the maximum achievable intellectual value in one day.

The problem is a variant of the 0/1 Knapsack Problem where each item must be read twice, meaning its effective page weight is 2 * articles[i] and its reward is iv[i]

[Microsoft Senior Software Engineer - online Assessment](https://leetcode.com/discuss/post/7279135/microsoft-senior-software-engineer-onlin-yxeo/)

[Vmware Interview Question](https://vipulgupta-19290.medium.com/vmware-interview-question-fda2120fc9b9)