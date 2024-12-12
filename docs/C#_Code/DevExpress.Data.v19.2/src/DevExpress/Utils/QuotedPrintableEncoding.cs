namespace DevExpress.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;

    public class QuotedPrintableEncoding
    {
        public static readonly QuotedPrintableEncoding Instance = new QuotedPrintableEncoding();
        private static readonly string[] newLineStrings = new string[] { "\r\n", "\n\r", "\r", "\n" };

        protected internal virtual void AppendCharNoDecode(byte ch, List<byte> sb)
        {
            sb.Add(ch);
        }

        protected internal virtual void AppendCharNoDecode(char ch, StringBuilder sb)
        {
            sb.Append(ch);
        }

        protected internal string ConvertToQuotedPrintable(byte value) => 
            !this.ShouldConvertByte(value) ? new string((char) value, 1) : $"={value:X2}";

        protected internal int DecodeQuotedChar(string value, int index, StringBuilder target)
        {
            int num;
            if ((index + 2) >= value.Length)
            {
                target.Append(value[index]);
                return 0;
            }
            if (!int.TryParse(value.Substring(index + 1, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out num))
            {
                target.Append(value[index]);
                return 0;
            }
            target.Append((char) num);
            return 2;
        }

        protected internal int DecodeQuotedChar(string value, int index, Encoding encoding, List<byte> target)
        {
            int num;
            if ((index + 2) >= value.Length)
            {
                target.Add((byte) value[index]);
                return 0;
            }
            if (!int.TryParse(value.Substring(index + 1, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out num))
            {
                target.Add((byte) value[index]);
                return 0;
            }
            target.Add((byte) num);
            return 2;
        }

        public string FromMultilineQuotedPrintableString(string value)
        {
            string[] plainTextLines = GetPlainTextLines(value);
            StringBuilder builder = new StringBuilder();
            int length = plainTextLines.Length;
            for (int i = 0; i < length; i++)
            {
                string str = plainTextLines[i];
                if (str.EndsWith("="))
                {
                    builder.Append(str.Substring(0, str.Length - 1));
                }
                else if (i >= (length - 1))
                {
                    builder.Append(str);
                }
                else
                {
                    builder.Append(str);
                    builder.Append("\r\n");
                }
            }
            return builder.ToString();
        }

        public string FromQuotedPrintableString(string value)
        {
            StringBuilder sb = new StringBuilder();
            int length = value.Length;
            for (int i = 0; i < length; i++)
            {
                char ch = value[i];
                if (ch != '=')
                {
                    this.AppendCharNoDecode(ch, sb);
                }
                else
                {
                    i += this.DecodeQuotedChar(value, i, sb);
                }
            }
            return sb.ToString();
        }

        public string FromQuotedPrintableString(string value, Encoding encoding)
        {
            List<byte> sb = new List<byte>();
            int length = value.Length;
            for (int i = 0; i < length; i++)
            {
                char ch = value[i];
                if (ch != '=')
                {
                    this.AppendCharNoDecode((byte) ch, sb);
                }
                else
                {
                    i += this.DecodeQuotedChar(value, i, encoding, sb);
                }
            }
            byte[] bytes = sb.ToArray();
            return encoding.GetString(bytes, 0, bytes.Length);
        }

        public static string[] GetPlainTextLines(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                return text.Split(newLineStrings, StringSplitOptions.None);
            }
            return new string[] { string.Empty };
        }

        protected internal bool ShouldConvertByte(byte value) => 
            (value <= 0x20) || ((value >= 0x80) || (value == 0x3d));

        public string ToMultilineQuotedPrintableString(string value, int maxLineWidth)
        {
            string[] plainTextLines = GetPlainTextLines(value);
            StringBuilder builder = new StringBuilder();
            int length = plainTextLines.Length;
            for (int i = 0; i < length; i++)
            {
                if (i > 0)
                {
                    builder.Append("\r\n");
                }
                builder.Append(this.ToMultilineQuotedPrintableStringCore(plainTextLines[i], maxLineWidth - 1));
            }
            return builder.ToString();
        }

        protected internal string ToMultilineQuotedPrintableStringCore(string value, int maxLineWidth)
        {
            StringBuilder builder = new StringBuilder();
            int length = value.Length;
            for (int i = 0; i < length; i += maxLineWidth)
            {
                if (i > 0)
                {
                    builder.Append("=");
                    builder.Append("\r\n");
                }
                string str = value.Substring(i, Math.Min(length - i, maxLineWidth));
                int num3 = str.LastIndexOf('=');
                if (num3 >= 0)
                {
                    int num4 = str.Length - num3;
                    if ((num4 <= 3) && ((num3 + 3) > maxLineWidth))
                    {
                        i -= num4;
                        str = str.Substring(0, str.Length - num4);
                    }
                }
                builder.Append(str);
            }
            return builder.ToString();
        }

        public string ToQuotedPrintableString(byte[] bytes)
        {
            StringBuilder builder = new StringBuilder();
            int length = bytes.Length;
            for (int i = 0; i < length; i++)
            {
                builder.Append(this.ConvertToQuotedPrintable(bytes[i]));
            }
            return builder.ToString();
        }

        public string ToQuotedPrintableString(string value, Encoding encoding) => 
            this.ToQuotedPrintableString(encoding.GetBytes(value));
    }
}

