using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataStructure;

namespace AbstractDataTypes
{
    public class SolutionToProblems
    {
        #region Anagram Check
        /// <summary>
        /// GIven two strings, check t see if they are anagrams. An anagram is when the two strings can be written using the exact same letters(so you can just rearrange the letters to geta differenet phrase or word.) Note" ingor spaces and capitalization. So "d go" is an anagram of "God" and "dog" and "o d g".
        /// </summary>
        /// <param name="str1"></param>
        /// <param name="str2"></param>
        /// <returns></returns>
        public static bool AnagramCheck(string str1, string str2)
        {
            #region Solution 1: using sorted list
            //List<char> list1 = ToListLowerAndTrimSpace(str1);
            //List<char> list2 = ToListLowerAndTrimSpace(str2);
            //if (list1.Count != list2.Count)
            //{
            //    return false;
            //}
            //list1.Sort();
            //list2.Sort();
            //for (int i = 0; i < list1.Count; i++)
            //{
            //    if (list1[i] != list2[i])
            //    {
            //        return false;
            //    }
            //}

            //return true; 
            #endregion

            #region Solution 2: using dictionary and counting, which is more efficient.

            Dictionary<Char, int> dic1 = ConvertToDictionary(str1);
            Dictionary<Char, int> dic2 = ConvertToDictionary(str2);
            if (dic1.Count != dic2.Count)
            {
                return false;
            }
            foreach (KeyValuePair<char, int> entry in dic1)
            {
                if (!dic2.ContainsKey(entry.Key) || entry.Value != dic2[entry.Key])
                {
                    return false;
                }
            }
            return true;

            #endregion
        }

        private static Dictionary<char, int> ConvertToDictionary(string str)
        {
            Dictionary<char, int> dic = new Dictionary<char, int>();
            foreach (char c in str.ToLower())
            {
                if (c != ' ')
                {
                    if (dic.ContainsKey(c))
                    {
                        dic[c]++;
                    }
                    else dic[c] = 1;
                }
            }
            return dic;
        }

        private static List<char> ToListLowerAndTrimSpace(string str)
        {
            List<char> list = new List<char>();
            foreach (char c in str.ToLower())
            {
                if (c != ' ')
                {
                    list.Add(c);
                }
            }
            return list;
        }
        #endregion

        #region Array Pair Sum

        /// <summary>
        /// Given an integer array, output all the unique pairs that sum up to a specific value k. Note: For testing purposes change your function so it outputs the number fairs 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="sum"></param>
        public static string PairSum(int[] array, int k)
        {
            HashSet<int> seen = new HashSet<int>();
            HashSet<Tuple<int, int>> output = new HashSet<Tuple<int, int>>();
            foreach (int n in array)  //O(n)
            {
                int target = k - n;
                if (!seen.Contains(target)) seen.Add(n);  //O(1)
                else output.Add(new Tuple<int, int>(Math.Min(n, target), Math.Max(n, target)));
            }

            string str = "";
            foreach (var n in output)
            {
                str += "(" + n.Item1 + ", " + n.Item2 + ") ";
            }
            return String.Format("{0} Pairs: {1}", output.Count, str);
        }
        #endregion

        #region Finding the Missing Element
        /// <summary>
        /// Consider an array of non-negtive integers. A second array is formed by shuffling the elements of the first array and deleting a random element. Given these two arrays, find which element is missing in the second array.
        /// </summary>
        /// <param name="arr1"></param>
        /// <param name="arr2"></param>
        /// <returns></returns>
        public static int[] FindMissingElem(int[] arr1, int[] arr2)
        {
            Dictionary<int, int> dic = new Dictionary<int, int>();
            List<int> output = new List<int>();
            foreach (int n in arr1)
            {
                if (dic.ContainsKey(n)) dic[n]++;
                else dic[n] = 1;
            }
            foreach (int n in arr2)
            {
                if (dic.ContainsKey(n) && dic[n] > 0) dic[n]--;
            }
            foreach (KeyValuePair<int, int> pair in dic)
            {
                if (pair.Value != 0)
                {
                    int times = pair.Value;
                    for (int i = 0; i < times; i++)
                    {
                        output.Add(pair.Key);
                    }
                }
            }
            return output.ToArray();
        }

        #endregion

        #region Largest Continuous Sum
        /// <summary>
        /// Given an array of integers(positive and negative) find the largest continuous sum plus start and end index.
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static Tuple<int, int, int> LargestSum(int[] array)
        {
            int maxSum = array[0], currentSum = array[0];
            int startIndex = 0, endIndex = startIndex;
            int startMaxIndex = 0, endMaxIndex = startMaxIndex;
            for (int i = 1; i < array.Length; i++)  //start from index of 1
            {
                if (currentSum + array[i] >= array[i])
                {
                    currentSum = currentSum + array[i];
                    endIndex = i;
                }
                else
                {
                    currentSum = array[i];
                    startIndex = i;
                    endIndex = startIndex;
                }

                if (currentSum >= maxSum)
                {
                    maxSum = currentSum;
                    startMaxIndex = startIndex;
                    endMaxIndex = endIndex;
                }
            }
            return new Tuple<int, int, int>(maxSum, startMaxIndex, endMaxIndex);
        }


        #endregion

        #region Sentence Reversal
        /// <summary>
        /// As part of this exercise you should remove all leading and trailing whitespace.
        /// </summary>
        /// <param name="inputStr"></param>
        /// <returns></returns>
        public static string ReserveSentence(string inputStr)
        {
            string output = "";
            string word = "";
            bool isWordStarted = false;
            System.Collections.Generic.Stack<string> stack = new System.Collections.Generic.Stack<string>();

            //find the individual words and push it to the stack
            foreach (char t in inputStr)
            {
                bool isWordEnded = false;
                if (t != ' ')
                {
                    isWordStarted = true;
                    word += t;
                }
                else
                {
                    if (!isWordStarted) continue;
                    isWordEnded = true;
                    isWordStarted = false;
                }

                if (isWordEnded)
                {
                    stack.Push(word);
                    word = "";
                }
            }

            //reverse words
            while (stack.Count != 0)
            {
                output += stack.Pop();
                if (stack.Count != 0)
                {
                    output += " ";
                }
            }
            return output;
        }
        #endregion

        #region String Compression
        /// <summary>
        /// Given a string in the form of 'AAAABBBBCCCCCDDEEEE' compress it to become 'A4B4C5D2E4'. Fr this problem, you can falsely "compress" strings of single or double letters. For instance, it is ok for 'AAB' to return 'A2B1' even though this technically takes more space. The function should also be case sensitive, so that a string 'AAAaaa' returns 'A3a3'.
        /// </summary>
        /// <param name="inputStr"></param>
        /// <returns></returns>
        public static string CompressString(string inputStr)
        {
            int length = inputStr.Length;
            if (length == 0)
            {
                return "";
            }
            char current = inputStr[0];
            int count = 1;
            string output = "";
            for (int i = 1; i < length; i++) //start from i=1
            {
                if (inputStr[i] == current) count++;
                else
                {
                    output = output + current + count;
                    current = inputStr[i];
                    count = 1;
                }
            }
            //append the last char
            output = output + current + count;

            return output;
        }

        #endregion

        #region Unique Characters in String

        public static bool isUnique(string inputStr)
        {
            //Solution 1: using Hashset with build-in method
            //HashSet<char> set = new HashSet<char>(inputStr);
            //return set.Count == inputStr.Length;

            //Solution 2: manually using Hashset/Dictionary
            HashSet<char> set = new HashSet<char>();
            foreach (char c in inputStr)
            {
                if (set.Contains(c)) return false;
                else set.Add(c);
            }
            return true;
        }

        #endregion

        #region Check for balanced parentheses
        /// <summary>
        /// Given a string of opening and closing parentheses, check whether it’s balanced. We have 3 types of parentheses: round brackets: (), square brackets: [], and curly brackets: {}. Assume that the string doesn’t contain any other character than these, no spaces words or numbers. As a reminder, balanced parentheses require every opening parenthesis to be closed in the reverse order opened. For example ‘([])’ is balanced but ‘([)]’ is not. You can assume the input string has no spaces.
        /// </summary>
        /// <param name="inputStr"></param>
        /// <returns></returns>
        public static bool AreParenthesesBalanced(string inputStr)
        {
            //if the number is odd, directly return false
            if (inputStr.Length % 2 != 0) return false;

            System.Collections.Generic.Stack<char> stack = new System.Collections.Generic.Stack<char>();
            HashSet<char> leftBrackets = new HashSet<char>("([{");
            foreach (char c in inputStr)
            {
                if (leftBrackets.Contains(c)) stack.Push(c); //if current char is a left bracket, then push the it into the stack
                else //if a right bracket
                {
                    if (stack.Count == 0) return false; //make sure the first item should not be the right bracket, otherwise directly return false. Without this check, the loop will iterate the end, which is unnecessary.
                    char paired;
                    switch (c)
                    {
                        case ')': paired = '('; break;
                        case ']': paired = '['; break;
                        case '}': paired = '{'; break;
                        default: return false;
                    }
                    if (stack.Peek() == paired) stack.Pop(); // if matches, then pop stack
                    else return false; //if not match, return false
                }
            }

            return stack.Count == 0; //stack should be empty if passed the checking.
        }

        #endregion

        #region Implement Queue Using Two Stacks

        public class Queue<T>
        {
            private System.Collections.Generic.Stack<T> _inStack;
            private System.Collections.Generic.Stack<T> _outStack;
            public Queue()
            {
                _inStack = new System.Collections.Generic.Stack<T>();
                _outStack = new System.Collections.Generic.Stack<T>();
            }

            public int Count => _inStack.Count + _outStack.Count;

            public void Enqueue(T t)
            {
                _inStack.Push(t);
            }

            public T Dequeue()
            {
                if (!IsEmpty())
                {
                    if (_outStack.Count == 0)
                    {
                        while (_inStack.Count > 0)
                        {
                            _outStack.Push(_inStack.Pop());
                        }
                    }
                    return _outStack.Pop();
                }
                throw new Exception("The Queue Is Empty!");
            }

            public bool IsEmpty()
            {
                return Count == 0;
            }


        }

        #endregion

        #region Hall Locker

        public static int GetNumOfOpenedLocker(int numOfLockers)
        {
            bool[] array = new Boolean[numOfLockers];
            int n = 1;
            while (n <= numOfLockers)
            {
                //toggle array
                int index = n - 1;
                while (index < numOfLockers)
                {
                    array[index] = !array[index];
                    index += n;
                }
                n++;
            }
            int totalNumOfOpened = 0;
            foreach (var b in array)
            {
                if (b) totalNumOfOpened++;
            }
            return totalNumOfOpened;
        }



        #endregion

        #region Jumping Numbers
        /// <summary>
        /// Given a positive number x, print all Jumping Numbers smaller than or equal to x. A number is called as a Jumping Number if all adjacent digits in it differ by 1. The difference between ‘9’ and ‘0’ is not considered as 1. All single digit numbers are considered as Jumping Numbers. For example 7, 8987 and 4343456 are Jumping numbers but 796 and 89098 are not.
        /// </summary>
        /// <param name="inputNum"></param>
        public static void PrintJumpingNumbers(int inputNum)
        {
            for (int i = 0; i < 10; i++)
            {
                Print(i, inputNum);
            }
        }

        private static void Print(int start, int inputNum)
        {
            if (start > inputNum) return;
            if (start == 0)
            {
                Console.WriteLine(start);
                return;
            }
            Console.WriteLine(start);
            if (start % 10 == 0) Print(10 * start + start % 10 + 1, inputNum);
            else if (start % 10 == 9) Print(10 * start + start % 10 - 1, inputNum);
            else
            {
                Print(10 * start + start % 10 + 1, inputNum);
                Print(10 * start + start % 10 - 1, inputNum);
            }
        }

        #endregion


    }
}
