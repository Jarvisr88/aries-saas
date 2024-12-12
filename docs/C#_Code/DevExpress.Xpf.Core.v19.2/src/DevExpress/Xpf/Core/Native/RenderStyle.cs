namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Markup;

    [ContentProperty("Setters")]
    public class RenderStyle : FreezableRenderObject
    {
        private RenderStyle basedOn;
        private RenderStyleSettersCollection setters;

        public RenderStyle();
        public RenderStyle(RenderStyleTarget target);
        protected override void FreezeOverride();

        public RenderStyle BasedOn { get; set; }

        public RenderStyleSettersCollection Setters { get; set; }

        public RenderStyleTarget Target { get; set; }
    }
}

