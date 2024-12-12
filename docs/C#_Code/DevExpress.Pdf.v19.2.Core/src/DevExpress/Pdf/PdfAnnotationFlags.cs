namespace DevExpress.Pdf
{
    using System;

    [Flags]
    public enum PdfAnnotationFlags
    {
        None = 0,
        Invisible = 1,
        Hidden = 2,
        Print = 4,
        NoZoom = 8,
        NoRotate = 0x10,
        NoView = 0x20,
        ReadOnly = 0x40,
        Locked = 0x80,
        ToggleNoView = 0x100,
        LockedContents = 0x200
    }
}

