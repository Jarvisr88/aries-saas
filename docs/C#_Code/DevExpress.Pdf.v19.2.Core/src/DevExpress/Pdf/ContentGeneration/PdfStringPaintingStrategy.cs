namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public abstract class PdfStringPaintingStrategy
    {
        protected PdfStringPaintingStrategy(PdfStringMeasurer measurer)
        {
            this.<Measurer>k__BackingField = measurer;
        }

        public abstract void Clip(PdfCommandConstructor commandConstructor);
        protected PdfStringAlignment GetActualAlignment(PdfStringAlignment alignment)
        {
            if (this.Format.DirectionRightToLeft)
            {
                if (alignment == PdfStringAlignment.Near)
                {
                    return PdfStringAlignment.Far;
                }
                if (alignment == PdfStringAlignment.Far)
                {
                    return PdfStringAlignment.Near;
                }
            }
            return alignment;
        }

        public abstract double GetFirstLineVerticalPosition(int count);
        public abstract double GetHorizontalPosition(IList<DXCluster> line);
        public double MeasureWidth(IList<DXCluster> line) => 
            this.Measurer.MeasureWidth(line);

        protected PdfStringFormat Format =>
            this.Measurer.Format;

        protected PdfStringMeasurer Measurer { get; }
    }
}

