using System;
using System.Collections.Generic;

namespace LRUCacheVersion2
{
    /*
        146. LRU Cache (Version 2)
        Difficulty : Medium

        Did work mainly in Text Editor online to avoid intellisense: https://dotnetfiddle.net/jKqpjy

        https://leetcode.com/problems/lru-cache/

        A cache with a maximum capacity that evicts the Least Recently Used item when full.

            1. Get(key)        - returns the value if the key exists, otherwise -1.
                                 Accessing a key marks it as most recently used.
            2. Put(key, value) - adds the item, or updates it if the key already exists.
                                 If the cache is full, the least recently used item is removed first.

        Approach: a Dictionary paired with a Doubly LinkedList.

            Dictionary:  key -> Node, finds any node in O(1).
            LinkedList:  keeps usage order. Most recently used sits right after Head,
                         least recently used sits right before Tail.

            Head and Tail are sentinel (dummy) nodes, so adding and removing never
            has to check for null neighbours.

            Head <-> [MRU] <-> ... <-> [LRU] <-> Tail

        Time:  O(1) for Get and Put
        Space: O(capacity)
    */
    public class Program
    {
        public static void Main()
        {
            Cache cache = new Cache(3);

            cache.Put(1, 10);
            cache.Put(2, 12);
            cache.Put(3, 13);               // order: 3, 2, 1

            Console.WriteLine($"Get(1) : {cache.Get(1)}");   // 10, order: 1, 3, 2
            Console.WriteLine($"Get(4) : {cache.Get(4)}");   // -1, not in cache
            Console.WriteLine($"Get(3) : {cache.Get(3)}");   // 13, order: 3, 1, 2
            Console.WriteLine($"Get(2) : {cache.Get(2)}");   // 12, order: 2, 3, 1

            cache.Put(4, 14);               // cache is full, evicts key 1. order: 4, 2, 3

            Console.WriteLine("Cache (most to least recently used):");
            cache.PrintLinkedList();
        }
    }

    public class Node
    {
        public Node(int key, int value)
        {
            Key = key;
            Value = value;
        }

        // Key is stored on the node so an evicted node can also be removed from the Dictionary
        public int Key { get; }
        public int Value { get; set; }

        public Node Previous { get; set; }
        public Node Next { get; set; }
    }

    public class Cache
    {
        private readonly int _capacity;
        private readonly Dictionary<int, Node> _dictionary;

        // sentinel nodes, never hold real data
        private readonly Node _head;
        private readonly Node _tail;

        public Cache(int capacity)
        {
            _capacity = capacity;
            _dictionary = new Dictionary<int, Node>(capacity);

            _head = new Node(0, 0);
            _tail = new Node(0, 0);
            _head.Next = _tail;
            _tail.Previous = _head;
        }

        // returns the value for the key, or -1 if it is not in the cache
        public int Get(int key)
        {
            if (!_dictionary.TryGetValue(key, out Node node))
            {
                return -1;
            }

            // the item was accessed, so it becomes the most recently used
            MoveToFirst(node);
            return node.Value;
        }

        // adds or updates an item, evicting the least recently used item if the cache is full
        public void Put(int key, int value)
        {
            if (_dictionary.TryGetValue(key, out Node existing))
            {
                existing.Value = value;
                MoveToFirst(existing);
                return;
            }

            if (_dictionary.Count == _capacity)
            {
                // the least recently used item is always right before Tail
                Node last = _tail.Previous;
                RemoveNode(last);
                _dictionary.Remove(last.Key);
            }

            Node node = new Node(key, value);
            AddNode(node);
            _dictionary.Add(key, node);
        }

        // prints the items from most to least recently used, skipping the sentinels
        public void PrintLinkedList()
        {
            for (Node current = _head.Next; current != _tail; current = current.Next)
            {
                Console.WriteLine($"Key: {current.Key}, Value: {current.Value}");
            }
        }

        #region Linked List Helpers

        // unlinks the node from its neighbours
        private void RemoveNode(Node node)
        {
            node.Previous.Next = node.Next;
            node.Next.Previous = node.Previous;
        }

        // inserts the node right after Head, making it the most recently used
        private void AddNode(Node node)
        {
            node.Previous = _head;
            node.Next = _head.Next;
            _head.Next.Previous = node;
            _head.Next = node;
        }

        private void MoveToFirst(Node node)
        {
            RemoveNode(node);
            AddNode(node);
        }

        #endregion
    }
}
