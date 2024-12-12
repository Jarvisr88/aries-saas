namespace DevExpress.Xpf.Core.Native
{
    using System;

    public class RenderBindingContext : RenderBindingBaseContext
    {
        public RenderBindingContext(RenderBinding factory, Namescope namescope);
        public override bool IsValid();
    }
}

