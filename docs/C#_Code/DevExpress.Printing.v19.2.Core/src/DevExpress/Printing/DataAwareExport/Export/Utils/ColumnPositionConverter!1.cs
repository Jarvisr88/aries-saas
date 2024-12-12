namespace DevExpress.Printing.DataAwareExport.Export.Utils
{
    using DevExpress.Export.Xl;
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;

    public class ColumnPositionConverter<TCol> : IXlColumnPositionConverter where TCol: class, IColumn
    {
        private Dictionary<string, int> columnPositionTable;
        private Dictionary<string, TCol> columnTable;

        public ColumnPositionConverter(Dictionary<string, int> columnPositionTable)
        {
            Guard.ArgumentNotNull(columnPositionTable, "columnTable");
            this.columnPositionTable = columnPositionTable;
        }

        public ColumnPositionConverter(Dictionary<string, TCol> columnTable)
        {
            Guard.ArgumentNotNull(columnTable, "columnTable");
            this.columnTable = columnTable;
        }

        public TCol GetColumnByFieldName(string fieldName)
        {
            TCol local;
            if ((this.columnTable != null) && this.columnTable.TryGetValue(fieldName, out local))
            {
                return local;
            }
            return default(TCol);
        }

        public int GetColumnIndex(string name)
        {
            int num;
            return (((this.columnPositionTable == null) || !this.columnPositionTable.TryGetValue(name, out num)) ? -1 : num);
        }

        public virtual int GetRowOffset(string columnName) => 
            0;
    }
}

