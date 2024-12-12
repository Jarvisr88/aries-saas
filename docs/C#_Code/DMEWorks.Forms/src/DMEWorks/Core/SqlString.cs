namespace DMEWorks.Core
{
    using System;
    using System.Globalization;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct SqlString : IConvertible
    {
        private const char Space = ' ';
        public readonly int Length;
        public readonly string String;
        private const CompareOptions compareOptions = (CompareOptions.StringSort | CompareOptions.IgnoreCase);
        public SqlString(string @string)
        {
            if (@string != null)
            {
                this = new SqlString(@string, @string.Length);
            }
            else
            {
                this = new SqlString();
            }
        }

        public SqlString(string @string, int length)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException("length", "cannot be negative");
            }
            if (@string == null)
            {
                this = new SqlString();
            }
            else
            {
                length = Math.Min(@string.Length, length);
                while (true)
                {
                    if (0 < length)
                    {
                        length--;
                        if (@string[length] == ' ')
                        {
                            continue;
                        }
                        length++;
                    }
                    this.Length = length;
                    this.String = @string;
                    return;
                }
            }
        }

        public override string ToString() => 
            this.GetValue();

        private bool ContainsAt(int index, SqlString value) => 
            (index >= 0) ? (((this.Length - index) >= value.Length) ? (CultureInfo.InvariantCulture.CompareInfo.Compare(this.String, index, value.Length, value.String, 0, value.Length, CompareOptions.StringSort | CompareOptions.IgnoreCase) == 0) : false) : false;

        public int CompareTo(SqlString value) => 
            Compare(this, value);

        public bool Equals(SqlString value) => 
            Equals(this, value);

        public bool StartsWith(SqlString value) => 
            this.ContainsAt(0, value);

        public bool EndsWith(SqlString value) => 
            this.ContainsAt(this.Length - value.Length, value);

        public int IndexOf(SqlString value)
        {
            int index = 0;
            int num2 = this.Length - value.Length;
            while (index < num2)
            {
                if (this.ContainsAt(index, value))
                {
                    return index;
                }
                index++;
            }
            return -1;
        }

        public bool Contains(SqlString value) => 
            0 <= this.IndexOf(value);

        public int CompareTo(string value) => 
            Compare(this, new SqlString(value));

        public bool Equals(string value) => 
            Equals(this, new SqlString(value));

        public bool StartsWith(string value) => 
            this.StartsWith(new SqlString(value));

        public bool EndsWith(string value) => 
            this.StartsWith(new SqlString(value));

        public int IndexOf(string value) => 
            this.IndexOf(new SqlString(value));

        public bool Contains(string value) => 
            this.Contains(new SqlString(value));

        public bool Is(string value) => 
            this.Equals(new SqlString(value));

        public bool In(params string[] values)
        {
            if (values != null)
            {
                int index = 0;
                int length = values.Length;
                while (index < length)
                {
                    if (this.Equals(values[index]))
                    {
                        return true;
                    }
                    index++;
                }
            }
            return false;
        }

        public override int GetHashCode() => 
            GetHashCode(this);

        public override bool Equals(object obj) => 
            (obj != null) ? ((obj is SqlString) && this.Equals((SqlString) obj)) : (this.Length == 0);

        public static explicit operator SqlString(string value) => 
            new SqlString(value);

        public static explicit operator string(SqlString value) => 
            value.ToString();

        public static bool operator ==(SqlString x, SqlString y) => 
            x.Equals(y);

        public static bool operator !=(SqlString x, SqlString y) => 
            !x.Equals(y);

        private string GetValue()
        {
            if (this.Length == 0)
            {
                return "";
            }
            string str = this.String;
            return ((str.Length != this.Length) ? str.Substring(0, this.Length) : str);
        }

        TypeCode IConvertible.GetTypeCode() => 
            TypeCode.String;

        bool IConvertible.ToBoolean(IFormatProvider provider) => 
            Convert.ToBoolean(this.GetValue(), provider);

        byte IConvertible.ToByte(IFormatProvider provider) => 
            Convert.ToByte(this.GetValue(), provider);

        char IConvertible.ToChar(IFormatProvider provider) => 
            Convert.ToChar(this.GetValue(), provider);

        DateTime IConvertible.ToDateTime(IFormatProvider provider) => 
            Convert.ToDateTime(this.GetValue(), provider);

        decimal IConvertible.ToDecimal(IFormatProvider provider) => 
            Convert.ToDecimal(this.GetValue(), provider);

        double IConvertible.ToDouble(IFormatProvider provider) => 
            Convert.ToDouble(this.GetValue(), provider);

        short IConvertible.ToInt16(IFormatProvider provider) => 
            Convert.ToInt16(this.GetValue(), provider);

        int IConvertible.ToInt32(IFormatProvider provider) => 
            Convert.ToInt32(this.GetValue(), provider);

        long IConvertible.ToInt64(IFormatProvider provider) => 
            Convert.ToInt64(this.GetValue(), provider);

        sbyte IConvertible.ToSByte(IFormatProvider provider) => 
            Convert.ToSByte(this.GetValue(), provider);

        float IConvertible.ToSingle(IFormatProvider provider) => 
            Convert.ToSingle(this.GetValue(), provider);

        public string ToString(IFormatProvider provider) => 
            this.GetValue();

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            if (conversionType == null)
            {
                throw new ArgumentNullException("conversionType");
            }
            if (conversionType == typeof(SqlString))
            {
                return this;
            }
            if (conversionType == typeof(string))
            {
                return this.GetValue();
            }
            if (conversionType == typeof(object))
            {
                return this;
            }
            if (conversionType == typeof(bool))
            {
                return Convert.ToBoolean(this.GetValue(), provider);
            }
            if (conversionType == typeof(char))
            {
                return Convert.ToChar(this.GetValue(), provider);
            }
            if (conversionType == typeof(sbyte))
            {
                return Convert.ToSByte(this.GetValue(), provider);
            }
            if (conversionType == typeof(byte))
            {
                return Convert.ToByte(this.GetValue(), provider);
            }
            if (conversionType == typeof(short))
            {
                return Convert.ToInt16(this.GetValue(), provider);
            }
            if (conversionType == typeof(ushort))
            {
                return Convert.ToUInt16(this.GetValue(), provider);
            }
            if (conversionType == typeof(int))
            {
                return Convert.ToInt32(this.GetValue(), provider);
            }
            if (conversionType == typeof(uint))
            {
                return Convert.ToUInt32(this.GetValue(), provider);
            }
            if (conversionType == typeof(long))
            {
                return Convert.ToInt64(this.GetValue(), provider);
            }
            if (conversionType == typeof(ulong))
            {
                return Convert.ToUInt64(this.GetValue(), provider);
            }
            if (conversionType == typeof(float))
            {
                return Convert.ToSingle(this.GetValue(), provider);
            }
            if (conversionType == typeof(double))
            {
                return Convert.ToDouble(this.GetValue(), provider);
            }
            if (conversionType == typeof(decimal))
            {
                return Convert.ToDecimal(this.GetValue(), provider);
            }
            if (conversionType == typeof(DateTime))
            {
                return Convert.ToDateTime(this.GetValue(), provider);
            }
            object[] args = new object[] { typeof(SqlString).FullName, conversionType.FullName };
            throw new InvalidCastException(string.Format(CultureInfo.CurrentCulture, "Invalid cast from {0} to {1}", args));
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider) => 
            Convert.ToUInt16(this.GetValue(), provider);

        uint IConvertible.ToUInt32(IFormatProvider provider) => 
            Convert.ToUInt32(this.GetValue(), provider);

        ulong IConvertible.ToUInt64(IFormatProvider provider) => 
            Convert.ToUInt64(this.GetValue(), provider);

        public static string Normalize(string value) => 
            new SqlString(value).ToString();

        public static bool IsNullOrEmpty(string value) => 
            new SqlString(value).Length == 0;

        public static int Compare(SqlString x, SqlString y) => 
            CultureInfo.InvariantCulture.CompareInfo.Compare(x.String, 0, x.Length, y.String, 0, y.Length, CompareOptions.StringSort | CompareOptions.IgnoreCase);

        public static bool Equals(SqlString x, SqlString y) => 
            (x.Length == y.Length) && (Compare(x, y) == 0);

        public static int GetHashCode(SqlString value)
        {
            if (value.String == null)
            {
                return 0;
            }
            if (value.Length == 0)
            {
                return 0;
            }
            int num = 0;
            int num2 = 0;
            int num3 = Math.Min(value.Length, 4);
            while (num2 < num3)
            {
                char ch = char.ToLower(value.String[num2], CultureInfo.InvariantCulture);
                num = (num << 8) | ((byte) ch);
                num2++;
            }
            return num;
        }

        public static int Compare(string x, string y, int length) => 
            Compare(new SqlString(x, length), new SqlString(y, length));

        public static int Compare(string x, string y) => 
            Compare(new SqlString(x), new SqlString(y));

        public static bool Equals(string x, string y) => 
            Equals(new SqlString(x), new SqlString(y));

        public static int GetHashCode(string value) => 
            GetHashCode(new SqlString(value));
    }
}

