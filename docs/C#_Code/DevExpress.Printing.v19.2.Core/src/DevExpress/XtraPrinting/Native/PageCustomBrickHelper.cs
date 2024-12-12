namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;

    public class PageCustomBrickHelper
    {
        private ReadonlyPageData pageData;

        public PageCustomBrickHelper(IPrintingSystemContext context)
        {
            this.pageData = GetPageData(context.DrawingPage, context.PrintingSystem);
        }

        public RectangleF AlignRect(RectangleF rect, BrickModifier modifier, BrickAlignment alignment, BrickAlignment lineAlignment)
        {
            if (this.pageData != null)
            {
                if (modifier == BrickModifier.MarginalHeader)
                {
                    return GetAlignedRect(rect, this.pageData.PageHeaderRect, alignment, lineAlignment);
                }
                if (modifier == BrickModifier.MarginalFooter)
                {
                    return GetAlignedRect(rect, this.pageData.PageFooterRect, alignment, lineAlignment);
                }
            }
            return rect;
        }

        private static BrickAlignment ForceAlignment(BrickAlignment alignment, BrickAlignment forceAlignment) => 
            (alignment != BrickAlignment.None) ? alignment : forceAlignment;

        private static RectangleF GetAlignedRect(RectangleF rect, RectangleF baseRect, BrickAlignment alignment, BrickAlignment lineAlignment)
        {
            baseRect.Location = PointF.Empty;
            return RectF.Align(rect, baseRect, ForceAlignment(alignment, BrickAlignment.Near), lineAlignment);
        }

        private static ReadonlyPageData GetPageData(Page page, PrintingSystemBase printingSystem) => 
            (page != null) ? page.PageData : printingSystem?.PageSettings.Data;
    }
}

