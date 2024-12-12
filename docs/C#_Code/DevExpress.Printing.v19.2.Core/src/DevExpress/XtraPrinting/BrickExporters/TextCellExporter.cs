namespace DevExpress.XtraPrinting.BrickExporters
{
    using DevExpress.XtraPrinting.Export;
    using System;

    public class TextCellExporter : CellWrapperExporter
    {
        protected internal override void FillDocxTableCellInternal(IDocxExportProvider exportProvider);
        protected internal override void FillHtmlTableCellInternal(IHtmlExportProvider exportProvider);
        protected internal override void FillRtfTableCellInternal(IRtfExportProvider exportProvider);
        protected internal override void FillTextTableCellInternal(ITableExportProvider exportProvider, bool shouldSplitText);
        protected internal override void FillXlTableCellInternal(IXlExportProvider exportProvider);
    }
}

