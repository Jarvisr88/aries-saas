namespace DevExpress.Printing.ExportHelpers
{
    using DevExpress.Export.Xl;
    using DevExpress.Printing.DataAwareExport.Export.Utils;
    using DevExpress.XtraExport.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;

    internal class AdvBandedExportInfo<TCol, TRow> : BandedExportInfo<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        private IAdvancedBandedGridView<TCol, TRow> advBandedView;
        private int bandedRowPatternCountLocal;
        private BandedAreaRowPattern bandedRowPattern;
        private BandedAreaRowPattern totalFooterRowPattern;
        private BandedAreaRowPattern groupFooterRowPattern;
        private List<int> columnsByRowsCount;
        private int leaves;

        public AdvBandedExportInfo(GridViewExcelExporter<TCol, TRow> helper) : base(helper)
        {
            this.bandedRowPatternCountLocal = -1;
            this.columnsByRowsCount = new List<int>();
            this.advBandedView = base.Helper.View as IAdvancedBandedGridView<TCol, TRow>;
        }

        protected override void AddColumns(IEnumerable<TCol> columns, int rowIndex, int columnIndex)
        {
            int num = rowIndex;
            int index = 0;
            while (index < this.advBandedView.ColumnPanelRowsCount)
            {
                List<TCol> list = (from x in columns
                    where ((IBandedGridColumn) x).RowIndex == index
                    select x).ToList<TCol>();
                int num2 = 0;
                int num3 = 0;
                while (true)
                {
                    if (num3 >= list.Count)
                    {
                        num += num2;
                        index++;
                        break;
                    }
                    TCol band = list[num3];
                    IBandedGridColumn col = (IBandedGridColumn) band;
                    int rowCount = this.CalcColumnRowCount(columns, col, col.RowCount);
                    BandNodeDescriptor descriptor = this.GetBandNodeDescriptor(band, num, rowCount, col.ColVIndex + columnIndex);
                    base.bandsLayoutInfo.Add(band, descriptor);
                    if (rowCount > num2)
                    {
                        num2 = rowCount;
                    }
                    num3++;
                }
            }
        }

        private void CalcBandLeaves(TCol band, int rowIndex)
        {
            if (band != null)
            {
                if (band.IsGroupColumn)
                {
                    this.columnsByRowsCount.Clear();
                    IEnumerable<IColumn> allColumns = band.GetAllColumns();
                    foreach (TCol local in allColumns)
                    {
                        this.CalcBandLeaves(local, ((IBandedGridColumn) local).RowIndex);
                    }
                    base.leavesCount = !allColumns.Any<IColumn>() ? (base.leavesCount + 1) : (base.leavesCount + this.leaves);
                    this.leaves = 0;
                }
                else
                {
                    while (true)
                    {
                        if (this.columnsByRowsCount.Count > rowIndex)
                        {
                            int num = rowIndex;
                            this.columnsByRowsCount[num] += 1;
                            this.leaves = ((IEnumerable<int>) this.columnsByRowsCount).Max();
                            break;
                        }
                        this.columnsByRowsCount.Add(0);
                    }
                }
            }
        }

        protected override int CalcColumnRowCount(IEnumerable<TCol> columns, IBandedGridColumn col, int rowCount)
        {
            if (col.AutoFillDown && AdvBandedExportInfo<TCol, TRow>.IsColumnLastInBand(columns, col))
            {
                rowCount = this.advBandedView.ColumnPanelRowsCount - col.RowIndex;
            }
            return rowCount;
        }

        internal BandedAreaRowPattern ConstructAdvBandedFooterRowPattern(IEnumerable<ISummaryItemEx> items)
        {
            Func<BandNodeDescriptor, bool> predicate = <>c<TCol, TRow>.<>9__20_0;
            if (<>c<TCol, TRow>.<>9__20_0 == null)
            {
                Func<BandNodeDescriptor, bool> local1 = <>c<TCol, TRow>.<>9__20_0;
                predicate = <>c<TCol, TRow>.<>9__20_0 = x => !x.Column.IsGroupColumn;
            }
            BandNodeDescriptor[] validValues = base.BandsLayoutInfo.Values.Where<BandNodeDescriptor>(predicate).ToArray<BandNodeDescriptor>();
            BandedAreaRowPattern source = new BandedAreaRowPattern();
            this.ItemsByNodes = this.EnsureItemsByNodes(items, validValues);
            if (this.ItemsByNodes.Count > 0)
            {
                int num = 0;
                while (num < this.BandedRowPatternCount)
                {
                    int num1;
                    int rowIndex = num;
                    KeyValuePair<BandNodeDescriptor, int>[] pairArray = (from x in this.ItemsByNodes
                        where Math.Abs((int) (x.Key.RowIndex - ((AdvBandedExportInfo<TCol, TRow>) this).HeaderPanelRowsCount)) == rowIndex
                        select x).ToArray<KeyValuePair<BandNodeDescriptor, int>>();
                    if (pairArray.Length == 0)
                    {
                        num1 = 1;
                    }
                    else
                    {
                        Func<KeyValuePair<BandNodeDescriptor, int>, int> selector = <>c<TCol, TRow>.<>9__20_2;
                        if (<>c<TCol, TRow>.<>9__20_2 == null)
                        {
                            Func<KeyValuePair<BandNodeDescriptor, int>, int> local2 = <>c<TCol, TRow>.<>9__20_2;
                            selector = <>c<TCol, TRow>.<>9__20_2 = x => x.Value;
                        }
                        num1 = pairArray.Max<KeyValuePair<BandNodeDescriptor, int>>(selector);
                    }
                    int num2 = num1;
                    int num3 = 0;
                    while (true)
                    {
                        Func<BandNodeDescriptor, bool> <>9__3;
                        if (num3 >= num2)
                        {
                            num++;
                            break;
                        }
                        source.Add(new BandedRowInfo());
                        Func<BandNodeDescriptor, bool> func4 = <>9__3;
                        if (<>9__3 == null)
                        {
                            Func<BandNodeDescriptor, bool> local3 = <>9__3;
                            func4 = <>9__3 = x => Math.Abs((int) (x.RowIndex - ((AdvBandedExportInfo<TCol, TRow>) this).HeaderPanelRowsCount)) == rowIndex;
                        }
                        BandNodeDescriptor[] descriptorArray2 = validValues.Where<BandNodeDescriptor>(func4).ToArray<BandNodeDescriptor>();
                        int index = 0;
                        while (true)
                        {
                            if (index >= descriptorArray2.Length)
                            {
                                num3++;
                                break;
                            }
                            source.Last<BandedRowInfo>().Add(descriptorArray2[index]);
                            index++;
                        }
                    }
                }
            }
            return source;
        }

        private int CountFieldSummaryRows(IEnumerable<ISummaryItemEx> items, TCol column) => 
            ((items == null) || !items.Any<ISummaryItemEx>()) ? 1 : items.Count<ISummaryItemEx>(x => (x.ShowInColumnFooterName == column.FieldName));

        internal override ColumnExportInfo<TCol, TRow> CreateColumnExportInfo() => 
            new AdvBandedColumnExportInfo<TCol, TRow>(this);

        internal override void CreateColumnExportProvidersCore(List<IColumnExportProvider<TRow>> providers)
        {
            if (base.BandsLayoutInfo.Count == 0)
            {
                base.CreateColumnExportProvidersCore(providers);
            }
            else
            {
                foreach (KeyValuePair<TCol, BandNodeDescriptor> pair in base.BandsLayoutInfo)
                {
                    if (!(pair.Key is IGridBand))
                    {
                        if (base.ColumnHasValidUnboundInfo(pair.Key))
                        {
                            AdvBandedUnboundColumnExportProvider<TCol, TRow> item = new AdvBandedUnboundColumnExportProvider<TCol, TRow>(pair.Key, this.ColumnExportInfo, pair.Value.ColIndex);
                            item.SetExpressionConverterOffset(pair.Value.RowIndex);
                            providers.Add(item);
                            continue;
                        }
                        if (pair.Key.ColEditType == ColumnEditTypes.RichText)
                        {
                            providers.Add(new RichTextColumnExportProvider<TCol, TRow>(pair.Key, this.ColumnExportInfo, pair.Value.ColIndex));
                            continue;
                        }
                        if (pair.Key.ColEditType == ColumnEditTypes.HtmlText)
                        {
                            providers.Add(new HyperTextColumnExportProvider<TCol, TRow>(pair.Key, this.ColumnExportInfo, pair.Key.VisibleIndex));
                            continue;
                        }
                        providers.Add(new ColumnExportProvider<TCol, TRow>(pair.Key, this.ColumnExportInfo, pair.Value.ColIndex));
                    }
                }
            }
        }

        internal override ExportHelpersProvider<TCol, TRow> CreateHelpersProvider() => 
            new AdvBandedExportHelpersProvider<TCol, TRow>(this);

        private Dictionary<BandNodeDescriptor, int> EnsureItemsByNodes(IEnumerable<ISummaryItemEx> items, BandNodeDescriptor[] validValues)
        {
            Dictionary<BandNodeDescriptor, int> dictionary = new Dictionary<BandNodeDescriptor, int>();
            if ((base.BandsLayoutInfo != null) && (base.BandsLayoutInfo.Count != 0))
            {
                Func<TCol, bool> predicate = <>c<TCol, TRow>.<>9__21_0;
                if (<>c<TCol, TRow>.<>9__21_0 == null)
                {
                    Func<TCol, bool> local1 = <>c<TCol, TRow>.<>9__21_0;
                    predicate = <>c<TCol, TRow>.<>9__21_0 = x => !x.IsGroupColumn;
                }
                foreach (TCol column in base.BandsLayoutInfo.Keys.Where<TCol>(predicate).ToArray<TCol>())
                {
                    BandNodeDescriptor key = validValues.FirstOrDefault<BandNodeDescriptor>(x => x.Column.FieldName == column.FieldName);
                    if (!dictionary.ContainsKey(key))
                    {
                        dictionary.Add(key, this.CountFieldSummaryRows(items, column));
                    }
                }
            }
            return dictionary;
        }

        protected override BandNodeDescriptor GetBandNodeDescriptor(TCol band, int rowIndex, int rowCount, int cIndex)
        {
            base.depth = 0;
            base.prevBandLevel = 0;
            if (!band.IsGroupColumn && (this.columnsByRowsCount.Count > 0))
            {
                int num = ((IBandedGridColumn) band).RowIndex;
                int num2 = this.columnsByRowsCount[num];
                base.leavesCount = ((IEnumerable<int>) this.columnsByRowsCount).Max() / num2;
            }
            else
            {
                base.leavesCount = 0;
                this.leaves = 0;
                this.CalcBandLeaves(band, 0);
            }
            BandNodeDescriptor descriptor = new BandNodeDescriptor();
            if (band.IsGroupColumn && (band.GetColumnGroupLevel() == 0))
            {
                base.depth++;
            }
            descriptor.Depth = base.depth;
            descriptor.Leaves = base.leavesCount;
            descriptor.Column = band;
            descriptor.RowCount = rowCount;
            descriptor.RowIndex = rowIndex;
            descriptor.ColIndex = cIndex;
            return descriptor;
        }

        private static bool IsColumnLastInBand(IEnumerable<TCol> columns, IBandedGridColumn col)
        {
            Func<TCol, int> selector = <>c<TCol, TRow>.<>9__32_0;
            if (<>c<TCol, TRow>.<>9__32_0 == null)
            {
                Func<TCol, int> local1 = <>c<TCol, TRow>.<>9__32_0;
                selector = <>c<TCol, TRow>.<>9__32_0 = x => ((IBandedGridColumn) x).RowIndex;
            }
            return (col.RowIndex == columns.Max<TCol>(selector));
        }

        internal override void RaiseAfterAddRowEvent(IRowBase row, DataAwareExportContext<TCol, TRow> context)
        {
        }

        public int BandedRowPatternCount
        {
            get
            {
                if (this.bandedRowPatternCountLocal == -1)
                {
                    int num = 0;
                    if (base.BandedHeaderRowPattern.Count > 0)
                    {
                        BandedRowInfo source = base.BandedHeaderRowPattern.Last<BandedRowInfo>();
                        if (source != null)
                        {
                            Func<BandNodeDescriptor, int> selector = <>c<TCol, TRow>.<>9__5_0;
                            if (<>c<TCol, TRow>.<>9__5_0 == null)
                            {
                                Func<BandNodeDescriptor, int> local1 = <>c<TCol, TRow>.<>9__5_0;
                                selector = <>c<TCol, TRow>.<>9__5_0 = x => x.RowCount;
                            }
                            num = source.Max<BandNodeDescriptor>(selector);
                        }
                    }
                    this.bandedRowPatternCountLocal = (this.BandedRowPattern.Count + num) - 1;
                }
                return this.bandedRowPatternCountLocal;
            }
        }

        public BandedAreaRowPattern BandedRowPattern
        {
            get
            {
                if (this.bandedRowPattern == null)
                {
                    if ((this.advBandedView.CustomBandedRowPattern != null) && (this.advBandedView.CustomBandedRowPattern.Count > 0))
                    {
                        this.bandedRowPattern = this.advBandedView.CustomBandedRowPattern;
                    }
                    else
                    {
                        Func<TCol, bool> searchPredicate = <>c<TCol, TRow>.<>9__8_0;
                        if (<>c<TCol, TRow>.<>9__8_0 == null)
                        {
                            Func<TCol, bool> local1 = <>c<TCol, TRow>.<>9__8_0;
                            searchPredicate = <>c<TCol, TRow>.<>9__8_0 = x => !x.IsGroupColumn;
                        }
                        this.bandedRowPattern = this.FillPattern(searchPredicate, x => Math.Abs((int) (x.RowIndex - base.HeaderPanelRowsCount)));
                    }
                }
                return this.bandedRowPattern;
            }
        }

        internal BandedAreaRowPattern TotalFooterRowPattern
        {
            get
            {
                this.totalFooterRowPattern ??= (((this.advBandedView.CustomBandedFooterRowPattern == null) || (this.advBandedView.CustomBandedFooterRowPattern.Count <= 0)) ? this.ConstructAdvBandedFooterRowPattern(base.View.GridTotalSummaryItemCollection) : this.advBandedView.CustomBandedFooterRowPattern);
                return this.totalFooterRowPattern;
            }
        }

        internal BandedAreaRowPattern GroupFooterRowPattern
        {
            get
            {
                this.groupFooterRowPattern ??= (((this.advBandedView.CustomBandedGroupFooterRowPattern == null) || (this.advBandedView.CustomBandedGroupFooterRowPattern.Count <= 0)) ? this.ConstructAdvBandedFooterRowPattern(base.View.GridGroupSummaryItemCollection) : this.advBandedView.CustomBandedGroupFooterRowPattern);
                return this.groupFooterRowPattern;
            }
        }

        internal Dictionary<BandNodeDescriptor, int> ItemsByNodes { get; set; }

        internal override CriteriaOperatorToXlExpressionConverter Converter
        {
            get
            {
                if (base.converter == null)
                {
                    AdvBandedColumnPositionConverter<TCol> columnNameConverter = new AdvBandedColumnPositionConverter<TCol>(base.BandsLayoutInfo, base.HeaderPanelRowsCount);
                    base.converter = new CriteriaOperatorToXlExpressionConverter(columnNameConverter);
                    base.converter.Culture = CultureInfo.CurrentCulture;
                }
                return base.converter;
            }
        }

        public override bool ApplyFormattingToEntireColumn =>
            false;

        public override bool AlignGroupSummaryInGroupRow =>
            base.AlignGroupSummaryInGroupRow && (this.BandedRowPattern.Count == 1);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly AdvBandedExportInfo<TCol, TRow>.<>c <>9;
            public static Func<BandNodeDescriptor, int> <>9__5_0;
            public static Func<TCol, bool> <>9__8_0;
            public static Func<BandNodeDescriptor, bool> <>9__20_0;
            public static Func<KeyValuePair<BandNodeDescriptor, int>, int> <>9__20_2;
            public static Func<TCol, bool> <>9__21_0;
            public static Func<TCol, int> <>9__32_0;

            static <>c()
            {
                AdvBandedExportInfo<TCol, TRow>.<>c.<>9 = new AdvBandedExportInfo<TCol, TRow>.<>c();
            }

            internal bool <ConstructAdvBandedFooterRowPattern>b__20_0(BandNodeDescriptor x) => 
                !x.Column.IsGroupColumn;

            internal int <ConstructAdvBandedFooterRowPattern>b__20_2(KeyValuePair<BandNodeDescriptor, int> x) => 
                x.Value;

            internal bool <EnsureItemsByNodes>b__21_0(TCol x) => 
                !x.IsGroupColumn;

            internal bool <get_BandedRowPattern>b__8_0(TCol x) => 
                !x.IsGroupColumn;

            internal int <get_BandedRowPatternCount>b__5_0(BandNodeDescriptor x) => 
                x.RowCount;

            internal int <IsColumnLastInBand>b__32_0(TCol x) => 
                ((IBandedGridColumn) x).RowIndex;
        }
    }
}

