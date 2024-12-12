namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class ExpandCollapseInfoEventArgs : EventArgs
    {
        public System.Windows.Size Size { get; set; }

        public DXExpander Expander { get; set; }
    }
}

