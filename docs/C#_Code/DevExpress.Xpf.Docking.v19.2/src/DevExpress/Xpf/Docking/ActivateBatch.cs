namespace DevExpress.Xpf.Docking
{
    using System;

    internal class ActivateBatch : IDisposable
    {
        private DockLayoutManager Container;

        public ActivateBatch(DockLayoutManager container)
        {
            this.Container = container;
            if (this.Container != null)
            {
                this.Container.LockActivation();
            }
        }

        public void Dispose()
        {
            if (this.Container != null)
            {
                this.Container.UnlockActivation();
            }
            this.Container = null;
        }
    }
}

