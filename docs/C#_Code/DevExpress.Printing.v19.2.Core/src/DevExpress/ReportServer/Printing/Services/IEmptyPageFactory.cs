namespace DevExpress.ReportServer.Printing.Services
{
    using DevExpress.XtraPrinting;
    using System;

    public interface IEmptyPageFactory
    {
        Page CreateEmptyPage(int pageIndex, int pageCount);
    }
}

