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

The algorithm accepts a string of length ***n*** and returns an array of length ***2n + 1*** with the index *j* containing the length of the palindrome centered at *j*.

### Example

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
