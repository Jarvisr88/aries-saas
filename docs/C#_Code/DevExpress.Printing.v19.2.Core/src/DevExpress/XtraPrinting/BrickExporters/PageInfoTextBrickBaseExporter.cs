namespace DevExpress.XtraPrinting.BrickExporters
{
    using DevExpress.DocumentView;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Export;
    using System;
    using System.Drawing;

    public class PageInfoTextBrickBaseExporter : TextBrickExporter
    {
        public void DeferredDraw(IGraphics gr, PrintingSystemBase ps, BrickBase brick, RectangleF rect, RectangleF parentRect, IPageItem page);
        public override void Draw(IGraphics gr, RectangleF rect, RectangleF parentRect);
        protected internal override void FillDocxTableCellInternal(IDocxExportProvider exportProvider);
        protected override void FillRtfTableCellCore(IRtfExportProvider exportProvider);

        protected PageInfoTextBrickBase PageInfoBrick { get; }

        private DevExpress.XtraPrinting.PageInfo PageInfo { get; }

        private int StartPageNumber { get; }

        private string Format { get; }

        protected virtual bool CanExportAsNativePageInfo { get; }
    }
}

