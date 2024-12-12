﻿namespace DevExpress.DirectX.Common.WIC
{
    using System;

    public enum WICBitmapDitherType
    {
        None = 0,
        Solid = 0,
        Ordered4x4 = 1,
        Ordered8x8 = 2,
        Ordered16x16 = 3,
        Spiral4x4 = 4,
        Spiral8x8 = 5,
        DualSpiral4x4 = 6,
        DualSpiral8x8 = 7,
        ErrorDiffusion = 8,
        FORCE_DWORD = 0x7fffffff
    }
}

