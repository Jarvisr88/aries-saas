namespace DevExpress.Text.Fonts
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct DXPoint
    {
        public int X { get; }
        public int Y { get; }
        public DXPoint(int x, int y)
        {
            this.<X>k__BackingField = x;
            this.<Y>k__BackingField = y;
        }
    }
}

