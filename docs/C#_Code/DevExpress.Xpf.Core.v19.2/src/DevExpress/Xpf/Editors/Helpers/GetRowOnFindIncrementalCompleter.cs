namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Data.Async;
    using System;
    using System.Runtime.CompilerServices;

    public class GetRowOnFindIncrementalCompleter : GetRowCompleter
    {
        public GetRowOnFindIncrementalCompleter(IAsyncListServerDataView view, string text, int startIndex, bool searchNext, bool ignoreStartIndex) : base(view)
        {
            this.Text = text;
            this.StartIndex = startIndex;
            this.SearchNext = searchNext;
            this.IgnoreStartIndex = ignoreStartIndex;
        }

        protected override void ProcessCommand(CommandGetRow command)
        {
            base.ProcessCommand(command);
            base.View.NotifyFindIncrementalCompleted(this.Text, this.StartIndex, this.SearchNext, this.IgnoreStartIndex, base.ControllerIndex);
        }

        public string Text { get; private set; }

        public int StartIndex { get; private set; }

        public bool SearchNext { get; private set; }

        public bool IgnoreStartIndex { get; private set; }
    }
}

