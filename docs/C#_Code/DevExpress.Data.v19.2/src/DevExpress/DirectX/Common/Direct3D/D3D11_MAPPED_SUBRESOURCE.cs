namespace DevExpress.DirectX.Common.Direct3D
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct D3D11_MAPPED_SUBRESOURCE
    {
        private readonly IntPtr pData;
        private readonly int rowPitch;
        private readonly int depthPitch;
        public IntPtr PData =>
            this.pData;
        public int RowPitch =>
            this.rowPitch;
        public int DepthPitch =>
            this.depthPitch;
    }
}

