namespace DevExpress.XtraExport.Implementation
{
    using DevExpress.Export.Xl;
    using DevExpress.Utils;
    using System;
    using System.Runtime.CompilerServices;

    internal class XlTableColumnReference : XlCellRange
    {
        public XlTableColumnReference(string tableName, string columnName, XlCellRange columnRange)
        {
            Guard.ArgumentIsNotNullOrEmpty(tableName, "tableName");
            Guard.ArgumentIsNotNullOrEmpty(columnName, "columnName");
            Guard.ArgumentNotNull(columnRange, "columnRange");
            this.TableName = tableName;
            this.ColumnName = columnName;
            base.TopLeft = columnRange.TopLeft;
            base.BottomRight = columnRange.BottomRight;
            base.SheetName = columnRange.SheetName;
        }

        public override string ToString() => 
            $"{this.TableName}[{XlTableReference.QuoteColumnName(this.ColumnName)}]";

        public string TableName { get; private set; }

        public string ColumnName { get; private set; }
    }
}

