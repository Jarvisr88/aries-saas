namespace DevExpress.DirectX.NativeInterop.Direct2D
{
    using DevExpress.DirectX.Common;
    using DevExpress.DirectX.NativeInterop;
    using System;

    public class D2D1DCRenderTarget : D2D1RenderTarget
    {
        public D2D1DCRenderTarget(IntPtr nativeObject) : base(nativeObject)
        {
        }

        public void BindDC(IntPtr hDC, RECT pSubRect)
        {
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, hDC, ref pSubRect, 0x39));
        }
    }
}

