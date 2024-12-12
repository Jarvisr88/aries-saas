namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public class RenderEventArgs : RenderEventArgsBase
    {
        public RenderEventArgs(IFrameworkRenderElementContext source, EventArgs originalEventArgs, RenderEvents renderEvent);
        protected internal override void InvokeEventHandler(IFrameworkRenderElementContext target);

        public RenderEvents RenderEvent { get; private set; }
    }
}

