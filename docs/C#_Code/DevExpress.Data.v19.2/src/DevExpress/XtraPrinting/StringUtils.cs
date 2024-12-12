namespace DevExpress.XtraPrinting
{
    using System;
    using System.Collections.Generic;

    public sealed class StringUtils
    {
        public static string Join(string separator, List<string> list) => 
            Join(separator, list.ToArray());

        public static string Join(string separator, params string[] array)
        {
            string str = string.Empty;
            foreach (string str2 in array)
            {
                str = Join(separator, str, str2);
            }
            return str;
        }

        public static string Join(string separator, string str1, string str2) => 
            (!string.IsNullOrEmpty(str1) || !string.IsNullOrEmpty(str2)) ? ((string.IsNullOrEmpty(str1) || !string.IsNullOrEmpty(str2)) ? ((!string.IsNullOrEmpty(str1) || string.IsNullOrEmpty(str2)) ? (str1 + separator + str2) : str2) : str1) : string.Empty;
    }
}

