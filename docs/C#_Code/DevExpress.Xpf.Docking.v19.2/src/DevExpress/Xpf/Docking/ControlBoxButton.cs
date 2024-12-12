namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Docking.VisualElements;
    using System;
    using System.Windows;

    public class ControlBoxButton : DevExpress.Xpf.Docking.VisualElements.ControlBoxButton
    {
        static ControlBoxButton()
        {
            new DependencyPropertyRegistrator<DevExpress.Xpf.Docking.ControlBoxButton>().OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
        }
    }
}

