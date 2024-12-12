namespace DevExpress.Export.Xl
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class XlNumberFormat
    {
        private static readonly XlNumberFormat general = new XlNumberFormat(0, string.Empty);
        private static readonly XlNumberFormat number = new XlNumberFormat(1, "0");
        private static readonly XlNumberFormat number2 = new XlNumberFormat(2, "0.00");
        private static readonly XlNumberFormat numberWithSeparator = new XlNumberFormat(3, "#,##0");
        private static readonly XlNumberFormat numberWithSeparator2 = new XlNumberFormat(4, "#,##0.00");
        private static readonly XlNumberFormat percentage = new XlNumberFormat(9, "0%");
        private static readonly XlNumberFormat percentage2 = new XlNumberFormat(10, "0.00%");
        private static readonly XlNumberFormat scientific = new XlNumberFormat(11, "0.00E+00");
        private static readonly XlNumberFormat fraction = new XlNumberFormat(12, "# ?/?");
        private static readonly XlNumberFormat fraction2 = new XlNumberFormat(13, "# ??/??");
        private static readonly XlNumberFormat shortDate = new XlNumberFormat(14, "mm-dd-yy");
        private static readonly XlNumberFormat longDate = new XlNumberFormat(15, "d-mmm-yy");
        private static readonly XlNumberFormat dayMonth = new XlNumberFormat(0x10, "d-mmm");
        private static readonly XlNumberFormat monthYear = new XlNumberFormat(0x11, "mmm-yy");
        private static readonly XlNumberFormat shortTime12 = new XlNumberFormat(0x12, "h:mm AM/PM");
        private static readonly XlNumberFormat longTime12 = new XlNumberFormat(0x13, "h:mm:ss AM/PM");
        private static readonly XlNumberFormat shortTime24 = new XlNumberFormat(20, "h:mm");
        private static readonly XlNumberFormat longTime24 = new XlNumberFormat(0x15, "h:mm:ss");
        private static readonly XlNumberFormat shortDateTime = new XlNumberFormat(0x16, "m/d/yy h:mm");
        private static readonly XlNumberFormat negativeParentheses = new XlNumberFormat(0x25, "#,##0;(#,##0)");
        private static readonly XlNumberFormat negativeParenthesesRed = new XlNumberFormat(0x26, "#,##0;[Red](#,##0)");
        private static readonly XlNumberFormat negativeParentheses2 = new XlNumberFormat(0x27, "#,##0.00;(#,##0.00)");
        private static readonly XlNumberFormat negativeParenthesesRed2 = new XlNumberFormat(40, "#,##0.00;[Red](#,##0.00)");
        private static readonly XlNumberFormat minuteSeconds = new XlNumberFormat(0x2d, "mm:ss");
        private static readonly XlNumberFormat timeSpan = new XlNumberFormat(0x2e, "[h]:mm:ss");
        private static readonly XlNumberFormat minuteSecondsMs = new XlNumberFormat(0x2f, "mm:ss.0");
        private static readonly XlNumberFormat scientific1 = new XlNumberFormat(0x30, "##0.0E+0");
        private static readonly XlNumberFormat text = new XlNumberFormat(0x31, "@");
        private static readonly Dictionary<string, XlNumberFormat> predefinedFormats = CreatePredefinedFormats();
        private static readonly Dictionary<int, XlNumberFormat> predefinedFormatIds = CreatePredefinedFormatIds();

        protected XlNumberFormat(int formatId, string formatCode)
        {
            this.FormatId = formatId;
            this.FormatCode = formatCode;
            this.IsDateTime = this.IsDateTimeFormat(formatCode);
        }

        private static Dictionary<int, XlNumberFormat> CreatePredefinedFormatIds() => 
            new Dictionary<int, XlNumberFormat> { 
                { 
                    0,
                    general
                },
                { 
                    1,
                    number
                },
                { 
                    2,
                    number2
                },
                { 
                    3,
                    numberWithSeparator
                },
                { 
                    4,
                    numberWithSeparator2
                },
                { 
                    9,
                    percentage
                },
                { 
                    10,
                    percentage2
                },
                { 
                    11,
                    scientific
                },
                { 
                    12,
                    fraction
                },
                { 
                    13,
                    fraction2
                },
                { 
                    14,
                    shortDate
                },
                { 
                    15,
                    longDate
                },
                { 
                    0x10,
                    dayMonth
                },
                { 
                    0x11,
                    monthYear
                },
                { 
                    0x12,
                    shortTime12
                },
                { 
                    0x13,
                    longTime12
                },
                { 
                    20,
                    shortTime24
                },
                { 
                    0x15,
                    longTime24
                },
                { 
                    0x16,
                    shortDateTime
                },
                { 
                    0x25,
                    negativeParentheses
                },
                { 
                    0x26,
                    negativeParenthesesRed
                },
                { 
                    0x27,
                    negativeParentheses2
                },
                { 
                    40,
                    negativeParenthesesRed2
                },
                { 
                    0x2d,
                    minuteSeconds
                },
                { 
                    0x2e,
                    timeSpan
                },
                { 
                    0x2f,
                    minuteSecondsMs
                },
                { 
                    0x30,
                    scientific1
                },
                { 
                    0x31,
                    text
                }
            };

        private static Dictionary<string, XlNumberFormat> CreatePredefinedFormats() => 
            new Dictionary<string, XlNumberFormat> { 
                { 
                    "0",
                    number
                },
                { 
                    "0.00",
                    number2
                },
                { 
                    "#,##0",
                    numberWithSeparator
                },
                { 
                    "#,##0.00",
                    numberWithSeparator2
                },
                { 
                    "0%",
                    percentage
                },
                { 
                    "0.00%",
                    percentage2
                },
                { 
                    "0.00E+00",
                    scientific
                },
                { 
                    "# ?/?",
                    fraction
                },
                { 
                    "# ??/??",
                    fraction2
                },
                { 
                    "mm-dd-yy",
                    shortDate
                },
                { 
                    "d-mmm-yy",
                    longDate
                },
                { 
                    "d-mmm",
                    dayMonth
                },
                { 
                    "mmm-yy",
                    monthYear
                },
                { 
                    "h:mm AM/PM",
                    shortTime12
                },
                { 
                    "h:mm:ss AM/PM",
                    longTime12
                },
                { 
                    "h:mm",
                    shortTime24
                },
                { 
                    "h:mm:ss",
                    longTime24
                },
                { 
                    "m/d/yy h:mm",
                    shortDateTime
                },
                { 
                    "#,##0;(#,##0)",
                    negativeParentheses
                },
                { 
                    "#,##0;[Red](#,##0)",
                    negativeParenthesesRed
                },
                { 
                    "#,##0.00;(#,##0.00)",
                    negativeParentheses2
                },
                { 
                    "#,##0.00;[Red](#,##0.00)",
                    negativeParenthesesRed2
                },
                { 
                    "mm:ss",
                    minuteSeconds
                },
                { 
                    "[h]:mm:ss",
                    timeSpan
                },
                { 
                    "mm:ss.0",
                    minuteSecondsMs
                },
                { 
                    "##0.0E+0",
                    scientific1
                },
                { 
                    "@",
                    text
                }
            };

        internal static XlNumberFormat FromId(int id)
        {
            XlNumberFormat format;
            if (!predefinedFormatIds.TryGetValue(id, out format))
            {
                format = null;
            }
            return format;
        }

        internal string GetLocalizedFormatCode(CultureInfo culture)
        {
            if (string.IsNullOrEmpty(this.FormatCode))
            {
                return string.Empty;
            }
            StringBuilder builder = new StringBuilder();
            List<string> list = SplitFormatCode(this.FormatCode);
            XlExportNumberFormatConverter converter = new XlExportNumberFormatConverter();
            for (int i = 0; i < list.Count; i++)
            {
                if (i > 0)
                {
                    builder.Append(";");
                }
                string formatString = list[i];
                if (this.IsDateTimeFormat(formatString))
                {
                    builder.Append(converter.GetLocalDateFormatString(formatString, culture));
                }
                else
                {
                    builder.Append(converter.GetLocalFormatString(formatString, culture));
                }
            }
            return builder.ToString();
        }

        private bool IsDateTimeFormat(string formatString)
        {
            if (string.IsNullOrEmpty(formatString))
            {
                return false;
            }
            bool flag = false;
            bool flag2 = false;
            bool flag3 = false;
            bool flag4 = true;
            bool flag5 = false;
            int length = formatString.Length;
            for (int i = 0; i < length; i++)
            {
                char ch = formatString[i];
                if (flag)
                {
                    if (ch == '"')
                    {
                        flag = false;
                    }
                }
                else if (flag3)
                {
                    if (ch == ']')
                    {
                        flag3 = false;
                    }
                }
                else if (flag2)
                {
                    flag2 = false;
                }
                else if (ch == '\\')
                {
                    flag2 = true;
                }
                else if (ch == '[')
                {
                    if (((i + 1) < length) && ((formatString[i + 1] != 'h') && ((formatString[i + 1] != 'm') && (formatString[i + 1] != 's'))))
                    {
                        flag3 = true;
                    }
                }
                else if (ch == '"')
                {
                    flag = true;
                }
                else if (ch == '/')
                {
                    if (((i + 1) >= length) || (formatString[i + 1] != 'P'))
                    {
                        flag4 = true;
                    }
                }
                else if (ch == ':')
                {
                    flag4 = false;
                }
                else if (ch == 'd')
                {
                    flag5 |= true;
                    flag4 = true;
                }
                else if (ch == 'y')
                {
                    flag5 |= true;
                    flag4 = true;
                }
                else if (ch == 'M')
                {
                    if (((i <= 0) || (formatString[i - 1] != 'A')) && ((i <= 0) || (formatString[i - 1] != 'P')))
                    {
                        flag5 |= true;
                        flag4 = true;
                    }
                }
                else if (ch == 'm')
                {
                    if (flag4 && ((((i + 1) < length) && (formatString[i + 1] == ':')) || (((i + 2) < length) && (formatString[i + 2] == ':'))))
                    {
                        flag4 = false;
                    }
                    flag5 |= true;
                }
                else if ((ch == 'h') || (ch == 'H'))
                {
                    flag5 |= true;
                    flag4 = false;
                }
                else if (ch != 's')
                {
                    flag4 = true;
                }
                else
                {
                    flag5 |= true;
                    flag4 = false;
                }
            }
            return flag5;
        }

        public static implicit operator XlNumberFormat(string value)
        {
            XlNumberFormat format;
            if (string.IsNullOrEmpty(value))
            {
                return general;
            }
            if (!predefinedFormats.TryGetValue(value, out format))
            {
                format = new XlNumberFormat(-1, value);
            }
            return format;
        }

        internal static List<string> SplitFormatCode(string formatCode)
        {
            List<string> list = new List<string>();
            bool flag = false;
            bool flag2 = false;
            StringBuilder builder = new StringBuilder();
            int num = 0;
            while (true)
            {
                while (true)
                {
                    if (num >= formatCode.Length)
                    {
                        list.Add(builder.ToString());
                        return list;
                    }
                    char ch = formatCode[num];
                    if (ch == '"')
                    {
                        if (flag2)
                        {
                            flag2 = false;
                        }
                        else
                        {
                            flag = !flag;
                        }
                    }
                    else if (ch == '\\')
                    {
                        if (!flag)
                        {
                            flag2 = !flag2;
                        }
                    }
                    else if (ch != ';')
                    {
                        if (flag2)
                        {
                            flag2 = false;
                        }
                    }
                    else if (flag2)
                    {
                        flag2 = false;
                    }
                    else if (!flag)
                    {
                        list.Add(builder.ToString());
                        builder.Clear();
                        break;
                    }
                    builder.Append(ch);
                    break;
                }
                num++;
            }
        }

        public int FormatId { get; private set; }

        public string FormatCode { get; private set; }

        public bool IsDateTime { get; private set; }

        public static XlNumberFormat General =>
            general;

        public static XlNumberFormat Number =>
            number;

        public static XlNumberFormat Number2 =>
            number2;

        public static XlNumberFormat NumberWithThousandSeparator =>
            numberWithSeparator;

        public static XlNumberFormat NumberWithThousandSeparator2 =>
            numberWithSeparator2;

        public static XlNumberFormat Percentage =>
            percentage;

        public static XlNumberFormat Percentage2 =>
            percentage2;

        public static XlNumberFormat Scientific =>
            scientific;

        public static XlNumberFormat Fraction =>
            fraction;

        public static XlNumberFormat Fraction2 =>
            fraction2;

        public static XlNumberFormat ShortDate =>
            shortDate;

        public static XlNumberFormat LongDate =>
            longDate;

        public static XlNumberFormat DayMonth =>
            dayMonth;

        public static XlNumberFormat MonthYear =>
            monthYear;

        public static XlNumberFormat ShortTime12 =>
            shortTime12;

        public static XlNumberFormat LongTime12 =>
            longTime12;

        public static XlNumberFormat ShortTime24 =>
            shortTime24;

        public static XlNumberFormat LongTime24 =>
            longTime24;

        public static XlNumberFormat ShortDateTime =>
            shortDateTime;

        public static XlNumberFormat NegativeParentheses =>
            negativeParentheses;

        public static XlNumberFormat NegativeParenthesesRed =>
            negativeParenthesesRed;

        public static XlNumberFormat NegativeParentheses2 =>
            negativeParentheses2;

        public static XlNumberFormat NegativeParenthesesRed2 =>
            negativeParenthesesRed2;

        public static XlNumberFormat MinuteSeconds =>
            minuteSeconds;

        public static XlNumberFormat Span =>
            timeSpan;

        public static XlNumberFormat MinuteSecondsMs =>
            minuteSecondsMs;

        public static XlNumberFormat Scientific1 =>
            scientific1;

        public static XlNumberFormat Text =>
            text;
    }
}

