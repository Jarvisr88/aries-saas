namespace Devart.Data.MySql
{
    using System;
    using System.ComponentModel;
    using System.Data.SqlTypes;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    [StructLayout(LayoutKind.Sequential), TypeConverter(typeof(ag))]
    public struct MySqlBinaryString : IComparable, INullable, IXmlSerializable
    {
        private byte[] a;
        private System.Text.Encoding b;
        private bool c;
        public static readonly MySqlBinaryString Null;
        public MySqlBinaryString(byte[] buffer);
        public MySqlBinaryString(byte[] buffer, int offset, int count);
        public MySqlBinaryString(byte[] buffer, System.Text.Encoding encoding);
        public MySqlBinaryString(byte[] buffer, int offset, int count, System.Text.Encoding encoding);
        public MySqlBinaryString(string value);
        public MySqlBinaryString(string value, System.Text.Encoding encoding);
        private MySqlBinaryString(bool A_0);
        public static implicit operator MySqlBinaryString(string value);
        public static explicit operator string(MySqlBinaryString value);
        public static MySqlBinaryString operator +(MySqlBinaryString x, MySqlBinaryString y);
        public static bool operator ==(MySqlBinaryString x, MySqlBinaryString y);
        public static bool operator !=(MySqlBinaryString x, MySqlBinaryString y);
        public static bool operator <(MySqlBinaryString x, MySqlBinaryString y);
        public static bool operator >(MySqlBinaryString x, MySqlBinaryString y);
        public static bool operator <=(MySqlBinaryString x, MySqlBinaryString y);
        public static bool operator >=(MySqlBinaryString x, MySqlBinaryString y);
        public static MySqlBinaryString Concat(MySqlBinaryString x, MySqlBinaryString y);
        public int CompareTo(object obj);
        public int CompareTo(MySqlBinaryString value);
        public override bool Equals(object obj);
        public override int GetHashCode();
        public override string ToString();
        public MySqlBinaryString Clone();
        public byte[] Buffer { get; }
        public string Value { get; }
        public System.Text.Encoding Encoding { get; set; }
        public bool IsNull { get; }
        public int Length { get; }
        XmlSchema IXmlSerializable.GetSchema();
        void IXmlSerializable.ReadXml(XmlReader reader);
        void IXmlSerializable.WriteXml(XmlWriter writer);
        static MySqlBinaryString();
    }
}

