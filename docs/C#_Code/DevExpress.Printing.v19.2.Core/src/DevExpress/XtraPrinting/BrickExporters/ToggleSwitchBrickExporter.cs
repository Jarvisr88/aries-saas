namespace DevExpress.XtraPrinting.BrickExporters
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Export;
    using System;
    using System.Drawing;

    public class ToggleSwitchBrickExporter : VisualBrickExporter
    {
        protected override RectangleF DeflateBorderWidth(RectangleF rect);
        protected override void DrawObject(IGraphics gr, RectangleF rect);
        protected internal override void FillDocxTableCellInternal(IDocxExportProvider exportProvider);
        protected override void FillHtmlTableCellCore(IHtmlExportProvider exportProvider);
        protected override void FillRtfTableCellCore(IRtfExportProvider exportProvider);
        private void FillTableCell(ITableExportProvider exportProvider);
        protected internal override void FillTextTableCellInternal(ITableExportProvider exportProvider, bool shouldSplitText);
        protected internal override void FillXlTableCellInternal(IXlExportProvider exportProvider);
        internal SizeF GetScaledCheckSize(IPrintingSystemContext context);
        protected override object[] GetSpecificKeyPart();

        private DevExpress.XtraPrinting.ToggleSwitchBrick ToggleSwitchBrick { get; }

        private bool IsOn { get; }
    }
}

