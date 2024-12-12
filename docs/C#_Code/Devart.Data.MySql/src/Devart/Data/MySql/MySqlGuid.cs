namespace Devart.Data.MySql
{
    using System;
    using System.Data.SqlTypes;

    public class MySqlGuid : IComparable, INullable
    {
        private Guid a;
        private bool b;
        public static readonly MySqlGuid Null;
        public static readonly MySqlGuid Empty;

        static MySqlGuid();
        public MySqlGuid();
        public MySqlGuid(string value);
        public MySqlGuid(byte[] value);
        private MySqlGuid(bool A_0);
        public MySqlGuid(Guid value);
        internal MySqlGuid(byte[] A_0, int A_1, int A_2);
        public int CompareTo(MySqlGuid obj);
        public int CompareTo(object obj);
        public override bool Equals(object obj);
        public static bool operator ==(MySqlGuid a, MySqlGuid b);
        public static implicit operator MySqlGuid(string value);
        public static implicit operator MySqlGuid(byte[] value);
        public static bool operator !=(MySqlGuid a, MySqlGuid b);
        public byte[] ToByteArray();
        public override string ToString();
        public string ToString(string format);
        public string ToString(string format, IFormatProvider provider);

        public Guid Value { get; }

        public bool IsNull { get; }

        public bool IsEmpty { get; }
    }
}

