# Palindromes

An implementation of [Manacher algorithm](https://en.wikipedia.org/wiki/Longest_palindromic_substring) algorithm to find palindromes.

## Introduction

A [palindrome](https://en.wikipedia.org/wiki/Palindrome) may be odd or even in length.
* Even length palindroms are centered *between* characters. e.g.

![alt text](https://github.com/rbec/Palindromes/blob/master/example_even_length.PNG)

* Odd length palindromes are centered *on* a character e.g.

![alt text](https://github.com/rbec/Palindromes/blob/master/example_odd_length.PNG)

Let's define a unique index for each possible palindrome in a string with even length palindromes represented by even indices and odd length palindromes represented by odd indices. e.g. for a string of length 4 we have 9 indices (labelled in blue):

![alt text](https://github.com/rbec/Palindromes/blob/master/example_indexes.PNG)

And for a string of length ***n*** we have ***2n + 1*** indices.

The algorithm accepts a string of length ***n*** and returns an array of length ***2n + 1*** with the index ***i*** containing the right-hand side of the longest palindrome centered at ***i***.

### Example
Consider the algorithm run on the string `ABBBABBBB`. This has 9 characters so the output will an array of 19 integers. The grey boxes in the grid below show the (longest) palindromes centered at each index (labelled blue). The three columns on the right show the range of the grey box as indices (0 - 8) in the original string shown in red at the top. The algorithm will output the *right (ex)* column of numbers. This is an exclusive bound since it makes the arithmetic slightly simpler.

![alt text](https://github.com/rbec/Palindromes/blob/master/example.PNG)

## Simple Approach

### Algorithm
For a string `s`:
1. Initialise an array `right` of length `2*s.Length + 1`
2. For each centre `i` from `1..2*s.Length`:
   * Initialise our starting guess to `right[i] = (i + 1)/2` and `left = i - right[i]`
     * When `i` is even `left == right` giving a length of zero
     * When `i` is odd `left + 1 == right` giving a length of one
   * Incrementally grow the palindrome to the maximum extent
3. Return `right`

This is **O**(***n²***).

### Implementation
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
## Manacher's Approach
Manacher's insight was that a palindrome, whose centre at ***i*** is *within* another palindrome centred at ***j***, will have an identical mirror palindrome centred at ***i*** reflected about ***j***.

### Example
Since the algorithm works left to right, by the time we reach ***i*** we will know that we are within a palindrome centred at ***j*** and hence that there is a palindrome at ***i*** that has the same length as the one centred at ***2 j - i*** (the mirror image of ***i*** about ***j***).

![alt text](https://github.com/rbec/Palindromes/blob/master/example_mirror.PNG)

It is possible that the palindrome extends further than it's mirror image, hence as before we can grow the palindrome to it's maximum extent, but starting with a better initial guess.

We therefore need to keep track of the right-most palindrome so far discovered.

### Algorithm
For a string `s`:
1. Initialise an array `right` of length `2*s.Length + 1`
2. Initialise a variable `rightmost = 0` that is the centre of the palindrome with the right-most right-hand side so far discovered
3. For each centre `i` from `1..2*s.Length`:
   * Initialise our starting guess:
     * If `i` is outside the right-most right-hand side (`2*i >= right[rightmost]`) we know nothing about any palindromes centred at `i` so use the simple algorithm `right[i] = (i + 1)/2`
     * Otherwise (`2*i < right[rightmost]`) we are inside a palindrome `Min(right[rightmost], right[2*rightmost - i] + i - rightmost)`
   * Incrementally grow the palindrome to the maximum extent
   * If this palindrome has a right-most extent is greater than the previous right-most palindrome, set `rightmost = i`
4. Return `right`

This is **O**(***n***).

### Implementation
``` C#
public static int[] Rights(string s)
{
    var right = new int[s.Length * 2 + 1];

    var rightmost = 0;
    for (var i = 1; i < right.Length; i++)
    {
        if (i < 2 * right[rightmost])
        {
            right[i] = Math.Min(right[rightmost], right[2 * rightmost - i] + i - rightmost);
        }
        else
            right[i] = (i + 1) / 2;

        var left = i - right[i] - 1;
        while (left >= 0 && right[i] < s.Length && s[left] == s[right[i]])
        {
            left--;
            right[i]++;
        }

        if (right[i] > right[rightmost])
            rightmost = i;
    }

    return right;
}
 ```
