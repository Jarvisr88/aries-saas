namespace DevExpress.Xpf.PdfViewer.Internal
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;

    public class PdfTargetScroll : PdfTarget
    {
        private readonly bool inCenter;

        public PdfTargetScroll(int pageIndex, PdfRectangle bounds, bool inCenter) : base(PdfTargetMode.XYZ, pageIndex, bounds)
        {
            this.inCenter = inCenter;
        }

        public bool InCenter =>
            this.inCenter;
    }
}

