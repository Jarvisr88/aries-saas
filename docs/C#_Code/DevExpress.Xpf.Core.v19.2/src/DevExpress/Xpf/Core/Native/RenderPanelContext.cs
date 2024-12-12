namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections.Generic;

    public class RenderPanelContext : FrameworkRenderElementContext
    {
        private readonly List<FrameworkRenderElementContext> children;
        private DevExpress.Xpf.Core.Native.LayoutProvider layoutProvider;

        public RenderPanelContext(FrameworkRenderElement factory);
        public override void AddChild(FrameworkRenderElementContext child);
        protected override FrameworkRenderElementContext GetRenderChild(int index);

        public DevExpress.Xpf.Core.Native.LayoutProvider LayoutProvider { get; set; }

        protected override int RenderChildrenCount { get; }
    }
}

