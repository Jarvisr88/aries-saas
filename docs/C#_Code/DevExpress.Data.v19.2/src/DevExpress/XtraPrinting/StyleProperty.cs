namespace DevExpress.XtraPrinting
{
    using System;

    [Flags]
    public enum StyleProperty
    {
        None = 0,
        BackColor = 1,
        ForeColor = 2,
        BorderColor = 4,
        Font = 8,
        BorderDashStyle = 0x10,
        Borders = 0x20,
        BorderWidth = 0x40,
        TextAlignment = 0x80,
        Padding = 0x100,
        All = 0x1ff
    }
}

