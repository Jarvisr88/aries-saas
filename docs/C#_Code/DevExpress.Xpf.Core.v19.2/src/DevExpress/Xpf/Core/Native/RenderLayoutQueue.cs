namespace DevExpress.Xpf.Core.Native
{
    using System;

    public abstract class RenderLayoutQueue
    {
        private RenderRequest head;
        private RenderRequest pocket;
        private int pocketSize;
        private const int PocketCapacity = 0x99;
        private const int PocketReserve = 8;

        protected RenderLayoutQueue();
        private void _addRequest(FrameworkRenderElementContext e);
        private RenderRequest _getNewRequest(FrameworkRenderElementContext e);
        private void _removeRequest(RenderRequest entry);
        public void Add(FrameworkRenderElementContext context);
        protected abstract bool canRelyOnChromeRecalc(FrameworkRenderElementContext context);
        protected abstract bool canRelyOnParentRecalc(FrameworkRenderElementContext parent);
        protected abstract RenderRequest getRequest(FrameworkRenderElementContext context);
        internal FrameworkRenderElementContext GetTopMost();
        protected abstract void invalidate(FrameworkRenderElementContext e);
        internal void Remove(FrameworkRenderElementContext e);
        internal void RemoveOrphans(FrameworkRenderElementContext parent);
        private void ReuseRequest(RenderRequest r);
        protected abstract void setRequest(FrameworkRenderElementContext e, RenderRequest r);

        internal bool IsEmpty { get; }
    }
}

