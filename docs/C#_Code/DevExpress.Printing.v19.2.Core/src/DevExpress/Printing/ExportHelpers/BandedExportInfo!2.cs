namespace DevExpress.Printing.ExportHelpers
{
    using DevExpress.Printing.DataAwareExport.Export.Utils;
    using DevExpress.Utils;
    using DevExpress.XtraExport.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    internal class BandedExportInfo<TCol, TRow> : ExportInfo<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        private IBandedGridView<TCol, TRow> bandedView;
        private int headerPanelRowsCount;
        private int headerRowCountLocal;
        private int bandedColumnsCount;
        private BandedAreaRowPattern bandedHeaderRowPattern;
        private BandedAreaRowPattern bandedHeaderBandsRowPattern;
        private BandedAreaRowPattern bandedHeaderColumnsRowPattern;
        private HashSet<BandNodeDescriptor> mergedBands;
        protected Dictionary<TCol, BandNodeDescriptor> bandsLayoutInfo;
        private List<IColumnExportProvider<TRow>> headerExportProviders;
        protected int depth;
        protected int leavesCount;
        protected int prevBandLevel;

        public BandedExportInfo(GridViewExcelExporter<TCol, TRow> helper) : base(helper)
        {
            this.headerPanelRowsCount = -1;
            this.headerRowCountLocal = -1;
            this.bandedColumnsCount = -1;
            this.leavesCount = 1;
            this.bandedView = base.Helper.View as IBandedGridView<TCol, TRow>;
        }

        private int AddBand(TCol band, int rowIndex, int cIndex)
        {
            IGridBand band2 = band as IGridBand;
            if (band2 == null)
            {
                return -1;
            }
            int rowCount = band2.RowCount;
            IEnumerable<IColumn> allColumns = band.GetAllColumns();
            bool flag = band2.VisibleChildrenBandsCount > 0;
            if (!flag && band2.AutoFillDown)
            {
                rowCount = this.bandedView.BandRowCount - rowIndex;
            }
            BandNodeDescriptor descriptor = this.GetBandNodeDescriptor(band, rowIndex, rowCount, cIndex);
            this.bandsLayoutInfo.Add(band, descriptor);
            if (flag)
            {
                this.AddBands(DevExpress.Printing.DataAwareExport.Export.Utils.Utils<TCol>.GetCollection(allColumns), rowIndex + rowCount, cIndex);
            }
            int num2 = rowIndex + rowCount;
            if (!band2.AutoFillDown)
            {
                num2 = Math.Max(this.bandedView.BandRowCount, num2);
            }
            this.AddColumns(DevExpress.Printing.DataAwareExport.Export.Utils.Utils<TCol>.GetCollection(band2.GetColumns()), num2, cIndex);
            return (cIndex + descriptor.Leaves);
        }

        private void AddBands(IEnumerable<TCol> bands, int rowIndex, int cIndex)
        {
            foreach (TCol local in bands)
            {
                cIndex = this.AddBand(local, rowIndex, cIndex);
            }
        }

        protected virtual void AddColumns(IEnumerable<TCol> columns, int rowIndex, int columnIndex)
        {
            foreach (TCol local in columns)
            {
                IBandedGridColumn col = (IBandedGridColumn) local;
                int num = rowIndex + col.RowIndex;
                int rowCount = this.CalcColumnRowCount(columns, col, col.RowCount);
                this.bandsLayoutInfo.Add(local, this.GetBandNodeDescriptor(local, num, rowCount, col.ColVIndex + columnIndex));
            }
        }

        protected virtual void CalcBandParams(TCol band)
        {
            if (band != null)
            {
                if (!band.IsGroupColumn)
                {
                    this.leavesCount++;
                }
                else
                {
                    if (band.GetColumnGroupLevel() > this.prevBandLevel)
                    {
                        this.depth++;
                        this.prevBandLevel = band.GetColumnGroupLevel();
                    }
                    IEnumerable<IColumn> allColumns = band.GetAllColumns();
                    foreach (TCol local in allColumns)
                    {
                        this.CalcBandParams(local);
                    }
                    if (!allColumns.Any<IColumn>())
                    {
                        this.leavesCount++;
                    }
                }
            }
        }

        protected virtual int CalcColumnRowCount(IEnumerable<TCol> columns, IBandedGridColumn col, int rowCount) => 
            rowCount;

        protected override int CalcColumns()
        {
            Func<BandedRowInfo, int> selector = <>c<TCol, TRow>.<>9__3_0;
            if (<>c<TCol, TRow>.<>9__3_0 == null)
            {
                Func<BandedRowInfo, int> local1 = <>c<TCol, TRow>.<>9__3_0;
                selector = <>c<TCol, TRow>.<>9__3_0 = x => x.Count;
            }
            return this.BandedHeaderColumnsRowPattern.Max<BandedRowInfo>(selector);
        }

        internal override bool CanExportCurrentColumn(TCol gridColumns) => 
            true;

        private bool CheckChildren(IGridBand target)
        {
            bool flag;
            using (IEnumerator<IColumn> enumerator = target.GetAllColumns().GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        IColumn current = enumerator.Current;
                        if (current is IGridBand)
                        {
                            this.CheckChildren((IGridBand) current);
                        }
                        if (string.Equals(current.Header, target.Header))
                        {
                            continue;
                        }
                        flag = false;
                    }
                    else
                    {
                        return true;
                    }
                    break;
                }
            }
            return flag;
        }

        protected override bool CheckShowOptions() => 
            base.ShowColumnHeaders || (base.ShowBandHeaders || (base.View.FixedRowsCount > 0));

        internal override void CreateColumnExportProvidersCore(List<IColumnExportProvider<TRow>> providers)
        {
            if (this.BandsLayoutInfo.Count == 0)
            {
                base.CreateColumnExportProvidersCore(providers);
            }
            else
            {
                foreach (KeyValuePair<TCol, BandNodeDescriptor> pair in this.BandsLayoutInfo)
                {
                    if (!(pair.Key is IGridBand))
                    {
                        if (base.ColumnHasValidUnboundInfo(pair.Key))
                        {
                            providers.Add(new UnboundColumnExportProvider<TCol, TRow>(pair.Key, this.ColumnExportInfo, pair.Value.ColIndex));
                            continue;
                        }
                        providers.Add(new ColumnExportProvider<TCol, TRow>(pair.Key, this.ColumnExportInfo, pair.Value.ColIndex));
                    }
                }
            }
        }

        internal List<IColumnExportProvider<TRow>> CreateHeaderExportProviders()
        {
            List<IColumnExportProvider<TRow>> list = new List<IColumnExportProvider<TRow>>();
            foreach (KeyValuePair<TCol, BandNodeDescriptor> pair in this.BandsLayoutInfo)
            {
                if (base.ColumnHasValidUnboundInfo(pair.Key))
                {
                    list.Add(new UnboundColumnExportProvider<TCol, TRow>(pair.Key, this.ColumnExportInfo, pair.Value.ColIndex));
                    continue;
                }
                list.Add(new ColumnExportProvider<TCol, TRow>(pair.Key, this.ColumnExportInfo, pair.Value.ColIndex));
            }
            return list;
        }

        internal override ExportHelpersProvider<TCol, TRow> CreateHelpersProvider() => 
            new BandedExportHelpersProvider<TCol, TRow>(this);

        protected BandedAreaRowPattern FillPattern(Func<TCol, bool> searchPredicate, Func<BandNodeDescriptor, int> calcRowIndex)
        {
            TCol[] localArray = this.BandsLayoutInfo.Keys.Where<TCol>(searchPredicate).ToArray<TCol>();
            BandNodeDescriptor[] source = (from x in this.BandsLayoutInfo.Values
                where searchPredicate((TCol) x.Column)
                select x).ToArray<BandNodeDescriptor>();
            BandedAreaRowPattern pattern = new BandedAreaRowPattern((source.Length != 0) ? (source.Max<BandNodeDescriptor>(calcRowIndex) + 1) : 1);
            for (int i = 0; i < localArray.Length; i++)
            {
                TCol local = localArray[i];
                IBandedGridColumn cl = local as IBandedGridColumn;
                if ((cl != null) && (source.Length != 0))
                {
                    BandNodeDescriptor item = source.FirstOrDefault<BandNodeDescriptor>(x => Equals(x.Column.GetHashCode(), cl.GetHashCode()));
                    pattern[calcRowIndex(item)].Add(item);
                }
            }
            return pattern;
        }

        private Func<TCol, bool> FillPatternAllHierarhy() => 
            <>c<TCol, TRow>.<>9__24_0 ??= x => true;

        private Func<TCol, bool> FillPatternOnlyBands() => 
            <>c<TCol, TRow>.<>9__26_0 ??= x => x.IsGroupColumn;

        private Func<TCol, bool> FillPatternOnlyColumns() => 
            <>c<TCol, TRow>.<>9__25_0 ??= x => !x.IsGroupColumn;

        protected virtual BandNodeDescriptor GetBandNodeDescriptor(TCol band, int rowIndex, int rowCount, int cIndex)
        {
            this.depth = 0;
            this.leavesCount = 0;
            this.prevBandLevel = 0;
            this.CalcBandParams(band);
            BandNodeDescriptor descriptor = new BandNodeDescriptor();
            if (band.IsGroupColumn && (band.GetColumnGroupLevel() == 0))
            {
                this.depth++;
            }
            descriptor.Depth = this.depth;
            descriptor.Leaves = this.leavesCount;
            descriptor.Column = band;
            descriptor.RowCount = rowCount;
            descriptor.RowIndex = rowIndex;
            descriptor.ColIndex = cIndex;
            return descriptor;
        }

        internal override int RaiseMergeEvent(int row1, int row2, TCol col) => 
            base.View.RaiseMergeEvent(row1, row2, col);

        private unsafe void RecalcColumnPositions()
        {
            if ((this.bandsLayoutInfo != null) && (this.bandsLayoutInfo.Count != 0))
            {
                Func<BandNodeDescriptor, int> selector = <>c<TCol, TRow>.<>9__48_0;
                if (<>c<TCol, TRow>.<>9__48_0 == null)
                {
                    Func<BandNodeDescriptor, int> local1 = <>c<TCol, TRow>.<>9__48_0;
                    selector = <>c<TCol, TRow>.<>9__48_0 = x => x.RowIndex;
                }
                int num = this.bandsLayoutInfo.Values.Max<BandNodeDescriptor>(selector) + 1;
                int i = 0;
                while (i < num)
                {
                    int leaves = 0;
                    int colIndex = 0;
                    KeyValuePair<TCol, BandNodeDescriptor>[] pairArray = (from x in this.bandsLayoutInfo
                        where x.Value.RowIndex == i
                        select x).ToArray<KeyValuePair<TCol, BandNodeDescriptor>>();
                    int index = 0;
                    while (true)
                    {
                        if (index >= pairArray.Length)
                        {
                            i++;
                            break;
                        }
                        TCol key = pairArray[index].Key;
                        BandNodeDescriptor descriptor = pairArray[index].Value;
                        int num5 = leaves + colIndex;
                        if ((num5 != 0) && ((num5 != descriptor.ColIndex) && ((((descriptor.ColIndex + leaves) - 1) < this.BandedColumnsCount) && ((descriptor.ColIndex - colIndex) == 1))))
                        {
                            int* numPtr1 = &descriptor.ColIndex;
                            numPtr1[0] += leaves - 1;
                            this.bandsLayoutInfo[key] = descriptor;
                        }
                        leaves = descriptor.Leaves;
                        colIndex = descriptor.ColIndex;
                        index++;
                    }
                }
            }
        }

        private HashSet<BandNodeDescriptor> SelectMergedBands()
        {
            HashSet<BandNodeDescriptor> set = new HashSet<BandNodeDescriptor>();
            Func<BandNodeDescriptor, bool> predicate = <>c<TCol, TRow>.<>9__31_0;
            if (<>c<TCol, TRow>.<>9__31_0 == null)
            {
                Func<BandNodeDescriptor, bool> local1 = <>c<TCol, TRow>.<>9__31_0;
                predicate = <>c<TCol, TRow>.<>9__31_0 = x => x.Column.IsGroupColumn;
            }
            Func<BandNodeDescriptor, bool> func2 = <>c<TCol, TRow>.<>9__31_1;
            if (<>c<TCol, TRow>.<>9__31_1 == null)
            {
                Func<BandNodeDescriptor, bool> local2 = <>c<TCol, TRow>.<>9__31_1;
                func2 = <>c<TCol, TRow>.<>9__31_1 = delegate (BandNodeDescriptor x) {
                    Func<IColumn, bool> func1 = <>c<TCol, TRow>.<>9__31_2;
                    if (<>c<TCol, TRow>.<>9__31_2 == null)
                    {
                        Func<IColumn, bool> local1 = <>c<TCol, TRow>.<>9__31_2;
                        func1 = <>c<TCol, TRow>.<>9__31_2 = c => ((IBandedGridColumn) c).ColIndex == 0;
                    }
                    return ((IGridBand) x.Column).GetAllColumns().All<IColumn>(func1);
                };
            }
            foreach (BandNodeDescriptor descriptor in this.BandsLayoutInfo.Values.Where<BandNodeDescriptor>(predicate).ToArray<BandNodeDescriptor>().Where<BandNodeDescriptor>(func2))
            {
                if (this.CheckChildren((IGridBand) descriptor.Column))
                {
                    set.Add(descriptor);
                }
            }
            return set;
        }

        public int HeaderPanelRowsCount
        {
            get
            {
                if ((this.headerPanelRowsCount == -1) && (this.BandedHeaderBandsRowPattern.Count > 0))
                {
                    this.headerPanelRowsCount = this.BandedHeaderBandsRowPattern.Count;
                }
                return this.headerPanelRowsCount;
            }
        }

        public int HeaderColumnPanelRowsCount =>
            this.BandedHeaderColumnsRowPattern.Count;

        public int HeaderRowsCount
        {
            get
            {
                if (this.headerRowCountLocal == -1)
                {
                    int num = 0;
                    int count = this.BandedHeaderRowPattern.Count;
                    if (count > 0)
                    {
                        BandedRowInfo source = this.BandedHeaderRowPattern.Last<BandedRowInfo>();
                        if (source != null)
                        {
                            Func<BandNodeDescriptor, int> selector = <>c<TCol, TRow>.<>9__11_0;
                            if (<>c<TCol, TRow>.<>9__11_0 == null)
                            {
                                Func<BandNodeDescriptor, int> local1 = <>c<TCol, TRow>.<>9__11_0;
                                selector = <>c<TCol, TRow>.<>9__11_0 = x => x.RowCount;
                            }
                            num = source.Max<BandNodeDescriptor>(selector);
                        }
                    }
                    this.headerRowCountLocal = (count + num) - 1;
                }
                return this.headerRowCountLocal;
            }
        }

        public int BandedColumnsCount
        {
            get
            {
                if (base.LinearBandsAndColumns)
                {
                    return base.ColumnsInfoColl.Count;
                }
                if ((this.bandedColumnsCount == -1) && (this.BandedHeaderRowPattern.Count > 0))
                {
                    for (int i = 0; i < this.BandedHeaderRowPattern.Count; i++)
                    {
                        Func<BandNodeDescriptor, int> selector = <>c<TCol, TRow>.<>9__14_0;
                        if (<>c<TCol, TRow>.<>9__14_0 == null)
                        {
                            Func<BandNodeDescriptor, int> local1 = <>c<TCol, TRow>.<>9__14_0;
                            selector = <>c<TCol, TRow>.<>9__14_0 = x => x.Leaves;
                        }
                        int num2 = this.BandedHeaderRowPattern[i].Sum<BandNodeDescriptor>(selector);
                        if (num2 > this.bandedColumnsCount)
                        {
                            this.bandedColumnsCount = num2;
                        }
                    }
                }
                return this.bandedColumnsCount;
            }
        }

        public BandedAreaRowPattern BandedHeaderRowPattern
        {
            get
            {
                if (this.bandedHeaderRowPattern == null)
                {
                    if ((this.bandedView.CustomBandedHeaderRowPattern != null) && (this.bandedView.CustomBandedHeaderRowPattern.Count > 0))
                    {
                        this.bandedHeaderRowPattern = this.bandedView.CustomBandedHeaderRowPattern;
                    }
                    else if (!this.ShowBands)
                    {
                        this.bandedHeaderRowPattern = this.BandedHeaderColumnsRowPattern;
                    }
                    else
                    {
                        Func<BandNodeDescriptor, int> calcRowIndex = <>c<TCol, TRow>.<>9__17_0;
                        if (<>c<TCol, TRow>.<>9__17_0 == null)
                        {
                            Func<BandNodeDescriptor, int> local1 = <>c<TCol, TRow>.<>9__17_0;
                            calcRowIndex = <>c<TCol, TRow>.<>9__17_0 = x => x.RowIndex;
                        }
                        this.bandedHeaderRowPattern = this.FillPattern(this.FillPatternAllHierarhy(), calcRowIndex);
                    }
                }
                return this.bandedHeaderRowPattern;
            }
        }

        private BandedAreaRowPattern BandedHeaderBandsRowPattern
        {
            get
            {
                if (this.bandedHeaderBandsRowPattern == null)
                {
                    Func<BandNodeDescriptor, int> calcRowIndex = <>c<TCol, TRow>.<>9__20_0;
                    if (<>c<TCol, TRow>.<>9__20_0 == null)
                    {
                        Func<BandNodeDescriptor, int> local1 = <>c<TCol, TRow>.<>9__20_0;
                        calcRowIndex = <>c<TCol, TRow>.<>9__20_0 = x => x.RowIndex;
                    }
                    this.bandedHeaderBandsRowPattern = this.FillPattern(this.FillPatternOnlyBands(), calcRowIndex);
                }
                return this.bandedHeaderBandsRowPattern;
            }
        }

        private BandedAreaRowPattern BandedHeaderColumnsRowPattern
        {
            get
            {
                this.bandedHeaderColumnsRowPattern ??= this.FillPattern(this.FillPatternOnlyColumns(), x => Math.Abs((int) (x.RowIndex - base.HeaderPanelRowsCount)));
                return this.bandedHeaderColumnsRowPattern;
            }
        }

        internal HashSet<BandNodeDescriptor> MergedBands
        {
            get
            {
                this.mergedBands ??= this.SelectMergedBands();
                return this.mergedBands;
            }
        }

        public bool AllowCombinedBandAndColumnHeaderCellMerge =>
            base.Options.AllowCombinedBandAndColumnHeaderCellMerge == DefaultBoolean.True;

        public bool ShowBands =>
            ((IBandedGridView<TCol, TRow>) base.View).BandedGridOptionsView.ShowBands;

        public bool AllowBandHeaderCellMerge =>
            base.Options.AllowBandHeaderCellMerge != DefaultBoolean.False;

        public Dictionary<TCol, BandNodeDescriptor> BandsLayoutInfo
        {
            get
            {
                if (this.bandsLayoutInfo == null)
                {
                    this.bandsLayoutInfo = new Dictionary<TCol, BandNodeDescriptor>();
                    this.AddBands(base.View.GetAllColumns(), 0, 0);
                    this.RecalcColumnPositions();
                }
                return this.bandsLayoutInfo;
            }
        }

        internal List<IColumnExportProvider<TRow>> HeaderExportProviders
        {
            get
            {
                this.headerExportProviders ??= this.CreateHeaderExportProviders();
                return this.headerExportProviders;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BandedExportInfo<TCol, TRow>.<>c <>9;
            public static Func<BandedRowInfo, int> <>9__3_0;
            public static Func<BandNodeDescriptor, int> <>9__11_0;
            public static Func<BandNodeDescriptor, int> <>9__14_0;
            public static Func<BandNodeDescriptor, int> <>9__17_0;
            public static Func<BandNodeDescriptor, int> <>9__20_0;
            public static Func<TCol, bool> <>9__24_0;
            public static Func<TCol, bool> <>9__25_0;
            public static Func<TCol, bool> <>9__26_0;
            public static Func<BandNodeDescriptor, bool> <>9__31_0;
            public static Func<IColumn, bool> <>9__31_2;
            public static Func<BandNodeDescriptor, bool> <>9__31_1;
            public static Func<BandNodeDescriptor, int> <>9__48_0;

            static <>c()
            {
                BandedExportInfo<TCol, TRow>.<>c.<>9 = new BandedExportInfo<TCol, TRow>.<>c();
            }

            internal int <CalcColumns>b__3_0(BandedRowInfo x) => 
                x.Count;

            internal bool <FillPatternAllHierarhy>b__24_0(TCol x) => 
                true;

            internal bool <FillPatternOnlyBands>b__26_0(TCol x) => 
                x.IsGroupColumn;

            internal bool <FillPatternOnlyColumns>b__25_0(TCol x) => 
                !x.IsGroupColumn;

            internal int <get_BandedColumnsCount>b__14_0(BandNodeDescriptor x) => 
                x.Leaves;

            internal int <get_BandedHeaderBandsRowPattern>b__20_0(BandNodeDescriptor x) => 
                x.RowIndex;

            internal int <get_BandedHeaderRowPattern>b__17_0(BandNodeDescriptor x) => 
                x.RowIndex;

            internal int <get_HeaderRowsCount>b__11_0(BandNodeDescriptor x) => 
                x.RowCount;

            internal int <RecalcColumnPositions>b__48_0(BandNodeDescriptor x) => 
                x.RowIndex;

            internal bool <SelectMergedBands>b__31_0(BandNodeDescriptor x) => 
                x.Column.IsGroupColumn;

            internal bool <SelectMergedBands>b__31_1(BandNodeDescriptor x)
            {
                Func<IColumn, bool> predicate = BandedExportInfo<TCol, TRow>.<>c.<>9__31_2;
                if (BandedExportInfo<TCol, TRow>.<>c.<>9__31_2 == null)
                {
                    Func<IColumn, bool> local1 = BandedExportInfo<TCol, TRow>.<>c.<>9__31_2;
                    predicate = BandedExportInfo<TCol, TRow>.<>c.<>9__31_2 = c => ((IBandedGridColumn) c).ColIndex == 0;
                }
                return ((IGridBand) x.Column).GetAllColumns().All<IColumn>(predicate);
            }

            internal bool <SelectMergedBands>b__31_2(IColumn c) => 
                ((IBandedGridColumn) c).ColIndex == 0;
        }
    }
}

