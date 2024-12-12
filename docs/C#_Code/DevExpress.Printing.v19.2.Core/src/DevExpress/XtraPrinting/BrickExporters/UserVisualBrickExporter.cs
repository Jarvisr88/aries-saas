namespace DevExpress.XtraPrinting.BrickExporters
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Export;
    using System;
    using System.Drawing;

    public class UserVisualBrickExporter : BrickExporter
    {
        public override void Draw(Graphics gr, RectangleF rect);
        protected internal override void DrawPdf(IPdfGraphics gr, RectangleF rect);
        protected internal override void FillDocxTableCellInternal(IDocxExportProvider exportProvider);
        protected internal override void FillHtmlTableCellInternal(IHtmlExportProvider exportProvider);
        protected internal override void FillRtfTableCellInternal(IRtfExportProvider exportProvider);
        private void FillTableCell(ITableExportProvider exportProvider);
        protected internal override void FillXlTableCellInternal(IXlExportProvider exportProvider);
        private Image GetBrickImage(PrintingSystemBase ps, RectangleF rect);
        private Image GetBrickImage(DevExpress.XtraPrinting.UserVisualBrick userVisualBrick, PrintingSystemBase ps, RectangleF rect);

        private DevExpress.XtraPrinting.UserVisualBrick UserVisualBrick { get; }

        private IBrick UserBrick { get; }

        private RectangleF Rect { get; }
    }
}

