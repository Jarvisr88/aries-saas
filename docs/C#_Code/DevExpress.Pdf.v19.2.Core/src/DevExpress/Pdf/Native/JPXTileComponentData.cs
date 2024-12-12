namespace DevExpress.Pdf.Native
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct JPXTileComponentData
    {
        private readonly int bitsPerComponent;
        private readonly float[] data;
        public int BitsPerComponent =>
            this.bitsPerComponent;
        public float[] Data =>
            this.data;
        public JPXTileComponentData(int bitsPerComponent, float[] data)
        {
            this.bitsPerComponent = bitsPerComponent;
            this.data = data;
        }
    }
}

