namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Markup;

    [ContentProperty("Conditions")]
    public class ConditionalRenderBinding : RenderBindingBase
    {
        public ConditionalRenderBinding();
        public override RenderTriggerContextBase CreateContext(Namescope namescope);

        public RenderConditionCollection Conditions { get; }

        protected internal RenderConditionGroup InnerConditions { get; set; }
    }
}

