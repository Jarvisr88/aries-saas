namespace DevExpress.XtraPrinting
{
    using System;
    using System.Runtime.InteropServices;

    [ComVisible(false)]
    public interface IPrintHeaderFooter
    {
        string InnerPageHeader { get; }

        string InnerPageFooter { get; }

        string ReportHeader { get; }

        string ReportFooter { get; }
    }
}

