namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class FindSearchTools
    {
        private static Func<string, DisplayTextHighlightRange?> CreatePrimitiveHighlighter(FilterCondition operation, string text);
        private static Func<string, DisplayTextHighlightRange?> CreatePrimitiveHighlighterContains(string specificText);
        private static Func<string, DisplayTextHighlightRange?> CreatePrimitiveHighlighterEquals(string specificText);
        private static Func<string, DisplayTextHighlightRange?> CreatePrimitiveHighlighterLike(string specificText);
        private static Func<string, DisplayTextHighlightRange?> CreatePrimitiveHighlighterStartsWith(string specificText);
        public static void Extract(ParseFindPanelTextEventArgs args, out CriteriaOperator findCriteria, out Func<string, string, DisplayTextHighlightRange[]> highlighter);
        public static void GetFindCriteriaAndHighlightedTextGetter(FindPanelParserKind parserKind, string findPanelText, IEnumerable<IDataColumnInfo> columns, FilterCondition defaultOperation, bool clientMode, out CriteriaOperator _findCriteria, out Func<string, string, DisplayTextHighlightRange[]> _highlightedTextGetter);
        private static void GetFindCriteriaAndHighlightedTextGetter(string findPanelText, IEnumerable<IDataColumnInfo> columns, char defaultGroupSymbol, FilterCondition defaultOperation, bool clientMode, out CriteriaOperator _findCriteria, out Func<string, string, DisplayTextHighlightRange[]> _highlightedTextGetter);
        public static void GetFindCriteriaAndHighlightedTextGetterKindAnd(string findPanelText, IEnumerable<IDataColumnInfo> columns, FilterCondition defaultOperation, bool clientMode, out CriteriaOperator _findCriteria, out Func<string, string, DisplayTextHighlightRange[]> _highlightedTextGetter);
        public static void GetFindCriteriaAndHighlightedTextGetterKindExact(string findPanelText, IEnumerable<IDataColumnInfo> columns, FilterCondition defaultOperation, bool clientMode, out CriteriaOperator _findCriteria, out Func<string, string, DisplayTextHighlightRange[]> _highlightedTextGetter);
        public static void GetFindCriteriaAndHighlightedTextGetterKindMixed(string findPanelText, IEnumerable<IDataColumnInfo> columns, FilterCondition defaultOperation, bool clientMode, out CriteriaOperator _findCriteria, out Func<string, string, DisplayTextHighlightRange[]> _highlightedTextGetter);
        public static void GetFindCriteriaAndHighlightedTextGetterKindOr(string findPanelText, IEnumerable<IDataColumnInfo> columns, FilterCondition defaultOperation, bool clientMode, out CriteriaOperator _findCriteria, out Func<string, string, DisplayTextHighlightRange[]> _highlightedTextGetter);
        private static bool IsFineAsIs(DisplayTextHighlightRange[] input, int displayTextLength);
        public static DisplayTextHighlightRange[] Sanitize(DisplayTextHighlightRange[] input, int displayTextLength);
        private static DisplayTextHighlightRange[] SanitizeCore(DisplayTextHighlightRange[] input, int displayTextLength);
        public static void WinFormsGetFindCriteriaAndHighlightedTextGetter(FindPanelParserKind parserKind, string findPanelText, IEnumerable<string> columns, FilterCondition defaultOperation, out CriteriaOperator _findCriteria, out Func<string, string, DisplayTextHighlightRange[]> _highlightedTextGetter);
        public static void WinFormsGetFindCriteriaAndHighlightedTextGetter(FindPanelParserKind parserKind, string findPanelText, IEnumerable<IDataColumnInfo> columns, FilterCondition defaultOperation, bool clientMode, out CriteriaOperator _findCriteria, out Func<string, string, DisplayTextHighlightRange[]> _highlightedTextGetter);
        public static void WpfGetFindCriteria(FindPanelParserKind parserKind, string findPanelText, IEnumerable<string> columns, FilterCondition defaultOperation, out CriteriaOperator _findCriteria);
        public static void WpfGetFindCriteria(FindPanelParserKind parserKind, string findPanelText, IEnumerable<IDataColumnInfo> columns, FilterCondition defaultOperation, bool clientMode, out CriteriaOperator _findCriteria);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FindSearchTools.<>c <>9;
            public static Func<DisplayTextHighlightRange?, bool> <>9__0_6;
            public static Func<DisplayTextHighlightRange?, DisplayTextHighlightRange> <>9__0_7;
            public static Func<string, DisplayTextHighlightRange?> <>9__5_0;
            public static Func<char, bool> <>9__5_1;
            public static Func<DisplayTextHighlightRange, int> <>9__8_1;
            public static Func<string, string, DisplayTextHighlightRange[]> <>9__13_1;
            public static Func<string, FindSearchTools.FakeColumnInfo> <>9__17_0;
            public static Func<string, FindSearchTools.FakeColumnInfo> <>9__19_0;

            static <>c();
            internal DisplayTextHighlightRange? <CreatePrimitiveHighlighterLike>b__5_0(string displayText);
            internal bool <CreatePrimitiveHighlighterLike>b__5_1(char ch);
            internal bool <GetFindCriteriaAndHighlightedTextGetter>b__0_6(DisplayTextHighlightRange? h);
            internal DisplayTextHighlightRange <GetFindCriteriaAndHighlightedTextGetter>b__0_7(DisplayTextHighlightRange? h);
            internal DisplayTextHighlightRange[] <GetFindCriteriaAndHighlightedTextGetterKindExact>b__13_1(string displayText, string fieldName);
            internal int <SanitizeCore>b__8_1(DisplayTextHighlightRange r);
            internal FindSearchTools.FakeColumnInfo <WinFormsGetFindCriteriaAndHighlightedTextGetter>b__17_0(string nm);
            internal FindSearchTools.FakeColumnInfo <WpfGetFindCriteria>b__19_0(string nm);
        }

        private class FakeColumnInfo : IDataColumnInfo
        {
            public FakeColumnInfo(string fieldName);

            public List<IDataColumnInfo> Columns { get; }

            public string UnboundExpression { get; }

            public string Caption { get; }

            public string FieldName { get; }

            public string Name { get; }

            public Type FieldType { get; }

            public DataControllerBase Controller { get; }
        }
    }
}

