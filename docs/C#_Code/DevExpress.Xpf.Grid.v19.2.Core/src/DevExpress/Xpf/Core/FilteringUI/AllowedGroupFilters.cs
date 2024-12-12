namespace DevExpress.Xpf.Core.FilteringUI
{
    using System;

    [Flags]
    public enum AllowedGroupFilters
    {
        None = 0,
        And = 1,
        Or = 2,
        NotAnd = 4,
        NotOr = 8,
        All = 15
    }
}

