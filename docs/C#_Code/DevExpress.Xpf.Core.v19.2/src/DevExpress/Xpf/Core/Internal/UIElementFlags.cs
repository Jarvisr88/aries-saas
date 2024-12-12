namespace DevExpress.Xpf.Core.Internal
{
    using System;

    [Flags]
    public enum UIElementFlags : uint
    {
        None = 0,
        SnapsToDevicePixelsCache = 1,
        ClipToBoundsCache = 2,
        MeasureDirty = 4,
        ArrangeDirty = 8,
        MeasureInProgress = 0x10,
        ArrangeInProgress = 0x20,
        NeverMeasured = 0x40,
        NeverArranged = 0x80,
        MeasureDuringArrange = 0x100,
        IsCollapsed = 0x200,
        IsKeyboardFocusWithinCache = 0x400,
        IsKeyboardFocusWithinChanged = 0x800,
        IsMouseOverCache = 0x1000,
        IsMouseOverChanged = 0x2000,
        IsMouseCaptureWithinCache = 0x4000,
        IsMouseCaptureWithinChanged = 0x8000,
        IsStylusOverCache = 0x10000,
        IsStylusOverChanged = 0x20000,
        IsStylusCaptureWithinCache = 0x40000,
        IsStylusCaptureWithinChanged = 0x80000,
        HasAutomationPeer = 0x100000,
        RenderingInvalidated = 0x200000,
        IsVisibleCache = 0x400000,
        AreTransformsClean = 0x800000,
        IsOpacitySuppressed = 0x1000000,
        ExistsEventHandlersStore = 0x2000000,
        TouchesOverCache = 0x4000000,
        TouchesOverChanged = 0x8000000,
        TouchesCapturedWithinCache = 0x10000000,
        TouchesCapturedWithinChanged = 0x20000000,
        TouchLeaveCache = 0x40000000,
        TouchEnterCache = 0x80000000
    }
}

