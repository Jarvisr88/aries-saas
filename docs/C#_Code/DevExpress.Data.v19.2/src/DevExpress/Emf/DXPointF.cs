namespace DevExpress.Emf
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct DXPointF
    {
        private readonly float x;
        private readonly float y;
        public float X =>
            this.x;
        public float Y =>
            this.y;
        public DXPointF(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
    }
}

