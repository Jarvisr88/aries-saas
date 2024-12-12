namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfStringPaintingAtPointStrategy : PdfStringPaintingStrategy
    {
        private readonly PdfPoint location;

        public PdfStringPaintingAtPointStrategy(PdfPoint location, PdfStringMeasurer measurer) : base(measurer)
        {
            this.location = location;
        }

        public override void Clip(PdfCommandConstructor commandConstructor)
        {
        }

        public override double GetFirstLineVerticalPosition(int lineCount)
        {
            PdfStringAlignment lineAlignment = base.Format.LineAlignment;
            return ((lineAlignment == PdfStringAlignment.Center) ? (this.location.Y + (base.Measurer.MeasureStringHeight(lineCount) / 2.0)) : ((lineAlignment == PdfStringAlignment.Far) ? (this.location.Y + base.Measurer.MeasureStringHeight(lineCount)) : this.location.Y));
        }

        public override double GetHorizontalPosition(IList<DXCluster> line)
        {
            PdfStringAlignment actualAlignment = base.GetActualAlignment(base.Format.Alignment);
            return ((actualAlignment == PdfStringAlignment.Center) ? (this.location.X - (base.Measurer.MeasureWidth(line) / 2.0)) : ((actualAlignment == PdfStringAlignment.Far) ? ((this.location.X - base.Measurer.MeasureWidth(line)) - base.Measurer.TrailingOffset) : (this.location.X + base.Measurer.LeadingOffset)));
        }
    }
}

