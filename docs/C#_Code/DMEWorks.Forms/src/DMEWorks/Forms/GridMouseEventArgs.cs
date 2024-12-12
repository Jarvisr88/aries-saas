namespace DMEWorks.Forms
{
    using System;
    using System.Windows.Forms;

    public class GridMouseEventArgs : MouseEventArgs
    {
        private DataGridViewRow row;

        public GridMouseEventArgs(DataGridViewRow row, MouseEventArgs e) : base(e.Button, e.Clicks, e.X, e.Y, e.Delta)
        {
            if (row == null)
            {
                throw new ArgumentNullException("row");
            }
            this.row = row;
        }

        public DataGridViewRow Row =>
            this.row;
    }
}

