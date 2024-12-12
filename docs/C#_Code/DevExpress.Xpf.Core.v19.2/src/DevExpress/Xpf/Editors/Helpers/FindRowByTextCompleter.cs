namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Data.Async;
    using System;
    using System.Runtime.CompilerServices;

    public class FindRowByTextCompleter : IAsyncListServerDataViewCommandCompletedContainer
    {
        public FindRowByTextCompleter(IAsyncListServerDataView view, string text, int startIndex, bool searchNext, bool ignoreStartIndex)
        {
            this.View = view;
            this.Text = text;
            this.StartIndex = startIndex;
            this.SearchNext = searchNext;
            this.IgnoreStartIndex = ignoreStartIndex;
        }

        public void Completed(object args)
        {
            CommandFindIncremental incremental = (CommandFindIncremental) args;
            if (incremental.RowIndex >= 0)
            {
                this.View.NotifyFindIncrementalCompleted(this.Text, this.StartIndex, this.SearchNext, this.IgnoreStartIndex, incremental.RowIndex);
            }
        }

        public IAsyncListServerDataView View { get; private set; }

        public string Text { get; private set; }

        public int StartIndex { get; private set; }

        public bool SearchNext { get; private set; }

        public bool IgnoreStartIndex { get; private set; }
    }
}

