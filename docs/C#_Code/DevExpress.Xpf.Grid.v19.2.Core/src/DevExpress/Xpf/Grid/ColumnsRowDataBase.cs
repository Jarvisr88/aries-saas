namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Grid.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows.Media;

    public abstract class ColumnsRowDataBase : RowDataBase, IViewRowData
    {
        private IList<GridColumnData> cellData;
        private IList<GridColumnData> fixedLeftCellData;
        private IList<GridColumnData> fixedRightCellData;
        private IList<GridColumnData> fixedNoneCellData;
        private double fixedNoneContentWidth;
        private readonly UpdateCellDataStrategy updateCellDataStrategy;
        internal DataTreeBuilder treeBuilder;

        public ColumnsRowDataBase(DataTreeBuilder treeBuilder, Func<FrameworkElement> createRowElementDelegate = null) : base(createRowElementDelegate)
        {
            this.UpdateTreeBuilder(treeBuilder);
            this.CellDataCache = new Dictionary<ColumnBase, GridColumnData>();
            this.updateCellDataStrategy = new UpdateCellDataStrategy(this);
        }

        internal virtual bool CanReuseCellData() => 
            true;

        internal GridColumnData CreateCellDataByColumn(ColumnBase column)
        {
            GridColumnData cellData = this.CreateGridCellDataCore();
            this.UpdateCellData(column, cellData);
            return cellData;
        }

        public GridCellDataList CreateCellDataList() => 
            this.UpdateOnlyData ? null : new GridCellDataList(this, this.treeBuilder.GetVisibleColumns());

        protected internal virtual GridColumnData CreateGridCellDataCore() => 
            new GridColumnData(this);

        void IViewRowData.SetViewAndUpdate(DataViewBase view)
        {
            this.UpdateTreeBuilder(view.VisualDataTreeBuilder);
            this.UpdateCellData();
            base.View.ViewBehavior.UpdateFixedNoneContentWidth(this);
        }

        internal GridColumnData GetCellDataByColumn(ColumnBase column) => 
            this.GetCellDataByColumn(column, true, true);

        internal GridColumnData GetCellDataByColumn(ColumnBase column, bool updateNewCellData, bool addToCache)
        {
            GridColumnData data;
            if (column == null)
            {
                return null;
            }
            if (!this.CellDataCache.TryGetValue(column, out data))
            {
                data = this.CreateGridCellDataCore();
                if (updateNewCellData)
                {
                    this.UpdateCellData(column, data);
                }
                if (addToCache)
                {
                    if (this.CellDataCache.ContainsKey(column))
                    {
                        this.CellDataCache[column] = data;
                    }
                    else
                    {
                        this.CellDataCache.Add(column, data);
                    }
                }
            }
            return data;
        }

        protected internal virtual double GetFixedNoneContentWidth(double totalWidth) => 
            totalWidth;

        protected virtual void OnFixedLeftCellDataChanged(IList<GridColumnData> oldValue)
        {
        }

        protected virtual void OnFixedNoneCellDataChanged()
        {
        }

        protected virtual void OnFixedNoneContentWidthCahnged()
        {
        }

        protected virtual void OnFixedRightCellDataChanged(IList<GridColumnData> oldValue)
        {
        }

        protected void ReuseCellData<TColumnData>(Func<ColumnsRowDataBase, IList<TColumnData>> getter, Action<ColumnsRowDataBase, IList> setter, UpdateCellDataStrategyBase<TColumnData> updateStrategy, IList<ColumnBase> sourceColumns, int maxDataCount, int bufferLength) where TColumnData: GridColumnData
        {
            if (updateStrategy.CanReuseCellData)
            {
                ReuseCellDataHelper.ReuseCellData<TColumnData>(this, getter, setter, updateStrategy, sourceColumns, bufferLength, maxDataCount);
            }
            else
            {
                setter(this, new GridCellDataList(this, sourceColumns));
            }
        }

        internal void ReuseCellDataNotVirtualized(Func<ColumnsRowDataBase, IList<GridColumnData>> getter, Action<ColumnsRowDataBase, IList> setter, IList<ColumnBase> sourceColumns)
        {
            this.ReuseCellDataNotVirtualized<GridColumnData>(getter, setter, this.updateCellDataStrategy, sourceColumns);
        }

        protected void ReuseCellDataNotVirtualized<TColumnData>(Func<ColumnsRowDataBase, IList<TColumnData>> getter, Action<ColumnsRowDataBase, IList> setter, UpdateCellDataStrategyBase<TColumnData> updateStrategy, IList<ColumnBase> sourceColumns) where TColumnData: GridColumnData
        {
            this.ReuseCellData<TColumnData>(getter, setter, updateStrategy, sourceColumns, sourceColumns.Count, 0);
        }

        internal bool ShouldUpdateCellData(GridColumnData data, ColumnBase column) => 
            !ReferenceEquals(data.Column, column) || this.ShouldUpdateCellDataCore(column, data);

        protected virtual bool ShouldUpdateCellDataCore(ColumnBase column, GridColumnData data) => 
            false;

        internal void UpdateCellData()
        {
            base.View.UpdateCellData(this);
        }

        internal virtual void UpdateCellData(ColumnBase column, GridColumnData cellData)
        {
            cellData.Column = column;
            this.treeBuilder.UpdateColumnData(this, cellData, column);
        }

        internal virtual void UpdateFixedLeftCellData()
        {
            if (!this.UpdateOnlyData)
            {
                Func<ColumnsRowDataBase, IList<GridColumnData>> getter = <>c.<>9__51_0;
                if (<>c.<>9__51_0 == null)
                {
                    Func<ColumnsRowDataBase, IList<GridColumnData>> local1 = <>c.<>9__51_0;
                    getter = <>c.<>9__51_0 = x => x.FixedLeftCellData;
                }
                this.ReuseCellDataNotVirtualized(getter, <>c.<>9__51_1 ??= (x, val) => (x.FixedLeftCellData = (IList<GridColumnData>) val), this.treeBuilder.GetFixedLeftColumns());
            }
        }

        internal void UpdateFixedNoneCellData(bool virtualized)
        {
            if (!this.UpdateOnlyData)
            {
                this.UpdateFixedNoneCellDataCore(virtualized && this.treeBuilder.SupportsHorizontalVirtualization);
            }
        }

        protected virtual void UpdateFixedNoneCellDataCore(bool virtualized)
        {
            if (!virtualized || !this.CanReuseCellData())
            {
                Func<ColumnsRowDataBase, IList<GridColumnData>> getter = <>c.<>9__57_2;
                if (<>c.<>9__57_2 == null)
                {
                    Func<ColumnsRowDataBase, IList<GridColumnData>> local3 = <>c.<>9__57_2;
                    getter = <>c.<>9__57_2 = x => x.FixedNoneCellData;
                }
                this.ReuseCellDataNotVirtualized(getter, <>c.<>9__57_3 ??= (x, val) => (x.FixedNoneCellData = (IList<GridColumnData>) val), this.treeBuilder.GetFixedNoneColumns());
            }
            else
            {
                ITableView view = (ITableView) base.View;
                Func<ColumnsRowDataBase, IList<GridColumnData>> getter = <>c.<>9__57_0;
                if (<>c.<>9__57_0 == null)
                {
                    Func<ColumnsRowDataBase, IList<GridColumnData>> local1 = <>c.<>9__57_0;
                    getter = <>c.<>9__57_0 = x => x.FixedNoneCellData;
                }
                this.ReuseCellData<GridColumnData>(getter, <>c.<>9__57_1 ??= (x, val) => (x.FixedNoneCellData = (IList<GridColumnData>) val), this.updateCellDataStrategy, view.ViewportVisibleColumns, view.TableViewBehavior.FixedNoneVisibleColumns.Count, 5);
            }
        }

        internal virtual void UpdateFixedRightCellData()
        {
            if (!this.UpdateOnlyData)
            {
                Func<ColumnsRowDataBase, IList<GridColumnData>> getter = <>c.<>9__52_0;
                if (<>c.<>9__52_0 == null)
                {
                    Func<ColumnsRowDataBase, IList<GridColumnData>> local1 = <>c.<>9__52_0;
                    getter = <>c.<>9__52_0 = x => x.FixedRightCellData;
                }
                this.ReuseCellDataNotVirtualized(getter, <>c.<>9__52_1 ??= (x, val) => (x.FixedRightCellData = (IList<GridColumnData>) val), this.treeBuilder.GetFixedRightColumns());
            }
        }

        internal void UpdateTreeBuilder(DataTreeBuilder newTreeBuilder)
        {
            if (!ReferenceEquals(this.treeBuilder, newTreeBuilder))
            {
                this.treeBuilder = newTreeBuilder;
                base.View = this.treeBuilder.View;
            }
        }

        [Description("")]
        public IList<GridColumnData> CellData
        {
            get => 
                this.cellData;
            internal set
            {
                if (!ReferenceEquals(this.cellData, value))
                {
                    this.cellData = value;
                    base.RaisePropertyChanged("CellData");
                }
            }
        }

        [Description("")]
        public IList<GridColumnData> FixedLeftCellData
        {
            get => 
                this.fixedLeftCellData;
            internal set
            {
                if (!ReferenceEquals(this.fixedLeftCellData, value))
                {
                    IList<GridColumnData> fixedLeftCellData = this.fixedLeftCellData;
                    this.fixedLeftCellData = value;
                    base.RaisePropertyChanged("FixedLeftCellData");
                    this.OnFixedLeftCellDataChanged(fixedLeftCellData);
                }
            }
        }

        [Description("")]
        public IList<GridColumnData> FixedRightCellData
        {
            get => 
                this.fixedRightCellData;
            internal set
            {
                if (!ReferenceEquals(this.fixedRightCellData, value))
                {
                    IList<GridColumnData> fixedRightCellData = this.fixedRightCellData;
                    this.fixedRightCellData = value;
                    base.RaisePropertyChanged("FixedRightCellData");
                    this.OnFixedRightCellDataChanged(fixedRightCellData);
                }
            }
        }

        [Description("")]
        public IList<GridColumnData> FixedNoneCellData
        {
            get => 
                this.fixedNoneCellData;
            internal set
            {
                if (!ReferenceEquals(this.fixedNoneCellData, value))
                {
                    this.fixedNoneCellData = value;
                    base.RaisePropertyChanged("FixedNoneCellData");
                    this.OnFixedNoneCellDataChanged();
                }
            }
        }

        [Description("")]
        public double FixedNoneContentWidth
        {
            get => 
                this.fixedNoneContentWidth;
            set
            {
                if (this.fixedNoneContentWidth != value)
                {
                    this.fixedNoneContentWidth = value;
                    base.RaisePropertyChanged("FixedNoneContentWidth");
                    this.OnFixedNoneContentWidthCahnged();
                }
            }
        }

        [Description("")]
        public virtual int Level =>
            0;

        public double ActualBandRightSeparatorWidth =>
            0.0;

        public Brush ActualBandHeaderRightSeparatorColor =>
            null;

        public double ActualBandLeftSeparatorWidth =>
            0.0;

        public Brush ActualBandHeaderLeftSeparatorColor =>
            null;

        internal Dictionary<ColumnBase, GridColumnData> CellDataCache { get; private set; }

        protected DataControlBase DataControl =>
            base.View?.DataControl;

        protected virtual bool UpdateOnlyData =>
            false;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ColumnsRowDataBase.<>c <>9 = new ColumnsRowDataBase.<>c();
            public static Func<ColumnsRowDataBase, IList<GridColumnData>> <>9__51_0;
            public static Action<ColumnsRowDataBase, IList> <>9__51_1;
            public static Func<ColumnsRowDataBase, IList<GridColumnData>> <>9__52_0;
            public static Action<ColumnsRowDataBase, IList> <>9__52_1;
            public static Func<ColumnsRowDataBase, IList<GridColumnData>> <>9__57_0;
            public static Action<ColumnsRowDataBase, IList> <>9__57_1;
            public static Func<ColumnsRowDataBase, IList<GridColumnData>> <>9__57_2;
            public static Action<ColumnsRowDataBase, IList> <>9__57_3;

            internal IList<GridColumnData> <UpdateFixedLeftCellData>b__51_0(ColumnsRowDataBase x) => 
                x.FixedLeftCellData;

            internal void <UpdateFixedLeftCellData>b__51_1(ColumnsRowDataBase x, IList val)
            {
                x.FixedLeftCellData = (IList<GridColumnData>) val;
            }

            internal IList<GridColumnData> <UpdateFixedNoneCellDataCore>b__57_0(ColumnsRowDataBase x) => 
                x.FixedNoneCellData;

            internal void <UpdateFixedNoneCellDataCore>b__57_1(ColumnsRowDataBase x, IList val)
            {
                x.FixedNoneCellData = (IList<GridColumnData>) val;
            }

            internal IList<GridColumnData> <UpdateFixedNoneCellDataCore>b__57_2(ColumnsRowDataBase x) => 
                x.FixedNoneCellData;

            internal void <UpdateFixedNoneCellDataCore>b__57_3(ColumnsRowDataBase x, IList val)
            {
                x.FixedNoneCellData = (IList<GridColumnData>) val;
            }

            internal IList<GridColumnData> <UpdateFixedRightCellData>b__52_0(ColumnsRowDataBase x) => 
                x.FixedRightCellData;

            internal void <UpdateFixedRightCellData>b__52_1(ColumnsRowDataBase x, IList val)
            {
                x.FixedRightCellData = (IList<GridColumnData>) val;
            }
        }

        private class UpdateCellDataStrategy : UpdateCellDataStrategyBase<GridColumnData>
        {
            private readonly ColumnsRowDataBase rowData;

            public UpdateCellDataStrategy(ColumnsRowDataBase rowData)
            {
                this.rowData = rowData;
            }

            public override GridColumnData CreateNewData() => 
                this.rowData.CreateGridCellDataCore();

            public override void UpdateData(ColumnBase column, GridColumnData columnData)
            {
                this.rowData.UpdateCellData(column, columnData);
            }

            public override bool CanReuseCellData =>
                this.rowData.CanReuseCellData();

            public override Dictionary<ColumnBase, GridColumnData> DataCache =>
                this.rowData.CellDataCache;
        }
    }
}

