namespace DevExpress.XtraExport.Helpers
{
    using DevExpress.Export;
    using DevExpress.Printing.ExportHelpers;
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class GridViewExcelExporter<TCol, TRow> : BaseViewExcelExporter<IGridView<TCol, TRow>> where TCol: class, IColumn where TRow: class, IRowBase
    {
        private ExportInfo<TCol, TRow> exportInfo;
        private DataAwareExportContext<TCol, TRow> context;

        public GridViewExcelExporter(IGridView<TCol, TRow> viewToExport) : base(viewToExport, DataAwareExportOptionsFactory.Create(ExportTarget.Xlsx))
        {
        }

        public GridViewExcelExporter(IGridView<TCol, TRow> viewToExport, IDataAwareExportOptions options) : base(viewToExport, options)
        {
        }

        protected virtual void AddGroupToList(int startIndex, int endIndex, int groupLevel = -1)
        {
            Group item = new Group {
                Start = startIndex,
                End = endIndex,
                GroupLevel = groupLevel
            };
            this.ExportInfo.GroupsList.Add(item);
        }

        private void CalcRanges()
        {
            int exportRowIndex = this.ExportInfo.ExportRowIndex;
            int endExcelGroupIndex = 0;
            int groupId = 1;
            this.ForAllRows(base.View, delegate (TRow gridRow) {
                int rowLevel = gridRow.GetRowLevel();
                bool flag = false;
                while (true)
                {
                    int num3;
                    if (((GridViewExcelExporter<TCol, TRow>) this).ExportInfo.GroupsStack.Count > 0)
                    {
                        XlGroup group2 = ((GridViewExcelExporter<TCol, TRow>) this).ExportInfo.GroupsStack.Peek();
                        if (group2.Group.OutlineLevel >= (rowLevel + 1))
                        {
                            XlGroup group = ((GridViewExcelExporter<TCol, TRow>) this).ExportInfo.GroupsStack.Peek();
                            if (!flag)
                            {
                                endExcelGroupIndex = exportRowIndex;
                            }
                            if (((GridViewExcelExporter<TCol, TRow>) this).ExportInfo.AllowExportSummaryItemsAlignByColumnsInFooter)
                            {
                                Func<ISummaryItemEx, bool> predicate = <>c<TCol, TRow>.<>9__24_1;
                                if (<>c<TCol, TRow>.<>9__24_1 == null)
                                {
                                    Func<ISummaryItemEx, bool> local1 = <>c<TCol, TRow>.<>9__24_1;
                                    predicate = <>c<TCol, TRow>.<>9__24_1 = x => x.AlignByColumnInFooter;
                                }
                                List<ISummaryItemEx> collection = ((GridViewExcelExporter<TCol, TRow>) this).View.GridGroupSummaryItemCollection.Where<ISummaryItemEx>(predicate).ToList<ISummaryItemEx>();
                                int num2 = ((GridViewExcelExporter<TCol, TRow>) this).ExportInfo.HelpersProvider.SummaryExporter.FooterRowCountForGroup(collection, group.GroupRowLevel, group.GroupRowHandle);
                                exportRowIndex += num2;
                            }
                            ((GridViewExcelExporter<TCol, TRow>) this).CloseGroupAndAddToList(endExcelGroupIndex);
                            flag = true;
                            continue;
                        }
                    }
                    if (gridRow.IsGroupRow && ((GridViewExcelExporter<TCol, TRow>) this).ExportInfo.AllowGroupingRows)
                    {
                        num3 = exportRowIndex;
                        exportRowIndex = num3 + 1;
                        num3 = groupId;
                        groupId = num3 + 1;
                        ((GridViewExcelExporter<TCol, TRow>) this).Context.CreateExportDataGroup(rowLevel, gridRow.LogicalPosition, exportRowIndex, num3, false, true);
                    }
                    if (((GridViewExcelExporter<TCol, TRow>) this).CanExportRow(gridRow))
                    {
                        num3 = exportRowIndex;
                        exportRowIndex = num3 + 1;
                    }
                    return;
                }
            });
            while (this.ExportInfo.GroupsStack.Count > 0)
            {
                this.CloseGroupAndAddToList(exportRowIndex);
            }
        }

        protected virtual bool CanExportRow(IRowBase row) => 
            !row.IsGroupRow && this.ExportInfo.ComplyWithFormatLimits();

        protected virtual void CloseAllOpenGroups()
        {
            while (this.ExportInfo.GroupsStack.Count > 0)
            {
                this.CloseGroup(this.ExportInfo.ExportRowIndex);
            }
        }

        protected virtual void CloseGroup(int endExcelGroupIndex)
        {
            int startGroup = this.ExportInfo.GroupsStack.Peek().StartGroup;
            if (this.ExportInfo.GroupsStack.Peek().DataRanges.Count == 0)
            {
                Group item = new Group {
                    Start = startGroup,
                    End = endExcelGroupIndex
                };
                this.ExportInfo.GroupsStack.Peek().DataRanges.Add(item);
            }
            this.Context.SetGroupSummary(this.ExportInfo.GroupsStack.Peek());
            List<Group> dataRanges = this.ExportInfo.GroupsStack.Peek().DataRanges;
            this.ExportInfo.GroupsStack.Pop();
            this.ExportInfo.Exporter.EndGroup();
            if (this.ExportInfo.GroupsStack.Count > 0)
            {
                this.ExportInfo.GroupsStack.Peek().DataRanges.AddRange(dataRanges);
            }
        }

        private void CloseGroupAndAddToList(int endExcelGroupIndex)
        {
            XlGroup item = this.ExportInfo.GroupsStack.Peek();
            int startGroup = item.StartGroup;
            if (item.DataRanges.Count == 0)
            {
                Group group2 = new Group {
                    Start = startGroup,
                    End = endExcelGroupIndex
                };
                item.DataRanges.Add(group2);
            }
            this.ExportInfo.PrecalculatedGroupsList.Add(item);
            List<Group> dataRanges = item.DataRanges;
            this.ExportInfo.GroupsStack.Pop();
            this.ExportInfo.Exporter.EndGroup();
            if (this.ExportInfo.GroupsStack.Count > 0)
            {
                this.ExportInfo.GroupsStack.Peek().DataRanges.AddRange(dataRanges);
            }
        }

        protected virtual int CompleteGrouping(TRow lastExportedRow, int endExcelGroupIndex, int currentRowLevel)
        {
            bool flag = false;
            while (true)
            {
                if (this.ExportInfo.GroupsStack.Count > 0)
                {
                    XlGroup group = this.ExportInfo.GroupsStack.Peek();
                    if (group.Group.OutlineLevel >= (currentRowLevel + 1))
                    {
                        if (!flag)
                        {
                            endExcelGroupIndex = this.ExportInfo.ExportRowIndex;
                        }
                        this.CompleteMergingInGroup(lastExportedRow, endExcelGroupIndex);
                        this.CloseGroup(endExcelGroupIndex);
                        flag = true;
                        continue;
                    }
                }
                return endExcelGroupIndex;
            }
        }

        protected void CompleteMerging(TRow lastRow, int endExcelGroupIndex)
        {
            if (this.ExportInfo.AllowCellMerge)
            {
                this.ExportInfo.HelpersProvider.CellMerger.MergedLastCells(lastRow, endExcelGroupIndex);
            }
        }

        protected void CompleteMergingInGroup(TRow lastExportedRow, int endExcelGroupIndex)
        {
            if (this.ExportInfo.AllowCellMerge)
            {
                this.ExportInfo.HelpersProvider.CellMerger.CompleteMergingInGroup(lastExportedRow, endExcelGroupIndex);
            }
        }

        protected virtual DataAwareExportContext<TCol, TRow> CreateContext(ExportInfo<TCol, TRow> exportInfo) => 
            new DataAwareExportContext<TCol, TRow>(exportInfo);

        internal virtual ExportInfo<TCol, TRow> CreateExportInfo() => 
            new ExportInfo<TCol, TRow>((GridViewExcelExporter<TCol, TRow>) this);

        protected virtual Action<TCol> ExportColumn() => 
            delegate (TCol column) {
                base.Context.CreateColumn(column);
            };

        protected virtual void ExportColumns()
        {
            this.ForAllColumns(base.View, this.ExportColumn());
        }

        protected virtual void ExportData()
        {
            this.OnStartFiltering();
            if (!this.ExportInfo.AlignGroupSummaryInGroupRow)
            {
                this.ExportDataDefaultMode();
            }
            else
            {
                this.CalcRanges();
                this.ExportDataDefaultMode();
            }
            this.OnEndFiltering();
        }

        protected virtual int ExportDataCore(ref TRow lastExportedRow, ref bool wasDataRow, ref int endExcelGroupIndex, ref int groupId, TRow gridRow, int startExcelGroupIndex)
        {
            lastExportedRow = gridRow;
            int rowLevel = gridRow.GetRowLevel();
            endExcelGroupIndex = this.CompleteGrouping(lastExportedRow, endExcelGroupIndex, rowLevel);
            this.ExportStartGroup(ref wasDataRow, startExcelGroupIndex, endExcelGroupIndex, ref groupId, gridRow, rowLevel);
            if (this.CanExportRow(gridRow))
            {
                if (!wasDataRow)
                {
                    startExcelGroupIndex = this.ExportInfo.ExportRowIndex;
                }
                this.Context.AddRow(gridRow);
                wasDataRow = true;
            }
            this.ExportInfo.ReportProgress(this.ExportInfo.ExportRowIndex);
            return startExcelGroupIndex;
        }

        private void ExportDataDefaultMode()
        {
            TRow lastExportedRow = default(TRow);
            bool wasDataRow = false;
            int startExcelGroupIndex = this.ExportInfo.ExportRowIndex;
            int endExcelGroupIndex = 0;
            int groupId = 1;
            this.ForAllRows(base.View, gridRow => startExcelGroupIndex = ((GridViewExcelExporter<TCol, TRow>) this).ExportDataCore(ref lastExportedRow, ref wasDataRow, ref endExcelGroupIndex, ref groupId, gridRow, startExcelGroupIndex));
            this.AddGroupToList(startExcelGroupIndex, this.ExportInfo.ExportRowIndex, -1);
            this.CloseAllOpenGroups();
            if (this.ExportInfo.OptionsAllowAddAutoFilter)
            {
                this.Context.AddAutoFilter();
            }
            this.CompleteMerging(lastExportedRow, this.ExportInfo.ExportRowIndex);
            this.Context.CreateFooter();
            this.RunExporters();
            this.ExportInfo.CompleteReportProgress();
            this.ExportInfo.ClearFormatInfo();
        }

        protected override bool ExportOverride()
        {
            this.ExportColumns();
            this.ExportSheetHeader();
            this.ExportData();
            return this.ExportInfo.EndSheetFlag;
        }

        protected virtual void ExportSheetHeader()
        {
            this.Context.SetExportSheetSettings();
            if (this.ExportInfo.ShowPagePrintTitles)
            {
                this.Context.SetPrintTitles();
            }
            if (this.ExportInfo.OptionsAllowSetFixedHeader)
            {
                this.Context.ConfigureHeader();
            }
            if (this.ExportInfo.ShowColumnHeaders)
            {
                this.Context.CreateHeader();
            }
        }

        protected virtual void ExportStartGroup(ref bool wasDataRow, int startExcelGroupIndex, int endExcelGroupIndex, ref int groupId, TRow gridRow, int currentRowLevel)
        {
            if (gridRow.IsGroupRow && this.ExportInfo.AllowGroupingRows)
            {
                this.ExportInfo.HelpersProvider.CellMerger.Clear();
                if (wasDataRow)
                {
                    if ((endExcelGroupIndex == 0) && (startExcelGroupIndex > 0))
                    {
                        endExcelGroupIndex = this.ExportInfo.ExportRowIndex;
                    }
                    this.AddGroupToList(startExcelGroupIndex, endExcelGroupIndex - 1, -1);
                }
                IGroupRow<TRow> groupRow = gridRow as IGroupRow<TRow>;
                if (this.ExportInfo.ComplyWithFormatLimits())
                {
                    this.Context.PrintGroupRowHeader(gridRow);
                }
                int num = groupId;
                groupId = num + 1;
                this.Context.CreateExportDataGroup(currentRowLevel, gridRow.LogicalPosition, num, this.IsCollapsed(groupRow), this.GetShowGroupFooter(groupRow));
                wasDataRow = false;
            }
        }

        public void ForAllColumns(IGridView<TCol, TRow> view, Action<TCol> action)
        {
            TCol parent = default(TCol);
            this.ForAllColumns(parent, view, action);
        }

        protected void ForAllColumns(IEnumerable<TCol> gridcolumns, Action<TCol> action)
        {
            foreach (TCol local in gridcolumns)
            {
                action(local);
            }
        }

        public void ForAllColumns(TCol parent, IGridView<TCol, TRow> view, Action<TCol> action)
        {
            IEnumerable<IColumn> enumerable = ((parent == null) || !parent.IsGroupColumn) ? ((IEnumerable<IColumn>) this.GetColumnsCore(view)) : parent.GetAllColumns();
            foreach (TCol local in enumerable)
            {
                action(local);
                if (local.IsGroupColumn)
                {
                    this.ForAllColumns(local, view, action);
                }
            }
        }

        internal virtual void ForAllRows(IGridView<TCol, TRow> view, Action<TRow> action)
        {
            this.ForAllRowsCore(null, view, action);
        }

        protected void ForAllRowsCore(IGroupRow<TRow> parent, IGridView<TCol, TRow> view, Action<TRow> action)
        {
            IEnumerable<TRow> enumerable = (parent != null) ? parent.GetAllRows() : this.GetRowsCore(view);
            foreach (TRow local in enumerable)
            {
                action(local);
                if (view.IsCancelPending)
                {
                    break;
                }
                IGroupRow<TRow> row = local as IGroupRow<TRow>;
                if (row != null)
                {
                    this.ForAllRowsCore(row, view, action);
                }
            }
        }

        protected virtual IEnumerable<TCol> GetColumnsCore(IGridView<TCol, TRow> view) => 
            view.GetAllColumns();

        protected int GetLastRowIndex(int endExcelGroupIndex) => 
            (endExcelGroupIndex == 0) ? this.ExportInfo.ExportRowIndex : endExcelGroupIndex;

        protected virtual IEnumerable<TRow> GetRowsCore(IGridView<TCol, TRow> view) => 
            view.GetAllRows();

        private bool GetShowGroupFooter(IGroupRow<TRow> groupRow) => 
            ((this.ExportInfo.Options.ShowGroupSummaries == DefaultBoolean.True) & this.ExportInfo.HelpersProvider.SummaryExporter.AllowExportGroupSummary) && ((groupRow != null) && groupRow.ShowGroupFooter);

        protected bool IsCollapsed(IGroupRow<TRow> groupRow) => 
            !this.ExportInfo.ApplyColumnFilteringEventSettings ? (((groupRow == null) || (base.Options.GroupState != GroupState.Default)) ? (base.Options.GroupState == GroupState.CollapseAll) : groupRow.IsCollapsed) : false;

        private void OnEndFiltering()
        {
            if (this.ExportInfo.ApplyColumnFilteringEventSettings)
            {
                base.Sheet.EndFiltering();
            }
        }

        private void OnStartFiltering()
        {
            if (this.ExportInfo.ApplyColumnFilteringEventSettings)
            {
                base.Sheet.BeginFiltering(base.Sheet.DataRange);
            }
        }

        protected virtual void RunExporters()
        {
            this.ExportInfo.HelpersProvider.Run();
        }

        protected internal ExportInfo<TCol, TRow> ExportInfo
        {
            get
            {
                this.exportInfo ??= this.CreateExportInfo();
                return this.exportInfo;
            }
        }

        internal DataAwareExportContext<TCol, TRow> Context
        {
            get
            {
                this.context ??= this.CreateContext(this.ExportInfo);
                return this.context;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly GridViewExcelExporter<TCol, TRow>.<>c <>9;
            public static Func<ISummaryItemEx, bool> <>9__24_1;

            static <>c()
            {
                GridViewExcelExporter<TCol, TRow>.<>c.<>9 = new GridViewExcelExporter<TCol, TRow>.<>c();
            }

            internal bool <CalcRanges>b__24_1(ISummaryItemEx x) => 
                x.AlignByColumnInFooter;
        }
    }
}

