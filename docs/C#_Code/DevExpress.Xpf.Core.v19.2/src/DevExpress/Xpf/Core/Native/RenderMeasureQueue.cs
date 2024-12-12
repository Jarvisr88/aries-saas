namespace DevExpress.Xpf.Core.Native
{
    using System;

    public class RenderMeasureQueue : RenderLayoutQueue
    {
        protected override bool canRelyOnChromeRecalc(FrameworkRenderElementContext context);
        protected override bool canRelyOnParentRecalc(FrameworkRenderElementContext parent);
        protected override RenderRequest getRequest(FrameworkRenderElementContext e);
        protected override void invalidate(FrameworkRenderElementContext e);
        protected override void setRequest(FrameworkRenderElementContext e, RenderRequest r);
    }
}

