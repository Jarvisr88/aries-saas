namespace DevExpress.Printing.ExportHelpers
{
    using System;

    internal class BandedSummaryExportHelper<TCol, TRow> : SummaryExportHelper<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        private BandedExportInfo<TCol, TRow> bandedExportInfo;

        public BandedSummaryExportHelper(ExportInfo<TCol, TRow> exportInfo) : base(exportInfo)
        {
            this.bandedExportInfo = exportInfo as BandedExportInfo<TCol, TRow>;
        }

        protected override int GetStartRangePosition()
        {
            int start = 0;
            if ((this.bandedExportInfo.GroupsList != null) && (this.bandedExportInfo.GroupsList.Count > 0))
            {
                start = this.bandedExportInfo.GroupsList[0].Start;
            }
            return start;
        }
    }
}

