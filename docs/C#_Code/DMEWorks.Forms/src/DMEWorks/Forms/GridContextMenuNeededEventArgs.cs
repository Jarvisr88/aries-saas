namespace DMEWorks.Forms
{
    using System;
    using System.Windows.Forms;

    public class GridContextMenuNeededEventArgs : EventArgs
    {
        private ContextMenuStrip contextMenu;
        private DataGridViewRow row;

        public GridContextMenuNeededEventArgs(DataGridViewRow row)
        {
            if (row == null)
            {
                throw new ArgumentNullException("row");
            }
            this.row = row;
        }

        internal GridContextMenuNeededEventArgs(DataGridViewRow dataRow, ContextMenuStrip contextMenu) : this(dataRow)
        {
            this.contextMenu = contextMenu;
        }

        public ContextMenuStrip ContextMenu
        {
            get => 
                this.contextMenu;
            set => 
                this.contextMenu = value;
        }

        public DataGridViewRow Row =>
            this.row;
    }
}

