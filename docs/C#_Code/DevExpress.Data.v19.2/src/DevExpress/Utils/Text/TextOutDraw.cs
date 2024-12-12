namespace DevExpress.Utils.Text
{
    using DevExpress.Data;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Text;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class TextOutDraw : IWordBreakProvider
    {
        [ThreadStatic]
        public static TextOutDraw LastTextOut;
        private Graphics graphics;
        private string text;
        private Rectangle drawBounds;
        private Rectangle clipedBounds;
        [ThreadStatic]
        private static int[] CachedWidths = new int[0x2000];
        private int[] widths;
        private bool[] hotPrefixes;
        private StringFormat format;
        private StringAlignment formatAlignment;
        private StringFormatFlags formatFlags;
        private TextHighLight highLight;
        private DevExpress.Utils.Text.FontCache fontCache;
        private int pos;
        private int drawTop;
        private int linepos;
        private bool isCliped;
        private bool measureTrailingSpaces;
        private bool isNoWrap;
        private bool trimming;
        private DevExpress.Utils.Text.TextLines lines;
        private readonly IWordBreakProvider wordBreakProvider;

        public TextOutDraw(bool singleLineSimpleString, DevExpress.Utils.Text.FontCache fontCache, Graphics graphics, string text, Rectangle drawBounds, StringFormat format)
        {
            this.fontCache = fontCache;
            this.graphics = graphics;
            this.format = format;
            this.drawBounds = drawBounds;
            this.formatAlignment = this.CalcAlignment(format.Alignment, (format.FormatFlags & StringFormatFlags.DirectionRightToLeft) != 0);
            this.formatFlags = format.FormatFlags;
            this.wordBreakProvider = this;
            this.text = text;
            this.lines = new DevExpress.Utils.Text.TextLines(this);
            int count = 0;
            this.widths = this.GetCharactersWidth(this.Text, drawBounds.Width, out count);
            this.IsCropped = count < this.Text.Length;
            this.measureTrailingSpaces = (this.formatFlags & StringFormatFlags.MeasureTrailingSpaces) == StringFormatFlags.MeasureTrailingSpaces;
            this.CreateSingleLine(count);
        }

        public TextOutDraw(DevExpress.Utils.Text.FontCache fontCache, Graphics graphics, string text, Rectangle drawBounds, Rectangle clipBounds, StringFormat format, TextHighLight highLight, IWordBreakProvider provider)
        {
            this.fontCache = fontCache;
            this.graphics = graphics;
            this.format = format;
            this.formatAlignment = this.CalcAlignment(format.Alignment, (format.FormatFlags & StringFormatFlags.DirectionRightToLeft) != 0);
            this.formatFlags = format.FormatFlags;
            this.highLight = highLight;
            this.wordBreakProvider = (provider != null) ? provider : this;
            this.text = this.ValidateHotPrefix(text);
            this.drawBounds = this.ApplyTextUtilsOffsets(drawBounds);
            if (clipBounds.IsEmpty)
            {
                this.clipedBounds = drawBounds;
            }
            else
            {
                this.isCliped = true;
                this.clipedBounds = clipBounds;
                this.clipedBounds.Intersect(drawBounds);
            }
            this.widths = this.GetCharactersWidth(this.Text);
            this.isNoWrap = (this.formatFlags & StringFormatFlags.NoWrap) == StringFormatFlags.NoWrap;
            this.trimming = format.Trimming != StringTrimming.None;
            this.isCliped |= ((this.formatFlags & StringFormatFlags.NoClip) == 0) || (this.IsNoWrap && !this.trimming);
            this.measureTrailingSpaces = (this.formatFlags & StringFormatFlags.MeasureTrailingSpaces) == StringFormatFlags.MeasureTrailingSpaces;
            this.CreateLines();
        }

        private void AddNewLineToList()
        {
            int length = this.Pos - this.linepos;
            if ((length > 0) && this.trimming)
            {
                int nextWord = this.GetNextWord(this.Pos - 1, this.Text.Length);
                if ((nextWord - 1) > 0)
                {
                    length += nextWord - 1;
                    this.pos += nextWord - 1;
                }
            }
            if ((length > 0) && !this.MeasureTrailingSpaces)
            {
                while (true)
                {
                    if ((length <= 0) || !this.CanRemoveSymbol((this.linepos + length) - 1))
                    {
                        while ((this.Pos < this.Text.Length) && this.CanRemoveSymbol(this.Pos))
                        {
                            this.pos++;
                        }
                        break;
                    }
                    length--;
                }
            }
            if ((this.Pos < this.Text.Length) && DevExpress.Utils.Text.FontCache.IsNewLine(this.Text[this.Pos]))
            {
                this.pos++;
            }
            this.lines.Add(this.linepos, length);
            if (this.trimming)
            {
                this.lines[this.lines.Count - 1].UpdateTrimmingLine(this.Format.Trimming, this.drawBounds.Width);
            }
            this.linepos = this.Pos;
            this.drawTop += this.FontHeight;
        }

        private unsafe Rectangle ApplyTextUtilsOffsets(Rectangle bounds)
        {
            Rectangle* rectanglePtr1 = &bounds;
            rectanglePtr1.X += TextUtils.LeftOffset;
            Rectangle* rectanglePtr2 = &bounds;
            rectanglePtr2.Y += TextUtils.TopOffset;
            Rectangle* rectanglePtr3 = &bounds;
            rectanglePtr3.Width -= TextUtils.RightOffset + TextUtils.LeftOffset;
            Rectangle* rectanglePtr4 = &bounds;
            rectanglePtr4.Height -= TextUtils.BottomOffset + TextUtils.TopOffset;
            return bounds;
        }

        private StringAlignment CalcAlignment(StringAlignment stringAlignment, bool rightToLeft) => 
            rightToLeft ? ((stringAlignment == StringAlignment.Near) ? StringAlignment.Far : ((stringAlignment == StringAlignment.Far) ? StringAlignment.Near : stringAlignment)) : stringAlignment;

        private bool CanAddOneMoreLine(int lineTop) => 
            (this.trimming || !this.IsNoWrap) ? (((this.drawTop + this.FontHeight) <= this.DrawBounds.Height) || (this.lines.Count == 0)) : (this.drawTop < this.DrawBounds.Height);

        private bool CanRemoveSymbol(int pos) => 
            char.IsWhiteSpace(this.Text[pos]) && (!DevExpress.Utils.Text.FontCache.IsNewLine(this.Text[pos]) && ((this.hotPrefixes == null) || !this.hotPrefixes[pos]));

        private unsafe Rectangle CorrectRectangleLeft(Rectangle bounds, TextLine line)
        {
            if (this.FormatAlignment != StringAlignment.Near)
            {
                int lineWidth = line.LineWidth;
                if (this.FormatAlignment == StringAlignment.Far)
                {
                    Rectangle* rectanglePtr1 = &bounds;
                    rectanglePtr1.X += bounds.Width - lineWidth;
                }
                else
                {
                    Rectangle* rectanglePtr2 = &bounds;
                    rectanglePtr2.X += (bounds.Width - lineWidth) / 2;
                }
            }
            return bounds;
        }

        private Rectangle CorrectRectangleTop(Rectangle bounds) => 
            this.CorrectRectangleTop(bounds, this.FontHeight);

        private unsafe Rectangle CorrectRectangleTop(Rectangle bounds, int height)
        {
            if (((bounds.Height >= height) || !this.trimming) && (bounds.Height != this.FontHeight))
            {
                switch (this.Format.LineAlignment)
                {
                    case StringAlignment.Center:
                    {
                        Rectangle* rectanglePtr2 = &bounds;
                        rectanglePtr2.Y += (bounds.Height - height) / 2;
                        break;
                    }
                    case StringAlignment.Far:
                    {
                        Rectangle* rectanglePtr1 = &bounds;
                        rectanglePtr1.Y += bounds.Height - height;
                        break;
                    }
                    default:
                        break;
                }
            }
            return bounds;
        }

        private void CreateLines()
        {
            this.IsCropped = false;
            this.lines = new DevExpress.Utils.Text.TextLines(this);
            if (!this.IsNoWrap)
            {
                this.CreateWrapLines();
            }
            else if (this.Text.IndexOf(DevExpress.Utils.Text.FontCache.NewLineChar) > -1)
            {
                this.CreateReturnLines();
            }
            else
            {
                this.CreateNoWrapLines();
            }
        }

        private void CreateNoWrapLines()
        {
            this.lines.Add(0, this.Text.Length);
            this.lines[0].UpdateTrimmingLine(this.Format.Trimming, this.drawBounds.Width);
            if (this.lines[0].Length < this.Text.Length)
            {
                this.IsCropped = true;
            }
        }

        private void CreateReturnLines()
        {
            int num = 0;
            while ((this.Pos < this.Text.Length) && this.CanAddOneMoreLine(this.drawTop))
            {
                if (!DevExpress.Utils.Text.FontCache.IsNewLine(this.Text[this.Pos]))
                {
                    if (this.Text[this.Pos] != DevExpress.Utils.Text.FontCache.ReturnChar)
                    {
                        num++;
                    }
                    this.pos++;
                    continue;
                }
                this.AddNewLineToList();
                if (this.lines[this.lines.Count - 1].Length < num)
                {
                    this.IsCropped = true;
                }
                num = 0;
            }
            if ((this.Pos - this.linepos) > 0)
            {
                this.AddNewLineToList();
            }
            char[] trimChars = new char[] { DevExpress.Utils.Text.FontCache.ReturnChar, DevExpress.Utils.Text.FontCache.NewLineChar };
            string str = this.Text.TrimEnd(trimChars);
            if (this.Pos < (str.Length - 1))
            {
                this.IsCropped = true;
            }
            this.pos = 0;
        }

        private void CreateSingleLine(int maxCount)
        {
            this.lines.Add(0, maxCount);
            if (this.IsCropped && ((this.Format.Trimming != StringTrimming.None) && (this.Format.Trimming != StringTrimming.Character)))
            {
                this.lines[0].UpdateTrimmingLine(this.Format.Trimming, this.drawBounds.Width);
            }
        }

        private void CreateWrapLines()
        {
            while (true)
            {
                if ((this.Pos >= this.Text.Length) || !this.CanAddOneMoreLine(this.drawTop))
                {
                    break;
                }
                while (true)
                {
                    if ((this.Pos < this.Text.Length) && DevExpress.Utils.Text.FontCache.IsNewLine(this.Text[this.Pos]))
                    {
                        this.AddNewLineToList();
                        continue;
                    }
                    if (((this.Pos >= this.Text.Length) || !this.CanAddOneMoreLine(this.drawTop)) || (this.widths[this.Pos] > this.DrawBounds.Width))
                    {
                        break;
                    }
                    else
                    {
                        int nextWord = this.GetNextWord(this.Pos, this.Text.Length);
                        if (this.GetTextWidth(this.linepos, (this.pos - this.linepos) + nextWord, false) <= this.DrawBounds.Width)
                        {
                            this.pos += nextWord;
                        }
                        else if ((this.Pos - this.linepos) <= 0)
                        {
                            int num3 = this.widths[this.Pos];
                            int num4 = 1;
                            while (true)
                            {
                                if (num4 < nextWord)
                                {
                                    Rectangle drawBounds = this.DrawBounds;
                                    if (((num3 + this.widths[this.Pos + num4]) + this.GetCharABCWidths((int) (this.Pos + num4))) <= drawBounds.Width)
                                    {
                                        num3 += this.widths[this.Pos + num4];
                                        num4++;
                                        continue;
                                    }
                                    this.pos += num4;
                                    this.IsCropped = true;
                                }
                                this.AddNewLineToList();
                                if (nextWord != 1)
                                {
                                    break;
                                }
                                break;
                            }
                        }
                        else
                        {
                            this.AddNewLineToList();
                        }
                    }
                    break;
                }
            }
            if ((this.Pos - this.linepos) > 0)
            {
                this.AddNewLineToList();
            }
            if (this.Pos < (this.Text.Length - 1))
            {
                if (this.lines.Count > 0)
                {
                    this.lines[this.lines.Count - 1].UpdateTrimmingLine(this.Format.Trimming, this.drawBounds.Width, true, this.Text);
                }
                this.IsCropped = true;
            }
            this.pos = 0;
        }

        bool IWordBreakProvider.IsWordBreakChar(char ch) => 
            !char.IsWhiteSpace(ch) ? ((CustomWordBreakProvider != null) && CustomWordBreakProvider.IsWordBreakChar(ch)) : true;

        public void DrawCached(Graphics graphics)
        {
            bool allowCache = AllowCache;
            try
            {
                IntPtr hdc = graphics.GetHdc();
                IntPtr handle = Win32Util.SelectObject(hdc, this.FontCache.FontHandle);
                Win32Util.SetTextColor(hdc, this.ForeColor);
                Win32Util.SetBkMode(hdc, 1);
                foreach (TextLineInfo info in this.CachedLines)
                {
                    this.DrawText(hdc, info.Text, info.X, info.Y, info.Widths);
                }
                Win32Util.SelectObject(hdc, handle);
                graphics.ReleaseHdcInternal(hdc);
            }
            finally
            {
                AllowCache = allowCache;
            }
        }

        private void DrawHighlightBackground(IntPtr hdc, Rectangle bounds)
        {
            if (this.highLight.BackColor != Color.Empty)
            {
                IntPtr hBrush = Win32Util.CreateSolidBrush(this.highLight.BackColor);
                Win32Util.FillRect(hdc, bounds, hBrush);
                Win32Util.DeleteObject(hBrush);
            }
        }

        private void DrawHighlightedRanges(IntPtr hdc, int startPos, int length, int x, int y, TextHighLight highLight, bool onlyHighlightedBackground)
        {
            for (int i = 0; i < highLight.Ranges.Length; i++)
            {
                DisplayTextHighlightRange range = highLight.Ranges[i];
                if (startPos < range.Start)
                {
                    if (length <= 0)
                    {
                        return;
                    }
                    this.DrawStringLine(hdc, ref startPos, ref length, ref x, y, Math.Min(range.Start - startPos, length), true, onlyHighlightedBackground);
                }
                int num2 = range.Start + range.Length;
                int highLightLen = (length < (num2 - startPos)) ? length : (num2 - startPos);
                if (length <= 0)
                {
                    return;
                }
                if (highLightLen > 0)
                {
                    this.DrawHighLightStringLine(hdc, ref startPos, ref length, ref x, y, highLightLen, onlyHighlightedBackground);
                }
            }
            if (!onlyHighlightedBackground && (length > 0))
            {
                this.DrawStringLine(hdc, startPos, length, x, y);
            }
        }

        protected virtual void DrawHighLightStringLine(IntPtr hdc, ref int startPos, ref int length, ref int x, int y, int highLightLen, bool onlyHighlightedBackground)
        {
            Rectangle bounds = new Rectangle(x, y, this.GetTextWidth(startPos, highLightLen, false), this.FontHeight);
            if (!this.ClipedBounds.IsEmpty && (bounds.Right >= this.ClipedBounds.Right))
            {
                bounds.Width = Math.Max(0, this.ClipedBounds.Right - bounds.X);
            }
            if (onlyHighlightedBackground)
            {
                this.DrawHighlightBackground(hdc, bounds);
            }
            int color = 0;
            if (!onlyHighlightedBackground)
            {
                color = Win32Util.GetTextColor(hdc);
                Win32Util.SetTextColor(hdc, this.highLight.ForeColor);
            }
            this.DrawStringLine(hdc, ref startPos, ref length, ref x, y, highLightLen, true, onlyHighlightedBackground);
            if (!onlyHighlightedBackground)
            {
                Win32Util.SetTextColor(hdc, color);
            }
        }

        public void DrawSingleLineString(IntPtr hdc)
        {
            TextLine line = this.lines[0];
            this.drawBounds = this.CorrectRectangleTop(this.DrawBounds, this.FontHeight);
            this.drawBounds = this.CorrectRectangleLeft(this.DrawBounds, line);
            Win32Util.ExtTextOut(hdc, this.drawBounds.X, this.drawBounds.Y, this.isCliped, this.DrawBounds, this.text, line.Length, this.Widths);
            if (line.HasEllipsis)
            {
                Win32Util.ExtTextOut(hdc, this.drawBounds.X + line.TextWidth, this.drawBounds.Y, this.isCliped, this.DrawBounds, line.EllipsisText, line.EllipsisWidths);
            }
        }

        public void DrawString(Graphics graphics)
        {
            IntPtr hdc = graphics.GetHdc();
            Win32Util.SetTextColor(hdc, this.ForeColor);
            Win32Util.SetBkMode(hdc, 1);
            this.DrawString(hdc);
            Win32Util.SelectObject(hdc, Win32Util.SelectObject(hdc, this.FontCache.FontHandle));
            graphics.ReleaseHdcInternal(hdc);
        }

        public void DrawString(IntPtr hdc)
        {
            if (this.Text.Length != 0)
            {
                this.DrawStringLines(hdc);
            }
        }

        private unsafe void DrawStringLine(IntPtr hdc, TextLine line, Rectangle bounds)
        {
            bounds = this.CorrectRectangleLeft(bounds, line);
            this.DrawStringLine(hdc, line.Position, line.Length, bounds.X, bounds.Y, this.highLight);
            if (line.HasEllipsis)
            {
                if ((this.highLight == null) || ((this.highLight.StartIndex < line.Length) || (bounds.Width <= 0)))
                {
                    this.DrawText(hdc, line.EllipsisText, bounds.X + line.TextWidth, bounds.Y, line.EllipsisWidths);
                }
                else
                {
                    Rectangle rectangle = new Rectangle(bounds.X + line.TextWidth, bounds.Y, Math.Min(bounds.Width, line.EllipsisWidth), this.FontHeight);
                    this.DrawHighlightBackground(hdc, rectangle);
                    int textColor = Win32Util.GetTextColor(hdc);
                    Win32Util.SetTextColor(hdc, this.highLight.ForeColor);
                    this.DrawText(hdc, line.EllipsisText, bounds.X + line.TextWidth, bounds.Y, line.EllipsisWidths);
                    Win32Util.SetTextColor(hdc, textColor);
                }
                Rectangle* rectanglePtr1 = &bounds;
                rectanglePtr1.X += line.EllipsisWidth;
            }
            if (line.Length2 > 0)
            {
                this.DrawStringLine(hdc, line.Position2, line.Length2, bounds.X + line.TextWidth, bounds.Y, this.highLight);
            }
        }

        private void DrawStringLine(IntPtr hdc, int startPos, int length, int x, int y)
        {
            int[] lineWidths = this.GetLineWidths(startPos, length);
            bool[] hotkeys = null;
            if (this.hotPrefixes != null)
            {
                hotkeys = new bool[lineWidths.Length];
                for (int i = 0; i < length; i++)
                {
                    hotkeys[i] = this.hotPrefixes[startPos + i];
                }
            }
            string lineText = ((startPos != 0) || (length != this.Text.Length)) ? this.Text.Substring(startPos, length) : this.Text;
            this.DrawStringLine(hdc, lineText, x, y, lineWidths, hotkeys);
        }

        protected void DrawStringLine(IntPtr hdc, string lineText, int x, int y, int[] lineWidths)
        {
            if (lineText.Length != 0)
            {
                lineText = this.ReplaceTabsWithSpaces(lineText);
                this.DrawText(hdc, lineText, x, y, lineWidths);
            }
        }

        private void DrawStringLine(IntPtr hdc, int startPos, int length, int x, int y, TextHighLight highLight)
        {
            if ((highLight == null) || !highLight.IsTextHighLighted(startPos, length))
            {
                this.DrawStringLine(hdc, startPos, length, x, y);
            }
            else
            {
                this.DrawHighlightedRanges(hdc, startPos, length, x, y, highLight, true);
                this.DrawHighlightedRanges(hdc, startPos, length, x, y, highLight, false);
            }
        }

        private void DrawStringLine(IntPtr hdc, string lineText, int x, int y, int[] lineWidths, bool[] hotkeys)
        {
            if (this.GetHotKeysCount(hotkeys, lineText.Length) == 0)
            {
                this.DrawStringLine(hdc, lineText, x, y, lineWidths);
            }
            else
            {
                this.DrawStringLineWithHotkeys(hdc, lineText, x, y, lineWidths, hotkeys);
            }
        }

        private void DrawStringLine(IntPtr hdc, ref int startPos, ref int length, ref int x, int y, int highLightLen, bool ignoreOverhang = false, bool onlyCalculate = false)
        {
            if (!onlyCalculate)
            {
                this.DrawStringLine(hdc, startPos, highLightLen, x, y);
            }
            x += this.GetTextWidth(startPos, highLightLen, ignoreOverhang);
            startPos += highLightLen;
            length -= highLightLen;
        }

        private void DrawStringLines(IntPtr hdc)
        {
            this.isCliped |= this.DrawBounds.Height < (this.FontHeight * this.lines.Count);
            this.drawBounds = this.CorrectRectangleTop(this.DrawBounds, this.FontHeight * this.lines.Count);
            for (int i = 0; i < this.lines.Count; i++)
            {
                Rectangle drawBounds = this.DrawBounds;
                Rectangle bounds = new Rectangle(this.DrawBounds.X, this.DrawBounds.Y + (i * this.FontHeight), drawBounds.Width, this.FontHeight);
                this.DrawStringLine(hdc, this.lines[i], bounds);
            }
        }

        protected virtual void DrawStringLineWithHotkeys(IntPtr hdc, string lineText, int textX, int textY, int[] lineWidths, bool[] hotkeys)
        {
            string str;
            int[] numArray;
            int num;
            this.GetHotKeyInfo(lineText, textX, lineWidths, hotkeys, out str, out numArray, out num);
            this.DrawStringLine(hdc, str, num, textY, numArray);
            IntPtr zero = IntPtr.Zero;
            if (this.FontCache != null)
            {
                zero = Win32Util.SelectObject(hdc, this.FontCache.FontUnderlineHandle);
            }
            num = textX;
            numArray = new int[1];
            for (int i = 0; i < lineText.Length; i++)
            {
                if (hotkeys[i])
                {
                    numArray[0] = lineWidths[i];
                    this.DrawStringLine(hdc, lineText[i].ToString(), num, textY, numArray);
                }
                num += lineWidths[i];
            }
            if (zero != IntPtr.Zero)
            {
                Win32Util.SelectObject(hdc, zero);
            }
        }

        protected virtual void DrawText(IntPtr hdc, string text, int x, int y, int[] textWidths)
        {
            if (AllowCache)
            {
                this.CachedLines ??= new List<TextLineInfo>();
                TextLineInfo item = new TextLineInfo();
                item.IsClipped = this.isCliped;
                item.Text = text;
                item.X = x;
                item.Y = y;
                item.Widths = textWidths;
                this.CachedLines.Add(item);
            }
            Win32Util.ExtTextOut(hdc, x, y, this.isCliped, this.ClipedBounds, text, textWidths);
        }

        protected int GetCharABCWidths(char ch) => 
            (this.FontCache != null) ? this.FontCache.GetCharABCWidths(ch) : 0;

        public int GetCharABCWidths(int index) => 
            this.GetCharABCWidths(this.Text[index]);

        public virtual int[] GetCharactersWidth(string text) => 
            this.FontCache.GetCharactersWidth(this.graphics, text, this.Format);

        public virtual int[] GetCharactersWidth(string text, int maxWidth, out int count) => 
            this.FontCache.GetCharactersWidth(CachedWidths, this.graphics, text, this.Format, maxWidth, out count);

        private unsafe void GetHotKeyInfo(string text, int x, int[] lineWidths, bool[] hotkeys, out string newText, out int[] newLineWidths, out int newX)
        {
            newX = x;
            newLineWidths = new int[lineWidths.Length - this.GetHotKeysCount(hotkeys, text.Length)];
            char[] chArray = new char[text.Length];
            int index = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (!hotkeys[i])
                {
                    chArray[index] = text[i];
                    newLineWidths[index++] = lineWidths[i];
                }
                else if (index == 0)
                {
                    newX += lineWidths[i];
                }
                else
                {
                    int* numPtr1 = &(newLineWidths[index - 1]);
                    numPtr1[0] += lineWidths[i];
                }
            }
            newText = new string(chArray, 0, index);
        }

        private int GetHotKeysCount(bool[] hotkeys, int length)
        {
            if (hotkeys == null)
            {
                return 0;
            }
            int num = 0;
            for (int i = 0; i < length; i++)
            {
                if (hotkeys[i])
                {
                    num++;
                }
            }
            return num;
        }

        private int[] GetLineWidths(int length) => 
            this.GetLineWidths(this.Pos, length);

        private int[] GetLineWidths(int startIndex, int length)
        {
            if ((startIndex == 0) && (length == this.Widths.Length))
            {
                return this.Widths;
            }
            int[] numArray = new int[length];
            for (int i = 0; i < length; i++)
            {
                numArray[i] = this.Widths[startIndex + i];
            }
            return numArray;
        }

        public int GetNextWord(int startIndex, int textLength)
        {
            if (this.wordBreakProvider.IsWordBreakChar(this.Text[startIndex]))
            {
                return 1;
            }
            int num = startIndex + 1;
            while ((num < textLength) && (!this.wordBreakProvider.IsWordBreakChar(this.Text[num]) && !DevExpress.Utils.Text.FontCache.IsNewLine(this.Text[num])))
            {
                num++;
            }
            return (num - startIndex);
        }

        public int GetTextWidth(int startIndex, int length, bool ignoreOverhang = false)
        {
            if (length == 0)
            {
                return 0;
            }
            int num = 0;
            for (int i = 0; i < length; i++)
            {
                num += this.widths[startIndex + i];
            }
            return (!ignoreOverhang ? (num + this.GetCharABCWidths((int) ((startIndex + length) - 1))) : num);
        }

        public void Offset(int x, int y)
        {
            this.drawBounds.Offset(x, y);
            this.clipedBounds.Offset(x, y);
            foreach (TextLineInfo info in this.CachedLines)
            {
                info.X += x;
                info.Y += y;
            }
        }

        private string ReplaceTabsWithSpaces(string st) => 
            st.Replace(DevExpress.Utils.Text.FontCache.TabStopChar, DevExpress.Utils.Text.FontCache.SpaceChar);

        public void SetGraphics(Graphics graphics)
        {
            this.graphics = graphics;
        }

        private string ValidateHotPrefix(string text)
        {
            this.hotPrefixes = null;
            if (this.format.HotkeyPrefix != HotkeyPrefix.None)
            {
                if ((this.format.HotkeyPrefix == HotkeyPrefix.Show) && !this.IsFontUnderline)
                {
                    this.hotPrefixes = new bool[text.Length];
                }
                int startIndex = 0;
                bool flag = false;
                while (startIndex < text.Length)
                {
                    if ((text[startIndex] == '&') && !flag)
                    {
                        flag = true;
                        text = text.Remove(startIndex, 1);
                        continue;
                    }
                    if ((this.hotPrefixes != null) && (text[startIndex] != '&'))
                    {
                        this.hotPrefixes[startIndex] = flag;
                    }
                    flag = false;
                    startIndex++;
                }
            }
            return text;
        }

        public DevExpress.Utils.Text.TextLines Lines =>
            this.lines;

        internal DevExpress.Utils.Text.TextLines TextLines =>
            this.lines;

        public DevExpress.Utils.Text.FontCache FontCache =>
            this.fontCache;

        public virtual int FontHeight =>
            this.FontCache.Height;

        public string Text =>
            this.text;

        public StringAlignment FormatAlignment =>
            this.formatAlignment;

        public StringFormat Format =>
            this.format;

        public StringFormatFlags FormatFlags =>
            this.formatFlags;

        public Rectangle DrawBounds
        {
            get => 
                this.drawBounds;
            set => 
                this.drawBounds = value;
        }

        public Rectangle ClipedBounds
        {
            get => 
                this.clipedBounds;
            set => 
                this.clipedBounds = value;
        }

        public int Pos
        {
            get => 
                this.pos;
            set => 
                this.pos = value;
        }

        public int[] Widths =>
            this.widths;

        public bool IsCliped =>
            this.isCliped;

        public bool MeasureTrailingSpaces =>
            this.measureTrailingSpaces;

        public int LineCount =>
            (this.lines != null) ? this.lines.Count : 1;

        public int MaxDrawWidth
        {
            get
            {
                int width = this.lines.Width;
                return ((width < this.DrawBounds.Width) ? width : this.DrawBounds.Width);
            }
        }

        public bool IsCropped { get; private set; }

        private bool IsNoWrap =>
            this.isNoWrap;

        private bool IsTrimmingElipsis =>
            this.trimming && ((this.Format.Trimming == StringTrimming.EllipsisCharacter) || ((this.Format.Trimming == StringTrimming.EllipsisPath) || (this.Format.Trimming == StringTrimming.EllipsisWord)));

        public static bool AllowCache { get; set; }

        protected List<TextLineInfo> CachedLines { get; set; }

        protected virtual bool IsFontUnderline =>
            (this.FontCache != null) ? this.FontCache.Underline : false;

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static IWordBreakProvider CustomWordBreakProvider { get; set; }

        public Color ForeColor { get; internal set; }

        public class TextLineInfo
        {
            public string Text { get; set; }

            public int X { get; set; }

            public int Y { get; set; }

            public int[] Widths { get; set; }

            public bool IsClipped { get; set; }
        }
    }
}

