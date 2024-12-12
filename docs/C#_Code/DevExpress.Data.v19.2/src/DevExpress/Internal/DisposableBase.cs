namespace DevExpress.Internal
{
    using System;

    public abstract class DisposableBase : IDisposable
    {
        private bool disposed;

        protected DisposableBase()
        {
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!this.Disposed)
            {
                this.disposed = true;
                if (disposing)
                {
                    this.DisposeManaged();
                }
                this.DisposeUnmanaged();
            }
        }

        protected virtual void DisposeManaged()
        {
        }

        protected virtual void DisposeUnmanaged()
        {
        }

        ~DisposableBase()
        {
            this.Dispose(false);
        }

        public bool Disposed =>
            this.disposed;
    }
}

