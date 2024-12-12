namespace DevExpress.Utils.Serializing
{
    using System;

    public enum DXTypeCode
    {
        Null = 0,
        Object = 0xee,
        DBNull = 0xdd,
        Boolean = 10,
        Char = 0xcc,
        SByte = 1,
        Byte = 2,
        Int16 = 3,
        UInt16 = 4,
        Int32 = 15,
        UInt32 = 5,
        Int64 = 6,
        UInt64 = 7,
        Single = 0xbb,
        Double = 170,
        Decimal = 8,
        DateTime = 9,
        TimeSpan = 11,
        String = 0x77,
        Guid = 0x88,
        ByteArray = 0x99,
        Enum = 0x66
    }
}

