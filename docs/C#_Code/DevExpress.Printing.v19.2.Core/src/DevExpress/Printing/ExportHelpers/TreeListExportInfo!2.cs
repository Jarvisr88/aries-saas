namespace DevExpress.Printing.ExportHelpers
{
    using DevExpress.Printing.DataAwareExport.Export.Utils;
    using DevExpress.XtraExport.Helpers;
    using System;

    internal class TreeListExportInfo<TCol, TRow> : ExportInfo<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        public TreeListExportInfo(GridViewExcelExporter<TCol, TRow> helper) : base(helper)
        {
        }

        internal override ExportHelpersProvider<TCol, TRow> CreateHelpersProvider() => 
            new TreeListHelpersProvider<TCol, TRow>(this);
    }
}

