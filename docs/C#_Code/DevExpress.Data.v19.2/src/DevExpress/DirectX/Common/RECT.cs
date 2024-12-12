namespace DevExpress.DirectX.Common
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        private readonly int left;
        private readonly int top;
        private readonly int right;
        private readonly int bottom;
        public RECT(int left, int top, int right, int bottom)
        {
            this.left = left;
            this.top = top;
            this.right = right;
            this.bottom = bottom;
        }
    }
}

