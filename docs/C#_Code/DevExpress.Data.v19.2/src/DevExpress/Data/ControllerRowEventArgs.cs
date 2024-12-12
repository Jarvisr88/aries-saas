namespace DevExpress.Data
{
    using System;

    public class ControllerRowEventArgs : EventArgs
    {
        private int rowHandle;
        private object row;

        public ControllerRowEventArgs(int rowHandle, object row);

        public int RowHandle { get; }

        public object Row { get; }
    }
}

