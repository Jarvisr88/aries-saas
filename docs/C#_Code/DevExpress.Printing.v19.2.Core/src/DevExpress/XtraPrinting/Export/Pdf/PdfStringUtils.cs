namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Globalization;
    using System.Text;

    public class PdfStringUtils
    {
        public static string ArrayToHexadecimalString(byte[] data)
        {
            if ((data == null) || (data.Length == 0))
            {
                return string.Empty;
            }
            StringBuilder builder = new StringBuilder();
            foreach (byte num2 in data)
            {
                builder.Append(num2.ToString("X2"));
            }
            return builder.ToString();
        }

        public static string ClearSpaces(string str)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                if ((str[i] != ' ') && ((str[i] != '\r') && (str[i] != '\n')))
                {
                    builder.Append(str[i]);
                }
            }
            return builder.ToString();
        }

        public static string EscapeString(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }
            string str2 = string.Empty;
            for (int i = 0; i < str.Length; i++)
            {
                str2 = str2 + ReplaceEscapeChar(str[i]);
            }
            return str2;
        }

        public static byte[] GetIsoBytes(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return new byte[0];
            }
            byte[] buffer = new byte[text.Length];
            for (int i = 0; i < text.Length; i++)
            {
                buffer[i] = (byte) text[i];
            }
            return buffer;
        }

        public static string GetIsoString(byte[] bytes)
        {
            if ((bytes == null) || (bytes.Length == 0))
            {
                return string.Empty;
            }
            StringBuilder builder = new StringBuilder();
            foreach (byte num2 in bytes)
            {
                builder.Append((char) num2);
            }
            return builder.ToString();
        }

        public static string HexChar(char ch)
        {
            byte[] bytes = BitConverter.GetBytes(ch);
            return (bytes[1].ToString("X2", CultureInfo.CurrentCulture) + bytes[0].ToString("X2", CultureInfo.CurrentCulture));
        }

        public static string HexCharAsByte(char ch) => 
            BitConverter.GetBytes(ch)[0].ToString("X2", CultureInfo.CurrentCulture);

        private static string ReplaceEscapeChar(char ch) => 
            (ch != '(') ? ((ch != ')') ? ((ch != '\\') ? ((ch != '\r') ? ((ch != '\n') ? ((ch != '\t') ? ((ch != '\b') ? ((ch != '\f') ? Convert.ToString(ch) : @"\f") : @"\b") : @"\t") : @"\n") : @"\r") : @"\\") : @"\)") : @"\(";
    }
}

