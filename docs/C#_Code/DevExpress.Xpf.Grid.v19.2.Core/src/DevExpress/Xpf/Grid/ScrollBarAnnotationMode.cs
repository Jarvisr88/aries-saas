namespace DevExpress.Xpf.Grid
{
    using System;

    [Flags]
    public enum ScrollBarAnnotationMode
    {
        None = 0,
        InvalidRows = 1,
        InvalidCells = 2,
        Selected = 4,
        SearchResult = 8,
        Custom = 0x10,
        FocusedRow = 0x20,
        All = 0x3f
    }
}

