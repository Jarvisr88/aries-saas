namespace DevExpress.Utils
{
    using System;
    using System.Globalization;
    using System.Text;

    public static class StringFormatHelper
    {
        public static bool ContainsParameterSpecifier(string formatString, int n)
        {
            if (string.IsNullOrEmpty(formatString))
            {
                return false;
            }
            string str = n.ToString(CultureInfo.InvariantCulture);
            int startIndex = 0;
            while (true)
            {
                if ((startIndex >= 0) && (startIndex < formatString.Length))
                {
                    startIndex = formatString.IndexOf('{', startIndex);
                    if (startIndex != -1)
                    {
                        StringBuilder builder = new StringBuilder(str.Length);
                        while (true)
                        {
                            if (++startIndex < formatString.Length)
                            {
                                char ch = formatString[startIndex];
                                if ((ch >= '0') && (ch <= '9'))
                                {
                                    if ((builder.Length == 0) && (ch == '0'))
                                    {
                                        continue;
                                    }
                                    builder.Append(ch);
                                    continue;
                                }
                            }
                            if (str != builder.ToString())
                            {
                                break;
                            }
                            return true;
                        }
                        continue;
                    }
                }
                return false;
            }
        }
    }
}

