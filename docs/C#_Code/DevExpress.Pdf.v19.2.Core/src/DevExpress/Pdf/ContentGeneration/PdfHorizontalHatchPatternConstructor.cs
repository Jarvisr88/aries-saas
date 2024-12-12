namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;

    public class PdfHorizontalHatchPatternConstructor : PdfHatchPatternConstructor
    {
        public PdfHorizontalHatchPatternConstructor(DXHatchBrush hatchBrush) : base(hatchBrush)
        {
        }

        protected override void FillCommands(PdfCommandConstructor constructor)
        {
            base.FillCommands(constructor);
            double y = this.LineWidth / 2.0;
            constructor.DrawLine(new PdfPoint(0.0, y), new PdfPoint(this.LineStep, y));
        }
    }
}

