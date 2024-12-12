namespace DevExpress.DirectX.Common.Direct2D
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct D2D1_GRADIENT_STOP
    {
        private readonly float position;
        private readonly D2D1_COLOR_F color;
        public D2D1_GRADIENT_STOP(float position, D2D1_COLOR_F color)
        {
            this.position = position;
            this.color = color;
        }
    }
}

