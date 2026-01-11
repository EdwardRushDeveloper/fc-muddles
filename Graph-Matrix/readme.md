# Working with Graphs and Matrices: The Number of Islands Problem

Graphs and matrices are fundamental data structures in computer science. Many problems, such as the "Number of Islands" problem, can be solved by representing data as a 2D grid (matrix) and applying graph traversal algorithms.

## The Number of Islands Problem

**Problem Statement:**  
Given a 2D grid of `'1'`s (land) and `'0'`s (water), count the number of islands. An island is surrounded by water and is formed by connecting adjacent lands horizontally or vertically.

### Example

```text
Input:
11110
11010
11000
00000

Output: 1
```

### Approach

1. **Model the Grid as a Graph:**  
    Each cell is a node. Edges exist between horizontally or vertically adjacent land cells.

2. **Traversal Algorithms:**  
    - **Depth-First Search (DFS)**
    - **Breadth-First Search (BFS)**

3. **Algorithm Steps:**
    - Iterate through each cell.
    - When a land cell (`'1'`) is found, start a DFS/BFS to mark all connected land as visited.
    - Increment the island count for each new traversal.

### Sample Python Code

```python
def numIslands(grid):
     if not grid:
          return 0

     rows, cols = len(grid), len(grid[0])
     visited = set()
     islands = 0

     def dfs(r, c):
          if (r < 0 or r >= rows or
                c < 0 or c >= cols or
                grid[r][c] == '0' or
                (r, c) in visited):
                return
          visited.add((r, c))
          dfs(r+1, c)
          dfs(r-1, c)
          dfs(r, c+1)
          dfs(r, c-1)

     for r in range(rows):
          for c in range(cols):
                if grid[r][c] == '1' and (r, c) not in visited:
                     dfs(r, c)
                     islands += 1

     return islands
```
### Sample C# Code

```csharp
public class Solution {
    public int NumIslands(char[][] grid) {
        if (grid == null || grid.Length == 0) return 0;
        int rows = grid.Length, cols = grid[0].Length, islands = 0;

        void Dfs(int r, int c) {
            if (r < 0 || r >= rows || c < 0 || c >= cols || grid[r][c] == '0')
                return;
            grid[r][c] = '0'; // Mark as visited
            Dfs(r + 1, c);
            Dfs(r - 1, c);
            Dfs(r, c + 1);
            Dfs(r, c - 1);
        }

        for (int r = 0; r < rows; r++) {
            for (int c = 0; c < cols; c++) {
                if (grid[r][c] == '1') {
                    Dfs(r, c);
                    islands++;
                }
            }
        }
        return islands;
    }
}
```
### Key Concepts

- **Matrix Traversal:** Systematically visit each cell.
- **Graph Representation:** Treat each cell as a node, with edges to adjacent cells.
- **Connected Components:** Each island is a connected component in the graph.

---

**References:**
- [LeetCode 200. Number of Islands](https://leetcode.com/problems/number-of-islands/)
- [Graph Theory Basics](https://en.wikipedia.org/wiki/Graph_theory)