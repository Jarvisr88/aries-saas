namespace DevExpress.XtraPrinting
{
    using System;
    using System.Runtime.CompilerServices;

    public class ExceptionEventArgs : EventArgs
    {
        public ExceptionEventArgs(System.Exception exception)
        {
            this.Exception = exception;
        }

        public System.Exception Exception { get; private set; }

        public bool Handled { get; set; }
    }
}

