namespace DevExpress.Utils.Zip
{
    using System;

    [Flags]
    public enum ZipFlags
    {
        Encrypted = 1,
        ImplodeUse8kSlidingDictionary = 2,
        ImplodeUse4kSlidingDictionary = 0,
        ImplodeUse3ShannonFanoTrees = 4,
        ImplodeUse2ShannonFanoTrees = 0,
        DeflateNormalCompression = 0,
        DeflateMaximumCompression = 2,
        DeflateFastCompression = 4,
        DeflateSuperFastCompression = 7,
        LZMAEOSIndicatesEndOfStream = 1,
        UseDataFromDataDescriptor = 8,
        ArchiveContainsCompressedPatchedData = 0x20,
        StrongEncryption = 0x40,
        Unused1 = 0x80,
        Unused2 = 0x100,
        Unused3 = 0x200,
        Unused4 = 0x400,
        EFS = 0x800,
        LocalHeaderDataMasked = 0x2000
    }
}

