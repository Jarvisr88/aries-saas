namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows;

    public class RenderContentPresenter : RenderDecorator
    {
        private bool allowVisualTree;
        private bool preferRenderTemplate;

        public RenderContentPresenter();
        protected override FrameworkRenderElementContext CreateContextInstance();
        protected virtual RenderTemplate GetActualRenderTemplate(RenderContentPresenterContext cpContext, out bool useTemplate, out bool? forceVisual, out DataTemplate defaultTemplate, object content);
        protected override void InitializeContext(FrameworkRenderElementContext context);
        protected override void PreApplyTemplate(FrameworkRenderElementContext context);
        public bool ShouldUpdateTemplate(RenderContentPresenterContext context, object oldValue, object newValue);

        public bool PreferRenderTemplate { get; set; }

        public bool AllowVisualTree { get; set; }
    }
}

