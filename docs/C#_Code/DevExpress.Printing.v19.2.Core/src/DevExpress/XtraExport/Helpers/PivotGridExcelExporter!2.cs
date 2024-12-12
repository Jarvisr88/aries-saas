namespace DevExpress.XtraExport.Helpers
{
    using DevExpress.Export;
    using DevExpress.Printing.ExportHelpers;
    using System;
    using System.Runtime.InteropServices;

    public class PivotGridExcelExporter<TCol, TRow> : GridViewExcelExporter<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        public PivotGridExcelExporter(IGridView<TCol, TRow> viewToExport) : base(viewToExport)
        {
        }

        public PivotGridExcelExporter(IGridView<TCol, TRow> viewToExport, IDataAwareExportOptions options) : base(viewToExport, options)
        {
        }

        protected override void AddGroupToList(int startIndex, int endIndex, int groupLevel = -1)
        {
            startIndex ??= (startIndex + base.ExportInfo.View.FixedRowsCount);
            base.AddGroupToList(startIndex, endIndex, groupLevel);
        }

        protected override DataAwareExportContext<TCol, TRow> CreateContext(ExportInfo<TCol, TRow> exportInfo) => 
            new PivotDataAwareExportContext<TCol, TRow>(base.ExportInfo);

        internal override ExportInfo<TCol, TRow> CreateExportInfo() => 
            new PivotDataAwareExportInfo<TCol, TRow>(this);

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
                    if (!gridColumn.IsGroupColumn || !base.ExportInfo.AllowGrouping)
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
    }
}

