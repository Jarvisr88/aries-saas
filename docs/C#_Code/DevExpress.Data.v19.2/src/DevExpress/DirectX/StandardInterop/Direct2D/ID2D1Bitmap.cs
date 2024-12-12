namespace DevExpress.DirectX.StandardInterop.Direct2D
{
    using DevExpress.DirectX.Common.Direct2D;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("a2296057-ea42-4099-983b-539fb6505426"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ID2D1Bitmap
    {
        void GetFactory();
        void GetSize();
        [PreserveSig]
        void GetPixelSize(out D2D_SIZE_U size);
        [PreserveSig]
        void GetPixelFormat(out D2D1_PIXEL_FORMAT pixelFormat);
        void GetDpi();
        void CopyFromBitmap(ref D2D_POINT_2U destPoint, ID2D1Bitmap bitmap, ref D2D_RECT_U srcRect);
        void CopyFromRenderTarget(ref D2D_POINT_2U destPoint, ID2D1RenderTarget renderTarget, ref D2D_RECT_U srcRect);
        void CopyFromMemory(ref D2D_RECT_U dstRect, IntPtr srcData, int pitch);
    }
}

