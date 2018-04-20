using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    //There are two main instancef s recursion.
    //The first is when recuresion is used as a technique in which a function makes one or more calls to itself.
    //The second is when a data structure uses smaller instances of the exact same type of data structure when it represents itself.

    //Whenever trying to develop a recursive solution it is very important to think about the base case, as your solution will need to return the base case once all the recursive cases have been worked through.
    public class Recursion
    {
        public static long SumOfOneTo(int n)
        {
            if (n == 1) return 1; //base case that every recursion should have
            return n + SumOfOneTo(n - 1);
        }

        /// <summary>
        /// Given an integer, returns the sum of all the individual digits in that integer. For example: if n = 4321, return 4+3+2+1
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int SumOfAllDigits(int n)
        {
            if (n < 10) return n; //base case
            return n % 10 + SumOfAllDigits(n / 10);
        }

        /// <summary>
        /// The function takes in a string phrase and a set list_of_words. The function will then determine if it is possible to split the string in a way in which words can be made from the list of words. You can assume the phrase will only contain words found in the dictionary if it is completely splittable.
        /// </summary>
        /// <returns></returns>
        public static List<string> SplitWord(string phrase, List<string> words)
        {
            if (words.Contains(phrase)) return new List<string>() { phrase }; //base case
            if (phrase.Length == 1 && !words.Contains(phrase)) return new List<string>();  //base case, return empty list

            var currentWordList = new List<string>();
            var leftWordList = new List<string>();
            for (int i = 0; i < phrase.Length - 1; i++)  //iterate the substring from the first until the second last letter
            {
                if (words.Contains(phrase.Substring(0, i + 1)))
                {
                    currentWordList.Add(phrase.Substring(0, i + 1));
                    leftWordList = SplitWord(phrase.Substring(i + 1), words);
                    break;
                }
            }
            //if neither of current and left wordlist is empty, return the union of two lists
            if (currentWordList.Count > 0 && leftWordList.Count > 0) currentWordList.AddRange(leftWordList);
            //otherwise return empty list
            else currentWordList.Clear();

            return currentWordList;

        }

        public static string RecursivelyReverse(string str)
        {
            if (str.Length == 1) return str; // base case

            return RecursivelyReverse(str.Substring(1)) + str[0].ToString();  // recursive case, swap the first letter and the reversed left.
        }

        #region String Permutation
        /// <summary>
        /// Given a string, write a function that uses recursion to output a list of all the possible permutations of that string. For example, given s='abc' the function should return ['abc', 'acb', 'bac', 'bca', 'cab', 'cba']  Note: If a character is repeated, treat each occurence as distinct, for example an input of 'xxx' would return a list with 6 "versions" of 'xxx'
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static List<string> GetPermutations(string str)
        {
            if (str.Length == 1) return new List<string>() { str }; // base case
            return Insert(str[0], GetPermutations(str.Substring(1))); // recursive case, take the first letter to combine with the list permutated with the subsequential substring
        }

        /// <summary>
        /// Combine the given char with each string from the given strList to form a new strList containing all permutations.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="inputList"></param>
        /// <returns></returns>
        private static List<string> Insert(char c, List<string> inputList)
        {
            var outputList = new List<string>();
            foreach (string str in inputList)
            {
                int length = str.Length;
                //for each str in the list, insert c at all positions in the str, and add to the list
                for (int i = 0; i <= length; i++)
                {
                    if (i == 0) { outputList.Add(c + str); continue; } //insert c at the begining of a string
                    if (i == length) { outputList.Add(str + c); continue; } //insert c at the end of a string
                    outputList.Add(str.Substring(0, i) + c + str.Substring(i)); //insert c in the middle f a string
                }
            }
            return outputList;
        }

        #endregion

        #region Fibonnaci Sequence
        /// <summary>
        /// The fibonacci sequence: 0,1,1,2,3,5,8,13,21,... starts off with a base case checking to see if n = 0 or 1, then it returns 1.
        /// The recursive solution is exponential time Big-O , with O(2^n). However, its a very simple and basic implementation to consider:
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static long RecursivelyGetFib(int n)
        {
            if (n == 0) return 0;
            if (n == 1) return 1;
            //return RecursivelyGetFib(n - 1) + RecursivelyGetFib(n - 2);

            //Dynamically implement a Fibonnaci Sequence (Using Memoization to store results)
            //check "lookup" table
            if (Fibdic.ContainsKey(n)) return Fibdic[n];
            //store result in "lookup" table
            Fibdic[n] = RecursivelyGetFib(n - 1) + RecursivelyGetFib(n - 2);

            return Fibdic[n];
        }

        private static Dictionary<int, long> Fibdic = new Dictionary<int, long>();

        /// <summary>
        /// Iteratively implement a Fibonnaci Sequence
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static long IterativelyGetFib(int n)
        {
            long[] fib = new long[n + 1];
            for (int i = 0; i <= n; i++)
            {
                switch (i)
                {
                    case 0:
                        fib[i] = 0;
                        break;
                    case 1:
                        fib[i] = 1;
                        break;
                    default:
                        fib[i] = fib[i - 1] + fib[i - 2];
                        break;
                }
            }
            return fib[n];
        }

        #endregion

        #region Coin Change
        /// <summary>
        /// Given a target amount n and a list (array) of distinct coin values, what's the fewest coins needed to make the   change amount.
        /// For example:
        /// If n = 10 and coins = [1, 5, 10].Then there are 4 possible ways to make change:
        /// 1+1+1+1+1+1+1+1+1+1
        /// 5 + 1+1+1+1+1
        /// 5+5
        /// 10
        /// With 1 coin being the minimum amount.
        /// </summary>
        /// <returns></returns>
        public static int MinNumOfCoin(int amount, int[] coins)
        {
            if (amount == 0) return 0; // base case
            if (coins.Contains(amount)) return 1;

            int min = amount;

            //For Every coin we have two options, whether so select it or don’t select it.
            foreach (int value in coins)
            {
                if (value <= amount)
                {
                    //tempList.Add(1 + MinNumOfCoin((amount - value), coins));
                    if (!DicOfCoinChange.ContainsKey(amount - value))
                        DicOfCoinChange[amount - value] = MinNumOfCoin((amount - value), coins);
                    min = min > 1 + DicOfCoinChange[amount - value] ? 1 + DicOfCoinChange[amount - value] : min;
                }
            }
            return min;
        }

        private static Dictionary<int, int> DicOfCoinChange = new Dictionary<int, int>();

        #endregion

        public static int RecursiveLinearSearch(int[] array, int fromIndex, int value)
        {
            if (fromIndex > array.Length - 1) return -1;
            if (array[fromIndex] == value) return fromIndex;
            return RecursiveLinearSearch(array, fromIndex + 1, value);
        }

        public static int RecursiveBinarySearch(int[] sortedArray, int fromIndex, int untilIndex, int value)
        {
            if (fromIndex < 0 || untilIndex > sortedArray.Length - 1)
            {
                throw new Exception("Out of Range!");
            }
            if (fromIndex > untilIndex) return -1;
            int mid = (fromIndex + untilIndex) / 2;
            if (sortedArray[mid] == value) return mid;
            else if (sortedArray[mid] > value) return RecursiveBinarySearch(sortedArray, fromIndex, mid - 1, value);
            else if (sortedArray[mid] < value) return RecursiveBinarySearch(sortedArray, mid + 1, untilIndex, value);
            else return -1;
        }
    }
}
