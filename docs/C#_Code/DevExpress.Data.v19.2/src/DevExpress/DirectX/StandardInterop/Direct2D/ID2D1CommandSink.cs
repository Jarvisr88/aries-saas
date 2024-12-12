namespace DevExpress.DirectX.StandardInterop.Direct2D
{
    using DevExpress.DirectX.Common.Direct2D;
    using System;
    using System.Runtime.InteropServices;

    [Guid("54d7898a-a061-40a7-bec7-e465bcba2c4f"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ID2D1CommandSink
    {
        [PreserveSig]
        int BeginDraw();
        [PreserveSig]
        int EndDraw();
        [PreserveSig]
        int SetAntialiasMode(D2D1_ANTIALIAS_MODE antialiasMode);
        [PreserveSig]
        int SetTags(long tag1, long tag2);
        [PreserveSig]
        int SetTextAntialiasMode(int textAntialiasMode);
        [PreserveSig]
        int SetTextRenderingParams(IntPtr textRenderingParams);
        [PreserveSig]
        int SetTransform(ref D2D_MATRIX_3X2_F transform);
        [PreserveSig]
        int SetPrimitiveBlend(D2D1_PRIMITIVE_BLEND primitiveBlend);
        [PreserveSig]
        int SetUnitMode(int unitMode);
        [PreserveSig]
        int Clear(ref D2D1_COLOR_F color);
        [PreserveSig]
        int DrawGlyphRun(D2D_POINT_2F baselineOrigin, IntPtr glyphRun, IntPtr glyphRunDescription, ID2D1Brush foregroundBrush, int measuringMode);
        [PreserveSig]
        int DrawLine(D2D_POINT_2F point0, D2D_POINT_2F point1, ID2D1Brush brush, float strokeWidth, ID2D1StrokeStyle strokeStyle);
        [PreserveSig]
        int DrawGeometry(ID2D1Geometry geometry, ID2D1Brush brush, float strokeWidth, ID2D1StrokeStyle strokeStyle);
        [PreserveSig]
        int DrawRectangle(ref D2D_RECT_F rect, ID2D1Brush brush, float strokeWidth, ID2D1StrokeStyle strokeStyle);
        [PreserveSig]
        int DrawBitmap(ID2D1Bitmap bitmap, ref D2D_RECT_F destinationRectangle, float opacity, D2D1_INTERPOLATION_MODE interpolationMode, ref D2D_RECT_F sourceRectangle, IntPtr perspectiveTransform);
        [PreserveSig]
        int DrawImage(ID2D1Image image, ref D2D_POINT_2F targetOffset, ref D2D_RECT_F imageRectangle, D2D1_INTERPOLATION_MODE interpolationMode, D2D1_COMPOSITE_MODE compositeMode);
        [PreserveSig]
        int DrawGdiMetafile(IntPtr gdiMetafile, ref D2D_POINT_2F targetOffset);
        [PreserveSig]
        int FillMesh(IntPtr mesh, ID2D1Brush brush);
        [PreserveSig]
        int FillOpacityMask(ID2D1Bitmap opacityMask, ID2D1Brush brush, ref D2D_RECT_F destinationRectangle, ref D2D_RECT_F sourceRectangle);
        [PreserveSig]
        int FillGeometry(ID2D1Geometry geometry, ID2D1Brush brush, ID2D1Brush opacityBrush);
        [PreserveSig]
        int FillRectangle(ref D2D_RECT_F rect, ID2D1Brush brush);
        [PreserveSig]
        int PushAxisAlignedClip(D2D_RECT_F clipRect, D2D1_ANTIALIAS_MODE antialiasMode);
        [PreserveSig]
        int PushLayer(ref D2D1_LAYER_PARAMETERS1_COMMON layerParameters1, ID2D1Layer layer);
        [PreserveSig]
        int PopAxisAlignedClip();
        [PreserveSig]
        int PopLayer();
    }
}

