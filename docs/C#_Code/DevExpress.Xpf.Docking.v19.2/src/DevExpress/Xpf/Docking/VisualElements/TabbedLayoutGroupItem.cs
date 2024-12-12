namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Docking;
    using System;
    using System.Windows;

    [DXToolboxBrowsable(false)]
    public class TabbedLayoutGroupItem : TabbedPaneItem
    {
        static TabbedLayoutGroupItem()
        {
            new DependencyPropertyRegistrator<TabbedLayoutGroupItem>().OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
        }

        protected override CaptionLocation DefaultCaptionLocation =>
            CaptionLocation.Top;

        protected override bool TransformBorderThickness =>
            false;
    }
}

