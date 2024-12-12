namespace DevExpress.DirectX.NativeInterop.Direct2D
{
    using DevExpress.DirectX.Common;
    using DevExpress.DirectX.Common.Direct2D;
    using DevExpress.DirectX.Common.DirectWrite;
    using DevExpress.DirectX.NativeInterop;
    using DevExpress.DirectX.NativeInterop.DirectWrite;
    using DevExpress.DirectX.NativeInterop.DXGI;
    using System;
    using System.Runtime.InteropServices;

    public class D2D1DeviceContext : D2D1RenderTarget
    {
        public D2D1DeviceContext(IntPtr nativeObject) : base(nativeObject)
        {
        }

        public D2D1DeviceContext2 AsDeviceContext2()
        {
            IntPtr? nullable = base.QueryInterface<D2D1DeviceContext2>(false);
            return ((nullable == null) ? null : new D2D1DeviceContext2(nullable.Value));
        }

        public D2D1Bitmap1 CreateBitmap(D2D_SIZE_U size, IntPtr sourceData, int pitch, D2D1_BITMAP_PROPERTIES1 bitmapProperties)
        {
            IntPtr ptr;
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, size, sourceData, pitch, ref bitmapProperties, out ptr, 0x39));
            return new D2D1Bitmap1(ptr);
        }

        public D2D1BitmapBrush1 CreateBitmapBrush(D2D1Bitmap bitmap, D2D1_BITMAP_BRUSH_PROPERTIES1 bitmapBrushProperties, D2D1_BRUSH_PROPERTIES brushProperties)
        {
            IntPtr ptr;
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, bitmap.ToNativeObject(), ref bitmapBrushProperties, ref brushProperties, out ptr, 0x42));
            return new D2D1BitmapBrush1(ptr);
        }

        public D2D1Bitmap1 CreateBitmapFromDxgiSurface(DXGISurface surface)
        {
            IntPtr ptr;
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, surface.ToNativeObject(), IntPtr.Zero, out ptr, 0x3e));
            return new D2D1Bitmap1(ptr);
        }

        public D2D1Bitmap1 CreateBitmapFromDxgiSurface(DXGISurface surface, D2D1_BITMAP_PROPERTIES1 bitmapProperties)
        {
            IntPtr ptr;
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, surface.ToNativeObject(), ref bitmapProperties, out ptr, 0x3e));
            return new D2D1Bitmap1(ptr);
        }

        public void CreateBitmapFromWicBitmap1()
        {
            throw new NotImplementedException();
        }

        public void CreateColorContext()
        {
            throw new NotImplementedException();
        }

        public void CreateColorContextFromFilename()
        {
            throw new NotImplementedException();
        }

        public void CreateColorContextFromWicColorContext()
        {
            throw new NotImplementedException();
        }

        public D2D1CommandList CreateCommandList()
        {
            IntPtr ptr;
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, out ptr, 0x43));
            return new D2D1CommandList(ptr);
        }

        public D2D1Effect CreateEffect(Guid effectId)
        {
            IntPtr ptr;
            using (ArrayMarshaler marshaler = new ArrayMarshaler(effectId.ToByteArray()))
            {
                InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, marshaler.Pointer, out ptr, 0x3f));
            }
            return new D2D1Effect(ptr);
        }

        public void CreateGradientStopCollection()
        {
            throw new NotImplementedException();
        }

        public D2D1ImageBrush CreateImageBrush(D2D1Image image, D2D1_IMAGE_BRUSH_PROPERTIES imageBrushProperties, D2D1_BRUSH_PROPERTIES brushProperties)
        {
            IntPtr ptr;
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, image.ToNativeObject(), ref imageBrushProperties, ref brushProperties, out ptr, 0x41));
            return new D2D1ImageBrush(ptr);
        }

        public void DrawBitmap(D2D1Bitmap bitmap, D2D_RECT_F destinationRectangle, float opacity, D2D1_INTERPOLATION_MODE interpolationMode)
        {
            ComObject.InvokeHelper.Calli(base.NativeObject, bitmap.ToNativeObject(), ref destinationRectangle, opacity, (int) interpolationMode, IntPtr.Zero, IntPtr.Zero, 0x55);
        }

        public void DrawBitmap(D2D1Bitmap bitmap, D2D_RECT_F destinationRectangle, float opacity, D2D1_INTERPOLATION_MODE interpolationMode, D2D_RECT_F sourceRectangle)
        {
            ComObject.InvokeHelper.Calli(base.NativeObject, bitmap.ToNativeObject(), ref destinationRectangle, opacity, (int) interpolationMode, ref sourceRectangle, IntPtr.Zero, 0x55);
        }

        public void DrawGdiMetafile(D2D1GdiMetafile metafile, D2D_POINT_2F targetOffset)
        {
            ComObject.InvokeHelper.Calli(base.NativeObject, metafile.ToNativeObject(), ref targetOffset, 0x54);
        }

        public void DrawGlyphRun1()
        {
            throw new NotImplementedException();
        }

        public void DrawImage(D2D1Image image, D2D_POINT_2F targetOffset, D2D1_INTERPOLATION_MODE interpolationMode, D2D1_COMPOSITE_MODE compositeMode)
        {
            ComObject.InvokeHelper.Calli(base.NativeObject, image.ToNativeObject(), ref targetOffset, IntPtr.Zero, (int) interpolationMode, (int) compositeMode, 0x53);
        }

        public void DrawImage(D2D1Image image, D2D_POINT_2F targetOffset, D2D_RECT_F imageRectangle, D2D1_INTERPOLATION_MODE interpolationMode, D2D1_COMPOSITE_MODE compositeMode)
        {
            ComObject.InvokeHelper.Calli(base.NativeObject, image.ToNativeObject(), ref targetOffset, ref imageRectangle, (int) interpolationMode, (int) compositeMode, 0x53);
        }

        public void FillOpacityMask(D2D1Bitmap opacityMask, D2D1Brush brush, D2D_RECT_F destinationRectangle, D2D_RECT_F sourceRectangle)
        {
            ComObject.InvokeHelper.Calli(base.NativeObject, opacityMask.ToNativeObject(), brush.ToNativeObject(), ref destinationRectangle, ref sourceRectangle, 0x5b);
        }

        public D2D1Device GetDevice()
        {
            IntPtr ptr;
            ComObject.InvokeHelper.Calli(base.NativeObject, out ptr, 0x49);
            return new D2D1Device(ptr);
        }

        public void GetEffectInvalidRectangleCount(D2D1Effect effect, int rectangleCount)
        {
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, effect.ToNativeObject(), rectangleCount, 0x58));
        }

        public void GetEffectInvalidRectangles(D2D1Effect effect, out D2D_RECT_F rectangles, int rectanglesCount)
        {
            throw new NotImplementedException();
        }

        public void GetEffectRequiredInputRectangles()
        {
            throw new NotImplementedException();
        }

        public D2D_RECT_F GetGlyphRunWorldBounds(D2D_POINT_2F baselineOrigin, DWRITE_GLYPH_RUN glyphRun, DWRITE_MEASURING_MODE measuringMode)
        {
            D2D_RECT_F infinite = D2D_RECT_F.Infinite;
            using (DWriteGlyphRunMarshaler marshaler = new DWriteGlyphRunMarshaler(glyphRun))
            {
                DWRITE_GLYPH_RUN_COMMON dwrite_glyph_run_common = marshaler.GlyphRun;
                InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, baselineOrigin, ref dwrite_glyph_run_common, (int) measuringMode, out infinite, 0x48));
                return infinite;
            }
        }

        public void GetImageLocalBounds()
        {
            throw new NotImplementedException();
        }

        public void GetImageWorldBounds()
        {
            throw new NotImplementedException();
        }

        public D2D1_PRIMITIVE_BLEND GetPrimitiveBlend() => 
            (D2D1_PRIMITIVE_BLEND) ComObject.InvokeHelper.CalliInt(base.NativeObject, 0x4f);

        public void GetRenderingControls()
        {
            throw new NotImplementedException();
        }

        public D2D1Image GetTarget()
        {
            IntPtr ptr;
            ComObject.InvokeHelper.Calli(base.NativeObject, out ptr, 0x4b);
            return new D2D1Image(ptr);
        }

        public void GetUnitMode()
        {
            throw new NotImplementedException();
        }

        public void InvalidateEffectInputRectangle(D2D1Effect effect, int input, D2D_RECT_F inputRectangle)
        {
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, effect.ToNativeObject(), input, ref inputRectangle, 0x57));
        }

        public void IsBufferPrecisionSupported()
        {
            throw new NotImplementedException();
        }

        public void IsDxgiFormatSupported()
        {
            throw new NotImplementedException();
        }

        public void PushLayer(D2D1_LAYER_PARAMETERS1 layerParameters)
        {
            this.PushLayer(layerParameters, null);
        }

        public void PushLayer(D2D1_LAYER_PARAMETERS1 layerParameters, D2D1Layer layer)
        {
            ComObject.InvokeHelper.Calli(base.NativeObject, ref layerParameters, layer.ToNativeObject(), 0x56);
        }

        public void SetPrimitiveBlend(D2D1_PRIMITIVE_BLEND primitiveBlend)
        {
            ComObject.InvokeHelper.Calli(base.NativeObject, (int) primitiveBlend, 0x4e);
        }

        public void SetRenderingControls()
        {
            throw new NotImplementedException();
        }

        public void SetTarget(D2D1Image image)
        {
            ComObject.InvokeHelper.Calli(base.NativeObject, image.ToNativeObject(), 0x4a);
        }

        public void SetUnitMode()
        {
            throw new NotImplementedException();
        }
    }
}

