namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;

    public class PdfRoundLineCapPainter : PdfLineCapPainter
    {
        private readonly PdfRectangle rectangle;

        public PdfRoundLineCapPainter(double penWidth)
        {
            this.rectangle = this.GetBounds(penWidth);
        }

        protected virtual PdfRectangle GetBounds(double penWidth)
        {
            double right = penWidth / 2.0;
            return new PdfRectangle(-right, -right, right, right);
        }

        protected override void PerformDraw(PdfCommandConstructor constructor, PdfTransformationMatrix lineTransform)
        {
            constructor.SaveGraphicsState();
            constructor.ModifyTransformationMatrix(lineTransform);
            constructor.FillEllipse(this.rectangle);
            constructor.RestoreGraphicsState();
        }
    }
}

