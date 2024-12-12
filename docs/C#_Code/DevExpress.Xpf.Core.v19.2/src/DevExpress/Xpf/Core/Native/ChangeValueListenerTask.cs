namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public class ChangeValueListenerTask
    {
        private bool isValid;

        public ChangeValueListenerTask(RenderPropertyChangedListenerContext context);
        public void Execute();
        public void Invalidate();

        public RenderPropertyChangedListenerContext Context { get; private set; }
    }
}

