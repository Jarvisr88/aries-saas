namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public class RenderStateTrigger : SettableRenderTriggerBase
    {
        public override RenderTriggerContextBase CreateContext(Namescope namescope);

        public string Name { get; set; }
    }
}

