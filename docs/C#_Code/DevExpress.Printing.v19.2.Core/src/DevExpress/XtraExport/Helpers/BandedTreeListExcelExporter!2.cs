namespace DevExpress.XtraExport.Helpers
{
    using DevExpress.Export;
    using DevExpress.Printing.ExportHelpers;
    using System;
    using System.Collections.Generic;

    public class BandedTreeListExcelExporter<TCol, TRow> : TreeListExcelExporter<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        public BandedTreeListExcelExporter(IGridView<TCol, TRow> viewToExport) : base(viewToExport)
        {
        }

        public BandedTreeListExcelExporter(IGridView<TCol, TRow> viewToExport, IDataAwareExportOptions options) : base(viewToExport, options)
        {
        }

        protected override DataAwareExportContext<TCol, TRow> CreateContext(ExportInfo<TCol, TRow> exportInfo) => 
            new TreeListDataAwareBandedExportContext<TCol, TRow>(exportInfo);

        internal override ExportInfo<TCol, TRow> CreateExportInfo() => 
            new TreeListBandedExportInfo<TCol, TRow>(this);

        protected override Action<TCol> ExportColumn() => 
            delegate (TCol gridColumn) {
                int columnGroupLevel = gridColumn.GetColumnGroupLevel();
                while (true)
                {
                    if (base.ExportInfo.GroupsStack.Count > 0)
                    {
                        XlGroup group = base.ExportInfo.GroupsStack.Peek();
                        if (group.Group.OutlineLevel >= (columnGroupLevel + 1))
                        {
                            base.ExportInfo.GroupsStack.Pop();
                            base.Exporter.EndGroup();
                            continue;
                        }
                    }
                    if (!gridColumn.IsGroupColumn || !base.ExportInfo.LinearBandsAndColumns)
                    {
                        base.Context.CreateColumn(gridColumn);
                        return;
                    }
                    base.ExportInfo.ColumnGrouping = true;
                    base.Context.CreateColumn(gridColumn);
                    base.Context.CreateExportDataGroup(columnGroupLevel, gridColumn.LogicalPosition, 0, gridColumn.IsCollapsed);
                    return;
                }
            };

        protected override void ExportColumns()
        {
            if (base.ExportInfo.LinearBandsAndColumns)
            {
                base.ForAllColumns(base.View, this.ExportColumn());
            }
            else
            {
                TCol parent = default(TCol);
                this.ForAllBandedColumns(parent, base.View, base.ExportColumn());
            }
            while (base.ExportInfo.GroupsStack.Count > 0)
            {
                base.ExportInfo.GroupsStack.Pop();
                base.ExportInfo.Exporter.EndGroup();
            }
            base.ExportInfo.GroupsStack.Clear();
        }

        protected override void ExportSheetHeader()
        {
            base.Context.SetExportSheetSettings();
            if (base.ExportInfo.ShowPagePrintTitles)
            {
                base.Context.SetPrintTitles();
            }
            if (base.ExportInfo.OptionsAllowSetFixedHeader)
            {
                base.Context.ConfigureHeader();
            }
            base.Context.CreateHeader();
        }

        protected void ForAllBandedColumns(TCol parent, IGridView<TCol, TRow> view, Action<TCol> action)
        {
            IEnumerable<IColumn> enumerable = ((parent == null) || !parent.IsGroupColumn) ? ((IEnumerable<IColumn>) this.GetColumnsCore(view)) : parent.GetAllColumns();
            foreach (TCol local in enumerable)
            {
                if (!local.IsGroupColumn)
                {
                    action(local);
                    continue;
                }
                this.ForAllBandedColumns(local, view, action);
            }
        }
    }
}

