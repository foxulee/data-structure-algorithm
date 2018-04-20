using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    public class Sorting
    {
        #region Bubble MSort
        /// <summary>
        /// Bubble sort has worst-case and average complexity both О(n2),
        /// </summary>
        /// <param name="array"></param>
        public static void BubbleSort(int[] array)
        {
            int length = array.Length;
            for (int i = length - 2; i > 0; i--)
            {
                for (int j = 0; j <= i; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        //swap array[j] and array[j+1]
                        int temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }
        }


        #endregion

        #region Selection MSort
        /// <summary>
        /// The selection sort algorithm sorts an array by repeatedly finding the minimum element (considering ascending order) from unsorted part and putting it at the beginning. 
        /// Complexity: O(n2)
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public void SelectSort(int[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                int rightMinIndex = GetIndexOfMinimum(array, i + 1);
                if (array[i] > array[rightMinIndex])
                {
                    //swap
                    int temp = array[i];
                    array[i] = array[rightMinIndex];
                    array[rightMinIndex] = temp;
                }
            }
        }

        private static int GetIndexOfMinimum(int[] array, int start)
        {
            int minIndex = start;
            for (int j = start + 1; j < array.Length; j++)
            {
                if (array[minIndex] > array[j]) minIndex = j;
            }
            return minIndex;
        }
        #endregion

        #region Insertion MSort
        /// <summary>
        /// The array is searched sequentially and unsorted items are moved and inserted into the sorted sub-list at the begining(in the same array). This algorithm is not suitable for large data sets 
        /// O(n2)
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static void InsertionSort(int[] array)
        {
            for (int i = 1; i < array.Length; i++)
            {
                int temp = array[i];
                int j = i;
                while (j>=1 && array[j] < array[j - 1])
                {
                    //swap
                    array[j] = array[j - 1];
                    array[j - 1] = temp;
                    j--;
                }
            }

        }

        #endregion

        #region Shell MSort

        public void ShellSort(int[] array)
        {
            
        }

        #endregion

        #region Merge MSort

        public static void MergeSort(int[] array)
        {
            MSort(array, 0, array.Length - 1);
        }
        /// <summary>
        /// In a Recursive way
        /// Complexity: O(nlogn)
        /// However, it will generate addition space to store the temperaty arrays. If the memory is an issue, Quick sort is a better choise.
        /// </summary>
        /// <param name="inputArray"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        private static void MSort(int[] inputArray, int start, int end)
        {
            if (start >= end) return;

            int mid = (start + end) / 2;
            //sort left
            MSort(inputArray, start, mid);
            //sort right
            MSort(inputArray, mid + 1, end);
            //Merge left and right into inputArray
            Merge(inputArray, start, mid, end);
        }

        private static void Merge(int[] inputArray, int start, int mid, int end)
        {
            int length = end - start + 1;
            int[] tempArray = new int[length];
            int leftSlot = start;
            int rightSlot = mid + 1;
            int j = 0;

            //using the for-loop to replace the 3 while-loops
            for (int i = 0; i < tempArray.Length; i++)
            {
                //before directly comparing left and right array, check if left/right is copied over
                if (leftSlot > mid) // if left array has been copied over, then simply copy right array into tempArray
                {
                    tempArray[i] = inputArray[rightSlot];
                    rightSlot++;
                }
                else if (rightSlot > end)
                {
                    tempArray[i] = inputArray[leftSlot];
                    leftSlot++;
                }
                else if (inputArray[leftSlot] < inputArray[rightSlot])
                {
                    tempArray[i] = inputArray[leftSlot];
                    leftSlot++;
                }
                else
                {
                    tempArray[i] = inputArray[rightSlot];
                    rightSlot++;
                }
            }

            ////merge left and right arrays into tempArray with ascending order
            //while (leftSlot <= mid && rightSlot <= end)
            //{
            //    if (inputArray[leftSlot] <= inputArray[rightSlot])
            //    {
            //        tempArray[j] = inputArray[leftSlot];
            //        leftSlot++;
            //    }
            //    else
            //    {
            //        tempArray[j] = inputArray[rightSlot];
            //        rightSlot++;
            //    }
            //    j++;
            //}

            ////copy the left of the left or right array into tempArray if any left
            //while (leftSlot <= mid)
            //{
            //    tempArray[j] = inputArray[leftSlot];
            //    leftSlot++;
            //    j++;
            //}
            //while (rightSlot <= end)
            //{
            //    tempArray[j] = inputArray[rightSlot];
            //    rightSlot++;
            //    j++;
            //}

            //replace inputArray with tempArray
            for (int i = 0; i < length; i++)
            {
                inputArray[start + i] = tempArray[i];
            }
        }

        #endregion

        #region Quick MSort

        public static void QuickSort(int[] array)
        {
            QSort(array, 0, array.Length - 1);
        }
        /// <summary>
        /// O(nlogn)
        /// </summary>
        /// <param name="inputArray"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        private static void QSort(int[] inputArray, int start, int end)
        {
            if (start >= end) return;
            int partitionIndex = Partion(inputArray, start, end);
            QSort(inputArray, start, partitionIndex - 1);
            QSort(inputArray, partitionIndex + 1, end);
        }

        /// <summary>
        /// return the index whose value is less than the left, and greater than the right. Meanwhile rearrange the array.
        /// </summary>
        /// <param name="inputArray"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private static int Partion(int[] inputArray, int start, int end)
        {
            int index = start - 1;
            int pivot = inputArray[end];
            for (int i = start; i <= end - 1; i++) //i can only reach to the second last
            {
                if (inputArray[i] <= pivot)
                {
                    index++;
                    //swap inputArray[i] and inputArray[partionIndex]
                    int temp = inputArray[index];
                    inputArray[index] = inputArray[i];
                    inputArray[i] = temp;
                }
            }

            //put the pivot at the right position
            int temp2 = inputArray[index + 1];
            inputArray[index + 1] = inputArray[end];
            inputArray[end] = temp2;

            return index + 1;
        }

        #endregion

        public static void PrintArray(int[] array)
        {
            string str = "[ ";
            for (int i = 0; i < array.Length; i++)
            {
                str += array[i] + " ";
            }
            str += "]";
            Console.WriteLine(str);
        }
    }
}
