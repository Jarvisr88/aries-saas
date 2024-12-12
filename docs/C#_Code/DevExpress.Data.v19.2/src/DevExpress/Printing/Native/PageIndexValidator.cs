namespace DevExpress.Printing.Native
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PageIndexValidator
    {
        private readonly string range;
        private readonly Tuple<int, int>[] ranges;
        private readonly IList<int> validatedIndexes = new List<int>();

        public PageIndexValidator(string range)
        {
            this.range = range;
            this.ranges = CalculateRanges(PageRangeParser.ValidateString(range));
        }

        private static Tuple<int, int>[] CalculateRanges(string ranges)
        {
            List<Tuple<int, int>> list = new List<Tuple<int, int>>();
            if (ranges != null)
            {
                char[] separator = new char[] { ',' };
                foreach (string str in ranges.Split(separator))
                {
                    Tuple<int, int> item = ParseRange(str);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            if (list.Count > 0)
            {
                return list.ToArray();
            }
            return new Tuple<int, int>[] { new Tuple<int, int>(-2147483648, 0x7fffffff) };
        }

        private static Tuple<int, int> ParseRange(string s)
        {
            int num;
            int num2;
            char[] separator = new char[] { '-' };
            string[] strArray = s.Split(separator);
            if (!int.TryParse(strArray[0], out num))
            {
                return null;
            }
            num--;
            if (strArray.Length == 1)
            {
                return new Tuple<int, int>(num, num);
            }
            if (!int.TryParse(strArray[1], out num2))
            {
                return new Tuple<int, int>(num, num);
            }
            num2--;
            return new Tuple<int, int>(Math.Min(num, num2), Math.Max(num, num2));
        }

        public bool ValidateIndex(int index)
        {
            if (!this.ranges.Any<Tuple<int, int>>(range => ((index >= range.Item1) && (index <= range.Item2))))
            {
                return false;
            }
            this.validatedIndexes.Add(index);
            return true;
        }

        public IList<int> ValidatedIndexes =>
            this.validatedIndexes;
    }
}

