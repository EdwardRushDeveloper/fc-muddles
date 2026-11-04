# Binary Tree Zigzag Level Order Traversal

[103. Binary Tree Zigzag Level Order Traversal](https://leetcode.com/problems/binary-tree-zigzag-level-order-traversal/description/)

**Difficulty:** Medium

Given the `root` of a binary tree, return the zigzag level order traversal of its nodes' values. (i.e., from left to right, then right to left for the next level and alternate between).

Example 1:
```
            3
          /   \
         9     20
              /  \
             15   7

Input: root = [3,9,20,null,null,15,7]
Output: [[3],[20,9],[15,7]]
```

Example 2:
```
Input: root = [1]
Output: [[1]]
```

Example 3:
```
Input: root = []
Output: []
```

Constraints:
```
The number of nodes in the tree is in the range [0, 2000].
-100 <= Node.val <= 100
```

## Reading the Input

The tree is given in **level order**: each level is read left to right, and `null` marks a missing child.

```
[3,9,20,null,null,15,7]

level 0:  3
level 1:  9, 20
level 2:  null, null (children of 9), 15, 7 (children of 20)
```

## Reading the Output

The output is a list of levels, one inner list per level, starting at the root. The direction alternates on each level:

```
level 0:  left to right   [3]
level 1:  right to left   [20, 9]
level 2:  left to right   [15, 7]
```

## LeetCode Starting Code

```csharp
/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int val=0, TreeNode left=null, TreeNode right=null) {
 *         this.val = val;
 *         this.left = left;
 *         this.right = right;
 *     }
 * }
 */
public class Solution {
    public IList<IList<int>> ZigzagLevelOrder(TreeNode root) {

    }
}
```

## The Key Idea: Read Normally, Write Some Levels Backwards

This is a normal **level order traversal** (Breadth First Search) with one change: every other level is written into the result in reverse.

The tree itself is always visited the same way, left to right, one level at a time. Only the **output** of a level changes direction:

```
level 0:  visited 3        written 3          (left to right)
level 1:  visited 9, 20    written 20, 9      (reversed)
level 2:  visited 15, 7    written 15, 7      (left to right)
```

So the problem breaks into two smaller questions:

1. **How do you visit a tree one level at a time?** Use a queue.
2. **How do you reverse a level?** Use a stack.

## Approach: BFS With a Queue, Reversing Odd Levels With a Stack

### Why a Queue Gives Level Order

A queue is **first in, first out**: things come out in the same order they went in. If you take a node out, then put its children in at the back, every node on one level comes out before any node on the next level.

```
queue: [3]                take 3,  add 9, 20
queue: [9, 20]            take 9   (no children)
queue: [20]               take 20, add 15, 7
queue: [15, 7]            take 15, take 7
queue: []                 done
```

Nodes came out in the order `3, 9, 20, 15, 7`, which is level by level.

### Why a Stack Reverses a Level

A stack is **last in, first out**: the last thing pushed is the first thing popped. Push `9` then `20`, and popping gives `20` then `9`. That's exactly the reversed order an odd level needs.

### The Code, Line by Line

```csharp
Queue<TreeNode> queue = new Queue<TreeNode>();
queue.Enqueue(root);
bool doswitch = false;
```

The queue starts with just the root. `doswitch` tracks the direction of the current level: `false` means left to right, `true` means right to left. Level 0 is left to right, so it starts at `false`.

```csharp
while(queue.Count > 0)
{
    int queueSize = queue.Count;
```

Each pass of the `while` loop handles **one whole level**. At the start of a pass, the queue holds exactly the nodes on that level, so `queueSize` is the number of nodes to take out.

`queueSize` has to be saved before the `for` loop starts. Inside the loop, children are added to the queue, so `queue.Count` keeps growing. Without the saved count, the loop would carry on into the next level.

```csharp
    Stack<int> stack = new Stack<int>();
    List<int> level = new List<int>();
```

A fresh stack and list for each level. `level` becomes this level's entry in the result.

```csharp
    for(int i = 0; i < queueSize; i++)
    {
        TreeNode current = queue.Dequeue();

        if(doswitch)
            stack.Push(current.val);
        else
            level.Add(current.val);

        if(current.left != null) queue.Enqueue(current.left);
        if(current.right!= null) queue.Enqueue(current.right);
    }
```

| Part | What it does |
|---|---|
| `queue.Dequeue()` | Takes the next node on this level, in left to right order |
| `doswitch` is `false` | Left to right level: add the value straight to `level` |
| `doswitch` is `true` | Right to left level: push the value onto the stack to reverse it later |
| `Enqueue(left)`, then `Enqueue(right)` | Lines up the next level. Left always goes first, so the queue stays left to right whatever direction this level is written in |

```csharp
    doswitch = !doswitch;

    while(stack.Count > 0)
    {
        level.Add(stack.Pop());
    }

    result.Add(level);
}
```

`doswitch` flips so the next level goes the other way. On a right to left level, popping the stack fills `level` in reverse. On a left to right level the stack is empty, so this loop does nothing. Finally the finished level is added to the result.

## Walkthrough

Example 1: `root = [3,9,20,null,null,15,7]`

```
            3
          /   \
         9     20
              /  \
             15   7
```

| Pass | Queue at start | `queueSize` | `doswitch` | What happens to each node | `level` added | Queue at end |
|---|---|---|---|---|---|---|
| 1 | [3] | 1 | false | 3 → added to `level` | [3] | [9, 20] |
| 2 | [9, 20] | 2 | true | 9 → pushed, 20 → pushed. Stack pops 20, then 9 | [20, 9] | [15, 7] |
| 3 | [15, 7] | 2 | false | 15 → added, 7 → added | [15, 7] | [] |

The queue is empty, so the loop stops.

**Result: `[[3],[20,9],[15,7]]`**

Pass 2 is where the zigzag happens. The queue gave the nodes in normal order (`9`, then `20`), and the stack turned them around (`20`, then `9`).

## Common Points of Confusion

- **The children are always queued left then right.** It's tempting to queue right then left on reversed levels. That breaks the levels below, because the queue would no longer be in left to right order. Keep the visiting order fixed and only reverse the output.
- **Why save `queueSize`?** The queue holds two levels at once while the `for` loop runs: what's left of the current level at the front, and the new children at the back. The saved count says where the current level ends.
- **The stack is just one way to reverse.** Calling `level.Reverse()` on odd levels, or using `level.Insert(0, value)`, gives the same result.
- **Empty tree (Example 3).** If `root` is `null`, the current code puts `null` in the queue, and `current.val` then throws a `NullReferenceException`. Adding `if (root == null) return result;` before the root is queued makes it return `[]`.

## Complexity

| | Cost | Why |
|---|---|---|
| **Time** | O(N) | Every node goes into the queue once and comes out once. Each value is pushed and popped at most once |
| **Space** | O(N) | The queue holds up to one full level. In a full tree, the bottom level has about N/2 nodes |

## Testing Helpers

- **`LoadTestTree()`** builds the Example 1 tree by hand and returns its root.
- **`Main`** builds the test tree and calls `ZigzagLevelOrder`. It doesn't print the result yet.

## Code

[Program.cs](Program.cs)
