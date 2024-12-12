namespace DevExpress.Xpf.Printing.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class PageLocationChangedEventArgs : EventArgs
    {
        public PageLocationChangedEventArgs(Point location)
        {
            this.PageLocation = location;
        }

        public Point PageLocation { get; private set; }
    }
}

