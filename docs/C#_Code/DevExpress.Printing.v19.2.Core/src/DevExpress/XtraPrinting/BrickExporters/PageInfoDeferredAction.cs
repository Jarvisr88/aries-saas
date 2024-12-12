namespace DevExpress.XtraPrinting.BrickExporters
{
    using DevExpress.Printing.Core.NativePdfExport;
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;

    internal class PageInfoDeferredAction : DeferredActionWithForm
    {
        private readonly long pageID;
        private readonly BrickBase brick;
        private readonly PageInfoTextBrickBaseExporter brickExporter;
        private readonly RectangleF parentRect;

        public PageInfoDeferredAction(Page page, RectangleF rect, RectangleF parentRect, BrickBase brick, PageInfoTextBrickBaseExporter brickExporter);
        protected override void DoAction(PrintingSystemBase ps, IGraphics gr);
    }
}

