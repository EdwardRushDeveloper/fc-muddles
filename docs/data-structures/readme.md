# Data Structures

A **data structure** is a way of organizing data in memory so it can be used efficiently. Each structure makes some operations fast and others slow, so picking the right one comes down to which operations a problem needs most: looking items up, inserting, deleting, keeping order, or finding the smallest or largest value.

Most interview problems are really asking "which data structure fits here?" Once that choice is made, the algorithm often follows naturally.

## Common Data Structures

| Structure | What it is | Fast at | Slow at |
|---|---|---|---|
| **Array** | Items stored side by side in memory, accessed by index | Access by index O(1) | Insert/delete in the middle O(n) |
| **Linked List** | Nodes that each point to the next (and/or previous) node | Insert/delete at a known node O(1) | Access by position O(n) |
| **Stack** | Last In, First Out (LIFO) | Push/pop from the top O(1) | Searching O(n) |
| **Queue** | First In, First Out (FIFO) | Enqueue/dequeue O(1) | Searching O(n) |
| **Hash Table** (`Dictionary`) | Key → value pairs found by hashing the key | Lookup, insert, delete O(1) average | Keeping items in order |
| **Tree** | Nodes with children, starting from a single root | Search/insert O(log n) when balanced | Degrades to O(n) when unbalanced |
| **Heap** | A tree that keeps the smallest (or largest) item on top | Get min/max O(1), insert/remove O(log n) | Searching for an arbitrary item O(n) |
| **Graph** | Nodes (vertices) connected by edges | Modeling relationships and networks | Traversal is O(V + E) |

## Choosing a Data Structure

- Need to find things by a key quickly? Use a **Hash Table**.
- Need items in a fixed order with fast access by position? Use an **Array**.
- Frequent inserts and removals in the middle? Use a **Linked List**.
- Undo, backtracking, or matching brackets? Use a **Stack**.
- Processing items in arrival order, or breadth-first search? Use a **Queue**.
- Always need the next smallest or largest item? Use a **Heap**.
- Modeling connections such as maps, networks or grids? Use a **Graph**.

Structures are often combined. The [LRU Cache](../../LinkedList/LRUCache/readme.md) pairs a hash table with a doubly linked list so that every operation runs in O(1).

## In This Repo

- [Array](../../Array/)
- [LinkedList](../../LinkedList/readme.md)
- [Graph-Matrix](../../Graph-Matrix/readme.md)
- [String](../../String/)
