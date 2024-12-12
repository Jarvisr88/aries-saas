namespace DevExpress.DirectX.StandardInterop
{
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    public abstract class ComObject<T> : IDisposable where T: class
    {
        private readonly T comObject;
        private readonly bool shouldRelease;

        protected ComObject(T comObject) : this(comObject, true)
        {
            this.comObject = comObject;
        }

        protected ComObject(T comObject, bool shouldRelease)
        {
            this.comObject = comObject;
            this.shouldRelease = shouldRelease;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            this.Dispose(true);
        }

        [SecuritySafeCritical]
        protected virtual void Dispose(bool dispose)
        {
            if (this.shouldRelease)
            {
                Marshal.ReleaseComObject(this.comObject);
            }
        }

        ~ComObject()
        {
            this.Dispose(false);
        }

        public T WrappedObject =>
            this.comObject;
    }
}

