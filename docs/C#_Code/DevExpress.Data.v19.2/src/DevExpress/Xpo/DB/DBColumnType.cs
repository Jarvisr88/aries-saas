namespace DevExpress.Xpo.DB
{
    using System;

    public enum DBColumnType
    {
        Unknown = 0,
        Boolean = 1,
        Byte = 2,
        SByte = 3,
        Char = 4,
        Decimal = 5,
        Double = 6,
        Single = 7,
        Int32 = 8,
        UInt32 = 9,
        Int16 = 10,
        UInt16 = 11,
        Int64 = 12,
        UInt64 = 13,
        String = 14,
        DateTime = 15,
        Guid = 0x10,
        [Obsolete("BC3523")]
        TimeSpan = 0x11,
        ByteArray = 0x12
    }
}

