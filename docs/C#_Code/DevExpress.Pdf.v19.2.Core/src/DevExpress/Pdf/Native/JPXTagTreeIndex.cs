namespace DevExpress.Pdf.Native
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct JPXTagTreeIndex
    {
        private readonly int x;
        private readonly int y;
        public int X =>
            this.x;
        public int Y =>
            this.y;
        public JPXTagTreeIndex(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}

