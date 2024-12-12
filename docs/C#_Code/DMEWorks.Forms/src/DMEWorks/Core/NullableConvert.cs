namespace DMEWorks.Core
{
    using System;
    using System.Globalization;
    using System.Text;

    public static class NullableConvert
    {
        public static int Compare<T>(T? x, T? y) where T: struct, IComparable<T> => 
            (x == null) ? ((y != null) ? -1 : 0) : ((y != null) ? x.Value.CompareTo(y.Value) : 1);

        public static bool Equals<T>(T? x, T? y) where T: struct, IComparable<T> => 
            Compare<T>(x, y) == 0;

        public static bool? ToBoolean(object value)
        {
            if (value as bool)
            {
                return new bool?((bool) value);
            }
            if (value != null)
            {
                if (value is DBNull)
                {
                    return null;
                }
                try
                {
                    if (value is IConvertible)
                    {
                        return new bool?(((IConvertible) value).ToBoolean(CultureInfo.InvariantCulture));
                    }
                }
                catch
                {
                }
            }
            return null;
        }

        public static bool ToBoolean(object value, bool @default) => 
            ToBoolean(value).GetValueOrDefault(@default);

        public static byte? ToByte(object value)
        {
            if (value is byte)
            {
                return new byte?((byte) value);
            }
            if (value != null)
            {
                if (value is DBNull)
                {
                    return null;
                }
                try
                {
                    if (value is IConvertible)
                    {
                        return new byte?(((IConvertible) value).ToByte(CultureInfo.InvariantCulture));
                    }
                }
                catch
                {
                }
            }
            return null;
        }

        public static byte ToByte(object value, byte @default) => 
            ToByte(value).GetValueOrDefault(@default);

        public static byte[] ToBytes(object value) => 
            ToBytes(value, null);

        public static byte[] ToBytes(object value, byte[] @default)
        {
            switch (value)
            {
                case (byte[] _):
                    return (byte[]) value;
                    break;
            }
            if (value == null)
            {
                return @default;
            }
            if (value is DBNull)
            {
                return @default;
            }
            if (value is string)
            {
                return Encoding.UTF8.GetBytes((string) value);
            }
            if (value is byte)
            {
                return new byte[] { ((byte) value) };
            }
            if (value is sbyte)
            {
                return new byte[] { ((byte) ((sbyte) value)) };
            }
            return (!(value is Guid) ? (!(value as bool) ? (!(value is char) ? (!(value is double) ? (!(value is float) ? (!(value is int) ? (!(value is long) ? (!(value is short) ? (!(value is uint) ? (!(value is ulong) ? (!(value is ushort) ? @default : BitConverter.GetBytes((ushort) value)) : BitConverter.GetBytes((ulong) value)) : BitConverter.GetBytes((uint) value)) : BitConverter.GetBytes((short) value)) : BitConverter.GetBytes((long) value)) : BitConverter.GetBytes((int) value)) : BitConverter.GetBytes((float) value)) : BitConverter.GetBytes((double) value)) : BitConverter.GetBytes((char) value)) : BitConverter.GetBytes((bool) value)) : ((Guid) value).ToByteArray());
        }

        public static DateTime? ToDate(DateTime? value)
        {
            if (value != null)
            {
                return new DateTime?(value.Value.Date);
            }
            return null;
        }

        public static DateTime? ToDate(object value)
        {
            DateTime? nullable = ToDateTime(value);
            if (nullable != null)
            {
                return new DateTime?(nullable.Value.Date);
            }
            return null;
        }

        public static DateTime ToDate(object value, DateTime @default) => 
            ToDateTime(value, @default).Date;

        public static DateTime? ToDateTime(object value)
        {
            if (value is DateTime)
            {
                return new DateTime?((DateTime) value);
            }
            if (value != null)
            {
                if (value is DBNull)
                {
                    return null;
                }
                try
                {
                    if (value is IConvertible)
                    {
                        return new DateTime?(((IConvertible) value).ToDateTime(CultureInfo.InvariantCulture));
                    }
                }
                catch
                {
                }
            }
            return null;
        }

        public static DateTime ToDateTime(object value, DateTime @default) => 
            ToDateTime(value).GetValueOrDefault(@default);

        public static object ToDb(object value) => 
            value ?? DBNull.Value;

        public static decimal? ToDecimal(object value)
        {
            if (value is decimal)
            {
                return new decimal?((decimal) value);
            }
            if (value != null)
            {
                if (value is DBNull)
                {
                    return null;
                }
                try
                {
                    if (value is IConvertible)
                    {
                        return new decimal?(((IConvertible) value).ToDecimal(CultureInfo.InvariantCulture));
                    }
                }
                catch
                {
                }
            }
            return null;
        }

        public static decimal ToDecimal(object value, decimal @default) => 
            ToDecimal(value).GetValueOrDefault(@default);

        public static double? ToDouble(object value)
        {
            if (value is double)
            {
                return new double?((double) value);
            }
            if (value != null)
            {
                if (value is DBNull)
                {
                    return null;
                }
                try
                {
                    if (value is IConvertible)
                    {
                        return new double?(((IConvertible) value).ToDouble(CultureInfo.InvariantCulture));
                    }
                }
                catch
                {
                }
            }
            return null;
        }

        public static double ToDouble(object value, double @default) => 
            ToDouble(value).GetValueOrDefault(@default);

        public static short? ToInt16(object value)
        {
            if (value is short)
            {
                return new short?((short) value);
            }
            if (value != null)
            {
                if (value is DBNull)
                {
                    return null;
                }
                try
                {
                    if (value is IConvertible)
                    {
                        return new short?(((IConvertible) value).ToInt16(CultureInfo.InvariantCulture));
                    }
                }
                catch
                {
                }
            }
            return null;
        }

        public static int? ToInt32(object value)
        {
            if (value is int)
            {
                return new int?((int) value);
            }
            if (value != null)
            {
                if (value is DBNull)
                {
                    return null;
                }
                try
                {
                    if (value is IConvertible)
                    {
                        return new int?(((IConvertible) value).ToInt32(CultureInfo.InvariantCulture));
                    }
                }
                catch
                {
                }
            }
            return null;
        }

        public static int ToInt32(object value, int @default)
        {
            if (value is int)
            {
                return (int) value;
            }
            if (value != null)
            {
                if (value is DBNull)
                {
                    return @default;
                }
                try
                {
                    if (value is IConvertible)
                    {
                        return ((IConvertible) value).ToInt32(CultureInfo.InvariantCulture);
                    }
                }
                catch
                {
                }
            }
            return @default;
        }

        public static long? ToInt64(object value)
        {
            if (value is long)
            {
                return new long?((long) value);
            }
            if (value != null)
            {
                if (value is DBNull)
                {
                    return null;
                }
                try
                {
                    if (value is IConvertible)
                    {
                        return new long?(((IConvertible) value).ToInt64(CultureInfo.InvariantCulture));
                    }
                }
                catch
                {
                }
            }
            return null;
        }

        public static long ToInt64(object value, long @default) => 
            ToInt64(value).GetValueOrDefault(@default);

        public static float? ToSingle(object value)
        {
            if (value is float)
            {
                return new float?((float) value);
            }
            if (value != null)
            {
                if (value is DBNull)
                {
                    return null;
                }
                try
                {
                    if (value is IConvertible)
                    {
                        return new float?(((IConvertible) value).ToSingle(CultureInfo.InvariantCulture));
                    }
                }
                catch
                {
                }
            }
            return null;
        }

        public static string ToString(object value) => 
            ToString(value, "");

        public static string ToString(object value, string @default)
        {
            switch (value)
            {
                case (string _):
                    return (string) value;
                    break;
            }
            if (value != null)
            {
                if (value is DBNull)
                {
                    return @default;
                }
                if (value is byte[])
                {
                    return Encoding.UTF8.GetString((byte[]) value);
                }
                try
                {
                    if (value is IConvertible)
                    {
                        return ((IConvertible) value).ToString(CultureInfo.InvariantCulture);
                    }
                }
                catch
                {
                }
            }
            return @default;
        }

        [CLSCompliant(false)]
        public static ushort? ToUInt16(object value)
        {
            if (value is int)
            {
                return new ushort?((ushort) value);
            }
            if (value != null)
            {
                if (value is DBNull)
                {
                    return null;
                }
                try
                {
                    if (value is IConvertible)
                    {
                        return new ushort?(((IConvertible) value).ToUInt16(CultureInfo.InvariantCulture));
                    }
                }
                catch
                {
                }
            }
            return null;
        }

        [CLSCompliant(false)]
        public static ushort ToUInt16(object value, ushort @default) => 
            ToUInt16(value).GetValueOrDefault(@default);
    }
}

