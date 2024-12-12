namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;

    public class PdfForwardDiagonalHatchPatternConstructor : PdfHatchPatternConstructor
    {
        public PdfForwardDiagonalHatchPatternConstructor(DXHatchBrush hatchBrush) : base(hatchBrush)
        {
        }

        protected override void FillCommands(PdfCommandConstructor commandConstructor)
        {
            base.FillCommands(commandConstructor);
            double lineStep = this.LineStep;
            double y = lineStep / 2.0;
            commandConstructor.DrawLine(new PdfPoint(0.0, y), new PdfPoint(y, lineStep));
            commandConstructor.DrawLine(new PdfPoint(y, 0.0), new PdfPoint(lineStep, y));
        }
    }
}

