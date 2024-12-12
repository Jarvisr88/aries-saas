namespace DevExpress.XtraReports
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Caching;
    using System;
    using System.Collections.Generic;

    internal class DocumentModifier : DocumentModifierBase
    {
        private CachedDocumentUpdater updater;

        public DocumentModifier(PageList pages, CachedDocumentUpdater updater) : base(pages)
        {
            this.updater = updater;
        }

        public override void AddPages(IEnumerable<Page> pages)
        {
            this.updater.AddRange(pages);
        }

        public override void InsertPage(int index, Page page)
        {
            this.updater.InsertPage(index, page);
        }

        public override void RemovePageAt(int index)
        {
            this.updater.RemovePageAt(index);
        }
    }
}

