namespace DevExpress.XtraPrinting
{
    using System;

    public class PageInsertCompleteEventArgs : EventArgs
    {
        private int pageIndex;

        public PageInsertCompleteEventArgs(int pageIndex)
        {
            this.pageIndex = pageIndex;
        }

        public int PageIndex =>
            this.pageIndex;
    }
}

