namespace DevExpress.Emf
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct DXRectangleF
    {
        private readonly float x;
        private readonly float y;
        private readonly float width;
        private readonly float height;
        public float X =>
            this.x;
        public float Y =>
            this.y;
        public float Width =>
            this.width;
        public float Height =>
            this.height;
        public DXRectangleF(float x, float y, float width, float height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }
    }
}

