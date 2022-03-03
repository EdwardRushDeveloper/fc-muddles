using System;
using System.Collections;

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

                Using a combination of a HashTable to be our Cache, 


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
                0 <= key <= 104
                0 <= value <= 105
                At most 2 * 105 calls will be made to get and put.


        */




        public class ListNode{

           public ListNode(int key, int value)
           {
               Key = key;
               Value = value;
           }
           public int Key {get; set;}
           public int Value {get; set;}

           public ListNode Next{get; set;}
           public ListNode Previous {get; set;}

        }


        static void Main(string[] args)
        {
            LRUCache cache = new LRUCache(4);

            cache.put(1, 1);
            cache.put(1 ,1);

            
        }

        public class LRUCache{
            
            ///////////////////////////////////    
            //entry and exit LinkedList Nodes
            //The Head and the Tail will be used to mark the beginning
            //and the ending of the LinkedList
            ///////////////////////////////////


            //the cache to maintain the reference to the objects that are 
            //in the LinkedList.
            Hashtable _cache;
            int _capacity = 0;
            public LRUCache(int capacity)
            {
                _capacity = capacity;
                _cache = new Hashtable(capacity);

            }

        #region Cache Operations Region

        ////////////////////
        //Will return an item out of the cache
        ////////////////////
        public int get(int key)
        {

            return 0;
        }

        ////////////////////
        //Will determine if the capacity has been reached
        //and if it is then it will remove the Least accessed item from the
        //cache before putting the new item.
        ////////////////////
        public void put(int key, int value)
        {


            ListNode current = (ListNode)_cache[key];

            if(current != null)
            {
                current.Value = value;

            } else
            {
                if(_capacity == _cache.Count)
                {
                    //remove the last item 

                }
                current = new ListNode(key, value);
                _cache.Add(current.Key, current);
            }

            


        }


        #endregion



        #region LinkedList Operation Region

        /*
            Removing a List Item from the LinkedList, we do not need to locate the ListItem
            as we have a reference to the object from the Hashtable already. We just simply 
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



        */
        public void AddListItem(ListNode current)
        {


        }




        #endregion


        }



    }
}
