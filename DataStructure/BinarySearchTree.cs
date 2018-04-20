using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

namespace DataStructure
{
    //A binary search tree relies on the property that keys that are less than the parent are found in the left subtree, and keys that are greater than the parent are found in the right subtree. 

    //Notice that the property holds for each parent and child. All of the keys in the left subtree are less than the key in the root. All of the keys in the right subtree are greater than the root.

    public class BinarySearchTree<T, U> where T : IComparable
    {
        private Node<T, U> _root;
        private int _size;

        public BinarySearchTree()
        {
            _root = null;
            _size = 0;
        }

        public int Length => _size;

        #region Put and Set
        public void Put(T key, U value)
        {
            if (_root != null) _Put(key, value, _root);
            else _root = new Node<T, U>(key, value);
            _size++;
        }

        private void _Put(T key, U value, Node<T, U> currentNode, bool changeable = false)
        {
            //if key less than currentNode, _put to left
            if (key.CompareTo(currentNode.Key) < 0)
            {
                if (currentNode.HasLeftChild()) _Put(key, value, currentNode.LeftChild);
                else currentNode.LeftChild = new Node<T, U>(key, value, parent: currentNode);
            }
            else if (key.CompareTo(currentNode.Key) > 0) //_put to right
            {
                if (currentNode.HasRightChild()) _Put(key, value, currentNode.RightChild);
                else currentNode.RightChild = new Node<T, U>(key, value, parent: currentNode);
            }
            else //equals to current key
            {
                if (!changeable) throw new Exception("The Id already exists!");
                else currentNode.Payload = value;
            }
        }

        public void SetItem(T key, U value)
        {
            if (Contains(key)) _Put(key, value, _root, changeable: true);
            else throw new Exception("The key of " + key + " doesn't exist!");
        }
        #endregion

        #region Get and Contains

        public U Get(T key)
        {
            var result = _Get(key, _root);

            //if find the result, then return it; otherwise return null
            return result != null ? result.Payload : default(U);
        }

        public bool Contains(T key)
        {
            return _Get(key, _root) != null;
        }

        private Node<T, U> _Get(T key, Node<T, U> currentNode)
        {
            if (currentNode == null) return null;// base case
            if (key.CompareTo(currentNode.Key) == 0) return currentNode; // base case

            //tail recursive case
            //if greater, go to right
            if (key.CompareTo(currentNode.Key) > 0) return _Get(key, _root.RightChild);
            // go to left
            return _Get(key, currentNode.LeftChild);
        }

        #endregion

        #region Delete
        /// <summary>
        /// if deleted successfully return 1, otherwise 0
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int Delete(T key)
        {
            if (_size == 0) return 0; //if tree is empty
            if (_size == 1 && _root.Key.CompareTo(key) == 0) //if there is only root and key equales to root's key
            {
                _root = null;
                _size = 0;
                return 1;
            }
            //if size > 1, find the node to be removed
            var nodeToRemove = _Get(key, _root);
            if (nodeToRemove != null)
            {
                _Remove(nodeToRemove);
                _size--;
            }
            //if not found
            return 0;
        }

        private void _Remove(Node<T, U> currentNode)
        {
            //is leaf
            if (currentNode.IsLeaf())
            {
                //is left child
                if (currentNode.IsLeftChild()) currentNode.Parent.LeftChild = null;
                //is right child
                else currentNode.Parent.RightChild = null;
            }
            //is not leaf
            else
            {
                //is left child
                if (currentNode.IsLeftChild())
                {
                    //has only left child
                    if (currentNode.HasLeftChild() && !currentNode.HasRightChild())
                    {
                        currentNode.Parent.LeftChild = currentNode.LeftChild;
                        currentNode.LeftChild.Parent = currentNode.Parent;
                    }
                    //has only right child
                    if (!currentNode.HasLeftChild() && currentNode.HasRightChild())
                    {
                        currentNode.Parent.LeftChild = currentNode.RightChild;
                        currentNode.RightChild.Parent = currentNode.Parent;
                    }
                    //has both children
                    else
                    {

                    }

                }
                //is right child
                else
                {

                }
            }
        }

        private Node<T, U> FindSuccessor(Node<T, U> currentNode)
        {
            Node<T, U> succ = null;
            //if curr has rightChild(right subtree), the succ is min of right sub tree 
            if (currentNode.HasRightChild()) succ = MinOfRightSubTree(currentNode);

            //if curr doesn't have right sub tree
            else
            {
                //if has parent and is left Child, the succ is its parent.
                if (currentNode.IsLeftChild()) succ = currentNode.Parent;
                //if curr is right child. the succ is the succ of its parent excluding itself
                else
                {
                    currentNode.Parent.RightChild = null;
                    succ = FindSuccessor(currentNode.Parent);
                    currentNode.Parent.RightChild = currentNode;
                }
            }

            return succ;
        }

        private Node<T, U> MinOfRightSubTree(Node<T, U> currentNode)
        {
            var min = currentNode.RightChild;
            while (min.HasLeftChild())
            {
                min = min.LeftChild;
            }
            return min;
        }

        public void SpliceOut()
        {

        }

        #endregion

        #region Iterator

        #region DFS -- Depth First Search
        /// <summary>
        /// Inorder traversal the tree, left -> root -> right
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Node<T, U>> InOrder(Node<T, U> currentNode)
        {
            if (currentNode != null)
            {
                //recursively call left child
                if (currentNode.HasLeftChild())
                {
                    foreach (var node in InOrder(currentNode.LeftChild))
                    {
                        yield return node;
                    }
                }

                //parent
                yield return currentNode;

                //recursively call right child
                if (currentNode.HasRightChild())
                {
                    foreach (var node in InOrder(currentNode.RightChild))
                    {
                        yield return node;
                    }
                }
            }
        }

        /// <summary>
        /// PreOrder traversal the tree, root -> left -> right
        /// </summary>
        /// <param name="currentNode"></param>
        /// <returns></returns>
        public IEnumerable<Node<T, U>> PreOrder(Node<T, U> currentNode)
        {
            if (currentNode != null)
            {
                //parent
                yield return currentNode;

                //recursively call left child
                if (currentNode.HasLeftChild())
                {
                    foreach (var node in PreOrder(currentNode.LeftChild))
                    {
                        yield return node;
                    }
                }

                //recursively call right child
                if (currentNode.HasRightChild())
                {
                    foreach (var node in PreOrder(currentNode.RightChild))
                    {
                        yield return node;
                    }
                }
            }
        }

        /// <summary>
        /// PostOrder traversal the tree, left -> right -> root
        /// </summary>
        /// <param name="currentNode"></param>
        /// <returns></returns>
        public IEnumerable<Node<T, U>> PostOrder(Node<T, U> currentNode)
        {
            if (currentNode != null)
            {
                //recursively call left child
                if (currentNode.HasLeftChild())
                {
                    foreach (var node in PostOrder(currentNode.LeftChild))
                    {
                        yield return node;
                    }
                }

                //recursively call right child
                if (currentNode.HasRightChild())
                {
                    foreach (var node in PostOrder(currentNode.RightChild))
                    {
                        yield return node;
                    }
                }
                //parent
                yield return currentNode;
            }
        }
        #endregion

        #region BFS -- Breadth First Search
        //BFS, three methods:
        // 2 queues
        // 1 queue + dummy node
        // 1 queue (best)

        //Maintain one queue
        public IEnumerable<Node<T, U>> BFS()
        {
            var queue = new System.Collections.Generic.Queue<Node<T, U>>();
            var currentNode = _root;
            queue.Enqueue(currentNode);
            while (queue.Count != 0)
            {
                currentNode = queue.Dequeue();
                yield return currentNode;
                if (currentNode.HasLeftChild()) queue.Enqueue(currentNode.LeftChild);
                if (currentNode.HasRightChild()) queue.Enqueue(currentNode.RightChild);
            }
        }

        //Maintain two stack
        public IEnumerable<Node<T, U>> BFSZigZag()
        {
            var stack1 = new System.Collections.Generic.Stack<Node<T, U>>();
            var stack2 = new System.Collections.Generic.Stack<Node<T, U>>();
            var currentNode = _root;
            int level = 1;
            var currentStack = stack1;
            stack1.Push(currentNode);
            while (currentStack.Count != 0)
            {
                currentNode = currentStack.Pop();
                yield return currentNode;

                if (level % 2 == 1) //even row, Push left first to the stack.
                {
                    if (currentNode.HasLeftChild()) stack2.Push(currentNode.LeftChild);
                    if (currentNode.HasRightChild()) stack2.Push(currentNode.RightChild);
                }
                else
                {
                    if (currentNode.HasRightChild()) stack1.Push(currentNode.RightChild);
                    if (currentNode.HasLeftChild()) stack1.Push(currentNode.LeftChild);
                }
                if (currentStack.Count == 0) //current level is over, then switch to the next level
                {
                    level++;
                    currentStack = currentStack == stack1 ? stack2 : stack1;
                }
            }
        }



        #endregion

        public IEnumerator<T> GetEnumerator()
        {
            return BFSZigZag().Select(node => node.Key).GetEnumerator();
        }



        #endregion

        #region _PrintByLevel

        public static void PrintByLevel(List<Node<T, U>> nodes)
        {
            _PrintByLevel(nodes);
        }

        public void PrintByLevel()
        {
            _PrintByLevel(new List<Node<T, U>>() { _root });
        }

        private static void _PrintByLevel(List<Node<T, U>> inputList)
        {
            if (inputList.Count == 0) return; // base case

            //tail recursive case
            string str = "";
            for (int i = 0; i < inputList.Count; i++)
            {
                str += (i != inputList.Count) ? (inputList[i].Key.ToString() + " ") : (inputList[i].Key.ToString());
            }
            Console.WriteLine(str);

            List<Node<T, U>> newList = new List<Node<T, U>>();
            foreach (var root in inputList)
            {
                if (root.HasLeftChild()) newList.Add(root.LeftChild);
                if (root.HasRightChild()) newList.Add(root.RightChild);
            }
            _PrintByLevel(newList);
        }

        #endregion

        #region Max Depth

        public int MaxDepth()
        {
            return MaxDepthHelper(_root);
        }
        private int MaxDepthHelper(Node<T, U> node)
        {

            if (node == null) return 0;

            int left = MaxDepthHelper(node.LeftChild);
            int right = MaxDepthHelper(node.RightChild);
            return Math.Max(left, right) + 1;
        }


        #endregion

        #region Maximum Path Sum
        /// <summary>
        /// Return the maximum path sum from root node.
        /// </summary>
        /// <returns></returns>
        public int MaximumPathSumFromRoot()
        {
            return MaximumPathSumFromRootHelper(_root);
        }

        private int MaximumPathSumFromRootHelper(Node<T, U> node)
        {
            if (node == null) return 0;
            return Math.Max(
                            Math.Max(
                                MaximumPathSumFromRootHelper(node.RightChild),
                                MaximumPathSumFromRootHelper(node.LeftChild)),
                            0)  //consider negative integer
                + int.Parse(node.Key.ToString());
        }

        /// <summary>
        /// The path may start and end at any node in the tree. (negative values are involved)
        /// </summary>
        /// <returns></returns>
        public int MaximumPathSumFromAnyNode()
        {
            return MaximumPathSumFromAnyNodeHelper(_root);
        }

        private int MaximumPathSumFromAnyNodeHelper(Node<T, U> node)
        {
            //Divide
            int nodeValue = Int32.Parse(_root.Key.ToString());
            int leftPathNotViaRoot = node.LeftChild == null ? Int32.Parse(node.Key.ToString()) : MaximumPathSumFromAnyNodeHelper(node.LeftChild);
            int rightPathNotViaRoot = node.RightChild == null ? Int32.Parse(node.Key.ToString()) : MaximumPathSumFromAnyNodeHelper(node.RightChild);
            int pathThroughRoot = Math.Max(0, MaximumPathSumFromRootHelper(node.LeftChild)) //if leftMax is negative, plus 0
                                + nodeValue
                                + Math.Max(0, MaximumPathSumFromRootHelper(node.RightChild)); //if rightMax is negative, plus 0

            //Conquer
            return Math.Max(Math.Max(leftPathNotViaRoot, rightPathNotViaRoot),  //path not via root
                            pathThroughRoot); //path via
        }


        #endregion

        #region Trim the tree by min and max
        /// <summary>
        /// Trim the tree such that all the numbers in the new tree are between min and max(inclusive). The resulting tree should still be a valid binary search tree. The complexity is O(n)
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public void TrimByIteration(T min, T max)
        {
            //using postorder, treat leaf first and then root
            foreach (var currentNode in PostOrder(_root))
            {
                if (currentNode.Key.CompareTo(min) >= 0 && currentNode.Key.CompareTo(max) <= 0) continue;

                //if out of range, remove it
                //if it is root
                if (currentNode.Parent == null)
                    _root = currentNode.LeftChild ?? currentNode.RightChild;
                else
                {
                    //if has any child(if current node considered to remove, it only has right or left child), is not leaf
                    if (currentNode.HasAnyChildren())
                    {
                        //if left child
                        if (currentNode.IsLeftChild())
                        {
                            //link node's parent and child after remove
                            currentNode.Parent.LeftChild = currentNode.RightChild ?? currentNode.LeftChild;
                            var child = currentNode.RightChild ?? currentNode.LeftChild;
                            child.Parent = currentNode.Parent;
                        }
                        //if left child
                        else
                        {
                            //link node's parent and child after remove
                            currentNode.Parent.RightChild = currentNode.RightChild ?? currentNode.LeftChild;
                            var child = currentNode.RightChild ?? currentNode.LeftChild;
                            child.Parent = currentNode.Parent;
                        }

                    }
                    else // has no child, is leaf, simply remove it
                    {
                        //if left child
                        if (currentNode.IsLeftChild()) currentNode.Parent.LeftChild = null;
                        //if left child
                        else currentNode.Parent.RightChild = null;
                    }
                }
            }
        }

        public void TrimByRecursion(T min, T max)
        {
            _root = _Trim(_root, min, max);
        }

        private Node<T, U> _Trim(Node<T, U> tree, T min, T max)
        {
            //base case
            if (tree == null) return null;

            //postorder to traversal the tree, left->right->root
            //recursively trim left and right child:
            tree.LeftChild = _Trim(tree.LeftChild, min, max);
            tree.RightChild = _Trim(tree.RightChild, min, max);
            //if within the range, keep it
            if (tree.Key.CompareTo(min) >= 0 && tree.Key.CompareTo(max) <= 0) return tree;


            //if current larger than max, cut the right, return smaller child(left), if current less than min, cut the left, return larger child(right)
            return tree.Key.CompareTo(max) > 0 ? tree.LeftChild : tree.RightChild;
        }

        #endregion

        #region Is Balanced Binary Tree?
        /// <summary>
        /// Each node is balanced, and the depth difference between left and right subtree of each node is no more than 1. Time complexity is O(n)
        /// </summary>
        /// <returns></returns>
        public bool IsBalanced()
        {
            return ValidMaxDepth(this._root) != -1;
        }

        private int ValidMaxDepth(Node<T, U> node)
        {
            if (node == null) return 0;//base case

            int left = ValidMaxDepth(node.LeftChild);
            int right = ValidMaxDepth(node.RightChild);
            if (left == -1 || right == -1 || Math.Abs(left - right) > 1)
                return -1;
            return Math.Max(left, right) + 1;
        }

        #endregion

    }
    #region Node class

    public class Node<T, U> where T : IComparable
    {


        public T Key { get; set; }
        public U Payload { get; set; }
        public Node<T, U> LeftChild { get; set; }
        public Node<T, U> RightChild { get; set; }
        public Node<T, U> Parent { get; set; }


        public Node(T key, U payload, Node<T, U> leftChild = null, Node<T, U> rightChild = null, Node<T, U> parent = null)
        {
            Key = key;
            Payload = payload;
            LeftChild = leftChild;
            RightChild = rightChild;
            Parent = parent;
        }

        public bool HasLeftChild()
        {
            return LeftChild != null;
        }

        public bool HasRightChild()
        {
            return RightChild != null;
        }

        public bool IsRoot()
        {
            return Parent == null;
        }

        public bool IsLeftChild()
        {
            return Parent != null && Parent.LeftChild == this;
        }

        public bool IsRightChild()
        {
            return Parent != null && Parent.RightChild == this;
        }

        public bool IsLeaf()
        {
            return Parent != null && !HasRightChild() && !HasLeftChild();
        }

        public bool HasAnyChildren()
        {
            return HasLeftChild() || HasRightChild();
        }

        public bool HasBothChildren()
        {
            return HasLeftChild() && HasRightChild();
        }

        public void ReplaceNodeData(T key, U value, Node<T, U> leftChild, Node<T, U> rightChild)
        {
            Key = key;
            Payload = value;

            if (HasLeftChild()) LeftChild.Parent = this;
            if (HasRightChild()) RightChild.Parent = this;

            LeftChild = leftChild;
            RightChild = rightChild;
        }
    }
    #endregion

}