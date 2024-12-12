namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class RenderTrigger : SettableRenderTriggerBase
    {
        public RenderTrigger();
        public override RenderTriggerContextBase CreateContext(Namescope namescope);

        public string Property { get; set; }

        public System.Windows.DependencyProperty DependencyProperty { get; set; }

        public object Value { get; set; }

        public string TargetName { get; set; }

        public RenderValueSource ValueSource { get; set; }

        public string SourceName { get; set; }

        public RenderCondition Condition { get; private set; }
    }
}

