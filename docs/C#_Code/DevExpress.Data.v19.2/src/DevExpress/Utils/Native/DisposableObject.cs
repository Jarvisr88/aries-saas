namespace DevExpress.Utils.Native
{
    using System;

    public abstract class DisposableObject : IDisposable
    {
        private bool isDisposed;

        protected DisposableObject()
        {
        }

        public void Dispose()
        {
            if (!this.isDisposed)
            {
                this.Dispose(true);
                this.isDisposed = true;
                GC.SuppressFinalize(this);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
        }

        ~DisposableObject()
        {
            this.Dispose(false);
        }
    }
}

