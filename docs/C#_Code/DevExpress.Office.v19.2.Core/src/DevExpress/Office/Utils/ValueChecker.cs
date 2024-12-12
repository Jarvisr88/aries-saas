namespace DevExpress.Office.Utils
{
    using System;

    public static class ValueChecker
    {
        public static void CheckLength(string value, int maxLength, string name)
        {
            if (!string.IsNullOrEmpty(value) && (value.Length > maxLength))
            {
                throw new ArgumentException($"{name}: number of characters in this string MUST be less than or equal to {maxLength}");
            }
        }

        public static void CheckValue(int value, int minValue, int maxValue)
        {
            if ((value < minValue) || (value > maxValue))
            {
                throw new ArgumentOutOfRangeException($"Value out of range {minValue}...{maxValue}");
            }
        }

        public static void CheckValue(double value, double minValue, double maxValue, string name)
        {
            if ((value < minValue) || (value > maxValue))
            {
                throw new ArgumentOutOfRangeException(string.Format(name + " value out of range {0}...{1}", minValue, maxValue));
            }
        }

        public static void CheckValue(int value, int minValue, int maxValue, string name)
        {
            if ((value < minValue) || (value > maxValue))
            {
                throw new ArgumentOutOfRangeException(string.Format(name + " value out of range {0}...{1}", minValue, maxValue));
            }
        }

        public static void CheckValue(long value, long minValue, long maxValue, string name)
        {
            if ((value < minValue) || (value > maxValue))
            {
                throw new ArgumentOutOfRangeException(string.Format(name + " value out of range {0}...{1}", minValue, maxValue));
            }
        }
    }
}

