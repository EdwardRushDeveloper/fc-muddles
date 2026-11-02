# Lowest Common Ancestor of a Binary Tree

[236. Lowest Common Ancestor of a Binary Tree](https://leetcode.com/problems/lowest-common-ancestor-of-a-binary-tree/description/)

**Difficulty:** Medium

Given a binary tree, find the lowest common ancestor (LCA) of two given nodes in the tree.

According to the [definition of LCA on Wikipedia](https://en.wikipedia.org/wiki/Lowest_common_ancestor): "The lowest common ancestor is defined between two nodes `p` and `q` as the lowest node in `T` that has both `p` and `q` as descendants (where we allow **a node to be a descendant of itself**)."

Example 1:
```
            3
          /   \
         5     1
        / \   / \
       6   2 0   8
          / \
         7   4

Input: root = [3,5,1,6,2,0,8,null,null,7,4], p = 5, q = 1
Output: 3
Explanation: The LCA of nodes 5 and 1 is 3.
```

Example 2:
```
Input: root = [3,5,1,6,2,0,8,null,null,7,4], p = 5, q = 4
Output: 5
Explanation: The LCA of nodes 5 and 4 is 5, since a node can be a descendant of itself according to the LCA definition.
```

Example 3:
```
            1
           /
          2

Input: root = [1,2], p = 1, q = 2
Output: 1
```

Constraints:
```
The number of nodes in the tree is in the range [2, 10^5].
-10^9 <= Node.val <= 10^9
All Node.val are unique.
p != q
p and q will exist in the tree.
```

## Reading the Input

The tree is given in **level order**: each level is read left to right, and `null` marks a missing child.

```
[3,5,1,6,2,0,8,null,null,7,4]

level 0:  3
level 1:  5, 1
level 2:  6, 2, 0, 8
level 3:  null, null (children of 6), 7, 4 (children of 2)
```

## Definitions

- **Ancestor:** a node is an ancestor of another node if it is on the path from the root down to that node. Because of the LCA definition, a node also counts as an ancestor of itself.
- **Common ancestor:** a node that is an ancestor of both `p` and `q`. The root is always a common ancestor.
- **Lowest:** the common ancestor farthest from the root (the deepest one).

## LeetCode Starting Code

```csharp
/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int x) { val = x; }
 * }
 */
public class Solution {
    public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q) {

    }
}
```

`p` and `q` are passed as node references, not values, and the answer is returned as a node.

## Common Points of Confusion

- **A node can be its own ancestor.** In Example 2, `5` is the answer because `4` is below it and `5` counts as a descendant of itself.
- **`p` and `q` are nodes, not values.** The solution compares references (`root == p`), so a `new TreeNode(5)` that isn't part of the tree will never match. The test in `Main` uses `FindNode` to get the real nodes out of the tree.
- **The tree is not a binary search tree.** The values aren't ordered, so you can't tell which side to search by comparing values. Both sides have to be searched.
- **Both targets are guaranteed to exist.** The solution relies on this (see Example 2 in the walkthrough below).

## Approach: Recursive Depth First Search

The search visits a node's children before deciding about the node itself (post-order). Each call answers one question about its subtree, **"what did I find in here?"**, and returns one of:

| Returns | Meaning |
|---|---|
| `null` | Neither `p` nor `q` is in this subtree |
| `p` or `q` | One target is in this subtree. If the other target is below it, this node is also the answer |
| The LCA | Both targets were found lower down, and the answer is being passed up unchanged |

1. **Base cases.** If the node is `null`, return `null`. If the node is `p` or `q`, return it right away without searching below it.
2. **Search both subtrees.** Call the function on the left child and on the right child.
3. **Both sides found something.** One target is on the left and the other is on the right, so the paths to them split here. This node is the LCA, so return it.
4. **Otherwise** return whichever side found something, or `null` if neither did.

### The Code, Line by Line

```csharp
public static TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
{
    if (root == null || root == p || root == q)
        return root;

    TreeNode left  = LowestCommonAncestor(root.left, p, q);
    TreeNode right = LowestCommonAncestor(root.right, p, q);

    if (left != null && right != null)
        return root;

    return left ?? right;
}
```

| Line | What it does |
|---|---|
| `if (root == null ...)` | Gone past a leaf. There's nothing here, so return `null` |
| `... root == p \|\| root == q` | Standing on a target. Return it without searching below it. `==` compares **references**, so `p` and `q` must be the real nodes in the tree |
| `TreeNode left = ...` | Ask the left subtree what it found |
| `TreeNode right = ...` | Ask the right subtree what it found. Unlike `FindNode` there's no `??` shortcut, because this node needs the answer from **both** sides |
| `if (left != null && right != null)` | One target is on each side. The paths to `p` and `q` split here, so no lower node can be an ancestor of both. This node is the LCA |
| `return left ?? right` | At most one side found something. Pass up that result (a target, or an LCA already found lower down), or `null` if neither side found anything |

### How Results Travel Up the Tree

The answer is decided at one node and then passed up unchanged by every node above it. Each node above the LCA gets the answer from one side and `null` from the other, so `left ?? right` passes it along.

Example: `p = 7, q = 4`. Neither target is near the root, so this shows an LCA being found deep in the tree and passed up.

```
            3
          /   \
         5     1
        / \   / \
       6   2 0   8
          / \
         7   4
```

```
LCA(3)  not a target, search both sides
  LCA(5)  not a target, search both sides
    LCA(6)  both children null → return null
    LCA(2)  not a target, search both sides
      LCA(7)  is p → return 7
      LCA(4)  is q → return 4
    2: left = 7, right = 4, both found → return 2       ← the LCA is decided here
  5: left = null, right = 2 → return left ?? right = 2  ← passed up
  LCA(1)  not a target
    LCA(0)  both children null → return null
    LCA(8)  both children null → return null
  1: left = null, right = null → return null
3: left = 2, right = null → return left ?? right = 2    ← passed up
```

**Result: 2**

What each node returns for this search:

| Node | left | right | Returns | Why |
|---|---|---|---|---|
| 7 | | | 7 | Is `p` |
| 4 | | | 4 | Is `q` |
| 6 | null | null | null | No target below it |
| 2 | 7 | 4 | **2** | One target on each side, this is the LCA |
| 5 | null | 2 | 2 | Passes up the LCA from its right side |
| 0 | null | null | null | No target below it |
| 8 | null | null | null | No target below it |
| 1 | null | null | null | No target below it |
| 3 | 2 | null | 2 | Passes up the LCA from its left side |

When a node returns a non-null value, it can mean "I found a target" or "I found the answer". A parent can't tell which, and doesn't need to. It treats both the same way: "something is on this side".

### Why the Early Return Is Safe

When the search reaches `p`, it stops and returns `p` without checking whether `q` is below it. There are only two possibilities:

- `q` is below `p`. Then `p` is the LCA, and returning `p` is correct.
- `q` is somewhere else. Then a node higher up will get `p` from one side and `q` from the other, and that node becomes the LCA.

Either way the right answer reaches the top. This depends on both targets being in the tree, which the constraints guarantee.

### Approach Walkthrough

Example 1: `p = 5, q = 1`

```
LCA(3)  not a target, search both sides
  LCA(5)  is p → return 5          (6, 2, 7, 4 never visited)
  LCA(1)  is q → return 1          (0, 8 never visited)
3: left = 5, right = 1, both found → return 3
```

**Result: 3**

Example 2: `p = 5, q = 4`

```
LCA(3)  not a target, search both sides
  LCA(5)  is p → return 5          (never looks for 4 below it)
  LCA(1)  not a target
    LCA(0)  both children null → return null
    LCA(8)  both children null → return null
  1: left = null, right = null → return null
3: left = 5, right = null → return left ?? right = 5
```

**Result: 5**

## Complexity

| | Cost | Why |
|---|---|---|
| **Time** | O(N) | Each node is visited at most once |
| **Space** | O(H) | One stack frame for each level of the tree (H is its height). That's O(log N) for a balanced tree and O(N) for a lopsided one |

### Doing It With BFS Instead

A breadth first search can also solve this, but it needs more work:

1. Run a BFS from the root and record each node's parent.
2. Climb from `p` to the root, adding every node to a set of ancestors.
3. Climb from `q` toward the root. The first node that is already in the set is the LCA.

This is also O(N) time but uses O(N) space for the parent records. It avoids recursion, which helps if a very deep tree could overflow the call stack.

## Testing Helpers

- **`LoadTestTree()`** builds the Example 1 tree by hand and returns its root.
- **`FindNode(root, val)`** is a pre-order depth first search that returns the node holding `val`, or `null`. It gets the real `p` and `q` nodes for a test, since the solution needs node references rather than values.

## Code

[Program.cs](Program.cs)
