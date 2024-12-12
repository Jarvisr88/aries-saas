namespace DevExpress.XtraPrinting.Export
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BrickExporters;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;

    public class XlLayoutBuilder : LayoutBuilder
    {
        private XlExportContext xlExportContext;

        public XlLayoutBuilder(Document document, XlExportContext xlExportContext) : base(document)
        {
            this.xlExportContext = xlExportContext;
        }

        protected override ILayoutControl[] GetBrickLayoutControls(Brick brick, RectangleF rect)
        {
            BrickExporter exporter = this.xlExportContext.PrintingSystem.ExportersFactory.GetExporter(brick) as BrickExporter;
            return base.ToLayoutControls(exporter.GetXlData(this.xlExportContext, rect, rect), brick);
        }
    }
}

