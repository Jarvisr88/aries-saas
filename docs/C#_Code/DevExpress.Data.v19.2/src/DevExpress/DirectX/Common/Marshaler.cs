namespace DevExpress.DirectX.Common
{
    using System;
    using System.Security;

    public abstract class Marshaler : IDisposable
    {
        private IntPtr pointer;

        protected Marshaler()
        {
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        [SecuritySafeCritical]
        protected virtual void Dispose(bool disposing)
        {
            if (this.pointer != IntPtr.Zero)
            {
                this.FreePointer();
                this.pointer = IntPtr.Zero;
            }
        }

        ~Marshaler()
        {
            this.Dispose(false);
        }

        protected abstract void FreePointer();

        public IntPtr Pointer
        {
            get => 
                this.pointer;
            protected set => 
                this.pointer = value;
        }
    }
}

