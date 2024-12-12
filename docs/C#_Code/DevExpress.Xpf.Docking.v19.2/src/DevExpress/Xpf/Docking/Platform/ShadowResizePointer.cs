namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.VisualElements;
    using System;
    using System.Windows;

    public class ShadowResizePointer : psvControl
    {
        static ShadowResizePointer()
        {
            new DependencyPropertyRegistrator<ShadowResizePointer>().OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
        }
    }
}

