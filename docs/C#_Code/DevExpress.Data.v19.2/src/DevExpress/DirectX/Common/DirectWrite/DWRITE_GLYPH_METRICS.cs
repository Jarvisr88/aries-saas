namespace DevExpress.DirectX.Common.DirectWrite
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct DWRITE_GLYPH_METRICS
    {
        private int leftSideBearing;
        private int advanceWidth;
        private int rightSideBearing;
        private int topSideBearing;
        private int advanceHeight;
        private int bottomSideBearing;
        private int verticalOriginY;
        public int AdvanceWidth =>
            this.advanceWidth;
    }
}

