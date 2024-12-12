namespace DevExpress.Printing.ExportHelpers
{
    using System;

    internal class TreeListBandedSummaryExportHelper<TCol, TRow> : TreeListSummaryExportHelper<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        private BandedExportInfo<TCol, TRow> bandedExportInfo;

        public TreeListBandedSummaryExportHelper(ExportInfo<TCol, TRow> exportInfo) : base(exportInfo)
        {
            this.bandedExportInfo = exportInfo as BandedExportInfo<TCol, TRow>;
        }

        protected override int GetStartRangePosition() => 
            this.bandedExportInfo.BandedHeaderRowPattern.Count;
    }
}

