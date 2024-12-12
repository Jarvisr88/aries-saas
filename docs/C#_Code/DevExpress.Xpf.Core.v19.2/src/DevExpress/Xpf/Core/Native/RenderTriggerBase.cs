namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public abstract class RenderTriggerBase
    {
        protected RenderTriggerBase();
        public abstract RenderTriggerContextBase CreateContext(DevExpress.Xpf.Core.Native.Namescope namescope);

        public IElementHost ElementHost { get; private set; }

        public INamescope Namescope { get; private set; }
    }
}

