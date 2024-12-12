namespace DevExpress.Xpf.Printing.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public class DocumentPrinterCompletedEventArgs : EventArgs
    {
        public DocumentPrinterCompletedEventArgs(Exception fault)
        {
            this.Fault = fault;
        }

        public Exception Fault { get; private set; }
    }
}

