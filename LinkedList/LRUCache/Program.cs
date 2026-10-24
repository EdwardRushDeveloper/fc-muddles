using System;
using System.Collections.Generic;

namespace LRUCache
{
    class Program
    {
        /*

                146. LRU Cache
                Difficulty : Medium

                Least Recently Used Cache (LRU Cache)

                https://leetcode.com/problems/lru-cache/
                https://www.interviewcake.com/concept/java/lru-cache
                https://www.youtube.com/watch?v=NDpwj0VWz1U

                Using a combination of a Dictionary (HashTable) to be our Cache, and a Doubly LinkedList
                to keep the order in which the items were used.

                    Dictionary:  key -> ListNode. Finds any node in O(1) without walking the list.
                    LinkedList:  most recently used at the front (after Head),
                                 least recently used at the back (before Tail).

                Neither structure can do it alone. The Dictionary has no order, and the LinkedList
                takes O(n) to find a key. Together every operation is O(1).


                Design a data structure that follows the constraints of a Least Recently Used (LRU) cache.
                Implement the LRUCache class:

                LRUCache(int capacity) Initialize the LRU cache with positive size capacity.
                int get(int key) Return the value of the key if the key exists, otherwise return -1.
                void put(int key, int value) Update the value of the key if the key exists. Otherwise,
                add the key-value pair to the cache.
                If the number of keys exceeds the capacity from this operation, evict the least recently used key.
                The functions get and put must each run in O(1) average time complexity.



                Example 1:

                Input
                ["LRUCache", "put", "put", "get", "put", "get", "put", "get", "get", "get"]
                [[2], [1, 1], [2, 2], [1], [3, 3], [2], [4, 4], [1], [3], [4]]
                Output
                [null, null, null, 1, null, -1, null, -1, 3, 4]

                Explanation
                LRUCache lRUCache = new LRUCache(2);
                lRUCache.put(1, 1); // cache is {1=1}
                lRUCache.put(2, 2); // cache is {1=1, 2=2}
                lRUCache.get(1);    // return 1
                lRUCache.put(3, 3); // LRU key was 2, evicts key 2, cache is {1=1, 3=3}
                lRUCache.get(2);    // returns -1 (not found)
                lRUCache.put(4, 4); // LRU key was 1, evicts key 1, cache is {4=4, 3=3}
                lRUCache.get(1);    // return -1 (not found)
                lRUCache.get(3);    // return 3
                lRUCache.get(4);    // return 4


                Constraints:

                1 <= capacity <= 3000
                0 <= key <= 10^4
                0 <= value <= 10^5
                At most 2 * 10^5 calls will be made to get and put.


        */



        /// <summary>
        /// ListNode instance to hold a node in the LinkedList. Each node has a Key and a Value, as well as a Next and Previous pointer to the next and previous nodes in the LinkedList.
        /// </summary>
        public class ListNode
        {
            public ListNode(int key, int value)
            {
                Key = key;
                Value = value;
            }
            //the Key is stored on the node so that when the node is evicted from the
            //back of the LinkedList we know which key to remove from the Dictionary.
            public int Key { get; set; }
            public int Value { get; set; }

            public ListNode Next { get; set; }
            public ListNode Previous { get; set; }

        }


        static void Main(string[] args)
        {
            //LeetCode Example 1
            LRUCache cache = new LRUCache(5);

            cache.Put(1, 1);
            Console.WriteLine($"Put(1, 1)  cache: {cache}");
            cache.Put(2, 2);
            Console.WriteLine($"Put(2, 2)  cache: {cache}");
            Console.WriteLine($"Get(1) = {cache.Get(1)}  expected 1   cache: {cache}");
            cache.Put(3, 3);
            Console.WriteLine($"Put(3, 3)  cache: {cache}  (evicted 2)");
            Console.WriteLine($"Get(2) = {cache.Get(2)} expected -1  cache: {cache}");
            cache.Put(4, 4);
            Console.WriteLine($"Put(4, 4)  cache: {cache}  (evicted 1)");
            Console.WriteLine($"Get(1) = {cache.Get(1)} expected -1  cache: {cache}");
            Console.WriteLine($"Get(3) = {cache.Get(3)}  expected 3   cache: {cache}");
            Console.WriteLine($"Get(4) = {cache.Get(4)}  expected 4   cache: {cache}");
        }


        /// <summary>
        /// LRUCache class implements a Least Recently Used (LRU) cache using a combination of a Dictionary and a Doubly LinkedList. The Dictionary allows for O(1) access to cache items by key, while the Doubly LinkedList maintains the order of usage, with the most recently used items at the front and the least recently used items at the back. The cache has a fixed capacity, and when that capacity is exceeded, the least recently used item is evicted from the cache.
        /// </summary>
        public class LRUCache
        {
            #region Constructor Region
            /// <summary>
            /// Initializes a new instance of the LRUCache class with the specified capacity.
            /// </summary>
            /// <param name="capacity">The maximum number of items that can be stored in the cache.</param>
            public LRUCache(int capacity)
            {
                _capacity = capacity;
                _cache = new Dictionary<int, ListNode>(capacity);

                //makes this a doubly linked list
                _head.Next = _tail;
                _tail.Previous = _head;
            }
            #endregion

            ///////////////////////////////////
            //entry and exit LinkedList Nodes
            //The Head and the Tail will be used to mark the beginning
            //and the ending of the LinkedList. They are sentinel (dummy) nodes that never
            //hold data, so every real node always has a Previous and a Next and we never
            //have to check for null when adding or removing.
            //
            //  Head <-> [most recently used] <-> ... <-> [least recently used] <-> Tail
            ///////////////////////////////////
            readonly ListNode _head = new(0, 0);
            readonly ListNode _tail = new(0, 0);

            //the cache to maintain the reference to the objects that are
            //in the LinkedList.
            readonly Dictionary<int, ListNode> _cache;
            readonly int _capacity;

            #region Cache Operations Region

            ////////////////////
            //Will return an item out of the cache, or -1 if it is not in the cache.
            //Reading an item counts as using it, so it is moved to the front of the LinkedList.
            ////////////////////
            public int Get(int key)
            {
                if (!_cache.TryGetValue(key, out ListNode current))
                {
                    return -1;
                }

                MoveToFront(current);
                return current.Value;
            }

            ////////////////////
            //Will determine if the capacity has been reached
            //and if it is then it will remove the Least accessed item from the
            //cache before putting the new item.
            ////////////////////
            public void Put(int key, int value)
            {
                
                //step 1: see if the item is in the cache
                //if it is then move it to the front by shifting its previous and next points 
                if (_cache.TryGetValue(key, out ListNode current))
                {
                    current.Value = value;
                    MoveToFront(current);
                    return;
                }

                if (_capacity == _cache.Count)
                {
                    //remove the last item, the node just before the Tail is the least recently used
                    ListNode leastRecentlyUsed = _tail.Previous;
                    RemoveListItem(leastRecentlyUsed);
                    _cache.Remove(leastRecentlyUsed.Key);
                }

                current = new ListNode(key, value);
                AddListItem(current);
                _cache.Add(current.Key, current);
            }

            ////////////////////
            //Lists the cache from most recently used to least recently used, for the demo output.
            ////////////////////
            public override string ToString()
            {
                var items = new List<string>();
                for (ListNode node = _head.Next; node != _tail; node = node.Next)
                {
                    items.Add($"{node.Key}={node.Value}");
                }
                return "{" + string.Join(", ", items) + "}";
            }


            #endregion

            #region LinkedList Operation Region

            /*
                Removing a List Item from the LinkedList, we do not need to locate the ListItem
                as we have a reference to the object from the Dictionary already. We just simply
                need to remove the item from the LinkedList and reassign pointers.
            */
            public void RemoveListItem(ListNode current)
            {
                //get the current ListNodes Next and Previous Pointers
                //we are preparing to remove this item from the Item List
                ListNode next = current.Next;
                ListNode previous = current.Previous;

                //reorder the node assignments here to remove the
                //current node from the chain. We reassign
                //the Previous pointer to our Next Item
                //and assign our Next Pointer to our Previous Item.
                //Thus the current ListItem loses any reference in the chain.
                next.Previous = previous;
                previous.Next = next;

                //the current ListItem is now removed.
            }


            /*
                Adding a List Item always puts it at the front of the LinkedList, directly after
                the Head, because a newly added item is the most recently used item.

                    Before:  Head <-> first
                    After:   Head <-> current <-> first
            */
            public void AddListItem(ListNode current)
            {
                ListNode first = _head.Next;

                current.Previous = _head;
                current.Next = first;

                first.Previous = current;
                _head.Next = current;
            }

            /*
                Using an item (Get, or Put on an existing key) makes it the most recently used,
                so it is unlinked from where it is and relinked at the front.
            */
            public void MoveToFront(ListNode current)
            {
                RemoveListItem(current);
                AddListItem(current);
            }

            #endregion


        }



    }
}
