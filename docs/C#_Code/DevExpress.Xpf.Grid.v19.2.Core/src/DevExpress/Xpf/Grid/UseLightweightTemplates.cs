namespace DevExpress.Xpf.Grid
{
    using System;

    [Flags]
    public enum UseLightweightTemplates
    {
        None = 0,
        Row = 1,
        GroupRow = 2,
        NewItemRow = 4,
        All = 7
    }
}

