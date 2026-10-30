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
        //create a linked list of nodes

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

        Reverse(head);

    }

    public static ListNode Reverse(ListNode head)
    {

        //we need to move
        // 1 > 2 > 3 > 4 > 5
        // 1 < 2 < 3 < 4 < 5
        ListNode currentItem = head;
        ListNode newHead = new ListNode(-1);

        while (currentItem != null)
        {
            ListNode next = currentItem.next;     // 1. Remember where the rest of the old list is
            currentItem.next = newHead.next;      // 2. Point this node at the front of the new list
            newHead.next = currentItem;           // 3. Make this node the new front
            currentItem = next;                   // 4. Move on to the next node of the old list
        }

    }
}
