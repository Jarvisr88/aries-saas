namespace Devart.Data.MySql
{
    using System;
    using System.ComponentModel;
    using System.Data.SqlTypes;
    using System.Runtime.InteropServices;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    [StructLayout(LayoutKind.Sequential), TypeConverter(typeof(ay))]
    public struct MySqlDecimal : IComparable, INullable, IFormattable, IXmlSerializable, IConvertible
    {
        private string a;
        private const string b = ".";
        private const string c = "Null";
        public static bool UseInvariantCultureNumberFormat;
        public static readonly byte MaxPrecision;
        public static readonly byte MaxScale;
        public static readonly MySqlDecimal Null;
        public static readonly MySqlDecimal MaxValue;
        public static readonly MySqlDecimal MinValue;
        public MySqlDecimal(decimal value);
        public MySqlDecimal(double value);
        public MySqlDecimal(long value);
        public MySqlDecimal(int value);
        public static MySqlDecimal Parse(string value);
        public static MySqlDecimal Parse(string value, IFormatProvider provider);
        internal static MySqlDecimal a(string A_0);
        public static implicit operator MySqlDecimal(decimal value);
        public static implicit operator MySqlDecimal(long value);
        public static implicit operator MySqlDecimal(int value);
        public static explicit operator MySqlDecimal(double value);
        public double ToDouble();
        public static explicit operator decimal(MySqlDecimal value);
        public static long c(MySqlDecimal A_0);
        public static int b(MySqlDecimal A_0);
        public static double a(MySqlDecimal A_0);
        public override string ToString();
        public string ToString(IFormatProvider provider);
        public string ToString(string format, IFormatProvider formatProvider);
        public string ToString(string format);
        public int CompareTo(object obj);
        public int CompareTo(MySqlDecimal other);
        public override bool Equals(object other);
        public override int GetHashCode();
        public static bool operator ==(MySqlDecimal d1, MySqlDecimal d2);
        public static bool operator !=(MySqlDecimal d1, MySqlDecimal d2);
        public static bool operator >(MySqlDecimal d1, MySqlDecimal d2);
        public static bool operator >=(MySqlDecimal d1, MySqlDecimal d2);
        public static bool operator <(MySqlDecimal d1, MySqlDecimal d2);
        public static bool operator <=(MySqlDecimal d1, MySqlDecimal d2);
        private static bool a(string A_0, out decimal A_1);
        private static bool b(string A_0, string A_1);
        private static string a(IFormatProvider A_0);
        public bool IsNull { get; }
        public bool IsPositive { get; }
        public byte Precision { get; }
        public byte Scale { get; }
        public decimal Value { get; }
        private int a(string A_0, string A_1);
        public TypeCode GetTypeCode();
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
        XmlSchema IXmlSerializable.GetSchema();
        void IXmlSerializable.ReadXml(XmlReader reader);
        void IXmlSerializable.WriteXml(XmlWriter writer);
        static MySqlDecimal();
    }
}

