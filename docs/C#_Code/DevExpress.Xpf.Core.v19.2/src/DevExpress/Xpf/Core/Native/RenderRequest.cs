namespace DevExpress.Xpf.Core.Native
{
    using System;

    public class RenderRequest
    {
        public FrameworkRenderElementContext Target;
        public RenderRequest Next;
        public RenderRequest Prev;
    }
}

