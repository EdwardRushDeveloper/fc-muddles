namespace BinaryTreeZigzagLevelOrderTraversal;

class Program
{
    /*

            103. Binary Tree Zigzag Level Order Traversal
            Medium
            Trees
                    https://leetcode.com/problems/binary-tree-zigzag-level-order-traversal/

            Given the root of a binary tree, return the zigzag level order traversal of its nodes' values.
            (i.e., from left to right, then right to left for the next level and alternate between).

                Example 1:

                            3
                          /   \
                         9     20
                              /  \
                             15   7

                Input: root = [3,9,20,null,null,15,7]
                Output: [[3],[20,9],[15,7]]

                Example 2:

                Input: root = [1]
                Output: [[1]]

                Example 3:

                Input: root = []
                Output: []

                Constraints:

                The number of nodes in the tree is in the range [0, 2000].
                -100 <= Node.val <= 100

                LeetCode definition for a binary tree node:

                    public class TreeNode {
                        public int val;
                        public TreeNode left;
                        public TreeNode right;
                        public TreeNode(int val=0, TreeNode left=null, TreeNode right=null) {
                            this.val = val;
                            this.left = left;
                            this.right = right;
                        }
                    }

                LeetCode signature:

                    public IList<IList<int>> ZigzagLevelOrder(TreeNode root)

    */

    public class TreeNode
    {
        public int val;
        //nullable because a node may not have a left or right child
        public TreeNode? left;
        public TreeNode? right;
        public TreeNode(int val = 0, TreeNode? left = null, TreeNode? right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }

    //////////////////
    // Loads the test data for Example 1 and returns the root of the tree.
    //
    //  root = [3,9,20,null,null,15,7]
    //
    //            3
    //          /   \
    //         9     20
    //              /  \
    //             15   7
    //////////////////
    public static TreeNode LoadTestTree()
    {
        //level 0
        TreeNode root = new TreeNode(3);

        //level 1
        root.left = new TreeNode(9);
        root.right = new TreeNode(20);

        //level 2, 9 has no children (null, null)
        root.right.left = new TreeNode(15);
        root.right.right = new TreeNode(7);

        return root;
    }

    static void Main(string[] args)
    {
        TreeNode root = LoadTestTree();

        IList<IList<int>> result = ZigzagLevelOrder(root);

        //loop through each node
    }

    //////////////////
    // Breadth First Search (BFS), one level at a time.
    //
    // The queue always holds the nodes in normal left to right order.
    // Only the way we WRITE a level into the result changes:
    //   even levels (0, 2, 4...) -> written left to right, straight into the level list
    //   odd levels  (1, 3, 5...) -> pushed onto a stack, then popped off, which reverses them
    //
    // Time  O(N) : every node goes into the queue once and comes out once
    // Space O(N) : the queue can hold a whole level, which is up to about N/2 nodes
    //////////////////
    public static IList<IList<int>> ZigzagLevelOrder(TreeNode root) {

        //returning list
        // [[3],[20,9],[15,7]] example
        IList<IList<int>> result = new List<IList<int>>();


        //Queue to hold our values FIFO
        //initialize with the root node for the queue
        //NOTE: if root is null (Example 3) this queues a null, and current.val below
        //will throw. A guard of "if (root == null) return result;" before this fixes it.
        Queue<TreeNode> queue = new Queue<TreeNode>();
        queue.Enqueue(root);

        //false = this level is written left to right
        //true  = this level is written right to left
        //level 0 (the root) is left to right, so we start at false
        bool doswitch = false;

        // keep looping while there are nodes left to visit in our queue
        // each pass of this while loop handles exactly ONE level of the tree
        while(queue.Count > 0)
        {
            //take a snapshot of how many nodes are in the queue right now.
            //that is exactly the number of nodes on this level.
            //we can't use queue.Count in the for loop, because the children we
            //enqueue below would make it grow and we'd run into the next level.
            int queueSize = queue.Count;

            //use the stack so the values are reversed off (last in, first out)
            Stack<int> stack = new Stack<int>();
            //use the level list so the items can be added forward
            List<int> level = new List<int>();

            for(int i = 0; i < queueSize; i++)
            {

                TreeNode current = queue.Dequeue();

                if(doswitch)
                {
                    //right to left level: save on the stack, reversed later
                    stack.Push(current.val);
                }
                else
                {
                    //left to right level: add directly in the order we see them
                    level.Add(current.val);
                }

                //queue up the next level. Always left child first, then right,
                //so the queue stays in left to right order no matter the direction
                if(current.left != null) queue.Enqueue(current.left);
                if(current.right!= null) queue.Enqueue(current.right);

            }

            //flip the direction for the next level
            doswitch = !doswitch;

            //on a right to left level, popping the stack hands the values back
            //in reverse order. e.g. pushed 9, 20 -> popped 20, 9
            //on a left to right level the stack is empty and this loop does nothing
            while(stack.Count > 0)
            {
                level.Add(stack.Pop());
            }

            //this level is finished, add it to the result
            result.Add(level);
        }
        return result;
    }
}
