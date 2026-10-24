# LRU Cache

[146. LRU Cache (LeetCode, Medium)](https://leetcode.com/problems/lru-cache/)

[DotNet Fiddle](https://dotnetfiddle.net/jKqpjy)

## What is an LRU Cache?

An **LRU (Least Recently Used) cache** holds a fixed number of items. When it is full and a new item arrives, it evicts the item that has gone unused the longest. The assumption is that something used recently is likely to be used again soon, so the stale items are the ones to throw out. Browser caches, database page buffers and CPU caches all use variations of this idea.

"Used" means **either reading or writing**. Calling `Get(1)` makes key 1 the most recently used item and protects it from the next eviction.

LeetCode requires both `Get` and `Put` to run in **O(1)** time.

## Why it takes two data structures

No single structure can meet the O(1) requirement on its own:

| Need | Dictionary alone | Linked list alone |
|---|---|---|
| Find a key | O(1) ✅ | O(n) walk ❌ |
| Know which item is least recent | no order ❌ | the back of the list ✅ |
| Move an item to "most recent" | no order ❌ | O(1) relink ✅ |

So the solution uses both:

- A **Dictionary** mapping `key → node`, to find any node instantly.
- A **doubly linked list** to keep the usage order:

```
Head <-> [most recent] <-> ... <-> [least recent] <-> Tail
```

## How the code works

### 1. `ListNode`: one cached item

Each node holds `Key`, `Value`, and two links, `Next` and `Previous`. The two links make the list doubly linked: a node knows its neighbor on each side, so it can be unlinked without searching the list. `Key` is stored on the node so that when a node is evicted, the code knows which dictionary entry to delete.

### 2. Setting up the cache

The `LRUCache` class has four fields:

- `_cache`, a `Dictionary<int, ListNode>` that finds any node by its key in O(1)
- `_capacity`, the maximum number of items
- `_head` and `_tail`, two placeholder (sentinel) nodes that never hold real data

The constructor links the two sentinels together, so the empty list looks like this:

```
Head <-> Tail
```

Real nodes always go between them. The node right after `Head` is the **most recently used**, and the node right before `Tail` is the **least recently used**.

### 3. The three list operations

These are the only methods that change links. The cache methods call them.

**`AddListItem`** puts a node right after `Head`:

```
Before:  Head <-> A <-> Tail
After:   Head <-> X <-> A <-> Tail
```

It sets four links: X's `Previous` and `Next`, A's `Previous`, and Head's `Next`.

**`RemoveListItem`** takes a node out by joining its neighbors to each other:

```
Before:  A <-> X <-> B
After:   A <-> B          (nothing points to X anymore)
```

It doesn't search for the node, because the caller already has a reference to it from the dictionary. That is why this is O(1).

**`MoveToFront`** calls remove and then add. It is used whenever an item is used, so the item jumps to the most recent position.

Because of the sentinels, every real node always has a neighbor on both sides. `next.Previous` and `previous.Next` can never be null, so none of these methods need special cases for an empty list, the first node, or the last node.

### 4. The cache operations

**`Get(key)`**

1. Look up the key in the dictionary. If it is not there, return `-1`.
2. If it is found, call `MoveToFront` (reading counts as using) and return the value.

**`Put(key, value)`** has three cases:

1. **The key already exists:** update the value, call `MoveToFront`, and stop. The item count doesn't change, so nothing is evicted.
2. **The key is new and the cache is full:** `_tail.Previous` is the least recently used node. Remove it from the list, then remove its `Key` from the dictionary.
3. **Then, for any new key:** create a node, add it after `Head`, and add it to the dictionary.

The list and the dictionary are always updated together, so they always contain exactly the same items.

**`ToString`** walks from `Head.Next` to `Tail` and prints the items from most to least recent. It is only used for the demo output.

## Walkthrough: LeetCode Example 1 (capacity 2)

`Main` runs this sequence and prints the cache after each step.

| Call | What happens | List (Head → Tail) |
|---|---|---|
| `Put(1,1)` | new key, room left → add front | `1` |
| `Put(2,2)` | new key, room left → add front | `2, 1` |
| `Get(1)` → **1** | found → move 1 to front | `1, 2` |
| `Put(3,3)` | full → evict `Tail.Previous` = **2**, add 3 | `3, 1` |
| `Get(2)` → **-1** | not in dictionary, list unchanged | `3, 1` |
| `Put(4,4)` | full → evict `Tail.Previous` = **1**, add 4 | `4, 3` |
| `Get(1)` → **-1** | evicted earlier | `4, 3` |
| `Get(3)` → **3** | move 3 to front | `3, 4` |
| `Get(4)` → **4** | move 4 to front | `4, 3` |

The key step is `Get(1)` in row 3. Key 1 was added before key 2, but reading it moved it to the front. That made key 2 the least recently used item, so key 2 was evicted when key 3 arrived.

### Program output

```
Put(1, 1)  cache: {1=1}
Put(2, 2)  cache: {2=2, 1=1}
Get(1) = 1  expected 1   cache: {1=1, 2=2}
Put(3, 3)  cache: {3=3, 1=1}  (evicted 2)
Get(2) = -1 expected -1  cache: {3=3, 1=1}
Put(4, 4)  cache: {4=4, 3=3}  (evicted 1)
Get(1) = -1 expected -1  cache: {4=4, 3=3}
Get(3) = 3  expected 3   cache: {3=3, 4=4}
Get(4) = 4  expected 4   cache: {4=4, 3=3}
```

## Complexity

- **Time:** O(1) for both `Get` and `Put`. Each does one dictionary lookup and a fixed number of link changes, with no loops. (`ToString` is O(n), but it is only used for printing.)
- **Space:** O(capacity) for the nodes plus their dictionary entries.

## Submitting to LeetCode

LeetCode expects `LRUCache` as a top-level class with `Get` and `Put`. Copy the `LRUCache` class along with the `ListNode` class it depends on. In this repo both are nested inside `Program`, to match the other problems.

## Links

- [Interview Cake: LRU Cache](https://www.interviewcake.com/concept/java/lru-cache)
- [LRU Cache video walkthrough](https://www.youtube.com/watch?v=NDpwj0VWz1U)
