namespace DMEWorks.Forms
{
    using System;
    using System.Windows.Forms;

    public class GridDataErrorEventArgs : GridCellCancelEventArgs
    {
        private DataGridViewDataErrorContexts _context;
        private System.Exception _exception;
        private bool _throwException;

        public GridDataErrorEventArgs(DataGridViewRow row, DataGridViewColumn column, System.Exception exception, DataGridViewDataErrorContexts context) : base(row, column)
        {
            this._exception = exception;
            this._context = context;
        }

        public DataGridViewDataErrorContexts Context =>
            this._context;

        public System.Exception Exception =>
            this._exception;

        public bool ThrowException
        {
            get => 
                this._throwException;
            set => 
                this._throwException = value;
        }
    }
}

