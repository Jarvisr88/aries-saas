namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections.Generic;

    public abstract class RenderBindingBaseContext : RenderTriggerContextBase, IRenderSetterFactory
    {
        private readonly RenderConditionGroupContext conditionContext;
        private readonly RenderPropertyChangedListenerContext propertyListenerContext;
        private IEnumerable<RenderSetterContext> setters;

        protected RenderBindingBaseContext(RenderBindingBase factory, Namescope namescope);
        protected override void AttachOverride();
        protected override void DetachOverride();
        protected override void ResetOverride(RenderValueSource valueSource);

        public RenderBindingBase Factory { get; }

        protected RenderConditionGroupContext RootContext { get; }

        public RenderPropertyChangedListenerContext PropertyListenerContext { get; }

        public override IEnumerable<RenderSetterContext> Setters { get; }

        bool IRenderSetterFactory.ThrowIfInvalidTargetName { get; }
    }
}

