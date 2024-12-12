namespace DevExpress.DirectX.NativeInterop.DXGI
{
    using DevExpress.DirectX.Common;
    using DevExpress.DirectX.NativeInterop;
    using System;

    public class DXGISurface1 : DXGISurface
    {
        public DXGISurface1(IntPtr nativeObject) : base(nativeObject)
        {
        }

        public IntPtr GetDC(bool discard)
        {
            IntPtr ptr;
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, MarshalBool(discard), out ptr, 11));
            return ptr;
        }

        public void ReleaseDC(RECT dirtyRect)
        {
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, ref dirtyRect, 12));
        }
    }
}

