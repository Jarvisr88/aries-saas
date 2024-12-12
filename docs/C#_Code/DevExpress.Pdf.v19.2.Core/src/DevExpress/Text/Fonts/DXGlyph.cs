namespace DevExpress.Text.Fonts
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct DXGlyph
    {
        internal ushort Index { get; }
        public float Advance { get; }
        public DXGlyphOffset Offset { get; }
        internal DXGlyph(ushort index, float advance, DXGlyphOffset offset)
        {
            this.<Index>k__BackingField = index;
            this.<Advance>k__BackingField = advance;
            this.<Offset>k__BackingField = offset;
        }
    }
}

