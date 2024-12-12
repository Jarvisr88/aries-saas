namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public static class PdfDocEncoding
    {
        private static readonly char[] docEncoding = new char[] { 
            '\0', '\x0001', '\x0002', '\x0003', '\x0004', '\x0005', '\x0006', '\a', '\b', '\t', '\n', '\v', '\f', '\r', '\x000e', '\x000f',
            '\x0010', '\x0011', '\x0012', '\x0013', '\x0014', '\x0015', '\x0017', '\x0017', '˘', 'ˇ', 'ˆ', '˙', '˝', '˛', '˚', '˜',
            ' ', '!', '"', '#', '$', '%', '&', '\'', '(', ')', '*', '+', ',', '-', '.', '/',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ':', ';', '<', '=', '>', '?',
            '@', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O',
            'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '[', '\\', ']', '^', '_',
            '`', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o',
            'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '{', '|', '}', '~', '-',
            '•', '†', '‡', '…', '—', '–', 'ƒ', '⁄', '‹', '›', '−', '‰', '„', '“', '”', '‘',
            '’', '‚', '™', 'ﬁ', 'ﬂ', 'Ł', 'Œ', 'Š', 'Ÿ', 'Ž', 'ı', 'ł', 'œ', 'š', 'ž', '-',
            '€', '\x00a1', '\x00a2', '\x00a3', '\x00a4', '\x00a5', '\x00a6', '\x00a7', '\x00a8', '\x00a9', '\x00aa', '\x00ab', '\x00ac', '-', '\x00ae', '\x00af',
            '\x00b0', '\x00b1', '\x00b2', '\x00b3', '\x00b4', '\x00b5', '\x00b6', '\x00b7', '\x00b8', '\x00b9', '\x00ba', '\x00bb', '\x00bc', '\x00bd', '\x00be', '\x00bf',
            '\x00c0', '\x00c1', '\x00c2', '\x00c3', '\x00c4', '\x00c5', '\x00c6', '\x00c7', '\x00c8', '\x00c9', '\x00ca', '\x00cb', '\x00cc', '\x00cd', '\x00ce', '\x00cf',
            '\x00d0', '\x00d1', '\x00d2', '\x00d3', '\x00d4', '\x00d5', '\x00d6', '\x00d7', '\x00d8', '\x00d9', '\x00da', '\x00db', '\x00dc', '\x00dd', '\x00de', '\x00df',
            '\x00e0', '\x00e1', '\x00e2', '\x00e3', '\x00e4', '\x00e5', '\x00e6', '\x00e7', '\x00e8', '\x00e9', '\x00ea', '\x00eb', '\x00ec', '\x00ed', '\x00ee', '\x00ef',
            '\x00f0', '\x00f1', '\x00f2', '\x00f3', '\x00f4', '\x00f5', '\x00f6', '\x00f7', '\x00f8', '\x00f9', '\x00fa', '\x00fb', '\x00fc', '\x00fd', '\x00fe', '\x00ff'
        };
        private static readonly IDictionary<char, byte> docEncodingMapping = new Dictionary<char, byte>();

        static PdfDocEncoding()
        {
            for (int i = 0; i < 0xff; i++)
            {
                char key = docEncoding[i];
                if (!docEncodingMapping.ContainsKey(key))
                {
                    docEncodingMapping.Add(key, (byte) i);
                }
            }
        }

        public static byte[] GetBytes(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return new byte[0];
            }
            int length = value.Length;
            byte[] buffer = new byte[length];
            for (int i = 0; i < length; i++)
            {
                docEncodingMapping.TryGetValue(value[i], out buffer[i]);
            }
            return buffer;
        }

        public static string GetString(byte[] data)
        {
            StringBuilder builder = new StringBuilder();
            foreach (byte num2 in data)
            {
                builder.Append(docEncoding[num2]);
            }
            return builder.ToString();
        }

        public static bool StringCanBeEncoded(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                foreach (char ch in str)
                {
                    if (!docEncodingMapping.ContainsKey(ch))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}

