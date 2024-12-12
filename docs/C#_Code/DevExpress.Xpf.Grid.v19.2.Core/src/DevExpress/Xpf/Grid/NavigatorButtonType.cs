namespace DevExpress.Xpf.Grid
{
    using System;

    [Flags]
    public enum NavigatorButtonType
    {
        MoveFirstRow = 1,
        MovePrevPage = 2,
        MovePrevRow = 4,
        MoveNextRow = 8,
        MoveNextPage = 0x10,
        MoveLastRow = 0x20,
        AddNewRow = 0x40,
        DeleteFocusedRow = 0x80,
        EditFocusedRow = 0x100,
        All = 0x1ff,
        Navigation = 0x3f,
        Editing = 0x1c0
    }
}

