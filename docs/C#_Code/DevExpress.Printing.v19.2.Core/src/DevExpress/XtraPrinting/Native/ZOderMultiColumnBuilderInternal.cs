namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class ZOderMultiColumnBuilderInternal : PageHeaderFooterRowBuilder
    {
        private int columnIndex;

        public ZOderMultiColumnBuilderInternal(YPageContentEngine pageContentEngine, int columnIndex);
        protected override bool ShouldOverFulfill(DocumentBand docBand, RectangleF bounds);
        private void UpdateMultiColumnBricks(IEnumerable<Brick> bricks);
        protected override PageUpdateData UpdatePageContent(DocumentBand docBand, RectangleF bounds);
    }
}

