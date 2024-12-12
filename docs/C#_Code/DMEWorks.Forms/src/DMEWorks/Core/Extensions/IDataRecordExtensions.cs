namespace DMEWorks.Core.Extensions
{
    using DMEWorks.Core;
    using System;
    using System.Data;
    using System.Runtime.CompilerServices;

    public static class IDataRecordExtensions
    {
        public static bool? GetBoolean(this IDataRecord record, string name) => 
            NullableConvert.ToBoolean(record.GetValue(name));

        public static byte? GetByte(this IDataRecord record, string name) => 
            NullableConvert.ToByte(record.GetValue(name));

        public static byte[] GetBytes(this IDataRecord record, string name) => 
            NullableConvert.ToBytes(record.GetValue(name));

        public static DateTime? GetDateTime(this IDataRecord record, string name) => 
            NullableConvert.ToDateTime(record.GetValue(name));

        public static decimal? GetDecimal(this IDataRecord record, string name) => 
            NullableConvert.ToDecimal(record.GetValue(name));

        public static double? GetDouble(this IDataRecord record, string name) => 
            NullableConvert.ToDouble(record.GetValue(name));

        public static float? GetFloat(this IDataRecord record, string name) => 
            NullableConvert.ToSingle(record.GetValue(name));

        public static Guid? GetGuid(this IDataRecord record, string name)
        {
            if (record == null)
            {
                throw new ArgumentNullException("record");
            }
            int ordinal = record.GetOrdinal(name);
            if (!record.IsDBNull(ordinal))
            {
                return new Guid?(record.GetGuid(ordinal));
            }
            return null;
        }

        public static short? GetInt16(this IDataRecord record, string name) => 
            NullableConvert.ToInt16(record.GetValue(name));

        public static int? GetInt32(this IDataRecord record, string name) => 
            NullableConvert.ToInt32(record.GetValue(name));

        public static long? GetInt64(this IDataRecord record, string name) => 
            NullableConvert.ToInt64(record.GetValue(name));

        public static string GetString(this IDataRecord record, string name) => 
            NullableConvert.ToString(record.GetValue(name));

        public static object GetValue(this IDataRecord record, string name)
        {
            if (record == null)
            {
                throw new ArgumentNullException("record");
            }
            int ordinal = record.GetOrdinal(name);
            return (!record.IsDBNull(ordinal) ? record.GetValue(ordinal) : DBNull.Value);
        }
    }
}

