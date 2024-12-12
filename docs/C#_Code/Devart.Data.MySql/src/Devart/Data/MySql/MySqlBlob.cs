namespace Devart.Data.MySql
{
    using System;
    using System.Data.SqlTypes;
    using System.IO;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    public class MySqlBlob : Stream, IDisposable, ICloneable, IComparable, IXmlSerializable, INullable
    {
        internal MemoryStream a;
        protected bool b;
        public static readonly MySqlBlob Null;

        static MySqlBlob();
        public MySqlBlob();
        public MySqlBlob(byte[] value);
        internal MySqlBlob(bool A_0);
        internal MySqlBlob(int A_0);
        internal byte[] b();
        public object Clone();
        public override void Close();
        public int CompareTo(MySqlBlob obj);
        public int CompareTo(object obj);
        public void Dispose();
        public override void Flush();
        protected internal virtual MemoryStream GetData();
        public override int Read(byte[] buffer, int offset, int count);
        public override long Seek(long offset, SeekOrigin origin);
        public override void SetLength(long len);
        XmlSchema IXmlSerializable.GetSchema();
        void IXmlSerializable.ReadXml(XmlReader reader);
        void IXmlSerializable.WriteXml(XmlWriter writer);
        public override string ToString();
        public override void Write(byte[] buffer, int offset, int count);

        public override bool CanRead { get; }

        public override bool CanSeek { get; }

        public override bool CanWrite { get; }

        public bool IsEmpty { get; }

        public bool IsNull { get; }

        public override long Length { get; }

        public override long Position { get; set; }

        public byte[] Value { get; }
    }
}

