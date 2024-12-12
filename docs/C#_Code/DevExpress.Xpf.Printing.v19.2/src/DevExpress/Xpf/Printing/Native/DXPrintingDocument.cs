namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;

    public class DXPrintingDocument : PrintingDocument
    {
        public DXPrintingDocument(PrintingSystemBase ps) : base(ps, null)
        {
        }

        protected internal override DocumentBand AddReportContainer()
        {
            throw new NotImplementedException();
        }

        protected internal override void Begin()
        {
            base.SetRoot(new RootDocumentBand(base.PrintingSystem));
            base.Begin();
        }

        protected internal override void BeginReport(DocumentBand docBand, PointF offset)
        {
            throw new NotImplementedException();
        }

        protected internal override void EndReport()
        {
            throw new NotImplementedException();
        }

        protected internal override void InsertPageBreak(float pos)
        {
            throw new NotImplementedException();
        }

        protected internal override void InsertPageBreak(float pos, CustomPageData nextPageData)
        {
            throw new NotImplementedException();
        }

        public override void ShowFromNewPage(Brick brick)
        {
            throw new NotImplementedException();
        }
    }
}

