namespace DevExpress.DirectX.Common.Direct2D
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct D2D1_IMAGE_BRUSH_PROPERTIES
    {
        private readonly D2D_RECT_F sourceRectangle;
        private readonly D2D1_EXTEND_MODE extendModeX;
        private readonly D2D1_EXTEND_MODE extendModeY;
        private readonly D2D1_INTERPOLATION_MODE interpolationMode;
        public D2D1_IMAGE_BRUSH_PROPERTIES(D2D_RECT_F sourceRectangle, D2D1_EXTEND_MODE extendModeX, D2D1_EXTEND_MODE extendModeY, D2D1_INTERPOLATION_MODE interpolationMode)
        {
            this.sourceRectangle = sourceRectangle;
            this.extendModeX = extendModeX;
            this.extendModeY = extendModeY;
            this.interpolationMode = interpolationMode;
        }
    }
}

