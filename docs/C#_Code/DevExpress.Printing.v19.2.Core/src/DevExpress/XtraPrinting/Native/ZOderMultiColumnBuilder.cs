namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Drawing;

    public class ZOderMultiColumnBuilder : PageHeaderFooterRowBuilder
    {
        private int columnIndex;

        public ZOderMultiColumnBuilder(YPageContentEngine pageContentEngine);
        public FillPageResult BuildZOrderMultiColumn(DocumentBand rootBand, MultiColumn mc, RectangleF bounds);
        protected override bool CanFillPageWithBricks(DocumentBand docBand);
        protected override bool CanProcessDetail(DocumentBand rootBand, PageBuildInfo pageBuildInfo);
        protected internal override PageRowBuilderBase CreateInternalPageRowBuilder();
        protected virtual void FillRowHeader(DocumentBand rootBand, DocumentBand docBand, RectangleF bounds, MultiColumn mc, int rowIndex);
        private static DocumentBand FindVerticalHeader(DocumentBand rootBand);
        private FillPageResult PrintRow(DocumentBand rootBand, RectangleF bounds, MultiColumn mc, OffsetHelperY offsetHelper);
        private void PrintRowHeader(DocumentBand rootBand, RectangleF bounds, MultiColumn mc, int bi);
        private bool ShouldOverFulfillRow(DocumentBand rootBand, RectangleF bounds, int bi, int columnCount, double negativeOffsetY);
        private void SkipEmptyBands(DocumentBand rootBand);
    }
}

