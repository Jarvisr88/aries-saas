namespace DevExpress.Text.Fonts
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct DXSize
    {
        public int Width { get; }
        public int Height { get; }
        public DXSize(int width, int height)
        {
            this.<Width>k__BackingField = width;
            this.<Height>k__BackingField = height;
        }
    }
}

