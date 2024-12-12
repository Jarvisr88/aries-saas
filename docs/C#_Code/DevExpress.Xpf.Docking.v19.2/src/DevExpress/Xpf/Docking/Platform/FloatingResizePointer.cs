namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.VisualElements;
    using System;
    using System.Windows;

    public class FloatingResizePointer : psvControl
    {
        static FloatingResizePointer()
        {
            new DependencyPropertyRegistrator<FloatingResizePointer>().OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
        }
    }
}

