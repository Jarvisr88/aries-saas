namespace DevExpress.DirectX.Common.Direct2D
{
    using System;

    public enum D2D1_FILTER
    {
        MIN_MAG_MIP_POINT = 0,
        MIN_MAG_POINT_MIP_LINEAR = 1,
        MIN_POINT_MAG_LINEAR_MIP_POINT = 4,
        MIN_POINT_MAG_MIP_LINEAR = 5,
        MIN_LINEAR_MAG_MIP_POINT = 0x10,
        MIN_LINEAR_MAG_POINT_MIP_LINEAR = 0x11,
        MIN_MAG_LINEAR_MIP_POINT = 20,
        MIN_MAG_MIP_LINEAR = 0x15,
        ANISOTROPIC = 0x55
    }
}

