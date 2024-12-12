namespace DevExpress.Xpf.LayoutControl
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class LayoutControlInitNewElementEventArgs : EventArgs
    {
        public LayoutControlInitNewElementEventArgs(FrameworkElement element)
        {
            this.Element = element;
        }

        public FrameworkElement Element { get; private set; }
    }
}

