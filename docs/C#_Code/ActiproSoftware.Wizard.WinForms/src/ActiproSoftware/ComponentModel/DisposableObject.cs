namespace ActiproSoftware.ComponentModel
{
    using #H;
    using ActiproSoftware.Products.Shared;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;

    [Serializable]
    public abstract class DisposableObject : MarshalByRefObject, IDisposable
    {
        private bool isDisposed;

        public event EventHandler Disposed;

        private void #bwe(EventArgs #yhb)
        {
            if (this.Disposed != null)
            {
                this.Disposed(this, #yhb);
            }
        }

        private void #cwe(bool #Fee)
        {
            this.isDisposed = true;
            this.Dispose(#Fee);
            if (#Fee)
            {
                this.#bwe(EventArgs.Empty);
            }
        }

        public void Dispose()
        {
            this.#cwe(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }

        ~DisposableObject()
        {
            this.#cwe(false);
        }

        public void VerifyNotDisposed()
        {
            if (this.isDisposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName, string.Format(ActiproSoftware.Products.Shared.SR.GetString(#G.#eg(0x259d)), base.GetType().FullName, this.ToString()));
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsDisposed =>
            this.isDisposed;
    }
}

