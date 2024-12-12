namespace DevExpress.DirectX.StandardInterop.Direct2D
{
    using DevExpress.DirectX.Common.Direct2D;
    using DevExpress.DirectX.StandardInterop.DXGI;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("bb12d362-daee-4b9a-aa1d-14ba401cfa1f"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ID2D1Factory1
    {
        void ReloadSystemMetrics();
        void GetDesktopDpi();
        void CreateRectangleGeometry();
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
        ID2D1Device CreateDevice(IDXGIDevice dxgiDevice);
        void CreateStrokeStyle1();
        void CreatePathGeometry1();
        void CreateDrawingStateBlock1();
        void CreateGdiMetafile();
        void RegisterEffectFromStream();
        void RegisterEffectFromString();
        void UnregisterEffect();
        void GetRegisteredEffects();
        void GetEffectProperties();
    }
}

