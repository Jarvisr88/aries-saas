namespace DevExpress.DirectX.Common.Direct2D
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct D2D1_MAPPED_RECT
    {
        private readonly int pitch;
        private readonly IntPtr bits;
        public int Pitch =>
            this.pitch;
        public IntPtr Bits =>
            this.bits;
    }
}

