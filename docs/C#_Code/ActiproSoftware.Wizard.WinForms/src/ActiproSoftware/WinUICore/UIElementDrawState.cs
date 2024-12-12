namespace ActiproSoftware.WinUICore
{
    using System;

    [Flags]
    public enum UIElementDrawState
    {
        None = 0,
        Hot = 1,
        Pressed = 2,
        Checked = 4,
        Selected = 8,
        Disabled = 0x10
    }
}

