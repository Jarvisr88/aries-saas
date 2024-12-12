namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public abstract class PdfHighlight
    {
        private readonly int pageIndex;

        protected PdfHighlight(int pageIndex)
        {
            this.pageIndex = pageIndex;
        }

        public int PageIndex =>
            this.pageIndex;

        public abstract IList<PdfOrientedRectangle> Rectangles { get; }

        public virtual IList<PdfOrientedRectangle> MarkupRectangles =>
            this.Rectangles;
    }
}

