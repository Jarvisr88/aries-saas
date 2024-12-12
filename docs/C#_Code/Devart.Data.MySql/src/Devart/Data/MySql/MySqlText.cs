namespace Devart.Data.MySql
{
    using System;
    using System.ComponentModel;
    using System.Text;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    [TypeConverter(typeof(s))]
    public class MySqlText : MySqlBlob, IComparable, IXmlSerializable
    {
        private bool a;
        internal System.Text.Encoding b;
        public static readonly MySqlText Null;

        static MySqlText();
        public MySqlText();
        internal MySqlText(bool A_0);
        internal MySqlText(int A_0);
        public MySqlText(string value);
        public MySqlText(string value, bool unicode);
        internal MySqlText(string A_0, System.Text.Encoding A_1);
        public int CompareTo(MySqlText obj);
        public int CompareTo(object obj);
        public override bool Equals(object obj);
        public override int GetHashCode();
        XmlSchema IXmlSerializable.GetSchema();
        void IXmlSerializable.ReadXml(XmlReader reader);
        void IXmlSerializable.WriteXml(XmlWriter writer);
        public override string ToString();

        public string Value { get; }

        internal System.Text.Encoding Encoding { get; set; }
    }
}

