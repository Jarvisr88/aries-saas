namespace DevExpress.Xpf.Core
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class TabControlTabAddingEventArgs : CancelEventArgs
    {
        public object Item { get; set; }
    }
}

