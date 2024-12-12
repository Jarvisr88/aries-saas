namespace Devart.Common
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Runtime.InteropServices;
    using System.Text;

    internal class z
    {
        private const string a = ".";

        public static bool a(string A_0) => 
            (A_0.Length > 0) && (A_0[0] == '-');

        private static bool a(string A_0, NumberFormatInfo A_1)
        {
            int index = A_0.IndexOf("e", StringComparison.CurrentCultureIgnoreCase);
            if (index > -1)
            {
                A_0 = A_0.Substring(0, index);
            }
            char[] trimChars = new char[] { '0' };
            A_0 = A_0.Trim(trimChars);
            return ((A_0 == string.Empty) || (A_0 == A_1.NumberDecimalSeparator));
        }

        private static int a(string A_0, string A_1)
        {
            int num = 0;
            int num2 = -1;
            if (A_0 != "0")
            {
                num = A_0.Length - 1;
            }
            else if (A_1 != string.Empty)
            {
                int num3 = 0;
                while (true)
                {
                    if (num3 < A_1.Length)
                    {
                        if (A_1[num3] == '0')
                        {
                            num3++;
                            continue;
                        }
                        num2 = num3;
                    }
                    if (num2 > -1)
                    {
                        num = (0 - num2) - 1;
                    }
                    break;
                }
            }
            return num;
        }

        private static bool a(string A_0, out char A_1, out int A_2)
        {
            bool flag = true;
            A_1 = A_0[0];
            A_2 = -1;
            if ((A_1 != 'G') && ((A_1 != 'g') && ((A_1 != 'F') && ((A_1 != 'f') && ((A_1 != 'E') && ((A_1 != 'e') && ((A_1 != 'N') && ((A_1 != 'n') && ((A_1 != 'P') && ((A_1 != 'p') && ((A_1 != 'C') && (A_1 != 'c'))))))))))))
            {
                flag = false;
            }
            else if ((A_0.Length > 1) && !int.TryParse(A_0.Substring(1), out A_2))
            {
                flag = false;
            }
            return flag;
        }

        private static string a(string A_0, int A_1, bool A_2)
        {
            string str = string.Empty;
            if ((A_0.Length == 0) || (A_0.Length < A_1))
            {
                throw new IndexOutOfRangeException("Index was outside the bounds of the array");
            }
            if (A_0.Length == A_1)
            {
                str = A_0;
            }
            else if (byte.Parse(A_0[A_1].ToString()) < 5)
            {
                str = A_0.Substring(0, A_1);
            }
            else
            {
                byte num2 = (byte) (byte.Parse(A_0[A_1 - 1].ToString()) + 1);
                byte num = num2;
                str = A_0.Substring(0, A_1 - 1) + num2.ToString();
            }
            if (A_2)
            {
                char[] trimChars = new char[] { '0' };
                str = str.TrimEnd(trimChars);
            }
            return str;
        }

        private static string a(string A_0, int A_1, string A_2)
        {
            string format = "";
            switch (A_1)
            {
                case 0:
                    format = "{0}{1}";
                    break;

                case 1:
                    format = "{1}{0}";
                    break;

                case 2:
                    format = "{0} {1}";
                    break;

                case 3:
                    format = "{1} {0}";
                    break;

                default:
                    throw new ArgumentOutOfRangeException("currencyPositivePattern", "Valid values are between 0 and 3, inclusive.");
            }
            return string.Format(format, A_2, A_0);
        }

        public static string a(string A_0, string A_1, IFormatProvider A_2)
        {
            char ch;
            string str;
            string str2;
            int num4;
            int percentDecimalDigits = -1;
            int startIndex = 0;
            StringBuilder builder = new StringBuilder();
            int index = A_0.IndexOf(".");
            if (string.IsNullOrEmpty(A_1))
            {
                A_1 = "G";
            }
            NumberFormatInfo info = (A_2 != null) ? NumberFormatInfo.GetInstance(A_2) : NumberFormatInfo.CurrentInfo;
            if ((A_0[startIndex] == '-') || (A_0[startIndex] == '+'))
            {
                startIndex++;
            }
            if (index > -1)
            {
                str = A_0.Substring(startIndex, index - startIndex);
                str2 = A_0.Substring(index + 1);
            }
            else
            {
                str = A_0.Substring(startIndex, A_0.Length - startIndex);
                str2 = string.Empty;
            }
            if ((str.Length > 1) && (str[0] == '0'))
            {
                char[] chArray1 = new char[] { '0' };
                str = str.TrimStart(chArray1);
            }
            if (str == string.Empty)
            {
                str = "0";
            }
            char[] trimChars = new char[] { '0' };
            str2 = str2.TrimEnd(trimChars);
            if (str2 == string.Empty)
            {
                index = -1;
            }
            bool flag = a(A_0);
            if (!a(A_1, out ch, out percentDecimalDigits))
            {
                builder.Append(a(A_1, str, str2, percentDecimalDigits, info, flag));
                goto TR_0001;
            }
            else if (ch > 'P')
            {
                switch (ch)
                {
                    case 'c':
                        goto TR_000C;

                    case 'd':
                        break;

                    case 'e':
                        goto TR_000F;

                    case 'f':
                        goto TR_0012;

                    case 'g':
                        goto TR_0017;

                    default:
                        if (ch == 'n')
                        {
                            goto TR_0008;
                        }
                        else if (ch == 'p')
                        {
                            goto TR_0004;
                        }
                        break;
                }
                goto TR_0000;
            }
            else
            {
                switch (ch)
                {
                    case 'C':
                        goto TR_000C;

                    case 'D':
                        break;

                    case 'E':
                        goto TR_000F;

                    case 'F':
                        goto TR_0012;

                    case 'G':
                        goto TR_0017;

                    default:
                        if (ch == 'N')
                        {
                            goto TR_0008;
                        }
                        else if (ch == 'P')
                        {
                            goto TR_0004;
                        }
                        break;
                }
                goto TR_0000;
            }
            goto TR_0017;
        TR_0000:
            throw new FormatException("Format specifier was invalid.");
        TR_0001:
            return builder.ToString();
        TR_0004:
            if (percentDecimalDigits < 0)
            {
                percentDecimalDigits = info.PercentDecimalDigits;
            }
            builder.Append(b(str, str2, percentDecimalDigits, info, flag));
            goto TR_0001;
        TR_0008:
            if (percentDecimalDigits < 0)
            {
                percentDecimalDigits = info.NumberDecimalDigits;
            }
            builder.Append(a(str, str2, percentDecimalDigits, info, flag));
            goto TR_0001;
        TR_000C:
            if (percentDecimalDigits < 0)
            {
                percentDecimalDigits = info.CurrencyDecimalDigits;
            }
            builder.Append(c(str, str2, percentDecimalDigits, info, flag));
            goto TR_0001;
        TR_000F:
            if (percentDecimalDigits < 0)
            {
                percentDecimalDigits = 6;
            }
            builder.Append(a(str, str2, percentDecimalDigits, info, false, ch == 'E', flag));
            goto TR_0001;
        TR_0012:
            if (percentDecimalDigits < 0)
            {
                percentDecimalDigits = info.NumberDecimalDigits;
            }
            builder.Append(a(str, str2, percentDecimalDigits, info, false, flag));
            goto TR_0001;
        TR_0017:
            num4 = a(str, str2);
            if (percentDecimalDigits < 1)
            {
                percentDecimalDigits = 0x7fffffff;
            }
            if ((num4 > -5) && (num4 < percentDecimalDigits))
            {
                builder.Append(a(str, str2, percentDecimalDigits, info, true, flag));
            }
            else
            {
                builder.Append(a(str, str2, percentDecimalDigits, info, true, ch == 'G', flag));
            }
            goto TR_0001;
        }

        private static string a(string A_0, int[] A_1, string A_2)
        {
            int index = 0;
            int num2 = 0;
            string str = string.Empty;
            while (true)
            {
                if (index < A_1.Length)
                {
                    num2 = (A_1[index] == 0) ? A_0.Length : A_1[index++];
                }
                if (num2 >= A_0.Length)
                {
                    return (A_0 + str);
                }
                str = A_2 + A_0.Substring(A_0.Length - num2) + str;
                A_0 = A_0.Substring(0, A_0.Length - num2);
            }
        }

        private static string a(string A_0, int A_1, string A_2, string A_3)
        {
            string format = "";
            bool flag = false;
            switch (A_1)
            {
                case 0:
                    format = "({0}{1})";
                    flag = true;
                    break;

                case 1:
                    format = "{2}{0}{1}";
                    break;

                case 2:
                    format = "{0}{2}{1}";
                    break;

                case 3:
                    format = "{0}{1}{2}";
                    break;

                case 4:
                    format = "({1}{0})";
                    break;

                case 5:
                    format = "{2}{1}{0}";
                    break;

                case 6:
                    format = "{1}{2}{0}";
                    break;

                case 7:
                    format = "{1}{0}{2}";
                    break;

                case 8:
                    format = "{2}{1} {0}";
                    break;

                case 9:
                    format = "{2}{0} {1}";
                    break;

                case 10:
                    format = "{1} {0}{2}";
                    break;

                case 11:
                    format = "{0} {1}{2}";
                    break;

                case 12:
                    format = "{0} {2}{1}";
                    break;

                case 13:
                    format = "{1}{2} {0}";
                    break;

                case 14:
                    format = "({0} {1})";
                    break;

                case 15:
                    format = "({1} {0})";
                    break;

                default:
                    throw new ArgumentOutOfRangeException("currencyNegativePattern", "Valid values are between 0 and 15, inclusive.");
            }
            return (!flag ? string.Format(format, A_2, A_0, A_3) : string.Format(format, A_2, A_0));
        }

        private static string a(string A_0, string A_1, int A_2, NumberFormatInfo A_3, bool A_4)
        {
            byte num = 0;
            StringBuilder builder = new StringBuilder();
            if (A_2 > 0)
            {
                A_1 = (A_2 > A_1.Length) ? (A_1 + num.ToString($"D{A_2 - A_1.Length}")) : a(A_1, A_2, false);
            }
            else
            {
                A_0 = a(A_0 + A_1, A_0.Length, false);
                A_1 = string.Empty;
            }
            builder.Append(a(A_0, A_3.NumberGroupSizes, A_3.NumberGroupSeparator));
            if (A_1 != string.Empty)
            {
                builder.Append(A_3.PercentDecimalSeparator);
                builder.Append(A_1);
            }
            string str = builder.ToString();
            return ((!A_4 || a(str, A_3)) ? str : c(str, A_3.NumberNegativePattern, A_3.NegativeSign));
        }

        private static string a(string A_0, string A_1, int A_2, NumberFormatInfo A_3, bool A_4, bool A_5)
        {
            StringBuilder builder = new StringBuilder();
            byte num = 0;
            if (!A_4)
            {
                if (A_2 <= 0)
                {
                    builder.Append(a(A_0 + A_1, A_0.Length, false));
                }
                else
                {
                    builder.Append(A_0);
                    builder.Append(A_3.NumberDecimalSeparator);
                    if (A_2 <= A_1.Length)
                    {
                        builder.Append(a(A_1, A_2, false));
                    }
                    else
                    {
                        builder.Append(A_1);
                        builder.Append(num.ToString($"D{A_2 - A_1.Length}"));
                    }
                }
            }
            else if (A_0 == "0")
            {
                builder.Append(A_0);
                char[] trimChars = new char[] { '0' };
                string str2 = A_1.TrimStart(trimChars);
                if (str2 != string.Empty)
                {
                    builder.Append(A_3.NumberDecimalSeparator);
                    builder.Append(A_1.Substring(0, A_1.Length - str2.Length));
                    builder.Append(a(str2, Math.Min(A_2, str2.Length), true));
                }
            }
            else if (A_2 <= A_0.Length)
            {
                builder.Append(a(A_0 + A_1, A_2, false));
            }
            else
            {
                builder.Append(A_0);
                if (A_1.Length > 0)
                {
                    A_1 = a(A_1, Math.Min(A_2 - A_0.Length, A_1.Length), true);
                }
                if (A_1.Length > 0)
                {
                    builder.Append(A_3.NumberDecimalSeparator);
                    builder.Append(A_1);
                }
            }
            string str = builder.ToString();
            return ((!A_5 || a(str, A_3)) ? str : c(str, NumberFormatInfo.InvariantInfo.NumberNegativePattern, A_3.NegativeSign));
        }

        private static string a(string A_0, string A_1, string A_2, int A_3, NumberFormatInfo A_4, bool A_5)
        {
            StringBuilder builder = new StringBuilder();
            int num = 0;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            bool flag = false;
            bool flag2 = false;
            int num5 = 0;
            List<string> list = new List<string>();
            int num6 = -1;
            int num7 = -1;
            while (++num6 < A_0.Length)
            {
                char ch = A_0[num6];
                if (ch != '#')
                {
                    switch (ch)
                    {
                        case ',':
                        {
                            num5++;
                            continue;
                        }
                        case '.':
                        {
                            if (flag)
                            {
                                continue;
                            }
                            flag = true;
                            if ((num3 + num) == 0)
                            {
                                builder.Append("{" + ++num7 + "}");
                                num3++;
                                list.Add("#");
                            }
                            builder.Append("{" + ++num7 + "}");
                            list.Add(A_4.NumberDecimalSeparator);
                            continue;
                        }
                        case '0':
                            break;

                        default:
                        {
                            builder.Append(A_0[num6]);
                            continue;
                        }
                    }
                }
                if (((num + num3) > 0) && (num5 > 0))
                {
                    flag2 = true;
                }
                num5 = 0;
                if (flag)
                {
                    if (ch == '0')
                    {
                        num2++;
                    }
                    else
                    {
                        num4++;
                    }
                }
                else if (ch == '0')
                {
                    num++;
                }
                else
                {
                    num3++;
                }
                builder.Append("{" + ++num7 + "}");
                list.Add(ch.ToString());
            }
            if ((num2 + num4) <= 0)
            {
                A_1 = a(A_1 + A_2, A_1.Length, false);
                A_2 = string.Empty;
            }
            else if (A_2.Length > 0)
            {
                A_2 = a(A_2, Math.Min(A_2.Length, num2 + num4), false);
                char[] trimChars = new char[] { '0' };
                A_2 = A_2.TrimEnd(trimChars);
            }
            if (num5 > 0)
            {
                int num10 = num5 * 3;
                A_1 = (A_1.Length <= num10) ? "0" : A_1.Substring(0, A_1.Length - num10);
            }
            if (flag2)
            {
                A_1 = a(A_1, A_4.NumberGroupSizes, A_4.NumberGroupSeparator);
            }
            int num8 = num + num3;
            int num9 = num8 - A_1.Length;
            if (A_1 == "0")
            {
                num9 = num8;
            }
            bool flag3 = false;
            for (int i = 0; i < num8; i++)
            {
                if (num9 <= i)
                {
                    list[i] = ((num9 >= 0) || (i != 0)) ? A_1[i - num9].ToString() : A_1.Substring(0, (i - num9) + 1);
                }
                else if (list[i] == "#")
                {
                    list[i] = flag3 ? "0" : string.Empty;
                }
                else if (list[i] == "0")
                {
                    flag3 = true;
                }
            }
            if (flag)
            {
                if ((num2 <= 0) && ((num4 <= 0) || (A_2.Length <= 0)))
                {
                    list[num8] = string.Empty;
                }
                num8++;
            }
            for (int j = num8; j < list.Count; j++)
            {
                if ((j - num8) < A_2.Length)
                {
                    if (list[j] == "0")
                    {
                        num2--;
                    }
                    list[j] = A_2[j - num8].ToString();
                }
                else if (list[j] == "#")
                {
                    list[j] = (num2 == 0) ? string.Empty : "0";
                }
                else if (list[j] == "0")
                {
                    num2--;
                }
            }
            string str = string.Format(builder.ToString(), (object[]) list.ToArray());
            return ((!A_5 || a(str, A_4)) ? str : c(str, A_4.NumberNegativePattern, A_4.NegativeSign));
        }

        private static string a(string A_0, string A_1, int A_2, NumberFormatInfo A_3, bool A_4, bool A_5, bool A_6)
        {
            string str3;
            StringBuilder builder = new StringBuilder();
            char ch = A_5 ? 'E' : 'e';
            int num = 0;
            string positiveSign = A_3.PositiveSign;
            string negativeSign = A_3.NegativeSign;
            byte num2 = 0;
            if (A_4)
            {
                if (A_0 != "0")
                {
                    num = A_0.Length - 1;
                    A_0 = a(A_0, A_2, true);
                    builder.Append(A_0[0]);
                    if (A_0.Length > 1)
                    {
                        builder.Append(A_3.NumberDecimalSeparator);
                        builder.Append(A_0.Substring(1));
                    }
                    builder.Append(ch.ToString() + positiveSign + num.ToString("D2"));
                }
                else
                {
                    char[] trimChars = new char[] { '0' };
                    str3 = A_1.TrimStart(trimChars);
                    num = (A_1.Length - str3.Length) + 1;
                    str3 = a(str3, Math.Min(A_2, str3.Length), true);
                    builder.Append(str3[0]);
                    if (str3.Length > 1)
                    {
                        builder.Append(A_3.NumberDecimalSeparator);
                        builder.Append(str3.Substring(1));
                    }
                    builder.Append(ch.ToString() + negativeSign + num.ToString("D2"));
                }
            }
            else
            {
                string str5;
                num = a(A_0, A_1);
                if (num >= 0)
                {
                    str3 = A_0 + A_1;
                    str5 = positiveSign;
                }
                else
                {
                    num = 0 - num;
                    str3 = A_1.Substring(num - 1);
                    str5 = negativeSign;
                }
                if (A_2 <= 0)
                {
                    builder.Append(a(str3, 1, false));
                    builder.Append(ch.ToString() + str5 + num.ToString("D3"));
                }
                else
                {
                    builder.Append(str3[0]);
                    builder.Append(A_3.NumberDecimalSeparator);
                    if (A_2 <= (str3.Length - 1))
                    {
                        builder.Append(a(str3.Substring(1), A_2, false));
                    }
                    else
                    {
                        builder.Append(str3.Substring(1));
                        builder.Append(num2.ToString($"D{(A_2 - str3.Length) + 1}"));
                    }
                    builder.Append(ch.ToString() + str5 + num.ToString("D3"));
                }
            }
            string str4 = builder.ToString();
            return ((!A_6 || a(str4, A_3)) ? str4 : c(str4, NumberFormatInfo.InvariantInfo.NumberNegativePattern, A_3.NegativeSign));
        }

        private static string b(string A_0, int A_1, string A_2)
        {
            string format = "";
            switch (A_1)
            {
                case 0:
                    format = "{0} {1}";
                    break;

                case 1:
                    format = "{0}{1}";
                    break;

                case 2:
                    format = "{1}{0}";
                    break;

                case 3:
                    format = "{1} {0}";
                    break;

                default:
                    throw new ArgumentOutOfRangeException("percentPositivePattern", "Valid values are between 0 and 3, inclusive.");
            }
            return string.Format(format, A_0, A_2);
        }

        private static string b(string A_0, int A_1, string A_2, string A_3)
        {
            string format = "";
            switch (A_1)
            {
                case 0:
                    format = "{0}{1} {2}";
                    break;

                case 1:
                    format = "{0}{1}{2}";
                    break;

                case 2:
                    format = "{0}{2}{1}";
                    break;

                case 3:
                    format = "{2}{0}{1}";
                    break;

                case 4:
                    format = "{2}{1}{0}";
                    break;

                case 5:
                    format = "{1}{0}{2}";
                    break;

                case 6:
                    format = "{1}{2}{0}";
                    break;

                case 7:
                    format = "{0}{2} {1}";
                    break;

                case 8:
                    format = "{1} {2}{0}";
                    break;

                case 9:
                    format = "{2} {1}{0}";
                    break;

                case 10:
                    format = "{2} {0}{1}";
                    break;

                case 11:
                    format = "{1}{0} {2}";
                    break;

                default:
                    throw new ArgumentOutOfRangeException("percentNegativePattern", "Valid values are between 0 and 11, inclusive.");
            }
            return string.Format(format, A_3, A_0, A_2);
        }

        private static string b(string A_0, string A_1, int A_2, NumberFormatInfo A_3, bool A_4)
        {
            byte num = 0;
            StringBuilder builder = new StringBuilder();
            if (A_1.Length >= 2)
            {
                A_0 = A_0 + A_1.Substring(0, 2);
                A_1 = A_1.Substring(2);
            }
            else
            {
                A_0 = A_0 + A_1 + num.ToString($"D{2 - A_1.Length}");
                A_1 = string.Empty;
            }
            char[] trimChars = new char[] { '0' };
            A_0 = A_0.TrimStart(trimChars);
            if (A_0 == string.Empty)
            {
                A_0 = "0";
            }
            if (A_2 > 0)
            {
                A_1 = (A_2 > A_1.Length) ? (A_1 + num.ToString($"D{A_2 - A_1.Length}")) : a(A_1, A_2, false);
            }
            else
            {
                A_0 = a(A_0 + A_1, A_0.Length, false);
                A_1 = string.Empty;
            }
            builder.Append(a(A_0, A_3.PercentGroupSizes, A_3.PercentGroupSeparator));
            if (A_1 != string.Empty)
            {
                builder.Append(A_3.PercentDecimalSeparator);
                builder.Append(A_1);
            }
            string str = builder.ToString();
            return ((!A_4 || a(str, A_3)) ? b(str, A_3.PercentPositivePattern, A_3.PercentSymbol) : b(str, A_3.PercentNegativePattern, A_3.PercentSymbol, A_3.NegativeSign));
        }

        private static string c(string A_0, int A_1, string A_2)
        {
            string format = "";
            bool flag = false;
            switch (A_1)
            {
                case 0:
                    format = "({0})";
                    flag = true;
                    break;

                case 1:
                    format = "{0}{1}";
                    break;

                case 2:
                    format = "{0} {1}";
                    break;

                case 3:
                    format = "{1}{0}";
                    break;

                case 4:
                    format = "{1} {0}";
                    break;

                default:
                    throw new ArgumentOutOfRangeException("numberNegativePattern", "Valid values are between 0 and 4, inclusive.");
            }
            return (!flag ? string.Format(format, A_2, A_0) : string.Format(format, A_0));
        }

        private static string c(string A_0, string A_1, int A_2, NumberFormatInfo A_3, bool A_4)
        {
            byte num = 0;
            StringBuilder builder = new StringBuilder();
            if (A_2 > 0)
            {
                A_1 = (A_2 > A_1.Length) ? (A_1 + num.ToString($"D{A_2 - A_1.Length}")) : a(A_1, A_2, false);
            }
            else
            {
                A_0 = a(A_0 + A_1, A_0.Length, false);
                A_1 = string.Empty;
            }
            builder.Append(a(A_0, A_3.CurrencyGroupSizes, A_3.CurrencyGroupSeparator));
            if (A_1 != string.Empty)
            {
                builder.Append(A_3.CurrencyDecimalSeparator);
                builder.Append(A_1);
            }
            string str = builder.ToString();
            return ((!A_4 || a(str, A_3)) ? a(str, A_3.CurrencyPositivePattern, A_3.CurrencySymbol) : a(str, A_3.CurrencyNegativePattern, A_3.CurrencySymbol, A_3.NegativeSign));
        }
    }
}

