namespace DevExpress.Export.Xl
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class XlExportNetFormatParser
    {
        public XlExportNetFormatParser(string format, bool isDateTimeFormat)
        {
            this.Prefix = string.Empty;
            this.Postfix = string.Empty;
            this.FormatString = string.Empty;
            if (string.IsNullOrEmpty(format))
            {
                this.OriginalFormat = string.Empty;
            }
            else
            {
                this.OriginalFormat = format;
                if (!isDateTimeFormat || !this.HasMultipleOneLevelBraces(format))
                {
                    this.Parse(format);
                }
                else
                {
                    List<string> list = this.SplitByBraces(format);
                    string prefix = string.Empty;
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < list.Count; i++)
                    {
                        this.Parse(list[i]);
                        if (i == 0)
                        {
                            prefix = this.Prefix;
                        }
                        builder.Append(this.FormatString);
                        if (!string.IsNullOrEmpty(this.Postfix) && (i < (list.Count - 1)))
                        {
                            if (this.Postfix.Length == 1)
                            {
                                builder.Append('\\');
                                builder.Append(this.Postfix);
                            }
                            else
                            {
                                builder.Append('"');
                                builder.Append(this.Postfix);
                                builder.Append('"');
                            }
                        }
                    }
                    this.FormatString = builder.ToString();
                    this.Prefix = prefix;
                }
            }
        }

        private string CleanupPostfix(string text)
        {
            int braceIndex = this.GetBraceIndex(text, '{');
            if (braceIndex != -1)
            {
                char[] trimChars = new char[] { ' ' };
                text = text.Substring(0, braceIndex).TrimEnd(trimChars);
            }
            return text.Replace("{{", "{").Replace("}}", "}");
        }

        private string CleanupPrefix(string text) => 
            text.Replace("{{", "{").Replace("}}", "}");

        private int GetBraceIndex(string formatString, char brace)
        {
            int length = formatString.Length;
            int num2 = -1;
            bool flag = false;
            for (int i = 0; i < length; i++)
            {
                if (formatString[i] != brace)
                {
                    if (flag)
                    {
                        break;
                    }
                }
                else if (flag)
                {
                    flag = false;
                    num2 = -1;
                }
                else
                {
                    flag = true;
                    num2 = i;
                }
            }
            return num2;
        }

        private int GetMatchingBraceIndex(string formatString, char brace, int startIndex) => 
            (startIndex != -1) ? formatString.IndexOf(brace, startIndex) : this.GetBraceIndex(formatString, brace);

        private bool HasMultipleOneLevelBraces(string format)
        {
            int num = 0;
            bool flag = false;
            bool flag2 = false;
            for (int i = 0; i < format.Length; i++)
            {
                char ch = format[i];
                if (flag2)
                {
                    flag2 = false;
                }
                else if (ch == '\\')
                {
                    flag2 = true;
                }
                else if (ch == '{')
                {
                    if (flag)
                    {
                        return false;
                    }
                    flag = true;
                }
                else if (ch == '}')
                {
                    if (!flag)
                    {
                        return false;
                    }
                    flag = false;
                    num++;
                }
            }
            return (!flag && (num > 1));
        }

        private void Parse(string format)
        {
            string str = string.Empty;
            string str2 = string.Empty;
            int braceIndex = this.GetBraceIndex(format, '{');
            int num2 = this.GetMatchingBraceIndex(format, '}', braceIndex);
            if (braceIndex == -1)
            {
                if (num2 != -1)
                {
                    return;
                }
            }
            else
            {
                if (num2 < braceIndex)
                {
                    return;
                }
                str = this.CleanupPrefix(format.Substring(0, braceIndex));
                str2 = this.CleanupPostfix(format.Substring(num2 + 1));
                format = format.Substring(braceIndex + 1, (num2 - braceIndex) - 1);
                int index = format.IndexOf(":");
                format = (index != -1) ? format.Remove(0, index + 1) : string.Empty;
            }
            this.Prefix = str;
            this.Postfix = str2;
            this.FormatString = format;
        }

        private List<string> SplitByBraces(string format)
        {
            List<string> list = new List<string>();
            StringBuilder builder = new StringBuilder();
            bool flag = false;
            bool flag2 = true;
            for (int i = 0; i < format.Length; i++)
            {
                char ch = format[i];
                if (flag)
                {
                    flag = false;
                }
                else if (ch == '\\')
                {
                    flag = true;
                }
                else if (ch == '{')
                {
                    if (!flag2 && (builder.Length > 0))
                    {
                        list.Add(builder.ToString());
                        builder.Clear();
                    }
                    flag2 = false;
                }
                builder.Append(ch);
            }
            if (builder.Length > 0)
            {
                list.Add(builder.ToString());
            }
            return list;
        }

        public string Prefix { get; private set; }

        public string Postfix { get; private set; }

        public string FormatString { get; private set; }

        public string OriginalFormat { get; private set; }
    }
}

