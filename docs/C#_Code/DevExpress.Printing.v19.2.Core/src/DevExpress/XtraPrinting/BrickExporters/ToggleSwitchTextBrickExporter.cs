namespace DevExpress.XtraPrinting.BrickExporters
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Export;
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraPrinting.NativeBricks;
    using System;
    using System.Drawing;

    public class ToggleSwitchTextBrickExporter : ContainerBrickBaseExporter
    {
        protected internal override void FillTextTableCellInternal(ITableExportProvider exportProvider, bool shouldSplitText);
        protected internal override BrickViewData[] GetTextData(ExportContext exportContext, RectangleF rect);
        protected override RectangleF GetViewDataBounds(ExportContext exportContext, Brick brick);

        private DevExpress.XtraPrinting.NativeBricks.ToggleSwitchTextBrick ToggleSwitchTextBrick { get; }
    }
}

