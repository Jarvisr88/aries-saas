namespace DevExpress.Printing.ExportHelpers
{
    using DevExpress.Printing.DataAwareExport.Export.Utils;
    using DevExpress.XtraExport.Helpers;
    using System;

    internal class TreeListAdvBandedExportInfo<TCol, TRow> : AdvBandedExportInfo<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        public TreeListAdvBandedExportInfo(GridViewExcelExporter<TCol, TRow> helper) : base(helper)
        {
        }

        internal override ExportHelpersProvider<TCol, TRow> CreateHelpersProvider() => 
            new TreeListAdvBandedHelpersProvider<TCol, TRow>(this);
    }
}

