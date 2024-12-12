namespace DevExpress.Xpf.Data
{
    using System;
    using System.Runtime.CompilerServices;

    public sealed class DisposeSourceEventArgs : EventArgs
    {
        internal DisposeSourceEventArgs(object source)
        {
            this.Source = source;
        }

        public object Source { get; private set; }
    }
}

