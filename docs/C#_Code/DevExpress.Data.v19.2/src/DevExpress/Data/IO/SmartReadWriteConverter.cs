namespace DevExpress.Data.IO
{
    using System;

    internal class SmartReadWriteConverter
    {
        public static byte GetByteByType(Type type);
        public static Type GetTypeByByte(byte value);
    }
}

