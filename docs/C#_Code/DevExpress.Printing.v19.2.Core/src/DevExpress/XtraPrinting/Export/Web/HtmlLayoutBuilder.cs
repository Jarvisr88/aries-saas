namespace DevExpress.XtraPrinting.Export.Web
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Export;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;

    public class HtmlLayoutBuilder : LayoutBuilder
    {
        private HtmlExportContext htmlExportContext;

        public HtmlLayoutBuilder(Document document, HtmlExportContext htmlExportContext) : base(document)
        {
            this.htmlExportContext = htmlExportContext;
        }

        protected override ILayoutControl[] GetBrickLayoutControls(Brick brick, RectangleF rect) => 
            base.ToLayoutControls(this.htmlExportContext.GetData(brick, rect, rect), brick);
    }
}

