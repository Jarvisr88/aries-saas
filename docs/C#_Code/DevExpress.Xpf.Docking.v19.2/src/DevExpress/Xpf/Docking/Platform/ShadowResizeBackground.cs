namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.VisualElements;
    using System;
    using System.Windows;

    public class ShadowResizeBackground : psvControl
    {
        static ShadowResizeBackground()
        {
            new DependencyPropertyRegistrator<ShadowResizeBackground>().OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
        }
    }
}

