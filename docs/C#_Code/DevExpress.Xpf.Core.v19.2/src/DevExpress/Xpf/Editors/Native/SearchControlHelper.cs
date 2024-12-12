namespace DevExpress.Xpf.Editors.Native
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Helpers;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class SearchControlHelper
    {
        private static void AddColumnsForceWithoutPrefix(ISearchPanelColumnProviderEx columnProviderEx, FilterCondition filterCondition, string searchText, SearchPanelParseMode parserKind, ref CriteriaOperator op)
        {
            if (columnProviderEx.ColumnsForceWithoutPrefix.Count != 0)
            {
                CriteriaOperator @operator = GetCriteriaOperatorCore(columnProviderEx.ColumnsForceWithoutPrefix.Cast<IDataColumnInfo>(), parserKind, filterCondition, searchText, !columnProviderEx.IsServerMode);
                op = ConcatCriteriaOperators(parserKind, op, @operator);
            }
        }

        private static void ApplyCriteriaOperatorCustomFormat(ISearchPanelColumnProviderEx columnProviderEx, FilterCondition customFilterCondition, string searchText, SearchPanelParseMode parserKind, ref CriteriaOperator op)
        {
            Func<CustomFilterColumn, IDataColumnInfo> selector = <>c.<>9__8_1;
            if (<>c.<>9__8_1 == null)
            {
                Func<CustomFilterColumn, IDataColumnInfo> local1 = <>c.<>9__8_1;
                selector = <>c.<>9__8_1 = col => col.Column;
            }
            CriteriaOperator @operator = GetCriteriaOperatorCore((from col in columnProviderEx.CustomFilterColumns
                where col.FilterCondition == customFilterCondition
                select col).Select<CustomFilterColumn, IDataColumnInfo>(selector).ToList<IDataColumnInfo>().Cast<IDataColumnInfo>(), parserKind, customFilterCondition, searchText, !columnProviderEx.IsServerMode);
            op = ConcatCriteriaOperators(parserKind, op, @operator);
        }

        private static CriteriaOperator ConcatCriteriaOperators(SearchPanelParseMode parserKind, CriteriaOperator op1, CriteriaOperator op2) => 
            (parserKind == SearchPanelParseMode.And) ? CriteriaOperator.And(op1, op2) : CriteriaOperator.Or(op1, op2);

        private static FindPanelParserKind ConvertParserKindValue(SearchPanelParseMode parseMode)
        {
            switch (parseMode)
            {
                case SearchPanelParseMode.Mixed:
                    return FindPanelParserKind.Mixed;

                case SearchPanelParseMode.Exact:
                    return FindPanelParserKind.Exact;

                case SearchPanelParseMode.Or:
                    return FindPanelParserKind.Or;

                case SearchPanelParseMode.And:
                    return FindPanelParserKind.And;
            }
            throw new NotSupportedException();
        }

        public static CriteriaOperator GetCriteriaOperator(ISearchPanelColumnProvider columnProvider, FilterCondition filterCondition, string searchText, SearchPanelParseMode parseMode)
        {
            if (string.IsNullOrEmpty(searchText) || (columnProvider == null))
            {
                return null;
            }
            if (parseMode != SearchPanelParseMode.Exact)
            {
                RemovePlusMinusIfNeed(ref searchText);
            }
            return GetCriteriaOperatorCore(columnProvider, filterCondition, searchText, parseMode);
        }

        public static CriteriaOperator GetCriteriaOperator(ISearchPanelColumnProviderEx columnProvider, FilterCondition filterCondition, string searchText, SearchPanelParseMode parseMode)
        {
            if (string.IsNullOrEmpty(searchText) || (columnProvider == null))
            {
                return null;
            }
            if (parseMode != SearchPanelParseMode.Exact)
            {
                RemovePlusMinusIfNeed(ref searchText);
            }
            return GetCriteriaOperatorCore(columnProvider, parseMode, filterCondition, searchText);
        }

        private static CriteriaOperator GetCriteriaOperatorCore(ISearchPanelColumnProvider columnProvider, FilterCondition filterCondition, string searchText, SearchPanelParseMode parseMode)
        {
            CriteriaOperator @operator = null;
            FindSearchTools.WpfGetFindCriteria(ConvertParserKindValue(parseMode), searchText, columnProvider.Columns, filterCondition, out @operator);
            return @operator;
        }

        private static CriteriaOperator GetCriteriaOperatorCore(ISearchPanelColumnProviderEx columnProviderEx, SearchPanelParseMode parserKind, FilterCondition filterCondition, string searchText)
        {
            CriteriaOperator op = GetCriteriaOperatorCore(columnProviderEx.Columns.Cast<IDataColumnInfo>(), parserKind, filterCondition, searchText, !columnProviderEx.IsServerMode);
            AddColumnsForceWithoutPrefix(columnProviderEx, filterCondition, searchText, parserKind, ref op);
            if ((columnProviderEx.CustomFilterColumns != null) && (columnProviderEx.CustomFilterColumns.Count != 0))
            {
                foreach (FilterCondition condition in Enum.GetValues(typeof(FilterCondition)).Cast<FilterCondition>())
                {
                    ApplyCriteriaOperatorCustomFormat(columnProviderEx, condition, searchText, parserKind, ref op);
                }
            }
            return op;
        }

        private static CriteriaOperator GetCriteriaOperatorCore(IEnumerable<IDataColumnInfo> dataColumns, SearchPanelParseMode parserKind, FilterCondition filterCondition, string searchText, bool isClientMode)
        {
            CriteriaOperator @operator = null;
            FindSearchTools.WpfGetFindCriteria(ConvertParserKindValue(parserKind), searchText, dataColumns, filterCondition, isClientMode, out @operator);
            return @operator;
        }

        internal static TextHighlightingProperties GetDefaultTextHighlightingProperties() => 
            new TextHighlightingProperties(null, FilterCondition.Default);

        private static string GetTextHighlightingString(string[] searchTexts, FilterCondition filterCondition)
        {
            if (searchTexts.Length == 0)
            {
                return string.Empty;
            }
            string str = string.Empty;
            for (int i = 0; i < searchTexts.Length; i++)
            {
                if (searchTexts[i].StartsWith("+") || searchTexts[i].StartsWith("-"))
                {
                    searchTexts[i] = searchTexts[i].Remove(0, 1);
                }
                if ((filterCondition == FilterCondition.Default) || (filterCondition == FilterCondition.Like))
                {
                    searchTexts[i] = searchTexts[i].Replace("%", string.Empty);
                }
                str = str + ((str == string.Empty) ? searchTexts[i] : ("\n" + searchTexts[i]));
            }
            return str;
        }

        public static List<FieldAndHighlightingString> GetTextHighlightingString(string searchText, ICollection columns, FilterCondition filterCondition, bool exactMatch = false)
        {
            FindSearchParserResults results = new FindSearchParser().Parse(searchText, columns.Cast<IDataColumnInfo>());
            List<FieldAndHighlightingString> list = new List<FieldAndHighlightingString>();
            foreach (IDataColumnInfo info in columns)
            {
                list.Add(new FieldAndHighlightingString(info.FieldName, exactMatch ? searchText : GetTextHighlightingString(results.SearchTexts, filterCondition)));
            }
            foreach (FindSearchField field in results.Fields)
            {
                List<FieldAndHighlightingString> list2 = (from fhr in list
                    where fhr.Field.ToLower() == field.Name.ToLower()
                    select fhr).ToList<FieldAndHighlightingString>();
                if (list2.Count != 0)
                {
                    list2[0].AddHighlightingString(GetTextHighlightingString(field.Values, filterCondition));
                }
            }
            return list;
        }

        public static List<string> ParseColumnsString(string columnsString)
        {
            char[] separator = new char[] { ';' };
            Func<string, string> selector = <>c.<>9__0_0;
            if (<>c.<>9__0_0 == null)
            {
                Func<string, string> local1 = <>c.<>9__0_0;
                selector = <>c.<>9__0_0 = s => s.Trim();
            }
            Func<string, bool> predicate = <>c.<>9__0_1;
            if (<>c.<>9__0_1 == null)
            {
                Func<string, bool> local2 = <>c.<>9__0_1;
                predicate = <>c.<>9__0_1 = s => !string.IsNullOrEmpty(s);
            }
            return columnsString.Split(separator).Select<string, string>(selector).Where<string>(predicate).Distinct<string>().ToList<string>();
        }

        private static void RemovePlusMinusIfNeed(ref string searchText)
        {
            if (!searchText.Contains("\""))
            {
                string str = string.Empty;
                char[] separator = new char[] { ' ' };
                foreach (string str2 in searchText.Split(separator))
                {
                    if (!string.IsNullOrWhiteSpace(str2) && ((str2 != "-") && (str2 != "+")))
                    {
                        str = str + (string.IsNullOrEmpty(str) ? string.Empty : " ") + str2;
                    }
                }
                searchText = str;
            }
        }

        public static void UpdateTextHighlighting(BaseEditSettings settings, TextHighlightingProperties highlightingProperties)
        {
            ISupportTextHighlighting highlighting = settings as ISupportTextHighlighting;
            if (highlighting != null)
            {
                highlightingProperties ??= GetDefaultTextHighlightingProperties();
                LookUpEditHelper.SetHighlightedText(highlighting, highlightingProperties.Text, highlightingProperties.FilterCondition);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SearchControlHelper.<>c <>9 = new SearchControlHelper.<>c();
            public static Func<string, string> <>9__0_0;
            public static Func<string, bool> <>9__0_1;
            public static Func<CustomFilterColumn, IDataColumnInfo> <>9__8_1;

            internal IDataColumnInfo <ApplyCriteriaOperatorCustomFormat>b__8_1(CustomFilterColumn col) => 
                col.Column;

            internal string <ParseColumnsString>b__0_0(string s) => 
                s.Trim();

            internal bool <ParseColumnsString>b__0_1(string s) => 
                !string.IsNullOrEmpty(s);
        }
    }
}

