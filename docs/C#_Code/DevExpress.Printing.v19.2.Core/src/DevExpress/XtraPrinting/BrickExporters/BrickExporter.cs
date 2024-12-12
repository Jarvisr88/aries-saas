namespace DevExpress.XtraPrinting.BrickExporters
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Export;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;

    public class BrickExporter : BrickBaseExporter
    {
        public virtual void Draw(Graphics gr, RectangleF rect);
        public override void Draw(IGraphics gr, RectangleF rect, RectangleF parentRect);
        protected internal virtual void DrawPdf(IPdfGraphics gr, RectangleF rect);
        protected internal override void DrawWarningRect(IGraphics gr, RectangleF r, string message);
        public void FillDocxTableCell(IDocxExportProvider exportProvider);
        protected internal virtual void FillDocxTableCellInternal(IDocxExportProvider exportProvider);
        public void FillHtmlTableCell(IHtmlExportProvider exportProvider);
        protected internal virtual void FillHtmlTableCellInternal(IHtmlExportProvider exportProvider);
        public void FillRtfTableCell(IRtfExportProvider exportProvider);
        protected internal virtual void FillRtfTableCellInternal(IRtfExportProvider exportProvider);
        public void FillTextTableCell(ITableExportProvider exportProvider, bool shouldSplitText);
        protected internal virtual void FillTextTableCellInternal(ITableExportProvider exportProvider, bool shouldSplitText);
        public void FillXlTableCell(IXlExportProvider exportProvider);
        protected internal virtual void FillXlTableCellInternal(IXlExportProvider exportProvider);
        protected internal virtual BrickViewData[] GetDocxData(ExportContext docxExportContext, RectangleF rect, RectangleF clipRect);
        protected internal virtual BrickViewData[] GetExportData(ExportContext exportContext, RectangleF rect, RectangleF clipRect);
        protected internal virtual BrickViewData[] GetHtmlData(ExportContext htmlExportContext, RectangleF bounds, RectangleF clipBounds);
        protected internal virtual BrickViewData[] GetRtfData(ExportContext rtfExportContext, RectangleF rect, RectangleF clipRect);
        protected internal virtual BrickViewData[] GetTextData(ExportContext exportContext, RectangleF rect);
        protected internal virtual BrickViewData[] GetXlData(ExportContext xlsExportContext, RectangleF rect, RectangleF clipRect);
        internal override void ProcessLayout(PageLayoutBuilder layoutBuilder, PointF pos, RectangleF clipRect);
        protected void ProcessLayoutCore(PageLayoutBuilder layoutBuilder, RectangleF rect, RectangleF clipRect);

        protected ITableCell TableCell { get; }

        protected string Url { get; }

        protected bool HasCrossReference { get; }
    }
}

