namespace DevExpress.Xpf.Grid
{
    using System;

    [Flags]
    internal enum SimpleBindingState
    {
        None = 0,
        Field = 1,
        Data = 2,
        Binding = 4,
        All = 7
    }
}

