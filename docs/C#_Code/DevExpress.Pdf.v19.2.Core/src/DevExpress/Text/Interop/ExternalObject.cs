namespace DevExpress.Text.Interop
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public abstract class ExternalObject : IDisposable
    {
        private readonly bool ownsNativeObject;

        protected ExternalObject(IntPtr nativeObject, bool ownsNativeObject = true)
        {
            this.NativeObject = nativeObject;
            this.ownsNativeObject = ownsNativeObject;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (this.ownsNativeObject && (this.NativeObject != IntPtr.Zero))
            {
                this.DisposeCore(disposing);
                this.NativeObject = IntPtr.Zero;
            }
        }

        protected abstract void DisposeCore(bool disposing);
        ~ExternalObject()
        {
            this.Dispose(false);
        }

        public IntPtr NativeObject { get; private set; }
    }
}

