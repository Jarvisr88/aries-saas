namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Data.Async;
    using System;
    using System.Runtime.CompilerServices;

    public class FindRowIndexByValueOnFindIncrementalCompleter : IAsyncListServerDataViewCommandCompletedContainer
    {
        public FindRowIndexByValueOnFindIncrementalCompleter(IAsyncListServerDataView view, string text, int startIndex, bool searchNext, bool ignoreStartIndex)
        {
            this.View = view;
            this.Text = text;
            this.StartIndex = startIndex;
            this.SearchNext = searchNext;
            this.IgnoreStartIndex = ignoreStartIndex;
        }

        public void Completed(object args)
        {
            CommandLocateByValue value2 = (CommandLocateByValue) args;
            if (value2.RowIndex >= 0)
            {
                this.View.NotifyFindIncrementalCompleted(this.Text, this.StartIndex, this.SearchNext, this.IgnoreStartIndex, value2.RowIndex);
            }
        }

        public IAsyncListServerDataView View { get; private set; }

        public string Text { get; private set; }

        public int StartIndex { get; private set; }

        public bool SearchNext { get; private set; }

        public bool IgnoreStartIndex { get; private set; }
    }
}

