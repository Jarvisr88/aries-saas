namespace DevExpress.Utils
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;

    public static class StringExtensions
    {
        public static int Compare(string str, string str2, bool ignoreCase, CultureInfo culture) => 
            string.Compare(str, str2, ignoreCase, culture);

        public static int CompareInvariantCulture(string str1, string str2) => 
            string.Compare(str1, str2, StringComparison.InvariantCulture);

        public static int CompareInvariantCultureIgnoreCase(string str1, string str2) => 
            string.Compare(str1, str2, StringComparison.InvariantCultureIgnoreCase);

        public static int CompareInvariantCultureWithOptions(string str1, string str2, CompareOptions options) => 
            CultureInfo.InvariantCulture.CompareInfo.Compare(str1, str2, options);

        public static int CompareWithCultureInfoAndOptions(string str1, string str2, CultureInfo info, CompareOptions options) => 
            string.Compare(str1, str2, info, options);

        public static bool EndsWithInvariantCulture(this string str1, string str2) => 
            str1.EndsWith(str2, StringComparison.InvariantCulture);

        public static bool EndsWithInvariantCultureIgnoreCase(this string str1, string str2) => 
            str1.EndsWith(str2, StringComparison.InvariantCultureIgnoreCase);

        public static UnicodeCategory GetUnicodeCategory(this char c) => 
            CharUnicodeInfo.GetUnicodeCategory(c);

        public static int IndexOfInvariantCulture(this string str1, string str2) => 
            str1.IndexOf(str2, StringComparison.InvariantCulture);

        public static int IndexOfInvariantCulture(this string str1, string str2, int startIndex) => 
            str1.IndexOf(str2, startIndex, StringComparison.InvariantCulture);

        public static int IndexOfInvariantCultureIgnoreCase(this string str1, string str2) => 
            str1.IndexOf(str2, StringComparison.InvariantCultureIgnoreCase);

        public static int IndexOfInvariantCultureIgnoreCase(this string str1, string str2, int startIndex) => 
            str1.IndexOf(str2, startIndex, StringComparison.InvariantCultureIgnoreCase);

        public static int LastIndexOfInvariantCultureIgnoreCase(this string str1, string str2) => 
            str1.LastIndexOf(str2, StringComparison.InvariantCultureIgnoreCase);

        public static int LastIndexOfInvariantCultureIgnoreCase(this string str1, string str2, int startIndex) => 
            str1.LastIndexOf(str2, startIndex, StringComparison.InvariantCultureIgnoreCase);

        public static bool StartsWithInvariantCulture(this string str1, string str2) => 
            str1.StartsWith(str2, StringComparison.InvariantCulture);

        public static bool StartsWithInvariantCultureIgnoreCase(this string str1, string str2) => 
            str1.StartsWith(str2, StringComparison.InvariantCultureIgnoreCase);

        public static StringComparer ComparerInvariantCulture =>
            StringComparer.InvariantCulture;

        public static StringComparer ComparerInvariantCultureIgnoreCase =>
            StringComparer.InvariantCultureIgnoreCase;
    }
}

