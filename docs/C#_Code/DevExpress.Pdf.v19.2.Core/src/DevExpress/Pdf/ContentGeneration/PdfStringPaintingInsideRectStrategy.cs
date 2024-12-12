namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfStringPaintingInsideRectStrategy : PdfStringPaintingStrategy
    {
        private readonly PdfRectangle layoutRect;

        public PdfStringPaintingInsideRectStrategy(PdfRectangle layoutRect, PdfStringMeasurer measurer) : base(measurer)
        {
            this.layoutRect = layoutRect;
        }

        public override void Clip(PdfCommandConstructor commandConstructor)
        {
            if (!base.Format.FormatFlags.HasFlag(PdfStringFormatFlags.NoClip))
            {
                commandConstructor.IntersectClip(this.layoutRect);
            }
        }

        public override double GetFirstLineVerticalPosition(int lineCount)
        {
            PdfStringAlignment lineAlignment = base.Format.LineAlignment;
            return ((lineAlignment == PdfStringAlignment.Center) ? (this.layoutRect.Top - ((this.layoutRect.Height - base.Measurer.MeasureStringHeight(lineCount)) / 2.0)) : ((lineAlignment == PdfStringAlignment.Far) ? (this.layoutRect.Top - (this.layoutRect.Height - base.Measurer.MeasureStringHeight(lineCount))) : this.layoutRect.Top));
        }

        public override double GetHorizontalPosition(IList<DXCluster> line)
        {
            PdfStringAlignment actualAlignment = base.GetActualAlignment(base.Format.Alignment);
            return ((actualAlignment == PdfStringAlignment.Center) ? (this.layoutRect.Left + ((this.layoutRect.Width - base.Measurer.MeasureWidth(line)) / 2.0)) : ((actualAlignment == PdfStringAlignment.Far) ? ((this.layoutRect.Right - base.Measurer.MeasureWidth(line)) - base.Measurer.TrailingOffset) : (this.layoutRect.Left + base.Measurer.LeadingOffset)));
        }
    }
}

