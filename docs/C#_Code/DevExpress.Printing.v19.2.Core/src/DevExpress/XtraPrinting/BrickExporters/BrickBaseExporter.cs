namespace DevExpress.XtraPrinting.BrickExporters
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Export;
    using System;
    using System.Drawing;

    public class BrickBaseExporter
    {
        private object brick;

        public virtual void Draw(IGraphics gr, RectangleF rect, RectangleF parentRect);
        protected internal virtual void DrawWarningRect(IGraphics gr, RectangleF r, string message);
        internal static BrickBaseExporter GetExporter(IPrintingSystemContext context, object brick);
        internal virtual void ProcessLayout(PageLayoutBuilder layoutBuilder, PointF pos, RectangleF clipRect);
        protected internal void SetBrick(object brick);

        protected object Brick { get; }

        protected DevExpress.XtraPrinting.BrickBase BrickBase { get; }
    }
}

