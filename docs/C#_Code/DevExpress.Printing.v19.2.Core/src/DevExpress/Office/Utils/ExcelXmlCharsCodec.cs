namespace DevExpress.Office.Utils
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Text;

    public static class ExcelXmlCharsCodec
    {
        private const int escapeCapacity = 7;
        private const int extraCapacity = 14;
        private static string[] replacements = (from ch in Enumerable.Range(0, 0x20) select $"_x{ch:x4}_").ToArray<string>();

        public static string Decode(string value)
        {
            if (string.IsNullOrEmpty(value) || (value.IndexOf('_') < 0))
            {
                return value;
            }
            StringBuilder builder = new StringBuilder(7);
            int length = value.Length;
            StringBuilder builder2 = new StringBuilder(length);
            int num3 = 0;
            while (true)
            {
                while (true)
                {
                    if (num3 >= length)
                    {
                        if (builder.Length > 0)
                        {
                            builder2.Append(builder.ToString());
                        }
                        return builder2.ToString();
                    }
                    char ch = value[num3];
                    int num = builder.Length;
                    if (num > 0)
                    {
                        if (num == 1)
                        {
                            if (ch == 'x')
                            {
                                builder.Append(ch);
                                break;
                            }
                            builder2.Append('_');
                            builder.Length = 0;
                        }
                        else if ((num >= 2) && (num < 6))
                        {
                            if (((ch >= '0') && (ch <= '9')) || (((ch >= 'a') && (ch <= 'f')) || ((ch >= 'A') && (ch <= 'F'))))
                            {
                                builder.Append(ch);
                                break;
                            }
                            builder2.Append(builder.ToString());
                            builder.Length = 0;
                        }
                        else if (num == 6)
                        {
                            if (ch == '_')
                            {
                                builder2.Append((char) int.Parse(builder.ToString(2, 4), NumberStyles.HexNumber, CultureInfo.InvariantCulture));
                                builder.Length = 0;
                                break;
                            }
                            builder2.Append(builder.ToString());
                            builder.Length = 0;
                        }
                    }
                    if (ch == '_')
                    {
                        builder.Append(ch);
                    }
                    else
                    {
                        builder2.Append(ch);
                    }
                    break;
                }
                num3++;
            }
        }

        public static string Encode(string value)
        {
            char ch;
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }
            StringBuilder builder = new StringBuilder(7);
            int length = value.Length;
            StringBuilder builder2 = new StringBuilder(length + 14);
            int num3 = 0;
            goto TR_0023;
        TR_0004:
            num3++;
            goto TR_0023;
        TR_0012:
            if (ch == '_')
            {
                builder.Append(ch);
            }
            else if (ch == '\t')
            {
                builder2.Append(ch);
            }
            else if (ch == '\n')
            {
                builder2.Append("\r\n");
            }
            else if ((ch >= '\0') && (ch <= '\x001f'))
            {
                builder2.Append(replacements[ch]);
            }
            else if (ch == 0xfffe)
            {
                builder2.Append("_xfffe_");
            }
            else if (ch == 0xffff)
            {
                builder2.Append("_xffff_");
            }
            else
            {
                builder2.Append(ch);
            }
            goto TR_0004;
        TR_0023:
            while (true)
            {
                if (num3 >= length)
                {
                    if (builder.Length > 0)
                    {
                        builder2.Append(builder.ToString());
                    }
                    return builder2.ToString();
                }
                ch = value[num3];
                int num = builder.Length;
                if (num <= 0)
                {
                    goto TR_0012;
                }
                else if (num != 1)
                {
                    if ((num < 2) || (num >= 6))
                    {
                        if (num == 6)
                        {
                            if (ch != '_')
                            {
                                builder2.Append(builder.ToString());
                                builder.Length = 0;
                            }
                            else
                            {
                                builder.Append(ch);
                                builder2.Append("_x005F");
                                builder2.Append(builder.ToString());
                                builder.Length = 0;
                                goto TR_0004;
                            }
                        }
                    }
                    else
                    {
                        if ((ch >= '0') && (ch <= '9'))
                        {
                            break;
                        }
                        if ((ch >= 'a') && (ch <= 'f'))
                        {
                            break;
                        }
                        if ((ch >= 'A') && (ch <= 'F'))
                        {
                            break;
                        }
                        builder2.Append(builder.ToString());
                        builder.Length = 0;
                    }
                    goto TR_0012;
                }
                else
                {
                    if (ch != 'x')
                    {
                        builder2.Append('_');
                        builder.Length = 0;
                    }
                    else
                    {
                        builder.Append(ch);
                        goto TR_0004;
                    }
                    goto TR_0012;
                }
                break;
            }
            builder.Append(ch);
            goto TR_0004;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ExcelXmlCharsCodec.<>c <>9 = new ExcelXmlCharsCodec.<>c();

            internal string <.cctor>b__5_0(int ch) => 
                $"_x{ch:x4}_";
        }
    }
}

