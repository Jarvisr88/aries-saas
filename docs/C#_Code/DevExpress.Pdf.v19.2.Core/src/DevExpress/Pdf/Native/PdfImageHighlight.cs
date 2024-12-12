namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;

    public class PdfImageHighlight : PdfHighlight
    {
        private readonly PdfOrientedRectangle rectangle;

        internal PdfImageHighlight(int pageIndex, PdfOrientedRectangle rectangle) : base(pageIndex)
        {
            this.rectangle = rectangle;
        }

        public override IList<PdfOrientedRectangle> Rectangles =>
            new PdfOrientedRectangle[] { this.rectangle };
    }
}

