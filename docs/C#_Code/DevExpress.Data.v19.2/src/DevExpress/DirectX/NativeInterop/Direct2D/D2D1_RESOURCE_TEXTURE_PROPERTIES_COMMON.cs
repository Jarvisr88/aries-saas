namespace DevExpress.DirectX.NativeInterop.Direct2D
{
    using DevExpress.DirectX.Common.Direct2D;
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct D2D1_RESOURCE_TEXTURE_PROPERTIES_COMMON
    {
        private IntPtr extents;
        private int dimensions;
        private D2D1_BUFFER_PRECISION bufferPrecision;
        private D2D1_CHANNEL_DEPTH channelDepth;
        private D2D1_FILTER filter;
        private IntPtr extendModes;
        public D2D1_RESOURCE_TEXTURE_PROPERTIES_COMMON(IntPtr extents, int dimensions, D2D1_BUFFER_PRECISION bufferPrecision, D2D1_CHANNEL_DEPTH channelDepth, D2D1_FILTER filter, IntPtr extendModes)
        {
            this.extents = extents;
            this.dimensions = dimensions;
            this.bufferPrecision = bufferPrecision;
            this.channelDepth = channelDepth;
            this.filter = filter;
            this.extendModes = extendModes;
        }
    }
}

