namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public static class PdfCharSetStringParser
    {
        private static int? ConvertToHexadecimalDigit(char c)
        {
            if ((c >= '0') && (c <= '9'))
            {
                return new int?(c - '0');
            }
            if ((c >= 'A') && (c <= 'F'))
            {
                return new int?((c - 'A') + 10);
            }
            if ((c >= 'a') && (c <= 'f'))
            {
                return new int?((c - 'a') + 10);
            }
            return null;
        }

        public static IList<string> Parse(string charSetString)
        {
            char ch;
            charSetString = charSetString.Trim();
            if (charSetString[0] == '(')
            {
                int num2 = charSetString.LastIndexOf(')');
                if (num2 < 0)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                charSetString = charSetString.Substring(1, num2 - 1);
            }
            List<string> list = new List<string>();
            StringBuilder builder = null;
            bool flag = false;
            int length = charSetString.Length;
            int num3 = 0;
            goto TR_001A;
        TR_0003:
            num3++;
            goto TR_001A;
        TR_0006:
            if (!flag || (ch < '!'))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            builder = new StringBuilder();
            builder.Append(ch);
            goto TR_0003;
        TR_001A:
            while (true)
            {
                if (num3 >= length)
                {
                    if (builder != null)
                    {
                        list.Add(builder.ToString());
                    }
                    return list;
                }
                ch = charSetString[num3];
                if (ch > '\r')
                {
                    if (ch != ' ')
                    {
                        if (ch == '#')
                        {
                            if ((length - num3) >= 3)
                            {
                                int num4 = num3;
                                int? nullable = ConvertToHexadecimalDigit(charSetString[++num4]);
                                int? nullable2 = ConvertToHexadecimalDigit(charSetString[++num4]);
                                if ((nullable != null) && (nullable2 != null))
                                {
                                    builder = new StringBuilder();
                                    builder.Append((char) ((nullable.Value * 0x10) + nullable2.Value));
                                    num3 = num4;
                                    goto TR_0003;
                                }
                            }
                        }
                        else if (ch == '/')
                        {
                            if (flag)
                            {
                                if (builder == null)
                                {
                                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                                }
                                list.Add(builder.ToString());
                                builder = null;
                            }
                            flag = true;
                            goto TR_0003;
                        }
                        break;
                    }
                }
                else if (ch != '\0')
                {
                    switch (ch)
                    {
                        case '\t':
                        case '\n':
                        case '\f':
                        case '\r':
                            break;

                        default:
                            goto TR_0006;
                    }
                }
                if (builder != null)
                {
                    list.Add(builder.ToString());
                    builder = null;
                    flag = false;
                }
                goto TR_0003;
            }
            goto TR_0006;
        }
    }
}

