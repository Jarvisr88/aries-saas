namespace DMEWorks.Forms
{
    using System;
    using System.Windows.Forms;

    public class GridCellEventArgs : EventArgs
    {
        private DataGridViewRow _row;
        private DataGridViewColumn _column;

        public GridCellEventArgs(DataGridViewRow row, DataGridViewColumn column)
        {
            if (row == null)
            {
                DataGridViewRow local1 = row;
                throw new ArgumentNullException("row");
            }
            this._row = row;
            if (column == null)
            {
                DataGridViewColumn local2 = column;
                throw new ArgumentNullException("column");
            }
            this._column = column;
        }

        public DataGridViewRow Row =>
            this._row;

        public DataGridViewColumn Column =>
            this._column;
    }
}

