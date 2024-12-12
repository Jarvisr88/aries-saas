namespace DevExpress.Printing.ExportHelpers
{
    using DevExpress.Export;
    using DevExpress.Export.Xl;
    using DevExpress.Printing.DataAwareExport.Export.Utils;
    using DevExpress.XtraExport.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    internal class DataAwareBandedExportContext<TCol, TRow> : DataAwareExportContext<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        protected BandedExportInfo<TCol, TRow> bandedExportInfo;
        private int currentRowIndex;
        private bool updateIndex;

        public DataAwareBandedExportContext(ExportInfo<TCol, TRow> exportInfo) : base(exportInfo)
        {
            this.currentRowIndex = -1;
            this.updateIndex = true;
            this.bandedExportInfo = exportInfo as BandedExportInfo<TCol, TRow>;
        }

        public override void AddRow()
        {
            TRow gridRow = default(TRow);
            base.CreateRowCore(SheetAreaType.DataArea, gridRow, null, -1);
        }

        private void CalcChildrenIndex(IEnumerable<IColumn> children, string header, ref int index)
        {
            foreach (IColumn column in children)
            {
                if (column is IGridBand)
                {
                    Func<BandNodeDescriptor, bool> predicate = <>c<TCol, TRow>.<>9__9_0;
                    if (<>c<TCol, TRow>.<>9__9_0 == null)
                    {
                        Func<BandNodeDescriptor, bool> local1 = <>c<TCol, TRow>.<>9__9_0;
                        predicate = <>c<TCol, TRow>.<>9__9_0 = x => x.Column is IGridBand;
                    }
                    List<BandNodeDescriptor> values = this.bandedExportInfo.BandsLayoutInfo.Values.Where<BandNodeDescriptor>(predicate).ToList<BandNodeDescriptor>();
                    if (values.Count > 0)
                    {
                        BandNodeDescriptor descriptor = this.FindDesc(values, column);
                        index += descriptor.RowCount;
                        this.CalcChildrenIndex(column.GetAllColumns(), header, ref index);
                    }
                }
                if (column is IBandedGridColumn)
                {
                    Func<BandNodeDescriptor, bool> predicate = <>c<TCol, TRow>.<>9__9_1;
                    if (<>c<TCol, TRow>.<>9__9_1 == null)
                    {
                        Func<BandNodeDescriptor, bool> local2 = <>c<TCol, TRow>.<>9__9_1;
                        predicate = <>c<TCol, TRow>.<>9__9_1 = x => !(x.Column is IGridBand);
                    }
                    List<BandNodeDescriptor> values = this.bandedExportInfo.BandsLayoutInfo.Values.Where<BandNodeDescriptor>(predicate).ToList<BandNodeDescriptor>();
                    if (values.Count > 0)
                    {
                        BandNodeDescriptor descriptor2 = this.FindDesc(values, column);
                        if (!string.Equals(column.Header, header))
                        {
                            this.updateIndex = false;
                        }
                        else if ((descriptor2.RowIndex > this.currentRowIndex) && this.updateIndex)
                        {
                            index += descriptor2.RowCount;
                        }
                        this.currentRowIndex = descriptor2.RowIndex;
                    }
                }
            }
        }

        protected virtual int CalcFixedRowsCount() => 
            this.bandedExportInfo.View.FixedRowsCount;

        private bool ColumnsEqual(IBandedGridColumn descColumn, IBandedGridColumn bgc) => 
            (descColumn.Header == bgc.Header) && ((descColumn.FieldName == bgc.FieldName) && ((descColumn.ColIndex == bgc.ColIndex) && (descColumn.RowIndex == bgc.RowIndex)));

        protected virtual void ConstructRow(SheetAreaType areaType, TRow row, int rowIndex)
        {
            BandedRowInfo bandedRowInfo = this.GetBandedRowInfo(rowIndex, areaType);
            for (int i = 0; i < this.bandedExportInfo.BandedColumnsCount; i++)
            {
                BandNodeDescriptor descriptorByCellPosition = bandedRowInfo.GetDescriptorByCellPosition(i);
                IXlCell cell = this.bandedExportInfo.Exporter.BeginCell();
                if (descriptorByCellPosition.Column == null)
                {
                    base.SetFormatting(cell, new XlCellFormatting(), false);
                }
                else
                {
                    IColumnExportProvider<TRow> columnExportProvider = this.GetColumnExportProvider(descriptorByCellPosition, areaType);
                    if ((columnExportProvider != null) && columnExportProvider.CanExportValue(areaType))
                    {
                        columnExportProvider.ExportValue(areaType, cell, row, this.bandedExportInfo.ExportRowIndex);
                    }
                    this.MergeCells(descriptorByCellPosition, i, areaType);
                }
                this.bandedExportInfo.Exporter.EndCell();
            }
        }

        public override void CreateHeader()
        {
            if (!this.bandedExportInfo.ShowBands || this.bandedExportInfo.LinearBandsAndColumns)
            {
                if (this.bandedExportInfo.ShowColumnHeaders)
                {
                    base.CreateHeader();
                }
            }
            else
            {
                int num = this.bandedExportInfo.ShowBandHeaders ? 0 : this.bandedExportInfo.HeaderPanelRowsCount;
                int num2 = this.bandedExportInfo.ShowColumnHeaders ? this.bandedExportInfo.HeaderRowsCount : this.bandedExportInfo.HeaderPanelRowsCount;
                for (int i = num; i < num2; i++)
                {
                    this.bandedExportInfo.Exporter.BeginRow();
                    TRow row = default(TRow);
                    this.ConstructRow(SheetAreaType.Header, row, i);
                    this.bandedExportInfo.Exporter.EndRow();
                    int exportRowIndex = this.bandedExportInfo.ExportRowIndex;
                    this.bandedExportInfo.ExportRowIndex = exportRowIndex + 1;
                }
                base.DataCellsRangeTop += this.bandedExportInfo.ExportRowIndex;
            }
        }

        protected IColumnExportProvider<TRow> FindColumnExportProvider(BandNodeDescriptor desc, List<IColumnExportProvider<TRow>> providers)
        {
            for (int i = 0; i < providers.Count; i++)
            {
                IColumnExportProvider<TRow> provider = providers[i];
                if (string.Equals(provider.FieldName, desc.Column.FieldName) && !provider.Used)
                {
                    provider.Used = true;
                    return provider;
                }
            }
            return null;
        }

        private BandNodeDescriptor FindDesc(List<BandNodeDescriptor> values, IColumn child)
        {
            IBandedGridColumn bgc = child as IBandedGridColumn;
            if (bgc != null)
            {
                for (int i = 0; i < values.Count; i++)
                {
                    BandNodeDescriptor descriptor = values[i];
                    IBandedGridColumn column = descriptor.Column as IBandedGridColumn;
                    if ((column != null) && this.ColumnsEqual(column, bgc))
                    {
                        return descriptor;
                    }
                }
            }
            return new BandNodeDescriptor();
        }

        protected virtual BandedRowInfo GetBandedRowInfo(int rowIndex, SheetAreaType areaType) => 
            (rowIndex < this.bandedExportInfo.BandedHeaderRowPattern.Count) ? this.bandedExportInfo.BandedHeaderRowPattern[rowIndex] : new BandedRowInfo();

        protected virtual IColumnExportProvider<TRow> GetColumnExportProvider(BandNodeDescriptor desc, SheetAreaType areaType) => 
            this.FindColumnExportProvider(desc, this.bandedExportInfo.HeaderExportProviders);

        protected virtual BandNodeDescriptor GetDescriptor(IList<BandNodeDescriptor> exportItems, int cnt, SheetAreaType areaType)
        {
            if (exportItems.Count > 0)
            {
                return exportItems[cnt];
            }
            return new BandNodeDescriptor();
        }

        protected override int GetIndexByRowCount()
        {
            if (!this.bandedExportInfo.ShowBands || this.bandedExportInfo.LinearBandsAndColumns)
            {
                return base.GetIndexByRowCount();
            }
            int headerRowsCount = this.bandedExportInfo.HeaderRowsCount;
            if (!this.bandedExportInfo.ShowBandHeaders)
            {
                headerRowsCount -= this.bandedExportInfo.HeaderPanelRowsCount;
            }
            if (!this.bandedExportInfo.ShowColumnHeaders)
            {
                headerRowsCount -= this.bandedExportInfo.HeaderColumnPanelRowsCount;
            }
            return ((headerRowsCount - 1) + this.CalcFixedRowsCount());
        }

        protected virtual void MergeCells(BandNodeDescriptor desc, int cellIndex, SheetAreaType areaType)
        {
            if (this.bandedExportInfo.AllowBandHeaderCellMerge)
            {
                if (!this.bandedExportInfo.AllowCombinedBandAndColumnHeaderCellMerge || !this.bandedExportInfo.MergedBands.Contains(desc))
                {
                    MergingUtils.MergeCells(this.bandedExportInfo.Sheet, desc, cellIndex, this.bandedExportInfo.ExportRowIndex, this.bandedExportInfo.AllowCombinedBandAndColumnHeaderCellMerge, false);
                }
                else
                {
                    List<IColumn> children = desc.Column.GetAllColumns().ToList<IColumn>();
                    if (children.Count > 0)
                    {
                        int index = desc.RowIndex + desc.RowCount;
                        this.CalcChildrenIndex(children, desc.Column.Header, ref index);
                        MergingUtils.MergeCells(this.bandedExportInfo.Sheet, desc, cellIndex, this.bandedExportInfo.ExportRowIndex, (int) (index - 1), true);
                        this.currentRowIndex = -1;
                    }
                }
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DataAwareBandedExportContext<TCol, TRow>.<>c <>9;
            public static Func<BandNodeDescriptor, bool> <>9__9_0;
            public static Func<BandNodeDescriptor, bool> <>9__9_1;

            static <>c()
            {
                DataAwareBandedExportContext<TCol, TRow>.<>c.<>9 = new DataAwareBandedExportContext<TCol, TRow>.<>c();
            }

            internal bool <CalcChildrenIndex>b__9_0(BandNodeDescriptor x) => 
                x.Column is IGridBand;

            internal bool <CalcChildrenIndex>b__9_1(BandNodeDescriptor x) => 
                !(x.Column is IGridBand);
        }
    }
}

