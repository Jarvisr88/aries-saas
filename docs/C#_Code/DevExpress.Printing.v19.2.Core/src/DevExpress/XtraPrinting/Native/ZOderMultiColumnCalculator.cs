namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Drawing;

    public class ZOderMultiColumnCalculator : ZOderMultiColumnBuilder
    {
        private readonly PageRowCalculator pageRowCalculator;
        private float usefulPageWidth;

        public ZOderMultiColumnCalculator(PageRowCalculator pageRowCalculator, float usefulPageWidth);
        protected internal override PageRowBuilderBase CreateInternalPageRowBuilder();
        protected override void FillRowHeader(DocumentBand rootBand, DocumentBand docBand, RectangleF bounds, MultiColumn mc, int rowIndex);

        protected override float UsefulPageWidth { get; }
    }
}

