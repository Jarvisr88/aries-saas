namespace DMEWorks.Forms
{
    using System;
    using System.Windows.Forms;

    public class GridCellFormattingEventArgs : ConvertEventArgs
    {
        private DataGridViewRow _row;
        private DataGridViewColumn _column;
        private DataGridViewCellStyle _cellStyle;
        private bool _formattingApplied;

        public GridCellFormattingEventArgs(DataGridViewRow row, DataGridViewColumn column, DataGridViewCellStyle cellStyle, object value, System.Type desiredType) : base(value, desiredType)
        {
            if (row == null)
            {
                throw new ArgumentNullException("row");
            }
            if (column == null)
            {
                throw new ArgumentNullException("column");
            }
            this._row = row;
            this._column = column;
            this._cellStyle = cellStyle;
        }

        public DataGridViewRow Row =>
            this._row;

        public DataGridViewColumn Column =>
            this._column;

        public DataGridViewCellStyle CellStyle
        {
            get => 
                this._cellStyle;
            set => 
                this._cellStyle = value;
        }

        public bool FormattingApplied
        {
            get => 
                this._formattingApplied;
            set => 
                this._formattingApplied = value;
        }
    }
}

