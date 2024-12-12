namespace DevExpress.Xpf.Core.Native
{
    using System.Windows;

    public class CanvasLayoutProvider : LayoutProvider
    {
        public override Size ArrangeOverride(Size finalSize, IFrameworkRenderElementContext context);
        public override Size MeasureOverride(Size availableSize, IFrameworkRenderElementContext context);
    }
}

