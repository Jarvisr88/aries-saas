namespace DevExpress.Printing.ExportHelpers
{
    using DevExpress.Export;
    using DevExpress.Export.Xl;
    using DevExpress.Printing.DataAwareExport.Export.Utils;
    using DevExpress.XtraExport.Helpers;
    using System;
    using System.Runtime.InteropServices;

    internal class DataAwareAdvBandedExportContext<TCol, TRow> : DataAwareBandedExportContext<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        private readonly AdvBandedExportInfo<TCol, TRow> advBandedExportInfo;
        private int columnWatchdog;

        public DataAwareAdvBandedExportContext(ExportInfo<TCol, TRow> exportInfo) : base(exportInfo)
        {
            this.advBandedExportInfo = exportInfo as AdvBandedExportInfo<TCol, TRow>;
        }

        protected override void AddAutoFilter(int left, int top, int right, int bottom)
        {
            if ((this.advBandedExportInfo.LinearBandsAndColumns || (this.advBandedExportInfo.BandedRowPatternCount == 1)) && (base.exportInfo.Exporter.CurrentColumnIndex != 0))
            {
                base.exportInfo.Sheet.AutoFilterRange = XlCellRange.FromLTRB(left, top, right, bottom);
            }
        }

        protected override int CalcFixedRowsCount() => 
            this.advBandedExportInfo.View.FixedRowsCount * this.advBandedExportInfo.BandedRowPatternCount;

        public override void CreateColumn(TCol gridColumn)
        {
            if (this.columnWatchdog < this.advBandedExportInfo.BandedColumnsCount)
            {
                IXlColumn column = this.advBandedExportInfo.Exporter.BeginColumn();
                if (this.advBandedExportInfo.AllowFixedColumns)
                {
                    SetColumnAsFixed(this.advBandedExportInfo.Sheet, gridColumn);
                }
                column.Formatting = FormattingUtils.GetDefault();
                SetColumnWidth(gridColumn, column);
                SetColumnVisibilityState(gridColumn, column);
                this.advBandedExportInfo.Exporter.EndColumn();
                this.columnWatchdog++;
            }
        }

        protected override void CreateRowCore(SheetAreaType areaType, TRow row, Action<IXlRow> rsAction, int colLimit = -1)
        {
            if (colLimit == -1)
            {
                colLimit = base.exportInfo.Exporter.DocumentOptions.MaxColumnCount;
            }
            for (int i = 0; i < this.advBandedExportInfo.ExportProviders.Count; i++)
            {
                this.advBandedExportInfo.ExportProviders[i].Used = false;
            }
            if (this.advBandedExportInfo.LinearBandsAndColumns)
            {
                base.CreateRowCore(areaType, row, rsAction, -1);
            }
            else
            {
                for (int j = 0; j < this.advBandedExportInfo.BandedRowPatternCount; j++)
                {
                    this.advBandedExportInfo.Exporter.BeginRow();
                    this.ConstructRow(areaType, row, j);
                    this.advBandedExportInfo.Exporter.EndRow();
                    int exportRowIndex = this.advBandedExportInfo.ExportRowIndex;
                    this.advBandedExportInfo.ExportRowIndex = exportRowIndex + 1;
                }
            }
        }

        protected override BandedRowInfo GetBandedRowInfo(int rowIndex, SheetAreaType areaType) => 
            (areaType != SheetAreaType.Header) ? ((rowIndex < this.advBandedExportInfo.BandedRowPattern.Count) ? this.advBandedExportInfo.BandedRowPattern[rowIndex] : new BandedRowInfo()) : base.GetBandedRowInfo(rowIndex, areaType);

        protected override IColumnExportProvider<TRow> GetColumnExportProvider(BandNodeDescriptor desc, SheetAreaType areaType) => 
            (areaType != SheetAreaType.Header) ? base.FindColumnExportProvider(desc, this.advBandedExportInfo.ExportProviders) : base.GetColumnExportProvider(desc, areaType);

        protected override void MergeCells(BandNodeDescriptor desc, int cellIndex, SheetAreaType areaType)
        {
            if (areaType == SheetAreaType.Header)
            {
                base.MergeCells(desc, cellIndex, areaType);
            }
            else
            {
                MergingUtils.MergeCells(this.advBandedExportInfo.Sheet, desc, cellIndex, this.advBandedExportInfo.ExportRowIndex, false, false);
            }
        }

        public override void PrintGroupRowHeader(TRow groupRow)
        {
            if (this.advBandedExportInfo.GroupsStack.Count < (this.advBandedExportInfo.MaxGroupingLevel - 1))
            {
                IGroupRow<TRow> groupRowCore = groupRow as IGroupRow<TRow>;
                base.CreateRowCore(SheetAreaType.GroupHeader, groupRow, base.SetGroupRowState(groupRowCore), (short) this.advBandedExportInfo.BandedColumnsCount);
                if (this.advBandedExportInfo.Options.CanRaiseAfterAddRow)
                {
                    this.advBandedExportInfo.RaiseAfterAddRowEvent(groupRow, this);
                }
            }
        }
    }
}

