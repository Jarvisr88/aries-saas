namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public sealed class DragRecordOverEventArgs : DragEventArgsBase
    {
        public DragRecordOverEventArgs(RoutedEvent routedEvent, object source) : base(routedEvent, source)
        {
        }

        public double DropPositionRelativeCoefficient { get; internal set; }
    }
}

