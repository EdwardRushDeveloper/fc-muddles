# Number of Islands

[200. Number of Islands](https://leetcode.com/problems/number-of-islands/)

**Difficulty:** Medium

The code in [Program.cs](Program.cs) solves this problem with a **depth-first search (DFS)** that "sinks" each island as it counts it.

## The Problem

You're given a grid of `'1'` (land) and `'0'` (water). An island is a group of land cells joined up, down, left or right. Diagonal cells don't count as joined. The task is to return how many islands there are.

## How the Class Is Organised

| Part | Lines | What it does |
|---|---|---|
| `Main` | [81-89](Program.cs#L81-L89) | Calls `NumberOfIslands()` and prints the count. |
| `NumberOfIslands` | [101-155](Program.cs#L101-L155) | Builds a hard-coded test grid, scans every cell, and starts a DFS whenever it finds a `'1'`. |
| `DFS` | [158-226](Program.cs#L158-L226) | Turns the whole connected island into `'0'`s and returns 1. |

The grid is a jagged array, `char[][]`, rather than `char[,]`. The comment at lines 27-31 explains why: LeetCode passes the grid as `char[][]`, so building it the same way lets the method move straight over.

## How It Works

### 1. The scan in `NumberOfIslands`

Two nested loops visit every cell, row by row and left to right. When a cell is `'1'`, it's the first unvisited cell of a new island. The code passes that cell to `DFS` and adds what comes back, which is always 1, to `returnValue`.

### 2. Sinking the island in `DFS`

- **Stop checks (lines 178-189):** return 0 straight away if the cell is outside the grid or is already `'0'`. The order of these checks matters. The row is bounds-checked before `matrix[currentRow].Length` is read, and `||` stops at the first true check, so the code never reads past the end of the array.
- **Sink (line 194):** set the cell to `'0'`. This is the "visited" mark. Because the cell is now water, it can't be counted again, either by the outer loop or by a later DFS call. No separate `visited` array is needed.
- **Spread (lines 206-221):** call `DFS` on the cells below, above, right and left. Each of those calls sinks its cell and spreads further, until the whole island is water.
- **Return 1 (line 223):** this one call accounts for one island. The return values of the four calls in each direction are ignored. They're 0 or 1, but only the first call for each island contributes to the count.

## Walkthrough

The active test grid in the code (expected answer: 3):

```
row 0:  1 1 0 0 0
row 1:  1 1 0 0 0
row 2:  0 0 1 0 0
row 3:  0 0 0 1 1
```

| Scan reaches | Cell value | What happens | Count |
|---|---|---|---|
| (0,0) | `1` | DFS sinks (0,0), (1,0), (1,1), (0,1): the top-left 2×2 block | 1 |
| (0,1) to (2,1) | `0` | These cells were already sunk or were water, so they're skipped | 1 |
| (2,2) | `1` | DFS sinks only (2,2). Its neighbours are all water. (3,3) is diagonal, so it isn't joined | 2 |
| (3,3) | `1` | DFS sinks (3,3) and then (3,4) to its right | 3 |
| (3,4) | `0` | Already sunk | 3 |

The program prints `Number Of Islands Result 3`. It also prints a `1` each time an island is found (line 149), so you'll see three `1` lines before the result.

## Complexity

| | Cost | Why |
|---|---|---|
| **Time** | O(m × n) | Every cell is visited by the scan. Each land cell is sunk once, and after that it stops any later DFS call immediately. |
| **Space** | O(m × n) in the worst case | No extra arrays are used, but the recursion can go as deep as the number of land cells, for example when the whole grid is one long winding island. |

## Things Worth Knowing

- **It changes the input.** After the method runs, the grid is all `'0'`s. That's fine on LeetCode, but you couldn't use the grid again afterwards. To keep it, copy the grid first or use a separate `visited` array.
- **Recursion depth.** The largest grid allowed is 300×300 (90,000 cells). If it's all land, the recursion can go very deep, and in .NET that can overflow the stack. A version that uses an explicit `Stack<(int,int)>`, or a BFS with a `Queue`, avoids that risk.
- **The test data is built in.** `NumberOfIslands()` takes no parameter and builds its own grid. To submit to LeetCode, you'd change it to `NumIslands(char[][] grid)`.
- **The comment at line 168** says "greater than", but the code correctly checks `>=`. A column equal to the row length is already out of bounds.
- **The `Console.WriteLine` at line 149** looks like leftover debug output. It doesn't affect the result.

## Code

[Program.cs](Program.cs)

## Visual Walkthrough

### 1. What the grid means

The code stores this grid:

```
     col 0  1  2  3  4
row 0    1  1  0  0  0
row 1    1  1  0  0  0
row 2    0  0  1  0  0
row 3    0  0  0  1  1
```

Here's the same grid as a map, with each island labelled:

```
        0   1   2   3   4
      ┌───┬───┬───┬───┬───┐
   0  │ A │ A │ ~ │ ~ │ ~ │
      ├───┼───┼───┼───┼───┤
   1  │ A │ A │ ~ │ ~ │ ~ │        A = island 1 (4 cells)
      ├───┼───┼───┼───┼───┤        B = island 2 (1 cell)
   2  │ ~ │ ~ │ B │ ~ │ ~ │        C = island 3 (2 cells)
      ├───┼───┼───┼───┼───┤        ~ = water
   3  │ ~ │ ~ │ ~ │ C │ C │
      └───┴───┴───┴───┴───┘
```

**Why are B and C separate islands?** Only the four sides count as joined:

```
          ↑
        ← ● →        joined: up, down, left, right
          ↓

        ↖   ↗
          ●          NOT joined: diagonals
        ↙   ↘
```

B at (2,2) and C at (3,3) touch only at a corner, so they're two islands.

### 2. The outer scan: reading the grid like a book

`NumberOfIslands` reads every cell left to right, top to bottom. It does nothing on water. When it lands on a `1`, it has found a new island. It adds 1 to the count and calls DFS to sink the whole island.

**Legend:** `■` land not visited yet · `~` water · `●` the scan is here · `·` sunk (land turned to 0)

**Step 1: the scan reaches (0,0) and finds land. Count = 1. DFS sinks island A.**

```
  before                  after DFS
  ● ■ ~ ~ ~               · · ~ ~ ~
  ■ ■ ~ ~ ~      ──►      · · ~ ~ ~
  ~ ~ ■ ~ ~               ~ ~ ■ ~ ~
  ~ ~ ~ ■ ■               ~ ~ ~ ■ ■
```

**Step 2: the scan moves on.** (0,1), (1,0) and (1,1) were land, but DFS already turned them to 0, so the scan passes them like water. **Sinking is what stops an island from being counted twice.**

**Step 3: the scan reaches (2,2) and finds land. Count = 2. DFS sinks island B.** It has no land neighbours, so only that one cell sinks.

```
  · · ~ ~ ~               · · ~ ~ ~
  · · ~ ~ ~      ──►      · · ~ ~ ~
  ~ ~ ● ~ ~               ~ ~ · ~ ~
  ~ ~ ~ ■ ■               ~ ~ ~ ■ ■
```

**Step 4: the scan reaches (3,3) and finds land. Count = 3. DFS sinks island C**, including (3,4) next to it.

```
  · · ~ ~ ~               · · ~ ~ ~
  · · ~ ~ ~      ──►      · · ~ ~ ~
  ~ ~ · ~ ~               ~ ~ · ~ ~
  ~ ~ ~ ● ■               ~ ~ ~ · ·
```

**Step 5: the scan reaches (3,4).** It's already sunk. The scan ends with **3**.

### 3. Inside DFS: how one island gets sunk

Here is island A sinking, starting from (0,0). DFS always tries the neighbours in the order **down, up, right, left**.

#### Recursive `DFS`: follow one path as far as it goes, then come back

```
DFS(0,0)  sink                          · ■      call stack (deepest at bottom)
 ├─ down → DFS(1,0)  sink               · ■      DFS(0,0)
 │   ├─ down → (2,0) water, return 0             └ DFS(1,0)
 │   ├─ up   → (0,0) sunk,  return 0
 │   ├─ right→ DFS(1,1)  sink           · ■
 │   │   ├─ down → (2,1) water                   DFS(0,0)
 │   │   ├─ up   → DFS(0,1)  sink       · ·      └ DFS(1,0)
 │   │   │    (all 4 neighbours are               └ DFS(1,1)
 │   │   │     water, sunk or off the grid)        └ DFS(0,1)  ← deepest
 │   │   ├─ right→ (1,2) water
 │   │   └─ left → (1,0) sunk
 │   └─ left → off the grid
 ├─ up    → off the grid
 ├─ right → (0,1) already sunk
 └─ left  → off the grid
return 1
```

Sink order: **(0,0) → (1,0) → (1,1) → (0,1)**. Each nested call is one frame on the call stack. A long, winding island means many nested frames, which is the stack overflow risk.

#### `DFSWithStack`: keep a to-do list instead of nesting calls

The cells waiting to be checked go on a `Stack`. A cell is sunk as soon as it's pushed.

```
 step  pop      neighbours found (D,U,R,L)        stack after (top on right)   grid
 ────  ───────  ────────────────────────────────  ───────────────────────────  ─────
 start  —       sink (0,0) and push it            [(0,0)]                      · ■
                                                                               ■ ■

 1     (0,0)    D (1,0) land → sink, push         [(1,0), (0,1)]               · ·
                U off grid                                                     · ■
                R (0,1) land → sink, push
                L off grid

 2     (0,1)    D (1,1) land → sink, push         [(1,0), (1,1)]               · ·
                U off grid   R water   L sunk                                  · ·

 3     (1,1)    nothing new                       [(1,0)]                      · ·
                                                                               · ·
 4     (1,0)    nothing new                       [ ]  empty → return 1
```

The stack never held more than 2 cells, and there's no nesting. The to-do list lives in an ordinary object instead of the call stack, so a huge island can't overflow anything.

### 4. Both versions side by side

```
          Recursive DFS                    DFSWithStack
   ┌──────────────────────────┐    ┌──────────────────────────┐
   │ To-do list = call stack  │    │ To-do list = Stack<>     │
   │ (managed by .NET)        │    │ (an object you manage)   │
   │                          │    │                          │
   │ Sinks when a call starts │    │ Sinks when a cell is     │
   │                          │    │ pushed                   │
   │ Very deep for big        │    │ Size is limited only by  │
   │ islands → can overflow   │    │ memory → no overflow     │
   │                          │    │                          │
   │ Same result: island sunk, return 1                      │
   └──────────────────────────┘    └──────────────────────────┘
```

**In short:** the scan finds a piece of land, counts it, then floods the whole island so the scan never counts it again. The count at the end is the number of islands.
