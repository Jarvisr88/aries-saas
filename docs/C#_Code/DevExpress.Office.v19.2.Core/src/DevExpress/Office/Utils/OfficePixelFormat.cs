namespace DevExpress.Office.Utils
{
    using System;

    public enum OfficePixelFormat
    {
        DontCare = 0,
        Undefined = 0,
        Max = 15,
        Indexed = 0x10000,
        Gdi = 0x20000,
        Format16bppRgb555 = 0x21005,
        Format16bppRgb565 = 0x21006,
        Format24bppRgb = 0x21808,
        Format32bppRgb = 0x22009,
        Format1bppIndexed = 0x30101,
        Format4bppIndexed = 0x30402,
        Format8bppIndexed = 0x30803,
        Alpha = 0x40000,
        Format16bppArgb1555 = 0x61007,
        PAlpha = 0x80000,
        Format32bppPArgb = 0xe200b,
        Extended = 0x100000,
        Format16bppGrayScale = 0x101004,
        Format48bppRgb = 0x10300c,
        Format64bppPArgb = 0x1c400e,
        Canonical = 0x200000,
        Format32bppArgb = 0x26200a,
        Format64bppArgb = 0x34400d
    }
}

