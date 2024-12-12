namespace DevExpress.Text.Fonts
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct DXSizeF
    {
        public float Width { get; }
        public float Height { get; }
        public DXSizeF(float width, float height)
        {
            this.<Width>k__BackingField = width;
            this.<Height>k__BackingField = height;
        }
    }
}

