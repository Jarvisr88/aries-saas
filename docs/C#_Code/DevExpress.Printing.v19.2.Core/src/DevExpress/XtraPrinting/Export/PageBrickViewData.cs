namespace DevExpress.XtraPrinting.Export
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;

    public class PageBrickViewData : BrickViewData
    {
        private RectangleF originalBounds;

        public PageBrickViewData(BrickStyle style, RectangleF bounds, ITableCell tableCell) : base(style, bounds, tableCell)
        {
            this.originalBounds = RectangleF.Empty;
        }

        public override void ApplyClipping(RectangleF clipBoundsF)
        {
            this.originalBounds = base.BoundsF;
            base.BoundsF = RectangleF.Intersect(base.BoundsF, clipBoundsF);
        }

        public override RectangleF OriginalBoundsF =>
            (this.originalBounds == RectangleF.Empty) ? base.BoundsF : this.originalBounds;
    }
}

