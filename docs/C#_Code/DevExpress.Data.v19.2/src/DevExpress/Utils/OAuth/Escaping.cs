namespace DevExpress.Utils.OAuth
{
    using System;
    using System.Globalization;
    using System.Text;

    public static class Escaping
    {
        private static readonly byte[] s_NoBytes = new byte[0];
        private static readonly byte[] s_HexChars = new byte[] { 0x30, 0x31, 50, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x41, 0x42, 0x43, 0x44, 0x45, 70 };

        public static string Escape(string value)
        {
            byte[] bytes = Escape(value, Encoding.UTF8);
            return Encoding.UTF8.GetString(bytes, 0, bytes.Length);
        }

        public static byte[] Escape(string value, Encoding encoding)
        {
            if (encoding == null)
            {
                throw new ArgumentNullException("encoding");
            }
            if (string.IsNullOrEmpty(value))
            {
                return s_NoBytes;
            }
            byte[] bytes = encoding.GetBytes(value);
            byte[] src = new byte[bytes.Length * 3];
            int count = 0;
            for (int i = 0; i < bytes.Length; i++)
            {
                byte c = bytes[i];
                if (IsUnreserved(c))
                {
                    src[count++] = c;
                }
                else
                {
                    src[count++] = 0x25;
                    src[count++] = s_HexChars[(c >> 4) & 15];
                    src[count++] = s_HexChars[c & 15];
                }
            }
            byte[] dst = new byte[count];
            Buffer.BlockCopy(src, 0, dst, 0, count);
            return dst;
        }

        public static bool IsHex(byte c) => 
            ((c < 0x61) || (c > 0x66)) ? (((c >= 0x41) && (c <= 70)) || ((c >= 0x30) && (c <= 0x39))) : true;

        public static bool IsUnreserved(byte c) => 
            ((c < 0x61) || (c > 0x7a)) ? (((c >= 0x41) && (c <= 90)) || (((c >= 0x30) && (c <= 0x39)) || ((c == 0x2d) || ((c == 0x5f) || ((c == 0x2e) || (c == 0x7e)))))) : true;

        public static string Unescape(string input) => 
            Unescape(input, true, false);

        public static string Unescape(string input, bool unescape, bool unquote) => 
            ((input == null) || (input.Length <= 0)) ? string.Empty : Unescape(input, 0, input.Length, unescape, unquote);

        public static string Unescape(string input, int index, int count) => 
            Unescape(input, index, count, true, false);

        public static string Unescape(string input, int index, int count, bool unescape, bool unquote)
        {
            if (input == null)
            {
                throw new ArgumentNullException("token");
            }
            int length = input.Length;
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            if (index > length)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException("count");
            }
            if (index > (length - count))
            {
                throw new ArgumentOutOfRangeException("count");
            }
            if (!unescape)
            {
                return input.Substring(index, count);
            }
            byte[] bytes = new byte[count];
            int num2 = 0;
            int num3 = index;
            while (num3 < (index + count))
            {
                byte num7 = (byte) input[num3];
                int num8 = num3 + 1;
                int num9 = num3 + 2;
                if ((num7 == 0x25) && ((num8 < (index + count)) && ((num9 < (index + count)) && (IsHex((byte) input[num8]) && IsHex((byte) input[num9])))))
                {
                    bytes[num2] = (byte) int.Parse($"{input[num8]}{input[num9]}", NumberStyles.HexNumber, CultureInfo.InvariantCulture);
                    num3 += 3;
                }
                else
                {
                    bytes[num2] = num7;
                    num3++;
                }
                num2++;
            }
            int num4 = 0;
            int num5 = num2 - 1;
            while ((num5 >= 0) && char.IsWhiteSpace((char) bytes[num5]))
            {
                num5--;
            }
            while ((num4 < num5) && char.IsWhiteSpace((char) bytes[num4]))
            {
                num4++;
            }
            if (unquote && ((num4 >= 0) && ((num4 < num5) && ((bytes[num4] == 0x22) && ((num5 < num2) && (bytes[num5] == 0x22))))))
            {
                num4++;
                num5--;
            }
            int num6 = (num5 - num4) + 1;
            return ((num6 > 0) ? Encoding.UTF8.GetString(bytes, num4, num6) : string.Empty);
        }
    }
}

