namespace DevExpress.DirectX.Common.DirectWrite
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct DWRITE_GLYPH_OFFSET
    {
        private readonly float advanceOffset;
        private readonly float ascenderOffset;
        public float AdvanceOffset =>
            this.advanceOffset;
        public float AscenderOffset =>
            this.ascenderOffset;
        public DWRITE_GLYPH_OFFSET(float advanceOffset, float ascenderOffset)
        {
            this.advanceOffset = advanceOffset;
            this.ascenderOffset = ascenderOffset;
        }
    }
}

