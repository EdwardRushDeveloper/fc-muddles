using System;

namespace ReverseLinkedList
{
    /*
        153. Reverse Linked List
        Difficulty : Easy
        Recursion, and Loop  examples in this code.
        The Recursion Example will have to be uncommented and ran. The Loop operation is the default operation.

        https://leetcode.com/problems/reverse-linked-list/
        
        https://www.geeksforgeeks.org/reverse-a-linked-list/
        
        Good Resource
        https://www.youtube.com/watch?v=Z--S2-RiS44

        Given the head of a singly linked list, reverse the list, and return the reversed list.

            Example 1:

            Input: head = [1,2,3,4,5]
            Output: [5,4,3,2,1]
            Example 2:


            Input: head = [1,2]
            Output: [2,1]
            Example 3:

            Input: head = []
            Output: []
            

            Constraints:

            The number of nodes in the list is the range [0, 5000].
            -5000 <= Node.val <= 5000
            

            Follow up: A linked list can be reversed either iteratively or recursively. Could you implement both?


*/
    class Program
    {
        //Singly LinkedList Node definition.
        public class ListNode
        {
            public int Value { get; set; }

            public ListNode Next { get; set; }

            public ListNode(int value)
            {
                Value = value;
            }
            public ListNode(){}
        }

        static void Main(string[] args)
        {


            ListNode head = null;
            ListNode current = null;

            //loads the items for the linked list 
            foreach(int i in new int[]{1,2,3,4,5})
            {
                //if the head is null then create the head
                if(head == null)
                {
                    head = new ListNode(i);
                    current = head;
                } else //else just add the items to the LinkedList as tail items.
                {
                    current.Next = new ListNode(i);
                    current = current.Next;
                }
            }

            //Uncomment the Recursive Method to use Recursion.
            //recursive method of the Linked List Reversal
            //ListNode result = ReverseLinkedListRecursive(head);

            //reverse the Linked using a Looping operation instead.
            ListNode result = ReverseLinkedListLoop(head);
        }


        /*

            Looping solution to the Problem
            The idea about this problem is to loop through each of the 
            LinkedList ListNodes, add the ListNodes from the LinkedList ListNode to the
            head of the Reorder Head ListNode we will create. This will
            reorder the LinkedList in reverse order.

        */
        static ListNode ReverseLinkedListLoop(ListNode currentNode)
        {

             //initialize our current node to the head coming into
             //the method call.
             ListNode currentItem = currentNode;

             //we will create a dummy node to help us to reorder the linked list
             //we will be adding to this at the head ListNode as we move through the linked list
             //this will reverse the order of the linked list
             //Note that the first ListNode in this Node is a dummy ListNode and not the true head
             //of the linked list. We will correct this when we return only reorder.Next at the end.
             ListNode reorder = new ListNode(-1);

             /*
                We are looking to reverse the linked lists for ListNodes
                    1 - 2 - 3 - 4 - 5
                        to 
                    5 - 4 - 3 - 2 -1
             */
             //loop through the ListNodes and put them at the front of the dummy  node
             while(currentItem != null)
             {
                 
                 //1. 
                 //get a reference to what current ListNode has coming up next
                 //so we don't lose our reference to the next ListNode.
                 //we are using this to iterate through our linked list
                 ListNode next = currentItem.Next;

                 //now set our currentItem Next node to the dummy nodes 
                 //next ListNode. on the first iteration this will be NULL
                 //
                 /*
                    Example: 
                        First Iteration

                        Remember our Next for use in our iteration
                        next = current(Node(1)).Next(Node(2))

                        Add Node(1) to head of Reorder
                        current(Node(1)).Next =  Reorder.Next(Null)
                        //add the item to the head as a Next Item
                        //remember that the original Reorder Beginning Node will be
                        //disposed of on the return of this method.
                        Reorder.Next = Node(1)


                        current = next;

                 */

                 //assign our curentItems Next to the Reorder Next value in preparation
                 //to move the ListNode to another position. Remember we are adding the ListNode to 
                 //the head of the list. We have maintained the currentItem.Next ListNode in a variable above.
                 currentItem.Next   = reorder.Next;
                 //now assign the current ListNode to the Head
                 reorder.Next       = currentItem;
                 //now assign our next ListNode to the currentItem for the next ListNode in the loop that 
                 //we will add to the head of the LinkedList pointer.
                 currentItem        = next;

             }

            //now return the Next node as the new Head Node and the LinkedList will be reordered.
            return reorder.Next;
        }

        /*
            Recursive solution to the Problem.

            We will wind into the LinkedList ListNodes and as we wind out, at each 
            stack context we will reorder the ListNode.

        */
        static ListNode ReverseLinkedListRecursive(ListNode currentNode)
        {
            if(currentNode == null ||currentNode.Next == null )
            {
                return currentNode;
            }

            //remember in the context of the current stack call, which Next node 
            //was passed in, we will use this to reassign our order
            ListNode rememberMe = currentNode;

            //this operation will always return the LAST node in the stack because
            //when we wind down all the way to the end of the linked list, the return 
            //pushes up the stack the reference to very bottom node. So we do a little trick
            currentNode = ReverseLinkedListRecursive(currentNode.Next);

            //Now with our current context node, reassign our order 
            rememberMe.Next.Next = rememberMe;
            //this breaks our linked list here, but look at line above, when the context
            //is accessed in the reverse stack, the link will be fixed.
            /*
                Example: When we wind all the way into the last node (5), and return that node
                Node(4) will be the rememberMe Node. So we assign rememberMe(4).Next(5).Next = rememberMe(4)
                This will make Node(5) now ahead of Node(4) because Node(4) is now the next Node after Node(5).
                We then blank out Node(4).Next to null. This breaks the LinkedList because Node(4) no longer points
                to Node(5). But in the next stack level context, the same operation will happen for Node(3) and this
                operation will occur all the way up the stack until we are done at Node(1).
            */
            rememberMe.Next = null;

            System.Console.WriteLine(rememberMe.Value);

            return currentNode;

        }
    }
}
