namespace DevExpress.Xpf.Printing.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class PageVisualSizeChangedEventArgs : EventArgs
    {
        public PageVisualSizeChangedEventArgs(Size size)
        {
            this.PageVisualSize = size;
        }

        public Size PageVisualSize { get; private set; }
    }
}

