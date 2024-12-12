namespace DevExpress.XtraPrinting.BrickExporters
{
    using DevExpress.XtraPrinting.Export;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;

    public class EmptyBrickExporter : BrickExporter
    {
        protected internal override BrickViewData[] GetExportData(ExportContext exportContext, RectangleF rect, RectangleF clipRect);
        internal override void ProcessLayout(PageLayoutBuilder layoutBuilder, PointF pos, RectangleF clipRect);
    }
}

