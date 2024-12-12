namespace DevExpress.Xpf.Core.Native
{
    using System;

    public class RenderButtonBorder : RenderControl
    {
        private Dock? placement;

        protected override FrameworkRenderElementContext CreateContextInstance();
        protected override void InitializeContext(FrameworkRenderElementContext context);

        public Dock? Placement { get; set; }
    }
}

