using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    public class Search
    {
        /// <summary>
        /// O(n)
        /// </summary>
        /// <param name="randomArray"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int LinearSearch(int[] randomArray, int value)
        {
            for (int i = 0; i < randomArray.Length; i++)
            {
                if (randomArray[i] == value)
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// If the array sorted, binary search comes to play, O(log n)
        /// </summary>
        /// <param name="sortedArray"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int BinarySearch(int[] sortedArray, int value)
        {
            int start = 0;
            int end = sortedArray.Length - 1;

            while (start <= end)
            {
                int mid = (end + start) / 2;
                if (value > sortedArray[mid]) start = mid + 1;
                else if (value < sortedArray[mid]) end = mid - 1;
                else return mid;
            }
            return -1;
        }
    }
}
