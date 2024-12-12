namespace DevExpress.XtraPrinting.Export
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;

    public class HtmlPageLayoutBuilder : PageLayoutBuilder
    {
        public HtmlPageLayoutBuilder(Page page, HtmlExportContext htmlExportContext) : base(page, htmlExportContext)
        {
        }

        internal override RectangleF GetCorrectClipRect(RectangleF clipRect) => 
            clipRect;

        internal override BrickViewData[] GetData(Brick brick, RectangleF bounds, RectangleF clipRect) => 
            base.exportContext.GetData(brick, bounds, clipRect);

        protected override ILayoutControl ValidateViewData(BrickViewData viewData) => 
            LayoutControl.Validate(viewData, false);
    }
}

