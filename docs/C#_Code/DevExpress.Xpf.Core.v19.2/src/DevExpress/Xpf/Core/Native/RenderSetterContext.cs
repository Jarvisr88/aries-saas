namespace DevExpress.Xpf.Core.Native
{
    using System;

    public abstract class RenderSetterContext
    {
        protected readonly IRenderSetterFactory factory;
        private readonly Namescope namescope;
        private readonly IElementHost elementHost;
        public readonly RenderTriggerContextBase owner;

        public RenderSetterContext(IRenderSetterFactory factory, Namescope namescope, IElementHost elementHost, RenderTriggerContextBase owner);
        private bool CheckContext(FrameworkRenderElementContext context);
        protected abstract object GetConvertedValue(FrameworkRenderElementContext context);
        public void SetValue(FrameworkRenderElementContext context);

        public abstract string TargetName { get; }

        public abstract string Property { get; }
    }
}

