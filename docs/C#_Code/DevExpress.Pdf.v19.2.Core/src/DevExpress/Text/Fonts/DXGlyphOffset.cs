namespace DevExpress.Text.Fonts
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct DXGlyphOffset
    {
        public static readonly DXGlyphOffset Empty;
        public float HorizontalOffset { get; }
        public float VerticalOffset { get; }
        public DXGlyphOffset(float horizontalOffset, float verticalOffset)
        {
            this.<HorizontalOffset>k__BackingField = horizontalOffset;
            this.<VerticalOffset>k__BackingField = verticalOffset;
        }

        static DXGlyphOffset()
        {
        }
    }
}

