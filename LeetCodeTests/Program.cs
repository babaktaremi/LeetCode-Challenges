Console.WriteLine("Hello, World!");

var t = Solution.IsSubsequence("b", "abc");


public class Solution
{
    public int[] TwoSum(int[] nums, int target)
    {
        var indexes = new Dictionary<int, int>();


        for (int i = 0; i < nums.Length; i++)
        {
            int complement = target - nums[i];

            if (indexes.TryGetValue(complement, out var index))
                return [index, i];

            indexes[nums[i]] = i;
        }

        return [];
    }

    public static int LengthOfLongestSubstring(string s)
    {
        if (string.IsNullOrEmpty(s))
            return 0;

        var hash = new HashSet<char>();
        int left = 0, result = 0;


        for (int right = 0; right < s.Length; right++)
        {
            if (hash.Add(s[right]))
                result = Math.Max(result, right - left + 1);

            else
            {
                while (hash.Contains(s[right]))
                {
                    hash.Remove(s[left]);
                    left++;
                }

                hash.Add(s[right]);
            }
        }

        return result;
    }

    public static int Reverse(int x)
    {
        var reversed = 0;
        while (x != 0)
        {
            var digit = x % 10;

            x /= 10;

            if (reversed > int.MaxValue / 10 || (reversed == int.MaxValue / 10 && digit > 7))
            {
                return 0;
            }

            if (reversed < int.MinValue / 10 || (reversed == int.MinValue / 10 && digit < -8))
            {
                return 0;
            }

            reversed = reversed * 10 + digit;
        }

        return reversed;
    }

    public static int RomanToInt(string s)
    {
        var roman = new Dictionary<char, int>
        {
            { 'I', 1 },
            { 'V', 5 },
            { 'X', 10 },
            { 'L', 50 },
            { 'C', 100 },
            { 'D', 500 },
            { 'M', 1000 }
        };

        var result = 0;

        if (string.IsNullOrEmpty(s))
            return result;

        for (int i = 0; i < s.Length; i++)
        {
            var currentValue = roman[s[i]];

            if (i < s.Length - 1 && currentValue < roman[s[i + 1]])
            {
                result -= currentValue;
            }
            else
            {
                result += currentValue;
            }
        }

        return result;
    }

    public static int MyAtoi(string s)
    {
        if (string.IsNullOrEmpty(s))
            return 0;

        var result = 0;
        var sign = 1;
        var index = 0;

        while (index < s.Length && s[index] == ' ')
            index++;

        if (index < s.Length && (s[index] == '-' || s[index] == '+'))
        {
            sign = s[index] == '-' ? -1 : 1;
            index++;
        }

        while (index < s.Length && char.IsDigit(s[index]))
        {
            var digit = s[index] - '0';

            if (result > (int.MaxValue - digit) / 10)
                return sign == 1 ? int.MaxValue : int.MinValue;

            result = result * 10 + digit;
            index++;
        }

        return result * sign;
    }


    public static string LongestCommonPrefix(string[] strs)
    {
        if (strs == null || strs.Length == 0)
            return "";

        var prefix = strs[0].AsSpan();

        for (int i = 1; i < strs.Length; i++)
        {
            while (strs[i].AsSpan().IndexOf(prefix) != 0)
            {
                prefix = prefix.Slice(0, prefix.Length - 1);
                if (prefix.Length == 0)
                    return "";
            }
        }

        return prefix.ToString();
    }

    public IList<IList<int>> ThreeSum(int[] nums)
    {
        Array.Sort(nums);

        IList<IList<int>> result = [];

        for (int i = 0; i < nums.Length - 2; i++)
        {
            if (i > 0 && nums[i] == nums[i - 1]) continue; // Skip duplicate i

            int left = i + 1;
            int right = nums.Length - 1;

            while (left < right)
            {
                int sum = nums[i] + nums[left] + nums[right];
                if (sum == 0)
                {
                    result.Add(new List<int> { nums[i], nums[left], nums[right] });
                    while (left < right && nums[left] == nums[left + 1]) left++; // Skip duplicate left
                    while (left < right && nums[right] == nums[right - 1]) right--; // Skip duplicate right
                    left++;
                    right--;
                }
                else if (sum < 0)
                {
                    left++;
                }
                else
                {
                    right--;
                }
            }
        }

        return result;
    }

    public static int RemoveDuplicates(int[] nums)
    {
        if (nums.Length == 0) return 0;

        int i = 0;
        for (int j = 1; j < nums.Length; j++)
        {
            if (nums[j] != nums[i])
            {
                i++;
                nums[i] = nums[j];
            }
        }

        return i + 1;
    }

    public int SearchInsert(int[] nums, int target)
    {
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] == target)
                return i;

            if (nums[i] > target)
                return i;
        }

        return nums.Length;
    }

    public static int[] PlusOne(int[] digits)
    {
        for (int i = digits.Length - 1; i >= 0; i--)
        {
            if (digits[i] < 9)
            {
                digits[i]++;
                return digits;
            }

            digits[i] = 0;
        }

        var result = new int[digits.Length + 1];
        result[0] = 1;
        return result;
    }

    public static IList<IList<string>> FindReversedPairs(string[] strs)
    {
        IList<IList<string>> result = new List<IList<string>>();
        HashSet<string> seen = new HashSet<string>();
        HashSet<string> addedPairs = new HashSet<string>();

        foreach (string s in strs)
        {
            seen.Add(s);
        }

        foreach (string s in strs)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            string reversed = new string(charArray);

            if (seen.Contains(reversed) && s != reversed && !addedPairs.Contains(reversed + s))
            {
                result.Add(new List<string> { s, reversed });
                addedPairs.Add(s + reversed);
            }
        }

        return result;
    }

    public static int MaximumNumberOfStringPairs(string[] words)
    {
        int result = 0;

        HashSet<string> addedPairs = new HashSet<string>();

        foreach (var s in words.AsSpan())
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            string reversed = new string(charArray);

            if (s == reversed)
                continue;

            if (words.Contains(reversed) && !addedPairs.Contains(reversed + s))
            {
                result++;
                addedPairs.Add(s + reversed);
            }
        }

        return result;
    }

    public static void MoveZeroes(int[] nums)
    {
        int lastNonZeroIndex = 0;

        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] != 0)
            {
                nums[lastNonZeroIndex] = nums[i];
                lastNonZeroIndex++;
            }
        }

        for (int i = lastNonZeroIndex; i < nums.Length; i++)
        {
            nums[i] = 0;
        }
    }

    public static void MoveZeroesRuntimeOptimized(int[] nums)
    {
        int lastNonZeroPos = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] != 0)
            {
                // Swap nums[i] with nums[lastNonZeroPos]
                (nums[i], nums[lastNonZeroPos]) = (nums[lastNonZeroPos], nums[i]);
                lastNonZeroPos++;
            }
        }
    }

    public static void MoveZeroesAllOptimized(int[] nums)
    {
        int lastNonZeroIndex = 0;

        // First pass: Move all non-zero elements to the front
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] != 0)
            {
                nums[lastNonZeroIndex++] = nums[i];
            }
        }

        // Second pass: Fill remaining positions with zeros (optimized)
        while (lastNonZeroIndex < nums.Length)
        {
            nums[lastNonZeroIndex++] = 0;
        }
    }


    public static bool IsSubsequence(string s, string t)
    {
        if (s.Length == 0) return true;

        var sIndex = 0;

        for (int i = 0; i < t.Length; i++)
        {
            if (sIndex >= s.Length)
                break;

            if (t[i] == s[sIndex])
                sIndex++;
        }

        return sIndex == s.Length;
    }
}