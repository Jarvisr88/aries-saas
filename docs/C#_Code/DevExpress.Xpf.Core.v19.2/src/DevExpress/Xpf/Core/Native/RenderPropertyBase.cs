namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.ComponentModel;

    [Browsable(false)]
    public abstract class RenderPropertyBase
    {
        protected RenderPropertyBase();
        public abstract RenderPropertyContextBase CreateContext();
    }
}

