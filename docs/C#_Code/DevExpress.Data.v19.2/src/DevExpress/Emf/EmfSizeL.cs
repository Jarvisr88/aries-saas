namespace DevExpress.Emf
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct EmfSizeL
    {
        private readonly int cx;
        private readonly int cy;
        public int Cx =>
            this.cx;
        public int Cy =>
            this.cy;
        public EmfSizeL(int cx, int cy)
        {
            this.cx = cx;
            this.cy = cy;
        }

        public EmfSizeL(BinaryReader reader) : this(reader.ReadInt32(), reader.ReadInt32())
        {
        }
    }
}

