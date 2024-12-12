namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Windows;

    public interface IExtendedColumnChooserView
    {
        IDragElement CreateDragElement(BaseGridHeader columnHeader, Point offset);

        bool IsInScrollingMode { get; }
    }
}

