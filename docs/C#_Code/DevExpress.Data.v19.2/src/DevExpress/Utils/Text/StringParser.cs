namespace DevExpress.Utils.Text
{
    using DevExpress.Utils;
    using DevExpress.Utils.Text.Internal;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;

    public class StringParser
    {
        private static ColorConverter converter;

        static StringParser()
        {
            DpiScaleFactor = 1f;
        }

        private static bool ApplyCommand(StringBlock block, StringCommand command)
        {
            if (IsImageCommand(command))
            {
                block.Type = StringBlockType.Image;
                block.Text = GetParameterOption(command);
                return true;
            }
            if (IsHrefCommand(command) && command.IsStart)
            {
                block.Type = StringBlockType.Link;
                block.Link = GetParameterOption(command);
            }
            return false;
        }

        private static void ApplyCommandSettings(StringFontSettings current, StringFontSettings defaultSettings, HyperlinkSettings hyperlinkSettings, StringCommand command)
        {
            if (IsFontStyleCommand(command))
            {
                FontStyle fontStyleCommandOptions = GetFontStyleCommandOptions(command);
                if (command.IsStart)
                {
                    if (command.Code == HtmlStringCommand.R)
                    {
                        current.PushStyle(fontStyleCommandOptions);
                    }
                    else
                    {
                        current.AddStyle(fontStyleCommandOptions);
                    }
                }
                else if (command.Code == HtmlStringCommand.R)
                {
                    current.PopStyle();
                }
                else
                {
                    current.RemoveStyle(fontStyleCommandOptions);
                }
            }
            else
            {
                if (IsHrefCommand(command))
                {
                    if (!command.IsStart)
                    {
                        current.SetColor(Color.Empty);
                        current.SetStyle(current.Style & ~FontStyle.Underline);
                    }
                    else
                    {
                        current.SetColor(hyperlinkSettings.Color);
                        if (hyperlinkSettings.Underline)
                        {
                            current.ModifyStyle(FontStyle.Underline);
                        }
                    }
                }
                if (IsColorCommand(command))
                {
                    current.SetColor(command.IsEnd ? Color.Empty : GetFontColorCommandOptions(command, current));
                }
                else if (IsBackColorCommand(command))
                {
                    current.SetBackColor(command.IsEnd ? Color.Empty : GetFontColorCommandOptions(command, current));
                }
                else if (!IsFontCommand(command))
                {
                    if (IsFontSizeCommand(command))
                    {
                        if (!IsScriptCommand(command))
                        {
                            current.SetSize(command.IsEnd ? defaultSettings.Size : GetFontSizeCommandOptions(command, current.Size));
                        }
                        else
                        {
                            float fontSizeCommandOptions = GetFontSizeCommandOptions(command, current.Size);
                            if (command.IsStart)
                            {
                                current.AddModifier((command.Code == HtmlStringCommand.Sub) ? StringBlockTextModifier.SubScript : StringBlockTextModifier.SuperScript, fontSizeCommandOptions);
                            }
                            else
                            {
                                current.RemoveLastModifier((command.Code == HtmlStringCommand.Sub) ? StringBlockTextModifier.SubScript : StringBlockTextModifier.SuperScript);
                            }
                            current.SetSize(fontSizeCommandOptions);
                        }
                    }
                }
                else if (!command.IsStart)
                {
                    current.SetSize(defaultSettings.Size);
                    current.SetColor(defaultSettings.Color);
                    current.FontFamily = null;
                }
                else
                {
                    string size = string.Empty;
                    string str2 = string.Empty;
                    string font = string.Empty;
                    if (ExtractFontParams(command.Command, out font, out size, out str2))
                    {
                        float fontSizeCoreWithoutParamName = current.Size;
                        Color colorFromString = current.Color;
                        if (!string.IsNullOrEmpty(size))
                        {
                            fontSizeCoreWithoutParamName = GetFontSizeCoreWithoutParamName(size, current.Size);
                            current.SetSize(fontSizeCoreWithoutParamName);
                        }
                        if (!string.IsNullOrEmpty(str2))
                        {
                            colorFromString = GetColorFromString(str2);
                            current.SetColor(colorFromString);
                        }
                        current.FontFamily = font;
                    }
                }
            }
        }

        private static bool ApplyMatchCore(List<StringBlock> blocks, int matchBlockStart, int matchStartPosition, int matchCount)
        {
            for (int i = matchBlockStart; i < blocks.Count; i++)
            {
                int num2;
                StringBlock block = blocks[i];
                if (IsAllowMatch(block, out num2) && !string.IsNullOrEmpty(block.Text))
                {
                    int matchLength = Math.Min(block.Text.Length - matchStartPosition, matchCount);
                    block.SetMatchInfo(matchStartPosition, matchLength);
                    matchCount -= matchLength;
                    matchStartPosition = 0;
                    if (matchCount < 1)
                    {
                        break;
                    }
                }
            }
            return true;
        }

        private static bool CheckValidCommand(string cmd, out string cmdUpdated, out HtmlStringCommand decodedCommand)
        {
            cmdUpdated = cmd.ToLowerInvariant();
            decodedCommand = HtmlStringCommand.Unknown;
            string s = cmdUpdated;
            uint num = <PrivateImplementationDetails>.ComputeStringHash(s);
            if (num <= 0xe70c2de5)
            {
                if (num <= 0xca4e1cbf)
                {
                    if (num != 0x4f2bc4b5)
                    {
                        if ((num == 0xca4e1cbf) && (s == "sup"))
                        {
                            decodedCommand = HtmlStringCommand.Sup;
                            return true;
                        }
                    }
                    else if (s == "br")
                    {
                        decodedCommand = HtmlStringCommand.Br;
                        return true;
                    }
                }
                else if (num != 0xdc4e3915)
                {
                    if ((num == 0xe70c2de5) && (s == "b"))
                    {
                        decodedCommand = HtmlStringCommand.B;
                        return true;
                    }
                }
                else if (s == "sub")
                {
                    decodedCommand = HtmlStringCommand.Sub;
                    return true;
                }
            }
            else if (num <= 0xf00c3c10)
            {
                if (num != 0xec0c35c4)
                {
                    if ((num == 0xf00c3c10) && (s == "u"))
                    {
                        decodedCommand = HtmlStringCommand.U;
                        return true;
                    }
                }
                else if (s == "i")
                {
                    decodedCommand = HtmlStringCommand.I;
                    return true;
                }
            }
            else if (num == 0xf60c4582)
            {
                if (s == "s")
                {
                    decodedCommand = HtmlStringCommand.S;
                    return true;
                }
            }
            else if (num != 0xf70c4715)
            {
                if ((num == 0xf83516fa) && (s == "nbsp"))
                {
                    decodedCommand = HtmlStringCommand.Nbsp;
                    return true;
                }
            }
            else if (s == "r")
            {
                decodedCommand = HtmlStringCommand.R;
                return true;
            }
            if (cmdUpdated.StartsWith("font"))
            {
                decodedCommand = HtmlStringCommand.Font;
                cmdUpdated = cmd;
                return true;
            }
            if (cmdUpdated.StartsWith("href"))
            {
                int index = cmd.IndexOf('=');
                if (index != -1)
                {
                    cmdUpdated = cmd.Substring(0, index).ToLowerInvariant() + cmd.Substring(index, cmd.Length - index);
                }
                decodedCommand = HtmlStringCommand.Href;
                return true;
            }
            if (cmdUpdated.StartsWith("color"))
            {
                decodedCommand = HtmlStringCommand.ForeColor;
                return true;
            }
            if (cmdUpdated.StartsWith("backcolor"))
            {
                decodedCommand = HtmlStringCommand.BackColor;
                return true;
            }
            if (cmdUpdated.StartsWith("size"))
            {
                decodedCommand = HtmlStringCommand.Size;
                return true;
            }
            if (!cmdUpdated.StartsWith("image"))
            {
                return false;
            }
            cmdUpdated = "image" + cmd.Substring(5);
            decodedCommand = HtmlStringCommand.Image;
            return true;
        }

        public static StringBlock CreateBlock()
        {
            StringBlock block1 = new StringBlock();
            block1.DpiScaleFactor = DpiScaleFactor;
            return block1;
        }

        private static bool ExtractFontParams(string command, out string font, out string size, out string color)
        {
            size = string.Empty;
            color = string.Empty;
            font = string.Empty;
            int startIndex = 0;
            string param = null;
            string str2 = FindParamValue(command, ref startIndex, out param);
            if (str2 == null)
            {
                return false;
            }
            while (str2 != null)
            {
                if (param == "font")
                {
                    font = str2;
                }
                else if (param == "color")
                {
                    color = str2;
                }
                else if (param == "size")
                {
                    size = str2;
                }
                str2 = FindParamValue(command, ref startIndex, out param);
            }
            return true;
        }

        private static int FindCommandEnd(string text, int pos, int len, char chCommandEnd)
        {
            int num = pos + 1;
            bool flag = false;
            while (true)
            {
                if (num < len)
                {
                    if (text[num++] != chCommandEnd)
                    {
                        continue;
                    }
                    flag = true;
                }
                return (flag ? num : -1);
            }
        }

        private static int FindNextNonSpaceSymbol(string text, int startIndex)
        {
            for (int i = startIndex; i < text.Length; i++)
            {
                if (!char.IsWhiteSpace(text[i]))
                {
                    return i;
                }
            }
            return -1;
        }

        private static int FindNextSpaceSymbol(string text, int startIndex)
        {
            bool flag = false;
            for (int i = startIndex; i < text.Length; i++)
            {
                if (text[i] != '\'')
                {
                    if (char.IsWhiteSpace(text[i]) && !flag)
                    {
                        return (i - 1);
                    }
                }
                else
                {
                    if (flag)
                    {
                        return i;
                    }
                    flag = true;
                }
            }
            return (text.Length - 1);
        }

        private static string FindParamValue(string text, ref int startIndex, out string param)
        {
            param = null;
            int num = FindNextNonSpaceSymbol(text, startIndex);
            if (num == -1)
            {
                return null;
            }
            string paramName = null;
            int num2 = ReadParamName(text, num, out paramName);
            if (num2 == -1)
            {
                return null;
            }
            int num3 = FindNextNonSpaceSymbol(text, num2 + 1);
            if ((num3 == -1) || (text[num3] != '='))
            {
                return null;
            }
            int num4 = FindNextNonSpaceSymbol(text, num3 + 1);
            int num5 = FindNextSpaceSymbol(text, num4);
            if (num5 < num4)
            {
                return string.Empty;
            }
            startIndex = num5 + 1;
            if (text[num4] == '\'')
            {
                num4++;
                num5--;
            }
            if (num5 < num4)
            {
                return string.Empty;
            }
            param = paramName;
            return text.Substring(num4, (num5 - num4) + 1);
        }

        private static Color GetColorFromString(string str)
        {
            Color color2;
            Color empty = Color.Empty;
            try
            {
                color2 = (Color) Converter.ConvertFromInvariantString(str);
            }
            catch
            {
                return empty;
            }
            return color2;
        }

        private static Color GetFontColorCommandOptions(StringCommand command, StringFontSettings current)
        {
            Color color = current.Color;
            if (command.Command.StartsWith("color="))
            {
                Color color2 = GetColorFromString(command.Command.Substring(6));
                return (!(color2 == Color.Empty) ? color2 : current.Color);
            }
            if (!command.Command.StartsWith("backcolor="))
            {
                return color;
            }
            Color colorFromString = GetColorFromString(command.Command.Substring(10));
            return (!(colorFromString == Color.Empty) ? colorFromString : current.BackColor);
        }

        private static float GetFontSizeCommandOptions(StringCommand command, float current)
        {
            string commandText = command.Command;
            if ((command.Code == HtmlStringCommand.Sub) || (command.Code == HtmlStringCommand.Sup))
            {
                commandText = !command.IsStart ? "size=+2" : "size=-2";
            }
            return ((!commandText.StartsWith("size=") || (commandText.Length <= 5)) ? current : GetFontSizeCore(commandText, current));
        }

        private static float GetFontSizeCore(string commandText, float current)
        {
            float num;
            int startIndex = 5;
            if ((commandText[5] == '+') || (commandText[5] == '-'))
            {
                startIndex = 6;
            }
            return ((!float.TryParse(commandText.Substring(startIndex), NumberStyles.Any, (IFormatProvider) NumberFormatInfo.InvariantInfo, out num) || (num <= 0f)) ? current : ((startIndex == 6) ? (current + ((commandText[5] == '-') ? -num : num)) : num));
        }

        private static float GetFontSizeCoreWithoutParamName(string commandText, float current)
        {
            float num;
            int startIndex = 0;
            if ((commandText[0] == '+') || (commandText[0] == '-'))
            {
                startIndex = 1;
            }
            return ((!float.TryParse(commandText.Substring(startIndex), NumberStyles.Any, (IFormatProvider) NumberFormatInfo.InvariantInfo, out num) || (num <= 0f)) ? current : ((startIndex == 1) ? (current + ((commandText[0] == '-') ? -num : num)) : num));
        }

        private static FontStyle GetFontStyleCommandOptions(StringCommand command)
        {
            FontStyle regular = FontStyle.Regular;
            switch (command.Code)
            {
                case HtmlStringCommand.B:
                    regular |= FontStyle.Bold;
                    break;

                case HtmlStringCommand.I:
                    regular |= FontStyle.Italic;
                    break;

                case HtmlStringCommand.U:
                    regular |= FontStyle.Underline;
                    break;

                case HtmlStringCommand.S:
                    regular |= FontStyle.Strikeout;
                    break;

                case HtmlStringCommand.R:
                    return regular;

                default:
                    break;
            }
            return regular;
        }

        private static string GetParameterOption(StringCommand command)
        {
            int index = command.Command.IndexOf('=');
            return ((index <= -1) ? string.Empty : command.Command.Substring(index + 1));
        }

        private static bool IsAllowMatch(StringBlock block, out int minStartPosition)
        {
            minStartPosition = 0;
            if (block.MatchIndexes != null)
            {
                minStartPosition = block.MatchIndexes.Last<int>() + block.MatchLength.Last<int>();
            }
            return ((block.Type == StringBlockType.Link) || (block.Type == StringBlockType.Text));
        }

        private static bool IsBackColorCommand(StringCommand command) => 
            command.Code == HtmlStringCommand.BackColor;

        private static bool IsColorCommand(StringCommand command) => 
            command.Code == HtmlStringCommand.ForeColor;

        private static bool IsFontCommand(StringCommand command) => 
            command.Code == HtmlStringCommand.Font;

        private static bool IsFontSizeCommand(StringCommand command) => 
            (command.Code == HtmlStringCommand.Size) || ((command.Code == HtmlStringCommand.Sub) || (command.Code == HtmlStringCommand.Sup));

        private static bool IsFontStyleCommand(StringCommand command) => 
            (command.Code >= HtmlStringCommand.B) && (command.Code <= HtmlStringCommand.R);

        private static bool IsHrefCommand(StringCommand command) => 
            command.Code == HtmlStringCommand.Href;

        private static bool IsImageCommand(StringCommand command) => 
            command.Code == HtmlStringCommand.Image;

        private static bool IsScriptCommand(StringCommand command) => 
            (command.Code == HtmlStringCommand.Sub) || (command.Code == HtmlStringCommand.Sup);

        private static bool IsSeparator(char ch) => 
            (ch != '\\') && ((ch != '/') && (ch.GetUnicodeCategory() == UnicodeCategory.OtherPunctuation));

        public static bool Match(List<StringBlock> blocks, string text, bool multiMatch = false, bool caseInsensitive = true)
        {
            if ((blocks.Count == 0) || string.IsNullOrEmpty(text))
            {
                return false;
            }
            if (!multiMatch)
            {
                foreach (StringBlock block in blocks)
                {
                    block.ResetMatchInfo();
                }
            }
            bool matchFound = false;
            int matchBlockStart = -1;
            int matchStartPosition = -1;
            int matchCount = 0;
            int num4 = 0;
            num4 = MatchCore(blocks, text, ref matchFound, ref matchBlockStart, ref matchStartPosition, ref matchCount, caseInsensitive);
            return (matchFound && ((matchCount == text.Length) && ApplyMatchCore(blocks, matchBlockStart, matchStartPosition, matchCount)));
        }

        private static int MatchCore(List<StringBlock> blocks, string text, ref bool matchFound, ref int matchBlockStart, ref int matchStartPosition, ref int matchCount, bool caseInsensitive)
        {
            string strB = text;
            StringComparison comparisonType = caseInsensitive ? StringComparison.CurrentCultureIgnoreCase : StringComparison.CurrentCulture;
            int num = 0;
            while (true)
            {
                int num2;
                if (num >= blocks.Count)
                {
                    break;
                }
                StringBlock block = blocks[num];
                if (IsAllowMatch(block, out num2))
                {
                    string str2 = block.Text;
                    if (!string.IsNullOrEmpty(str2))
                    {
                        int num3 = -1;
                        if (matchFound && (num2 > 0))
                        {
                            matchFound = false;
                        }
                        if (!matchFound)
                        {
                            num3 = Math.Max(matchStartPosition, num2 - 1);
                            matchStartPosition = num2 - 1;
                        }
                        while (true)
                        {
                            int indexA = ((num3 + 1) >= str2.Length) ? -1 : str2.IndexOf(strB.Substring(0, 1), num3 + 1, comparisonType);
                            if (indexA < 0)
                            {
                                if (matchFound)
                                {
                                    num = matchBlockStart + 1;
                                    matchStartPosition = -1;
                                    matchFound = false;
                                    strB = text;
                                }
                            }
                            else
                            {
                                int num5 = str2.Length - indexA;
                                if (string.Compare(str2, indexA, strB, 0, Math.Min(num5, strB.Length), comparisonType) != 0)
                                {
                                    num3 = indexA;
                                    continue;
                                }
                                if (matchFound)
                                {
                                    matchCount += Math.Min(num5, strB.Length);
                                }
                                else
                                {
                                    matchFound = true;
                                    matchStartPosition = indexA;
                                    matchCount = Math.Min(num5, strB.Length);
                                    matchBlockStart = num;
                                }
                                strB = strB.Substring(Math.Min(num5, strB.Length));
                            }
                            if (!matchFound || (matchCount != text.Length))
                            {
                                break;
                            }
                            return num;
                        }
                    }
                }
                else if (matchFound)
                {
                    num = matchBlockStart;
                    matchFound = false;
                    matchStartPosition = -1;
                    strB = text;
                }
                num++;
            }
            return num;
        }

        private static bool MustBreak(StringCommand command) => 
            (command.Code == HtmlStringCommand.Br) || (command.Code == HtmlStringCommand.Image);

        public static List<StringBlock> Parse(float fontSize, string text, bool allowNewLineSymbols) => 
            Parse(Color.Empty, fontSize, text, allowNewLineSymbols);

        public static List<StringBlock> Parse(Color foreColor, float fontSize, string text, bool allowNewLineSymbols) => 
            Parse(Color.Empty, Color.Empty, fontSize, text, allowNewLineSymbols);

        public static List<StringBlock> Parse(Color foreColor, Color backColor, float fontSize, string text, bool allowNewLineSymbols)
        {
            HyperlinkSettings hyperlinkSettings = new HyperlinkSettings();
            hyperlinkSettings.Color = Color.Blue;
            hyperlinkSettings.Underline = true;
            return Parse(foreColor, backColor, hyperlinkSettings, fontSize, FontStyle.Regular, text, allowNewLineSymbols);
        }

        public static List<StringBlock> Parse(Color foreColor, Color backColor, HyperlinkSettings hyperlinkSettings, float fontSize, string text, bool allowNewLineSymbols) => 
            Parse(foreColor, backColor, hyperlinkSettings, fontSize, FontStyle.Regular, text, allowNewLineSymbols);

        public static List<StringBlock> Parse(Color foreColor, Color backColor, HyperlinkSettings hyperlinkSettings, float fontSize, FontStyle style, string text, bool allowNewLineSymbols)
        {
            text ??= string.Empty;
            List<StringBlock> res = new List<StringBlock>();
            StringBlock block = CreateBlock();
            block.FontSettings.SetColor(foreColor);
            block.FontSettings.SetBackColor(backColor);
            block.FontSettings.SetSize(fontSize);
            if (KeepFontStyle)
            {
                block.FontSettings.SetStyle(style);
            }
            block = ParseCore(text, res, block, block.FontSettings.Clone(), hyperlinkSettings);
            if (!block.IsEmpty)
            {
                res.Add(block);
            }
            if (allowNewLineSymbols)
            {
                UpdateLineNumbers(res);
            }
            return res;
        }

        public static StringCommand ParseCommand(ref int n, string text)
        {
            StringCommand command = new StringCommand();
            int pos = n + 1;
            if (string.IsNullOrEmpty(text) || ((text[n] != '<') && (text[n] != '&')))
            {
                if (IsSeparator(text[n]))
                {
                    command.Command = text[n].ToString();
                }
                return command;
            }
            int length = text.Length;
            if (length >= 3)
            {
                char ch = '<';
                char chCommandEnd = '>';
                if (text[n] == '&')
                {
                    ch = '\0';
                    chCommandEnd = ';';
                }
                if (text[pos] == ch)
                {
                    n++;
                    return command;
                }
                if (text[pos] != '/')
                {
                    command.SetStart();
                }
                else
                {
                    pos++;
                    command.SetEnd();
                }
                int num3 = FindCommandEnd(text, pos, length, chCommandEnd);
                if (num3 != -1)
                {
                    string cmd = text.Substring(pos, (num3 - pos) - 1);
                    if (cmd.Length < 1)
                    {
                        return command;
                    }
                    HtmlStringCommand unknown = HtmlStringCommand.Unknown;
                    if (!CheckValidCommand(cmd, out cmd, out unknown))
                    {
                        command.Code = unknown;
                        return command;
                    }
                    command.Code = unknown;
                    command.Command = cmd;
                    n = num3;
                }
            }
            return command;
        }

        private static StringBlock ParseCore(string text, List<StringBlock> res, StringBlock block, StringFontSettings defaultSettings, HyperlinkSettings hyperlinkSettings)
        {
            int length = text.Length;
            StringBuilder builder = new StringBuilder();
            StringCommand command = null;
            int n = 0;
            while (true)
            {
                while (true)
                {
                    char ch;
                    if (n >= length)
                    {
                        if (builder.Length > 0)
                        {
                            block.Text = builder.ToString();
                        }
                        return block;
                    }
                    if (text[n] == '\n')
                    {
                        if (((n > 0) && (text[n - 1] == '\r')) || ((n < (length - 1)) && (text[n + 1] == '\r')))
                        {
                            break;
                        }
                        ch = '\r';
                    }
                    bool flag = ((command != null) && command.IsStart) && IsHrefCommand(command);
                    if (((ch == '<') || ((ch == '&') || (IsSeparator(ch) && !flag))) && (n < (length - 1)))
                    {
                        StringCommand command2 = ParseCommand(ref n, text);
                        if (command2.IsValid)
                        {
                            if (command2.IsSeparator && (builder.Length > 0))
                            {
                                builder.Append(ch);
                            }
                            else
                            {
                                n--;
                                if (command2.Code == HtmlStringCommand.Br)
                                {
                                    builder.Append("\r");
                                }
                                if (command2.Code == HtmlStringCommand.Nbsp)
                                {
                                    builder.Append("\x00a0");
                                }
                                else
                                {
                                    if (command2.IsSeparator && (builder.Length == 0))
                                    {
                                        n++;
                                    }
                                    StringFontSettings current = block.FontSettings.Clone();
                                    ApplyCommandSettings(current, defaultSettings, hyperlinkSettings, command2);
                                    if (!MustBreak(command2))
                                    {
                                        if (!command2.IsSeparator && block.FontSettings.IsEquals(current))
                                        {
                                            break;
                                        }
                                        if (builder.Length == 0)
                                        {
                                            if ((IsHrefCommand(command2) || IsScriptCommand(command2)) && command2.IsStart)
                                            {
                                                ApplyCommand(block, command2);
                                            }
                                            block.FontSettings.Assign(current);
                                            if (!command2.IsSeparator)
                                            {
                                                command = command2;
                                                break;
                                            }
                                        }
                                    }
                                    block.Text = (!command2.IsSeparator || (builder.Length != 0)) ? builder.ToString() : command2.Command;
                                    if (((block.Type != StringBlockType.Text) && (block.Type != StringBlockType.Link)) || !string.IsNullOrEmpty(block.Text))
                                    {
                                        res.Add(block);
                                    }
                                    block = CreateBlock();
                                    block.FontSettings.Assign(current);
                                    if (ApplyCommand(block, command2))
                                    {
                                        res.Add(block);
                                        block = CreateBlock();
                                        block.FontSettings.Assign(current);
                                    }
                                    command = command2;
                                    builder = new StringBuilder();
                                }
                            }
                            break;
                        }
                    }
                    builder.Append(ch);
                    break;
                }
                n++;
            }
        }

        private static int ReadParamName(string text, int startIndex, out string paramName)
        {
            paramName = null;
            for (int i = startIndex; i < text.Length; i++)
            {
                if (!char.IsLetter(text[i]))
                {
                    paramName = text.Substring(startIndex, i - startIndex);
                    return (i - 1);
                }
            }
            return -1;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static void RegisterColorConverter(ColorConverter colorConverter)
        {
            converter = colorConverter;
        }

        private static void UpdateLineNumbers(List<StringBlock> stringBlocks)
        {
            int num = 0;
            for (int i = 0; i < stringBlocks.Count; i++)
            {
                stringBlocks[i].LineNumber = num;
                stringBlocks[i].Text = stringBlocks[i].Text.Replace("\r\n", "\r");
                stringBlocks[i].Text = stringBlocks[i].Text.Replace('\n', '\r');
                char[] separator = new char[] { '\r' };
                string[] strArray = stringBlocks[i].Text.Split(separator);
                if (strArray.Length > 1)
                {
                    stringBlocks[i].Text = strArray[0];
                    for (int n = 1; n < strArray.Length; n++)
                    {
                        num++;
                        StringBlock item = CreateBlock();
                        item.SetBlock(stringBlocks[i]);
                        item.Text = strArray[n];
                        item.LineNumber = num;
                        i++;
                        stringBlocks.Insert(i, item);
                    }
                }
            }
            for (int j = 1; j < stringBlocks.Count; j++)
            {
                if ((stringBlocks[j].Text == string.Empty) && (stringBlocks[j].LineNumber == stringBlocks[j - 1].LineNumber))
                {
                    stringBlocks.RemoveAt(j--);
                }
            }
            for (int k = 0; k < (stringBlocks.Count - 1); k++)
            {
                if ((stringBlocks[k].Text == string.Empty) && (stringBlocks[k].LineNumber == stringBlocks[k + 1].LineNumber))
                {
                    stringBlocks.RemoveAt(k--);
                }
            }
            for (int m = 0; m < stringBlocks.Count; m++)
            {
                if (stringBlocks[m].Text == string.Empty)
                {
                    stringBlocks[m].Text = "\r";
                }
            }
        }

        public static bool KeepFontStyle { get; set; }

        private static ColorConverter Converter
        {
            get
            {
                converter ??= new ColorConverter();
                return converter;
            }
        }

        public static float DpiScaleFactor { get; internal set; }
    }
}

