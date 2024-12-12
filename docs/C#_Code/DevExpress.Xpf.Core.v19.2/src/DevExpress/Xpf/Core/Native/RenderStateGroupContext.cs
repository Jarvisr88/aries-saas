namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class RenderStateGroupContext : RenderTriggerContextBase
    {
        private Dictionary<string, RenderStateTriggerContext> stateContexts;
        private IEnumerable<RenderSetterContext> setters;

        public RenderStateGroupContext(RenderStateGroup factory, Namescope namescope);
        protected override void AttachOverride();
        protected virtual void CreateStateContexts(Namescope scope);
        protected override void DetachOverride();
        public void GoToState(string stateName);
        public override bool IsValid();

        private RenderStateGroup Factory { get; }

        public string ActiveStateName { get; private set; }

        public RenderStateTriggerContext ActiveState { get; private set; }

        public override IEnumerable<RenderSetterContext> Setters { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly RenderStateGroupContext.<>c <>9;
            public static Action<KeyValuePair<string, RenderStateTriggerContext>> <>9__15_0;

            static <>c();
            internal void <DetachOverride>b__15_0(KeyValuePair<string, RenderStateTriggerContext> x);
        }
    }
}

