using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    /// <summary>
    /// Binary tree: Tree where each node has up to two leaves
    /// Binary search tree: Used for searching. A binary tree where the left child contains only nodes with values less than the parent node, and where the right child only contains nodes with values greater than the parent.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BinaryTree<T> where T : IComparable<T>
    {
        private BinaryTree<T> leftChild;
        private BinaryTree<T> rightChild;

        public T RootVal { get; set; }

        public BinaryTree(T root)
        {
            RootVal = root;
        }

        public void InsertLeft(T t)
        {
            if (leftChild == null) leftChild = new BinaryTree<T>(t);
            //if leftChild exits, insert the node and push existing leftChild down one level in the tree
            var temp = new BinaryTree<T>(t) { leftChild = this.leftChild };
            this.leftChild = temp;
        }

        public void InsertRight(T t)
        {
            if (rightChild == null) rightChild = new BinaryTree<T>(t);
            //if rightChild exits, insert the node and push existing rightChild down one level in the tree
            var temp = new BinaryTree<T>(t) { rightChild = this.rightChild };
            this.rightChild = temp;
        }

        public BinaryTree<T> GetRightChild()
        {
            return rightChild;
        }

        public BinaryTree<T> GetLeftChild()
        {
            return leftChild;
        }

        public void Preorder(List<T> list)
        {
            list.Add(RootVal);
            leftChild?.Preorder(list);
            rightChild?.Preorder(list);
        }

        #region BST Validation
        public bool IsBinarySearchTree()
        {
            return _IsBinarySearchTree(this);
        }
        /// <summary>
        /// Check whether it’s a binary search tree or not.
        /// </summary>
        /// <param name="currentRoot"></param>
        /// <returns></returns>
        private bool _IsBinarySearchTree(BinaryTree<T> currentRoot)
        {
            if (currentRoot != null)
            {
                //compare left and root
                if (currentRoot.leftChild != null)
                {
                    if (currentRoot.RootVal.CompareTo(currentRoot.leftChild.RootVal) < 0) return false;
                    if (!_IsBinarySearchTree(currentRoot.leftChild)) return false;
                }


                //compare right and root
                if (currentRoot.rightChild != null)
                {
                    if (currentRoot.RootVal.CompareTo(currentRoot.rightChild.RootVal) < 0) return false;
                    if (!_IsBinarySearchTree(currentRoot.rightChild)) return false;
                }

            }
            return true;
        }
        #endregion
    }

}
