namespace DevExpress.Xpf.Printing
{
    using System;
    using System.Runtime.CompilerServices;

    public class FaultEventArgs : SimpleFaultEventArgs
    {
        public FaultEventArgs(Exception fault) : base(fault)
        {
        }

        public bool Handled { get; set; }
    }
}

