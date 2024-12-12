namespace DevExpress.DirectX.Common.DirectWrite
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct DWRITE_UNDERLINE
    {
        private float width;
        private float thickness;
        private float offset;
        private float runHeight;
        private DWRITE_READING_DIRECTION readingDirection;
        private DWRITE_FLOW_DIRECTION flowDirection;
        private IntPtr localeName;
        private DWRITE_MEASURING_MODE measuringMode;
    }
}

