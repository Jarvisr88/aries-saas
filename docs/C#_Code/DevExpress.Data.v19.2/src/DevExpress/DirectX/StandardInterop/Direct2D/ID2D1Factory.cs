namespace DevExpress.DirectX.StandardInterop.Direct2D
{
    using DevExpress.DirectX.Common.Direct2D;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("06152247-6f50-465a-9245-118bfd3b6007"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ID2D1Factory
    {
        void ReloadSystemMetrics();
        void GetDesktopDpi();
        ID2D1Geometry CreateRectangleGeometry(ref D2D_RECT_F rectangle);
        void CreateRoundedRectangleGeometry();
        void CreateEllipseGeometry();
        void CreateGeometryGroup();
        ID2D1TransformedGeometry CreateTransformedGeometry(ID2D1Geometry sourceGeometry, ref D2D_MATRIX_3X2_F transform);
        ID2D1PathGeometry CreatePathGeometry();
        ID2D1StrokeStyle CreateStrokeStyle(ref D2D1_STROKE_STYLE_PROPERTIES1 strokeStyleProperties, [MarshalAs(UnmanagedType.LPArray)] float[] dashes, int dashesCount);
        void CreateDrawingStateBlock();
        void CreateWicBitmapRenderTarget();
        void CreateHwndRenderTarget();
        void CreateDxgiSurfaceRenderTarget();
        ID2D1DCRenderTarget CreateDCRenderTarget(ref D2D1_RENDER_TARGET_PROPERTIES renderTargetProperties);
    }
}

