namespace DevExpress.XtraPrinting
{
    using DevExpress.Data;
    using DevExpress.Utils.Design;
    using System;
    using System.ComponentModel;

    [TypeConverter(typeof(EnumTypeConverter)), ResourceFinder(typeof(ResFinder))]
    public enum TextAlignment
    {
        TopLeft = 1,
        TopCenter = 2,
        TopRight = 4,
        MiddleLeft = 0x10,
        MiddleCenter = 0x20,
        MiddleRight = 0x40,
        BottomLeft = 0x100,
        BottomCenter = 0x200,
        BottomRight = 0x400,
        TopJustify = 0x800,
        MiddleJustify = 0x1000,
        BottomJustify = 0x1fa0
    }
}

