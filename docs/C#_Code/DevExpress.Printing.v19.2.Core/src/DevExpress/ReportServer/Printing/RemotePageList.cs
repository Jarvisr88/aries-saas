namespace DevExpress.ReportServer.Printing
{
    using DevExpress.XtraPrinting;
    using System;

    internal class RemotePageList : PageList, IDisposable
    {
        public RemotePageList(RemoteDocument document, DevExpress.ReportServer.Printing.RemoteInnerPageList list) : base(document, list)
        {
            list.PageCountChanged += new EventHandler<EventArgs>(this.innerList_PageCountChanged);
        }

        public void Dispose()
        {
            this.RemoteInnerPageList.Dispose();
        }

        private void innerList_PageCountChanged(object sender, EventArgs e)
        {
            base.RaiseDocumentChanged();
        }

        protected override void OnClear(int count)
        {
            base.OnClear(count);
            base.InnerList.Clear();
        }

        internal void ReplaceCachedPage(int i, Page page)
        {
            page.SetOwner(this, i);
            this.RemoteInnerPageList.ReplaceCachedPage(i, page);
        }

        public void SetCount(int pageCount)
        {
            if (this.RemoteInnerPageList.Count != pageCount)
            {
                this.RemoteInnerPageList.SetCount(pageCount);
            }
        }

        private DevExpress.ReportServer.Printing.RemoteInnerPageList RemoteInnerPageList =>
            (DevExpress.ReportServer.Printing.RemoteInnerPageList) base.InnerList;
    }
}

