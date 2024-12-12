namespace DevExpress.XtraPrinting.BrickExporters
{
    using DevExpress.XtraPrinting.Export;
    using System;

    public class AnchorCellExporter : CellWrapperExporter
    {
        protected internal override void FillHtmlTableCellInternal(IHtmlExportProvider exportProvider);
        protected internal override void FillRtfTableCellInternal(IRtfExportProvider exportProvider);
    }
}

