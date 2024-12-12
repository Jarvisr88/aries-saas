namespace DevExpress.Xpf.Editors.DateNavigator
{
    using System;

    [Flags]
    public enum DateNavigatorCellState
    {
        None = 0,
        IsDisabled = 1,
        IsSpecialDate = 2,
        IsHoliday = 4
    }
}

