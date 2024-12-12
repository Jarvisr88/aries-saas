namespace DevExpress.Office.Utils
{
    using System;
    using System.Globalization;
    using System.Runtime.InteropServices;

    public static class StringValueParser
    {
        private static char[] valueChars = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '.' };

        internal static ValueInfo Parse(SplitResult result)
        {
            double num;
            return (!double.TryParse(result.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out num) ? new ValueInfo(result.Unit) : new ValueInfo((float) num, result.Unit));
        }

        internal static SplitResult SplitUnitFromValue(string inputString)
        {
            string str = string.Empty;
            string unit = inputString;
            int num = inputString.LastIndexOfAny(valueChars);
            if (num != -1)
            {
                str = inputString.Substring(0, num + 1);
                unit = inputString.Substring(num + 1);
            }
            return new SplitResult(str, unit);
        }

        public static ValueInfo TryParse(string inputString) => 
            !string.IsNullOrEmpty(inputString) ? Parse(SplitUnitFromValue(inputString)) : ValueInfo.Empty;

        [StructLayout(LayoutKind.Sequential)]
        internal struct SplitResult
        {
            public string Value;
            public string Unit;
            public SplitResult(string value, string unit)
            {
                this.Value = value;
                this.Unit = unit;
            }
        }
    }
}

