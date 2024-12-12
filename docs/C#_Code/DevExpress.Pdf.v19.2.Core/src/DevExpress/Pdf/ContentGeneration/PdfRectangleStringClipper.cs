namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using System;

    public class PdfRectangleStringClipper : PdfStringClipper
    {
        private readonly double clipTop;
        private readonly double clipBottom;

        public PdfRectangleStringClipper(PdfRectangle clipRectangle)
        {
            this.clipTop = clipRectangle.Top;
            this.clipBottom = clipRectangle.Bottom;
        }

        public override PdfTextLineBounds CalculateActualLineBounds(double firstLineY, double lineSpacing, int lineCount)
        {
            int num = (int) Math.Floor((double) (((firstLineY - this.clipTop) / lineSpacing) + 0.1));
            int num2 = (int) Math.Ceiling((double) (((firstLineY - this.clipBottom) / lineSpacing) - 0.1));
            return ((num <= (lineCount - 1)) ? new PdfTextLineBounds(Math.Max(0, num), Math.Min(num2, lineCount)) : new PdfTextLineBounds(0, 0));
        }
    }
}

