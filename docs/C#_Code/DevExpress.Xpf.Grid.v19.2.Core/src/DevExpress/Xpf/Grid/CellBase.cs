namespace DevExpress.Xpf.Grid
{
    using System;

    public class CellBase
    {
        private int rowHandle;
        private ColumnBase column;

        public CellBase(int rowHandle, ColumnBase column)
        {
            this.column = column;
            this.rowHandle = rowHandle;
        }

        public bool Equals(CellBase gridCell) => 
            (gridCell != null) && ((gridCell.RowHandleCore == this.rowHandle) && ReferenceEquals(gridCell.ColumnCore, this.column));

        protected internal int RowHandleCore =>
            this.rowHandle;

        protected internal ColumnBase ColumnCore =>
            this.column;
    }
}

