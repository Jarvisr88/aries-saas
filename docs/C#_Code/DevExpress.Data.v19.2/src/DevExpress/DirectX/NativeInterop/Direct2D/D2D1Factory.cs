namespace DevExpress.DirectX.NativeInterop.Direct2D
{
    using DevExpress.DirectX.Common;
    using DevExpress.DirectX.Common.Direct2D;
    using DevExpress.DirectX.NativeInterop;
    using DevExpress.DirectX.NativeInterop.DXGI;
    using System;

    public class D2D1Factory : ComObject
    {
        public D2D1Factory(IntPtr nativeObject) : base(nativeObject)
        {
        }

        public D2D1DCRenderTarget CreateDCRenderTarget()
        {
            D2D1_RENDER_TARGET_PROPERTIES renderTargetProperties = new D2D1_RENDER_TARGET_PROPERTIES(D2D1_RENDER_TARGET_TYPE.Default, D2D1_PIXEL_FORMAT.Default, 0f, 0f, D2D1_RENDER_TARGET_USAGE.GdiCompatible, D2D1_FEATURE_LEVEL.Level_DEFAULT);
            return this.CreateDCRenderTarget(renderTargetProperties);
        }

        public D2D1DCRenderTarget CreateDCRenderTarget(D2D1_RENDER_TARGET_PROPERTIES renderTargetProperties)
        {
            IntPtr ptr;
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, ref renderTargetProperties, out ptr, 0x10));
            return new D2D1DCRenderTarget(ptr);
        }

        public void CreateDrawingStateBlock()
        {
            throw new NotImplementedException();
        }

        public D2D1RenderTarget CreateDxgiSurfaceRenderTarget(DXGISurface dxgiSurface, D2D1_RENDER_TARGET_PROPERTIES renderTargetProperties)
        {
            IntPtr ptr;
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, dxgiSurface.NativeObject, ref renderTargetProperties, out ptr, 15));
            return new D2D1RenderTarget(ptr);
        }

        public void CreateEllipseGeometry()
        {
            throw new NotImplementedException();
        }

        public void CreateGeometryGroup()
        {
            throw new NotImplementedException();
        }

        public void CreateHwndRenderTarget()
        {
            throw new NotImplementedException();
        }

        public D2D1PathGeometry CreatePathGeometry()
        {
            IntPtr ptr;
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, out ptr, 10));
            return new D2D1PathGeometry(ptr);
        }

        public D2D1Geometry CreateRectangleGeometry(D2D_RECT_F rectangle)
        {
            IntPtr ptr;
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, ref rectangle, out ptr, 5));
            return new D2D1Geometry(ptr);
        }

        public void CreateRoundedRectangleGeometry()
        {
            throw new NotImplementedException();
        }

        public void CreateStrokeStyle()
        {
            throw new NotImplementedException();
        }

        public D2D1TransformedGeometry CreateTransformedGeometry(D2D1Geometry sourceGeometry, D2D_MATRIX_3X2_F transform)
        {
            IntPtr ptr;
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, sourceGeometry.ToNativeObject(), ref transform, out ptr, 9));
            return new D2D1TransformedGeometry(ptr);
        }

        public void CreateWicBitmapRenderTarget()
        {
            throw new NotImplementedException();
        }

        public void GetDesktopDpi(float dpiX, float dpiY)
        {
            ComObject.InvokeHelper.Calli(base.NativeObject, dpiX, dpiY, 4);
        }

        public void ReloadSystemMetrics()
        {
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, 3));
        }
    }
}

