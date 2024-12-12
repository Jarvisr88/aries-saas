namespace DevExpress.Xpf.DocumentViewer
{
    using System;
    using System.Runtime.CompilerServices;

    public class PageIndexChangedEventArgs : EventArgs
    {
        public PageIndexChangedEventArgs(int pageIndex)
        {
            this.PageIndex = pageIndex;
        }

        public int PageIndex { get; private set; }
    }
}

