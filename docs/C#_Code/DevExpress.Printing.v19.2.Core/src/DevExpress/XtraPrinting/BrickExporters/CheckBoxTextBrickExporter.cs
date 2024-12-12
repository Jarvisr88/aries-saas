namespace DevExpress.XtraPrinting.BrickExporters
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Export;
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraPrinting.NativeBricks;
    using System;
    using System.Drawing;

    public class CheckBoxTextBrickExporter : ContainerBrickBaseExporter
    {
        protected internal override void FillTextTableCellInternal(ITableExportProvider exportProvider, bool shouldSplitText);
        private RectangleF GetDocxCheckBoxBounds();
        protected internal override BrickViewData[] GetTextData(ExportContext exportContext, RectangleF rect);
        protected override RectangleF GetViewDataBounds(ExportContext exportContext, Brick brick);

        private DevExpress.XtraPrinting.NativeBricks.CheckBoxTextBrick CheckBoxTextBrick { get; }

        private bool IsNearCheckBoxAlignment { get; }
    }
}

