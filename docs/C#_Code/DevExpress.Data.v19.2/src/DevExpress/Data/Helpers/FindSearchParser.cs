namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public class FindSearchParser
    {
        private static Regex parser;
        private static Regex queryParser;
        private static Regex quotedGroups;

        public static string AppendColumnPrefix(string field);
        public static void AppendColumnPrefixes(string[] columns);
        public static IDataColumnInfo ColumnByCaption(IEnumerable<IDataColumnInfo> columns, string caption);
        private static IDataColumnInfo ColumnByCaption(IEnumerable<IDataColumnInfo> columns, string caption, bool toLower, bool firstLetters, bool useStartWith);
        private string DefaultOnFindResolveColumnName(FindSearchField field, IEnumerable<IDataColumnInfo> columns);
        private string ExtractField(string field);
        private string ExtractValue(string text);
        public static string GetWordFirstLetters(string text);
        private bool IsMatchInsideQuotedGroups(Match m, MatchCollection quotedGroups);
        public FindSearchParserResults Parse(string sourceText);
        public virtual FindSearchParserResults Parse(string sourceText, FindSearchFieldResolveDelegate fieldResolver);
        public virtual FindSearchParserResults Parse(string sourceText, IEnumerable<IDataColumnInfo> columns);
        protected virtual List<FindSearchField> ParseFields(List<Match> matches, FindSearchFieldResolveDelegate fieldResolver);
        private string[] ParseSearchText(string searchText);
        private string RemoveQuotes(string text);
        private List<Match> UpdateFieldMatches(MatchCollection fields, MatchCollection quotedGroups);

        protected static Regex SParser { get; }

        private static Regex QuotedGroup { get; }

        protected static Regex SQueryParser { get; }

        protected Regex Parser { get; }

        protected Regex QueryParser { get; }
    }
}

