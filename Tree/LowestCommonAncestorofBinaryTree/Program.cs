namespace LowestCommonAncestorofBinaryTree;

class Program
{
    /*

            236. Lowest Common Ancestor of a Binary Tree
            Medium
            Trees
                    https://leetcode.com/problems/lowest-common-ancestor-of-a-binary-tree/

            Given a binary tree, find the lowest common ancestor (LCA) of two given nodes in the tree.

            According to the definition of LCA on Wikipedia: "The lowest common ancestor is defined
            between two nodes p and q as the lowest node in T that has both p and q as descendants
            (where we allow a node to be a descendant of itself)."

                Example 1:

                            3
                          /   \
                         5     1
                        / \   / \
                       6   2 0   8
                          / \
                         7   4

                Input: root = [3,5,1,6,2,0,8,null,null,7,4], p = 5, q = 1
                Output: 3
                Explanation: The LCA of nodes 5 and 1 is 3.

                Example 2:

                Input: root = [3,5,1,6,2,0,8,null,null,7,4], p = 5, q = 4
                Output: 5
                Explanation: The LCA of nodes 5 and 4 is 5, since a node can be a descendant of
                itself according to the LCA definition.

                Example 3:

                            1
                           /
                          2

                Input: root = [1,2], p = 1, q = 2
                Output: 1

                Constraints:

                The number of nodes in the tree is in the range [2, 10^5].
                -10^9 <= Node.val <= 10^9
                All Node.val are unique.
                p != q
                p and q will exist in the tree.

                LeetCode definition for a binary tree node:

                    public class TreeNode {
                        public int val;
                        public TreeNode left;
                        public TreeNode right;
                        public TreeNode(int x) { val = x; }
                    }

                LeetCode signature:

                    public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)

    */


    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int x) { val = x; }
    }


    //////////////////
    // Loads the test data for Example 1 and returns the root of the tree.
    //
    //  root = [3,5,1,6,2,0,8,null,null,7,4]
    //
    //            3
    //          /   \
    //         5     1
    //        / \   / \
    //       6   2 0   8
    //          / \
    //         7   4
    //////////////////
    public static TreeNode LoadTestTree()
    {
        //level 0
        TreeNode root = new TreeNode(3);

        //level 1
        root.left = new TreeNode(5);
        root.right = new TreeNode(1);

        //level 2
        root.left.left = new TreeNode(6);
        root.left.right = new TreeNode(2);
        root.right.left = new TreeNode(0);
        root.right.right = new TreeNode(8);

        //level 3, 6 has no children (null, null)
        root.left.right.left = new TreeNode(7);
        root.left.right.right = new TreeNode(4);

        return root;
    }


    static void Main(string[] args)
    {

        TreeNode root = LoadTestTree();

        //Example tree searches
        TreeNode found = FindNode(root, 4);
        Console.WriteLine($"FindNode(root, 4) = {found?.val}, its left child = {found?.left?.val.ToString() ?? "null"}");

        TreeNode missing = FindNode(root, 9);
        Console.WriteLine($"FindNode(root, 9) = {missing?.val.ToString() ?? "null"}");

        TreeNode lowest = LowestCommonAncestor(root, FindNode(root, 5), FindNode(root, 4));

        Console.WriteLine($"{lowest.val}");

    }



    /// <summary>
    /// DFS Search - Very simple node search to traverse and find the node with the value
    ///
    /// Pre-order depth first search: check the current node, then search the whole left
    /// subtree, then the right subtree. The search stops as soon as the value is found.
    ///
    /// Example search for 4 in the test tree:
    ///
    ///             3
    ///           /   \
    ///          5     1
    ///         / \   / \
    ///        6   2 0   8
    ///           / \
    ///          7   4
    ///
    ///   FindNode(3)  not 4, search left
    ///     FindNode(5)  not 4, search left
    ///       FindNode(6)  not 4, search left
    ///         FindNode(null)  returns null
    ///       6 left was null, search right
    ///         FindNode(null)  returns null
    ///       6 returns null
    ///     5 left was null, search right
    ///       FindNode(2)  not 4, search left
    ///         FindNode(7)  not 4, both children null, returns null
    ///       2 left was null, search right
    ///         FindNode(4)  found, returns node 4
    ///       2 returns node 4
    ///     5 returns node 4
    ///   3 left found node 4, so the right side (1, 0, 8) is never searched
    ///   returns node 4
    ///
    ///   Visit order: 3, 5, 6, 2, 7, 4
    ///
    /// Searching for a value that isn't in the tree (e.g. 9) visits all 9 nodes and returns null.
    ///
    /// Time O(N), every node may be visited. Space O(H), one stack frame per level of the tree.
    /// </summary>
    /// <param name="node">The node to start searching from. Pass the root to search the whole tree.</param>
    /// <param name="val">The value to look for.</param>
    /// <returns>The node holding val, or null if no node holds it.</returns>
    public static TreeNode FindNode(TreeNode node, int val)
    {
        //base case: we've gone past a leaf, there is no node here to check
        if (node == null) return null;

        //base case: this node holds the value, return it back up the call chain
        if (node.val == val)
        {
            return node;
        }

        //search the left subtree first. ?? only runs the right side search when the left
        //search returned null, so once the value is found nothing else is visited.
        //Whichever side found it (or null if neither did) is passed back to the caller.
        TreeNode returnValue = FindNode(node.left, val) ?? FindNode(node.right, val);

        return returnValue;

    }

    /// <summary>
    /// LeetCode 236 solution. Matches the LeetCode signature LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q).
    ///
    /// Post-order depth first search: search the left subtree, search the right subtree, then
    /// decide at the current node using what both sides found.
    ///
    /// Each call answers "what did I find in this subtree?" and returns one of:
    ///   null      neither p nor q is in this subtree
    ///   p or q    one target is in this subtree (if the other is below it, it is also the LCA)
    ///   the LCA   both targets were found lower down, the answer is passed up unchanged
    ///
    /// Example 1: p = 5, q = 1
    ///
    ///             3
    ///           /   \
    ///          5     1
    ///         / \   / \
    ///        6   2 0   8
    ///           / \
    ///          7   4
    ///
    ///   LCA(3)  not a target, search both sides
    ///     LCA(5)  is p, return 5          (6, 2, 7, 4 are never visited)
    ///     LCA(1)  is q, return 1          (0, 8 are never visited)
    ///   3: left = 5, right = 1, both found, return 3
    ///
    /// Example 2: p = 5, q = 4
    ///
    ///   LCA(3)  not a target, search both sides
    ///     LCA(5)  is p, return 5          (never looks for 4 below it)
    ///     LCA(1)  not a target
    ///       LCA(0)  both children null, return null
    ///       LCA(8)  both children null, return null
    ///     1: left = null, right = null, return null
    ///   3: left = 5, right = null, return left ?? right = 5
    ///
    /// Example 2 works because the problem guarantees p and q both exist in the tree. If q is not
    /// found anywhere outside of p, it must be below p, which makes p the LCA. If a target could
    /// be missing, this would return the wrong node.
    ///
    /// Time O(N), each node is visited at most once.
    /// Space O(H), one stack frame per level of the tree. O(log N) balanced, O(N) lopsided.
    /// </summary>
    /// <param name="root">The root of the subtree to search. Pass the tree's root to search the whole tree.</param>
    /// <param name="p">The first target node. Must be a node in the tree, not just a matching value.</param>
    /// <param name="q">The second target node. Must be a node in the tree, not just a matching value.</param>
    /// <returns>The lowest node that has both p and q as descendants (a node is a descendant of itself).</returns>
    public static TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
    {
        //base case: root == null, we've gone past a leaf, nothing here.
        //base case: root == p or q, we're standing on a target so return it without searching
        //below it. If the other target is below, this node is the LCA. If the other target is
        //somewhere else, a node higher up will see both sides and become the LCA.
        //== compares references, which is why p and q must be the real nodes in the tree.
        if (root == null || root == p || root == q)
            return root;                        // base case: empty, or found a target

        //search both subtrees. Unlike FindNode there is no ?? shortcut here, this node needs
        //the answer from both sides to decide if it is the LCA.
        TreeNode left  = LowestCommonAncestor(root.left, p, q);
        TreeNode right = LowestCommonAncestor(root.right, p, q);

        //one target on the left and the other on the right, the paths to p and q split at
        //this node. No lower node can be an ancestor of both, so this node is the LCA.
        if (left != null && right != null)
            return root;                        // p and q split here, so this is the LCA

        //at most one side found something. Pass it up (a target or an LCA already found
        //lower down), or null if neither side found anything.
        return left ?? right;

    }
}
