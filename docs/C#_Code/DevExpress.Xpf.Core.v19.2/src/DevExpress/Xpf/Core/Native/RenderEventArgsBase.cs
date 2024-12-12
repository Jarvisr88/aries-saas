namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public class RenderEventArgsBase : EventArgs
    {
        protected RenderEventArgsBase(IFrameworkRenderElementContext source, EventArgs originalEventArgs);
        protected internal virtual void InvokeEventHandler(IFrameworkRenderElementContext target);

        public bool Handled { get; set; }

        public IFrameworkRenderElementContext Source { get; private set; }

        public EventArgs OriginalEventArgs { get; private set; }
    }
}

