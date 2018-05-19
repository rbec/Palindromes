# Palindromes

An implementation of [Manacher algorithm](https://en.wikipedia.org/wiki/Longest_palindromic_substring) algorithm to find plaindromes.

## Introduction

A palindrome may be odd or even in length.
* Even length palindroms are centered *between* characters. e.g.

| A |   | B |
|---|---|---|
|   | ↑ |   |

| A |   | B |   | B |   | A |
|---|---|---|---|---|---|---|
|   |   |   | ↑ |   |   |   |

* Odd length palindromes are centered *on* a character e.g.

| A |
|---|
| ↑ |

| A |   | B |   | A |
|---|---|---|---|---|
|   |   | ↑ |   |   |

Let's define a unique index *j* for each possible palindrome in a string **S** with even length palindromes represented by even indices and odd length palindromes represented by odd indices. e.g. for a string of length 4 we have 9 indices:

|   | S₀|   | S₁|   | S₂|   | S₃|   |
|---|---|---|---|---|---|---|---|---|
| 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 |

And for a string of length ***n*** we have ***2n + 1*** indices.

The algorithm accepts a string of length ***n*** and returns an array of length ***2n + 1*** with the index *j* containing the right-hand side of the palindrome centered at *j*.

### Example
Consider the algorithm run on the string `ABBBABBBB`. This has 9 characters so the output will an array of 19 integers. The grey boxes in the grid below show the palindromes centered at each index (labelled blue). The three columns on the right show the range of the grey box. The algorithm will output the *right (ex)* column of numbers. This is an exclusive bound since it makes the arithmetic slightly simpler.

![alt text](https://github.com/rbec/Palindromes/blob/master/example.PNG)

### Simple Algorithm
Move through the centres from left to right. For each centre *i*:
* Initliase the right-hand side to the minimum it can be: `right[i] = (i + 1)/2`
* Then `left = i - right[i]`
  * When *i* is odd `left == right` giving a length of zero
  * When *i* is even `left + 1 == right` giving a length of one
  
``` C#
public static int[] Rights(string s)
{
    var rights = new int[s.Length * 2 + 1];

    for (var i = 1; i < rights.Length; i++)
    {
        rights[i] = (i + 1) / 2;

        var left = i - rights[i] - 1;
        while (left >= 0 && rights[i] < s.Length && s[left] == s[rights[i]])
        {
            left--;
            rights[i]++;
        }
    }

    return rights;
}
```
