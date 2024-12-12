namespace DevExpress.Xpf.Printing
{
    using DevExpress.Export;
    using DevExpress.XtraExport.Helpers;
    using System;

    public class ClipboardExportManager<TCol, TRow> : ClipboardExportManagerBase<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        public ClipboardExportManager(IClipboardGridView<TCol, TRow> gridView) : base(gridView)
        {
        }

        public ClipboardExportManager(IClipboardGridView<TCol, TRow> gridView, ClipboardOptions exportOptions) : base(gridView, exportOptions)
        {
        }

        protected override IClipboardExporter<TCol, TRow> CreateRTFExporter(bool exportRtf, bool exportHtml) => 
            new ClipboardRTFExporter<TCol, TRow>(exportRtf, exportHtml);
    }
}

