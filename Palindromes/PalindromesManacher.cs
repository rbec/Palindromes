using System.Collections.Generic;
using System.Linq;

namespace Palindromes
{
    public static class PalindromesManacher
    {
        private static int ExpandHalves(string s, int start, int end)
            => Expand(s, start / 2, end / 2);

        /// <summary>
        /// Receives the bounds of a known palindrome and incrementally grows it to maximum size
        /// </summary>
        /// <param name="s"></param>
        /// <param name="start">inclusive start index</param>
        /// <param name="end">exclusive end index</param>
        /// <returns>The length of the maximum palindrome</returns>
        private static int Expand(string s, int start, int end)
        {
            while (start > 0 && end < s.Length && s[start - 1] == s[end])
            {
                start--;
                end++;
            }

            return end - start;
        }

        private static int Reflect(this int point, int axis) => 2 * axis - point;

        /// <summary>
        /// An implementation of Manacher algorithm
        /// https://en.wikipedia.org/wiki/Longest_palindromic_substring
        /// </summary>
        /// <returns>Every unique palindrome ordered by centre</returns>
        public static IEnumerable<Range> Palindromes(string s)
        {
            if (s == null)
                yield break;

            // stores the lengths of the palindromes centred at a given index (or half index)
            // odd indices are the centre of characters and even indices are between
            var lengths = new int[s.Length * 2 + 1];

            // j is the index of the centre of the rightmost range found so far (in halves)
            var j = 0;
            for (var i = 1; i < lengths.Length; i++)
            {
                // k is the mirror image of i about the centre of the rightmost range (in halves)
                var k = i.Reflect(j);

                // jEnd is the right of the rightmost range (in havles)
                var jEnd = j + lengths[j];

                // l is the distance from i to the right of the rightmost range (in halves)
                var l = jEnd - i;

                // the centre of a palindrome at i may:
                // (a) be outside of the rightmost palindrome (l < 0)
                // (b) be within the rightmost palindrome (l >= 0), in which case the same palindrome will be centred at k.
                //     if the palindrome centred at k started after the leftmost point of the rightmost palindrome (l > lengths[k])
                //     we know that the palindrome at i must have the same length as the one at k.
                if (k >= 0 && l > lengths[k])
                {
                    lengths[i] = lengths[k];
                    continue;
                }

                // if we are outside the rightmost palindrome (l < 0) we start afresh looking for a palindrome centred at i.
                // otherwise there must be a palindrome that extends at least as far as the rightmost range.
                lengths[i] = l < 0
                                 ? ExpandHalves(s, i, i + 1)
                                 : ExpandHalves(s, jEnd.Reflect(i), jEnd);

                // check if we have found a new rightmost palindrome
                if (lengths[i] > l)
                    j = i;
            }

            for (var i = 0; i < lengths.Length; i++)
                if (lengths[i] > 0)
                    yield return new Range((i - lengths[i]) / 2, lengths[i]);
        }

        // using this to find a small number of palindromes in a long string is wasteful of resources
        // a better implementation might immediately discard any palindromes known to be too short
        public static IEnumerable<Range> LongestPalindromes(string s, int count) =>
            Palindromes(s).OrderByDescending(palindrome => palindrome.Length).Take(count);
    }
}