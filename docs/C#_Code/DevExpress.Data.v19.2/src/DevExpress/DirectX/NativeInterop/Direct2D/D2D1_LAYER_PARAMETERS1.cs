namespace DevExpress.DirectX.NativeInterop.Direct2D
{
    using DevExpress.DirectX.Common.Direct2D;
    using DevExpress.DirectX.NativeInterop;
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct D2D1_LAYER_PARAMETERS1
    {
        private readonly D2D1_LAYER_PARAMETERS1_COMMON layerParameters;
        public D2D1_LAYER_OPTIONS1 LayerOptions =>
            this.layerParameters.LayerOptions;
        public D2D1_LAYER_PARAMETERS1(D2D_RECT_F contentBounds, D2D1Geometry geometricMaskPointer, D2D1_ANTIALIAS_MODE maskAntialiasMode, D2D_MATRIX_3X2_F maskTransform, float opacity, D2D1Brush opacityBrushPointer, D2D1_LAYER_OPTIONS1 layerOptions)
        {
            this.layerParameters = new D2D1_LAYER_PARAMETERS1_COMMON(contentBounds, geometricMaskPointer.ToNativeObject(), maskAntialiasMode, maskTransform, opacity, opacityBrushPointer.ToNativeObject(), layerOptions);
        }
    }
}

