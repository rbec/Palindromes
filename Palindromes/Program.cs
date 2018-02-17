using System;

namespace Palindromes
{
    internal class Program
    {
        private static void Main()
        {
            while (true)
            {
                Console.WriteLine("Enter a string to search for palindromes or 'q' to exit:");
                var s = Console.ReadLine();
                if (s == "q")
                    return;
                Console.Write("# to find: ");

                int n;
                var nStr = Console.ReadLine();
                while (!int.TryParse(nStr, out n) || n < 1)
                {
                    if (nStr == "q")
                        return;
                    Console.Write("Invalid number. Please enter again: ");
                    nStr = Console.ReadLine();
                }

                foreach (var range in PalindromesManacher.LongestPalindromes(s, n))
                {
                    Console.WriteLine($"Text: {s.Substring(range.Index, range.Length)}, {range}");
                }

                Console.WriteLine();
            }
        }
    }
}