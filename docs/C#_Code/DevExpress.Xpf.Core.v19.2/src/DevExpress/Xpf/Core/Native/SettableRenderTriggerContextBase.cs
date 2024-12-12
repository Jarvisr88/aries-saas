namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections.Generic;

    public abstract class SettableRenderTriggerContextBase : RenderTriggerContextBase
    {
        private IEnumerable<RenderSetterContext> setters;

        protected SettableRenderTriggerContextBase(SettableRenderTriggerBase factory, Namescope namescope);

        private SettableRenderTriggerBase Factory { get; }

        public override IEnumerable<RenderSetterContext> Setters { get; }
    }
}

