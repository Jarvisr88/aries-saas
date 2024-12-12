namespace DevExpress.Data
{
    using System;

    public class ControllerRowCellExceptionEventArgs : ControllerRowExceptionEventArgs
    {
        private int column;

        public ControllerRowCellExceptionEventArgs(int controllerRow, int column, object row, Exception exception);

        public int Column { get; }
    }
}

