namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public abstract class RenderTriggerContextBase
    {
        protected RenderTriggerContextBase(RenderTriggerBase factory, DevExpress.Xpf.Core.Native.Namescope namescope);
        public void Attach();
        protected abstract void AttachOverride();
        public void Detach();
        protected abstract void DetachOverride();
        public void Invalidate();
        public abstract bool IsValid();
        public void Reset(RenderValueSource valueSource);
        protected virtual void ResetOverride(RenderValueSource valueSource);

        public RenderTriggerBase Factory { get; private set; }

        public IElementHost ElementHost { get; private set; }

        public INamescope Namescope { get; private set; }

        public IPropertyChangedListener Listener { get; private set; }

        protected bool IsAttached { get; private set; }

        public abstract IEnumerable<RenderSetterContext> Setters { get; }
    }
}

