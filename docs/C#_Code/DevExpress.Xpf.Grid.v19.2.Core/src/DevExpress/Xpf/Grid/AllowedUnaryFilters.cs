namespace DevExpress.Xpf.Grid
{
    using System;

    [Flags]
    public enum AllowedUnaryFilters
    {
        None = 0,
        IsNull = 1,
        IsNotNull = 2,
        IsNullOrEmpty = 4,
        IsNotNullOrEmpty = 8,
        All = 15
    }
}

