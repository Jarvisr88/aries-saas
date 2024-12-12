namespace DevExpress.Utils.Controls
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public abstract class DisposableObject : IDisposable
    {
        private bool isDisposed;

        [Browsable(false)]
        public event EventHandler Disposed;

        protected DisposableObject()
        {
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            this.isDisposed = true;
            if (this.Disposed != null)
            {
                this.Disposed(this, EventArgs.Empty);
            }
        }

        ~DisposableObject()
        {
            this.Dispose(false);
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsDisposed =>
            this.isDisposed;
    }
}

