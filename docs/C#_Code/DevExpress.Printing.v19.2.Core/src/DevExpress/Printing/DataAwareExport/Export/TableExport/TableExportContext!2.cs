namespace DevExpress.Printing.DataAwareExport.Export.TableExport
{
    using DevExpress.Export;
    using DevExpress.Export.Xl;
    using DevExpress.Printing.DataAwareExport.Export.Utils;
    using DevExpress.Printing.ExportHelpers;
    using DevExpress.Utils;
    using DevExpress.XtraExport.Implementation;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal class TableExportContext<TCol, TRow> : DataAwareExportContext<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        private TableExportInfo<TCol, TRow> tableExportInfo;
        private List<XlTableColumnInfo> columns;

        public TableExportContext(ExportInfo<TCol, TRow> exportInfo) : base(exportInfo)
        {
            this.tableExportInfo = exportInfo as TableExportInfo<TCol, TRow>;
        }

        public override void AddAutoFilter()
        {
        }

        public override void CreateColumn(TCol column)
        {
            base.CreateColumn(column);
            XlTableColumnInfo item = new XlTableColumnInfo(string.IsNullOrEmpty(column.Header) ? column.FieldName : column.Header);
            this.TableColumns.Add(item);
        }

        public override void CreateHeader()
        {
            using (this.tableExportInfo.Sheet.CreateRow())
            {
                this.Table = this.CreateTable();
                this.Table.HasAutoFilter = this.tableExportInfo.OptionsAllowAddAutoFilter;
                this.tableExportInfo.RaiseBeforeExportTable(this.Table);
                this.tableExportInfo.ExportRowIndex++;
            }
        }

        private IXlTable CreateTable()
        {
            Guard.ArgumentNotNull(this.TableColumns, "columns");
            int uniqueColumnsCount = this.GetUniqueColumnsCount(this.columns);
            Guard.ArgumentPositive(uniqueColumnsCount, "Columns have to contain at least one item.");
            IXlTableContainer currentSheet = this.tableExportInfo.Exporter.CurrentSheet as IXlTableContainer;
            if (currentSheet == null)
            {
                return null;
            }
            XlCellRange range = new XlCellRange(new XlCellPosition(this.tableExportInfo.Exporter.CurrentColumnIndex, this.tableExportInfo.Exporter.CurrentRowIndex), new XlCellPosition((this.tableExportInfo.Exporter.CurrentColumnIndex + uniqueColumnsCount) - 1, this.tableExportInfo.Exporter.CurrentRowIndex));
            if (!currentSheet.IsValidRange(range) || currentSheet.HasIntersectionWithTable(range))
            {
                throw new InvalidOperationException("Cannot create a table in the specified position.");
            }
            IXlTableRepository currentDocument = this.tableExportInfo.Exporter.CurrentDocument as IXlTableRepository;
            XlTable table = new XlTable(currentDocument, range, this.columns, true) {
                HeaderRowFormatting = null
            };
            if (currentDocument != null)
            {
                table.Name = currentDocument.GetUniqueTableName();
            }
            currentSheet.AddTable(table);
            for (int i = 0; i < this.tableExportInfo.ExportProviders.Count; i++)
            {
                IColumnExportProvider<TRow> provider = this.tableExportInfo.ExportProviders[i];
                IXlCell cell = this.tableExportInfo.Exporter.BeginCell();
                TRow gridRow = default(TRow);
                provider.ExportValue(SheetAreaType.Header, cell, gridRow, this.tableExportInfo.ExportRowIndex);
                this.tableExportInfo.Exporter.EndCell();
            }
            return table;
        }

        private int GetUniqueColumnsCount(List<XlTableColumnInfo> tableColumns)
        {
            HashSet<string> set = new HashSet<string>();
            for (int i = 0; i < tableColumns.Count; i++)
            {
                XlTableColumnInfo info = tableColumns[i];
                if (set.Contains(info.Name))
                {
                    throw new ArgumentException("Column names must be unique!");
                }
                set.Add(info.Name);
            }
            return set.Count;
        }

        internal IXlTable Table { get; set; }

        internal List<XlTableColumnInfo> TableColumns
        {
            get
            {
                this.columns ??= new List<XlTableColumnInfo>();
                return this.columns;
            }
        }
    }
}

