namespace DevExpress.Xpf.Core.Native
{
    using System;

    public class MultiRenderTriggerContext : SettableRenderTriggerContextBase
    {
        private readonly RenderConditionGroupContext conditionGroupContext;

        public MultiRenderTriggerContext(MultiRenderTrigger factory, Namescope namescope);
        protected override void AttachOverride();
        protected override void DetachOverride();
        public override bool IsValid();
        protected override void ResetOverride(RenderValueSource valueSource);

        private MultiRenderTrigger Factory { get; }
    }
}

