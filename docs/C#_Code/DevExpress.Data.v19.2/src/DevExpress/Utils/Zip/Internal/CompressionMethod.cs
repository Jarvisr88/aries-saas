namespace DevExpress.Utils.Zip.Internal
{
    using System;

    public enum CompressionMethod
    {
        Store = 0,
        Shrunke = 1,
        Reduce1 = 2,
        Reduce2 = 3,
        Reduce3 = 4,
        Reduce4 = 5,
        Implode = 6,
        TokenizingCompression = 7,
        Deflate = 8,
        Deflate64 = 9,
        PkWareImplode = 10,
        BZip2 = 12,
        LZMA = 14,
        IbmTerse = 0x12,
        LZ77 = 0x13,
        PPMd11 = 0x62,
        AESEncryption = 0x63
    }
}

