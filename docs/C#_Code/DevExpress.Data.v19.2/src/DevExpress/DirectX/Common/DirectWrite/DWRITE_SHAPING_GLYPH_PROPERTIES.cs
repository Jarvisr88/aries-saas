namespace DevExpress.DirectX.Common.DirectWrite
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct DWRITE_SHAPING_GLYPH_PROPERTIES
    {
        private readonly short value;
        public int Justification =>
            this.value & 15;
        public bool IsClusterStart =>
            (this.value & 0x10) != 0;
        public bool IsDiacritic =>
            (this.value & 0x20) != 0;
        public bool IsZeroWidthSpace =>
            (this.value & 0x40) != 0;
        public DWRITE_SHAPING_GLYPH_PROPERTIES(bool isClusterStart)
        {
            this.value = isClusterStart ? ((short) 0x10) : ((short) 0);
        }
    }
}

