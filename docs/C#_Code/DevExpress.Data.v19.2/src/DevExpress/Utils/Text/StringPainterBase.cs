namespace DevExpress.Utils.Text
{
    using DevExpress.Utils;
    using DevExpress.Utils.Text.Internal;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;

    public abstract class StringPainterBase
    {
        public static float AscentHeightMultiplier = 0.84f;

        protected StringPainterBase()
        {
        }

        private void AddEllipsisCharacter(StringInfoBase info, Rectangle bounds, TextProcessInfoBase te, int index)
        {
            StringBlock local1 = info.Blocks[index];
            local1.Text = local1.Text + "...";
            Rectangle rectangle = info.BlocksBounds[index];
            rectangle.Width = this.GetStringWidth(te, info, info.Blocks[index], info.Blocks[index].Text);
            if (rectangle.Right > bounds.Right)
            {
                rectangle.Width = bounds.Right - rectangle.X;
            }
            info.BlocksBounds[index] = rectangle;
        }

        public List<Rectangle> CalcBlocksInnerBounds(StringInfoBase stringInfo)
        {
            List<Rectangle> list = new List<Rectangle>();
            for (int i = 0; i < stringInfo.Blocks.Count; i++)
            {
                list.Add(this.CalcTextInnerBounds(stringInfo.Blocks[i], stringInfo.BlocksBounds[i]));
            }
            return list;
        }

        private int CalcConnectedWidth(TextProcessInfoBase te, List<StringBlock> strings, int index) => 
            this.CalcSeparatorsWidth(te, strings, index) + this.CalcNbspTextWidth(te, strings, index);

        private int CalcNbspTextWidth(TextProcessInfoBase te, List<StringBlock> strings, int index)
        {
            int num = 0;
            if ((strings != null) && (index != (strings.Count - 1)))
            {
                StringBlock block = strings[index + 1];
                if ((block.Text.Length > 0) && (block.Text[0] == '\x00a0'))
                {
                    num = this.CalcStringWidthCore(te, block);
                    index++;
                    if ((block.Text.Length == 1) && (strings.Count > (index + 1)))
                    {
                        num += this.CalcStringWidthCore(te, strings[index + 1]);
                    }
                }
            }
            return num;
        }

        private int CalcSeparatorsWidth(TextProcessInfoBase te, List<StringBlock> strings, int index)
        {
            if (strings == null)
            {
                return 0;
            }
            int num = 0;
            if (te.Block.Text[te.Block.Text.Length - 1] == '\r')
            {
                return 0;
            }
            for (int i = index + 1; (i < strings.Count) && this.IsSeparator(strings[i]); i++)
            {
                StringBlock block = te.Block;
                te.Block = strings[i];
                num += this.GetStringWidth(te, strings[i].Text);
                te.Block = block;
            }
            return num;
        }

        protected abstract Size CalcStringBlockSize(TextProcessInfoBase te, StringBlock block, string s, StringFormat format);
        private int CalcStringWidthCore(TextProcessInfoBase te, StringBlock block)
        {
            int stringWidth = 0;
            StringBlock block2 = te.Block;
            te.Block = block;
            stringWidth = this.GetStringWidth(te, block.Text);
            te.Block = block2;
            return stringWidth;
        }

        public unsafe Rectangle CalcTextInnerBounds(StringBlock block, Rectangle bounds)
        {
            Rectangle rectangle = bounds;
            rectangle.Height = block.FontAscentHeight - block.FontInternalLeading;
            Rectangle* rectanglePtr1 = &rectangle;
            rectanglePtr1.Y += block.FontInternalLeading;
            return rectangle;
        }

        protected abstract Size CalcTextSizeInt(StringInfoBase info, StringCalculateArgsBase e);
        public virtual StringInfoBase Calculate(StringCalculateArgsBase e)
        {
            StringInfoBase info = this.CreateAndInitializeStringInfo(e);
            if (e.AllowSimpleString && this.IsSimpleStringCore(e.Text))
            {
                this.SetupSimpleString(info, e);
                return info;
            }
            DevExpress.Utils.Text.StringParser.DpiScaleFactor = this.GetDpiScaleFactor(e);
            DevExpress.Utils.Text.StringParser.DpiScaleFactor = 1f;
            return this.CalculateCore(e, info, this.Parse(info, e.Text));
        }

        protected StringInfoBase CalculateCore(StringCalculateArgsBase e, StringInfoBase info, List<StringBlock> strings)
        {
            Rectangle bounds = e.Bounds;
            this.UpdateFont(e, strings, info.Font);
            List<StringBlock> blocks = new List<StringBlock>();
            info.Assign(blocks, new List<Rectangle>(), e.Text);
            info.OriginalBounds = bounds;
            if (strings.Count != 0)
            {
                TextProcessInfoBase te = this.CreateTextProcessInfo(e);
                te.RoundTextHeight = e.RoundTextHeight;
                te.AllowMultiLine = info.WordWrap == WordWrap.Wrap;
                te.CurrentPosition = bounds.Location;
                te.Info = info;
                te.Bounds = bounds;
                te.Context = e.Context;
                for (int i = 0; i < strings.Count; i++)
                {
                    StringBlock block = strings[i];
                    if (block.FontSettings.HasModifiers)
                    {
                        info.HasScripts = true;
                    }
                    if (block.Type == StringBlockType.Image)
                    {
                        info.HasImages = true;
                    }
                    te.LineHeight ??= this.GetBlockHeight(info.Context, block, te.RoundTextHeight);
                    te.Block = block;
                    this.ProcessBlock(e, te, strings, i);
                }
                this.UpdateBaseLine(bounds, info, te);
                if (info.HasScripts)
                {
                    this.UpdateScripts(e, bounds, info, te);
                }
                this.UpdateMaximumBounds(info, bounds, te);
                info.SetBounds(Rectangle.FromLTRB(bounds.X, bounds.Y, te.End.X, te.End.Y));
                this.UpdateBoundsByAppearance(info, bounds);
            }
            return info;
        }

        private unsafe Rectangle CalculateOffset(StringCalculateArgsBase e, Rectangle anchorBounds, Rectangle scriptBounds, StringBlock scriptBlock)
        {
            Rectangle rectangle = new Rectangle(scriptBounds.X, anchorBounds.Y, scriptBounds.Width, anchorBounds.Height);
            List<StringBlockTextModifierInfo> modifiers = scriptBlock.FontSettings.Modifiers;
            StringBlockTextModifierInfo info = modifiers.Last<StringBlockTextModifierInfo>();
            foreach (StringBlockTextModifierInfo info2 in modifiers)
            {
                if (info2.Height == 0)
                {
                    this.SetModifierFontInfo(e, scriptBlock.Font, info2);
                }
                rectangle.Y = (info2.Modifier != StringBlockTextModifier.SubScript) ? ((rectangle.Y + (rectangle.Height / 2)) - info2.InnerHeight) : (rectangle.Bottom - (rectangle.Height / 2));
                rectangle.Height = info2.InnerHeight;
            }
            Rectangle* rectanglePtr1 = &rectangle;
            rectanglePtr1.Y -= info.InternalLeading;
            rectangle.Height = info.Height;
            return rectangle;
        }

        internal static int CalcXByAlignment(Rectangle totalBounds, int textWidth, HorzAlignment align)
        {
            int num = totalBounds.Width - textWidth;
            int x = totalBounds.X;
            if (num >= 1)
            {
                if (align == HorzAlignment.Center)
                {
                    x += (num / 2) + (num % 2);
                }
                if (align == HorzAlignment.Far)
                {
                    x += num;
                }
            }
            return x;
        }

        private int CalcYByAppearance(VertAlignment align, Rectangle bounds, int height, bool reverseHeight)
        {
            int num = reverseHeight ? (height - bounds.Height) : (bounds.Height - height);
            int y = bounds.Y;
            if (num >= 1)
            {
                if ((align == VertAlignment.Center) || (align == VertAlignment.Default))
                {
                    y += (num / 2) + (!reverseHeight ? (num % 2) : 0);
                }
                if (align == VertAlignment.Bottom)
                {
                    y += num;
                }
            }
            return y;
        }

        private HorzAlignment CheckRTLHAlignment(StringInfoBase info)
        {
            HorzAlignment hAlignment = info.HAlignment;
            if (info.RightToLeft)
            {
                switch (hAlignment)
                {
                    case HorzAlignment.Default:
                    case HorzAlignment.Near:
                        return HorzAlignment.Far;

                    case HorzAlignment.Far:
                        return HorzAlignment.Near;

                    default:
                        break;
                }
            }
            return hAlignment;
        }

        protected abstract StringInfoBase CreateAndInitializeStringInfo(StringCalculateArgsBase e);
        private Rectangle CreateNewBlock(StringCalculateArgsBase e, TextProcessInfoBase te, int textWidth, string words)
        {
            StringBlock block1 = new StringBlock();
            block1.DpiScaleFactor = this.GetDpiScaleFactor(e);
            StringBlock item = block1;
            item.Text = words;
            item.SetBlock(te.Block);
            item.LineNumber = te.LineNumber;
            item.Link = te.Block.Link;
            te.Info.Blocks.Add(item);
            Rectangle rectangle = new Rectangle(te.CurrentPosition, new Size(textWidth, this.GetBlockHeight(te.Context, te.Block, te.RoundTextHeight)));
            te.Info.BlocksBounds.Add(rectangle);
            this.UpdateBlockAscentHeight(te, item, rectangle);
            te.CurrentX += textWidth;
            te.End.Y = Math.Max(te.End.Y, te.CurrentY + te.LineHeight);
            te.End.X = Math.Max(te.End.X, te.CurrentX);
            return rectangle;
        }

        protected virtual TextProcessInfoBase CreateTextProcessInfo(StringCalculateArgsBase e) => 
            new TextProcessInfoBase();

        private int FindNextAnchorIndex(StringInfoBase info, int lineIndex, int startIndex)
        {
            for (int i = startIndex; (i < info.Blocks.Count) && (info.Blocks[i].LineNumber == lineIndex); i++)
            {
                if (!info.Blocks[i].FontSettings.HasModifiers)
                {
                    return i;
                }
            }
            return -1;
        }

        protected int GetBlockHeight(object context, StringBlock block, bool roundTextHeight)
        {
            switch (block.Type)
            {
                case StringBlockType.Text:
                case StringBlockType.Link:
                {
                    int fontHeight = block.FontHeight;
                    if (roundTextHeight && ((fontHeight % 2) == 1))
                    {
                        fontHeight++;
                    }
                    return fontHeight;
                }
                case StringBlockType.Image:
                    return this.GetBlockSize(context, block).Height;
            }
            return 0;
        }

        protected Size GetBlockSize(object context, StringBlock block)
        {
            if (block.Type != StringBlockType.Image)
            {
                throw new ArgumentException("Unsupported block type", "block.Type");
            }
            return this.GetImageBlockSize(context, block);
        }

        protected virtual float GetDpiScaleFactor(StringCalculateArgsBase e) => 
            1f;

        protected virtual Image GetImage(object context, string id, Size size)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            if (id[0] == '#')
            {
                return ResourceImageProvider.Current.GetImage(context, id, 1f, size);
            }
            IStringImageProvider provider = context as IStringImageProvider;
            return provider?.GetImage(id);
        }

        protected virtual Size GetImageBlockSize(object context, StringBlock imageBlock)
        {
            Size size = imageBlock.Size;
            if (!size.IsEmpty)
            {
                return size;
            }
            Image image = this.GetImage(context, imageBlock.ImageName, imageBlock.Size);
            return ((image == null) ? Size.Empty : image.Size);
        }

        private int GetLineHeight(StringInfoBase info, int startIndex, out int count, out int ascentHeight, bool roundTextHeight, out bool allAscentHeightsSame)
        {
            count = 0;
            allAscentHeightsSame = true;
            ascentHeight = info.Blocks[startIndex].FontBaseLine;
            int num = 0;
            int lineNumber = info.Blocks[startIndex].LineNumber;
            for (int i = startIndex; (i < info.Blocks.Count) && (info.Blocks[i].LineNumber == lineNumber); i++)
            {
                info.Blocks[i].Height = this.GetBlockHeight(info.Context, info.Blocks[i], roundTextHeight);
            }
            int num3 = 0;
            for (int j = startIndex; (j < info.Blocks.Count) && (info.Blocks[j].LineNumber == lineNumber); j++)
            {
                count++;
                num3 = Math.Max(num3, info.Blocks[j].Height);
                if (info.Blocks[j].Type == StringBlockType.Image)
                {
                    num = Math.Max(num, info.Blocks[j].FontBaseLine);
                }
                else
                {
                    if (ascentHeight != info.Blocks[j].FontBaseLine)
                    {
                        allAscentHeightsSame = false;
                    }
                    ascentHeight = Math.Max(ascentHeight, info.Blocks[j].FontBaseLine);
                }
            }
            return num3;
        }

        private string GetNextWord(ref string drawText)
        {
            if (this.GetUseAltAlgorithm())
            {
                return this.GetNextWordAlt(ref drawText);
            }
            if (drawText.Length == 0)
            {
                return "";
            }
            int length = 1;
            while ((length < drawText.Length) && (((!char.IsWhiteSpace(drawText[length]) && (drawText[length] >= ' ')) || (drawText[length] == '\x00a0')) || (drawText[length] == '\t')))
            {
                length++;
            }
            string str = drawText.Substring(0, length);
            drawText = drawText.Substring(length, drawText.Length - length);
            return str;
        }

        private string GetNextWordAlt(ref string drawText)
        {
            if (drawText.Length == 0)
            {
                return "";
            }
            int length = 1;
            while (((length < drawText.Length) && (!char.IsWhiteSpace(drawText[length]) && (drawText[length] >= ' '))) && (((char.GetUnicodeCategory(drawText[length]) != UnicodeCategory.OtherLetter) || (length >= (drawText.Length - 1))) || (char.GetUnicodeCategory(drawText[length + 1]) != UnicodeCategory.OtherLetter)))
            {
                length++;
            }
            string str = drawText.Substring(0, length);
            drawText = drawText.Substring(length, drawText.Length - length);
            return str;
        }

        private string GetNextWords(TextProcessInfoBase te, string drawText, int wordsWidth, out int width, int separatorWidth)
        {
            wordsWidth -= separatorWidth;
            if (this.GetUseAltAlgorithm())
            {
                return this.GetNextWordsAlt(te, drawText, wordsWidth, out width);
            }
            if (!te.AllowMultiLine)
            {
                width = this.GetStringWidth(te, drawText);
                return drawText;
            }
            StringBuilder builder = new StringBuilder(te.IsNewLine ? this.GetNextWord(ref drawText) : "");
            width = 0;
            string nextWord = "";
            while (true)
            {
                if (drawText != "")
                {
                    nextWord = this.GetNextWord(ref drawText);
                    if ((nextWord != "") && (nextWord[0] >= ' '))
                    {
                        int stringWidth = this.GetStringWidth(te, builder + nextWord);
                        if (stringWidth <= wordsWidth)
                        {
                            width = stringWidth;
                            builder.Append(nextWord);
                            continue;
                        }
                    }
                }
                if ((width == 0) && (builder.Length > 0))
                {
                    width = this.GetStringWidth(te, builder.ToString());
                }
                return builder.ToString();
            }
        }

        private string GetNextWordsAlt(TextProcessInfoBase te, string drawText, int wordsWidth, out int width)
        {
            width = 0;
            if (!te.AllowMultiLine)
            {
                width = this.GetStringWidth(te, drawText);
                return drawText;
            }
            string[] wordList = this.GetWordList(te, drawText);
            StringBuilder builder = new StringBuilder();
            if (wordList.Length <= 6)
            {
                for (int i = 0; i < wordList.Length; i++)
                {
                    string str3 = wordList[i];
                    int stringWidth = this.GetStringWidth(te, builder + str3);
                    if ((stringWidth > wordsWidth) && ((i > 0) || !te.IsNewLine))
                    {
                        break;
                    }
                    builder.Append(str3);
                }
            }
            else
            {
                string str = "";
                int num = 0;
                int num2 = wordList.Length - 1;
                int num3 = -1;
                while (true)
                {
                    if (num <= num2)
                    {
                        int count = num + ((num2 - num) >> 1);
                        string words = this.GetWords(wordList, count);
                        int stringWidth = this.GetStringWidth(te, words);
                        if (stringWidth < wordsWidth)
                        {
                            num = count + 1;
                            num3 = count;
                            str = words;
                            width = stringWidth;
                            continue;
                        }
                        if (stringWidth != wordsWidth)
                        {
                            num2 = count - 1;
                            continue;
                        }
                        width = stringWidth;
                        str = words;
                    }
                    if (str != "")
                    {
                        return str;
                    }
                    if (te.IsNewLine)
                    {
                        builder.Append(wordList[0]);
                    }
                    break;
                }
            }
            if ((width == 0) && (builder.Length > 0))
            {
                width = this.GetStringWidth(te, builder.ToString());
            }
            return builder.ToString();
        }

        private int GetStringWidth(TextProcessInfoBase te, string drawText) => 
            this.GetStringWidth(te, te.Info, te.Block, drawText);

        protected virtual int GetStringWidth(TextProcessInfoBase te, StringInfoBase info, StringBlock block, string drawText)
        {
            int width;
            if (drawText.Length == 0)
            {
                return 0;
            }
            StringFormat stringFormat = info.GetStringFormat();
            StringAlignment alignment = stringFormat.Alignment;
            StringFormatFlags formatFlags = stringFormat.FormatFlags;
            try
            {
                stringFormat.Alignment = StringAlignment.Near;
                stringFormat.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;
                width = this.CalcStringBlockSize(te, block, drawText, stringFormat).Width;
            }
            finally
            {
                stringFormat.FormatFlags = formatFlags;
                stringFormat.Alignment = alignment;
            }
            return width;
        }

        private string GetTextFitPart(TextProcessInfoBase te, string drawText, int maxWidth)
        {
            int num = 0;
            int length = drawText.Length;
            int num3 = 0;
            int stringWidth = 0;
            int num5 = 0;
            while (true)
            {
                if ((num5 < 0x20) && ((length - num) > 1))
                {
                    num3 = (length + num) / 2;
                    string str = drawText.Substring(0, num3);
                    stringWidth = this.GetStringWidth(te, str);
                    if (stringWidth != maxWidth)
                    {
                        if (stringWidth > maxWidth)
                        {
                            length = num3;
                        }
                        else
                        {
                            num = num3;
                        }
                        num5++;
                        continue;
                    }
                }
                if (stringWidth > maxWidth)
                {
                    num3 = num;
                }
                return drawText.Substring(0, num3);
            }
        }

        protected virtual bool GetUseAltAlgorithm() => 
            false;

        private string[] GetWordList(TextProcessInfoBase te, string drawText)
        {
            List<string> list = new List<string>();
            while (true)
            {
                if (drawText != "")
                {
                    string nextWord = this.GetNextWord(ref drawText);
                    if ((nextWord != "") && (nextWord[0] >= ' '))
                    {
                        list.Add(nextWord);
                        continue;
                    }
                }
                return list.ToArray();
            }
        }

        private string GetWords(string[] array, int count) => 
            string.Join(string.Empty, array, 0, Math.Min(count, array.Length));

        private bool HasPrecedingVisibleBlockTo(int blockIndex, Rectangle bounds, StringInfoBase info) => 
            (blockIndex > 0) && (info.BlocksBounds[blockIndex - 1].Bottom < bounds.Bottom);

        protected bool HasScriptBlocks(StringInfoBase info)
        {
            bool flag;
            using (List<StringBlock>.Enumerator enumerator = info.Blocks.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        StringBlock current = enumerator.Current;
                        if (!current.FontSettings.HasModifiers)
                        {
                            continue;
                        }
                        flag = true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                }
            }
            return flag;
        }

        private bool IsSeparator(StringBlock block) => 
            (block.Text != null) && ((block.Text.Length == 1) && (char.GetUnicodeCategory(block.Text, 0) == UnicodeCategory.OtherPunctuation));

        protected bool IsSimpleStringCore(string text) => 
            !string.IsNullOrEmpty(text) ? ((text.IndexOf('<') < 0) && (text.IndexOf('&') < 0)) : true;

        public static bool IsValidSize(Rectangle bounds) => 
            (bounds.Width > 0) && (bounds.Height > 0);

        private unsafe void OffsetBlocks(StringInfoBase info, int startLineNumber, int deltaY)
        {
            for (int i = 0; i < info.Blocks.Count; i++)
            {
                if (info.Blocks[i].LineNumber >= startLineNumber)
                {
                    Rectangle rectangle = info.BlocksBounds[i];
                    Rectangle* rectanglePtr1 = &rectangle;
                    rectanglePtr1.Y += deltaY;
                    info.BlocksBounds[i] = rectangle;
                }
            }
        }

        private unsafe void OffsetLines(StringInfoBase info, int startLineNumber, int deltaY)
        {
            for (int i = startLineNumber; i < info.LinesBounds.Count; i++)
            {
                Rectangle rectangle = info.LinesBounds[i];
                if (!rectangle.IsEmpty)
                {
                    Rectangle* rectanglePtr1 = &rectangle;
                    rectanglePtr1.Y += deltaY;
                    info.LinesBounds[i] = rectangle;
                }
            }
        }

        public List<StringBlock> Parse(StringInfoBase info, string text) => 
            this.ParseCore(info.Font.Size, info.Font.Style, info.HyperlinkSettings, text);

        protected virtual List<StringBlock> ParseCore(float fontSize, FontStyle style, HyperlinkSettings hyperlinkSettings, string text) => 
            DevExpress.Utils.Text.StringParser.Parse(Color.Empty, Color.Empty, hyperlinkSettings, fontSize, style, text, false);

        protected void ProcessBlock(StringCalculateArgsBase e, TextProcessInfoBase te, List<StringBlock> strings, int index)
        {
            switch (te.Block.Type)
            {
                case StringBlockType.Text:
                case StringBlockType.Link:
                    this.ProcessText(e, te, strings, index);
                    return;

                case StringBlockType.Image:
                    this.ProcessText(e, te, strings, index);
                    return;
            }
            throw new Exception($"Not implemented {te.Block.Type} block type");
        }

        private string ProcessEmptyWord(TextProcessInfoBase te, string drawText)
        {
            bool flag = true;
            if ((drawText.Length <= 0) || (drawText[0] != '\r'))
            {
                te.CurrentY += te.LineHeight;
            }
            else
            {
                while (true)
                {
                    if ((drawText.Length <= 0) || (drawText[0] != '\r'))
                    {
                        flag = false;
                        break;
                    }
                    drawText = drawText.Remove(0, 1);
                    this.RemoveControlChars(ref drawText);
                    te.CurrentY += te.LineHeight;
                }
            }
            this.RemoveControlChars(ref drawText);
            te.LineNumber++;
            te.LineHeight = 0;
            te.CurrentX = te.Bounds.X;
            te.IsNewLine = true;
            if (flag)
            {
                char[] trimChars = new char[] { ' ', '\x00a0' };
                drawText = drawText.TrimStart(trimChars);
            }
            return (!te.Info.DisableWrapNonFitText ? drawText : "");
        }

        protected void ProcessText(StringCalculateArgsBase e, TextProcessInfoBase te, List<StringBlock> strings, int index)
        {
            int num;
            int width;
            string str2;
            int num3;
            int num4;
            int num5;
            if (te.Bounds.Width <= 0)
            {
                num = 0x7fffffff;
            }
            string text = te.Block.Text;
            bool flag = false;
            bool flag2 = false;
            goto TR_0025;
        TR_0007:
            num4 = this.GetBlockHeight(te.Context, te.Block, te.RoundTextHeight);
            te.LineHeight = !te.IsNewLine ? Math.Max(num4, te.LineHeight) : num4;
            Rectangle rectangle = this.CreateNewBlock(e, te, width, str2);
            if (!te.AllowMultiLine && ((num != 0x7fffffff) && (te.CurrentX >= (te.Bounds.X + num))))
            {
                rectangle.Width = Math.Max(0, (te.Bounds.X + num) - rectangle.X);
                te.End.X = rectangle.Right;
                te.CurrentX = rectangle.Right;
                te.Info.BlocksBounds[te.Info.BlocksBounds.Count - 1] = rectangle;
                return;
            }
            te.End.X = Math.Min(te.End.X, te.Bounds.X + num);
            if (((te.Block.Type != StringBlockType.Text) && (te.Block.Type != StringBlockType.Link)) || (str2.Length == text.Length))
            {
                return;
            }
            else
            {
                text = text.Substring(str2.Length, text.Length - str2.Length);
            }
            goto TR_0025;
        TR_000A:
            if (te.Block.Type == StringBlockType.Image)
            {
                width = this.GetBlockSize(te.Context, te.Block).Width;
            }
            goto TR_0007;
        TR_0025:
            while (true)
            {
                if ((text.Length <= 0) || (text[0] == '\0'))
                {
                    return;
                }
                else
                {
                    te.IsNewLine = (te.Bounds.X == te.CurrentX) || !te.AllowMultiLine;
                    width = 0;
                    str2 = string.Empty;
                    num3 = this.CalcConnectedWidth(te, strings, index);
                    flag2 = (te.Block.Type == StringBlockType.Image) && (num3 > 0);
                    if (!(((te.Block.Type == StringBlockType.Text) || (te.Block.Type == StringBlockType.Link)) | flag2))
                    {
                        str2 = te.Block.Text;
                        width = this.GetBlockSize(te.Context, te.Block).Width;
                        goto TR_0007;
                    }
                    else
                    {
                        num5 = num - (te.CurrentX - te.Bounds.X);
                        if (!flag2)
                        {
                            str2 = this.GetNextWords(te, text, num5, out width, num3);
                        }
                        else
                        {
                            width = this.GetBlockSize(te.Context, te.Block).Width;
                            str2 = text;
                        }
                        if (!te.AllowMultiLine)
                        {
                            goto TR_000A;
                        }
                        else
                        {
                            if (flag2)
                            {
                                if (width <= num5)
                                {
                                    break;
                                }
                                this.ProcessEmptyWord(te, text);
                                continue;
                            }
                            if (!flag && (((str2 != "") && (str2[0] >= ' ')) || this.StartsWithNbsp(text)))
                            {
                                break;
                            }
                        }
                    }
                }
                flag = false;
                text = this.ProcessEmptyWord(te, text);
            }
            if ((str2 == "") && this.StartsWithNbsp(text))
            {
                width = this.GetStringWidth(te, text);
                str2 = text;
            }
            if (!te.AllowMultiLine || this.TextFit(te, width, num, num3))
            {
                goto TR_000A;
            }
            else if (!flag2)
            {
                string str3 = this.GetTextFitPart(te, text, num5);
                if (str3 != string.Empty)
                {
                    str2 = str3;
                    width = this.GetStringWidth(te, str2);
                    if (te.Info.DisableWrapNonFitText)
                    {
                        str2 = text;
                    }
                    flag = true;
                }
                goto TR_000A;
            }
            else if (te.CurrentX == te.Bounds.X)
            {
                goto TR_000A;
            }
            else
            {
                flag = false;
                text = this.ProcessEmptyWord(te, text);
            }
            goto TR_0025;
        }

        private void RemoveControlChars(ref string drawText)
        {
            while ((drawText.Length > 0) && ((drawText[0] != '\r') && (drawText[0] < ' ')))
            {
                drawText = drawText.Remove(0, 1);
            }
        }

        public string RemoveFormat(string text, bool allowNewLine)
        {
            if (this.IsSimpleStringCore(text))
            {
                return text;
            }
            List<StringBlock> list = DevExpress.Utils.Text.StringParser.Parse(12f, text, allowNewLine);
            if ((list == null) || (list.Count == 0))
            {
                return string.Empty;
            }
            if ((list.Count == 1) && ((list[0].Type == StringBlockType.Text) || (list[0].Type == StringBlockType.Link)))
            {
                return list[0].Text;
            }
            StringBuilder builder = new StringBuilder();
            int lineNumber = 0;
            for (int i = 0; i < list.Count; i++)
            {
                StringBlock block = list[i];
                if ((block.Type == StringBlockType.Text) || (block.Type == StringBlockType.Link))
                {
                    if ((lineNumber != block.LineNumber) & allowNewLine)
                    {
                        builder.Append("\r\n");
                        lineNumber = block.LineNumber;
                    }
                    builder.Append(block.Text);
                }
            }
            return builder.ToString();
        }

        public virtual void SetModifierFontInfo(StringCalculateArgsBase e, Font font, StringBlockTextModifierInfo modifier)
        {
        }

        private unsafe void SetupSimpleString(StringInfoBase info, StringCalculateArgsBase e)
        {
            Size size = this.CalcTextSizeInt(info, e);
            if (e.RoundTextHeight && (e.RoundTextHeight && ((size.Height % 2) == 1)))
            {
                Size* sizePtr1 = &size;
                sizePtr1.Height++;
            }
            info.simpleString = true;
            info.SetBounds(new Rectangle(e.Bounds.Location, size));
            info.OriginalBounds = e.Bounds;
            info.Assign(null, null, e.Text);
        }

        private bool StartsWithNbsp(string text) => 
            (text.Length > 0) && (text[0] == '\x00a0');

        private bool TextFit(TextProcessInfoBase te, int textWidth, int maxWidth, int separatorWidth) => 
            (((te.CurrentX - te.Bounds.X) + textWidth) + separatorWidth) <= maxWidth;

        protected void UpdateBaseLine(Rectangle originalBounds, StringInfoBase info, TextProcessInfoBase te)
        {
            if (info.Blocks.Count < 2)
            {
                if (info.Blocks.Count == 1)
                {
                    info.LinesBounds = new List<Rectangle>();
                    info.LinesBounds.Add(info.BlocksBounds[0]);
                }
            }
            else
            {
                info.LinesBounds = new List<Rectangle>();
                int startIndex = 0;
                while (startIndex < info.Blocks.Count)
                {
                    int num;
                    int num3;
                    bool flag;
                    int num4 = this.GetLineHeight(info, startIndex, out num, out num3, info.RoundTextHeight, out flag);
                    num3 = Math.Min(num3, num4);
                    Rectangle empty = Rectangle.Empty;
                    int num5 = startIndex;
                    while (true)
                    {
                        if (num5 >= (startIndex + num))
                        {
                            while (true)
                            {
                                if (info.LinesBounds.Count >= info.Blocks[startIndex].LineNumber)
                                {
                                    info.LinesBounds.Add(empty);
                                    startIndex += num;
                                    break;
                                }
                                info.LinesBounds.Add(Rectangle.Empty);
                            }
                            break;
                        }
                        Rectangle bounds = info.BlocksBounds[num5];
                        StringBlock sb = info.Blocks[num5];
                        if ((sb.Type != StringBlockType.Text) && (sb.Type != StringBlockType.Link))
                        {
                            bounds = this.UpdateBlockBoundsByAlign(sb, bounds, num4);
                        }
                        else if ((originalBounds.Height < 1) || (originalBounds.Height > bounds.Height))
                        {
                            VertAlignment vAlignment = info.VAlignment;
                            bounds.Y = (!flag || !info.AllowBaselineAlignment) ? this.UpdateTextBaseLine(info, num3, num4, sb, bounds, vAlignment) : this.CalcYByAppearance(vAlignment, bounds, num4, true);
                            if (!IsValidSize(info.Bounds))
                            {
                                te.End.Y = Math.Max(te.End.Y, bounds.Bottom);
                            }
                        }
                        if (empty.IsEmpty)
                        {
                            empty = bounds;
                        }
                        else
                        {
                            empty.X = Math.Min(empty.X, bounds.X);
                            empty.Y = Math.Min(empty.Y, bounds.Y);
                            if (bounds.Right > empty.Right)
                            {
                                empty.Width = bounds.Right - empty.X;
                            }
                            if (bounds.Bottom > empty.Bottom)
                            {
                                empty.Height = bounds.Bottom - empty.Y;
                            }
                        }
                        info.BlocksBounds[num5] = bounds;
                        num5++;
                    }
                }
            }
        }

        private void UpdateBlockAscentHeight(TextProcessInfoBase te, StringBlock newBlock, Rectangle lastBlock)
        {
            if ((newBlock.Type == StringBlockType.Image) && (lastBlock.Height > 0))
            {
                newBlock.SetAscentHeight((int) (lastBlock.Height * AscentHeightMultiplier));
            }
        }

        private unsafe Rectangle UpdateBlockBoundsByAlign(StringBlock sb, Rectangle bounds, int lineHeight)
        {
            if (bounds.Height < lineHeight)
            {
                switch (sb.Alignment)
                {
                    case StringBlockAlignment.Center:
                    {
                        Rectangle* rectanglePtr2 = &bounds;
                        rectanglePtr2.Y += (lineHeight - bounds.Height) / 2;
                        break;
                    }
                    case StringBlockAlignment.Bottom:
                    {
                        Rectangle* rectanglePtr1 = &bounds;
                        rectanglePtr1.Y += lineHeight - bounds.Height;
                        break;
                    }
                    default:
                        break;
                }
            }
            return bounds;
        }

        protected abstract void UpdateBlockFont(StringCalculateArgsBase e, StringBlock block, Font initialFont);
        public void UpdateBoundsByAppearance(StringInfoBase info)
        {
            this.UpdateBoundsByAppearance(info, info.Bounds);
        }

        protected void UpdateBoundsByAppearance(StringInfoBase info, Rectangle bounds)
        {
            int x = CalcXByAlignment(bounds, info.Bounds.Width, this.CheckRTLHAlignment(info));
            int y = this.CalcYByAppearance(info.VAlignment, bounds, info.Bounds.Height, false);
            if (bounds.Width < 1)
            {
                x = bounds.X;
            }
            if (bounds.Height < 1)
            {
                y = bounds.Y;
            }
            info.UpdateLocation(new Point(x, y));
            if (bounds.Width > 0)
            {
                info.UpdateXLocation(this.CheckRTLHAlignment(info), bounds);
            }
            info.OriginalBounds = bounds;
        }

        protected void UpdateFont(StringCalculateArgsBase e, List<StringBlock> strings, Font initialFont)
        {
            foreach (StringBlock block in strings)
            {
                this.UpdateBlockFont(e, block, initialFont);
            }
        }

        private unsafe void UpdateLineHeight(StringInfoBase info, int lineIndex, int deltaHeight)
        {
            Rectangle rectangle = info.LinesBounds[lineIndex];
            Rectangle* rectanglePtr1 = &rectangle;
            rectanglePtr1.Height += deltaHeight;
            info.LinesBounds[lineIndex] = rectangle;
        }

        private void UpdateMaximumBounds(StringInfoBase info, Rectangle bounds, TextProcessInfoBase te)
        {
            if (IsValidSize(bounds))
            {
                for (int i = info.BlocksBounds.Count - 1; i >= 0; i--)
                {
                    Rectangle empty = info.BlocksBounds[i];
                    if (empty.Right > bounds.Right)
                    {
                        empty.Width = bounds.Right - empty.X;
                        if (empty.Width < 1)
                        {
                            empty = Rectangle.Empty;
                        }
                    }
                    if (empty.Bottom > bounds.Bottom)
                    {
                        if ((info.GetIsEllipsisTrimming() && (i > 0)) || !info.AllowPartiallyVisibleRows)
                        {
                            empty = Rectangle.Empty;
                        }
                        else
                        {
                            empty.Height = bounds.Bottom - empty.Y;
                            if (empty.Height < 1)
                            {
                                empty = Rectangle.Empty;
                            }
                        }
                    }
                    if (!empty.IsEmpty)
                    {
                        info.BlocksBounds[i] = empty;
                    }
                    else
                    {
                        info.BlocksBounds.RemoveAt(i);
                        info.Blocks.RemoveAt(i);
                        if (!info.AllowPartiallyVisibleRows)
                        {
                            te.End.Y = (i > 0) ? info.BlocksBounds[i - 1].Bottom : te.Bounds.Y;
                        }
                        if (info.GetIsEllipsisTrimming() && this.HasPrecedingVisibleBlockTo(i, bounds, info))
                        {
                            this.AddEllipsisCharacter(info, bounds, te, i - 1);
                        }
                    }
                }
            }
        }

        protected void UpdateScriptBlocks(Rectangle originalBounds, StringInfoBase info)
        {
            for (int i = 0; i < info.Blocks.Count; i++)
            {
                bool hasModifiers = info.Blocks[i].FontSettings.HasModifiers;
            }
        }

        protected unsafe void UpdateScripts(StringCalculateArgsBase e, Rectangle originalBounds, StringInfoBase info, TextProcessInfoBase te)
        {
            int deltaY = 0;
            int num2 = 0;
            int startLineNumber = 0;
            int num4 = -1;
            StringBlockTextModifierInfo modifier = null;
            Point location = e.Bounds.Location;
            bool flag = false;
            for (int i = 0; i < info.Blocks.Count; i++)
            {
                if (info.Blocks[i].LineNumber != startLineNumber)
                {
                    if (num2 > 0)
                    {
                        this.OffsetBlocks(info, startLineNumber, num2);
                        this.OffsetLines(info, startLineNumber + 1, num2);
                        this.UpdateLineHeight(info, startLineNumber, num2);
                    }
                    if (deltaY > 0)
                    {
                        this.OffsetBlocks(info, startLineNumber + 1, deltaY);
                        this.OffsetLines(info, startLineNumber + 1, deltaY);
                        this.UpdateLineHeight(info, startLineNumber, deltaY);
                    }
                    deltaY = num2 = 0;
                    num4 = -1;
                    startLineNumber = info.Blocks[i].LineNumber;
                }
                if (!info.Blocks[i].FontSettings.HasModifiers)
                {
                    if (modifier == null)
                    {
                        StringBlockTextModifierInfo info1 = new StringBlockTextModifierInfo();
                        info1.Modifier = StringBlockTextModifier.Normal;
                        info1.Height = info.Blocks[i].FontHeight;
                        info1.AscentHeight = info.Blocks[i].FontAscentHeight;
                        info1.InternalLeading = info.Blocks[i].FontInternalLeading;
                        modifier = info1;
                    }
                    num4 = i;
                }
                else
                {
                    flag = true;
                    if (num4 == -1)
                    {
                        num4 = this.FindNextAnchorIndex(info, startLineNumber, i);
                    }
                    Rectangle anchorBounds = new Rectangle(0, location.Y, 0, 0);
                    if (num4 != -1)
                    {
                        anchorBounds = info.BlocksBounds[num4];
                        Rectangle* rectanglePtr1 = &anchorBounds;
                        rectanglePtr1.Y += info.Blocks[num4].FontInternalLeading;
                        anchorBounds.Height = info.Blocks[num4].FontInnerHeight;
                    }
                    else
                    {
                        if (modifier == null)
                        {
                            StringBlockTextModifierInfo info3 = new StringBlockTextModifierInfo();
                            info3.Modifier = StringBlockTextModifier.Normal;
                            info3.Size = info.Font.Size;
                            modifier = info3;
                            this.SetModifierFontInfo(e, info.Font, modifier);
                        }
                        anchorBounds.Y = info.LinesBounds[startLineNumber].Y + modifier.InternalLeading;
                        anchorBounds.Height = modifier.InnerHeight;
                    }
                    Rectangle rectangle4 = this.CalculateOffset(e, anchorBounds, info.BlocksBounds[i], info.Blocks[i]);
                    num2 = Math.Max(num2, info.LinesBounds[startLineNumber].Top - rectangle4.Top);
                    deltaY = Math.Max(deltaY, rectangle4.Bottom - info.LinesBounds[startLineNumber].Bottom);
                    info.BlocksBounds[i] = rectangle4;
                    te.End.Y = Math.Max(te.End.Y, rectangle4.Bottom);
                }
            }
            if (num2 > 0)
            {
                this.OffsetBlocks(info, startLineNumber, num2);
                this.OffsetLines(info, startLineNumber + 1, num2);
                this.UpdateLineHeight(info, startLineNumber, num2);
            }
            if (deltaY > 0)
            {
                this.OffsetBlocks(info, startLineNumber + 1, deltaY);
                this.OffsetLines(info, startLineNumber + 1, deltaY);
                this.UpdateLineHeight(info, startLineNumber, deltaY);
            }
            if (flag)
            {
                for (int j = 0; j < info.BlocksBounds.Count; j++)
                {
                    Rectangle rectangle = info.BlocksBounds[j];
                    te.End.Y = Math.Max(te.End.Y, rectangle.Bottom);
                }
            }
        }

        private unsafe int UpdateTextBaseLine(StringInfoBase info, int ascentHeight, int lineHeight, StringBlock block, Rectangle bounds, VertAlignment valign)
        {
            int y = bounds.Y;
            int num2 = bounds.Y + lineHeight;
            switch (valign)
            {
                case VertAlignment.Default:
                case VertAlignment.Center:
                {
                    Rectangle* rectanglePtr1 = &bounds;
                    rectanglePtr1.Y += ((lineHeight - bounds.Height) / 2) + ((ascentHeight - block.FontAscentHeight) / 2);
                    break;
                }
                case VertAlignment.Top:
                {
                    Rectangle* rectanglePtr2 = &bounds;
                    rectanglePtr2.Y += ascentHeight - block.FontBaseLine;
                    break;
                }
                case VertAlignment.Bottom:
                {
                    Rectangle* rectanglePtr3 = &bounds;
                    rectanglePtr3.Y += lineHeight - bounds.Height;
                    break;
                }
                default:
                    break;
            }
            bounds.Y = Math.Max(bounds.Y, y);
            if ((bounds.Y + block.FontAscentHeight) > num2)
            {
                Rectangle* rectanglePtr4 = &bounds;
                rectanglePtr4.Y -= bounds.Bottom - num2;
            }
            return bounds.Y;
        }
    }
}

