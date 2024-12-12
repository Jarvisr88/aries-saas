namespace DevExpress.Xpf.Printing
{
    using System;
    using System.Runtime.CompilerServices;

    public class ParametersMergingErrorEventArgs : EventArgs
    {
        public ParametersMergingErrorEventArgs(Exception error)
        {
            this.Error = error;
        }

        public Exception Error { get; private set; }
    }
}

