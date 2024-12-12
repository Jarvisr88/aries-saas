namespace DevExpress.DirectX.Common.Direct2D
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct D2D1_VERTEX_BUFFER_PROPERTIES
    {
        private int inputCount;
        private D2D1_VERTEX_USAGE usage;
        private IntPtr data;
        private int byteWidth;
        public D2D1_VERTEX_BUFFER_PROPERTIES(int inputCount, D2D1_VERTEX_USAGE usage, IntPtr data, int byteWidth)
        {
            this.inputCount = inputCount;
            this.usage = usage;
            this.data = data;
            this.byteWidth = byteWidth;
        }
    }
}

