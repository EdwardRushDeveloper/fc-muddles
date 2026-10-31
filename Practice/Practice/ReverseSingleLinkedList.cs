using System;

namespace Practice;

public class ReverseSingleLinkedList
{

    //Definition for singly-linked list.
    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int val = 0, ListNode next = null)
        {
            this.val = val;
            this.next = next;
        }
    }


    public static void Execute()
    {
        Console.WriteLine("******************** Reverse Linked List");

        //builds the linked list collection
        int[] values = [1, 2, 3, 4, 5];
        ListNode head = null;
        ListNode current = null;
        for (int i = 0; i < values.Length; i++)
        {
            if (head == null)
            {
                head = new ListNode(values[i]);
                current = head;
            }
            else
            {
                current.next = new ListNode(values[i]);
                current = current.next;
            }
        }
        ListNode result = Reverse(head);

        ListNode loopResult = result;

        while(loopResult != null)
        {
            Console.WriteLine($"Current Node Value: {loopResult.val}");
            loopResult = loopResult.next;
        }

    }

    public static ListNode Reverse(ListNode head)
    {

        //we need to move
        // 1 > 2 > 3 > 4 > 5
        // 1 < 2 < 3 < 4 < 5

             ListNode one = head;
             ListNode reorder = new ListNode(-1);
    
             // we work with one node at a time, and work so that we dont lose our pointers
             //eventually rotating rorder node to be a dummy head
             while(one != null)
             {
                 //grab two from one  1 > 2 : get a reference to 2 - this node becomes disconnected at this point
                 ListNode two       = one.next;
                 //replace twos spot with reorder  1 > ro : set 1 to point to null
                 one.next           = reorder.next;
                 //now use ro to point to one  ro > 1
                 reorder.next       = one;
                 //THIS IS THE POINT OF BEING DISCONNECTED ON THE ITERATION
                 // now set one to the disconnected two, and iterate again  (This 1 < ro  disconnected from this :   2 > 3 > 4 > 5)
                 //at this stage our Linked List looks like this until our work is done.  
                 // 2 becomes our new one
                 one                = two;

                 //in the end the iteration will turn into
                 // 1 < 2 < 3 < 4 < 5 < ro
                 // as we rotate ro over each the nodes
             }
            //we will disgard ro here, by ony returning the pointer to 5
            return reorder.next;

    }
}
