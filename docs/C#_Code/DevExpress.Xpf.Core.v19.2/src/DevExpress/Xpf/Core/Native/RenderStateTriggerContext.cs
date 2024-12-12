namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public sealed class RenderStateTriggerContext : SettableRenderTriggerContextBase
    {
        public RenderStateTriggerContext(RenderStateTrigger factory, Namescope namescope);
        protected override void AttachOverride();
        protected override void DetachOverride();
        public override bool IsValid();

        public RenderStateGroupContext GroupContext { get; set; }

        private RenderStateTrigger Factory { get; }

        public string Name { get; }
    }
}

