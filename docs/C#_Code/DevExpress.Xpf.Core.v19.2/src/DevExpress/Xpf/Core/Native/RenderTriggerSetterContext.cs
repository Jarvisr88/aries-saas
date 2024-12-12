namespace DevExpress.Xpf.Core.Native
{
    using System;

    public class RenderTriggerSetterContext : RenderSetterContext
    {
        public RenderTriggerSetterContext(RenderSetter factory, SettableRenderTriggerContextBase owner, Namescope namescope, IElementHost elementHost);
        protected override object GetConvertedValue(FrameworkRenderElementContext context);

        private RenderSetter Factory { get; }

        public override string TargetName { get; }

        public override string Property { get; }
    }
}

