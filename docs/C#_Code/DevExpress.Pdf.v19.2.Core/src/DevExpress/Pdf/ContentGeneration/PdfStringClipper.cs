namespace DevExpress.Pdf.ContentGeneration
{
    using System;

    public class PdfStringClipper
    {
        public virtual PdfTextLineBounds CalculateActualLineBounds(double firstLineY, double lineSpacing, int lineCount) => 
            new PdfTextLineBounds(0, lineCount);
    }
}

