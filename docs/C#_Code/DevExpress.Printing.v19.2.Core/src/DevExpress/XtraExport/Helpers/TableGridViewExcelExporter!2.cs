namespace DevExpress.XtraExport.Helpers
{
    using DevExpress.Export;
    using DevExpress.Printing.DataAwareExport.Export.TableExport;
    using DevExpress.Printing.ExportHelpers;
    using System;

    public class TableGridViewExcelExporter<TCol, TRow> : GridViewExcelExporter<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        public TableGridViewExcelExporter(IGridView<TCol, TRow> viewToExport) : base(viewToExport, DataAwareExportOptionsFactory.Create(ExportTarget.Xlsx))
        {
        }

        public TableGridViewExcelExporter(IGridView<TCol, TRow> viewToExport, IDataAwareExportOptions options) : base(viewToExport, options)
        {
        }

        protected override DataAwareExportContext<TCol, TRow> CreateContext(ExportInfo<TCol, TRow> exportInfo) => 
            new TableExportContext<TCol, TRow>(base.ExportInfo);

        internal override ExportInfo<TCol, TRow> CreateExportInfo() => 
            new TableExportInfo<TCol, TRow>(this);

        protected override Action<TCol> ExportColumn() => 
            delegate (TCol column) {
                base.Context.CreateColumn(column);
            };

        protected override void ExportData()
        {
            int exportRowIndex = base.ExportInfo.ExportRowIndex;
            this.ForAllRows(base.View, delegate (TRow gridRow) {
                if (!gridRow.IsGroupRow)
                {
                    base.Context.AddRow(gridRow);
                    base.ExportInfo.ReportProgress(base.ExportInfo.ExportRowIndex);
                }
            });
            this.AddGroupToList(exportRowIndex, base.ExportInfo.ExportRowIndex, -1);
            base.Context.CreateFooter();
            this.RunExporters();
            base.ExportInfo.CompleteReportProgress();
        }

        protected override void ExportSheetHeader()
        {
            if (base.ExportInfo.ShowColumnHeaders)
            {
                base.Context.CreateHeader();
            }
        }
    }
}

