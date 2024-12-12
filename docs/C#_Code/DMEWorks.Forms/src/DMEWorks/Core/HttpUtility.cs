namespace DMEWorks.Core
{
    using System;
    using System.Text;

    public static class HttpUtility
    {
        private static char IntToHex(int n) => 
            (n > 9) ? ((char) ((n - 10) + 0x61)) : ((char) (n + 0x30));

        private static bool IsSafe(char ch)
        {
            if (((((ch < 'a') || (ch > 'z')) && ((ch < 'A') || (ch > 'Z'))) && ((ch < '0') || (ch > '9'))) && (ch != '!'))
            {
                switch (ch)
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
                        return (ch == '_');
                }
            }
            return true;
        }

        public static string UrlEncode(string str) => 
            (str != null) ? UrlEncode(str, Encoding.UTF8) : null;

        public static string UrlEncode(string str, Encoding encoding) => 
            (str != null) ? Encoding.ASCII.GetString(UrlEncodeToBytes(str, encoding)) : null;

        private static byte[] UrlEncodeBytesToBytesInternal(byte[] bytes, int offset, int count, bool alwaysCreateReturnValue)
        {
            int num = 0;
            int num2 = 0;
            for (int i = 0; i < count; i++)
            {
                char ch = (char) bytes[offset + i];
                if (ch == ' ')
                {
                    num++;
                }
                else if (!IsSafe(ch))
                {
                    num2++;
                }
            }
            if (!alwaysCreateReturnValue && ((num == 0) && (num2 == 0)))
            {
                return bytes;
            }
            byte[] buffer = new byte[count + (num2 * 2)];
            int num3 = 0;
            for (int j = 0; j < count; j++)
            {
                byte num6 = bytes[offset + j];
                char ch = (char) num6;
                if (IsSafe(ch))
                {
                    buffer[num3++] = num6;
                }
                else if (ch == ' ')
                {
                    buffer[num3++] = 0x2b;
                }
                else
                {
                    buffer[num3++] = 0x25;
                    buffer[num3++] = (byte) IntToHex((num6 >> 4) & 15);
                    buffer[num3++] = (byte) IntToHex(num6 & 15);
                }
            }
            return buffer;
        }

        private static byte[] UrlEncodeToBytes(string str, Encoding encoding)
        {
            if (str == null)
            {
                return null;
            }
            byte[] bytes = encoding.GetBytes(str);
            return UrlEncodeBytesToBytesInternal(bytes, 0, bytes.Length, false);
        }
    }
}

