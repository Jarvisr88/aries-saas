namespace DevExpress.DirectX.Common.Direct2D
{
    using System;

    [Flags]
    public enum D2D1_BITMAP_OPTIONS
    {
        None = 0,
        Target = 1,
        CannotDraw = 2,
        CpuRead = 4,
        GdiCompatible = 8
    }
}

