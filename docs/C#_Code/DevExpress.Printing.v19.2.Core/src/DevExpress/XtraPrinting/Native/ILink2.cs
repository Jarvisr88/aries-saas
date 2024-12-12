namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;
    using System.Drawing.Printing;

    public interface ILink2 : ILink
    {
        void AddSubreport(PrintingSystemBase ps, DocumentBand band, PointF offset);

        System.Drawing.Printing.PaperKind PaperKind { get; set; }

        System.Drawing.Printing.Margins Margins { get; set; }

        System.Drawing.Printing.Margins MinMargins { get; set; }

        Size CustomPaperSize { get; set; }
    }
}

