using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection.Emit;

namespace DataStructure
{
    //Priority Queues with Binary Heaps

    //One important variation of a queue is called apriority queue.
    //A priority queue acts like a queue in that you dequeue an item by removing it from the front. However, in a priority queue the logical order of items inside a queue is determined by their priority. 
    //The highest priority items are at the front of the queue and the lowest priority items are at the back.
    //When you enqueue an item on a priority queue, the new item may move all the way to the front.

    //The classic way to implement a priority queue is using a data structure called abinary heap.
    //A binary heap will allow us both enqueue and dequeue items in O(logn).
    //The binary heap has two common variations: the min heap, in which the smallest key is always at the front, and the max heap, in which the largest key value is always at the front.

    //Min Heap Implementation
    //Consider k-th element of the array, the
    //its left child is located at 2* k+1 index
    //its right child is located at 2* k+2 index
    //its parent is located at (k+1)/2-1 index
    public class BinaryHeap<T> where T : IComparable
    {
        private List<T> _data = new List<T>();
        public int Count => _data.Count;

        /// <summary>
        /// Adds a new item to the heap. The complexity is O(logn).
        /// </summary>
        /// <param name="t"></param>
        public void Enqueue(T t)
        {
            _data.Add(t);
            if (Count > 1) PercUp(Count - 1); //Count-1: the childIndex of the last element
        }

        /// <summary>
        /// percolates a new item as far up in the tree as it needs to go to maintain the heap property.
        /// </summary>
        /// <param name="childIndex"></param>
        private void PercUp(int childIndex)
        {
            int parentIndex = (childIndex + 1) / 2 - 1;
            if (parentIndex>0 && _data[parentIndex].CompareTo(_data[childIndex]) > 0) //if parent value > child value, then swap
            {
                T temp = _data[parentIndex];
                _data[parentIndex] = _data[childIndex];
                _data[childIndex] = temp;
                //recusively up to the root
                PercUp(parentIndex);
            }
        }

        /// <summary>
        /// The minimum element can be found at the root, which is the first element of the list. We remove the root and replace it with the last element of the heap and then restore the heap property by percolating down. Similar to insertion, the worst-case runtime is O(logn).
        /// </summary>
        /// <returns></returns>
        public T Dequeue()
        {
            if (IsEmpty()) throw new Exception("The Heap is empty");
            T deleteItem = _data[0];
            int lastIndex = Count - 1;
            _data[0] = _data[lastIndex];
            _data.RemoveAt(lastIndex);
            if (Count > 2) PercDown(0);
            return deleteItem;
        }

        private void PercDown(int parentIndex)
        {
            int smallerChildIndex = GetMinChildIndex(parentIndex);

            if (smallerChildIndex != parentIndex) // if indeed right or left child smaller than parent, then swap
            {
                T temp = _data[parentIndex];
                _data[parentIndex] = _data[smallerChildIndex];
                _data[smallerChildIndex] = temp;

                //recursively percolates down...
                PercDown(smallerChildIndex);
            }
        }

        /// <summary>
        /// if any child smaller than parent return the smaller child index, otherwise return itself
        /// </summary>
        /// <param name="parentIndex"></param>
        /// <returns></returns>
        private int GetMinChildIndex(int parentIndex)
        {
            int leftChildIndex = 2 * parentIndex + 1;
            int rightChildIndex = 2 * parentIndex + 2;
            int largestIndex = parentIndex;

            //MinValue of (leftChild, rightChild) compared to parents, swap parents with smaller child 
            if (leftChildIndex < Count - 1 && _data[leftChildIndex].CompareTo(_data[largestIndex]) < 0)
                largestIndex = leftChildIndex;
            if (rightChildIndex < Count - 1 && _data[rightChildIndex].CompareTo(_data[largestIndex]) < 0)
                largestIndex = rightChildIndex;
            return largestIndex;
        }

        /// <summary>
        /// Building a heap from an array of n input elements can be done by starting with an empty heap, then successively inserting each element. This approach, called Williams’ method after the inventor of binary heaps, is easily seen to run in O(n log n) time: it performs n insertions at O(log n) cost each.
        /// A faster method starts by arbitrarily putting the elements on a binary tree, respecting the shape property (the tree could be represented by an array, see below). Then starting from the lowest level and moving upwards, sift the root of each subtree downward as in the deletion algorithm until the heap property is restored. The complexity is actually O(n).
        /// </summary>
        /// <param name="list"></param>
        public void BuildHeap(List<T> list)
        {
            _data = list;
            for (int i = list.Count - 1; i >= 0; i--)
            {
                PercDown(i);
            }
        }

        public T Peek()
        {
            return _data[0];
        }

        public bool IsEmpty()
        {
            return Count == 0;
        }

        public List<T> ToList()
        {
            return _data;
        }
    }
}