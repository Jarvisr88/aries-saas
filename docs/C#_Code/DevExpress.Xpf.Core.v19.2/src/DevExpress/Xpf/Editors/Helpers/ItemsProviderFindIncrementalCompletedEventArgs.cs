namespace DevExpress.Xpf.Editors.Helpers
{
    using System;
    using System.Runtime.CompilerServices;

    public class ItemsProviderFindIncrementalCompletedEventArgs : ItemsProviderChangedEventArgs
    {
        public ItemsProviderFindIncrementalCompletedEventArgs(string text, int startIndex, bool searchNext, bool ignoreStartIndex, object value)
        {
            this.Value = value;
            this.Text = text;
            this.StartIndex = startIndex;
            this.SearchNext = searchNext;
            this.IgnoreStartIndex = ignoreStartIndex;
        }

        public object Value { get; private set; }

        public string Text { get; private set; }

        public int StartIndex { get; private set; }

        public bool SearchNext { get; private set; }

        public bool IgnoreStartIndex { get; private set; }
    }
}

