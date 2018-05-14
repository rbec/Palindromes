using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Palindromes
{
    public static class Tests
    {
        public static bool IsPalindrome(this string s)
        {
            for (var i = 0; i < s.Length; i++)
                if (s[i] != s[s.Length - i - 1])
                    return false;
            return true;
        }

        public static IEnumerable<Range> Ranges(int length)
        {
            for (var i = 0; i < length; i++)
                for (var j = 1; j <= length - i; j++)
                    yield return new Range(i, j);
        }

        /// <summary>
        /// A very inefficient naive implementation that simply examines every possible substring of a string and checks
        /// if it is a palindrome
        /// </summary>
        public static IEnumerable<Range> Palindromes(string s)
        {
            return Ranges(s.Length) // get every possible substring
                   .Where(range => s.Substring(range.Index, range.Length).IsPalindrome()) // check if its a palindrome
                   .GroupBy(
                       range => 2 * range.Index + range.Length, // discard non-unique palindromes with the same centre index
                       (centre, ranges) => ranges.OrderByDescending(range => range.Length).First()); // except for the longest one
        }

        [Test]
        public static void TestIsPalindrome()
        {
            Assert.True("".IsPalindrome());
            Assert.True("A".IsPalindrome());
            Assert.True("AA".IsPalindrome());
            Assert.True("AAA".IsPalindrome());
            Assert.True("ABA".IsPalindrome());
            Assert.False("ABC".IsPalindrome());
            Assert.False("AB".IsPalindrome());
        }

        [Test]
        public static void TestSubstrings()
        {
            Assert.That(Ranges(3), Is.EquivalentTo(new[] {new Range(0, 1), new Range(1, 1), new Range(2, 1), new Range(0, 2), new Range(1, 2), new Range(0, 3)}));
            Assert.That(Ranges(4).Count(), Is.EqualTo(4 * 5 / 2));
            Assert.That(Ranges(4), Is.Unique);
            Assert.That(Ranges(5).Count(), Is.EqualTo(5 * 6 / 2));
            Assert.That(Ranges(5), Is.Unique);
        }

        [Test]
        public static void TestPalindrome1()
        {
            var s = "sqrrqabccbatudefggfedvwhijkllkjihxymnnmzpop";
            Assert.That(PalindromesManacher.AllPalindromes(s)
                                           .OrderByDescending(range => range.Length)
                                           .Take(3), Is.EquivalentTo(new[] {new Range(23, 10), new Range(13, 8), new Range(5, 6)}));
        }

        [Test]
        public static void TestPalindrome2()
        {
            var s = "";
            Assert.That(PalindromesManacher.LongestPalindromes(s), Is.EqualTo(new Range(0, 0)));
        }

        [Test]
        public static void TestPalindrome3()
        {
            Assert.Throws<ArgumentNullException>(() => PalindromesManacher.LongestPalindromes(null));
        }

        [Test]
        public static void TestPalindrome4()
        {
            var s = "AAAAAAAAAA";
            Assert.That(PalindromesManacher.LongestPalindromes(s), Is.EqualTo(new Range(0, 10)));
        }

        /// <summary>
        /// Randomly generates strings and compares the results of the 2 plaindrome finding algorithms
        /// </summary>
        [Test]
        public static void TestPalindromesEqualsPalindromesSlow()
        {
            var random = new Random();
            for (var i = 0; i < 1000; i++)
            {
                var s = GenerateString(random, random.Next(10), 100);
                Assert.That(PalindromesManacher.AllPalindromes(s), Is.EquivalentTo(Palindromes(s)));
            }
        }

        /// <summary>
        /// generates a random string of random length of in specified base
        /// </summary>
        public static string GenerateString(Random random, int @base, int maxLength)
        {
            var chars = new char[random.Next(maxLength)];
            for (var i = 0; i < chars.Length; i++)
                chars[i] = (char) ('0' + random.Next(@base));
            return new string(chars);
        }
    }
}