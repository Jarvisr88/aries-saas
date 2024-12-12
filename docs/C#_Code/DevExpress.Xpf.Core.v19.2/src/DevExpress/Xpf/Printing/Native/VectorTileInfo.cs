namespace DevExpress.Xpf.Printing.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows.Media;

    [StructLayout(LayoutKind.Sequential)]
    public struct VectorTileInfo
    {
        public Matrix Transform { get; private set; }
        public System.Windows.Media.Visual Visual { get; private set; }
        public VectorTileInfo(System.Windows.Media.Visual visual, Matrix transform)
        {
            this.Transform = transform;
            this.Visual = visual;
        }
    }
}

