namespace DevExpress.DirectX.Common.DirectWrite
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct DWRITE_UNICODE_RANGE
    {
        private readonly int first;
        private readonly int last;
        public int First =>
            this.first;
        public int Last =>
            this.last;
    }
}

