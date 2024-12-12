namespace DevExpress.Xpf.Core.Native
{
    using System;

    public class ConditionalRenderBindingContext : RenderBindingBaseContext
    {
        private readonly RenderConditionGroupContext innerConditionsContext;

        public ConditionalRenderBindingContext(ConditionalRenderBinding factory, Namescope namescope);
        public override bool IsValid();

        protected RenderConditionGroupContext InnerConditionsContext { get; }

        private ConditionalRenderBinding Factory { get; }
    }
}

