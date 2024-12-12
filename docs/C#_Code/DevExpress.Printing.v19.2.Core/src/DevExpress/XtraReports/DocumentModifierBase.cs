namespace DevExpress.XtraReports
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections.Generic;

    internal class DocumentModifierBase : IDocumentModifier
    {
        private PageList pages;

        public DocumentModifierBase(PageList pages)
        {
            this.pages = pages;
        }

        public virtual void AddPages(IEnumerable<Page> pages)
        {
            this.pages.AddRange(pages);
        }

        public int GetPageIndexByID(long id) => 
            this.pages.GetPageIndexByID(id);

        public virtual void InsertPage(int index, Page page)
        {
            this.pages.Insert(index, page);
        }

        public virtual void RemovePageAt(int index)
        {
            this.pages.RemoveAt(index);
        }

        public int PageCount =>
            this.pages.Count;
    }
}

