namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Markup;

    [ContentProperty("Children")]
    public class RenderPanel : FrameworkRenderElement
    {
        private DevExpress.Xpf.Core.Native.LayoutProvider layoutProvider;
        private FRElementCollection children;

        public RenderPanel();
        protected override Size ArrangeOverride(Size finalSize, IFrameworkRenderElementContext context);
        protected override FrameworkRenderElementContext CreateContextInstance();
        protected override IEnumerable<IFrameworkRenderElement> GetChildren();
        protected override void InitializeContext(FrameworkRenderElementContext context);
        protected override Size MeasureOverride(Size availableSize, IFrameworkRenderElementContext context);

        public DevExpress.Xpf.Core.Native.LayoutProvider LayoutProvider { get; set; }

        public FRElementCollection Children { get; }
    }
}

