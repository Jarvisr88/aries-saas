namespace DevExpress.Xpf.Core.FilteringUI
{
    using System;

    [Flags]
    public enum AllowedBinaryFilters
    {
        None = 0,
        Equals = 1,
        DoesNotEqual = 2,
        Greater = 4,
        GreaterOrEqual = 8,
        Less = 0x10,
        LessOrEqual = 0x20,
        Contains = 0x40,
        DoesNotContain = 0x80,
        BeginsWith = 0x100,
        EndsWith = 0x200,
        Like = 0x400,
        NotLike = 0x800,
        All = 0xfff
    }
}

