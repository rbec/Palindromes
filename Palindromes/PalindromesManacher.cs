using System;
using System.Collections.Generic;

namespace Palindromes
{
    public static class PalindromesManacher
    {
        /// <summary>
        /// An implementation of Manacher algorithm
        /// https://en.wikipedia.org/wiki/Longest_palindromic_substring
        /// </summary>
        /// <returns>The rightmost character of each palindrome ordered by centre</returns>
        private static int[] Rights(string s)
        {
            if (s == null)
                throw new ArgumentNullException(nameof(s));

            var right = new int[s.Length * 2 + 1];

            var rightmost = 0;
            for (var i = 1; i < right.Length; i++)
            {
                if (i < 2 * right[rightmost])
                {
                    // we are inside the rightmost palindrome so reuse knowledge of the
                    // mirror palindrome about the centre of the rightmost palindrome
                    right[i] = Math.Min(right[rightmost], right[2 * rightmost - i] + i - rightmost);
                }
                else
                    right[i] = i / 2; // we are outside the right most palindrome so start with immediate right

                var left = i - right[i] - 1;
                while (left >= 0 && right[i] < s.Length && s[left] == s[right[i]]) // grow the palindrome to the maximum extent
                {
                    left--;
                    right[i]++;
                }

                if (right[i] > right[rightmost])
                    rightmost = i; // we have found a new rightmost palindrome
            }

            return right;
        }

        public static IEnumerable<Range> AllPalindromes(string s)
        {
            var right = Rights(s);
            for (var i = 1; i < right.Length; i++)
                if (2 * right[i] > i)
                    yield return new Range(i - right[i], 2 * right[i] - i);
        }

        public static Range LongestPalindromes(string s)
        {
            var longest = 0;
            var right = Rights(s);
            for (var i = 1; i < right.Length; i++)
                if (2 * right[i] - i > 2 * right[longest] - longest)
                {
                    longest = i;
                }
            return new Range(longest - right[longest], 2 * right[longest] - longest);
        }
    }
}