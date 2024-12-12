namespace DevExpress.Data.Helpers
{
    using System;
    using System.Runtime.CompilerServices;

    public class ErrorEventArgs : EventArgs
    {
        public ErrorEventArgs(Exception error);

        public Exception Error { get; set; }
    }
}

