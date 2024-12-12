namespace DevExpress.Data
{
    using System;

    public class InternalExceptionEventArgs : EventArgs
    {
        private System.Exception exception;

        public InternalExceptionEventArgs(System.Exception e);

        public System.Exception Exception { get; }
    }
}

