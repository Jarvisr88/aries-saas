namespace DevExpress.Xpf.Docking
{
    using System;

    internal static class AccessKeyHelper
    {
        private const char _accessKeyMarker = '_';
        private const string _doubleAccessKeyMarker = "__";

        private static int FindAccessKeyMarker(string text)
        {
            int index;
            int length = text.Length;
            for (int i = 0; i < length; i = index + 2)
            {
                index = text.IndexOf('_', i);
                if (index == -1)
                {
                    return -1;
                }
                if (((index + 1) < length) && (text[index + 1] != '_'))
                {
                    return index;
                }
            }
            return -1;
        }

        public static string RemoveAccessKeyMarker(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                int startIndex = FindAccessKeyMarker(text);
                if ((startIndex >= 0) && (startIndex < (text.Length - 1)))
                {
                    text = text.Remove(startIndex, 1);
                }
                text = text.Replace("__", '_'.ToString());
            }
            return text;
        }
    }
}

