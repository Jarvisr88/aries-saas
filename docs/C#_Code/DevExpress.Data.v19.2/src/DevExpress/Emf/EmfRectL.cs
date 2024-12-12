namespace DevExpress.Emf
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct EmfRectL
    {
        private readonly int left;
        private readonly int top;
        private readonly int right;
        private readonly int bottom;
        public int Left =>
            this.left;
        public int Top =>
            this.top;
        public int Right =>
            this.right;
        public int Bottom =>
            this.bottom;
        public EmfRectL(int left, int top, int right, int bottom)
        {
            this.left = left;
            this.top = top;
            this.right = right;
            this.bottom = bottom;
        }

        public EmfRectL(BinaryReader reader) : this(reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32())
        {
        }
    }
}

