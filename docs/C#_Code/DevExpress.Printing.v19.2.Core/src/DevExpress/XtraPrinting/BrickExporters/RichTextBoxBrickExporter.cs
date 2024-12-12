namespace DevExpress.XtraPrinting.BrickExporters
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Export;
    using DevExpress.XtraPrinting.NativeBricks;
    using System;
    using System.Drawing;

    public class RichTextBoxBrickExporter : VisualBrickExporter
    {
        private static string CreateTextLayoutString(string text);
        protected override void DrawClientContent(IGraphics gr, RectangleF clientRect);
        protected override void FillHtmlTableCellCore(IHtmlExportProvider exportProvider);
        protected override void FillRtfTableCellCore(IRtfExportProvider exportProvider);
        protected internal override void FillTextTableCellInternal(ITableExportProvider exportProvider, bool shouldSplitText);
        protected internal override void FillXlTableCellInternal(IXlExportProvider exportProvider);
        protected override RectangleF GetClientRect(RectangleF rect, IGraphics gr);

        private DevExpress.XtraPrinting.NativeBricks.RichTextBoxBrick RichTextBoxBrick { get; }

        private IRichTextBoxBrickOwner RichTextBoxBrickContainer { get; }

        private string RtfText { get; }
    }
}

