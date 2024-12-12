namespace DevExpress.Emf
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct ARGBColor
    {
        private byte a;
        private byte r;
        private byte g;
        private byte b;
        public static ARGBColor FromArgb(byte a, byte r, byte g, byte b) => 
            new ARGBColor { 
                a = a,
                r = r,
                g = g,
                b = b
            };

        public static ARGBColor FromArgb(int value) => 
            FromArgb((byte) (value >> 0x18), (byte) (value >> 0x10), (byte) (value >> 8), (byte) value);

        public static ARGBColor FromArgb(byte r, byte g, byte b) => 
            FromArgb(0xff, r, g, b);

        public byte A
        {
            get => 
                this.a;
            set => 
                this.a = value;
        }
        public byte R
        {
            get => 
                this.r;
            set => 
                this.r = value;
        }
        public byte G
        {
            get => 
                this.g;
            set => 
                this.g = value;
        }
        public byte B
        {
            get => 
                this.b;
            set => 
                this.b = value;
        }
        public int ToArgb() => 
            (((this.a << 0x18) | (this.r << 0x10)) | (this.g << 8)) | this.b;
    }
}

