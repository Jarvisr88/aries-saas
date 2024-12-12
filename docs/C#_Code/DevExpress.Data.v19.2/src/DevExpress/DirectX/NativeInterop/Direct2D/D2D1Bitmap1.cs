namespace DevExpress.DirectX.NativeInterop.Direct2D
{
    using DevExpress.DirectX.Common;
    using DevExpress.DirectX.Common.Direct2D;
    using DevExpress.DirectX.NativeInterop;
    using System;

    public class D2D1Bitmap1 : D2D1Bitmap
    {
        public D2D1Bitmap1(IntPtr nativeObject) : base(nativeObject)
        {
        }

        public D2D1_MAPPED_RECT Map(D2D1_MAP_OPTIONS options)
        {
            D2D1_MAPPED_RECT dd_mapped_rect = new D2D1_MAPPED_RECT();
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, (int) options, ref dd_mapped_rect, 14));
            return dd_mapped_rect;
        }
    }
}

