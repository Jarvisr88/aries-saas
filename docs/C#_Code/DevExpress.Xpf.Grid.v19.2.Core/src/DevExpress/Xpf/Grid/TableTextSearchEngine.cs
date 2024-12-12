namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Editors.Native;
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class TableTextSearchEngine : TextSearchEngineBase
    {
        private readonly Func<string, TableIndex, bool, bool, DataViewBase, TableIndex> searchCallback;

        public TableTextSearchEngine(Func<string, TableIndex, bool, bool, DataViewBase, TableIndex> searchCallback, int? delay) : this((delay != null) ? new TimeSpan(0, 0, 0, 0, delay.Value) : TextSearchEngineBase.DefaultTextSearchTimeout)
        {
            this.searchCallback = searchCallback;
        }

        internal bool DoSearch(string nextChar, int currentItemIndex, int currentColumnIndex, int level, DataViewBase view, bool? down = new bool?())
        {
            TableIndex index2;
            if (this.IsEscape(nextChar))
            {
                this.ResetState();
                return false;
            }
            bool lookForFallbackMatchToo = false;
            TableIndex startIndex = null;
            if (base.IsActive)
            {
                startIndex = new TableIndex(currentItemIndex, currentColumnIndex, view, level);
            }
            if ((base.charsEntered.Count > 0) && (string.Compare(base.charsEntered[base.charsEntered.Count - 1], nextChar, true, CultureInfo.CurrentCulture) == 0))
            {
                lookForFallbackMatchToo = true;
            }
            bool wasNewCharUsed = false;
            if (down != null)
            {
                index2 = this.FindMatchingPrefix(base.SeachText, nextChar, new TableIndex(currentItemIndex, currentColumnIndex, view, level), true, ref wasNewCharUsed, view, new bool?(down.Value));
            }
            else
            {
                bool? nullable = null;
                index2 = this.FindMatchingPrefix(base.Prefix, nextChar, startIndex, lookForFallbackMatchToo, ref wasNewCharUsed, view, nullable);
            }
            if (index2 == null)
            {
                if (base.IsBackSpace(nextChar) && (base.Prefix.Length > 0))
                {
                    base.Prefix = base.Prefix.Substring(0, base.Prefix.Length - 1);
                }
            }
            else
            {
                if (!base.IsActive || !ReferenceEquals(index2, startIndex))
                {
                    this.MatchedItemIndex = index2;
                }
                if (wasNewCharUsed)
                {
                    base.AddCharToPrefix(nextChar);
                }
                if (!base.IsActive)
                {
                    base.IsActive = true;
                    if (down == null)
                    {
                        base.SeachText = base.Prefix;
                    }
                }
            }
            if (base.IsActive)
            {
                base.ResetTimeout();
            }
            return (index2 != null);
        }

        private TableIndex FindMatchingPrefix(string prefix, string newChar, TableIndex startIndex, bool lookForFallbackMatchToo, ref bool wasNewCharUsed, DataViewBase view, bool? down)
        {
            TableIndex index = null;
            string str = base.StartFindMatchingPrefix(prefix, newChar);
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }
            wasNewCharUsed = false;
            TableIndex index2 = (down != null) ? null : this.searchCallback(str, startIndex, false, true, view);
            if (index2 != null)
            {
                wasNewCharUsed = true;
                index = index2;
            }
            else
            {
                if (lookForFallbackMatchToo)
                {
                    index2 = this.searchCallback(prefix, startIndex, true, (down != null) ? down.Value : true, view);
                }
                if (index2 != null)
                {
                    index = index2;
                }
                else
                {
                    index2 = this.searchCallback(prefix, null, false, (down != null) ? down.Value : true, view);
                    if (index2 != null)
                    {
                        index = index2;
                    }
                }
            }
            return index;
        }

        private bool IsEscape(string text) => 
            !string.IsNullOrEmpty(text) ? (text[0] == '\x001b') : false;

        protected override void ResetState()
        {
            base.ResetState();
            this.MatchedItemIndex = null;
        }

        public TableIndex MatchedItemIndex { get; private set; }

        public class TableIndex
        {
            public TableIndex()
            {
            }

            public TableIndex(int rowIndex, int columnIndex, DataViewBase view, int level)
            {
                this.RowIndex = rowIndex;
                this.ColumnIndex = columnIndex;
                this.View = view;
                this.Level = level;
            }

            public int RowIndex { get; set; }

            public int ColumnIndex { get; set; }

            public DataViewBase View { get; set; }

            public int Level { get; set; }
        }
    }
}

