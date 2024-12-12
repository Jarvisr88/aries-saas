namespace DevExpress.XtraPrinting.Export
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BrickExporters;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;

    public class RtfLayoutBuilder : LayoutBuilder
    {
        private RtfExportContext rtfExportContext;

        public RtfLayoutBuilder(Document document, RtfExportContext rtfExportContext) : base(document)
        {
            this.rtfExportContext = rtfExportContext;
        }

        protected override ILayoutControl[] GetBrickLayoutControls(Brick brick, RectangleF rect)
        {
            BrickExporter exporter = BrickBaseExporter.GetExporter(this.rtfExportContext, brick) as BrickExporter;
            return base.ToLayoutControls(exporter.GetRtfData(this.rtfExportContext, rect, rect), brick);
        }
    }
}

