using System;
using System.Collections.Generic;

namespace DataStructure
{
    //p.s.
    //Be cautious!!!!!
    //Before visit X.next, make sure X is NOT NULL!!!
    //If head node may change, create a Dummy node, dummy node' next point to head, finally return dummy.next 

    public class SinglyLinkedList<T> where T : IComparable
    {
        private SinglyNode<T> _firstNode;
        public int Count { get; set; }

        public bool IsEmpty()
        {
            return _firstNode == null;
        }

        #region Finding a Cycle In a Singly Linked List
        /// <summary>
        /// You can make use of Floyd's cycle-finding algorithm, also known as tortoise and hare algorithm. The idea is to have two references to the list and move them at different speeds.Move one forward by 1 node and the other by 2 nodes. If the linked list has a loop they will definitely meet. Else either of the two references(or their next) will become null.
        /// </summary>
        /// <returns></returns>
        public bool HasCycle()
        {
            SinglyNode<T> slow, fast;
            slow = fast = _firstNode;
            while (fast != null && fast.Next != null) //fast.Next != null make sure fast can make 2 hops
            {
                slow = slow.Next;  // 1 hop
                fast = fast.Next.Next; // 2 hops
                if (slow == fast) return true;
            }
            return false;
        }

        #endregion

        #region Reversing the Linked List
        /// <summary>
        /// O(n)
        /// </summary>
        public void Reverse()
        {
            SinglyNode<T> prev = null;
            SinglyNode<T> current = _firstNode;
            while (current != null)
            {
                var next = current.Next;
                current.Next = prev;
                prev = current;
                current = next;
            }
            _firstNode = prev;
        }

        /// <summary>
        /// Reverse from mth to nth nodes in the list, m and n meets 1 <= m <= n <= length
        /// </summary>
        /// <param name="m"></param>
        /// <param name="n"></param>
        public void PartlyReverse(int m, int n)
        {
            var curr = _firstNode;
            var steps = 1;
            SinglyNode<T> before = new SinglyNode<T>();
            SinglyNode<T> start = new SinglyNode<T>();
            SinglyNode<T> after = new SinglyNode<T>();

            SinglyNode<T> pre = new SinglyNode<T>();
            if (m == 1)  //edge case
            {
                before = null;
                start = _firstNode;
            }

            while (steps <= n)
            {
                if (steps == m - 1) // find starting point
                {
                    before = curr;
                    start = curr.Next;
                }
                if (steps >= m && steps < n) //reverse
                {
                    var temp = curr.Next;
                    curr.Next = pre;
                    pre = curr;
                    curr = temp;
                    steps++;
                    continue;
                }
                if (steps == n) //last step, steps and curr arrive at n, link curr and prev, set after
                {
                    after = curr.Next;
                    curr.Next = pre;
                    break;
                }

                steps++;
                curr = curr.Next;
            }

            if (before != null) before.Next = curr; // edge case
            else _firstNode = curr;
            start.Next = after;

        }

        #endregion

        #region From Nth to the last Node
        /// <summary>
        /// O(n)
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public T GetValueNthToLastNode(int n)
        {
            SinglyNode<T> leftPointer = _firstNode;
            SinglyNode<T> rightPointer = _firstNode;
            for (int i = 0; i < n - 1; i++) //rightPointer hop n nodes
            {
                rightPointer = rightPointer.Next ?? throw new Exception("N is Out of Range!");
            }
            while (rightPointer.Next != null) //left and right hop together until rightPointer reaches the last node(rightPointer.Next = null)
            {
                leftPointer = leftPointer.Next;
                rightPointer = rightPointer.Next;
            }
            return leftPointer.Data; //then leftPointer points to the nth last node.
        }

        #endregion

        #region Remove duplicate items
        /// <summary>
        /// Suppose the list is ordered, remove duplicates, keep only one copy
        /// </summary>
        public void RemoveDuplicateIfOrdered()
        {
            var current = _firstNode;
            while (current.Next != null)
            {
                if (current.Next.Data.CompareTo(current.Data) == 0) current.Next = current.Next.Next;
                else current = current.Next;
            }
        }

        /// <summary>
        /// Suppose the list is ordered, remove all if duplicated
        /// </summary>
        public void RemoveAllIfDuplicated()
        {
            //use dummy node before _firstNode
            var dummy = new SinglyNode<T>();
            dummy.Next = _firstNode;
            _firstNode = dummy;
            while (_firstNode.Next != null && _firstNode.Next.Next != null)
            {
                if (_firstNode.Next.Data.CompareTo(_firstNode.Next.Next.Data) == 0)
                {
                    var value = _firstNode.Next.Data;
                    while (_firstNode.Next != null && _firstNode.Next.Data.CompareTo(value) == 0)
                    {
                        _firstNode.Next = _firstNode.Next.Next;
                    }
                }
                else _firstNode = _firstNode.Next;
            }
            _firstNode = dummy.Next;

        }

        public void RemoveDuplicateIfNotOrdered()
        {
            var current = _firstNode;
            var hash = new HashSet<T>();
            while (current.Next != null)
            {
                hash.Add(current.Data);
                if (hash.Contains(current.Next.Data)) current.Next = current.Next.Next;
                else current = current.Next;
            }
        }



        #endregion

        #region Partition List
        /// <summary>
        /// Given a linked list and a value x, partition it such that all nodes less that x come before nodes greater than r equal to x
        /// </summary>
        /// <param name="value"></param>
        public void Partition(T value)
        {
            SinglyNode<T> dummyLess = new SinglyNode<T>();
            SinglyNode<T> dummyLarger = new SinglyNode<T>();
            dummyLess.Next = _firstNode;
            _firstNode = dummyLess;
            var largerCurr = dummyLarger;//set current point

            while (_firstNode.Next != null)
            {
                if (_firstNode.Next.Data.CompareTo(value) >= 0) // greater than x, move to right
                {
                    //add to large list
                    largerCurr.Next = _firstNode.Next; //add to larger list
                    largerCurr = largerCurr.Next; // move pointer

                    //remove from current list
                    _firstNode.Next = _firstNode.Next.Next;
                }
                else _firstNode = _firstNode.Next;
            }
            _firstNode.Next = dummyLarger.Next;


            _firstNode = dummyLess.Next;//remove dummy node
        }

        #endregion

        #region Find Middle of the Linked List

        /// <summary>
        ///Given a singly linked list, find middle of the linked list. For example, if given linked list is 1->2->3->4->5 then output should be 3. If there are even nodes, then there would be two middle nodes, we need to print second middle element.
        /// </summary>
        /// <returns></returns>
        public T MiddleItem()
        {
            if (_firstNode == null) return default(T);

            SinglyNode<T> slow = _firstNode;
            SinglyNode<T> fast = _firstNode;
            while (fast?.Next != null)    //p.s. fast could be null, put question mark
            {
                slow = slow.Next;
                fast = fast.Next.Next;
            }
            return slow.Data;
        }


        #endregion

        #region Sort

        /// <summary>
        /// Time complexity O(nlogn), splace O(1)
        /// </summary>
        /// 
        public void Sort()
        {
            _firstNode = SortHelper(_firstNode);
        }
        private SinglyNode<T> SortHelper(SinglyNode<T> head)
        {
            //base case
            if (head?.Next == null) return head;

            //recursion
            var middelNode = GetMiddleNode(head);
            var node2 = middelNode.Next; //set head for the right part
            middelNode.Next = null; // break the list into two parts

            var left = SortHelper(head); //potentially cause overflow-exception, head is the same with the method parameter, such that the size for head may not be reduced.
            var right = SortHelper(node2);
            return Merge(left, right);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node1">sorted array</param>
        /// <param name="node2">sorted array</param>
        /// <returns></returns>
        private SinglyNode<T> Merge(SinglyNode<T> node1, SinglyNode<T> node2)
        {
            if (node1 == null) return node2;
            if (node2 == null) return node1;

            SinglyNode<T> dummy = new SinglyNode<T>();
            var curr = dummy;

            while (node1 != null && node2 != null)
            {
                if (node1.Data.CompareTo(node2.Data) >= 0) //node1 value larger thant node2 value
                {
                    curr.Next = node2;
                    node2 = node2.Next;

                }
                else
                {
                    curr.Next = node1;
                    node1 = node1.Next;
                }
                curr = curr.Next;
            }
            curr.Next = node1 ?? node2;
            return dummy.Next;
        }

        private SinglyNode<T> GetMiddleNode(SinglyNode<T> head)
        {
            if (head == null) return null;
            if (head.Next == null) return head;

            SinglyNode<T> slow = head;
            SinglyNode<T> fast = head;
            while (fast.Next != null && fast.Next.Next != null)
            {
                slow = slow.Next;
                fast = fast.Next.Next;
            }
            return slow;
        }


        #endregion

        #region Rearrange

        public void Rearrange()
        {
            if (_firstNode?.Next == null) return;

            var middleNode = GetMiddleNode(_firstNode);
            var rightPart = middleNode.Next;
            middleNode.Next = null;
            _firstNode = Combine(_firstNode, Reverse(rightPart));
        }

        private SinglyNode<T> Reverse(SinglyNode<T> node)
        {
            if (node?.Next == null) return node;

            SinglyNode<T> pre = null;
            while (node != null)
            {
                var next = node.Next;
                node.Next = pre;
                pre = node;
                node = next;
            }
            return pre;
        }

        private SinglyNode<T> Combine(SinglyNode<T> first, SinglyNode<T> second)
        {
            SinglyNode<T> dummy = new SinglyNode<T>();
            var curr = dummy;

            while (first != null && second != null)
            {
                curr.Next = first;
                curr = curr.Next;
                first = first.Next;

                curr.Next = second;
                curr = curr.Next;
                second = second.Next;
            }
            curr.Next = first ?? second;
            return dummy.Next;
        }

        #endregion

        #region Convert Sorted List to Balanced Binary Search Tree

        public Node<T, T> ToBinarySearchTree()
        {
            Sort();
            return ToBST(_firstNode, this.Count);
        }

        private Node<T, T> ToBST(SinglyNode<T> node, int count)
        {
            if (count == 0) return null;
            if (count == 1) return new Node<T, T>(node.Data, node.Data);

            int middle = count / 2;
            int steps = 0;
            var curr = node;
            while (steps < middle)
            {
                curr = curr.Next;
                steps++;
            }
            return new Node<T, T>(curr.Data, curr.Data,
                                  leftChild: ToBST(node, middle),
                                  rightChild: ToBST(curr.Next, count - middle - 1
                                  ));

        }

        #endregion

        public void InsertFirst(T t)
        {
            SinglyNode<T> newNode = new SinglyNode<T>
            {
                Data = t,
                Next = _firstNode
            };
            _firstNode = newNode;
            Count++;
        }

        /// <summary>
        /// O(n)
        /// </summary>
        /// <param name="t"></param>
        public SinglyNode<T> InsertLast(T t)
        {
            SinglyNode<T> current = _firstNode;
            if (current == null)
            {
                _firstNode = new SinglyNode<T>() { Data = t };
                Count++;
                return _firstNode;
            }
            else
            {
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = new SinglyNode<T>() { Data = t };
                Count++;
                return current.Next;
            }
        }

        public void DeleteFirst()
        {
            if (_firstNode != null)
            {
                _firstNode = _firstNode.Next;
                Count--;
            }
            else
            {
                throw new Exception("The list is empty");
            }
        }

        public void Print()
        {
            SinglyNode<T> current = _firstNode;
            while (current != null)
            {
                Console.WriteLine(current.Data);
                current = current.Next;
            }
        }

        private void Print(SinglyNode<T> node)
        {
            SinglyNode<T> current = node;
            while (current != null)
            {
                Console.WriteLine(current.Data);
                current = current.Next;
            }
        }

    }



    public class CircularLinkedList<T>
    {
        private SinglyNode<T> _first;
        private SinglyNode<T> _last;

        public CircularLinkedList()
        {
            _first = null;
            _last = null;
        }

        public void InsertFirst(T t)
        {
            SinglyNode<T> newNode = new SinglyNode<T>() { Data = t };
            if (IsEmpty())
            {
                _last = newNode;
            }
            newNode.Next = _first; // newNode--> old first
            _first = newNode; // first place
        }

        /// <summary>
        /// O(1)
        /// </summary>
        /// <param name="t"></param>
        public void InsertLast(T t)
        {
            SinglyNode<T> newNode = new SinglyNode<T>() { Data = t };
            if (IsEmpty())
            {
                _first = newNode;
            }
            else
            {
                _last.Next = newNode;
                _last = newNode;
            }
        }

        public void DeleteFirst()
        {
            if (_first.Next == null) // if only one node
            {
                _last = null;
            }
            _first = _first.Next;
        }

        private bool IsEmpty()
        {
            return _first == null;
        }
    }

    public class DoublyLinkedList<T>
    {
        private DoublyNode<T> _first;
        private DoublyNode<T> _last;

        public DoublyLinkedList()
        {
            _first = null;
            _last = null;
        }

        public void InsertFirst(T t)
        {
            DoublyNode<T> newNode = new DoublyNode<T>() { Data = t };
            if (IsEmpty())
            {
                _last = newNode;
            }
            else
            {
                _first.Previous = newNode;
            }

            newNode.Next = _first; // newNode--> old first
            _first = newNode; // first place
        }

        public void InsertLast(T t)
        {
            DoublyNode<T> newNode = new DoublyNode<T>() { Data = t };
            if (IsEmpty())
            {
                _first = newNode;
            }
            else
            {
                _last.Next = newNode;
                newNode.Previous = _last;
            }
            _last = newNode;
        }

        public void DeleteFirst()
        {
            if (!IsEmpty()) //
            {
                if (_first.Next == null) // if there is only one node in the list
                {
                    _last = null;
                }
                else
                {
                    _first.Next.Previous = null; // the list's first node will point to null
                }
                _first = _first.Next;
            }
            else
            {
                throw new Exception("The list is empty!");
            }

        }

        public void DeleteLast()
        {
            if (!IsEmpty())
            {
                if (_first.Next == null)
                {
                    _first = null;
                }
                else
                {
                    _last.Previous.Next = null;
                }
                _last = _last.Previous;
            }
            else
            {
                throw new Exception("The list is empty!");
            }

        }

        //public bool InsertAfter(T key, T data)
        //{
        //    DoublyNode<T> current = _first;
        //    while (current.Next != null)
        //    {
        //        if (key == current.Data)
        //        {
        //            DoublyNode<T> newNode = new DoublyNode<T>(){Data = data};
        //            if (current==_last)
        //            {
        //                current.Next = null;
        //                _last = newNode;
        //            }
        //            else
        //            {
        //                newNode.Next = current.Next;
        //                current.Next.Previous = newNode;
        //            }
        //            newNode.Previous = current;
        //            current.Next = newNode;
        //            return true;
        //        }
        //        current = current.Next;
        //    }
        //    return false;
        //}

        private bool IsEmpty()
        {
            return _first == null;
        }
    }

    public class SinglyNode<T>
    {
        public T Data { get; set; }
        public SinglyNode<T> Next { get; set; }
    }

    internal class DoublyNode<T>
    {
        public T Data { get; set; }
        public DoublyNode<T> Next { get; set; }
        public DoublyNode<T> Previous { get; set; }
    }
}
