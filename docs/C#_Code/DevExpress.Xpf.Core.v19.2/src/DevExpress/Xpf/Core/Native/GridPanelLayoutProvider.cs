namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Windows;

    public class GridPanelLayoutProvider : LayoutProvider
    {
        public override Size ArrangeOverride(Size finalSize, IFrameworkRenderElementContext context);
        private Dock? GetDock(FrameworkRenderElementContext element);
        public override Size MeasureOverride(Size availableSize, IFrameworkRenderElementContext context);
        public override object ProvideValue(IServiceProvider serviceProvider);
    }
}

