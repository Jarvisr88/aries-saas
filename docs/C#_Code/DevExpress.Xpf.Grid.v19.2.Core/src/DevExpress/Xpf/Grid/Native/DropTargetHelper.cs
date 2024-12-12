namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;
    using System.Windows;

    internal static class DropTargetHelper
    {
        internal static BaseColumn GetColumnFromDragSource(UIElement source)
        {
            BaseGridHeader header = (BaseGridHeader) source;
            return (header.DraggedColumn ?? header.Column);
        }
    }
}

