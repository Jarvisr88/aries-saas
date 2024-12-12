namespace DevExpress.Xpf.Editors.DateNavigator.Controls
{
    using System;

    [Flags]
    public enum DateNavigatorCalendarCellState
    {
        Focused = 1,
        Holiday = 2,
        Inactive = 4,
        Selected = 8,
        Special = 0x10,
        Today = 0x20,
        Disabled = 0x40
    }
}

