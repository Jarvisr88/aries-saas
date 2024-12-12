namespace DevExpress.Xpf.Core.Native
{
    using System;

    public class RenderTriggerContext : SettableRenderTriggerContextBase
    {
        private readonly RenderConditionContext conditionContext;

        public RenderTriggerContext(RenderTrigger factory, Namescope namescope);
        protected override void AttachOverride();
        protected override void DetachOverride();
        public override bool IsValid();
        protected override void ResetOverride(RenderValueSource valueSource);

        public RenderTrigger Factory { get; }
    }
}

