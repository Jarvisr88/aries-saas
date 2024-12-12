namespace DevExpress.ReportServer.Printing.Services
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Drawing;
    using DevExpress.XtraPrinting.Native;
    using System;

    public class EmptyPageFactory : IEmptyPageFactory
    {
        public Page CreateEmptyPage(int pageIndex, int pageCount)
        {
            PSPage page1 = new PSPage(new PageData());
            page1.OriginalIndex = pageIndex;
            page1.OriginalPageCount = pageCount;
            Page page = page1;
            page.AssignWatermark(new Watermark());
            return page;
        }
    }
}

