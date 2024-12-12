namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;

    public class PdfImageSelection : PdfSelection
    {
        private readonly int pageIndex;
        private readonly PdfPageImageData pageImageData;
        private readonly PdfRectangle clipRectangle;
        private PdfOrientedRectangle selectionRecangle;

        public PdfImageSelection(int pageIndex, PdfPageImageData pageImageData, PdfRectangle clipRectangle)
        {
            this.pageIndex = pageIndex;
            this.pageImageData = pageImageData;
            this.clipRectangle = clipRectangle;
        }

        public static bool AreEqual(PdfImageSelection selection1, PdfImageSelection selection2) => 
            (selection1.pageIndex == selection2.pageIndex) && (PdfRectangle.AreEqual(selection1.clipRectangle, selection2.clipRectangle, 0.0001) && selection1.pageImageData.Equals(selection2.pageImageData));

        public PdfPageImageData PageImageData =>
            this.pageImageData;

        public PdfRectangle ClipRectangle
        {
            get
            {
                PdfRectangle boundingRectangle = this.pageImageData.BoundingRectangle;
                return ((this.clipRectangle != null) ? new PdfRectangle(this.clipRectangle.Left - boundingRectangle.Left, this.clipRectangle.Bottom - boundingRectangle.Bottom, this.clipRectangle.Right - boundingRectangle.Left, this.clipRectangle.Top - boundingRectangle.Bottom) : new PdfRectangle(0.0, 0.0, boundingRectangle.Width, boundingRectangle.Height));
            }
        }

        public override PdfDocumentContentType ContentType =>
            PdfDocumentContentType.Image;

        public override IList<PdfHighlight> Highlights
        {
            get
            {
                if (this.selectionRecangle == null)
                {
                    PdfRectangle boundingRectangle = this.pageImageData.BoundingRectangle;
                    if (this.clipRectangle == null)
                    {
                        this.selectionRecangle = new PdfOrientedRectangle(boundingRectangle.TopLeft, boundingRectangle.Width, boundingRectangle.Height, 0.0);
                    }
                    else
                    {
                        PdfRectangle clipRectangle = this.ClipRectangle;
                        this.selectionRecangle = new PdfOrientedRectangle(new PdfPoint(boundingRectangle.Left + clipRectangle.Left, boundingRectangle.Bottom + clipRectangle.Top), clipRectangle.Width, clipRectangle.Height, 0.0);
                    }
                }
                return new PdfHighlight[] { new PdfImageHighlight(this.pageIndex, this.selectionRecangle) };
            }
        }
    }
}

