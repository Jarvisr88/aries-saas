namespace DevExpress.XtraReports
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections.Generic;

    public interface IDocumentModifier
    {
        void AddPages(IEnumerable<Page> pages);
        int GetPageIndexByID(long id);
        void InsertPage(int index, Page page);
        void RemovePageAt(int index);

        int PageCount { get; }
    }
}

