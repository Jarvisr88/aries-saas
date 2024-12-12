namespace DevExpress.Xpf.Docking.VisualElements
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class PanelResizingEventArgs : RoutedEventArgs
    {
        public PanelResizingEventArgs(double size)
        {
            this.Size = size;
        }

        public double Size { get; private set; }
    }
}

