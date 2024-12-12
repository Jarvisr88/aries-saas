namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Windows;

    public class BaseLocationStrategy
    {
        public virtual Point GetMousePosition(IndependentMouseEventArgs e, UIElement relativeTo);
        public virtual Point GetPosition(FrameworkElement element, FrameworkElement relativeTo);
    }
}

