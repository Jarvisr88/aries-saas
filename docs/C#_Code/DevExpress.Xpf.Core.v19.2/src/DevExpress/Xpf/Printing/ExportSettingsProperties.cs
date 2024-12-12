namespace DevExpress.Xpf.Printing
{
    using System;

    [Flags]
    public enum ExportSettingsProperties
    {
        None = 0,
        TargetType = 1,
        Background = 2,
        Foreground = 4,
        BorderColor = 8,
        BorderThickness = 0x10,
        Url = 0x20,
        OnPageUpdater = 0x40,
        BorderDashStyle = 0x80,
        MergeValue = 0x100
    }
}

