namespace DevExpress.Export.Xl
{
    using DevExpress.Utils;
    using DevExpress.XtraExport;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class XlExportNumberFormatConverter : FormatStringToExcelNumberFormatConverter
    {
        private const int maxFormatStringLength = 0x97;
        private static LocalDateFormatInfo invariantFormatInfo = new LocalDateFormatInfo('d', 'm', 'y', 'h', 'm', 's', '/', ':');
        private static LocalDateFormatInfo latinFormatInfo = new LocalDateFormatInfo('d', 'm', 'a', 'h', 'm', 's', '/', ':');
        private static LocalDateFormatInfo russianFormatInfo = new LocalDateFormatInfo('Д', 'М', 'Г', 'ч', 'м', 'с', '.', ':');
        private static LocalDateFormatInfo deutchFormatInfo = new LocalDateFormatInfo('T', 'M', 'J', 'h', 'm', 's', '.', ':');
        private static LocalDateFormatInfo italianoFormatInfo = new LocalDateFormatInfo('g', 'm', 'a', 'h', 'm', 's', '/', ':');
        private static LocalDateFormatInfo norwegianFormatInfo = new LocalDateFormatInfo('d', 'm', '\x00e5', 't', 'm', 's', '.', ':');
        private static LocalDateFormatInfo finnishFormatInfo = new LocalDateFormatInfo('p', 'k', 'v', 't', 'm', 's', '.', ':');
        private static LocalDateFormatInfo greekFormatInfo = new LocalDateFormatInfo('η', 'μ', 'ε', 'ω', 'λ', 'δ', '/', ':');
        private static Dictionary<string, LocalDateFormatInfo> localDateFormatTable = CreateLocalDateFormatTable();

        private string ComposeFormatString(string prefix, string formatString, string postfix)
        {
            StringBuilder sb = new StringBuilder();
            List<string> parts = XlNumberFormat.SplitFormatCode(formatString);
            this.ComposeParts(prefix, postfix, sb, parts, parts.Count);
            if (sb.Length > 0x97)
            {
                if ((parts.Count == 4) && ((parts[3] == "@") && (!string.IsNullOrEmpty(parts[0]) && !string.IsNullOrEmpty(parts[1]))))
                {
                    sb.Clear();
                    this.ComposeParts(prefix, postfix, sb, parts, 3);
                    sb.Append(";@");
                }
                if (sb.Length > 0x97)
                {
                    sb.Clear();
                    this.ComposeParts(string.Empty, string.Empty, sb, parts, parts.Count);
                }
            }
            return sb.ToString();
        }

        private void ComposeParts(string prefix, string postfix, StringBuilder sb, List<string> parts, int count)
        {
            for (int i = 0; i < count; i++)
            {
                if (i > 0)
                {
                    sb.Append(";");
                }
                string str = parts[i];
                if (string.IsNullOrEmpty(str))
                {
                    if (i == 1)
                    {
                        str = parts[0];
                        if (!string.IsNullOrEmpty(str))
                        {
                            str = "-" + str;
                        }
                    }
                    else if (i == 2)
                    {
                        str = parts[0];
                    }
                    else if (i == 3)
                    {
                        str = "@";
                    }
                }
                if ((i == (parts.Count - 1)) && (str == @"\"))
                {
                    str = string.Empty;
                }
                if (!string.IsNullOrEmpty(str))
                {
                    if (!string.IsNullOrEmpty(prefix))
                    {
                        sb.AppendFormat("\"{0}\"", prefix);
                    }
                    sb.Append(str);
                    if (!string.IsNullOrEmpty(postfix))
                    {
                        sb.AppendFormat("\"{0}\"", postfix);
                    }
                }
                else if (!string.IsNullOrEmpty(prefix) || !string.IsNullOrEmpty(postfix))
                {
                    sb.Append("\"");
                    if (!string.IsNullOrEmpty(prefix))
                    {
                        sb.Append(prefix);
                    }
                    if (!string.IsNullOrEmpty(postfix))
                    {
                        sb.Append(postfix);
                    }
                    sb.Append("\"");
                }
            }
        }

        public ExcelNumberFormat Convert(string formatString, bool isDateTimeFormat, CultureInfo culture)
        {
            if (string.IsNullOrEmpty(formatString))
            {
                return null;
            }
            XlExportNetFormatParser parser = new XlExportNetFormatParser(formatString, isDateTimeFormat);
            if (string.IsNullOrEmpty(parser.FormatString))
            {
                return ((isDateTimeFormat || (string.IsNullOrEmpty(parser.Prefix) && string.IsNullOrEmpty(parser.Postfix))) ? null : new ExcelNumberFormat(-1, this.ComposeFormatString(parser.Prefix, "@", parser.Postfix)));
            }
            if (this.IsNotSupportedNumberFormat(parser.FormatString, isDateTimeFormat))
            {
                return null;
            }
            ExcelNumberFormat format = isDateTimeFormat ? base.ConvertDateTime(parser.FormatString, culture) : base.ConvertNumeric(parser.FormatString, culture);
            if (format == null)
            {
                return format;
            }
            if ((format.Id != -1) && (string.IsNullOrEmpty(parser.Prefix) && string.IsNullOrEmpty(parser.Postfix)))
            {
                return format;
            }
            if (!isDateTimeFormat && this.HasThousandBeforeDecimalSeparator(format.FormatString))
            {
                format = new ExcelNumberFormat(-1, this.RelocateThousand(format.FormatString));
            }
            return new ExcelNumberFormat(-1, this.ComposeFormatString(parser.Prefix, format.FormatString, parser.Postfix));
        }

        private char ConvertSubSeconds(char currentChar) => 
            ((currentChar == '.') || (currentChar == ',')) ? '.' : '0';

        private static Dictionary<string, LocalDateFormatInfo> CreateLocalDateFormatTable() => 
            new Dictionary<string, LocalDateFormatInfo> { 
                { 
                    "en",
                    invariantFormatInfo
                },
                { 
                    "en-US",
                    invariantFormatInfo
                },
                { 
                    "en-IN",
                    new LocalDateFormatInfo('d', 'm', 'y', 'h', 'm', 's', '-', ':')
                },
                { 
                    "ga-IE",
                    invariantFormatInfo
                },
                { 
                    "cy-GB",
                    invariantFormatInfo
                },
                { 
                    "ru",
                    russianFormatInfo
                },
                { 
                    "ru-RU",
                    russianFormatInfo
                },
                { 
                    "de",
                    deutchFormatInfo
                },
                { 
                    "de-DE",
                    deutchFormatInfo
                },
                { 
                    "de-LU",
                    new LocalDateFormatInfo('d', 'm', 'y', 'h', 'm', 's', '.', ':')
                },
                { 
                    "it",
                    italianoFormatInfo
                },
                { 
                    "it-IT",
                    italianoFormatInfo
                },
                { 
                    "it-CH",
                    new LocalDateFormatInfo('g', 'm', 'a', 'h', 'm', 's', '.', ':')
                },
                { 
                    "es",
                    latinFormatInfo
                },
                { 
                    "es-AR",
                    invariantFormatInfo
                },
                { 
                    "es-BO",
                    invariantFormatInfo
                },
                { 
                    "es-CL",
                    new LocalDateFormatInfo('d', 'm', 'y', 'h', 'm', 's', '-', ':')
                },
                { 
                    "es-CO",
                    invariantFormatInfo
                },
                { 
                    "es-CR",
                    invariantFormatInfo
                },
                { 
                    "es-DO",
                    invariantFormatInfo
                },
                { 
                    "es-EC",
                    invariantFormatInfo
                },
                { 
                    "es-ES",
                    latinFormatInfo
                },
                { 
                    "es-SV",
                    invariantFormatInfo
                },
                { 
                    "es-GT",
                    invariantFormatInfo
                },
                { 
                    "es-HN",
                    invariantFormatInfo
                },
                { 
                    "es-MX",
                    latinFormatInfo
                },
                { 
                    "es-NI",
                    invariantFormatInfo
                },
                { 
                    "es-PA",
                    invariantFormatInfo
                },
                { 
                    "es-PY",
                    invariantFormatInfo
                },
                { 
                    "es-PE",
                    invariantFormatInfo
                },
                { 
                    "es-PR",
                    invariantFormatInfo
                },
                { 
                    "es-US",
                    invariantFormatInfo
                },
                { 
                    "es-UY",
                    invariantFormatInfo
                },
                { 
                    "es-VE",
                    invariantFormatInfo
                },
                { 
                    "pt",
                    latinFormatInfo
                },
                { 
                    "pt-BR",
                    latinFormatInfo
                },
                { 
                    "pt-PT",
                    new LocalDateFormatInfo('d', 'm', 'a', 'h', 'm', 's', '-', ':')
                },
                { 
                    "fr",
                    new LocalDateFormatInfo('j', 'm', 'a', 'h', 'm', 's', '/', ':')
                },
                { 
                    "fr-CA",
                    new LocalDateFormatInfo('j', 'm', 'a', 'h', 'm', 's', '-', ':')
                },
                { 
                    "fr-FR",
                    new LocalDateFormatInfo('j', 'm', 'a', 'h', 'm', 's', '/', ':')
                },
                { 
                    "fr-LU",
                    invariantFormatInfo
                },
                { 
                    "fr-MC",
                    invariantFormatInfo
                },
                { 
                    "fr-CH",
                    new LocalDateFormatInfo('j', 'm', 'a', 'h', 'm', 's', '.', ':')
                },
                { 
                    "uk",
                    new LocalDateFormatInfo('d', 'm', 'y', 'h', 'm', 's', '.', ':')
                },
                { 
                    "uk-UA",
                    new LocalDateFormatInfo('d', 'm', 'y', 'h', 'm', 's', '.', ':')
                },
                { 
                    "be",
                    new LocalDateFormatInfo('Д', 'М', 'Г', 'ч', 'м', 'с', '.', ':')
                },
                { 
                    "be-BY",
                    new LocalDateFormatInfo('Д', 'М', 'Г', 'ч', 'м', 'с', '.', ':')
                },
                { 
                    "pl",
                    new LocalDateFormatInfo('d', 'm', 'r', 'g', 'm', 's', '-', ':')
                },
                { 
                    "pl-PL",
                    new LocalDateFormatInfo('d', 'm', 'r', 'g', 'm', 's', '-', ':')
                },
                { 
                    "cs",
                    new LocalDateFormatInfo('d', 'm', 'r', 'h', 'm', 's', '.', ':')
                },
                { 
                    "cs-CZ",
                    new LocalDateFormatInfo('d', 'm', 'r', 'h', 'm', 's', '.', ':')
                },
                { 
                    "da",
                    new LocalDateFormatInfo('d', 'm', '\x00e5', 't', 'm', 's', '-', ':')
                },
                { 
                    "da-DK",
                    new LocalDateFormatInfo('d', 'm', '\x00e5', 't', 'm', 's', '-', ':')
                },
                { 
                    "nl",
                    new LocalDateFormatInfo('d', 'm', 'j', 'u', 'm', 's', '-', ':')
                },
                { 
                    "nl-BE",
                    new LocalDateFormatInfo('d', 'm', 'j', 'u', 'm', 's', '/', ':')
                },
                { 
                    "nl-NL",
                    new LocalDateFormatInfo('d', 'm', 'j', 'u', 'm', 's', '-', ':')
                },
                { 
                    "fi",
                    finnishFormatInfo
                },
                { 
                    "fi-FI",
                    finnishFormatInfo
                },
                { 
                    "sv",
                    new LocalDateFormatInfo('D', 'M', '\x00c5', 't', 'm', 's', '-', ':')
                },
                { 
                    "sv-FI",
                    finnishFormatInfo
                },
                { 
                    "sv-SE",
                    new LocalDateFormatInfo('D', 'M', '\x00c5', 't', 'm', 's', '-', ':')
                },
                { 
                    "el",
                    greekFormatInfo
                },
                { 
                    "el-GR",
                    greekFormatInfo
                },
                { 
                    "he",
                    invariantFormatInfo
                },
                { 
                    "he-IL",
                    invariantFormatInfo
                },
                { 
                    "id",
                    invariantFormatInfo
                },
                { 
                    "id-ID",
                    invariantFormatInfo
                },
                { 
                    "no",
                    norwegianFormatInfo
                },
                { 
                    "nb-NO",
                    norwegianFormatInfo
                },
                { 
                    "nn-NO",
                    norwegianFormatInfo
                },
                { 
                    "sk",
                    new LocalDateFormatInfo('d', 'm', 'y', 'h', 'm', 's', '.', ':')
                },
                { 
                    "sk-SK",
                    new LocalDateFormatInfo('d', 'm', 'y', 'h', 'm', 's', '.', ':')
                },
                { 
                    "sl",
                    new LocalDateFormatInfo('d', 'm', 'y', 'h', 'm', 's', '.', ':')
                },
                { 
                    "sl-SI",
                    new LocalDateFormatInfo('d', 'm', 'y', 'h', 'm', 's', '.', ':')
                },
                { 
                    "bg",
                    new LocalDateFormatInfo('d', 'm', 'y', 'h', 'm', 's', '.', ':')
                },
                { 
                    "bg-BG",
                    new LocalDateFormatInfo('d', 'm', 'y', 'h', 'm', 's', '.', ':')
                },
                { 
                    "hr",
                    new LocalDateFormatInfo('d', 'm', 'y', 'h', 'm', 's', '.', ':')
                },
                { 
                    "hr-HR",
                    new LocalDateFormatInfo('d', 'm', 'y', 'h', 'm', 's', '.', ':')
                },
                { 
                    "hr-BA",
                    new LocalDateFormatInfo('d', 'm', 'y', 'h', 'm', 's', '.', ':')
                },
                { 
                    "hu",
                    new LocalDateFormatInfo('n', 'h', '\x00e9', '\x00f3', 'p', 'm', '.', ':')
                },
                { 
                    "hu-HU",
                    new LocalDateFormatInfo('n', 'h', '\x00e9', '\x00f3', 'p', 'm', '.', ':')
                },
                { 
                    "ro",
                    invariantFormatInfo
                },
                { 
                    "ro-RO",
                    invariantFormatInfo
                },
                { 
                    "et",
                    invariantFormatInfo
                },
                { 
                    "et-EE",
                    invariantFormatInfo
                },
                { 
                    "lv",
                    invariantFormatInfo
                },
                { 
                    "lv-LV",
                    invariantFormatInfo
                },
                { 
                    "lt",
                    invariantFormatInfo
                },
                { 
                    "lt-LT",
                    invariantFormatInfo
                },
                { 
                    "hy",
                    invariantFormatInfo
                },
                { 
                    "hy-AM",
                    invariantFormatInfo
                },
                { 
                    "az",
                    invariantFormatInfo
                },
                { 
                    "az-Cyrl-AZ",
                    invariantFormatInfo
                },
                { 
                    "az-Latn-AZ",
                    invariantFormatInfo
                },
                { 
                    "ka",
                    invariantFormatInfo
                },
                { 
                    "ka-GE",
                    invariantFormatInfo
                },
                { 
                    "ja",
                    invariantFormatInfo
                },
                { 
                    "ja-JP",
                    invariantFormatInfo
                },
                { 
                    "th",
                    invariantFormatInfo
                },
                { 
                    "th-TH",
                    invariantFormatInfo
                },
                { 
                    "vi",
                    invariantFormatInfo
                },
                { 
                    "vi-VN",
                    invariantFormatInfo
                },
                { 
                    "ko",
                    new LocalDateFormatInfo('d', 'm', 'y', 'h', 'm', 's', '-', ':')
                },
                { 
                    "ko-KR",
                    new LocalDateFormatInfo('d', 'm', 'y', 'h', 'm', 's', '-', ':')
                },
                { 
                    "kk",
                    russianFormatInfo
                },
                { 
                    "kk-KZ",
                    russianFormatInfo
                },
                { 
                    "zh",
                    invariantFormatInfo
                },
                { 
                    "zh-CN",
                    invariantFormatInfo
                },
                { 
                    "zh-SG",
                    invariantFormatInfo
                },
                { 
                    "zh-HK",
                    invariantFormatInfo
                },
                { 
                    "zh-MO",
                    invariantFormatInfo
                },
                { 
                    "zh-TW",
                    invariantFormatInfo
                },
                { 
                    "tr",
                    new LocalDateFormatInfo('g', 'a', 'y', 's', 'd', 'n', '.', ':')
                },
                { 
                    "tr-TR",
                    new LocalDateFormatInfo('g', 'a', 'y', 's', 'd', 'n', '.', ':')
                },
                { 
                    "ar",
                    invariantFormatInfo
                },
                { 
                    "ar-DZ",
                    new LocalDateFormatInfo('d', 'm', 'y', 'h', 'm', 's', '-', ':')
                },
                { 
                    "ar-BH",
                    invariantFormatInfo
                },
                { 
                    "ar-EG",
                    invariantFormatInfo
                },
                { 
                    "ar-IQ",
                    invariantFormatInfo
                },
                { 
                    "ar-JO",
                    invariantFormatInfo
                },
                { 
                    "ar-KW",
                    invariantFormatInfo
                },
                { 
                    "ar-LB",
                    invariantFormatInfo
                },
                { 
                    "ar-LY",
                    invariantFormatInfo
                },
                { 
                    "ar-SA",
                    invariantFormatInfo
                },
                { 
                    "ar-MA",
                    new LocalDateFormatInfo('d', 'm', 'y', 'h', 'm', 's', '-', ':')
                },
                { 
                    "ar-OM",
                    invariantFormatInfo
                },
                { 
                    "ar-QA",
                    invariantFormatInfo
                },
                { 
                    "ar-SY",
                    invariantFormatInfo
                },
                { 
                    "ar-TN",
                    new LocalDateFormatInfo('d', 'm', 'y', 'h', 'm', 's', '-', ':')
                },
                { 
                    "ar-YE",
                    invariantFormatInfo
                },
                { 
                    "fa",
                    invariantFormatInfo
                },
                { 
                    "fa-IR",
                    invariantFormatInfo
                }
            };

        public XlExportNetFormatType GetFormatType(string formatString, bool isDateTimeFormat, CultureInfo culture)
        {
            if (string.IsNullOrEmpty(formatString))
            {
                return XlExportNetFormatType.General;
            }
            XlExportNetFormatParser parser = new XlExportNetFormatParser(formatString, isDateTimeFormat);
            if (string.IsNullOrEmpty(parser.FormatString))
            {
                return ((isDateTimeFormat || (string.IsNullOrEmpty(parser.Prefix) && string.IsNullOrEmpty(parser.Postfix))) ? XlExportNetFormatType.General : XlExportNetFormatType.Custom);
            }
            if (this.IsNotSupportedNumberFormat(parser.FormatString, isDateTimeFormat))
            {
                return XlExportNetFormatType.NotSupported;
            }
            if (!string.IsNullOrEmpty(parser.Prefix) || !string.IsNullOrEmpty(parser.Postfix))
            {
                return XlExportNetFormatType.Custom;
            }
            ExcelNumberFormat format = isDateTimeFormat ? base.ConvertDateTime(parser.FormatString, culture) : base.ConvertNumeric(parser.FormatString, culture);
            return (((format == null) || (format.Id == 0)) ? XlExportNetFormatType.General : ((format.Id == -1) ? XlExportNetFormatType.Custom : XlExportNetFormatType.Standard));
        }

        private LocalDateFormatInfo GetLocalDateFormat(CultureInfo culture)
        {
            LocalDateFormatInfo info;
            if (!localDateFormatTable.TryGetValue(culture.Name, out info))
            {
                char[] separator = new char[] { '-' };
                string[] strArray = culture.Name.Split(separator, StringSplitOptions.None);
                if (!localDateFormatTable.TryGetValue(strArray[0], out info))
                {
                    info = localDateFormatTable["en-US"];
                }
            }
            return info;
        }

        public string GetLocalDateFormatString(string formatString, CultureInfo culture)
        {
            Guard.ArgumentNotNull(culture, "culture");
            if (string.IsNullOrEmpty(formatString))
            {
                return string.Empty;
            }
            LocalDateFormatInfo localDateFormat = this.GetLocalDateFormat(culture);
            StringBuilder builder = new StringBuilder();
            bool flag = false;
            bool flag2 = false;
            bool flag3 = false;
            bool flag4 = true;
            int length = formatString.Length;
            for (int i = 0; i < length; i++)
            {
                char ch = formatString[i];
                if (flag)
                {
                    builder.Append(ch);
                    if (ch == '"')
                    {
                        flag = false;
                    }
                }
                else if (flag3)
                {
                    builder.Append(ch);
                    if (ch == ']')
                    {
                        flag3 = false;
                    }
                }
                else if (flag2)
                {
                    builder.Append(ch);
                    flag2 = false;
                }
                else if (ch == '\\')
                {
                    builder.Append(ch);
                    flag2 = true;
                }
                else if (ch == '[')
                {
                    builder.Append(ch);
                    if (((i + 1) < length) && ((formatString[i + 1] != 'h') && ((formatString[i + 1] != 'm') && (formatString[i + 1] != 's'))))
                    {
                        flag3 = true;
                    }
                }
                else if (ch == '"')
                {
                    builder.Append(formatString[i]);
                    flag = true;
                }
                else if (ch == '/')
                {
                    if (((i + 1) < length) && (formatString[i + 1] == 'P'))
                    {
                        builder.Append(ch);
                    }
                    else
                    {
                        builder.Append(localDateFormat.DateSeparator);
                        flag4 = true;
                    }
                }
                else if (ch == ':')
                {
                    builder.Append(localDateFormat.TimeSeparator);
                    flag4 = false;
                }
                else if (ch == 'd')
                {
                    builder.Append(localDateFormat.DaySymbol);
                    flag4 = true;
                }
                else if (ch == 'y')
                {
                    builder.Append(localDateFormat.YearSymbol);
                    flag4 = true;
                }
                else if (ch == 'M')
                {
                    if (((i > 0) && (formatString[i - 1] == 'A')) || ((i > 0) && (formatString[i - 1] == 'P')))
                    {
                        builder.Append(ch);
                    }
                    else
                    {
                        builder.Append(localDateFormat.MonthSymbol);
                        flag4 = true;
                    }
                }
                else if (ch == 'm')
                {
                    if (flag4 && ((((i + 1) < length) && (formatString[i + 1] == ':')) || (((i + 2) < length) && (formatString[i + 2] == ':'))))
                    {
                        flag4 = false;
                    }
                    builder.Append(flag4 ? localDateFormat.MonthSymbol : localDateFormat.MinuteSymbol);
                }
                else if ((ch == 'h') || (ch == 'H'))
                {
                    builder.Append(localDateFormat.HourSymbol);
                    flag4 = false;
                }
                else if (ch == 's')
                {
                    builder.Append(localDateFormat.SecondSymbol);
                    flag4 = false;
                }
                else if (ch == '.')
                {
                    builder.Append(culture.NumberFormat.NumberDecimalSeparator);
                }
                else
                {
                    builder.Append(ch);
                    flag4 = true;
                }
            }
            return builder.ToString();
        }

        public string GetLocalFormatString(string formatString, CultureInfo culture)
        {
            Guard.ArgumentNotNull(culture, "culture");
            if (string.IsNullOrEmpty(formatString))
            {
                return string.Empty;
            }
            StringBuilder builder = new StringBuilder();
            bool flag = false;
            bool flag2 = false;
            bool flag3 = false;
            int length = formatString.Length;
            string str = culture.NumberFormat.NumberGroupSeparator.Replace('\x00a0', ' ');
            for (int i = 0; i < length; i++)
            {
                char ch = formatString[i];
                if (flag)
                {
                    builder.Append(ch);
                    if (ch == '"')
                    {
                        flag = false;
                    }
                }
                else if (flag3)
                {
                    builder.Append(ch);
                    if (ch == ']')
                    {
                        flag3 = false;
                    }
                }
                else if (flag2)
                {
                    builder.Append(ch);
                    flag2 = false;
                }
                else if (ch == '\\')
                {
                    builder.Append(ch);
                    flag2 = true;
                }
                else if (ch == '[')
                {
                    builder.Append(ch);
                    flag3 = true;
                }
                else if (ch == '"')
                {
                    builder.Append(formatString[i]);
                    flag = true;
                }
                else if (ch == '.')
                {
                    builder.Append(culture.NumberFormat.NumberDecimalSeparator);
                }
                else if (ch == ',')
                {
                    builder.Append(str);
                }
                else if ((str.Length != 1) || (ch != str[0]))
                {
                    builder.Append(ch);
                }
                else
                {
                    builder.Append('\\');
                    builder.Append(ch);
                }
            }
            return builder.ToString();
        }

        private bool HasThousandBeforeDecimalSeparator(string formatCode)
        {
            bool flag = false;
            bool flag2 = false;
            char ch = '\0';
            for (int i = 0; i < formatCode.Length; i++)
            {
                char ch2 = formatCode[i];
                if (ch2 == '"')
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
                else if (ch2 == '\\')
                {
                    if (!flag)
                    {
                        flag2 = !flag2;
                    }
                }
                else if (flag2)
                {
                    flag2 = false;
                    ch = '\0';
                }
                else if (!flag)
                {
                    if ((ch == ',') && (ch2 == '.'))
                    {
                        return true;
                    }
                    if ((ch2 == 'E') || ((ch2 == 'e') || (ch2 == '%')))
                    {
                        return false;
                    }
                    ch = ch2;
                }
            }
            return false;
        }

        private bool IsExponent(string pattern, int index)
        {
            int length = pattern.Length;
            char ch = pattern[index];
            bool flag = (ch == 'e') || (ch == 'E');
            if (flag)
            {
                char ch2 = (index > 0) ? pattern[index - 1] : ' ';
                char ch3 = (index < (length - 1)) ? pattern[index + 1] : ' ';
                flag = ((ch2 == '0') || ((ch2 == '#') || (ch2 == '.'))) ? (((ch3 == '+') || (ch3 == '-')) || (ch3 == '0')) : false;
            }
            return flag;
        }

        private bool IsNotSupportedNumberFormat(string formatString, bool isDateTimeFormat)
        {
            string str = new string(formatString[0], 1);
            bool flag = (str == "r") || (str == "R");
            return (!isDateTimeFormat ? (((str == "x") || (str == "X")) | flag) : ((flag | ((str == "o") || (str == "O"))) | (str == "U")));
        }

        private bool IsSubSeconds(string pattern, int index)
        {
            int length = pattern.Length;
            char ch = pattern[index];
            bool flag = (ch == '.') || (ch == ',');
            char ch2 = (index > 0) ? pattern[index - 1] : ' ';
            char ch3 = (index < (length - 1)) ? pattern[index + 1] : ' ';
            if (flag)
            {
                flag = (ch3 == 'f') || (ch3 == 'F');
            }
            else
            {
                flag = (ch == 'f') || (ch == 'F');
                if (flag)
                {
                    flag = ((ch2 == '.') || ((ch2 == ',') || (ch2 == 'f'))) || (ch2 == 'F');
                }
            }
            return flag;
        }

        private List<string> ParseFormatCode(string formatCode)
        {
            List<string> list = new List<string>();
            bool flag = false;
            bool flag2 = false;
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < formatCode.Length; i++)
            {
                char ch = formatCode[i];
                builder.Append(ch);
                if (ch == '"')
                {
                    if (flag2)
                    {
                        flag2 = false;
                        list.Add(builder.ToString());
                        builder.Clear();
                    }
                    else if (flag)
                    {
                        list.Add(builder.ToString());
                        builder.Clear();
                    }
                }
                else if (ch == '\\')
                {
                    if (!flag && flag2)
                    {
                        list.Add(builder.ToString());
                        builder.Clear();
                    }
                }
                else if (flag2)
                {
                    flag2 = false;
                    list.Add(builder.ToString());
                    builder.Clear();
                }
                else if (!flag)
                {
                    list.Add(builder.ToString());
                    builder.Clear();
                }
            }
            if (builder.Length > 0)
            {
                list.Add(builder.ToString());
            }
            return list;
        }

        protected override string ProcessNetCustomPattern(string pattern, List<char> patternChars, bool isDateTime)
        {
            bool flag = false;
            bool flag2 = false;
            bool flag3 = false;
            patternChars = this.StripExpChars(patternChars);
            StringBuilder builder = new StringBuilder();
            int num = 0;
            int length = pattern.Length;
            for (int i = 0; i < length; i++)
            {
                char ch = pattern[i];
                if (flag)
                {
                    if ((ch != '\'') && (ch != '"'))
                    {
                        builder.Append(ch);
                    }
                    else
                    {
                        builder.Append('"');
                        flag = false;
                    }
                }
                else if (flag3)
                {
                    builder.Append(ch);
                    flag3 = false;
                }
                else if (ch == '\\')
                {
                    if (!flag2)
                    {
                        builder.Append(ch);
                    }
                    flag3 = true;
                }
                else if (patternChars.Contains(ch))
                {
                    if (!flag2)
                    {
                        builder.Append(ch);
                    }
                    else
                    {
                        builder.Append('"');
                        builder.Append(ch);
                        flag2 = false;
                    }
                }
                else if (isDateTime && this.IsSubSeconds(pattern, i))
                {
                    if (flag2)
                    {
                        builder.Append('"');
                        builder.Append(this.ConvertSubSeconds(ch));
                        num++;
                        flag2 = false;
                    }
                    else if ((num + 1) <= 4)
                    {
                        builder.Append(this.ConvertSubSeconds(ch));
                    }
                }
                else if (this.IsExponent(pattern, i))
                {
                    if (!flag2)
                    {
                        builder.Append((ch == 'e') ? 'E' : ch);
                    }
                    else
                    {
                        builder.Append('"');
                        builder.Append((ch == 'e') ? 'E' : ch);
                        flag2 = false;
                    }
                }
                else if (base.IsControlChar(ch))
                {
                    if (flag2)
                    {
                        builder.Append(ch);
                    }
                    else
                    {
                        builder.Append('\\');
                        builder.Append(ch);
                    }
                }
                else if (base.IsNonEscapedChar(ch))
                {
                    if ((ch != '\'') && (ch != '"'))
                    {
                        builder.Append(ch);
                    }
                    else
                    {
                        builder.Append('"');
                        flag = !flag;
                    }
                }
                else
                {
                    if (ch != ';')
                    {
                        if (!flag2)
                        {
                            builder.Append('"');
                            flag2 = true;
                        }
                    }
                    else
                    {
                        if (flag2)
                        {
                            builder.Append('"');
                            flag2 = false;
                        }
                        flag3 = false;
                    }
                    builder.Append(pattern[i]);
                }
            }
            if (flag | flag2)
            {
                builder.Append('"');
            }
            return builder.ToString().Replace("\"tt\"", "AM/PM");
        }

        private string RelocateThousand(string formatString)
        {
            StringBuilder builder = new StringBuilder();
            List<string> list = XlNumberFormat.SplitFormatCode(formatString);
            for (int i = 0; i < list.Count; i++)
            {
                if (i > 0)
                {
                    builder.Append(";");
                }
                builder.Append(this.RelocateThousandCore(list[i]));
            }
            return builder.ToString();
        }

        private string RelocateThousandCore(string formatString)
        {
            if (string.IsNullOrEmpty(formatString))
            {
                return formatString;
            }
            List<string> tokens = this.ParseFormatCode(formatString);
            this.RelocateTokens(tokens);
            StringBuilder builder = new StringBuilder();
            foreach (string str in tokens)
            {
                builder.Append(str);
            }
            return builder.ToString();
        }

        private void RelocateTokens(List<string> tokens)
        {
            int index = tokens.IndexOf(".");
            while ((index > 0) && (tokens[index - 1] == ","))
            {
                index--;
                tokens.RemoveAt(index);
                int num2 = index + 1;
                while (true)
                {
                    if ((num2 >= tokens.Count) || ((tokens[num2] != "0") && (tokens[num2] != "#")))
                    {
                        tokens.Insert(num2, ",");
                        break;
                    }
                    num2++;
                }
            }
        }

        private List<char> StripExpChars(List<char> patternChars)
        {
            List<char> list = new List<char>();
            foreach (char ch in patternChars)
            {
                if ((ch != 'e') && (ch != 'E'))
                {
                    list.Add(ch);
                }
            }
            return list;
        }

        private class LocalDateFormatInfo
        {
            public LocalDateFormatInfo(char daySymbol, char monthSymbol, char yearSymbol, char hourSymbol, char minuteSymbol, char secondSymbol, char dateSeparator, char timeSeparator)
            {
                this.DaySymbol = daySymbol;
                this.MonthSymbol = monthSymbol;
                this.YearSymbol = yearSymbol;
                this.HourSymbol = hourSymbol;
                this.MinuteSymbol = minuteSymbol;
                this.SecondSymbol = secondSymbol;
                this.DateSeparator = dateSeparator;
                this.TimeSeparator = timeSeparator;
            }

            public char DaySymbol { get; private set; }

            public char MonthSymbol { get; private set; }

            public char YearSymbol { get; private set; }

            public char HourSymbol { get; private set; }

            public char MinuteSymbol { get; private set; }

            public char SecondSymbol { get; private set; }

            public char DateSeparator { get; private set; }

            public char TimeSeparator { get; private set; }
        }
    }
}

