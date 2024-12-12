namespace DevExpress.DirectX.NativeInterop
{
    using DevExpress.DirectX.Common;
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    public abstract class ComObject : IDisposable
    {
        private const int E_NOINTERFACE = -2147467262;
        private static IInvokeHelper invokeHelper = DynamicAssemblyHelper.CreateInvokeHelper();
        private IntPtr nativeObject;

        protected ComObject(IntPtr nativeObject)
        {
            this.nativeObject = nativeObject;
        }

        [SecuritySafeCritical]
        protected void AddRef()
        {
            Marshal.AddRef(this.NativeObject);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            this.Dispose(true);
        }

        [SecuritySafeCritical]
        protected virtual void Dispose(bool dispose)
        {
            if (this.nativeObject != IntPtr.Zero)
            {
                Marshal.Release(this.nativeObject);
                this.nativeObject = IntPtr.Zero;
            }
        }

        ~ComObject()
        {
            this.Dispose(false);
        }

        protected static int MarshalBool(bool value) => 
            value ? 1 : 0;

        protected IntPtr QueryInterface<T>() => 
            this.QueryInterface<T>(true).Value;

        [SecuritySafeCritical]
        protected IntPtr? QueryInterface<T>(bool throwIfNoInterface)
        {
            IntPtr ptr;
            using (ArrayMarshaler marshaler = new ArrayMarshaler(typeof(T).GUID.ToByteArray()))
            {
                int hResult = InvokeHelper.CalliInt(this.NativeObject, marshaler.Pointer, out ptr, 0);
                if ((hResult != -2147467262) || throwIfNoInterface)
                {
                    InteropHelpers.CheckHResult(hResult);
                }
                else
                {
                    return null;
                }
            }
            return new IntPtr?(ptr);
        }

        internal static IInvokeHelper InvokeHelper =>
            invokeHelper;

        internal IntPtr NativeObject
        {
            get
            {
                if (this.nativeObject == IntPtr.Zero)
                {
                    throw new NullReferenceException();
                }
                return this.nativeObject;
            }
        }
    }
}

