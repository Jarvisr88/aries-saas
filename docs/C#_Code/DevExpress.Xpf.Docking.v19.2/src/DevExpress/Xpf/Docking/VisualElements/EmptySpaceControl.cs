namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Windows;

    public class EmptySpaceControl : psvContentControl
    {
        static EmptySpaceControl()
        {
            new DependencyPropertyRegistrator<EmptySpaceControl>().OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
        }

        protected override void OnDispose()
        {
            base.ClearValue(DockLayoutManager.DockLayoutManagerProperty);
            base.ClearValue(DockLayoutManager.LayoutItemProperty);
            base.OnDispose();
        }
    }
}

