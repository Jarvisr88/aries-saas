namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Windows;

    internal interface IDraggableWindow
    {
        DevExpress.Xpf.Docking.FloatGroup FloatGroup { get; }

        bool IsDragging { get; set; }

        DockLayoutManager Manager { get; }

        bool SuspendDragging { get; set; }

        System.Windows.Window Window { get; }
    }
}

