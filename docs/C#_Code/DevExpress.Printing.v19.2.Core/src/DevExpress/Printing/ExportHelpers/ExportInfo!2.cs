namespace DevExpress.Printing.ExportHelpers
{
    using DevExpress.Export;
    using DevExpress.Export.Xl;
    using DevExpress.Printing.DataAwareExport.Export.Utils;
    using DevExpress.Utils;
    using DevExpress.XtraExport.Helpers;
    using DevExpress.XtraExport.Implementation;
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class ExportInfo<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        private int gridRowsCount;
        private XlExportOptionsBase optionsBase;
        private XlsExportOptions optionsXls;
        private XlsxExportOptionsEx optionsXlsx;
        private Lazy<Stack<DevExpress.Printing.ExportHelpers.XlGroup>> groupsStack;
        private Lazy<List<Group>> groupsList;
        private GridViewExcelExporter<TCol, TRow> helper;
        private HashSet<string> columnsWithEmptyCells;
        private XlRangeOverlapChecker overlapChecker;
        private List<DevExpress.Printing.ExportHelpers.XlGroup> precalculatedGroupsList;
        protected CriteriaOperatorToXlExpressionConverter converter;
        private ExportColumnsCollection<TCol> columnsInfoCollection;
        private int columnsCount;
        private IEnumerable<TCol> _gridColumns;
        private List<IColumnExportProvider<TRow>> exportProviders;
        private ColumnExportInfo<TCol, TRow> columnExportInfo;
        private IList<TCol> gridGroupedColumns;
        private const int maxGroupingLevel = 8;
        private const int defaultRowHeight = -1;
        private bool endSheetFlag;
        private int skipRowIndex;
        private bool? allowExportSummaryItemsAlignByColumnsInFooter;
        private int alignByColumnsSummaryMaxCount;
        private ExportHelpersProvider<TCol, TRow> provider;

        public ExportInfo(GridViewExcelExporter<TCol, TRow> helper)
        {
            this.columnsCount = -1;
            this.endSheetFlag = true;
            this.skipRowIndex = -1;
            this.alignByColumnsSummaryMaxCount = -1;
            this.ProgressPercentage = -1;
            this.helper = helper;
            this.gridRowsCount = this.CalcAllRows();
            this.optionsBase = helper.Options as XlExportOptionsBase;
            this.optionsXls = helper.Options as XlsExportOptions;
            this.optionsXlsx = helper.Options as XlsxExportOptionsEx;
            this.groupsStack = new Lazy<Stack<DevExpress.Printing.ExportHelpers.XlGroup>>(ExportInfo<TCol, TRow>.CreateStackInstance());
            this.groupsList = new Lazy<List<Group>>(ExportInfo<TCol, TRow>.CreateGroupsListInstance());
            this.IsCsvExport = helper.Options is CsvExportOptions;
        }

        private void AddColumn(TCol gridColumn, ExportColumnsCollection<TCol> columns)
        {
            if (this.LinearBandsAndColumns || !gridColumn.IsGroupColumn)
            {
                columns.Add(gridColumn, this.AllowLookupValues);
            }
        }

        private void AssignRowSettings(DataAwareEventArgsBase e, IRowBase row)
        {
            e.DocumentRow = this.ExportRowIndex;
            if (row == null)
            {
                e.DataSourceRowIndex = -1;
            }
            else
            {
                e.RowHandle = row.LogicalPosition;
                e.DataSourceRowIndex = row.DataSourceRowIndex;
            }
        }

        private int CalcAllRows()
        {
            int rowCount = (int) this.helper.View.RowCount;
            if (this.Options.AllowLookupValues == DefaultBoolean.True)
            {
                rowCount += LookUpValuesExporter<TCol, TRow>.AuxiliarySheetRowsCount;
            }
            return rowCount;
        }

        protected virtual int CalcColumns() => 
            this.ColumnsInfoColl.Count;

        internal virtual bool CanExportCurrentColumn(TCol gridColumn) => 
            this.View.OptionsView.ShowGroupedColumns || (this.View.OptionsBehavior.AlignGroupSummaryInGroupRow || (gridColumn.GroupIndex == -1));

        internal bool CanSkipRow() => 
            this.SkipRowIndex == this.ExportRowIndex;

        protected virtual bool CheckShowOptions() => 
            this.ShowColumnHeaders || (this.View.FixedRowsCount > 0);

        public void ClearFormatInfo()
        {
            for (int i = 0; i < this.ExportProviders.Count; i++)
            {
                this.ExportProviders[i].ClearCaches();
            }
        }

        protected bool ColumnHasValidUnboundInfo(TCol col) => 
            !(col is IGridBand) && ((col.UnboundInfo != null) && !string.IsNullOrEmpty(col.UnboundInfo.UnboundExpression));

        internal bool ColumnIsSourceForCondFmtRule(string columnFieldName)
        {
            bool flag;
            using (IEnumerator<IFormatRuleBase> enumerator = this.View.FormatRules.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        IFormatRuleBase current = enumerator.Current;
                        if (current.Rule is IFormatConditionRuleExpression)
                        {
                            IFormatConditionRuleExpression rule = current.Rule as IFormatConditionRuleExpression;
                            if (!string.IsNullOrEmpty(rule.Expression) && rule.Expression.Contains(columnFieldName))
                            {
                                flag = true;
                                break;
                            }
                        }
                        if (current.Rule is IFormatConditionRuleValue)
                        {
                            IFormatConditionRuleValue rule = current.Rule as IFormatConditionRuleValue;
                            if ((rule.Condition == FormatConditions.Expression) && (!string.IsNullOrEmpty(rule.Expression) && rule.Expression.Contains(columnFieldName)))
                            {
                                flag = true;
                                break;
                            }
                        }
                        IColumn column = current.Column ?? current.ColumnApplyTo;
                        if ((column == null) || (column.FieldName != columnFieldName))
                        {
                            continue;
                        }
                        flag = true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                }
            }
            return flag;
        }

        internal void CompleteReportProgress()
        {
            for (int i = this.ProgressPercentage; i <= 100; i++)
            {
                this.ReportProgress(i);
            }
        }

        public bool ComplyWithFormatLimits() => 
            (this.SuppressMaxRowsWarning == DefaultBoolean.True) ? (this.ExportRowIndex < this.Exporter.DocumentOptions.MaxRowCount) : true;

        public bool ComplyWithFormatLimits(int index) => 
            (this.SuppressMaxColumnsWarning == DefaultBoolean.True) ? (index < this.Exporter.DocumentOptions.MaxColumnCount) : true;

        private ExportColumnsCollection<TCol> Create()
        {
            ExportColumnsCollection<TCol> columns = new ExportColumnsCollection<TCol>();
            this.Helper.ForAllColumns(this.helper.View, delegate (TCol gridColumn) {
                ((ExportInfo<TCol, TRow>) this).AddColumn(gridColumn, columns);
            });
            return columns;
        }

        internal virtual ColumnExportInfo<TCol, TRow> CreateColumnExportInfo() => 
            new ColumnExportInfo<TCol, TRow>((ExportInfo<TCol, TRow>) this);

        internal List<IColumnExportProvider<TRow>> CreateColumnExportProviderCollection()
        {
            List<IColumnExportProvider<TRow>> providers = new List<IColumnExportProvider<TRow>>();
            this.CreateColumnExportProvidersCore(providers);
            return providers;
        }

        internal virtual void CreateColumnExportProvidersCore(List<IColumnExportProvider<TRow>> providers)
        {
            foreach (TCol local in this.GridColumns)
            {
                if (local != null)
                {
                    if (this.ColumnHasValidUnboundInfo(local))
                    {
                        providers.Add(new UnboundColumnExportProvider<TCol, TRow>(local, this.ColumnExportInfo, local.VisibleIndex));
                        continue;
                    }
                    if (local.ColEditType == ColumnEditTypes.RichText)
                    {
                        providers.Add(new RichTextColumnExportProvider<TCol, TRow>(local, this.ColumnExportInfo, local.VisibleIndex));
                        continue;
                    }
                    if (local.ColEditType == ColumnEditTypes.HtmlText)
                    {
                        providers.Add(new HyperTextColumnExportProvider<TCol, TRow>(local, this.ColumnExportInfo, local.VisibleIndex));
                        continue;
                    }
                    providers.Add(new ColumnExportProvider<TCol, TRow>(local, this.ColumnExportInfo, local.VisibleIndex));
                }
            }
        }

        private IEnumerable<TCol> CreateGridColumnsCollection()
        {
            List<TCol> columns = new List<TCol>();
            this.Helper.ForAllColumns(this.helper.View, delegate (TCol gridColumn) {
                if (((ExportInfo<TCol, TRow>) this).LinearBandsAndColumns || !gridColumn.IsGroupColumn)
                {
                    columns.Add(gridColumn);
                }
            });
            return columns;
        }

        private static Func<List<Group>> CreateGroupsListInstance() => 
            <>c<TCol, TRow>.<>9__22_0 ??= () => new List<Group>();

        internal virtual ExportHelpersProvider<TCol, TRow> CreateHelpersProvider() => 
            new ExportHelpersProvider<TCol, TRow>((ExportInfo<TCol, TRow>) this);

        private static Func<Stack<DevExpress.Printing.ExportHelpers.XlGroup>> CreateStackInstance() => 
            <>c<TCol, TRow>.<>9__23_0 ??= () => new Stack<DevExpress.Printing.ExportHelpers.XlGroup>();

        internal virtual void RaiseAfterAddRowEvent(IRowBase row, DataAwareExportContext<TCol, TRow> context)
        {
            if (!this.AlignGroupSummaryInGroupRow)
            {
                AfterAddRowEventArgs args1 = new AfterAddRowEventArgs();
                args1.DataSourceOwner = this.helper.View;
                args1.ExportContext = context;
                AfterAddRowEventArgs e = args1;
                this.AssignRowSettings(e, row);
                this.Options.RaiseAfterAddRowEvent(e);
            }
        }

        internal virtual void RaiseContextCustomizationEvent(EventType type, DataAwareExportContext<TCol, TRow> context)
        {
            ContextEventArgs args1 = new ContextEventArgs();
            args1.ExportContext = context;
            ContextEventArgs ea = args1;
            if (type == EventType.Header)
            {
                this.Options.RaiseCustomizeSheetHeaderEvent(ea);
            }
            else if (type == EventType.Footer)
            {
                this.Options.RaiseCustomizeSheetFooterEvent(ea);
            }
        }

        internal virtual void RaiseCustomizeSheetSettingsEvent(DataAwareExportContext<TCol, TRow> context)
        {
            CustomizeSheetEventArgs args1 = new CustomizeSheetEventArgs();
            args1.ExportContext = context;
            args1.Sheet = this.Sheet;
            CustomizeSheetEventArgs ea = args1;
            this.Options.RaiseCustomizeSheetSettingsEvent(ea);
        }

        internal virtual int RaiseMergeEvent(int row1, int row2, TCol col) => 
            this.View.RaiseMergeEvent(row1, row2, col);

        internal virtual void RaiseSkipRowEvent(FooterAreaType areaType, int areaRowHandle, DevExpress.Printing.ExportHelpers.XlGroup group)
        {
            SkipFooterRowEventArgs args1 = new SkipFooterRowEventArgs();
            args1.AreaType = areaType;
            SkipFooterRowEventArgs ea = args1;
            ea.MultiLineSummaryFooterIndex = areaRowHandle;
            ea.SummaryFooterHandle = group.GroupRowHandle;
            ea.GroupHierarchyLevel = (group.Group != null) ? (group.Group.OutlineLevel - 1) : -1;
            ea.GroupFieldName = group.GroupFieldName;
            this.Options.RaiseSkipFooterRowEvent(ea);
            if (ea.SkipFooterRow)
            {
                this.SetSkipRowIndex();
            }
        }

        public void ReportProgress()
        {
            this.helper.Options.ReportProgress(new ProgressChangedEventArgs(this.ProgressPercentage, null));
        }

        public void ReportProgress(int exportRowIndex)
        {
            int num = Math.Min(100, (int) ((((float) (exportRowIndex + 1)) / ((float) this.gridRowsCount)) * 100f));
            if (num > this.ProgressPercentage)
            {
                this.ProgressPercentage = num;
                this.ReportProgress();
            }
        }

        internal void ResetSkipRowIndex()
        {
            this.SkipRowIndex = -1;
        }

        internal void SetSkipRowIndex()
        {
            this.SkipRowIndex = this.ExportRowIndex;
            int skipRowWatchdog = this.SkipRowWatchdog;
            this.SkipRowWatchdog = skipRowWatchdog + 1;
        }

        public GridViewExcelExporter<TCol, TRow> Helper =>
            this.helper;

        public IGridView<TCol, TRow> View =>
            this.helper.View;

        public IXlSheet Sheet =>
            this.helper.Sheet;

        public IDataAwareExportOptions Options =>
            this.helper.Options;

        public IXlExport Exporter =>
            this.helper.Exporter;

        internal bool IsCsvExport { get; private set; }

        public Stack<DevExpress.Printing.ExportHelpers.XlGroup> GroupsStack =>
            this.groupsStack.Value;

        public List<Group> GroupsList =>
            this.groupsList.Value;

        internal HashSet<string> ColumnsWithEmptyCells
        {
            get
            {
                this.columnsWithEmptyCells ??= new HashSet<string>();
                return this.columnsWithEmptyCells;
            }
        }

        internal XlRangeOverlapChecker OverlapChecker
        {
            get
            {
                this.overlapChecker ??= new XlRangeOverlapChecker();
                return this.overlapChecker;
            }
        }

        internal List<DevExpress.Printing.ExportHelpers.XlGroup> PrecalculatedGroupsList
        {
            get
            {
                this.precalculatedGroupsList ??= new List<DevExpress.Printing.ExportHelpers.XlGroup>();
                return this.precalculatedGroupsList;
            }
        }

        internal virtual CriteriaOperatorToXlExpressionConverter Converter
        {
            get
            {
                if (this.converter == null)
                {
                    Dictionary<string, int> columnPositionTable = ColumnTableCreator<TCol>.Create<int>(this.ColumnsInfoColl, gridColumn => base.ColumnsInfoColl.IndexOf(gridColumn.FieldName));
                    this.converter = new CriteriaOperatorToXlExpressionConverter(new ColumnPositionConverter<TCol>(columnPositionTable));
                    this.converter.Culture = CultureInfo.CurrentCulture;
                }
                return this.converter;
            }
        }

        public ExportColumnsCollection<TCol> ColumnsInfoColl
        {
            get
            {
                this.columnsInfoCollection ??= this.Create();
                return this.columnsInfoCollection;
            }
        }

        public int ColumnsCount
        {
            get
            {
                if (this.columnsCount == -1)
                {
                    this.columnsCount = this.CalcColumns();
                }
                return this.columnsCount;
            }
        }

        public IEnumerable<TCol> GridColumns
        {
            get
            {
                this._gridColumns ??= this.CreateGridColumnsCollection();
                return this._gridColumns;
            }
        }

        internal List<IColumnExportProvider<TRow>> ExportProviders
        {
            get
            {
                this.exportProviders ??= this.CreateColumnExportProviderCollection();
                return this.exportProviders;
            }
        }

        internal virtual ColumnExportInfo<TCol, TRow> ColumnExportInfo
        {
            get
            {
                if (this.columnExportInfo == null)
                {
                    this.columnExportInfo = this.CreateColumnExportInfo();
                    this.columnExportInfo.ExpressionConverter = this.Converter;
                }
                return this.columnExportInfo;
            }
        }

        public IList<TCol> GridGroupedColumns
        {
            get
            {
                this.gridGroupedColumns ??= ((this.View.GetGroupedColumns() == null) ? new List<TCol>() : (this.View.GetGroupedColumns() as IList<TCol>));
                return this.gridGroupedColumns;
            }
        }

        internal int MaxGroupingLevel =>
            8;

        internal int DefaultRowHeight =>
            -1;

        public bool ColumnGrouping { get; set; }

        internal int ExportRowIndex { get; set; }

        internal int DataAreaBottomRowIndex { get; set; }

        internal int ProgressPercentage { get; set; }

        internal bool EndSheetFlag
        {
            get => 
                this.endSheetFlag;
            set => 
                this.endSheetFlag = value;
        }

        internal int SkipRowIndex
        {
            get => 
                this.skipRowIndex;
            set => 
                this.skipRowIndex = value;
        }

        internal int SkipRowWatchdog { get; set; }

        public DefaultBoolean SuppressMaxRowsWarning =>
            (this.optionsXls == null) ? ((this.optionsXlsx == null) ? DefaultBoolean.Default : (this.optionsXlsx.SuppressMaxRowsWarning ? DefaultBoolean.True : DefaultBoolean.False)) : (this.optionsXls.Suppress65536RowsWarning ? DefaultBoolean.True : DefaultBoolean.False);

        public DefaultBoolean SuppressMaxColumnsWarning =>
            (this.optionsXls == null) ? ((this.optionsXlsx == null) ? DefaultBoolean.Default : (this.optionsXlsx.SuppressMaxColumnsWarning ? DefaultBoolean.True : DefaultBoolean.False)) : (this.optionsXls.Suppress256ColumnsWarning ? DefaultBoolean.True : DefaultBoolean.False);

        public bool ApplyColumnFilteringEventSettings =>
            this.OptionsAllowAddAutoFilter && this.Options.CanRaiseDocumentColumnFilteringEvent;

        public bool OptionsAllowAddAutoFilter =>
            (this.Options.ShowColumnHeaders == DefaultBoolean.True) && (this.Options.AllowSortingAndFiltering == DefaultBoolean.True);

        public bool OptionsAllowSetFixedHeader =>
            this.CheckShowOptions() && (this.Options.AllowFixedColumnHeaderPanel == DefaultBoolean.True);

        public bool RawDataMode =>
            (this.optionsBase != null) && this.optionsBase.RawDataMode;

        public bool ShowGridLines =>
            (this.optionsBase != null) && this.optionsBase.ShowGridLines;

        public bool AllowGrouping =>
            this.Options.AllowGrouping == DefaultBoolean.True;

        public bool LinearBandsAndColumns =>
            this.Options.BandedLayoutMode == BandedLayoutMode.LinearBandsAndColumns;

        public bool AllowGroupingRows =>
            this.Options.AllowGroupingRows == DefaultBoolean.True;

        public bool ShowPagePrintTitles =>
            this.Options.ShowPageTitle == DefaultBoolean.True;

        public bool AllowSparklines =>
            this.Options.AllowSparklines == DefaultBoolean.True;

        public bool AllowConditionalFormatting =>
            this.Options.AllowConditionalFormatting == DefaultBoolean.True;

        public bool AutoSelectMinimumIconValue =>
            this.Options.AutoCalcConditionalFormattingIconSetMinValue != DefaultBoolean.False;

        public bool AllowCellMerge =>
            this.Options.AllowCellMerge == DefaultBoolean.True;

        public bool AllowLookupValues =>
            this.Options.AllowLookupValues == DefaultBoolean.True;

        public virtual bool AlignGroupSummaryInGroupRow =>
            this.View.OptionsBehavior.AlignGroupSummaryInGroupRow;

        public bool ShowColumnHeaders =>
            this.Options.ShowColumnHeaders == DefaultBoolean.True;

        public bool ShowBandHeaders =>
            this.Options.ShowBandHeaders == DefaultBoolean.True;

        public bool ShowTotalSummary =>
            this.Options.ShowTotalSummaries == DefaultBoolean.True;

        public bool AllowFixedColumns =>
            this.Options.AllowFixedColumns == DefaultBoolean.True;

        public virtual bool ApplyFormattingToEntireColumn =>
            (this.Options.ApplyFormattingToEntireColumn == DefaultBoolean.True) || (this.Options.ApplyFormattingToEntireColumn == DefaultBoolean.Default);

        public virtual bool AllowHorzLines =>
            this.Options.AllowHorzLines == DefaultBoolean.True;

        public virtual bool AllowVertLines =>
            this.Options.AllowVertLines == DefaultBoolean.True;

        public bool AllowExportSummaryItemsAlignByColumnsInFooter
        {
            get
            {
                if (!this.AlignGroupSummaryInGroupRow)
                {
                    return false;
                }
                if (this.allowExportSummaryItemsAlignByColumnsInFooter == null)
                {
                    Func<ISummaryItemEx, bool> predicate = <>c<TCol, TRow>.<>9__150_0;
                    if (<>c<TCol, TRow>.<>9__150_0 == null)
                    {
                        Func<ISummaryItemEx, bool> local1 = <>c<TCol, TRow>.<>9__150_0;
                        predicate = <>c<TCol, TRow>.<>9__150_0 = x => x.AlignByColumnInFooter;
                    }
                    this.allowExportSummaryItemsAlignByColumnsInFooter = new bool?(this.View.GridGroupSummaryItemCollection.Count<ISummaryItemEx>(predicate) > 0);
                }
                return this.allowExportSummaryItemsAlignByColumnsInFooter.Value;
            }
        }

        public int AlignByColumnsSummaryMaxCount
        {
            get
            {
                if (this.alignByColumnsSummaryMaxCount == -1)
                {
                    Func<ISummaryItemEx, bool> predicate = <>c<TCol, TRow>.<>9__153_0;
                    if (<>c<TCol, TRow>.<>9__153_0 == null)
                    {
                        Func<ISummaryItemEx, bool> local1 = <>c<TCol, TRow>.<>9__153_0;
                        predicate = <>c<TCol, TRow>.<>9__153_0 = x => x.AlignByColumnInFooter;
                    }
                    IEnumerable<ISummaryItemEx> collection = this.View.GridGroupSummaryItemCollection.Where<ISummaryItemEx>(predicate);
                    this.alignByColumnsSummaryMaxCount = this.HelpersProvider.SummaryExporter.FooterRowsCount(collection);
                }
                return this.alignByColumnsSummaryMaxCount;
            }
        }

        internal ExportHelpersProvider<TCol, TRow> HelpersProvider
        {
            get
            {
                this.provider ??= this.CreateHelpersProvider();
                return this.provider;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ExportInfo<TCol, TRow>.<>c <>9;
            public static Func<List<Group>> <>9__22_0;
            public static Func<Stack<DevExpress.Printing.ExportHelpers.XlGroup>> <>9__23_0;
            public static Func<ISummaryItemEx, bool> <>9__150_0;
            public static Func<ISummaryItemEx, bool> <>9__153_0;

            static <>c()
            {
                ExportInfo<TCol, TRow>.<>c.<>9 = new ExportInfo<TCol, TRow>.<>c();
            }

            internal List<Group> <CreateGroupsListInstance>b__22_0() => 
                new List<Group>();

            internal Stack<DevExpress.Printing.ExportHelpers.XlGroup> <CreateStackInstance>b__23_0() => 
                new Stack<DevExpress.Printing.ExportHelpers.XlGroup>();

            internal bool <get_AlignByColumnsSummaryMaxCount>b__153_0(ISummaryItemEx x) => 
                x.AlignByColumnInFooter;

            internal bool <get_AllowExportSummaryItemsAlignByColumnsInFooter>b__150_0(ISummaryItemEx x) => 
                x.AlignByColumnInFooter;
        }
    }
}

