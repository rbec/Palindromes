# Palindromes

An implementation of [Manacher algorithm](https://en.wikipedia.org/wiki/Longest_palindromic_substring) algorithm to find plaindromes.

# UK Postcode Data Structure

## Description

Every UK address is associated with a [postcode](https://en.wikipedia.org/wiki/Postcodes_in_the_United_Kingdom). This consists of between 5 and 7 letters and digits in one of these formats:

| Area | Sector | Unit |
|------|--------|------|
| A    | 1      | 1AA  |
| A    | 11     | 1AA  |
| A    | 1A     | 1AA  |
| AA   | 1      | 1AA  |
| AA   | 11     | 1AA  |
| AA   | 1A     | 1AA  |

Where `A` represents an upper case letter A-Z and `1` represents a digit 0-9.

### Observations
* 1st character of the Area is a letter (26 possibilities)
* 2nd character of the Area is either a letter or missing (26 + 1 = 27 possibilities)
* 1st character of the Sector is a digit (10 possibilities)
* 2nd character of the Sector is either a letter or a digit or missing (26 + 10 + 1 = 37 possibilities)
* 1st character of the Unit is a digit (10 possibilities)
* 2nd character of the Unit is a letter (26 possibilities)
* 3rd character of the Unit is a letter (26 possibilities)

Hence the number of possible postcodes is
`26 * 27 * 10 * 37 * 10 * 10 * 26 * 26 = 17,558,424,000 < 2 ³²`

## Motivation
