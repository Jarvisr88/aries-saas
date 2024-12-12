namespace DevExpress.XtraPrinting.Export
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BrickExporters;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;

    public class DocxLayoutBuilder : LayoutBuilder
    {
        private DocxExportContext docxExportContext;

        public DocxLayoutBuilder(Document document, DocxExportContext docxExportContext) : base(document)
        {
            this.docxExportContext = docxExportContext;
        }

        protected override ILayoutControl[] GetBrickLayoutControls(Brick brick, RectangleF rect)
        {
            BrickExporter exporter = BrickBaseExporter.GetExporter(this.docxExportContext, brick) as BrickExporter;
            return base.ToLayoutControls(exporter.GetDocxData(this.docxExportContext, rect, rect), brick);
        }
    }
}

