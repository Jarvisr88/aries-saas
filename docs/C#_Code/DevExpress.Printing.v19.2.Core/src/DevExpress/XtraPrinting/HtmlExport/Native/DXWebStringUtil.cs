namespace DevExpress.XtraPrinting.HtmlExport.Native
{
    using System;

    internal static class DXWebStringUtil
    {
        internal static bool EqualsIgnoreCase(string s1, string s2) => 
            (!string.IsNullOrEmpty(s1) || !string.IsNullOrEmpty(s2)) ? (!string.IsNullOrEmpty(s1) && (!string.IsNullOrEmpty(s2) && ((s2.Length == s1.Length) ? (string.Compare(s1, 0, s2, 0, s2.Length, StringComparison.OrdinalIgnoreCase) == 0) : false))) : true;

        private static bool IsDirectorySeparatorChar(char value) => 
            (value == '\\') || (value == '/');

        internal static bool IsUncSharePath(string path) => 
            (path.Length > 2) && (IsDirectorySeparatorChar(path[0]) && IsDirectorySeparatorChar(path[1]));

        internal static bool StringEndsWith(string s, char c) => 
            (s.Length != 0) && (s[s.Length - 1] == c);
    }
}

