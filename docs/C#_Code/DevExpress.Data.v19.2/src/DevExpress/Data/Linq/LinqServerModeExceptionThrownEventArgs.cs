namespace DevExpress.Data.Linq
{
    using System;

    public class LinqServerModeExceptionThrownEventArgs : EventArgs
    {
        private System.Exception _Exception;

        public LinqServerModeExceptionThrownEventArgs(System.Exception exception);

        public System.Exception Exception { get; }
    }
}

