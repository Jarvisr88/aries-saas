namespace DevExpress.XtraExport.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    internal class ClipboardData : IClipboardData
    {
        private int columnCount = -1;
        private List<IClipboardRow> rows = new List<IClipboardRow>();

        public void AddRow(IClipboardRow row)
        {
            if (row != null)
            {
                if ((row.Cells != null) && (row.Cells.Length > this.columnCount))
                {
                    this.columnCount = row.Cells.Length;
                }
                this.rows.Add(row);
            }
        }

        internal void Reset()
        {
            this.rows.Clear();
            this.columnCount = -1;
        }

        public int ColumnCount =>
            this.columnCount;

        public int RowCount =>
            this.rows.Count;

        public ReadOnlyCollection<IClipboardRow> Rows =>
            this.rows.AsReadOnly();
    }
}

