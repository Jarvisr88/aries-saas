namespace DevExpress.XtraExport.Utils
{
    using System;

    public static class LikePatternHelper
    {
        public static LikePatternKind GetPatternKind(string pattern)
        {
            if (!string.IsNullOrEmpty(pattern) && (pattern.Length > 1))
            {
                if (pattern[0] == '*')
                {
                    char[] anyOf = new char[] { '*', '?', '~' };
                    if (pattern.Substring(1).IndexOfAny(anyOf) == -1)
                    {
                        return LikePatternKind.EndWith;
                    }
                }
                if (pattern[pattern.Length - 1] == '*')
                {
                    char[] anyOf = new char[] { '*', '?', '~' };
                    if (pattern.Substring(0, pattern.Length - 1).IndexOfAny(anyOf) == -1)
                    {
                        return LikePatternKind.StartWith;
                    }
                }
            }
            return LikePatternKind.ContainsText;
        }
    }
}

