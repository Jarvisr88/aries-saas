namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public class ErrorsChangedEventArgs : EventArgs
    {
        public ErrorsChangedEventArgs(int index);

        public int Index { get; private set; }
    }
}

