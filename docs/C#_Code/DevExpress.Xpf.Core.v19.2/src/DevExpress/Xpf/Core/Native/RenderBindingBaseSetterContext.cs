namespace DevExpress.Xpf.Core.Native
{
    using System;

    public class RenderBindingBaseSetterContext : RenderSetterContext
    {
        public RenderBindingBaseSetterContext(RenderBindingBaseContext factory, Namescope namescope, IElementHost elementHost);
        protected override object GetConvertedValue(FrameworkRenderElementContext context);

        private RenderBindingBaseContext Factory { get; }

        public override string TargetName { get; }

        public override string Property { get; }
    }
}

