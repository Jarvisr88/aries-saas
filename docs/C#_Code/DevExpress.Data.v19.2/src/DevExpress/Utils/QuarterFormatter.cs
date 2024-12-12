namespace DevExpress.Utils
{
    using System;
    using System.Text;

    public static class QuarterFormatter
    {
        private const int monthsInQuarter = 3;
        private const char arabicQuarterSymbol = 'Q';
        private const char romanQuarterSymbol = 'q';
        private const char singleQuote = '\'';
        private const char doubleQuote = '"';
        private const char backSlash = '\\';
        private const char percent = '%';
        private static readonly string[] arabicDigits = new string[] { "1", "2", "3", "4" };
        private static readonly string[] romanDigits = new string[] { "I", "II", "III", "IV" };
        private static readonly char[] specialChars = new char[] { '\'', '"', '\\', '%' };

        public static string FormatDateTime(DateTime dateTime, string formatString, string quarterFormat) => 
            FormatDateTime((int) (((dateTime.Month - 1) / 3) + 1), formatString, quarterFormat);

        public static string FormatDateTime(int quarterNumber, string formatString, string quarterFormat)
        {
            if (string.IsNullOrEmpty(formatString))
            {
                return string.Empty;
            }
            StringBuilder builder = new StringBuilder();
            int startIndex = 0;
            while (true)
            {
                int num2 = formatString.IndexOfAny(specialChars, startIndex);
                if (num2 < 0)
                {
                    builder.Append(FormatQuarter(quarterNumber, formatString.Substring(startIndex), quarterFormat));
                }
                else
                {
                    if (num2 > startIndex)
                    {
                        builder.Append(FormatQuarter(quarterNumber, formatString.Substring(startIndex, num2 - startIndex), quarterFormat));
                        startIndex = num2;
                    }
                    char ch = formatString[startIndex];
                    if (ch == '%')
                    {
                        if (++startIndex >= formatString.Length)
                        {
                            builder.Append('%');
                        }
                        else
                        {
                            char c = formatString[startIndex++];
                            if (IsQuarterSymbol(c))
                            {
                                builder.Append(FormatQuarter(quarterNumber, new string(c, 1), quarterFormat));
                            }
                            else
                            {
                                builder.Append('%');
                                builder.Append(c);
                            }
                        }
                    }
                    else if (ch == '\\')
                    {
                        builder.Append('\\');
                        if (++startIndex < formatString.Length)
                        {
                            builder.Append(formatString[startIndex++]);
                        }
                    }
                    else
                    {
                        int index = formatString.IndexOf(formatString[startIndex], startIndex + 1);
                        if (index < 0)
                        {
                            builder.Append(formatString.Substring(startIndex));
                            startIndex = formatString.Length;
                        }
                        else
                        {
                            builder.Append(formatString.Substring(startIndex, (index - startIndex) + 1));
                            startIndex = index + 1;
                        }
                    }
                    if (startIndex < formatString.Length)
                    {
                        continue;
                    }
                }
                return builder.ToString();
            }
        }

        public static string FormatQuarter(int quarter, string format, string predefinedFormat)
        {
            if (string.IsNullOrEmpty(format))
            {
                return format;
            }
            int digitIndex = quarter - 1;
            string str = string.Empty;
            int counter = 0;
            char quarterSymbol = '\0';
            foreach (char ch2 in format)
            {
                if ((ch2 != quarterSymbol) && (counter > 0))
                {
                    str = str + MakeQuarterString(quarterSymbol, digitIndex, counter, predefinedFormat);
                    counter = 0;
                }
                if (IsQuarterSymbol(ch2))
                {
                    counter++;
                }
                else
                {
                    str = str + ch2.ToString();
                }
                quarterSymbol = ch2;
            }
            if (counter > 0)
            {
                str = str + MakeQuarterString(quarterSymbol, digitIndex, counter, predefinedFormat);
            }
            return str;
        }

        private static bool IsQuarterSymbol(char c) => 
            (c == 'Q') || (c == 'q');

        private static string MakeQuarterString(char quarterSymbol, int digitIndex, int counter, string predefinedFormat)
        {
            string str = (quarterSymbol == 'Q') ? arabicDigits[digitIndex] : romanDigits[digitIndex];
            if (counter > 1)
            {
                str = string.Format(predefinedFormat, str);
            }
            return ("'" + str + "'");
        }
    }
}

