namespace DevExpress.XtraPrinting.BrickExporters
{
    using DevExpress.Printing.Core.NativePdfExport;
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;

    internal class LinkDeferredAction : DeferredAction
    {
        private string destinationName;
        private Page page;
        private VisualBrick brick;
        private RectangleF bounds;

        public LinkDeferredAction(string destinationName, Page page, VisualBrick brick, RectangleF bounds);
        protected override void DoAction(PrintingSystemBase ps, IGraphics graphics);
    }
}

