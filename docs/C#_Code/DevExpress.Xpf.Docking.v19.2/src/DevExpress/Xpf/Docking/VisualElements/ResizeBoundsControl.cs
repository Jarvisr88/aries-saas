namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class ResizeBoundsControl : Control
    {
        static ResizeBoundsControl()
        {
            new DependencyPropertyRegistrator<ResizeBoundsControl>().OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
        }
    }
}

