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


                foreach (var range in PalindromesManacher.AllPalindromes(s))
                {
                    Console.WriteLine($"Text: {s.Substring(range.Index, range.Length)}, {range}");
                }

                Console.WriteLine();
            }
        }
    }
}