namespace DevExpress.Printing.Native
{
    using System;
    using System.Collections.Generic;

    public static class PageRangeParser
    {
        private static int[] GetAllIndexes(int count)
        {
            int[] numArray = new int[count];
            for (int i = 0; i < count; i++)
            {
                numArray[i] = i;
            }
            return numArray;
        }

        public static int[] GetIndices(string range, int maxIndex)
        {
            if (range == null)
            {
                throw new ArgumentException("range");
            }
            return GetValues(range, maxIndex);
        }

        private static int[] GetValues(string range, int maxIndex)
        {
            List<int> list = new List<int>();
            string str = ValidateString(range);
            if (str.Length > 0)
            {
                char[] separator = new char[] { ',' };
                foreach (string str2 in str.Split(separator))
                {
                    try
                    {
                        foreach (int num3 in ParseElement(str2, maxIndex))
                        {
                            list.Add(num3 - 1);
                        }
                    }
                    catch
                    {
                    }
                }
            }
            return (string.IsNullOrEmpty(range) ? GetAllIndexes(maxIndex) : list.ToArray());
        }

        private static int[] ParseElement(string s, int maxIndex)
        {
            char[] separator = new char[] { '-' };
            string[] strArray = s.Split(separator);
            int item = Convert.ToInt32(strArray[0]);
            if (strArray.Length == 1)
            {
                if (item > maxIndex)
                {
                    return new int[0];
                }
                return new int[] { item };
            }
            int num2 = (strArray[1].Length > 0) ? Convert.ToInt32(strArray[1]) : maxIndex;
            if (item > num2)
            {
                num2 = item;
                item = num2;
            }
            List<int> list = new List<int>();
            while (true)
            {
                if (item <= maxIndex)
                {
                    list.Add(item);
                }
                item++;
                if (item > num2)
                {
                    return list.ToArray();
                }
            }
        }

        private static string Replace(string s, string oldValue, string newValue)
        {
            while (s.IndexOf(oldValue) >= 0)
            {
                s = s.Replace(oldValue, newValue);
            }
            return s;
        }

        public static string ValidateString(string s)
        {
            if (s == null)
            {
                return "";
            }
            char[] array = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '-', ',' };
            for (int i = s.Length - 1; i >= 0; i--)
            {
                if (Array.IndexOf<char>(array, s[i]) < 0)
                {
                    s = s.Remove(i, 1);
                }
            }
            s = Replace(s, "--", "-");
            s = Replace(s, ",,", ",");
            s = Replace(s, ",0-", ",1-");
            s = Replace(s, "-0,", ",");
            s = Replace(s, ",0,", ",");
            char[] trimChars = new char[] { ',', '-' };
            s = s.TrimStart(trimChars);
            if (s.StartsWith("0,"))
            {
                s = s.Substring(2);
            }
            if (s.StartsWith("0-"))
            {
                s = "1-" + s.Substring(2);
            }
            if (s.EndsWith(",0"))
            {
                s = s.Substring(0, s.Length - 2);
            }
            if (s.EndsWith("-0"))
            {
                s = s.Substring(0, s.Length - 2);
            }
            char[] chArray3 = new char[] { ',' };
            s = s.TrimEnd(chArray3);
            return s;
        }
    }
}

