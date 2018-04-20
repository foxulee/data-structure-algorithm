using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Algorithm
{
    //Clues: when to use DP
    //1. Find a max/min result
    //2. Decide whether something is possible or not
    //3. Count all possible solutions
    //*problem doesn't care about the solution details, only care abut the count or possibility
    public class DynamicProgramming
    {
        #region Single Sequence DP

        #region Climbing Stairs

        private static Dictionary<int, int> climbStairsDic = new Dictionary<int, int>();
        /// <summary>
        /// You are climbing a stair case. It takes n steps to reach to the top. Each time you can either climb 1 or 3 steps. In how many distinct ways you climb to the top?
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int ClimbStairs(int n)
        {
            if (n == 1) return 1;
            if (n == 2) return 2;

            if (!climbStairsDic.ContainsKey(n))
                climbStairsDic.Add(n, ClimbStairs(n - 1) +  // climb 1 step from n-1 
                                      ClimbStairs(n - 2));  // climb 2 steps from n-2
            return climbStairsDic[n];
        }

        #endregion

        #region Jump Game
        private static Dictionary<int, bool> canJumpDic = new Dictionary<int, bool>();
        public static bool CanJumpToEnd(int[] array)
        {
            return CanJumpToEndHelper(array, 0);
        }

        private static bool CanJumpToEndHelper(int[] array, int index)
        {
            if (index == array.Length - 1) return true;
            if (array[index] == 0) return false;
            if (index >= array.Length) return false;

            if (!canJumpDic.ContainsKey(index))
            {
                for (int i = 1; i <= array[index]; i++)
                {
                    if (CanJumpToEndHelper(array, index + i))
                    {
                        canJumpDic.Add(index, true);
                        return true;
                    }
                }
            }
            return false;
        }

        #endregion

        #region Longest Increasing Subsequence 【Important!】

        private static Dictionary<int, int> lisDic = new Dictionary<int, int>();
        /// <summary>
        /// find the length of the longest subsequence of a given sequence such that all elements of the subsequence are sorted in increasing order. For example, the length of LIS for {10, 22, 9, 33, 21, 50, 41, 60, 80} is 6 and LIS is {10, 22, 33, 50, 60, 80}.
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static int LIS(int[] array)
        {
            if (array.Length == 1) return 1;
            if (array.Length == 0) return 0;
            return LISHelper(array, 0);
        }

        private static int LISHelper(int[] array, int startIndex)
        {
            if (startIndex > array.Length - 1) return 0;
            if (startIndex == array.Length - 1) return 1;

            if (lisDic.ContainsKey(startIndex)) return lisDic[startIndex];
            int nextStartIndex = FindGreatIndex(array, startIndex);
            if (nextStartIndex == -1) lisDic.Add(startIndex, 1); // can not find a greater value after startIndex
            else
            {
                var value = Math.Max(LISHelper(array, startIndex + 1),    //not count startIndex, calculate startIndex + 1
                                     LISHelper(array, nextStartIndex) + 1);  //count startIndex as 1, add to the result from nextStartIndex
                lisDic.Add(startIndex, value);
            }
            return lisDic[startIndex];
        }

        private static int FindGreatIndex(int[] array, int startIndex)
        {
            for (int i = startIndex + 1; i < array.Length; i++)
                if (array[i] >= array[startIndex])
                    return i;
            return -1;
        }

        #endregion

        #region Word Break
        /// <summary>
        /// Given a non-empty string s and a dictionary wordDict containing a list of non-empty words, determine if s can be segmented into a space-separated sequence of one or more dictionary words. You may assume the dictionary does not contain duplicate words. For example, given s = "leetcode", dict = ["leet", "code"]. Return true because "leetcode" can be segmented as "leet code".
        /// </summary>
        /// <param name="inputStr"></param>
        /// <param name="wordsDic"></param>
        /// <returns></returns>
        public static bool CanBeSeperated(string inputStr, HashSet<string> wordsDic)
        {
            if (wordsDic.Contains(inputStr)) return true;

            for (int i = 1; i < inputStr.Length; i++)
            {
                if (wordsDic.Contains(inputStr.Substring(0, i)))
                    if (CanBeSeperated(inputStr.Substring(i), wordsDic))
                        return true;
            }
            return false;
        }

        #endregion

        #region Palindrome Partitioning
        //int stores the min number for the specific string
        private static Dictionary<string, int> palindromeDic = new Dictionary<string, int>();

        /// <summary>
        /// Given a string s, partition s such that every substring of the partition is a palindrome. Retrurn the minimum cuts needed for a palindrome partitioning of s.
        /// </summary>
        /// <param name="inputStr"></param>
        /// <returns></returns>
        public static int MinCutForPalindromePartition(string inputStr)
        {
            if (IsPalindrome(inputStr)) return 0;

            if (!palindromeDic.ContainsKey(inputStr))
            {
                var min = inputStr.Length - 1;
                for (int i = 1; i < inputStr.Length; i++)
                {
                    min = Math.Min(min,
                                   1 +
                                   MinCutForPalindromePartition(inputStr.Substring(0, i)) +
                                   MinCutForPalindromePartition(inputStr.Substring(i)));
                }
                palindromeDic.Add(inputStr, min);
            }

            return palindromeDic[inputStr];
        }

        private static bool IsPalindrome(string str)
        {
            for (int i = 0, j = str.Length - 1; i <= j; i++, j--)
            {
                if (str[i] != str[j]) return false;
            }
            return true;
        }

        #endregion

        #endregion

        #region Two Sequence Dp

        #region Edit Distance

        private static Dictionary<string, int> minEditDistanceDic = new Dictionary<string, int>();
        /// <summary>
        /// Give two words word1 and word2, find the minimum number of steps required to convert word1 to word2.(each operation is counted as 1 step). You have the following 3 operations permitted on a word: a) insert a character b) delete a character c) replace a character
        /// </summary>
        /// <param name="word1"></param>
        /// <param name="word2"></param>
        /// <returns></returns>
        public static int MinEditDistance(string word1, string word2)
        {
            //base case
            if (word1 == null || word2 == null) return -1;
            if (word1 == word2) return 0;
            if (word1 == "") return word2.Length;
            if (word2 == "") return word1.Length;

            var key = word1 + "-" + word2;
            if (!minEditDistanceDic.ContainsKey(key))
            {
                if (word1[0] == word2[0])
                    return MinEditDistance(word1.Substring(1), word2.Substring(1));

                var value = 1 + Math.Min(Math.Min(MinEditDistance(word1.Substring(1), word2.Substring(1)),   //word1 replace first char
                                                  MinEditDistance(word1, word2.Substring(1))),               //word1 insert first char
                                         MinEditDistance(word1.Substring(1), word2));                        //word1 delete first char
                minEditDistanceDic.Add(key, value);
            }
            return minEditDistanceDic[key];
        }

        #endregion

        #region Interleaving String
        /// <summary>
        /// Given str, s2, s3, find whether s3 is formed by the interleaving of str and s2. For example, str = "aabcc", s2 = "dbbca", when s3 = "aadbbcbcac", return true. When s3 = "aadbbbaccc", return false;
        /// </summary>
        /// <param name="testStr"></param>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        public static bool IsInterleavingString(string s1, string s2, string testStr)
        {
            if (s1 == null || s2 == null || testStr == null) return false;
            if (s1 + s2 == testStr || s2 + s1 == testStr) return true;
            if ((s1 == "" && s2 != testStr) || (s2 == "" && s1 != testStr)) return false;
            if (s1.Length + s2.Length != testStr.Length) return false;
            if (s1[0] != testStr[0] && s2[0] != testStr[0]) return false;


            if (s1[0] == s2[0]) return IsInterleavingString(s1.Substring(1), s2.Substring(1), testStr.Substring(2));
            if (s1[0] == testStr[0])
            {
                int index = FindRestartIndex(s1, testStr);
                return IsInterleavingString(s1.Substring(index), s2, testStr.Substring(index));
            }
            else //s2[0] == testStr[0]
            {
                int index = FindRestartIndex(s2, testStr);
                return IsInterleavingString(s1, s2.Substring(index), testStr.Substring(index));
            }
        }

        /// <summary>
        /// return the index of the first different char between two string 
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        private static int FindRestartIndex(string s1, string s2)
        {
            int i = 0;
            for (; i < s1.Length && i < s2.Length; i++)
                if (s1[i] != s2[i]) return i;
            return i;
        }


        #endregion

        #region Longest Common Subsequence (LCS) 【Important!!!】
        private static Dictionary<string, int> lcsDic = new Dictionary<string, int>();

        /// <summary>
        /// Given two strings, find the longest common subsequence (LCS). LCS for input Sequences “ABCDGH” and “AEDFHR” is “ADH” of length 3. LCS for input Sequences “AGGTAB” and “GXTXAYB” is “GTAB” of length 4.
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        public static int LCS(string s1, string s2)
        {
            if (String.IsNullOrEmpty(s1) || String.IsNullOrEmpty(s2)) return 0;

            //find first match char
            FindFirstMatchIndexes(s1, s2, out var indexInS1, out var matchIndexInS2);
            if (indexInS1 == s1.Length || matchIndexInS2 == -1) return 0;

            string key = s1 + "-" + s2;
            if (!lcsDic.ContainsKey(key))
            {
                var value = Math.Max(LCS(s1.Substring(indexInS1 + 1), s2.Substring(matchIndexInS2 + 1)) + 1,   //choose match
                                     LCS(s1.Substring(indexInS1 + 1), s2));               //choose not match
                lcsDic.Add(key, value);
            }
            return lcsDic[key];
        }

        private static void FindFirstMatchIndexes(string s1, string s2, out int indexInS1, out int matchIndexInS2)
        {
            indexInS1 = 0;
            matchIndexInS2 = -1;
            for (; indexInS1 < s1.Length; indexInS1++)
            {
                matchIndexInS2 = FindMatchIndex(s1[indexInS1], s2);
                if (matchIndexInS2 != -1) return;
            }
        }

        private static int FindMatchIndex(char c, string str)
        {
            for (int i = 0; i < str.Length; i++)
                if (c == str[i]) return i;
            return -1;
        }

        #endregion

        #endregion

        #region Matrix DP

        #region Minimum Path Sum in Triangle
        /// <summary>
        /// Given a triangle, find the minimum path sum from top to bottom. Each step you may move to adjacent numbers on the row below.
        /// </summary>
        /// <param name="arrays"></param>
        /// <returns></returns>
        public static int FindMinPathSum(int[][] arrays)
        {
            if (arrays.Length == 0) return 0;
            //memoization:
            Dictionary<int, int> triangleDic = new Dictionary<int, int>();

            return FindMinPathSumHelper(arrays, 0, 0, triangleDic);
        }

        private static int FindMinPathSumHelper(int[][] arrays, int row, int col, Dictionary<int, int> dic)
        {
            //base case
            if (row == arrays.Length - 1) return arrays[row][col];

            //recursion
            var key = row * 10 + col;
            if (!dic.ContainsKey(key))
            {
                var result = arrays[row][col] + Math.Min(
                                 FindMinPathSumHelper(arrays, row + 1, col, dic),
                                 FindMinPathSumHelper(arrays, row + 1, col + 1, dic)
                             );
                dic.Add(key, result);
            }
            return dic[key];
        }

        #endregion

        #region Minimum Path Sum in grid

        private static Dictionary<string, int> minSumGridDic = new Dictionary<string, int>();
        /// <summary>
        /// Given a m*n grid filled with non-negative numbers, find a path from top left to bottom right which minimized the sum of all numbers along its path. You can only move either down or right at any point in time.
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public static int MinPathSumInGrid(int[][] grid)
        {
            return MinPathSumInGridHelper(grid, 0, 0);
        }

        private static int MinPathSumInGridHelper(int[][] grid, int row, int col)
        {
            if (row == grid.Length - 1 && col == grid[0].Length - 1) return grid[row][col];

            string key = $"{row}-{col}";
            if (!minSumGridDic.ContainsKey(key))
            {
                if (row == grid.Length - 1 && col != grid[0].Length - 1)
                    minSumGridDic.Add(key, grid[row][col] + MinPathSumInGridHelper(grid, row, col + 1));

                else if (row != grid.Length - 1 && col == grid[0].Length - 1)
                    minSumGridDic.Add(key, grid[row][col] + MinPathSumInGridHelper(grid, row + 1, col));

                else
                    minSumGridDic.Add(key, grid[row][col] + Math.Min(MinPathSumInGridHelper(grid, row, col + 1),
                                               MinPathSumInGridHelper(grid, row + 1, col)));
            }
            return minSumGridDic[key];
        }

        #endregion

        #region Unique Paths
        private static Dictionary<string, int> uniquePathDic = new Dictionary<string, int>();

        /// <summary>
        /// A robot is located at the top-left corner of a m*n grid. The robot can only move either down or right at any point in time. The robot is trying to reach the bottom-right corner of the grid. How many possible unique paths are there?
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public static int TotalNumberOfUniquePath(int row, int col)
        {
            if (row <= 0 || col <= 0) return 0;
            if (row == 1 || col == 1) return 1;

            string key = $"{row}-{col}";
            if (!uniquePathDic.ContainsKey(key))
                uniquePathDic.Add(key, TotalNumberOfUniquePath(row - 1, col) + TotalNumberOfUniquePath(row, col - 1));
            return uniquePathDic[key];
        }

        #endregion

        #endregion
    }
}