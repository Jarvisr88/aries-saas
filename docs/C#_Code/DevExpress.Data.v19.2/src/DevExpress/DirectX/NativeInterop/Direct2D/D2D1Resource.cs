namespace DevExpress.DirectX.NativeInterop.Direct2D
{
    using DevExpress.DirectX.NativeInterop;
    using System;

    public class D2D1Resource : ComObject
    {
        public D2D1Resource(IntPtr nativeObject) : base(nativeObject)
        {
        }

        public D2D1Factory GetFactory()
        {
            IntPtr ptr;
            ComObject.InvokeHelper.Calli(base.NativeObject, out ptr, 3);
            return new D2D1Factory(ptr);
        }
    }
}

