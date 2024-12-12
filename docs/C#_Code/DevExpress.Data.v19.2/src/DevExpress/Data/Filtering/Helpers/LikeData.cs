namespace DevExpress.Data.Filtering.Helpers
{
    using System;

    public static class LikeData
    {
        public static string ConvertToRegEx(string originalPattern);
        [Obsolete("Use FunctionOperatorType.Contains instead")]
        public static string CreateContainsPattern(string autoFilterText);
        internal static Func<string, bool?> CreatePredicate(string pat, bool caseSensitive);
        [Obsolete("Use FunctionOperatorType.StartsWith instead")]
        public static string CreateStartsWithPattern(string autoFilterText);
        [Obsolete("Use FunctionOperatorType.StartsWith, .EndsWith, .Contains instead")]
        public static string Escape(string autoFilterText);
        [Obsolete("Use FunctionOperatorType.StartsWith, .EndsWith, .Contains instead")]
        public static string UnEscape(string likePattern);
    }
}

