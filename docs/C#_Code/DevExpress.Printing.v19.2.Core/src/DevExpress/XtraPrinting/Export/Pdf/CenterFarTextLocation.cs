﻿namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;

    public class CenterFarTextLocation : CenterTextLocation
    {
        public CenterFarTextLocation(bool directionVertical) : base(directionVertical)
        {
        }

        protected override float CalculateTextX_Horizontal(PdfGraphicsRectangle pdfRect, float textWidth) => 
            pdfRect.Right - textWidth;

        protected override float CalculateTextX_Vertical(PdfGraphicsRectangle pdfRect, float textWidth) => 
            pdfRect.Right - (textWidth / 2f);

        protected override float CalculateTextY_Vertical(PdfGraphicsRectangle pdfRect, float textHeight) => 
            pdfRect.Bottom + (pdfRect.Height / 2f);
    }
}
