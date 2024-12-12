namespace DevExpress.DirectX.NativeInterop.Direct2D
{
    using DevExpress.DirectX.Common;
    using DevExpress.DirectX.NativeInterop;
    using System;

    public class D2D1CommandList : D2D1Image
    {
        public D2D1CommandList(IntPtr nativeObject) : base(nativeObject)
        {
        }

        public void Close()
        {
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, 5));
        }
    }
}

