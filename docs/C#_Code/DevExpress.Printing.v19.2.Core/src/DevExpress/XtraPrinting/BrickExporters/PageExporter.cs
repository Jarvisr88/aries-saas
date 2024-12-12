namespace DevExpress.XtraPrinting.BrickExporters
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Export;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class PageExporter : BrickBaseExporter
    {
        public PageExporter();
        public override void Draw(IGraphics gr, RectangleF rect, RectangleF parentRect);
        private void DrawBackground(IGraphics gr, RectangleF rect, Color backColor);
        private void DrawCore(IGraphics gr, RectangleF rect, RectangleF parentRect);
        private static void DrawInformation(string infoString, IGraphics gr, RectangleF rect, PrintingSystemBase ps);
        private void DrawWatermark(IGraphics gr, RectangleF rect);
        private void ExecWithDefaultSmoothingMode(IGraphics gr, Action action);
        internal override void ProcessLayout(PageLayoutBuilder layoutBuilder, PointF pos, RectangleF clipRect);
        private Color ValidateBackgrColor(Color color);

        public bool IsPrinting { get; set; }

        public DevExpress.XtraPrinting.Page Page { get; }

        public bool RetainBackgroundTransparency { get; set; }
    }
}

