namespace DevExpress.Xpf.Printing
{
    using System;
    using System.Runtime.CompilerServices;

    public class SimpleFaultEventArgs : EventArgs
    {
        public SimpleFaultEventArgs(Exception fault)
        {
            this.Fault = fault;
        }

        public Exception Fault { get; private set; }
    }
}

