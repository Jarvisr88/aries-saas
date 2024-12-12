namespace DevExpress.DirectX.StandardInterop.Direct2D
{
    using DevExpress.DirectX.Common;
    using DevExpress.DirectX.Common.Direct2D;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("1c51bc64-de61-46fd-9899-63a5d8f03950"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ID2D1DCRenderTarget
    {
        void GetFactory();
        ID2D1Bitmap CreateBitmap(D2D_SIZE_U size, IntPtr srcData, int pitch, ref D2D1_BITMAP_PROPERTIES bitmapProperties);
        void CreateBitmapFromWicBitmap();
        void CreateSharedBitmap();
        void CreateBitmapBrush();
        ID2D1SolidColorBrush CreateSolidColorBrush(ref D2D1_COLOR_F color, ref D2D1_BRUSH_PROPERTIES brushProperties);
        void CreateGradientStopCollection();
        void CreateLinearGradientBrush();
        void CreateRadialGradientBrush();
        void CreateCompatibleRenderTarget();
        void CreateLayer();
        void CreateMesh();
        void DrawLine();
        void DrawRectangle();
        void FillRectangle();
        void DrawRoundedRectangle();
        void FillRoundedRectangle();
        void DrawEllipse();
        void FillEllipse();
        void DrawGeometry();
        [PreserveSig]
        void FillGeometry(ID2D1Geometry geometry, ID2D1Brush brush, ID2D1Brush opacityBrush);
        void FillMesh();
        void FillOpacityMask();
        [PreserveSig]
        void DrawBitmap(ID2D1Bitmap bitmap, ref D2D_RECT_F destinationRectangle, float opacity, D2D1_BITMAP_INTERPOLATION_MODE interpolationMode, ref D2D_RECT_F sourceRectangle);
        void DrawTextW();
        void DrawTextLayout();
        void DrawGlyphRun();
        void SetTransform();
        void GetTransform();
        void SetAntialiasMode();
        void GetAntialiasMode();
        void SetTextAntialiasMode();
        void GetTextAntialiasMode();
        void SetTextRenderingParams();
        void GetTextRenderingParams();
        void SetTags();
        void GetTags();
        void PushLayer();
        void PopLayer();
        void Flush();
        void SaveDrawingState();
        void RestoreDrawingState();
        void PushAxisAlignedClip();
        void PopAxisAlignedClip();
        [PreserveSig]
        void Clear(ref D2D1_COLOR_F clearColor);
        [PreserveSig]
        void BeginDraw();
        int EndDraw(out long tag1, out long tag2);
        void GetPixelFormat();
        void SetDpi();
        void GetDpi();
        void GetSize();
        void GetPixelSize();
        void GetMaximumBitmapSize();
        void IsSupported();
        void BindDC(IntPtr hDC, ref RECT subRectRef);
    }
}

