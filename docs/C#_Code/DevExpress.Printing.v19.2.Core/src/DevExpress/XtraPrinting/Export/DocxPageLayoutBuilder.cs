namespace DevExpress.XtraPrinting.Export
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;

    public class DocxPageLayoutBuilder : PageLayoutBuilder
    {
        private PointF offset;

        public DocxPageLayoutBuilder(Page page, DocxExportContext docxExportContext) : base(page, docxExportContext)
        {
            float num = GraphicsUnitConverter.DocToDip(base.Page.MarginsF.Left);
            this.offset = new PointF(-num, 0f);
        }

        internal override RectangleF GetCorrectClipRect(RectangleF clipRect)
        {
            clipRect.Offset(this.offset);
            return clipRect;
        }

        internal override BrickViewData[] GetData(Brick brick, RectangleF bounds, RectangleF clipRect)
        {
            bounds.Offset(this.offset);
            return base.exportContext.GetData(brick, bounds, clipRect);
        }
    }
}

