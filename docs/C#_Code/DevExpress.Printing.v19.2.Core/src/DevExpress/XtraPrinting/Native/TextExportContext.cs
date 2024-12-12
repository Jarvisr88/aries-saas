namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Export;
    using System;
    using System.Drawing;

    public class TextExportContext : ExportContext
    {
        public TextExportContext(PrintingSystemBase ps);
        public override BrickViewData CreateBrickViewData(BrickStyle style, RectangleF bounds, ITableCell tableCell);
        public override BrickViewData[] GetData(Brick brick, RectangleF rect, RectangleF clipRect);

        public override bool AllowEmptyAreas { get; }

        public override bool RawDataMode { get; }
    }
}

