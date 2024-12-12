namespace DevExpress.XtraPrinting.BrickExporters
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Export;
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class CheckBoxBrickExporter : VisualBrickExporter
    {
        private static object ConvertToValue(System.Windows.Forms.CheckState checkState);
        protected override RectangleF DeflateBorderWidth(RectangleF rect);
        protected override void DrawClientContent(IGraphics gr, RectangleF clientRect);
        protected internal override void FillDocxTableCellInternal(IDocxExportProvider exportProvider);
        protected override void FillHtmlTableCellCore(IHtmlExportProvider exportProvider);
        protected override void FillRtfTableCellCore(IRtfExportProvider exportProvider);
        private void FillTableCell(ITableExportProvider exportProvider);
        protected internal override void FillTextTableCellInternal(ITableExportProvider exportProvider, bool shouldSplitText);
        protected internal override void FillXlTableCellInternal(IXlExportProvider exportProvider);
        protected override RectangleF GetClientRect(RectangleF rect, IGraphics gr);
        protected override RectangleF GetClipRect(RectangleF rect, IGraphics gr);
        protected override object[] GetSpecificKeyPart();
        private bool IsStandardGlyph();

        private DevExpress.XtraPrinting.CheckBoxBrick CheckBoxBrick { get; }

        private System.Windows.Forms.CheckState CheckState { get; }
    }
}

