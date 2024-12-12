namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Markup;

    [ContentProperty("Setters")]
    public abstract class SettableRenderTriggerBase : RenderTriggerBase
    {
        protected SettableRenderTriggerBase();

        public RenderSetterCollection Setters { get; private set; }
    }
}

