using System;
using Algorithm;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //DataStructure.Stack<int> myStack = new DataStructure.Stack<int>(3);
            //Console.WriteLine("IsEmpty" + myStack.IsEmpty());
            //myStack.Push(1);
            //myStack.Push(2);
            //Console.WriteLine(myStack.ToString());
            //Console.WriteLine(myStack.Peek());

            //myStack.Push(3);
            //Console.WriteLine(myStack.IsFull());

            //Console.WriteLine(myStack.Pop());
            //Console.WriteLine(myStack.Pop());
            //Console.WriteLine(myStack.IsFull());
            //Console.WriteLine(myStack.Pop());
            //Console.WriteLine(myStack.ToString());
            //Console.WriteLine(myStack.IsEmpty());

            //Console.WriteLine(ReverseString("Hello"));

            ////Search
            //int[] array = new[] { 1, 12, 33, 47, 51, 60, 65, 77, 79, 80, 81 };
            //int num = 66;
            //Console.WriteLine("Linear: " + Search.RecursiveLinearSearch(array, num));
            //Console.WriteLine("Binary: " + Search.BinarySearch(array, num));

            //Recursion
            //Console.WriteLine(Recursion.SumOfOneTo(100));
            //Console.WriteLine(Recursion.RecursiveBinarySearch(new int[] { 1, 2, 3, 4, 5, 6 }, fromIndex: 0, untilIndex: 5, value: 3));

            //Sorting
            //Sorting.PrintArray(Sort.InsertionSort(new int[] { 2, 21, 3, 67, 14, 3, 100, 56, 24, -1, 100, -2, 98 }));
            //int[] array = new int[] { 2, 21, 3, 67, 14, 3, 100, 56, 24, -1, 100, -2, 98, 1000 };
            //Sorting.QuickSort(array);
            //Sorting.PrintArray(array);

            //var bst = new DataStructure.BinarySearchTree<int, int>();
            //bst.Put(6, 6);
            //bst.Put(4, 4);
            //bst.Put(7, 7);
            //bst.Put(3, 3);
            //bst.Put(5, 5);
            //bst.Put(9, 9);
            ////bst.Put(1, 1);
            //bst.Put(2, 2);
            //bst.Put(8, 8);
            //bst.Put(1, 1);
            //bst.Put(10, 10);
            ////bst.Put(12, 12);
            ////bst.Put(9, 9);
            ////bst.Put(10, 10);
            ////Console.WriteLine(bst.IsBalanced());
            //////bst.TrimByIteration(7, 10);
            ////bst.TrimByRecursion(7, 10);
            ////Console.WriteLine($"Max Depth is {bst.MaxDepth()}");
            //Console.WriteLine($"MaxPathSumFromRoot is {bst.MaximumPathSumFromRoot()}");
            //Console.WriteLine($"MaxPathSumFromAnyNode is {bst.MaximumPathSumFromAnyNode()}");
            //bst.PrintByLevel();
            //Console.WriteLine("--------------------------------");
            //foreach (var item in bst)
            //{
            //    Console.WriteLine(item);
            //}




            //var graph = new DataStructure.Graph<string>();
            //graph.AddEdge("Beijing","Shanghai",10);
            //graph.AddEdge("Beijing","Wuhan",15);
            //graph.AddEdge("Beijing", "Chongqing", 29);
            //graph.AddEdge("Shanghai", "Chongqing", 12);
            //graph.AddEdge("Wuhan", "Chongqing", 15);
            //graph.AddEdge("Wuhan","Shanghai", 13);
            //graph.AddEdge("Shanghai", "Wuhan", 13);

            //var paths = graph.DFS_FindPath("Beijing", "Chongqing");
            //foreach (var path in paths)
            //{
            //     foreach (var city in path)
            //    {
            //        Console.WriteLine(city.Id);
            //    }
            //    Console.WriteLine("_________________________________");
            //}

            //SolutionToProblems.PrintJumpingNumbers(89384);

            //var list = PermutationAndSubset.GetPermutations(new int[] { 1, 2, 3}, 2);
            //foreach (var item in list)
            //{
            //    foreach (var i in item)
            //    {
            //        Console.Write(i + " ");
            //    }
            //    Console.WriteLine();
            //}

            //var list = PermutationAndSubset.SolveNQueens(11);
            //foreach (var strs in list)
            //{
            //    foreach (var str in strs)
            //    {
            //        Console.WriteLine(str);
            //    }
            //    Console.WriteLine();
            //}

            //var list = new SinglyLinkedList<int>();

            //var first = list.InsertLast(1);
            //list.InsertLast(4);
            //list.InsertLast(5);
            //list.InsertLast(2);
            //list.InsertLast(3);
            //list.InsertLast(8);
            //list.InsertLast(9);
            //list.InsertLast(6);

            //list.Print();
            //Console.WriteLine();

            //var binarySearchTree = list.ToBinarySearchTree();
            //BinarySearchTree<int,int>.PrintByLevel(binarySearchTree == null ? new List<Node<int, int>>(){} : new List<Node<int, int>>() { binarySearchTree });



            int[][] arr = {
                new []{1, 2, 3, 4, 5},
                new []{2, 1, 3, 5, 2},
                new []{3, 7, 8, 10, 9},
                new []{4, 9, 10, 2, 1}
            };

            int[] a = { 10, 22, 9, 33, 21, 50, 41, 60, 80, 1 };
            int[] b = { 1, 2, 3, 4, 5, 1, 2 };
            int[] c = { 6, 1 };
            int[] d = { 2, 3, 6, 7 };






            //      5
            //     / \
            //    4   8
            //   /   / \
            //  11  13  4
            // /  \    / \
            //7    2  5   1
            //           /
            //          4
            var n5 = new PermutationAndSubset.TreeNode(5)
            {
                left = new PermutationAndSubset.TreeNode(4)
                {
                    left = new PermutationAndSubset.TreeNode(11)
                    {
                        left = new PermutationAndSubset.TreeNode(7),
                        right = new PermutationAndSubset.TreeNode(2)
                    }
                },
                right = new PermutationAndSubset.TreeNode(8)
                {
                    left = new PermutationAndSubset.TreeNode(13),
                    right = new PermutationAndSubset.TreeNode(4)
                    {
                        left = new PermutationAndSubset.TreeNode(5),
                        right = new PermutationAndSubset.TreeNode(1)
                        {
                            left = new PermutationAndSubset.TreeNode(4)
                        }
                    }
                }
            };

            var n1 = new PermutationAndSubset.TreeNode(1);
            //Console.WriteLine();
            //Console.WriteLine($"result is {DynamicProgramming.LIS(a)}");


            foreach (var list in PermutationAndSubset.PathSum(n5, 27))
            {
                foreach (var str in list)
                {
                    Console.Write(str + " ");
                }
                Console.WriteLine();
            }

            //string s = "s";
            //Console.WriteLine(s.Substring(2));
            Console.ReadLine();
        }


        /// <summary>
        /// using Stack
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ReverseString(string str)
        {
            string reversedString = "";
            DataStructure.Stack<char> chars = new DataStructure.Stack<char>(str.Length);
            foreach (char t in str)
            {
                chars.Push(t);
            }
            while (!chars.IsEmpty())
            {
                reversedString += chars.Pop();
            }
            return reversedString;
        }


    }
}
