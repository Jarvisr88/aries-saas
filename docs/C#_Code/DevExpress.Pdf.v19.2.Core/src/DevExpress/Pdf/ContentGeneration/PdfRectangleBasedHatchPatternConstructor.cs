namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfRectangleBasedHatchPatternConstructor : PdfHatchPatternConstructor
    {
        private readonly IEnumerable<PdfHatchPatternRect> rectangles;

        public PdfRectangleBasedHatchPatternConstructor(DXHatchBrush hatchBrush, IEnumerable<PdfHatchPatternRect> rectangles) : base(hatchBrush)
        {
            this.rectangles = rectangles;
        }

        protected override void FillCommands(PdfCommandConstructor constructor)
        {
            base.FillCommands(constructor);
            double num = this.LineStep / 8.0;
            foreach (PdfHatchPatternRect rect in this.rectangles)
            {
                constructor.FillRectangle(new PdfRectangle(rect.X * num, rect.Y * num, rect.Right * num, rect.Top * num));
            }
        }
    }
}

