namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class NOderMultiColumnBuilder : PageHeaderFooterRowBuilder
    {
        private int columnIndex;

        public NOderMultiColumnBuilder(YPageContentEngine pageContentEngine);
        private void AttachColumnIndexValue(List<BandBricksPair> docBands, int columnIndex);
        public FillPageResult BuildNOrderMultiColumn(DocumentBand rootBand, MultiColumn mc, RectangleF bounds);
        protected override bool CanFillPageWithBricks(DocumentBand docBand);
        protected override void CorrectPrintAtBottomBricks(List<BandBricksPair> docBands, float pageBottom);
        protected internal override PageRowBuilderBase CreateInternalPageRowBuilder();
        protected override bool ShouldOverFulfill(DocumentBand docBand, RectangleF bounds);
        private void UpdateMultiColumnBricks(IEnumerable<Brick> bricks);
        protected override PageUpdateData UpdatePageContent(DocumentBand docBand, RectangleF bounds);

        private class NOderOffsetHelperXY : PageRowBuilderBase.OffsetHelperXY
        {
            public NOderOffsetHelperXY(float offsetX);

            protected override bool ShouldUpdateNegativeOffsetY { get; }
        }
    }
}

