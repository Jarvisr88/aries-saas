namespace Devart.Data.MySql
{
    using System;
    using System.ComponentModel;
    using System.Data.SqlTypes;
    using System.Globalization;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    [Serializable, StructLayout(LayoutKind.Sequential), TypeConverter(typeof(n))]
    internal struct MySqlDateTime : IComparable, INullable, IFormattable, IXmlSerializable, IConvertible
    {
        private int year;
        private int month;
        private int day;
        private int hour;
        private int minute;
        private int second;
        private int microsecond;
        private bool isNull;
        private const string RecognizedDateTimeFormatException = "String was not recognized as a valid DateTime.";
        private const string NotCorrectFormatException = "Input string was not in a correct format.";
        private static readonly string ISO8601_DateTimeFormat;
        private static readonly int[] DaysToMonth;
        public static readonly MySqlDateTime Null;
        static MySqlDateTime();
        public MySqlDateTime(DateTime dt);
        public MySqlDateTime(int year, int month, int day);
        public MySqlDateTime(int year, int month, int day, int hour, int minute, int second);
        public MySqlDateTime(int year, int month, int day, int hour, int minute, int second, int microsecond);
        private MySqlDateTime(int year, int month, int day, int hour, int minute, int second, int microsecond, bool isNull);
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public int Second { get; set; }
        public int Microsecond { get; set; }
        public bool IsValidDateTime { get; }
        public DateTime Value { get; }
        public bool IsNull { get; }
        public static implicit operator MySqlDateTime(DateTime value);
        public static explicit operator DateTime(MySqlDateTime value);
        public override string ToString();
        public string ToString(string format);
        public string ToString(IFormatProvider provider);
        public string ToString(string format, IFormatProvider formatProvider);
        TypeCode IConvertible.GetTypeCode();
        bool IConvertible.ToBoolean(IFormatProvider provider);
        byte IConvertible.ToByte(IFormatProvider provider);
        char IConvertible.ToChar(IFormatProvider provider);
        DateTime IConvertible.ToDateTime(IFormatProvider provider);
        decimal IConvertible.ToDecimal(IFormatProvider provider);
        double IConvertible.ToDouble(IFormatProvider provider);
        short IConvertible.ToInt16(IFormatProvider provider);
        int IConvertible.ToInt32(IFormatProvider provider);
        long IConvertible.ToInt64(IFormatProvider provider);
        sbyte IConvertible.ToSByte(IFormatProvider provider);
        float IConvertible.ToSingle(IFormatProvider provider);
        string IConvertible.ToString(IFormatProvider provider);
        object IConvertible.ToType(Type conversionType, IFormatProvider provider);
        ushort IConvertible.ToUInt16(IFormatProvider provider);
        uint IConvertible.ToUInt32(IFormatProvider provider);
        ulong IConvertible.ToUInt64(IFormatProvider provider);
        public int CompareTo(object obj);
        public int CompareTo(MySqlDateTime other);
        public override bool Equals(object obj);
        public override int GetHashCode();
        XmlSchema IXmlSerializable.GetSchema();
        void IXmlSerializable.ReadXml(XmlReader reader);
        void IXmlSerializable.WriteXml(XmlWriter writer);
        public static MySqlDateTime Parse(string value);
        public static MySqlDateTime Parse(string value, IFormatProvider provider);
        public static MySqlDateTime Parse(string value, string format, IFormatProvider provider);
        public static bool operator ==(MySqlDateTime d1, MySqlDateTime d2);
        public static bool operator !=(MySqlDateTime d1, MySqlDateTime d2);
        public static bool operator >(MySqlDateTime d1, MySqlDateTime d2);
        public static bool operator >=(MySqlDateTime d1, MySqlDateTime d2);
        public static bool operator <(MySqlDateTime d1, MySqlDateTime d2);
        public static bool operator <=(MySqlDateTime d1, MySqlDateTime d2);
        private void AddTimeZoneOffset(TimeSpan value);
        private static MySqlDateTime FromCustomFormat(string value, string format, IFormatProvider provider);
        private static string ToCustomFormat(string format, IFormatProvider formatProvider, int yearVal, int monthVal, int dayVal, int hourVal, int minuteVal, int secondVal, int microsecondVal);
        private static int ParseRepeatSpecifier(string format, int pos, char specifierChar);
        private static void TimeZoneFormat(DateTime datetime, int specifierLen, StringBuilder buffer);
        private static void MicrosecondFormat(int microsecond, int specifierLen, bool skipTrailingZeros, StringBuilder buffer);
        private static int ParseQuoteString(string format, int pos, StringBuilder buffer);
        private static int ConvertTwoDigitYearToFullYear(int twoDigitYear, Calendar calendar);
        private static DateTime GetApproximateValidDateTime(int year, int month, int day, int hour, int minute, int second, int microsecond);
        private int InternalGetHashCode();
    }
}

