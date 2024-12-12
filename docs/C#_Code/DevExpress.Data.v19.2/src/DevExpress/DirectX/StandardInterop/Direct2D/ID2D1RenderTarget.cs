namespace DevExpress.DirectX.StandardInterop.Direct2D
{
    using DevExpress.DirectX.Common.Direct2D;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("2cd90694-12e2-11dc-9fed-001143a055f9"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ID2D1RenderTarget
    {
        void GetFactory();
        ID2D1Bitmap CreateBitmap(D2D_SIZE_U size, IntPtr srcData, int pitch, ref D2D1_BITMAP_PROPERTIES bitmapProperties);
        ID2D1Bitmap CreateBitmapFromWicBitmap(IntPtr wicBitmapSource, IntPtr zero);
        void CreateSharedBitmap();
        void CreateBitmapBrush();
        ID2D1SolidColorBrush CreateSolidColorBrush(ref D2D1_COLOR_F color, ref D2D1_BRUSH_PROPERTIES brushProperties);
        ID2D1GradientStopCollection CreateGradientStopCollection([MarshalAs(UnmanagedType.LPArray)] D2D1_GRADIENT_STOP[] gradientStops, int gradientStopsCount, D2D1_GAMMA colorInterpolationGamma, D2D1_EXTEND_MODE extendMode);
        ID2D1LinearGradientBrush CreateLinearGradientBrush(ref D2D1_LINEAR_GRADIENT_BRUSH_PROPERTIES linearGradientBrushProperties, ref D2D1_BRUSH_PROPERTIES brushProperties, ID2D1GradientStopCollection gradientStopCollection);
        void CreateRadialGradientBrush();
        void CreateCompatibleRenderTarget();
        ID2D1Layer CreateLayer(IntPtr sizePtr);
        void CreateMesh();
        [PreserveSig]
        void DrawLine(D2D_POINT_2F point0, D2D_POINT_2F point1, ID2D1Brush brush, float strokeWidth, ID2D1StrokeStyle strokeStyle);
        void DrawRectangle();
        [PreserveSig]
        void FillRectangle(ref D2D_RECT_F rect, ID2D1Brush brush);
        void DrawRoundedRectangle();
        void FillRoundedRectangle();
        [PreserveSig]
        void DrawEllipse(ref D2D1_ELLIPSE ellipse, ID2D1Brush brush, float strokeWidth, ID2D1StrokeStyle strokeStyle);
        void FillEllipse();
        [PreserveSig]
        void DrawGeometry(ID2D1Geometry geometry, ID2D1Brush brush, float strokeWidth, ID2D1StrokeStyle strokeStyle);
        [PreserveSig]
        void FillGeometry(ID2D1Geometry geometry, ID2D1Brush brush, ID2D1Brush opacityBrush);
        void FillMesh();
        void FillOpacityMask();
        [PreserveSig]
        void DrawBitmap(ID2D1Bitmap bitmap, ref D2D_RECT_F destinationRectangle, float opacity, D2D1_BITMAP_INTERPOLATION_MODE interpolationMode, ref D2D_RECT_F sourceRectangle);
        void DrawTextW();
        void DrawTextLayout();
        void DrawGlyphRun();
        [PreserveSig]
        void SetTransform(ref D2D_MATRIX_3X2_F transform);
        [PreserveSig]
        void GetTransform(out D2D_MATRIX_3X2_F transform);
        [PreserveSig]
        void SetAntialiasMode(D2D1_ANTIALIAS_MODE antialiasMode);
        void GetAntialiasMode();
        void SetTextAntialiasMode();
        void GetTextAntialiasMode();
        void SetTextRenderingParams();
        void GetTextRenderingParams();
        void SetTags();
        void GetTags();
        [PreserveSig]
        void PushLayer(IntPtr layerParameters, ID2D1Layer layer);
        [PreserveSig]
        void PopLayer();
        void Flush(out long tag1, out long tag2);
        void SaveDrawingState();
        void RestoreDrawingState();
        [PreserveSig]
        void PushAxisAlignedClip(ref D2D_RECT_F clipRect, D2D1_ANTIALIAS_MODE antialiasMode);
        [PreserveSig]
        void PopAxisAlignedClip();
        [PreserveSig]
        void Clear(ref D2D1_COLOR_F clearColor);
        [PreserveSig]
        void BeginDraw();
        void EndDraw(out long tag1, out long tag2);
        void GetPixelFormat();
        void SetDpi();
        void GetDpi();
        void GetSize();
        void GetPixelSize();
        void GetMaximumBitmapSize();
        void IsSupported();
    }
}

