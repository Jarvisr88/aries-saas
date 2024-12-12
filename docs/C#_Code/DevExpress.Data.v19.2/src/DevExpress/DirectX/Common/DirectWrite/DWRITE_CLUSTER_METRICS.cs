namespace DevExpress.DirectX.Common.DirectWrite
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct DWRITE_CLUSTER_METRICS
    {
        private float width;
        private short length;
        private short flags;
        public float Width =>
            this.width;
    }
}

