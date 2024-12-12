namespace DevExpress.XtraPrinting.Export
{
    using DevExpress.XtraPrinting.BrickExporters;
    using System;

    public class NullTableCellExporter : BrickExporter
    {
        protected internal override void FillHtmlTableCellInternal(IHtmlExportProvider exportProvider)
        {
        }

        protected internal override void FillRtfTableCellInternal(IRtfExportProvider exportProvider)
        {
        }

        protected internal override void FillTextTableCellInternal(ITableExportProvider exportProvider, bool shouldSplitText)
        {
        }

        protected internal override void FillXlTableCellInternal(IXlExportProvider exportProvider)
        {
        }
    }
}

