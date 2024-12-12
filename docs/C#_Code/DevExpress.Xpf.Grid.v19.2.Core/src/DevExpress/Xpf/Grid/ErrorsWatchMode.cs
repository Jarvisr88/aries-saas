namespace DevExpress.Xpf.Grid
{
    using System;

    [Flags]
    public enum ErrorsWatchMode
    {
        None = 0,
        Default = 1,
        Rows = 2,
        Cells = 4,
        All = 6
    }
}

