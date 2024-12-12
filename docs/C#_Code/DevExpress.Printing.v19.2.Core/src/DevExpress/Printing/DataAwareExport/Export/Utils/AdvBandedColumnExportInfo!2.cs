namespace DevExpress.Printing.DataAwareExport.Export.Utils
{
    using DevExpress.Printing.ExportHelpers;
    using System;
    using System.Runtime.CompilerServices;

    internal class AdvBandedColumnExportInfo<TCol, TRow> : ColumnExportInfo<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        private AdvBandedExportInfo<TCol, TRow> advBandedExportInfo;

        public AdvBandedColumnExportInfo(ExportInfo<TCol, TRow> exportInfo) : base(exportInfo)
        {
            this.advBandedExportInfo = exportInfo as AdvBandedExportInfo<TCol, TRow>;
            this.BandedColumnsPanelRowsCount = this.advBandedExportInfo.BandedRowPatternCount;
            if (this.advBandedExportInfo.ShowColumnHeaders)
            {
                this.HeaderPanelRowsCount = this.advBandedExportInfo.HeaderPanelRowsCount;
            }
        }

        public override void RaiseDocumentColumnFilteringEventArgs(TCol col)
        {
            if (this.BandedColumnsPanelRowsCount == 1)
            {
                base.RaiseDocumentColumnFilteringEventArgs(col);
            }
        }

        public int BandedColumnsPanelRowsCount { get; private set; }

        public int HeaderPanelRowsCount { get; private set; }
    }
}

