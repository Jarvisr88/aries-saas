namespace ActiproSoftware.WinUICore.Input
{
    using System;

    [Flags]
    public enum InputAdapterEventKinds
    {
        None = 0,
        PointerCaptureLost = 1,
        PointerEntered = 2,
        PointerExited = 4,
        PointerMoved = 8,
        PointerPressed = 0x10,
        PointerReleased = 0x20,
        PointerWheelChanged = 0x40,
        All = 0x7f
    }
}

