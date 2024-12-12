namespace DevExpress.XtraPrinting.BrickExporters
{
    using DevExpress.Printing.Core.NativePdfExport;
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;

    internal class LabelBrickDeferredAction : DeferredActionWithForm
    {
        private readonly LabelBrickExporter exporter;
        private readonly StringFormat sf;
        private readonly Brush brush;
        private readonly BrickBase brick;

        public LabelBrickDeferredAction(RectangleF rect, BrickBase brick, StringFormat sf, Brush brush, LabelBrickExporter exporter);
        protected override void DoAction(PrintingSystemBase ps, IGraphics gr);
    }
}

