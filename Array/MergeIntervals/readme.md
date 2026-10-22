# Merge Intervals

[56. Merge Intervals - LeetCode (Medium)](https://leetcode.com/problems/merge-intervals/)

Given an array of `intervals` where `intervals[i] = [start, end]`, merge all overlapping intervals and return an array of the non-overlapping intervals that cover every interval in the input.

## What is an Interval?

An interval is a range on a number line with a `start` and an `end`.

```
[1,3]   covers   1 2 3
[2,6]   covers     2 3 4 5 6
```

Two intervals **overlap** when one starts before (or exactly when) the other one ends:

```
a = [1,3]
b = [2,6]

b.start (2) <= a.end (3)   ->  they overlap  ->  merged = [1,6]
```

When they overlap, the merged interval keeps the smaller start and the **larger** end:

```
merged = [ min(a.start, b.start), max(a.end, b.end) ]
```

## The Merge Intervals Process

The trick is to **sort first**. Once the intervals are sorted by their start value, any interval that can overlap the current one will sit right next to it. That means one pass through the list is enough.

1. **Sort** the intervals by `start`.
2. **Start** a result list holding the first interval.
3. **Walk** through the rest of the intervals. For each one, compare it to the **last** interval in the result:
   - If `current.start <= last.end`, they overlap. **Extend** the last interval: `last.end = max(last.end, current.end)`.
   - Otherwise there is a gap. **Add** the current interval to the result as a new interval.
4. **Return** the result list.

```
           sort by start
intervals ───────────────► sorted ──► for each interval
                                         │
                       ┌─────────────────┴─────────────────┐
             start <= last.end?                   start > last.end?
                       │                                   │
          extend last.end = max(...)            append as a new interval
```

## Examples

### Example 1: Basic overlap

```
Input:  [[1,3],[2,6],[8,10],[15,18]]
Output: [[1,6],[8,10],[15,18]]
```

```
          0    5    10   15   20
          |....|....|....|....|
[1,3]      ===
[2,6]       =====
[8,10]            ===
[15,18]                  ====

merged     ======  ===   ====
           [1,6] [8,10] [15,18]
```

Step by step (already sorted):

| Current  | Last in result | Overlap?        | Result after step         |
|----------|----------------|-----------------|---------------------------|
| `[1,3]`  | none           | first interval  | `[1,3]`                   |
| `[2,6]`  | `[1,3]`        | 2 <= 3, **yes** | `[1,6]`                   |
| `[8,10]` | `[1,6]`        | 8 > 6, no       | `[1,6] [8,10]`            |
| `[15,18]`| `[8,10]`       | 15 > 10, no     | `[1,6] [8,10] [15,18]`    |

### Example 2: Touching intervals

```
Input:  [[1,4],[4,5]]
Output: [[1,5]]
```

`4 <= 4`, so intervals that share only an end point still count as overlapping.

### Example 3: Unsorted input

```
Input:  [[8,10],[1,3],[2,6]]
Sorted: [[1,3],[2,6],[8,10]]
Output: [[1,6],[8,10]]
```

If you skip the sort, `[8,10]` and `[1,3]` look like they have a gap, and `[1,3]` never gets merged with `[2,6]`.

### Example 4: One interval swallows another

```
Input:  [[1,10],[2,3],[4,5]]
Output: [[1,10]]
```

This is why the merge uses `max(last.end, current.end)` instead of just `current.end`. Without `max`, merging `[1,10]` with `[2,3]` would wrongly shrink it to `[1,3]`.

## Simple C# Solution

```csharp
public static int[][] Merge(int[][] intervals)
{
    if (intervals.Length <= 1)
    {
        return intervals;
    }

    // 1. sort by start
    Array.Sort(intervals, (a, b) => a[0].CompareTo(b[0]));

    // 2. start the result with the first interval
    List<int[]> merged = new List<int[]> { intervals[0] };

    // 3. walk the rest, comparing against the last merged interval
    for (int i = 1; i < intervals.Length; i++)
    {
        int[] last    = merged[merged.Count - 1];
        int[] current = intervals[i];

        if (current[0] <= last[1])
        {
            // overlap: extend the end of the last interval
            last[1] = Math.Max(last[1], current[1]);
        }
        else
        {
            // gap: start a new interval
            merged.Add(current);
        }
    }

    // 4. return the result
    return merged.ToArray();
}
```

## Complexity

| | Cost | Why |
|---|---|---|
| **Time**  | `O(n log n)` | Sorting dominates; the merge pass itself is `O(n)`. |
| **Space** | `O(n)`       | The result list can hold up to `n` intervals (no overlaps at all). |

## Common Mistakes

- **Forgetting to sort.** The single-pass merge only works on sorted input.
- **Using `<` instead of `<=`.** `[1,4]` and `[4,5]` should merge into `[1,5]`.
- **Overwriting the end instead of taking `max`.** A short interval inside a long one must not shrink it.

## Where This Pattern Shows Up

- Combining calendar bookings or meeting times
- [57. Insert Interval](https://leetcode.com/problems/insert-interval/)
- [252. Meeting Rooms](https://leetcode.com/problems/meeting-rooms/) / [253. Meeting Rooms II](https://leetcode.com/problems/meeting-rooms-ii/)
- [435. Non-overlapping Intervals](https://leetcode.com/problems/non-overlapping-intervals/)
