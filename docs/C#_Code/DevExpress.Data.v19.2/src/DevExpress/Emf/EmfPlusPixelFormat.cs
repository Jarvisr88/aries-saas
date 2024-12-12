namespace DevExpress.Emf
{
    using System;

    public enum EmfPlusPixelFormat
    {
        PixelFormatUndefined = 0,
        PixelFormat1bppIndexed = 0x30101,
        PixelFormat4bppIndexed = 0x30402,
        PixelFormat8bppIndexed = 0x30803,
        PixelFormat16bppGrayScale = 0x101004,
        PixelFormat16bppRGB555 = 0x21005,
        PixelFormat16bppRGB565 = 0x21006,
        PixelFormat16bppARGB1555 = 0x61007,
        PixelFormat24bppRGB = 0x21808,
        PixelFormat32bppRGB = 0x22009,
        PixelFormat32bppARGB = 0x26200a,
        PixelFormat32bppPARGB = 0xe200b,
        PixelFormat48bppRGB = 0x10300c,
        PixelFormat64bppARGB = 0x34400d,
        PixelFormat64bppPARGB = 0x1a400e
    }
}

