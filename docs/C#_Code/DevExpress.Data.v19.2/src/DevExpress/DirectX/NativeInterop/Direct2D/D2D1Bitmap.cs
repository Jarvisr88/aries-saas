namespace DevExpress.DirectX.NativeInterop.Direct2D
{
    using DevExpress.DirectX.Common;
    using DevExpress.DirectX.Common.Direct2D;
    using DevExpress.DirectX.NativeInterop;
    using System;

    public class D2D1Bitmap : D2D1Image
    {
        public D2D1Bitmap(IntPtr nativeObject) : base(nativeObject)
        {
        }

        public void CopyFromBitmap(D2D_POINT_2U destPoint, D2D1Bitmap bitmap, D2D_RECT_U srcRect)
        {
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, ref destPoint, bitmap.ToNativeObject(), ref srcRect, 8));
        }

        public void CopyFromMemory(D2D_RECT_U dstRect, IntPtr srcData, int pitch)
        {
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, ref dstRect, srcData, pitch, 10));
        }

        public void CopyFromRenderTarget(D2D_POINT_2U destPoint, D2D1RenderTarget renderTarget, D2D_RECT_U srcRect)
        {
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, ref destPoint, renderTarget.ToNativeObject(), ref srcRect, 9));
        }

        public void GetDpi()
        {
            throw new NotImplementedException();
        }

        public D2D1_PIXEL_FORMAT GetPixelFormat()
        {
            D2D1_PIXEL_FORMAT dd_pixel_format;
            ComObject.InvokeHelper.Calli(base.NativeObject, out dd_pixel_format, 6);
            return dd_pixel_format;
        }

        public D2D_SIZE_U GetPixelSize()
        {
            D2D_SIZE_U dd_size_u;
            ComObject.InvokeHelper.Calli(base.NativeObject, out dd_size_u, 5);
            return dd_size_u;
        }

        public D2D_SIZE_F GetSize()
        {
            throw new NotImplementedException();
        }
    }
}

