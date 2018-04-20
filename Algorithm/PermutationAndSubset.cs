using System;
using System.Collections.Generic;

namespace Algorithm
{
    public class PermutationAndSubset
    {
        #region Subset

        /// <summary>
        /// Given a set of distinct integers, nums, return all possible subsets (the power set). Note: The solution set must not contain duplicate subsets.
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static List<List<int>> GetSubsets(int[] array)
        {
            var resultSubsets = new List<List<int>>();
            Array.Sort(array); //sort array first to prevent duplications 

            GetSubsetsHelper(resultSubsets, new List<int>(), array, 0);

            return resultSubsets;
        }

        private static void GetSubsetsHelper(List<List<int>> resultSubsets, List<int> subset, int[] array, int position)
        {
            resultSubsets.Add(new List<int>(subset)); // deep copy subset, add to resultList

            for (int i = position; i < array.Length; i++)
            {
                if (i > position && array[i] == array[i - 1]) continue; //if duplicate, skip

                subset.Add(array[i]);
                GetSubsetsHelper(resultSubsets, subset, array, i + 1);
                subset.RemoveAt(subset.Count - 1); //backtrack and build another one, and so on until you generate all n
            }
        }

        #endregion

        #region Permutation I

        public static List<List<int>> GetPermutationsI(int[] array)
        {
            Array.Sort(array);
            var resultList = new List<List<int>>();
            GetPermutationsIHelper(resultList, new List<int>(), array, new bool[array.Length]);
            return resultList;
        }

        private static void GetPermutationsIHelper(List<List<int>> resultList, List<int> permutation, int[] array,
            bool[] visited)
        {
            if (permutation.Count == array.Length)
            {
                resultList.Add(new List<int>(permutation)); //p.s. should new list
                return;
            }

            for (int i = 0; i < array.Length; i++)
            {
                if (visited[i] || (i > 0 && array[i] == array[i - 1] && !visited[i - 1]))
                    continue; // skip duplicates; array[i] == array[i - 1] can be used, because array has been sorted

                //if (permutation.Contains(array[i])) continue;
                permutation.Add(array[i]);
                visited[i] = true;

                GetPermutationsIHelper(resultList, permutation, array, visited);

                permutation.RemoveAt(permutation.Count - 1); // backtrack
                visited[i] = false; // backtrack
            }
        }

        #endregion

        #region Permutation II

        public static List<List<int>> GetPermutationsII(int[] array, int givenLength)
        {
            var resultList = new List<List<int>>();
            if (givenLength > array.Length) return resultList;
            Array.Sort(array);

            GetPermutationsIIHelper(resultList, new List<int>(), array, givenLength, new bool[array.Length]);
            return resultList;
        }

        private static void GetPermutationsIIHelper(List<List<int>> resultList, List<int> permutations, int[] array,
            int givenLength, bool[] visited)
        {
            if (permutations.Count == givenLength)
            {
                resultList.Add(new List<int>(permutations)); //p.s. should new List
                return;
            }

            for (int i = 0; i < array.Length; i++)
            {
                if (visited[i] || (i > 0 && array[i] == array[i - 1] && !visited[i - 1])) continue;

                visited[i] = true;
                permutations.Add(array[i]);

                GetPermutationsIIHelper(resultList, permutations, array, givenLength, visited);

                visited[i] = false;
                permutations.RemoveAt(permutations.Count - 1);
            }

        }


        #endregion

        #region N-queens

        public static List<string[]> SolveNQueens(int n)
        {
            var result = new List<string[]>();
            NQueensHelper(result, new List<int>(), n);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <param name="cols">store Q index(column number) for each row</param>
        /// <param name="n">number of steps on each row</param>
        public static void NQueensHelper(List<string[]> result, List<int> cols, int n)
        {
            //base case: recursion to the end, valid, then add to result.
            if (cols.Count == n)
            {
                result.Add(DrawChessBoard(cols));
                return;
            }

            for (int col = 0; col < n; col++)
            {
                if (!IsValid(cols, col)) continue;
                cols.Add(col);
                NQueensHelper(result, cols, n);
                cols.Remove(col);
            }
        }

        private static bool IsValid(List<int> cols, int col)
        {
            var row = cols.Count;
            for (int i = 0; i < row; i++)
            {
                int diffRows = row - i;
                if (cols[i] == col //same column
                    || cols[i] + diffRows == col //right-top to left-bottom
                    || cols[i] - diffRows == col //left-top to right-bottom
                ) return false;
            }
            return true;
        }

        private static string[] DrawChessBoard(List<int> cols)
        {
            var length = cols.Count;
            var result = new string[length];
            var i = 0;
            foreach (var col in cols)
            {
                string temp = "";
                for (int j = 0; j < length; j++)
                    temp += j == col ? 'Q' : '.';

                result[i] = temp;
                i++;
            }
            return result;
        }

        #endregion

        #region Restore IP Addresses

        /// <summary>
        /// Given a string containing only digits, restore it by returning all possible valid IP address combinations. For example: Given "25525511135", return ["255.255.11.135", "255.255.111.35"]. (Order does not matter)
        /// 一个有效的IP地址由4个数字组成，每个数字在0-255之间。对于其中的2位数或3位数，不能以0开头。
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static List<string> GetAllPossibleIPAdresses(string str)
        {
            var resultList = new List<string>();
            if (str.Length > 12)
            {
                return resultList;
            }

            GetAllPossibleIPAdressesHelper(resultList, new List<string>(), str, 4);
            return resultList;
        }

        private static void GetAllPossibleIPAdressesHelper(List<string> resultList, List<string> fragments, string str,
            int count)
        {
            if (str.Length == 0 && count == 0) // str has no substring and fragments has been calculated 4 times
            {
                if (fragments.Count == 4) // if has 4 valid fragments, then add the result list
                {
                    resultList.Add(ConvertToString(fragments));
                }
                return;
            }

            for (int j = 1; j <= 3; j++) //ip fragment can be 1, 2 or 3 digits
            {
                if (str.Length >= j)
                {
                    string digits = str.Substring(0, j);
                    if (!IsValidDigits(digits)) continue;

                    fragments.Add(digits);
                    GetAllPossibleIPAdressesHelper(resultList, fragments, str.Substring(j), count - 1);
                    fragments.RemoveAt(fragments.Count - 1);
                }
            }
        }

        private static string ConvertToString(List<string> fragments)
        {
            var ipAddress = "";
            for (int i = 0; i < 4; i++)
            {
                ipAddress += i != 3 ? fragments[i] + "." : fragments[i];
            }
            return ipAddress;
        }

        private static bool IsValidDigits(string digits)
        {
            if (digits.Length <= 3 && digits.Length >= 1)
            {
                if (digits.Length == 1) return true; // one digit, can be any number

                if (digits.Length == 3 || digits.Length == 2)
                {
                    if (digits[0] == '0') return false; //first digit should not be 0
                    if (Convert.ToInt32(digits) > 255) return false; //should be less than 255
                    return true;
                }

            } // 1 <= number <=3


            return false;
        }


        #endregion

        #region Letter Combinations of a Phone Number

        private static Dictionary<char, List<char>> digitToLetterDic = new Dictionary<char, List<char>>()
        {
            {'1', new List<char>() {'1'}},
            {'2', new List<char>() {'a', 'b', 'c'}},
            {'3', new List<char>() {'d', 'e', 'f'}},
            {'4', new List<char>() {'g', 'h', 'i'}},
            {'5', new List<char>() {'j', 'k', 'l'}},
            {'6', new List<char>() {'m', 'n', 'o'}},
            {'7', new List<char>() {'p', 'q', 's'}},
            {'8', new List<char>() {'t', 'u', 'v'}},
            {'9', new List<char>() {'w', 'x', 'y', 'z'}},
            {'0', new List<char>() {' '}},
        };

        /// <summary>
        /// Given a digit string, return all possible letter combinations that the number could represent. A mapping of digit to letters(just like on the telephone buttons) is given below. Input:Digit string "23" Output: ["ad", "ae", "af", "bd", "be", "bf", "cd", "ce", "cf"].
        /// </summary>
        /// <returns></returns>
        public static List<string> GetLetterCombinations(string inputStr)
        {
            List<string> resultList = new List<string>();
            if (inputStr.Length == 0)
                return resultList;
            GetLetterCombinationsHelper(resultList, new List<char>(), inputStr, 0);
            return resultList;
        }

        private static void GetLetterCombinationsHelper(List<string> resultList, List<char> list, string inputStr,
            int position
        )
        {
            if (position > inputStr.Length - 1)
            {
                var str = "";
                foreach (var c in list)
                {
                    str += c;
                }
                resultList.Add(str);
                return;
            }

            foreach (var c in digitToLetterDic[inputStr[position]])
            {
                list.Add(c);
                GetLetterCombinationsHelper(resultList, list, inputStr, position + 1);
                list.RemoveAt(list.Count - 1);
            }
        }

        #endregion

        #region Combination Sum II

        /// <summary>
        /// Given a collection of candidate numbers (C) and a target number (T), find all unique combinations in C where the candidate numbers sums to T. Each number in C may only be used once in the combination. Note: All numbers (including target) will be positive integers. The solution set must not contain duplicate combinations. For example, given  candidate set [10, 1, 2, 7, 6, 1, 5] and target 8, [ [1, 7], [1, 2, 5], [2, 6], [1, 1, 6] ]
        /// </summary>
        /// <param name="numbers"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static List<List<int>> FindCombinationSumII(int[] numbers, int target)
        {
            var resultList = new List<List<int>>();
            if (numbers.Length == 0) return resultList;
            Array.Sort(numbers);
            FindCombinationSumIIHelper(resultList, new HashSet<string>(), new List<int>(), numbers, 0, target,
                new bool[numbers.Length]);
            return resultList;
        }

        private static void FindCombinationSumIIHelper(List<List<int>> resultList, HashSet<string> hash,
            List<int> numList, int[] numbers, int position, int target, bool[] visited)
        {
            if (target == 0 && numList.Count <= numbers.Length)
            {
                var code = "";
                foreach (var i in numList)
                {
                    code += i + "-";
                }

                //in case of duplication
                if (hash.Contains(code)) return;

                resultList.Add(new List<int>(numList));
                hash.Add(code);
                return;

            }

            for (int i = position; i < numbers.Length; i++)
            {
                if (visited[i] ||
                    numbers[i] > target)
                {
                    continue;
                }

                visited[i] = true;
                numList.Add(numbers[i]);
                FindCombinationSumIIHelper(resultList, hash, numList, numbers, i, target - numbers[i], visited);
                numList.RemoveAt(numList.Count - 1);
                visited[i] = false;
            }

        }

        #endregion

        #region Combination Sum I

        /// <summary>
        /// Given a set of candidate numbers (C) (without duplicates) and a target number (T), find all unique combinations in C where the candidate numbers sums to T. The same repeated number may be chosen from C unlimited number of times. Note: All numbers(including target) will be positive integers. The solution set must not contain duplicate combinations. For example, given candidate set[2, 3, 6, 7] and target 7, A solution set is: [[7], [2, 2, 3]]
        /// </summary>
        /// <param name="numbers"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static List<List<int>> FindCombinationSumI(int[] numbers, int target)
        {
            Array.Sort(numbers); // in case duplicate
            var resultList = new List<List<int>>();
            FindCombinationSumIHelper(resultList, new List<int>(), numbers, target);
            return resultList;
        }

        private static void FindCombinationSumIHelper(List<List<int>> resultList, List<int> list, int[] numbers,
            int target)
        {
            if (target == 0)
            {
                resultList.Add(new List<int>(list));
            }

            foreach (int number in numbers)
            {
                if (number > target) continue;
                if (list.Count > 0)
                {
                    if (list[list.Count - 1] > number)
                        continue; //never choose smaller number in case of duplication since the array is sorted
                }

                list.Add(number);
                FindCombinationSumIHelper(resultList, list, numbers, target - number);
                list.RemoveAt(list.Count - 1);
            }
        }

        #endregion

        #region Word Break II

        /// <summary>
        /// Given a non-empty string s and a dictionary wordDict containing a list of non-empty words, add spaces in s to construct a sentence where each word is a valid dictionary word. You may assume the dictionary does not contain duplicate words. Return all such possible sentences. For example, given s = "catsanddog", dict = ["cat", "cats", "and", "sand", "dog"]. A solution is ["cats and dog", "cat sand dog"].
        /// </summary>
        /// <param name="inputStr"></param>
        /// <param name="wordDic"></param>
        /// <returns></returns>
        public static List<string> GetSentences(string inputStr, List<string> wordDic)
        {
            var maxLength = wordDic[0].Length;
            foreach (var word in wordDic)
            {
                maxLength = Math.Max(maxLength, word.Length);
            }
            var resultList = new List<List<string>>();
            GetSentencesHelper(resultList, new List<string>(), wordDic, inputStr, maxLength);
            return ConvertToStrList(resultList);
        }

        private static void GetSentencesHelper(List<List<string>> resultList, List<string> list, List<string> wordDic,
            string inputStr, int maxWordLength)
        {
            if (String.IsNullOrEmpty(inputStr))
            {
                resultList.Add(new List<string>(list));
                return;
            }

            //find all possible starting words
            var len = 1;
            List<string> words = new List<string>();
            while (len <= maxWordLength && len <= inputStr.Length)
            {
                var temp = inputStr.Substring(0, len);
                if (!wordDic.Contains(temp))
                {
                    len++;
                    continue;
                }
                words.Add(temp);
                len++;
            }

            //if not found any
            if (words.Count == 0) return;

            //DFS
            foreach (var word in words)
            {
                list.Add(word);
                GetSentencesHelper(resultList, list, wordDic, inputStr.Substring(word.Length), maxWordLength);
                list.RemoveAt(list.Count - 1);
            }
        }

        private static List<string> ConvertToStrList(List<List<string>> resultList)
        {
            var strList = new List<string>();
            foreach (var list in resultList)
            {
                var temp = "";
                for (var i = 0; i < list.Count; i++)
                {
                    temp += i == list.Count - 1 ? list[i] : list[i] + " ";
                }
                strList.Add(temp);
            }

            return strList;
        }

        #endregion

        #region Palindrome Partitioning

        /// <summary>
        /// Given a string s, partition s such that every substring of the partition is a palindrome. Return all possible palindrome partitioning of s. For example, given s = "aab", Return [["aa", "b"], ["a","a","b"]]
        /// </summary>
        /// <param name="inputStr"></param>
        /// <returns></returns>
        public static List<List<string>> GetAllPalindrome(string inputStr)
        {
            var resultList = new List<List<string>>();

            GetAllPalindromeHelper(resultList, new List<string>(), inputStr);

            return resultList;
        }

        private static void GetAllPalindromeHelper(List<List<string>> resultList, List<string> list, string inputStr)
        {
            if (inputStr.Length == 0)
            {
                resultList.Add(new List<string>(list));
                return;
            }

            for (int i = 1; i <= inputStr.Length; i++) //test substring from length of 1 to total length
            {
                var str = inputStr.Substring(0, i);
                if (!IsPalindrome(str)) continue;
                list.Add(str);
                GetAllPalindromeHelper(resultList, list, inputStr.Substring(i));
                list.RemoveAt(list.Count - 1);
            }

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

        #region Path Sum II
        /// <summary>
        /// Given a binary tree and a sum, find all root-to-leaf paths where each path's sum equals the given sum. For example, given the below binary tree and sum = 22,
        ///       5
        ///      / \
        ///     4   8
        ///    /   / \
        ///   11  13  4
        ///  /  \    / \
        /// 7    2  5   1
        /// the method returns the following:[[5,4,11,2],[5,8,4,5]]
        /// </summary>
        public class TreeNode
        {
            public int val;
            public TreeNode left;
            public TreeNode right;

            public TreeNode(int x)
            {
                val = x;
            }
        }

        public static List<List<int>> PathSum(TreeNode root, int sum)
        {
            var resultList = new List<List<int>>();
            if (root == null) return resultList;
            PathSumHelper(resultList, new List<int>() { root.val }, root, sum);   //different from other questions, list has initial value
            return resultList;
        }

        private static void PathSumHelper(List<List<int>> resultList, List<int> list, TreeNode root, int sum)
        {
            if (sum == root.val && root.right == null && root.left == null)
            {
                resultList.Add(new List<int>(list));
                return;
            }
            if (sum < root.val) return;


            if (root.left != null)
            {
                list.Add(root.left.val);
                PathSumHelper(resultList, list, root.left, sum - root.val);
                list.RemoveAt(list.Count - 1);
            }


            if (root.right != null)
            {
                list.Add(root.right.val);
                PathSumHelper(resultList, list, root.right, sum - root.val);
                list.RemoveAt(list.Count - 1);
            }
        }

        #endregion
    }
}
