namespace DevExpress.DirectX.Common.Direct2D
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct D2D1_BITMAP_BRUSH_PROPERTIES1
    {
        private readonly D2D1_EXTEND_MODE extendModeX;
        private readonly D2D1_EXTEND_MODE extendModeY;
        private readonly D2D1_INTERPOLATION_MODE interpolationMode;
        public D2D1_BITMAP_BRUSH_PROPERTIES1(D2D1_INTERPOLATION_MODE interpolationMode)
        {
            this.extendModeX = D2D1_EXTEND_MODE.Clamp;
            this.extendModeY = D2D1_EXTEND_MODE.Clamp;
            this.interpolationMode = interpolationMode;
        }
    }
}

