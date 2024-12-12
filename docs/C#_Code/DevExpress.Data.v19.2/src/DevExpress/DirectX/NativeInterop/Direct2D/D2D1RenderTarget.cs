namespace DevExpress.DirectX.NativeInterop.Direct2D
{
    using DevExpress.DirectX.Common;
    using DevExpress.DirectX.Common.Direct2D;
    using DevExpress.DirectX.Common.DirectWrite;
    using DevExpress.DirectX.NativeInterop;
    using DevExpress.DirectX.NativeInterop.CCW;
    using DevExpress.DirectX.NativeInterop.DirectWrite;
    using DevExpress.DirectX.NativeInterop.WIC;
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    public class D2D1RenderTarget : D2D1Resource
    {
        public D2D1RenderTarget(IntPtr nativeObject) : base(nativeObject)
        {
        }

        public void BeginDraw()
        {
            ComObject.InvokeHelper.Calli(base.NativeObject, 0x30);
        }

        public void Clear(D2D1_COLOR_F clearColor)
        {
            ComObject.InvokeHelper.Calli(base.NativeObject, ref clearColor, 0x2f);
        }

        public D2D1Bitmap CreateBitmap(D2D_SIZE_U size, IntPtr srcData, int pitch, D2D1_BITMAP_PROPERTIES bitmapProperties)
        {
            IntPtr ptr;
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, size, srcData, pitch, ref bitmapProperties, out ptr, 4));
            return new D2D1Bitmap(ptr);
        }

        public void CreateBitmapBrush()
        {
            throw new NotImplementedException();
        }

        [SecuritySafeCritical]
        public D2D1Bitmap CreateBitmapFromWicBitmap(IComCallableWrapper<IWICBitmapSourceCCW> wicBitmapSource) => 
            this.CreateBitmapFromWicBitmap(wicBitmapSource.NativeObject);

        public D2D1Bitmap CreateBitmapFromWicBitmap(WICBitmapSource wicBitmapSource) => 
            this.CreateBitmapFromWicBitmap(wicBitmapSource.NativeObject);

        private D2D1Bitmap CreateBitmapFromWicBitmap(IntPtr wicBitmapSource)
        {
            IntPtr ptr;
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, wicBitmapSource, IntPtr.Zero, out ptr, 5));
            return new D2D1Bitmap(ptr);
        }

        public void CreateCompatibleRenderTarget()
        {
            throw new NotImplementedException();
        }

        public D2D1GradientStopCollection CreateGradientStopCollection(D2D1_GRADIENT_STOP[] gradientStops, D2D1_GAMMA colorInterpolationGamma, D2D1_EXTEND_MODE extendMode)
        {
            IntPtr ptr;
            using (ArrayMarshaler marshaler = new ArrayMarshaler(gradientStops))
            {
                InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, marshaler.Pointer, gradientStops.Length, (int) colorInterpolationGamma, (int) extendMode, out ptr, 9));
            }
            return new D2D1GradientStopCollection(ptr);
        }

        public D2D1Layer CreateLayer(D2D_SIZE_F size)
        {
            IntPtr ptr;
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, ref size, out ptr, 13));
            return new D2D1Layer(ptr);
        }

        public D2D1LinearGradientBrush CreateLinearGradientBrush(D2D1_LINEAR_GRADIENT_BRUSH_PROPERTIES linearGradientBrushProperties, D2D1_BRUSH_PROPERTIES brushProperties, D2D1GradientStopCollection gradientStopCollection)
        {
            IntPtr ptr;
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, ref linearGradientBrushProperties, ref brushProperties, gradientStopCollection.ToNativeObject(), out ptr, 10));
            return new D2D1LinearGradientBrush(ptr);
        }

        public D2D1LinearGradientBrush CreateLinearGradientBrush(D2D_POINT_2F startPoint, D2D_POINT_2F endPoint, D2D1_COLOR_F startColor, D2D1_COLOR_F endColor)
        {
            D2D1_LINEAR_GRADIENT_BRUSH_PROPERTIES linearGradientBrushProperties = new D2D1_LINEAR_GRADIENT_BRUSH_PROPERTIES(startPoint, endPoint);
            D2D1_BRUSH_PROPERTIES brushProperties = D2D1_BRUSH_PROPERTIES.Default;
            D2D1_GRADIENT_STOP[] gradientStops = new D2D1_GRADIENT_STOP[] { new D2D1_GRADIENT_STOP(0f, startColor), new D2D1_GRADIENT_STOP(1f, endColor) };
            using (D2D1GradientStopCollection stops = this.CreateGradientStopCollection(gradientStops, D2D1_GAMMA.Linear, D2D1_EXTEND_MODE.Clamp))
            {
                return this.CreateLinearGradientBrush(linearGradientBrushProperties, brushProperties, stops);
            }
        }

        public void CreateMesh()
        {
        }

        public D2D1RadialGradientBrush CreateRadialGradientBrush(D2D1_RADIAL_GRADIENT_BRUSH_PROPERTIES radialGradientBrushProperties, D2D1_BRUSH_PROPERTIES brushProperties, D2D1GradientStopCollection gradientStopCollection)
        {
            IntPtr ptr;
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, ref radialGradientBrushProperties, ref brushProperties, gradientStopCollection.ToNativeObject(), out ptr, 11));
            return new D2D1RadialGradientBrush(ptr);
        }

        public void CreateSharedBitmap()
        {
            throw new NotImplementedException();
        }

        public D2D1SolidColorBrush CreateSolidColorBrush(D2D1_COLOR_F color, D2D1_BRUSH_PROPERTIES brushProperties)
        {
            IntPtr ptr;
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, ref color, ref brushProperties, out ptr, 8));
            return new D2D1SolidColorBrush(ptr);
        }

        public void DrawBitmap(D2D1Bitmap bitmap, D2D_RECT_F destinationRectangle, float opacity, D2D1_BITMAP_INTERPOLATION_MODE interpolationMode)
        {
            ComObject.InvokeHelper.Calli(base.NativeObject, bitmap.ToNativeObject(), ref destinationRectangle, opacity, (int) interpolationMode, IntPtr.Zero, 0x1a);
        }

        public void DrawBitmap(D2D1Bitmap bitmap, D2D_RECT_F destinationRectangle, float opacity, D2D1_BITMAP_INTERPOLATION_MODE interpolationMode, D2D_RECT_F sourceRectangle)
        {
            ComObject.InvokeHelper.Calli(base.NativeObject, bitmap.ToNativeObject(), ref destinationRectangle, opacity, (int) interpolationMode, ref sourceRectangle, 0x1a);
        }

        public void DrawEllipse(D2D1_ELLIPSE ellipse, D2D1Brush brush, float strokeWidth, D2D1StrokeStyle strokeStyle)
        {
            ComObject.InvokeHelper.Calli(base.NativeObject, ref ellipse, brush.ToNativeObject(), strokeWidth, strokeStyle.ToNativeObject(), 20);
        }

        public void DrawGeometry(D2D1Geometry geometry, D2D1Brush brush, float strokeWidth)
        {
            this.DrawGeometry(geometry, brush, strokeWidth, null);
        }

        public void DrawGeometry(D2D1Geometry geometry, D2D1Brush brush, float strokeWidth, D2D1StrokeStyle strokeStyle)
        {
            ComObject.InvokeHelper.Calli(base.NativeObject, geometry.ToNativeObject(), brush.ToNativeObject(), strokeWidth, strokeStyle.ToNativeObject(), 0x16);
        }

        public void DrawGlyphRun(D2D_POINT_2F baselineOrigin, DWRITE_GLYPH_RUN glyphRun, D2D1Brush foregroundBrush, DWRITE_MEASURING_MODE measuringMode)
        {
            using (DWriteGlyphRunMarshaler marshaler = new DWriteGlyphRunMarshaler(glyphRun))
            {
                DWRITE_GLYPH_RUN_COMMON dwrite_glyph_run_common = marshaler.GlyphRun;
                ComObject.InvokeHelper.Calli(base.NativeObject, baselineOrigin, ref dwrite_glyph_run_common, foregroundBrush.ToNativeObject(), (int) measuringMode, 0x1d);
            }
        }

        public void DrawLine(D2D_POINT_2F startPoint, D2D_POINT_2F endPoint, D2D1Brush brush, float strokeWidth)
        {
            this.DrawLine(startPoint, endPoint, brush, strokeWidth, null);
        }

        public void DrawLine(D2D_POINT_2F point0, D2D_POINT_2F point1, D2D1Brush brush, float strokeWidth, D2D1StrokeStyle strokeStyle)
        {
            ComObject.InvokeHelper.Calli(base.NativeObject, point0, point1, brush.ToNativeObject(), strokeWidth, strokeStyle.ToNativeObject(), 15);
        }

        public void DrawRectangle(D2D_RECT_F rect, D2D1Brush brush, float strokeWidth, D2D1StrokeStyle strokeStyle)
        {
            ComObject.InvokeHelper.Calli(base.NativeObject, ref rect, brush.ToNativeObject(), strokeWidth, strokeStyle.ToNativeObject(), 0x10);
        }

        public void DrawRoundedRectangle(D2D1_ROUNDED_RECT rect, D2D1Brush brush, float strokeWidth, D2D1StrokeStyle strokeStyle)
        {
            ComObject.InvokeHelper.Calli(base.NativeObject, ref rect, brush.ToNativeObject(), strokeWidth, strokeStyle.ToNativeObject(), 0x12);
        }

        public void DrawText()
        {
            throw new NotImplementedException();
        }

        public void DrawTextLayout()
        {
            throw new NotImplementedException();
        }

        public void EndDraw()
        {
            long num;
            long num2;
            this.EndDraw(out num, out num2);
        }

        public void EndDraw(out long tag1, out long tag2)
        {
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, out tag1, out tag2, 0x31));
        }

        public void FillEllipse(D2D1_ELLIPSE ellipse, D2D1Brush brush)
        {
            ComObject.InvokeHelper.Calli(base.NativeObject, ref ellipse, brush.ToNativeObject(), 0x15);
        }

        public void FillGeometry(D2D1Geometry geometry, D2D1Brush brush)
        {
            this.FillGeometry(geometry, brush, null);
        }

        public void FillGeometry(D2D1Geometry geometry, D2D1Brush brush, D2D1Brush opacityBrush)
        {
            ComObject.InvokeHelper.Calli(base.NativeObject, geometry.ToNativeObject(), brush.ToNativeObject(), opacityBrush.ToNativeObject(), 0x17);
        }

        public void FillMesh()
        {
            throw new NotImplementedException();
        }

        public void FillOpacityMask()
        {
            throw new NotImplementedException();
        }

        public void FillRectangle(D2D_RECT_F rect, D2D1Brush brush)
        {
            ComObject.InvokeHelper.Calli(base.NativeObject, ref rect, brush.ToNativeObject(), 0x11);
        }

        public void FillRoundedRectangle(D2D1_ROUNDED_RECT rect, D2D1Brush brush)
        {
            ComObject.InvokeHelper.Calli(base.NativeObject, ref rect, brush.ToNativeObject(), 0x13);
        }

        public void Flush()
        {
            long num;
            long num2;
            this.Flush(out num, out num2);
        }

        public void Flush(out long tag1, out long tag2)
        {
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, out tag1, out tag2, 0x2a));
        }

        public D2D1_ANTIALIAS_MODE GetAntialiasMode() => 
            (D2D1_ANTIALIAS_MODE) ComObject.InvokeHelper.CalliInt(base.NativeObject, 0x21);

        public void GetDpi()
        {
            throw new NotImplementedException();
        }

        public int GetMaximumBitmapSize() => 
            ComObject.InvokeHelper.CalliInt(base.NativeObject, 0x37);

        public D2D1_PIXEL_FORMAT GetPixelFormat()
        {
            throw new NotImplementedException();
        }

        public D2D_SIZE_U GetPixelSize()
        {
            throw new NotImplementedException();
        }

        public D2D_SIZE_F GetSize()
        {
            throw new NotImplementedException();
        }

        public void GetTags(long tag1, long tag2)
        {
            throw new NotImplementedException();
        }

        public void GetTextAntialiasMode()
        {
            throw new NotImplementedException();
        }

        public void GetTextRenderingParams()
        {
            throw new NotImplementedException();
        }

        public D2D_MATRIX_3X2_F GetTransform()
        {
            D2D_MATRIX_3X2_F identity = D2D_MATRIX_3X2_F.Identity;
            ComObject.InvokeHelper.Calli(base.NativeObject, ref identity, 0x1f);
            return identity;
        }

        public void IsSupported()
        {
            throw new NotImplementedException();
        }

        public void PopAxisAlignedClip()
        {
            ComObject.InvokeHelper.Calli(base.NativeObject, 0x2e);
        }

        public void PopLayer()
        {
            ComObject.InvokeHelper.Calli(base.NativeObject, 0x29);
        }

        public void PushAxisAlignedClip(D2D_RECT_F clipRect, D2D1_ANTIALIAS_MODE antialiasMode)
        {
            ComObject.InvokeHelper.Calli(base.NativeObject, ref clipRect, (int) antialiasMode, 0x2d);
        }

        public void PushLayer(D2D1_LAYER_PARAMETERS layerParameters, D2D1Layer layer)
        {
            ComObject.InvokeHelper.Calli(base.NativeObject, ref layerParameters, layer.ToNativeObject(), 40);
        }

        public void RestoreDrawingState()
        {
            throw new NotImplementedException();
        }

        public void SaveDrawingState()
        {
            throw new NotImplementedException();
        }

        public void SetAntialiasMode(D2D1_ANTIALIAS_MODE antialiasMode)
        {
            ComObject.InvokeHelper.Calli(base.NativeObject, (int) antialiasMode, 0x20);
        }

        public void SetDpi(float dpiX, float dpiY)
        {
            ComObject.InvokeHelper.Calli(base.NativeObject, dpiX, dpiY, 0x33);
        }

        public void SetTags(long tag1, long tag2)
        {
            throw new NotImplementedException();
        }

        public void SetTextAntialiasMode(D2D1_TEXT_ANTIALIAS_MODE mode)
        {
            ComObject.InvokeHelper.Calli(base.NativeObject, (int) mode, 0x22);
        }

        public void SetTextRenderingParams()
        {
            throw new NotImplementedException();
        }

        public void SetTransform(D2D_MATRIX_3X2_F transform)
        {
            ComObject.InvokeHelper.Calli(base.NativeObject, ref transform, 30);
        }
    }
}

