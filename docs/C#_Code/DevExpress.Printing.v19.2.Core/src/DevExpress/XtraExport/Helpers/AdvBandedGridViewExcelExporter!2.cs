namespace DevExpress.XtraExport.Helpers
{
    using DevExpress.Export;
    using DevExpress.Printing.ExportHelpers;
    using System;

    public class AdvBandedGridViewExcelExporter<TCol, TRow> : BandedGridViewExcelExporter<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        public AdvBandedGridViewExcelExporter(IBandedGridView<TCol, TRow> viewToExport) : base(viewToExport, DataAwareExportOptionsFactory.Create(ExportTarget.Xlsx))
        {
        }

        public AdvBandedGridViewExcelExporter(IBandedGridView<TCol, TRow> viewToExport, IDataAwareExportOptions options) : base(viewToExport, options)
        {
        }

        protected override DataAwareExportContext<TCol, TRow> CreateContext(ExportInfo<TCol, TRow> exportInfo) => 
            new DataAwareAdvBandedExportContext<TCol, TRow>(exportInfo);

        internal override ExportInfo<TCol, TRow> CreateExportInfo() => 
            new AdvBandedExportInfo<TCol, TRow>(this);
    }
}

