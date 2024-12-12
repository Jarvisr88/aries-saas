namespace DevExpress.Printing.ExportHelpers
{
    using DevExpress.Printing.DataAwareExport.Export.Utils;
    using DevExpress.XtraExport.Helpers;
    using System;

    internal class TreeListBandedExportInfo<TCol, TRow> : BandedExportInfo<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        public TreeListBandedExportInfo(GridViewExcelExporter<TCol, TRow> helper) : base(helper)
        {
        }

        internal override ExportHelpersProvider<TCol, TRow> CreateHelpersProvider() => 
            new TreeListBandedHelpersProvider<TCol, TRow>(this);
    }
}

