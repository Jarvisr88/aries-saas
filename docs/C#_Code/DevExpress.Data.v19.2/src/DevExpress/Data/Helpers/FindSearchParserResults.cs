namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class FindSearchParserResults
    {
        private FindColumnInfo[] columnNames;
        private string sourceText;
        private string[] searchText;
        private List<FindSearchField> fields;
        private Dictionary<string, string[]> cache;

        public FindSearchParserResults(string sourceText, string[] searchText, List<FindSearchField> fields);
        public void AppendColumnFieldPrefixes();
        public bool ContainsField(string field);
        private void EnsureFieldsCache();
        private string[] GetFieldCache(string field);
        private string GetFieldMatchedText(string field, string text, string[] cachedInfo);
        public DisplayTextHighlightRange[] GetMatchedRanges(string displayText, string fieldName);
        private IEnumerable<DisplayTextHighlightRange> GetMatchedRangesCore(string displayText, string fieldName);
        [IteratorStateMachine(typeof(FindSearchParserResults.<GetMatchedRangesCoreCore>d__30))]
        private IEnumerable<DisplayTextHighlightRange> GetMatchedRangesCoreCore(string text, IEnumerable<string> strings);
        private string GetMatchedSearchText(string text);
        public string GetMatchedText(string field, string text);
        public bool IsAllowHighlight(string field, string text);
        public string[] RemoveFieldColumns(string[] columns);

        public FindColumnInfo[] ColumnNames { get; set; }

        public FindSearchField this[int index] { get; }

        public List<FindSearchField> Fields { get; }

        public string[] SearchTexts { get; }

        public string SearchText { get; }

        public string SourceText { get; }

        public int FieldCount { get; }

        [CompilerGenerated]
        private sealed class <GetMatchedRangesCoreCore>d__30 : IEnumerable<DisplayTextHighlightRange>, IEnumerable, IEnumerator<DisplayTextHighlightRange>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private DisplayTextHighlightRange <>2__current;
            private int <>l__initialThreadId;
            private string text;
            public string <>3__text;
            private IEnumerable<string> strings;
            public IEnumerable<string> <>3__strings;
            private IEnumerator<string> <>7__wrap1;

            [DebuggerHidden]
            public <GetMatchedRangesCoreCore>d__30(int <>1__state);
            private void <>m__Finally1();
            private bool MoveNext();
            [DebuggerHidden]
            IEnumerator<DisplayTextHighlightRange> IEnumerable<DisplayTextHighlightRange>.GetEnumerator();
            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator();
            [DebuggerHidden]
            void IEnumerator.Reset();
            [DebuggerHidden]
            void IDisposable.Dispose();

            DisplayTextHighlightRange IEnumerator<DisplayTextHighlightRange>.Current { [DebuggerHidden] get; }

            object IEnumerator.Current { [DebuggerHidden] get; }
        }
    }
}

