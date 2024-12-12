namespace DevExpress.XtraPrinting.BrickExporters
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Export;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;

    public class BrickContainerBaseExporter : BrickExporter
    {
        public override void Draw(IGraphics gr, RectangleF rect, RectangleF parentRect);
        protected internal override void FillDocxTableCellInternal(IDocxExportProvider exportProvider);
        protected internal override void FillHtmlTableCellInternal(IHtmlExportProvider exportProvider);
        protected internal override void FillRtfTableCellInternal(IRtfExportProvider exportProvider);
        protected internal override void FillTextTableCellInternal(ITableExportProvider exportProvider, bool shouldSplitText);
        protected internal override void FillXlTableCellInternal(IXlExportProvider exportProvider);
        private BrickExporter GetBaseBrickExporter(IPrintingSystemContext context);
        protected internal override BrickViewData[] GetExportData(ExportContext exportContext, RectangleF rect, RectangleF clipRect);
        protected internal override BrickViewData[] GetHtmlData(ExportContext htmlExportContext, RectangleF bounds, RectangleF clipBounds);
        protected internal override BrickViewData[] GetTextData(ExportContext exportContext, RectangleF rect);
        protected internal override BrickViewData[] GetXlData(ExportContext xlExportContext, RectangleF rect, RectangleF clipRect);
        internal override void ProcessLayout(PageLayoutBuilder layoutBuilder, PointF pos, RectangleF clipRect);

        private DevExpress.XtraPrinting.BrickContainerBase BrickContainerBase { get; }
    }
}

