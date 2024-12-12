namespace DevExpress.Xpf.Core.Native
{
    using System;

    public class MultiRenderTrigger : SettableRenderTriggerBase
    {
        private readonly RenderConditionGroup conditionGroup;

        public MultiRenderTrigger();
        public override RenderTriggerContextBase CreateContext(Namescope namescope);

        public RenderConditionCollection Conditions { get; }

        protected internal RenderConditionGroup ConditionGroup { get; }
    }
}

