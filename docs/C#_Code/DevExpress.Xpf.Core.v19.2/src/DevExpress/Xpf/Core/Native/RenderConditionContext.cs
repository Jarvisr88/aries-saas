namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public class RenderConditionContext : RenderPropertyChangedListenerContext, IRenderConditionContext, IRenderPropertyContext
    {
        public RenderConditionContext(RenderCondition factory);
        protected override void AttachOverride(Namescope scope, RenderTriggerContextBase context);
        protected override void DetachOverride();
        protected override void InitializeDescriptorOverride();
        public override void PreviewValueChanged(object sender, EventArgs args);
        private void UpdateIsValid(object source);

        public RenderCondition Factory { get; }

        public object ConvertedValue { get; private set; }
    }
}

