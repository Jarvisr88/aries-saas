namespace DevExpress.Printing.DataAwareExport.Export.Utils
{
    using DevExpress.Export;
    using DevExpress.Export.Xl;
    using DevExpress.Printing.ExportHelpers;
    using DevExpress.Utils;
    using DevExpress.XtraExport.Helpers;
    using System;
    using System.Runtime.CompilerServices;

    internal class CustomizeCellInfo<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        protected void AssignRowSettings(DataAwareEventArgsBase e, IRowBase row, int exportRowIndex, int rowHandle)
        {
            e.DocumentRow = exportRowIndex;
            if (row != null)
            {
                e.RowHandle = row.LogicalPosition;
                e.DataSourceRowIndex = row.DataSourceRowIndex;
            }
            else
            {
                e.DataSourceRowIndex = -1;
                e.RowHandle = rowHandle;
            }
        }

        public virtual CustomizeCellEventArgsExtended CreateEventArgs()
        {
            XlFormattingObject obj2 = new XlFormattingObject();
            CustomizeCellEventArgsExtended e = new CustomizeCellEventArgsExtended();
            if (this.Column != null)
            {
                obj2.CopyFrom(this.Column.Appearance, FormatType.Custom);
                e.AreaType = this.AreaType;
                e.Column = this.Column;
                e.Row = this.Row;
                e.ColumnFieldName = this.Column.FieldName;
                e.DataSourceOwner = this.View;
                e.Formatting = obj2;
                e.Hyperlink = this.Hyperlink;
                e.Value = this.CellValue;
                this.AssignRowSettings(e, this.Row, this.ExportRowIndex, this.RowHandle);
            }
            return e;
        }

        protected virtual CellObject GetExportedCellObject(TCol gridColumn) => 
            null;

        public SheetAreaType AreaType { get; set; }

        public IXlCell Cell { get; set; }

        public CellObject Cellobj { get; set; }

        public IGridView<TCol, TRow> View { get; set; }

        public TCol Column { get; set; }

        public TRow Row { get; set; }

        public int RowHandle { get; set; }

        public int ExportRowIndex { get; set; }

        public IDataAwareExportOptions Options { get; set; }

        public string Hyperlink { get; set; }

        public object CellValue { get; set; }
    }
}

