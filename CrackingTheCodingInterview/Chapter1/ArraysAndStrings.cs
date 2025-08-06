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
    
    public static void Main()
    {
        // var result = HasUniqueCharacters("aac");
        // var result = CheckPermutationUnicode("abc", "bda");
        // var result = Urlify("Mr John Smith", 13);
        var result = UrlifyStringBuilder("Mr John Smith", 13);
        
        Console.WriteLine(result);
    }
}