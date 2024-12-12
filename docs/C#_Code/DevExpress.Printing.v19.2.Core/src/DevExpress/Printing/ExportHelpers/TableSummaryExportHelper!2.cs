namespace DevExpress.Printing.ExportHelpers
{
    using DevExpress.Data;
    using DevExpress.Export;
    using DevExpress.Export.Xl;
    using DevExpress.Printing.DataAwareExport.Export.TableExport;
    using DevExpress.Printing.DataAwareExport.Export.Utils;
    using DevExpress.Utils;
    using DevExpress.XtraExport.Helpers;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;

    internal class TableSummaryExportHelper<TCol, TRow> : SummaryExportHelper<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        private TableExportContext<TCol, TRow> tableExportContext;
        private TableExportInfo<TCol, TRow> tableExportInfo;

        public TableSummaryExportHelper(ExportInfo<TCol, TRow> exportInfo) : base(exportInfo)
        {
            this.tableExportContext = exportInfo.Helper.Context as TableExportContext<TCol, TRow>;
            this.tableExportInfo = exportInfo as TableExportInfo<TCol, TRow>;
            Guard.ArgumentNotNull(this.tableExportContext, "TableExportContext");
        }

        private XlTotalRowFunction ConvertFunction(SummaryItemType itemSummaryType)
        {
            switch (itemSummaryType)
            {
                case SummaryItemType.Sum:
                    return XlTotalRowFunction.Sum;

                case SummaryItemType.Min:
                    return XlTotalRowFunction.Min;

                case SummaryItemType.Max:
                    return XlTotalRowFunction.Max;

                case SummaryItemType.Count:
                    return XlTotalRowFunction.Count;

                case SummaryItemType.Average:
                    return XlTotalRowFunction.Average;
            }
            return XlTotalRowFunction.None;
        }

        private CustomizeTableSummaryCellInfo<TCol, TRow> CreateCellInfo(IXlTableColumn tableColumn, ISummaryItemEx item, TCol column) => 
            new CustomizeTableSummaryCellInfo<TCol, TRow> { 
                AreaType = SheetAreaType.TotalFooter,
                TableColumn = tableColumn,
                Item = item,
                ItemEx = CustomizationEventsUtils<TCol, TRow>.GetExportedItem(item),
                View = base.ExportInfo.View,
                Column = column,
                ExportRowIndex = base.ExportInfo.ExportRowIndex,
                Options = base.ExportInfo.Options
            };

        private void EndTable(bool hasTotalRow)
        {
            using (IXlRow row = base.ExportInfo.Sheet.CreateRow())
            {
                row.EndTable(this.tableExportContext.Table, hasTotalRow);
            }
        }

        public override void ExportTotalSummary()
        {
            if (!this.tableExportInfo.UseTableTotalFooter)
            {
                base.ExportTotalSummary();
            }
            else if (base.AllowExportTotalSummary && (base.ExportInfo.ExportRowIndex != this.GetStartRangePosition()))
            {
                Func<ISummaryItemEx, bool> predicate = <>c<TCol, TRow>.<>9__3_0;
                if (<>c<TCol, TRow>.<>9__3_0 == null)
                {
                    Func<ISummaryItemEx, bool> local1 = <>c<TCol, TRow>.<>9__3_0;
                    predicate = <>c<TCol, TRow>.<>9__3_0 = x => x.SummaryType != SummaryItemType.None;
                }
                ISummaryItemEx[] source = base.ExportInfo.View.GridTotalSummaryItemCollection.Where<ISummaryItemEx>(predicate).ToArray<ISummaryItemEx>();
                foreach (TCol column in base.ExportInfo.GridColumns)
                {
                    if (column != null)
                    {
                        ISummaryItemEx item = source.FirstOrDefault<ISummaryItemEx>(x => (x.ShowInColumnFooterName == x.FieldName) && (x.ShowInColumnFooterName == column.FieldName));
                        if (item != null)
                        {
                            IXlTableColumn tableColumn = this.tableExportContext.Table.Columns[column.VisibleIndex];
                            tableColumn.TotalRowFunction = this.ConvertFunction(item.SummaryType);
                            if (base.ExportInfo.Options.CanRaiseCustomizeCellEvent)
                            {
                                CustomizationEventsUtils<TCol, TRow>.OnCustomizeTableFooterCell(this.CreateCellInfo(tableColumn, item, column), base.ExportInfo);
                                continue;
                            }
                            tableColumn.TotalRowFormatting = new XlDifferentialFormatting();
                            tableColumn.TotalRowFormatting.NetFormatString = item.DisplayFormat;
                        }
                    }
                }
                this.EndTable(true);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TableSummaryExportHelper<TCol, TRow>.<>c <>9;
            public static Func<ISummaryItemEx, bool> <>9__3_0;

            static <>c()
            {
                TableSummaryExportHelper<TCol, TRow>.<>c.<>9 = new TableSummaryExportHelper<TCol, TRow>.<>c();
            }

            internal bool <ExportTotalSummary>b__3_0(ISummaryItemEx x) => 
                x.SummaryType != SummaryItemType.None;
        }
    }
}

