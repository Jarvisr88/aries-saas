namespace DevExpress.DirectX.StandardInterop.Direct2D
{
    using DevExpress.DirectX.Common.Direct2D;
    using DevExpress.DirectX.StandardInterop.DXGI;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("e8f7fe7a-191c-466d-ad95-975678bda998"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ID2D1DeviceContext
    {
        void GetFactory();
        void CreateBitmap();
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
        void PushLayer(ref D2D1_LAYER_PARAMETERS_COMMON layerParameters, ID2D1Layer layer);
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
        ID2D1Bitmap1 CreateBitmap(D2D_SIZE_U size, IntPtr sourceData, int pitch, D2D1_BITMAP_PROPERTIES1 bitmapProperties);
        void CreateBitmapFromWicBitmap1();
        void CreateColorContext();
        void CreateColorContextFromFilename();
        void CreateColorContextFromWicColorContext();
        ID2D1Bitmap1 CreateBitmapFromDxgiSurface(IDXGISurface surface, D2D1_BITMAP_PROPERTIES1 bitmapProperties);
        ID2D1Effect CreateEffect(ref Guid effectId);
        void CreateGradientStopCollection1();
        ID2D1ImageBrush CreateImageBrush(ID2D1Image image, ref D2D1_IMAGE_BRUSH_PROPERTIES imageBrushProperties, ref D2D1_BRUSH_PROPERTIES brushProperties);
        ID2D1BitmapBrush1 CreateBitmapBrush(ID2D1Bitmap bitmap, ref D2D1_BITMAP_BRUSH_PROPERTIES1 bitmapBrushProperties, ref D2D1_BRUSH_PROPERTIES brushProperties);
        ID2D1CommandList CreateCommandList();
        void IsDxgiFormatSupported();
        void IsBufferPrecisionSupported();
        void GetImageLocalBounds();
        void GetImageWorldBounds();
        void GetGlyphRunWorldBounds();
        void GetDevice();
        [PreserveSig]
        void SetTarget(ID2D1Image image);
        [PreserveSig]
        void GetTarget(out ID2D1Image image);
        void SetRenderingControls();
        void GetRenderingControls();
        void SetPrimitiveBlend();
        void GetPrimitiveBlend();
        void SetUnitMode();
        void GetUnitMode();
        void DrawGlyphRun1();
        [PreserveSig]
        void DrawImage(ID2D1Image image, ref D2D_POINT_2F targetOffset, IntPtr imageRectangle, D2D1_INTERPOLATION_MODE interpolationMode, D2D1_COMPOSITE_MODE compositeMode);
        void DrawGdiMetafile();
        [PreserveSig]
        void DrawBitmap(ID2D1Bitmap bitmap, ref D2D_RECT_F destinationRectangle, float opacity, D2D1_INTERPOLATION_MODE interpolationMode, IntPtr sourceRectangle, IntPtr perspectiveTransformRef);
        [PreserveSig]
        void PushLayer(IntPtr layerParameters, ID2D1Layer layer);
        void InvalidateEffectInputRectangle();
        void GetEffectInvalidRectangleCount();
        void GetEffectInvalidRectangles();
        void GetEffectRequiredInputRectangles();
        void FillOpacityMask1();
    }
}

