namespace DevExpress.XtraPrinting.HtmlExport.Native
{
    using DevExpress.Utils;
    using System;
    using System.Globalization;
    using System.IO;
    using System.Text;

    public sealed class DXHttpUtility
    {
        private static int HexToInt(char value) => 
            ((value < '0') || (value > '9')) ? (((value < 'a') || (value > 'f')) ? (((value < 'A') || (value > 'F')) ? -1 : ((value - 'A') + 10)) : ((value - 'a') + 10)) : (value - '0');

        public static string HtmlAttributeEncode(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }
            if (IndexOfHtmlAttributeEncodingChars(value, 0) == -1)
            {
                return value;
            }
            StringWriter output = new StringWriter(CultureInfo.InvariantCulture);
            HtmlAttributeEncode(value, output);
            return output.ToString();
        }

        internal static void HtmlAttributeEncode(string value, TextWriter output)
        {
            if (value != null)
            {
                Guard.ArgumentNotNull(output, "output");
                int num = IndexOfHtmlAttributeEncodingChars(value, 0);
                if (num == -1)
                {
                    output.Write(value);
                }
                else
                {
                    int num2 = value.Length - num;
                    int num3 = 0;
                    while (num-- > 0)
                    {
                        output.Write(value[num3++]);
                    }
                    while (num2-- > 0)
                    {
                        char ch = value[num3++];
                        if (ch > '<')
                        {
                            output.Write(ch);
                            continue;
                        }
                        if (ch <= '&')
                        {
                            if (ch == '"')
                            {
                                output.Write("&quot;");
                                continue;
                            }
                            if (ch == '&')
                            {
                                output.Write("&amp;");
                                continue;
                            }
                        }
                        else
                        {
                            if (ch == '\'')
                            {
                                output.Write("&#39;");
                                continue;
                            }
                            if (ch == '<')
                            {
                                output.Write("&lt;");
                                continue;
                            }
                        }
                        output.Write(ch);
                    }
                }
            }
        }

        private static int IndexOfHtmlAttributeEncodingChars(string html, int startPosition)
        {
            int num = html.Length - startPosition;
            int num2 = startPosition;
            while (num > 0)
            {
                char ch = html[num2];
                if ((ch <= '<') && ((ch == '"') || ((ch == '&') || (ch == '<'))))
                {
                    return (html.Length - num);
                }
                num2++;
                num--;
            }
            return -1;
        }

        internal static char IntToHex(int value) => 
            (value > 9) ? ((char) ((value - 10) + 0x61)) : ((char) (value + 0x30));

        private static bool IsNonAsciiByte(byte value) => 
            (value >= 0x7f) || (value < 0x20);

        internal static bool IsSafe(char value)
        {
            if (((((value < 'a') || (value > 'z')) && ((value < 'A') || (value > 'Z'))) && ((value < '0') || (value > '9'))) && (value != '!'))
            {
                switch (value)
                {
                    case '\'':
                    case '(':
                    case ')':
                    case '*':
                    case '-':
                    case '.':
                        break;

                    case '+':
                    case ',':
                        return false;

                    default:
                        return (value == '_');
                }
            }
            return true;
        }

        internal static bool IsSpecialSymbol(char ch) => 
            (ch == '{') || ((ch == '}') || (ch == '\\'));

        public static string UrlDecode(string value) => 
            (value != null) ? UrlDecode(value, Encoding.UTF8) : null;

        public static string UrlDecode(string url, Encoding encoding) => 
            (url != null) ? UrlDecodeStringFromStringInternal(url, encoding) : null;

        private static string UrlDecodeStringFromStringInternal(string value, Encoding encoding)
        {
            Guard.ArgumentNotNull(value, "value");
            int length = value.Length;
            UrlDecoder decoder = new UrlDecoder(length, encoding);
            int num2 = 0;
            while (true)
            {
                while (true)
                {
                    if (num2 >= length)
                    {
                        return decoder.GetString();
                    }
                    char ch = value[num2];
                    if (ch == '+')
                    {
                        ch = ' ';
                    }
                    else if ((ch == '%') && (num2 < (length - 2)))
                    {
                        int num3 = HexToInt(value[num2 + 1]);
                        int num4 = HexToInt(value[num2 + 2]);
                        if ((num3 >= 0) && (num4 >= 0))
                        {
                            num2 += 2;
                            decoder.AddByte((byte) ((num3 << 4) | num4));
                            break;
                        }
                    }
                    if ((ch & 0xff80) == 0)
                    {
                        decoder.AddByte((byte) ch);
                    }
                    else
                    {
                        decoder.AddChar(ch);
                    }
                    break;
                }
                num2++;
            }
        }

        public static string UrlEncodeAsciiSpecialSymbols(string url)
        {
            if (url != null)
            {
                if (url.IndexOf(' ') >= 0)
                {
                    url = url.Replace(" ", "%20");
                }
                if (url.IndexOf('\'') >= 0)
                {
                    url = url.Replace("'", "%27");
                }
            }
            return url;
        }

        private static byte[] UrlEncodeBytesToBytesInternalNonAscii(byte[] bytes, int offset, int count, bool alwaysCreateReturnValue)
        {
            int num = 0;
            for (int i = 0; i < count; i++)
            {
                if (IsNonAsciiByte(bytes[offset + i]))
                {
                    num++;
                }
            }
            if (!alwaysCreateReturnValue && (num == 0))
            {
                return bytes;
            }
            byte[] buffer = new byte[count + (num * 2)];
            int num2 = 0;
            for (int j = 0; j < count; j++)
            {
                byte num5 = bytes[offset + j];
                if (!IsNonAsciiByte(num5))
                {
                    buffer[num2++] = num5;
                }
                else
                {
                    buffer[num2++] = 0x25;
                    buffer[num2++] = (byte) IntToHex((num5 >> 4) & 15);
                    buffer[num2++] = (byte) IntToHex(num5 & 15);
                }
            }
            return buffer;
        }

        internal static string UrlEncodeNonAscii(string url, Encoding encoding)
        {
            if (string.IsNullOrEmpty(url))
            {
                return url;
            }
            encoding ??= Encoding.UTF8;
            byte[] bytes = encoding.GetBytes(url);
            bytes = UrlEncodeBytesToBytesInternalNonAscii(bytes, 0, bytes.Length, false);
            return DXEncoding.ASCII.GetString(bytes, 0, bytes.Length);
        }

        public static string UrlEncodeToUnicodeCompatible(string stringData)
        {
            string str = UrlPathEncode(stringData);
            for (int i = 0; i < str.Length; i++)
            {
                char ch = str[i];
                if (IsSpecialSymbol(ch))
                {
                    str = str.Insert(i, @"\");
                    i++;
                }
            }
            return str;
        }

        public static string UrlPathEncode(string url)
        {
            if (url == null)
            {
                return null;
            }
            int index = url.IndexOf('?');
            return ((index < 0) ? UrlEncodeAsciiSpecialSymbols(UrlEncodeNonAscii(url, Encoding.UTF8)) : (UrlPathEncode(url.Substring(0, index)) + url.Substring(index)));
        }

        private class UrlDecoder
        {
            private int bufferSize;
            private int numChars;
            private char[] charBuffer;
            private int numBytes;
            private byte[] byteBuffer;
            private Encoding encoding;

            internal UrlDecoder(int bufferSize, Encoding encoding)
            {
                this.bufferSize = bufferSize;
                this.encoding = encoding;
                this.charBuffer = new char[bufferSize];
            }

            internal void AddByte(byte b)
            {
                this.byteBuffer ??= new byte[this.bufferSize];
                int numBytes = this.numBytes;
                this.numBytes = numBytes + 1;
                this.byteBuffer[numBytes] = b;
            }

            internal void AddChar(char ch)
            {
                if (this.numBytes > 0)
                {
                    this.FlushBytes();
                }
                int numChars = this.numChars;
                this.numChars = numChars + 1;
                this.charBuffer[numChars] = ch;
            }

            private void FlushBytes()
            {
                if (this.numBytes > 0)
                {
                    this.numChars += this.encoding.GetChars(this.byteBuffer, 0, this.numBytes, this.charBuffer, this.numChars);
                    this.numBytes = 0;
                }
            }

            internal string GetString()
            {
                if (this.numBytes > 0)
                {
                    this.FlushBytes();
                }
                return ((this.numChars <= 0) ? string.Empty : new string(this.charBuffer, 0, this.numChars));
            }
        }
    }
}

