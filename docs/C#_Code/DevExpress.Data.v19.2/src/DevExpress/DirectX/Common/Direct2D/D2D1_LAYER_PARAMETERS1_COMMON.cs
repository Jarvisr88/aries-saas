namespace DevExpress.DirectX.Common.Direct2D
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct D2D1_LAYER_PARAMETERS1_COMMON
    {
        private readonly D2D_RECT_F contentBounds;
        private readonly IntPtr geometricMaskPointer;
        private readonly D2D1_ANTIALIAS_MODE maskAntialiasMode;
        private readonly D2D_MATRIX_3X2_F maskTransform;
        private readonly float opacity;
        private readonly IntPtr opacityBrushPointer;
        private readonly D2D1_LAYER_OPTIONS1 layerOptions;
        public D2D1_LAYER_OPTIONS1 LayerOptions =>
            this.layerOptions;
        public D2D1_LAYER_PARAMETERS1_COMMON(D2D_RECT_F contentBounds, IntPtr geometricMaskPointer, D2D1_ANTIALIAS_MODE maskAntialiasMode, D2D_MATRIX_3X2_F maskTransform, float opacity, IntPtr opacityBrushPointer, D2D1_LAYER_OPTIONS1 layerOptions)
        {
            this.contentBounds = contentBounds;
            this.geometricMaskPointer = geometricMaskPointer;
            this.maskAntialiasMode = maskAntialiasMode;
            this.maskTransform = maskTransform;
            this.opacity = opacity;
            this.opacityBrushPointer = opacityBrushPointer;
            this.layerOptions = layerOptions;
        }
    }
}

