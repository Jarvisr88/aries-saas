namespace DevExpress.XtraPrinting.BrickExporters
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Export;
    using System;
    using System.Drawing;

    public class LabelBrickExporter : TextBrickExporter
    {
        private int CalculateHypotenuse(float cathetus, float angle);
        private int CalculateWidth(float width, float height, float angle);
        protected override ITableCell CreateClippedDataTableCell(string text);
        public void DrawDeferred(IGraphics gr, BrickBase brick, RectangleF clientRectangle, StringFormat sf, Brush brush);
        private void DrawRotatedText(IGraphics gr, string text, Rectangle bounds, Brush brush, StringFormat format);
        protected override void DrawText(IGraphics gr, RectangleF clientRectangle, StringFormat sf, Brush brush);
        protected override void FillRtfTableCellCore(IRtfExportProvider exportProvider);

        private DevExpress.XtraPrinting.LabelBrick LabelBrick { get; }

        private bool HasAngle { get; }
    }
}

