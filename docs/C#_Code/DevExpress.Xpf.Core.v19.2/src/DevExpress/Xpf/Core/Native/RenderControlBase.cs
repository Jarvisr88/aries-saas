namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Windows;
    using System.Windows.Media;

    public abstract class RenderControlBase : FrameworkRenderElement
    {
        private static readonly Func<UIElement, bool> GetNeverMeasured;

        static RenderControlBase();
        protected RenderControlBase();
        protected override Size ArrangeOverride(Size finalSize, IFrameworkRenderElementContext context);
        protected virtual bool CalcNeverMeasured(FrameworkElement control);
        protected abstract FrameworkElement CreateFrameworkElement(FrameworkRenderElementContext context);
        protected override void InitializeContext(FrameworkRenderElementContext context);
        private void InitializeNamescope(FrameworkRenderElementContext context, FrameworkElement control);
        protected override Size MeasureOverride(Size availableSize, IFrameworkRenderElementContext context);
        protected override void RenderOverride(DrawingContext dc, IFrameworkRenderElementContext context);
    }
}

