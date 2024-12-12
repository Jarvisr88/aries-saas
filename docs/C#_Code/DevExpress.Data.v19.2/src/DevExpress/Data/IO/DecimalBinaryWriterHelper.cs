namespace DevExpress.Data.IO
{
    using System;
    using System.Runtime.CompilerServices;

    internal static class DecimalBinaryWriterHelper
    {
        public const decimal MaxInt64Value = 9223372036854775807M;
        public const decimal MinInt64Value = -9223372036854775808M;

        static DecimalBinaryWriterHelper();
        public static bool CanConvertToInt64(decimal value);
    }
}

