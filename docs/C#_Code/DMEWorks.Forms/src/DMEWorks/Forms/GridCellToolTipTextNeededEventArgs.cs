namespace DMEWorks.Forms
{
    using System;
    using System.Windows.Forms;

    public class GridCellToolTipTextNeededEventArgs : GridCellEventArgs
    {
        private string _toolTipText;

        public GridCellToolTipTextNeededEventArgs(DataGridViewRow row, DataGridViewColumn column, string toolTipText) : base(row, column)
        {
            this._toolTipText = toolTipText;
        }

        public string ToolTipText
        {
            get => 
                this._toolTipText;
            set => 
                this._toolTipText = value;
        }
    }
}

