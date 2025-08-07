using System.Text;
using JetBrains.Annotations;

namespace CrackingTheCodingInterview.Chapter1;

public static class ArraysAndStrings
{
    // 1.1
    // Implement and algorithm to determine if a string has all unique characters. What if you cannot use additional data structures?
    // implement hashset if any value > 1 then return false else true
    // create array and store character count in ascii position in the array if any number in array > 1 return false else true
    // sort the array by ascii value and if two consecutive are the same return false
    [UsedImplicitly]
    private static bool HasUniqueCharacters(string str)
    {
        ArgumentNullException.ThrowIfNull(str);
        var chars = str.ToCharArray();
        Array.Sort(chars);
        for (var i = 1; i < chars.Length; i++)
        {
            if (chars[i] == chars[i - 1]) return false;
        }

        return true;
    }

    // 1.2 
    // Check Permutation: Given two strings, write a method to decide if one is a permutation of the other.
    // Dictionary again
    [UsedImplicitly]
    private static bool CheckPermutationUnicode(string string1, string string2)
    {
        ArgumentException.ThrowIfNullOrEmpty(string1);
        ArgumentException.ThrowIfNullOrEmpty(string2);
        if (string1.Length != string2.Length) return false;

        var dictionary = new Dictionary<char, int>(capacity: string1.Length);
        foreach (var key in string1) dictionary[key] = dictionary.GetValueOrDefault(key) + 1;

        foreach (var key in string2)
        {
            if (!dictionary.TryGetValue(key, out var value) || value == 0) return false;
            dictionary[key] = value - 1;
        }

        return true;
    }

    // 1.3
    // URLify: Write a method to replace all spaces in a string with %20: You may assume that the string has sufficient space at the end to hold the additional
    // characters, and that you are given the "true" length of the string. (Note: If implementing in Java, please use a character array so that you can perform
    // this operation in place.)
    // EXAMPLE
    // Input: "Mr John Smith ", 13
    // Output: "Mr%20John%20Smith"
    [UsedImplicitly]
    private static string Urlify(string input, int trueLength) // O(n) time and O(1) space
    {
        ArgumentNullException.ThrowIfNull(input);
        if (trueLength < 0 || trueLength > input.Length) throw new ArgumentOutOfRangeException(nameof(trueLength));

        var spaceCount = 0;
        for (var i = 0; i < trueLength; i++)
        {
            if (input[i] == ' ') spaceCount++;
        }

        var newLen = trueLength + spaceCount * 2;
        var result = new char[newLen];

        var writeIndex = newLen - 1;
        for (var readIndex = trueLength - 1; readIndex >= 0; readIndex--)
        {
            var c = input[readIndex];
            if (c == ' ')
            {
                result[writeIndex--] = '0';
                result[writeIndex--] = '2';
                result[writeIndex--] = '%';
            }
            else
            {
                result[writeIndex--] = c;
            }
        }

        return new string(result);
    }

    // 1.3 - shorter and cleaner (more allocation needed)
    // URLify: Write a method to replace all spaces in a string with %20: You may assume that the string has sufficient space at the end to hold the additional
    // characters, and that you are given the "true" length of the string. (Note: If implementing in Java, please use a character array so that you can perform
    // this operation in place.)
    // EXAMPLE
    // Input: "Mr John Smith ", 13
    // Output: "Mr%20John%20Smith"
    [UsedImplicitly]
    private static string UrlifyStringBuilder(string input, int trueLength) // O(n) time and O(n) space
    {
        ArgumentNullException.ThrowIfNull(input);
        if (trueLength < 0 || trueLength > input.Length) throw new ArgumentOutOfRangeException(nameof(trueLength));

        var stringBuilder = new StringBuilder(capacity: trueLength);
        for (var i = 0; i < trueLength; i++)
        {
            if (input[i] == ' ') stringBuilder.Append("%20");
            else stringBuilder.Append(input[i]);
        }

        return stringBuilder.ToString();
    }


    // 1.4 Palindrome Permutation: Given a string, write a function to check if it is a permutation of a palindrome. A palindrome is a word or phrase that is the
    // same forwards and backwards. A permutation is a rearrangement of letters. The palindrome does not need to be limited to just dictionary words.
    // You can ignore casing and non-letter characters.
    // EXAMPLE
    // Input: Tact Coa
    // Output: True (permutations: "taco cat", "atco cta", etc.)
    [UsedImplicitly]
    private static bool Palindrome(string st1) // O(n) time and O(1) space
    {
        ArgumentException.ThrowIfNullOrEmpty(st1);
        var stLower = st1.ToLower();
        var dictionary = new Dictionary<char, int>();

        // add all keys in the dictionary
        foreach (var c in stLower)
        {
            if (c.ToString() != " ") dictionary[c] = dictionary.GetValueOrDefault(c) + 1;
        }

        var oddCount = 0;
        //check that all keys are an even number but one is an odd number
        foreach (var kvp in dictionary)
        {
            if (kvp.Value % 2 != 1) continue;
            oddCount += 1;
            if (oddCount > 1)
            {
                return false;
            }
        }

        return true;
    }

    // 1.5 One away: there are three types of edits that can be performed on strings: insert a character, remove a character, or replace a character. Given two
    // strings, write a function to check if thet are one edit (or zero edis) away.
    // Example:
    // pale, ple -> true
    // pales, pale -> true
    // pale, bale -> true
    // pale, bake -> false
    [UsedImplicitly]
    private static bool OneAway(string s1, string s2)
    {
        ArgumentException.ThrowIfNullOrEmpty(s1);
        ArgumentException.ThrowIfNullOrEmpty(s2);
        if (Math.Abs(s1.Length - s2.Length) > 1) return false;

        var i = 0;
        var j = 0;
        var diff = 0;
        while (i < s1.Length && j < s2.Length)
        {
            if (s1[i] == s2[j])
            {
                // characters match → advance both
                i++;
                j++;
                continue;
            }

            // first mismatch found
            if (diff == 1) return false; // second mismatch → too many edits

            diff = 1; // remember that we used our one edit

            if (s1.Length == s2.Length) // same length → replace
            {
                i++;
                j++;
            }
            else if (s1.Length > s2.Length) // delete from st1 (or insert into st2)
            {
                i++; // skip one char in the longer string
            }
            else // st2 longer
            {
                j++; // skip one char in the longer string
            }
        }

        return true;
    }

    // 1.6 String Compression: Implement a method to perform basic string compression using the counts of repeated characters. For example, the string
    // aabcccccaaa would become a2b1c5a3. If the compressed string would not become smaller than the original string your method should return the original
    // string. You can assume the string has only uppercase and lowercase letters (a-z).
    [UsedImplicitly]
    private static string StringCompression(string s)
    {
        ArgumentException.ThrowIfNullOrEmpty(s);

        var sb = new StringBuilder();
        var runCount = 1;

        for (var i = 1; i < s.Length; i++)
        {
            if (s[i] == s[i - 1])
            {
                runCount++;
            }
            else
            {
                // append the finished run
                sb.Append(s[i - 1]).Append(runCount);
                // early exit if no win
                if (sb.Length >= s.Length) return s;
                runCount = 1;
            }
        }

        // append the final run
        sb.Append(s[^1]).Append(runCount);

        // if compressed isn't smaller, return original
        return sb.Length >= s.Length ? s : sb.ToString();
    }

    // 1.7 Rotate matrix: Given an image represented by an N x N matrix, where each pixel in the image is represented by an integer, write a method to rotate
    // the image by 90 degrees. Can you do this in place?
    [UsedImplicitly]
    private static int[][] RotateMatrix(int[][] matrix)
    {
        // so if we want to rotate the matrix counter-clockwise by 90 degrees only reverse the indices i and j 
        // if we want to rotate the matrix clockwise by 90 degrees after reversing the indices i and j we also reverse each row

        for (var i = 0; i < matrix.Length - 1; i++)
        {
            for (var j = i + 1; j < matrix.Length; j++)
            {
                (matrix[i][j], matrix[j][i]) = (matrix[j][i], matrix[i][j]);
            }
        }

        foreach (var c in matrix)
        {
            Array.Reverse(c);
        }

        return matrix;
    }

    // 1.8 Zero matrix: Write an algorithm such that if an element in a M x N matrix is 0, its entire row and column are set to 0
    [UsedImplicitly]
    private static int[][] ZeroMatrix(int[][] matrix)
    {
        if (matrix.Length == 0) return matrix;

        var m = matrix.Length;
        var n = matrix[0].Length;

        // 1) Detection pass: mark rows and columns that contain a zero
        var rowsToZero = new bool[m];
        var colsToZero = new bool[n];

        for (var i = 0; i < m; i++)
        {
            for (var j = 0; j < n; j++)
            {
                if (matrix[i][j] != 0) continue;
                rowsToZero[i] = true;
                colsToZero[j] = true;
            }
        }

        // 2) Zero out marked rows
        for (var i = 0; i < m; i++)
        {
            if (!rowsToZero[i]) continue;
            for (var j = 0; j < n; j++)
            {
                matrix[i][j] = 0;
            }
        }

        // 3) Zero out marked columns
        for (var j = 0; j < n; j++)
        {
            if (!colsToZero[j]) continue;
            for (var i = 0; i < m; i++)
            {
                matrix[i][j] = 0;
            }
        }

        return matrix;
    }

    // 1.9 String Rotation: Assume you have a method isSubstring which checks if one word is a substring of another. iven two strings, s1 and s2, write code to 
    // check if s2 is a rotation of s1 using only onw call to isSubstring (e.g. "waterbottle" is a rotation of "erbottlewat")
    [UsedImplicitly]
    private static bool StringRotation(string s1, string s2)
    {
        ArgumentException.ThrowIfNullOrEmpty(s1);
        ArgumentException.ThrowIfNullOrEmpty(s2);
        if (s1.Length != s2.Length) return false;

        var doubled = s1 + s1;

        return doubled.Contains(s2);
    }


    public static void Main()
    {
        // var result = HasUniqueCharacters("aac");
        // var result = CheckPermutationUnicode("abc", "bda");
        // var result = Urlify("Mr John Smith", 13);
        // var result = UrlifyStringBuilder("Mr John Smith", 13);
        // var result = Palindrome("aabb");
        // var result = OneAway("ad", "da");
        // var result = StringCompression("aabcccccaaa");
        // var m = new[]
        // {
        //     new[] { 0, 2, 3 },
        //     new[] { 4, 0, 6 },
        //     new[] { 7, 8, 0 }
        // };
        // var result = RotateMatrix(m);
        // var result = ZeroMatrix(m);
        // foreach (var t in result)
        // {
        //     for (var j = 0; j < result.Length; j++)
        //     {
        //         Console.WriteLine(t[j]);
        //     }
        // }

        var result = StringRotation("bottledwater", "ledwaterbitt");

        Console.WriteLine(result);
    }
}