namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Markup;

    [ContentProperty("States")]
    public class RenderStateGroup : RenderTriggerBase
    {
        private readonly RenderStateTriggerCollection states;

        public RenderStateGroup();
        public override RenderTriggerContextBase CreateContext(Namescope namescope);
        protected bool Equals(RenderStateGroup other);
        public override bool Equals(object obj);
        public override int GetHashCode();

        public string Name { get; set; }

        public RenderStateTriggerCollection States { get; }
    }
}

