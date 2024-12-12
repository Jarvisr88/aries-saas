namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;

    public class PdfDashedHorizontalHatchPatternConstructor : PdfHatchPatternConstructor
    {
        public PdfDashedHorizontalHatchPatternConstructor(DXHatchBrush hatchBrush) : base(hatchBrush)
        {
        }

        protected override void FillCommands(PdfCommandConstructor constructor)
        {
            base.FillCommands(constructor);
            constructor.SetLineCapStyle(PdfLineCapStyle.Butt);
            double dashLength = this.LineStep / 2.0;
            constructor.SetLineStyle(PdfLineStyle.CreateDashed(dashLength, dashLength, 0.0));
            double y = this.LineWidth / 2.0;
            constructor.DrawLine(new PdfPoint(0.0, y), new PdfPoint(this.LineStep, y));
            constructor.SetLineStyle(PdfLineStyle.CreateDashed(dashLength, dashLength, dashLength));
            double num3 = y + dashLength;
            constructor.DrawLine(new PdfPoint(0.0, num3), new PdfPoint(this.LineStep, num3));
        }

        protected override PdfLineCapStyle LineCapStyle =>
            PdfLineCapStyle.Butt;
    }
}

