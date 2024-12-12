﻿namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using System;

    public class PdfDarkUpwardDiagonalHatchPatternConstructor : PdfBackwardDiagonalHatchPatternConstructor
    {
        public PdfDarkUpwardDiagonalHatchPatternConstructor(DXHatchBrush hatchBrush) : base(hatchBrush)
        {
        }

        protected override double LineStep =>
            base.LineStep / 2.0;

        protected override double LineWidth =>
            base.LineWidth * 2.0;
    }
}

