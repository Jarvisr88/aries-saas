namespace DevExpress.XtraExport
{
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    public class FormatStringToExcelNumberFormatConverter
    {
        private static readonly string[] currencyPositivePatterns = new string[] { "$n", "n$", "$ n", "n $" };
        private static readonly string[] currencyNegativePatterns;
        private static readonly string[] percentPositivePatterns;
        private static readonly string[] percentNegativePatterns;
        private static readonly List<char> numberChars;
        private static readonly List<char> dateTimeChars;
        private static readonly List<char> nonEscapedChars;
        private static readonly List<char> controlChars;
        private static readonly Dictionary<string, ExcelNumberFormat> numericStandardFormats;

        static FormatStringToExcelNumberFormatConverter()
        {
            string[] textArray2 = new string[0x10];
            textArray2[0] = "($n)";
            textArray2[1] = "-$n";
            textArray2[2] = "$-n";
            textArray2[3] = "$n-";
            textArray2[4] = "(n$)";
            textArray2[5] = "-n$";
            textArray2[6] = "n-$";
            textArray2[7] = "n$-";
            textArray2[8] = "-n $";
            textArray2[9] = "-$ n";
            textArray2[10] = "n $-";
            textArray2[11] = "$ n-";
            textArray2[12] = "$ -n";
            textArray2[13] = "n- $";
            textArray2[14] = "($ n)";
            textArray2[15] = "(n $)";
            currencyNegativePatterns = textArray2;
            percentPositivePatterns = new string[] { "n %", "n%", "%n", "% n" };
            string[] textArray4 = new string[12];
            textArray4[0] = "-n %";
            textArray4[1] = "-n%";
            textArray4[2] = "-%n";
            textArray4[3] = "%-n";
            textArray4[4] = "%n-";
            textArray4[5] = "n-%";
            textArray4[6] = "n%-";
            textArray4[7] = "-% n";
            textArray4[8] = "n %-";
            textArray4[9] = "% n-";
            textArray4[10] = "% -n";
            textArray4[11] = "n- %";
            percentNegativePatterns = textArray4;
            char[] collection = new char[] { '0', '#', '.', ',', '%', '‰', 'e', 'E', '+', '-' };
            numberChars = new List<char>(collection);
            char[] chArray2 = new char[] { 'd', 'D', 'm', 'M', 'y', 'Y', 'h', 'H', 's', 'S' };
            dateTimeChars = new List<char>(chArray2);
            char[] chArray3 = new char[] { 
                '$', '-', '+', '/', '(', ')', ':', '!', '^', '&', '\'', '~', '{', '}', '<', '>',
                '=', '"'
            };
            nonEscapedChars = new List<char>(chArray3);
            char[] chArray4 = new char[] { '0', '#', '?', '.', '%', ',', '\\', '*', '_', ' ' };
            controlChars = new List<char>(chArray4);
            numericStandardFormats = CreateNumericStandardFormats();
        }

        public ExcelNumberFormat CalculateFinalGenericFormat(string formattedValue, CultureInfo culture)
        {
            int from = formattedValue.LastIndexOf('e');
            if (from < 0)
            {
                from = formattedValue.LastIndexOf('E');
            }
            int num2 = formattedValue.LastIndexOf(culture.NumberFormat.NumberDecimalSeparator);
            if ((from < 0) && (num2 < 0))
            {
                return new ExcelNumberFormat(1, "0");
            }
            if (from < 0)
            {
                int count = (formattedValue.Length - 1) - num2;
                return ((count != 2) ? new ExcelNumberFormat(-1, "0." + new string('0', count)) : new ExcelNumberFormat(2, "0.00"));
            }
            if (num2 < 0)
            {
                int num4 = this.CalculateFirstDigitIndex(formattedValue, from);
                return ((num4 >= 0) ? new ExcelNumberFormat(-1, "0" + formattedValue[from].ToString() + formattedValue.Substring(from + 1, (num4 - from) - 1) + new string('0', formattedValue.Length - num4)) : new ExcelNumberFormat(0, ""));
            }
            if (from <= num2)
            {
                return new ExcelNumberFormat(0, "");
            }
            int num5 = this.CalculateFirstDigitIndex(formattedValue, from);
            if (num5 < 0)
            {
                return new ExcelNumberFormat(0, "");
            }
            string[] textArray1 = new string[] { "0.", new string('0', (from - num2) - 1), formattedValue[from].ToString(), formattedValue.Substring(from + 1, (num5 - from) - 1), new string('0', formattedValue.Length - num5) };
            return new ExcelNumberFormat(-1, string.Concat(textArray1));
        }

        private int CalculateFirstDigitIndex(string text, int from)
        {
            int length = text.Length;
            for (int i = from; i < length; i++)
            {
                if ((text[i] >= '0') && (text[i] <= '9'))
                {
                    return i;
                }
            }
            return -1;
        }

        private int CalculateFirstNonDigitIndex(string text, int from)
        {
            int length = text.Length;
            for (int i = from; i < length; i++)
            {
                if ((text[i] < '0') || (text[i] > '9'))
                {
                    return i;
                }
            }
            return -1;
        }

        private string CalculateNegativeCurrencyFormat(string numberFormatString, CultureInfo culture) => 
            this.CalculatePatternFormatCore(numberFormatString, culture, currencyNegativePatterns, culture.NumberFormat.CurrencyNegativePattern);

        private string CalculateNegativePercentFormat(string numberFormatString, CultureInfo culture) => 
            this.CalculatePatternFormatCore(numberFormatString, culture, percentNegativePatterns, culture.NumberFormat.PercentNegativePattern);

        private string CalculatePatternFormatCore(string numberFormatString, CultureInfo culture, string[] patterns, int patternIndex)
        {
            if ((patternIndex < 0) || (patternIndex >= patterns.Length))
            {
                patternIndex = 0;
            }
            NumberFormatInfo numberFormat = culture.NumberFormat;
            StringBuilder builder = new StringBuilder();
            string str = patterns[patternIndex];
            int length = str.Length;
            int num2 = 0;
            while (true)
            {
                while (true)
                {
                    if (num2 >= length)
                    {
                        return builder.ToString();
                    }
                    char ch = str[num2];
                    if (ch <= '%')
                    {
                        if (ch == '$')
                        {
                            builder.Append(numberFormat.CurrencySymbol);
                            break;
                        }
                        if (ch == '%')
                        {
                            builder.Append(numberFormat.PercentSymbol);
                            break;
                        }
                    }
                    else
                    {
                        if (ch == '-')
                        {
                            builder.Append(numberFormat.NegativeSign);
                            break;
                        }
                        if (ch == 'n')
                        {
                            builder.Append(numberFormatString);
                            break;
                        }
                    }
                    builder.Append(str[num2]);
                    break;
                }
                num2++;
            }
        }

        private string CalculatePositiveCurrencyFormat(string numberFormatString, CultureInfo culture) => 
            this.CalculatePatternFormatCore(numberFormatString, culture, currencyPositivePatterns, culture.NumberFormat.CurrencyPositivePattern);

        private string CalculatePositivePercentFormat(string numberFormatString, CultureInfo culture) => 
            this.CalculatePatternFormatCore(numberFormatString, culture, percentPositivePatterns, culture.NumberFormat.PercentPositivePattern);

        public ExcelNumberFormat ConvertDateTime(string formatString, CultureInfo culture)
        {
            DateTimeFormatInfo dateTimeFormat = culture.DateTimeFormat;
            return ((formatString != "d") ? ((formatString != "D") ? ((formatString != "f") ? ((formatString != "F") ? ((formatString != "g") ? ((formatString != "G") ? (((formatString == "m") || (formatString == "M")) ? new ExcelNumberFormat(-1, this.ProcessNetDateTimePattern(dateTimeFormat.MonthDayPattern)) : ((formatString != "s") ? ((formatString != "t") ? ((formatString != "T") ? ((formatString != "u") ? (((formatString == "y") || (formatString == "Y")) ? new ExcelNumberFormat(-1, this.ProcessNetDateTimePattern(dateTimeFormat.YearMonthPattern)) : new ExcelNumberFormat(-1, this.ProcessNetDateTimePattern(formatString))) : new ExcelNumberFormat(-1, this.ProcessNetDateTimePattern(dateTimeFormat.UniversalSortableDateTimePattern))) : new ExcelNumberFormat(-1, "[$-F400]" + this.ProcessNetDateTimePattern(dateTimeFormat.LongTimePattern))) : new ExcelNumberFormat(-1, this.ProcessNetDateTimePattern(dateTimeFormat.ShortTimePattern))) : new ExcelNumberFormat(-1, this.ProcessNetDateTimePattern(dateTimeFormat.SortableDateTimePattern)))) : new ExcelNumberFormat(-1, this.ProcessNetDateTimePattern(dateTimeFormat.ShortDatePattern + " " + dateTimeFormat.LongTimePattern))) : new ExcelNumberFormat(-1, this.ProcessNetDateTimePattern(dateTimeFormat.ShortDatePattern + " " + dateTimeFormat.ShortTimePattern))) : new ExcelNumberFormat(-1, this.ProcessNetDateTimePattern(dateTimeFormat.FullDateTimePattern))) : new ExcelNumberFormat(-1, this.ProcessNetDateTimePattern(dateTimeFormat.LongDatePattern + " " + dateTimeFormat.ShortTimePattern))) : new ExcelNumberFormat(-1, "[$-F800]" + this.ProcessNetDateTimePattern(dateTimeFormat.LongDatePattern))) : new ExcelNumberFormat(-1, this.ProcessNetDateTimePattern(dateTimeFormat.ShortDatePattern)));
        }

        public ExcelNumberFormat ConvertNumeric(string formatString, CultureInfo culture)
        {
            if (string.IsNullOrEmpty(formatString))
            {
                return null;
            }
            ExcelNumberFormat format = this.TryConvertFromStandardNumericFormat(formatString, culture);
            return ((format == null) ? new ExcelNumberFormat(-1, this.ProcessNetNumberPattern(formatString)) : format);
        }

        private ExcelNumberFormat CreateCompositeFormat(string positiveFormat, string negativeFormat, string negativeSign, string specialSign)
        {
            if (!string.IsNullOrEmpty(negativeSign) && !string.IsNullOrEmpty(specialSign))
            {
                if (negativeFormat.StartsWith(negativeSign) && (negativeFormat.EndsWith(specialSign) && positiveFormat.EndsWith(specialSign)))
                {
                    char[] trimChars = new char[] { negativeSign[0] };
                    if (positiveFormat == negativeFormat.TrimStart(trimChars))
                    {
                        return new ExcelNumberFormat(-1, positiveFormat);
                    }
                }
                if (negativeFormat.EndsWith(negativeSign) && (negativeFormat.StartsWith(specialSign) && positiveFormat.StartsWith(specialSign)))
                {
                    char[] trimChars = new char[] { negativeSign[0] };
                    if (positiveFormat == negativeFormat.TrimEnd(trimChars))
                    {
                        return new ExcelNumberFormat(-1, positiveFormat);
                    }
                }
            }
            string str = positiveFormat;
            string[] textArray1 = new string[] { positiveFormat, ";", negativeFormat, ";", str, ";@" };
            return new ExcelNumberFormat(-1, string.Concat(textArray1));
        }

        private string CreateExcelLocaleString(CultureInfo culture)
        {
            int num = LanguageIdToCultureConverter.Convert(culture);
            num ??= LanguageIdToCultureConverter.Convert(new CultureInfo("en-US"));
            int num2 = num & 0xffff;
            return $"[${culture.NumberFormat.CurrencySymbol}-{num2:X4}]";
        }

        private static Dictionary<string, ExcelNumberFormat> CreateNumericStandardFormats() => 
            new Dictionary<string, ExcelNumberFormat> { 
                { 
                    "n",
                    new ExcelNumberFormat(4, "#,##0.00")
                },
                { 
                    "N",
                    new ExcelNumberFormat(4, "#,##0.00")
                },
                { 
                    "n0",
                    new ExcelNumberFormat(3, "#,##0")
                },
                { 
                    "N0",
                    new ExcelNumberFormat(3, "#,##0")
                },
                { 
                    "n2",
                    new ExcelNumberFormat(4, "#,##0.00")
                },
                { 
                    "N2",
                    new ExcelNumberFormat(4, "#,##0.00")
                },
                { 
                    "d",
                    new ExcelNumberFormat(1, "0")
                },
                { 
                    "D",
                    new ExcelNumberFormat(1, "0")
                },
                { 
                    "d0",
                    new ExcelNumberFormat(1, "0")
                },
                { 
                    "D0",
                    new ExcelNumberFormat(1, "0")
                },
                { 
                    "d1",
                    new ExcelNumberFormat(1, "0")
                },
                { 
                    "D1",
                    new ExcelNumberFormat(1, "0")
                },
                { 
                    "e",
                    new ExcelNumberFormat(-1, "0.000000E+000")
                },
                { 
                    "E",
                    new ExcelNumberFormat(-1, "0.000000E+000")
                },
                { 
                    "e0",
                    new ExcelNumberFormat(-1, "0E+000")
                },
                { 
                    "E0",
                    new ExcelNumberFormat(-1, "0E+000")
                },
                { 
                    "f",
                    new ExcelNumberFormat(2, "0.00")
                },
                { 
                    "F",
                    new ExcelNumberFormat(2, "0.00")
                },
                { 
                    "f0",
                    new ExcelNumberFormat(1, "0")
                },
                { 
                    "F0",
                    new ExcelNumberFormat(1, "0")
                },
                { 
                    "f2",
                    new ExcelNumberFormat(2, "0.00")
                },
                { 
                    "F2",
                    new ExcelNumberFormat(2, "0.00")
                },
                { 
                    "g",
                    new ExcelNumberFormat(0, "")
                },
                { 
                    "G",
                    new ExcelNumberFormat(0, "")
                },
                { 
                    "g0",
                    new ExcelNumberFormat(0, "")
                },
                { 
                    "G0",
                    new ExcelNumberFormat(0, "")
                },
                { 
                    "c",
                    new ExcelNumberFormat(-1, "")
                },
                { 
                    "C",
                    new ExcelNumberFormat(-1, "")
                }
            };

        public ExcelNumberFormat GetExcelFormat(ExportCacheCellStyle style)
        {
            string formatString = style.FormatString;
            if (string.IsNullOrEmpty(formatString))
            {
                return new ExcelNumberFormat(style.PreparedCellType, "");
            }
            if (style.PreparedCellType == 0x31)
            {
                return new ExcelNumberFormat(-1, "\"" + Regex.Replace(formatString, "{[^}]*}", "\"@\"") + "\"");
            }
            string str2 = "";
            string str3 = "";
            if (formatString.IndexOf('{') != -1)
            {
                str2 = formatString.Substring(0, formatString.IndexOf('{'));
                str3 = formatString.Substring(formatString.IndexOf('}') + 1, (formatString.Length - formatString.IndexOf('}')) - 1);
                formatString = Regex.Match(formatString, "{([^}]*)}").Groups[1].Value;
                if (formatString.IndexOf(':') != -1)
                {
                    formatString = formatString.Remove(0, formatString.IndexOf(':') + 1);
                }
            }
            ExcelNumberFormat format = (style.PreparedCellType == 0x16) ? this.ConvertDateTime(formatString, this.Culture) : this.ConvertNumeric(formatString, this.Culture);
            if (string.IsNullOrEmpty(str2) && string.IsNullOrEmpty(str3))
            {
                return format;
            }
            string str4 = format.FormatString;
            if (!string.IsNullOrEmpty(str2))
            {
                str4 = "\"" + str2 + "\"" + str4;
            }
            if (!string.IsNullOrEmpty(str3))
            {
                str4 = str4 + "\"" + str3 + "\"";
            }
            return new ExcelNumberFormat(-1, str4);
        }

        protected bool IsControlChar(char item) => 
            controlChars.Contains(item);

        protected bool IsNonEscapedChar(char item) => 
            nonEscapedChars.Contains(item);

        protected virtual string ProcessNetCustomPattern(string pattern, List<char> patternChars, bool isDateTime)
        {
            bool flag = false;
            StringBuilder builder = new StringBuilder();
            int length = pattern.Length;
            for (int i = 0; i < length; i++)
            {
                if (flag)
                {
                    if ((pattern[i] != '\'') && (pattern[i] != '"'))
                    {
                        builder.Append(pattern[i]);
                    }
                    else
                    {
                        builder.Append('"');
                        flag = false;
                    }
                }
                else if (patternChars.Contains(pattern[i]))
                {
                    if (!flag)
                    {
                        builder.Append((pattern[i] == 'e') ? 'E' : pattern[i]);
                    }
                    else
                    {
                        builder.Append('"');
                        builder.Append(pattern[i]);
                        flag = false;
                    }
                }
                else if (this.IsControlChar(pattern[i]))
                {
                    if (flag)
                    {
                        builder.Append(pattern[i]);
                    }
                    else
                    {
                        builder.Append('\\');
                        builder.Append(pattern[i]);
                    }
                }
                else if (this.IsNonEscapedChar(pattern[i]))
                {
                    if ((pattern[i] != '\'') && (pattern[i] != '"'))
                    {
                        builder.Append(pattern[i]);
                    }
                    else
                    {
                        builder.Append('"');
                        flag = !flag;
                    }
                }
                else
                {
                    if (pattern[i] == ';')
                    {
                        if (flag)
                        {
                            builder.Append('"');
                            flag = false;
                        }
                    }
                    else if (!flag)
                    {
                        builder.Append('"');
                        flag = true;
                    }
                    builder.Append(pattern[i]);
                }
            }
            if (flag)
            {
                builder.Append('"');
            }
            return builder.ToString().Replace("\"tt\"", "AM/PM");
        }

        private string ProcessNetDateTimePattern(string pattern) => 
            this.ProcessNetCustomPattern(pattern, dateTimeChars, true);

        private string ProcessNetNumberPattern(string pattern) => 
            this.ProcessNetCustomPattern(pattern, numberChars, false);

        private ExcelNumberFormat TryConvertFromStandardNumericFormat(string formatString, CultureInfo culture)
        {
            string str = new string(formatString[0], 1);
            bool flag = (str == "c") || (str == "C");
            bool flag2 = (str == "p") || (str == "P");
            if (!flag && !flag2)
            {
                return this.TryConvertFromStandardNumericFormatCore(formatString, culture);
            }
            ExcelNumberFormat format = this.TryConvertFromStandardNumericFormatCore("n" + formatString.Substring(1), culture);
            if (format == null)
            {
                return null;
            }
            NumberFormatInfo numberFormat = culture.NumberFormat;
            if (!flag)
            {
                string str4 = this.CalculatePositivePercentFormat(format.FormatString, culture);
                return this.CreateCompositeFormat(str4, this.CalculateNegativePercentFormat(format.FormatString, culture), numberFormat.NegativeSign, numberFormat.PercentSymbol);
            }
            string positiveFormat = this.CalculatePositiveCurrencyFormat(format.FormatString, culture);
            ExcelNumberFormat format2 = this.CreateCompositeFormat(positiveFormat, this.CalculateNegativeCurrencyFormat(format.FormatString, culture), numberFormat.NegativeSign, numberFormat.CurrencySymbol);
            return new ExcelNumberFormat(-1, string.IsNullOrEmpty(numberFormat.CurrencySymbol) ? format2.FormatString : format2.FormatString.Replace(numberFormat.CurrencySymbol, this.CreateExcelLocaleString(culture)));
        }

        private ExcelNumberFormat TryConvertFromStandardNumericFormatCore(string formatString, CultureInfo culture)
        {
            ExcelNumberFormat format;
            int num;
            if (numericStandardFormats.TryGetValue(formatString, out format))
            {
                return format;
            }
            string key = new string(formatString[0], 1);
            if (!numericStandardFormats.TryGetValue(key, out format))
            {
                return null;
            }
            if (formatString.Length == 1)
            {
                return format;
            }
            if (!int.TryParse(formatString.Substring(1, formatString.Length - 1), NumberStyles.Integer, culture, out num))
            {
                return null;
            }
            int num2 = format.FormatString.LastIndexOf('.');
            if (num2 < 0)
            {
                return ((format.FormatString.Length != 0) ? ((format.FormatString.Length == 1) ? new ExcelNumberFormat(-1, new string(format.FormatString[0], Math.Max(1, num))) : null) : format);
            }
            int startIndex = format.FormatString.LastIndexOf('e');
            if (startIndex < 0)
            {
                startIndex = format.FormatString.LastIndexOf('E');
            }
            if (startIndex >= 0)
            {
                return new ExcelNumberFormat(-1, format.FormatString.Substring(0, num2 + 1) + new string('0', num) + format.FormatString.Substring(startIndex));
            }
            int num4 = this.CalculateFirstNonDigitIndex(format.FormatString, num2 + 1);
            return ((num4 >= 0) ? new ExcelNumberFormat(-1, format.FormatString.Substring(0, num2 + 1) + new string('0', num) + format.FormatString.Substring(num4)) : new ExcelNumberFormat(-1, format.FormatString.Substring(0, num2 + 1) + new string('0', num)));
        }

        protected CultureInfo Culture =>
            Application.CurrentCulture;
    }
}

