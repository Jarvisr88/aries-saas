namespace DevExpress.DirectX.Common.Direct2D
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct D2D_POINT_2F
    {
        private static readonly D2D_POINT_2F empty;
        private float x;
        private float y;
        public static D2D_POINT_2F Empty =>
            empty;
        public float X
        {
            get => 
                this.x;
            set => 
                this.x = value;
        }
        public float Y
        {
            get => 
                this.y;
            set => 
                this.y = value;
        }
        public D2D_POINT_2F(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        static D2D_POINT_2F()
        {
        }
    }
}

