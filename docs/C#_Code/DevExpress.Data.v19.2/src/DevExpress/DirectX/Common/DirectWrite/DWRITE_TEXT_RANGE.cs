namespace DevExpress.DirectX.Common.DirectWrite
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct DWRITE_TEXT_RANGE
    {
        private readonly int startPosition;
        private readonly int length;
        public int StartPosition =>
            this.startPosition;
        public int Length =>
            this.length;
        public DWRITE_TEXT_RANGE(int startPosition, int length)
        {
            this.startPosition = startPosition;
            this.length = length;
        }
    }
}

