namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Security;

    [SecuritySafeCritical]
    public class DisposableObject : IDisposable
    {
        private EventHandler disposing;

        public event EventHandler Disposing;

        public void Dispose();
        protected void Dispose(bool disposing);
        protected virtual void DisposeManagedResources();
        protected virtual void DisposeNativeResources();
        protected override void Finalize();
        protected void ThrowIfDisposed();

        public bool IsDisposed { get; set; }
    }
}

