namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public abstract class RenderPropertyContextBase : IRenderPropertyContext
    {
        protected RenderPropertyContextBase(RenderPropertyBase factory);
        public void Attach(DevExpress.Xpf.Core.Native.Namescope scope, RenderTriggerContextBase context);
        protected abstract void AttachOverride(DevExpress.Xpf.Core.Native.Namescope scope, RenderTriggerContextBase context);
        public void Detach();
        protected abstract void DetachOverride();
        public void Reset(RenderValueSource valueSource);
        protected abstract void ResetOverride(RenderValueSource valueSource);

        private DevExpress.Xpf.Core.Native.Namescope NamescopeHolder { get; set; }

        public RenderPropertyBase Factory { get; private set; }

        public IElementHost ElementHost { get; }

        public INamescope Namescope { get; }

        public IPropertyChangedListener PropertyChangedListener { get; }

        public RenderTriggerContextBase TriggerContext { get; private set; }

        protected bool IsAttached { get; private set; }
    }
}

