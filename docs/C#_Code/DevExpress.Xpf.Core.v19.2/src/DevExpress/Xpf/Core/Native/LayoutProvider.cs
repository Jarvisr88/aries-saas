namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Windows;
    using System.Windows.Markup;

    public abstract class LayoutProvider : MarkupExtension
    {
        public static readonly LayoutProvider GridInstance;
        public static readonly LayoutProvider DockInstance;
        public static readonly LayoutProvider StackInstance;
        public static readonly LayoutProvider CanvasInstance;

        static LayoutProvider();
        protected LayoutProvider();
        public abstract Size ArrangeOverride(Size finalSize, IFrameworkRenderElementContext context);
        public abstract Size MeasureOverride(Size availableSize, IFrameworkRenderElementContext context);
        public override object ProvideValue(IServiceProvider serviceProvider);
    }
}

