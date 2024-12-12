namespace DevExpress.Xpf.Core.Native
{
    using System;

    public class RenderDecoratorContext : FrameworkRenderElementContext
    {
        private FrameworkRenderElementContext child;

        public RenderDecoratorContext(RenderDecorator factory);
        public override void AddChild(FrameworkRenderElementContext child);
        protected override FrameworkRenderElementContext GetRenderChild(int index);

        public FrameworkRenderElementContext Child { get; }

        protected override int RenderChildrenCount { get; }
    }
}

