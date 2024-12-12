namespace DevExpress.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public static class Guard
    {
        public static void ArgumentIsInRange<T>(IList<T> list, int index, string name)
        {
            ArgumentIsInRange(0, list.Count - 1, index, name);
        }

        public static void ArgumentIsInRange(int minValue, int maxValue, int value, string name)
        {
            if ((value < minValue) || (value > maxValue))
            {
                ThrowArgumentException(name, value, null);
            }
        }

        public static void ArgumentIsNotNullOrEmpty(string value, string name)
        {
            if (string.IsNullOrEmpty(value))
            {
                ThrowArgumentException(name, value, null);
            }
        }

        public static void ArgumentMatch<TValue>(TValue value, string name, Func<TValue, bool> predicate)
        {
            if (!predicate(value))
            {
                ThrowArgumentException(name, value, null);
            }
        }

        public static TValue ArgumentMatchType<TValue>(object value, string name)
        {
            TValue local;
            try
            {
                local = (TValue) value;
            }
            catch (InvalidCastException exception)
            {
                ThrowArgumentException(name, value, exception);
                throw new InvalidOperationException();
            }
            return local;
        }

        public static void ArgumentNonNegative(double value, string name)
        {
            if (value < 0.0)
            {
                ThrowArgumentException(name, value, null);
            }
        }

        public static void ArgumentNonNegative(int value, string name)
        {
            if (value < 0)
            {
                ThrowArgumentException(name, value, null);
            }
        }

        public static void ArgumentNonNegative(float value, string name)
        {
            if (value < 0f)
            {
                ThrowArgumentException(name, value, null);
            }
        }

        public static void ArgumentNotNull(object value, string name)
        {
            if (value == null)
            {
                ThrowArgumentNullException(name);
            }
        }

        public static void ArgumentPositive(double value, string name)
        {
            if (value <= 0.0)
            {
                ThrowArgumentException(name, value, null);
            }
        }

        public static void ArgumentPositive(int value, string name)
        {
            if (value <= 0)
            {
                ThrowArgumentException(name, value, null);
            }
        }

        public static void ArgumentPositive(float value, string name)
        {
            if (value <= 0f)
            {
                ThrowArgumentException(name, value, null);
            }
        }

        private static void ThrowArgumentException(string propName, object val, Exception innerException = null)
        {
            string str = (val == string.Empty) ? "String.Empty" : ((val == null) ? "null" : val.ToString());
            throw new ArgumentException($"'{str}' is not a valid value for '{propName}'", innerException);
        }

        private static void ThrowArgumentNullException(string propName)
        {
            throw new ArgumentNullException(propName);
        }
    }
}

