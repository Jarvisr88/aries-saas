namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.PInvoke;
    using DevExpress.Office.Utils;
    using DevExpress.Utils.Text;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class OfficeTextOutDraw : IWordBreakProvider
    {
        private readonly GdiPlusFontInfo fontInfo;
        private string text;
        private Rectangle drawBounds;
        private Rectangle rotatedBounds;
        private Rectangle clipedBounds;
        private int[] widths;
        private bool[] hotPrefixes;
        private StringFormat format;
        private StringAlignment formatAlignment;
        private StringFormatFlags formatFlags;
        private TextHighLight highLight;
        private int pos;
        private int drawTop;
        private int linepos;
        private bool isCliped;
        private bool measureTrailingSpaces;
        private bool isNoWrap;
        private bool trimming;
        private DevExpress.Office.Drawing.TextLines lines;
        private readonly IWordBreakProvider wordBreakProvider;
        private readonly bool textIsRotated;

        public OfficeTextOutDraw(GdiPlusFontInfo fontInfo, IntPtr hdc, string text, Rectangle drawBounds, Rectangle clipBounds, StringFormat format, TextHighLight highLight, IWordBreakProvider provider)
        {
            this.fontInfo = fontInfo;
            this.format = format;
            this.formatAlignment = this.CalcAlignment(format.Alignment, (format.FormatFlags & StringFormatFlags.DirectionRightToLeft) != 0);
            this.formatFlags = format.FormatFlags;
            this.highLight = highLight;
            this.wordBreakProvider = (provider != null) ? provider : this;
            this.text = text;
            this.textIsRotated = RotatedTextUtils.IsRotated(fontInfo.TextRotation);
            this.drawBounds = this.ApplyTextUtilsOffsets(drawBounds);
            this.rotatedBounds = this.PrepareRotatedBounds(drawBounds, fontInfo.TextRotation);
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
            this.widths = fontInfo.GetCharactersWidth(hdc, this.Text, format);
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
            if ((this.Pos < this.Text.Length) && DevExpress.Office.Drawing.FontInfo.IsNewLine(this.Text[this.Pos]))
            {
                this.pos++;
            }
            this.lines.Add(this.linepos, length);
            if (this.trimming)
            {
                this.lines[this.lines.Count - 1].UpdateTrimmingLine(this.Format.Trimming, this.rotatedBounds.Width);
            }
            this.linepos = this.Pos;
            this.drawTop += this.LineSpacing;
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
            rightToLeft ? this.GetOppositeAlignment(stringAlignment) : stringAlignment;

        private PInvokeSafeNativeMethods.TextAlignment CalculateHorizontalTextAlign(StringFormat stringFormat)
        {
            switch (stringFormat.Alignment)
            {
                case StringAlignment.Center:
                    return PInvokeSafeNativeMethods.TextAlignment.TA_CENTER;

                case StringAlignment.Far:
                    return PInvokeSafeNativeMethods.TextAlignment.TA_RIGHT;
            }
            return PInvokeSafeNativeMethods.TextAlignment.TA_LEFT;
        }

        private int CalculateRotationOffset(bool positiveAngle, double dx, StringFormat format)
        {
            StringAlignment alignment = format.Alignment;
            if (alignment == StringAlignment.Far)
            {
                positiveAngle = !positiveAngle;
            }
            return ((alignment != StringAlignment.Center) ? (positiveAngle ? 0 : ((int) (dx * (this.lines.Count - 1)))) : (((int) (dx * (this.lines.Count - 1))) / 2));
        }

        private int CalculateVerticalRotationOffset(bool positiveAngle, double dx)
        {
            int lineOffset = (int) (dx * this.lines.Count);
            return (this.CalculateVerticalRotationOffsetCore(lineOffset, positiveAngle) + ((this.Format.LineAlignment == StringAlignment.Far) ? ((int) dx) : 0));
        }

        private int CalculateVerticalRotationOffsetCore(int lineOffset, bool positiveAngle)
        {
            StringAlignment alignment = this.Format.Alignment;
            return ((alignment != StringAlignment.Near) ? ((alignment != StringAlignment.Center) ? (this.DrawBounds.Right + (positiveAngle ? -lineOffset : 0)) : (((this.DrawBounds.Left + this.DrawBounds.Right) / 2) - (lineOffset / 2))) : (this.DrawBounds.Left + (positiveAngle ? 0 : -lineOffset)));
        }

        private PInvokeSafeNativeMethods.TextAlignment CalculateVerticalTextAlign(StringFormat stringFormat)
        {
            switch (stringFormat.LineAlignment)
            {
                case StringAlignment.Center:
                    return PInvokeSafeNativeMethods.TextAlignment.TA_LEFT;

                case StringAlignment.Far:
                    return PInvokeSafeNativeMethods.TextAlignment.TA_BOTTOM;
            }
            return PInvokeSafeNativeMethods.TextAlignment.TA_LEFT;
        }

        private bool CanAddOneMoreLine(int lineTop) => 
            (this.trimming || !this.IsNoWrap) ? (!this.WholeLinesOnly ? ((this.drawTop < this.DrawBounds.Height) || ((this.lines.Count == 0) || this.textIsRotated)) : (((this.drawTop + this.LineSpacing) <= this.DrawBounds.Height) || ((this.lines.Count == 0) || this.textIsRotated))) : (this.drawTop < this.DrawBounds.Height);

        private bool CanRemoveSymbol(int pos) => 
            char.IsWhiteSpace(this.Text[pos]) && (!DevExpress.Office.Drawing.FontInfo.IsNewLine(this.Text[pos]) && ((this.hotPrefixes == null) || !this.hotPrefixes[pos]));

        private unsafe Rectangle CorrectRectangleLeft(Rectangle bounds, DevExpress.Office.Drawing.TextLine line)
        {
            if (this.FormatAlignment != StringAlignment.Near)
            {
                int lineWidth = this.GetLineWidth(line);
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

        private unsafe Rectangle CorrectRectangleTop(Rectangle bounds, int height)
        {
            if (((bounds.Height >= height) || !this.trimming) && (bounds.Height != this.LineSpacing))
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

        private unsafe Rectangle CorrectRectangleTopForRotatedText(Rectangle bounds, int height)
        {
            if (((bounds.Height >= height) || !this.trimming) && (bounds.Height != this.LineSpacing))
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
                        rectanglePtr1.Y += bounds.Height - this.LineSpacing;
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
            this.lines = new DevExpress.Office.Drawing.TextLines(null);
            if (!this.IsNoWrap)
            {
                this.CreateWrapLines();
            }
            else if (this.Text.IndexOf(DevExpress.Office.Drawing.FontInfo.NewLineChar) > -1)
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
            this.lines[0].UpdateTrimmingLine(this.Format.Trimming, this.DrawBounds.Width);
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
                if (!DevExpress.Office.Drawing.FontInfo.IsNewLine(this.Text[this.Pos]))
                {
                    if (this.Text[this.Pos] != DevExpress.Office.Drawing.FontInfo.ReturnChar)
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
            char[] trimChars = new char[] { DevExpress.Office.Drawing.FontInfo.ReturnChar, DevExpress.Office.Drawing.FontInfo.NewLineChar };
            string str = this.Text.TrimEnd(trimChars);
            if (this.Pos < (str.Length - 1))
            {
                this.IsCropped = true;
            }
            this.pos = 0;
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
                    if ((this.Pos < this.Text.Length) && DevExpress.Office.Drawing.FontInfo.IsNewLine(this.Text[this.Pos]))
                    {
                        this.AddNewLineToList();
                        continue;
                    }
                    if ((this.Pos >= this.Text.Length) || (!this.CanAddOneMoreLine(this.drawTop) || (!this.textIsRotated && (this.widths[this.Pos] > this.rotatedBounds.Width))))
                    {
                        break;
                    }
                    else
                    {
                        int nextWord = this.GetNextWord(this.Pos, this.Text.Length);
                        if (this.GetTextWidth(this.linepos, (this.pos - this.linepos) + nextWord) <= this.rotatedBounds.Width)
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
                                    if (((num3 + this.widths[this.Pos + num4]) + this.GetCharABCWidths((int) (this.Pos + num4))) <= this.rotatedBounds.Width)
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
                    this.lines[this.lines.Count - 1].UpdateTrimmingLine(this.Format.Trimming, this.rotatedBounds.Width, true, this.Text);
                }
                this.IsCropped = true;
            }
            this.pos = 0;
        }

        bool IWordBreakProvider.IsWordBreakChar(char ch) => 
            char.IsWhiteSpace(ch);

        private unsafe void DrawRotatedStringLine(IntPtr hdc, DevExpress.Office.Drawing.TextLine line, Rectangle bounds)
        {
            this.DrawStringLine(hdc, line.Position, line.Length, bounds.X, bounds.Y, this.highLight);
            if (line.HasElipsis)
            {
                this.DrawText(hdc, line.ElipsisText, bounds.X + line.TextWidth, bounds.Y, line.ElipsisWidths);
                Rectangle* rectanglePtr1 = &bounds;
                rectanglePtr1.X += line.ElipsisWidth;
            }
            if (line.Length2 > 0)
            {
                this.DrawStringLine(hdc, line.Position2, line.Length2, bounds.X + line.TextWidth, bounds.Y, this.highLight);
            }
        }

        private unsafe void DrawRotatedStringLines(IntPtr hdc)
        {
            if (this.lines.Count > 0)
            {
                int textRotation = this.FontInfo.TextRotation;
                this.isCliped |= this.rotatedBounds.Height < (this.LineSpacing * this.lines.Count);
                double a = RotatedTextUtils.ConvertDegreesToRadian(textRotation);
                double dx = ((double) this.LineSpacing) / Math.Sin(a);
                bool flag = Math.Abs(textRotation) == 90;
                bool positiveAngle = textRotation > 0;
                StringFormat stringFormat = flag ? this.GetStringFormatForVerticalText(positiveAngle) : this.Format;
                Size nonRotatedSize = new Size(this.GetMaxLineWidth(), this.LineSpacing);
                Point point = RotatedTextUtils.GetRotatedTextPosition(this.DrawBounds, nonRotatedSize, textRotation, stringFormat);
                if (flag)
                {
                    point.X = this.CalculateVerticalRotationOffset(positiveAngle, dx);
                }
                else
                {
                    Point* pointPtr1 = &point;
                    pointPtr1.X -= this.CalculateRotationOffset(positiveAngle, dx, stringFormat);
                }
                int num4 = this.SetTextAlign(hdc, stringFormat);
                try
                {
                    for (int i = 0; i < this.lines.Count; i++)
                    {
                        Rectangle bounds = new Rectangle(point.X + ((int) (dx * i)), point.Y, this.rotatedBounds.Width, this.LineSpacing);
                        this.DrawRotatedStringLine(hdc, this.lines[i], bounds);
                    }
                }
                finally
                {
                    this.SetTextAlign(hdc, num4);
                }
            }
        }

        public void DrawString(IntPtr hdc)
        {
            if (this.Text.Length != 0)
            {
                this.DrawStringLines(hdc);
            }
        }

        private unsafe void DrawStringLine(IntPtr hdc, DevExpress.Office.Drawing.TextLine line, Rectangle bounds)
        {
            bounds = this.CorrectRectangleLeft(bounds, line);
            this.DrawStringLine(hdc, line.Position, line.Length, bounds.X, bounds.Y + this.LineOffset, this.highLight);
            if (line.HasElipsis)
            {
                this.DrawText(hdc, line.ElipsisText, bounds.X + line.TextWidth, bounds.Y + this.LineOffset, line.ElipsisWidths);
                Rectangle* rectanglePtr1 = &bounds;
                rectanglePtr1.X += line.ElipsisWidth;
            }
            if (line.Length2 > 0)
            {
                this.DrawStringLine(hdc, line.Position2, line.Length2, bounds.X + line.TextWidth, bounds.Y + this.LineOffset, this.highLight);
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
            this.DrawStringLine(hdc, startPos, length, x, y);
        }

        private void DrawStringLine(IntPtr hdc, ref int startPos, ref int length, ref int x, int y, int highLightLen)
        {
            this.DrawStringLine(hdc, startPos, highLightLen, x, y);
            x += this.GetTextWidth(startPos, highLightLen);
            startPos += highLightLen;
            length -= highLightLen;
        }

        private void DrawStringLine(IntPtr hdc, string lineText, int x, int y, int[] lineWidths, bool[] hotkeys)
        {
            this.DrawStringLine(hdc, lineText, x, y, lineWidths);
        }

        private void DrawStringLines(IntPtr hdc)
        {
            if (this.textIsRotated)
            {
                this.DrawRotatedStringLines(hdc);
            }
            else
            {
                this.isCliped |= this.rotatedBounds.Height < (this.LineSpacing * this.lines.Count);
                this.rotatedBounds = this.CorrectRectangleTop(this.rotatedBounds, this.LineSpacing * this.lines.Count);
                for (int i = 0; i < this.lines.Count; i++)
                {
                    Rectangle bounds = new Rectangle(this.rotatedBounds.X, this.rotatedBounds.Y + (i * this.LineSpacing), this.rotatedBounds.Width, this.LineSpacing);
                    this.DrawStringLine(hdc, this.lines[i], bounds);
                }
            }
        }

        protected virtual void DrawText(IntPtr hdc, string text, int x, int y, int[] textWidths)
        {
            Win32.EtoFlags options = this.isCliped ? Win32.EtoFlags.ETO_CLIPPED : Win32.EtoFlags.ETO_NONE;
            Win32.RECT clip = Win32.RECT.FromRectangle(this.ClipedBounds);
            Win32.ExtTextOut(hdc, x, y, options, ref clip, text, text.Length, textWidths);
        }

        protected int GetCharABCWidths(char ch) => 
            (this.FontInfo != null) ? this.FontInfo.GetCharABCWidths(ch) : 0;

        public int GetCharABCWidths(int index) => 
            this.GetCharABCWidths(this.Text[index]);

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

        private int GetLineWidth(DevExpress.Office.Drawing.TextLine line) => 
            (this.GetTextWidth(line.Position, line.Length) + line.ElipsisWidth) + this.GetTextWidth(line.Position2, line.Length2);

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

        private int GetMaxLineWidth()
        {
            int num = -2147483648;
            for (int i = 0; i < this.lines.Count; i++)
            {
                num = Math.Max(num, this.GetLineWidth(this.lines[i]));
            }
            return num;
        }

        public int GetNextWord(int startIndex, int textLength)
        {
            if (this.wordBreakProvider.IsWordBreakChar(this.Text[startIndex]))
            {
                return 1;
            }
            int num = startIndex + 1;
            while ((num < textLength) && (!this.wordBreakProvider.IsWordBreakChar(this.Text[num]) && !DevExpress.Office.Drawing.FontInfo.IsNewLine(this.Text[num])))
            {
                num++;
            }
            return (num - startIndex);
        }

        private StringAlignment GetOppositeAlignment(StringAlignment alignment) => 
            (alignment != StringAlignment.Near) ? ((alignment != StringAlignment.Far) ? StringAlignment.Center : StringAlignment.Near) : StringAlignment.Far;

        private StringFormat GetStringFormatForVerticalText(bool positiveAngle)
        {
            StringFormat format = (StringFormat) this.Format.Clone();
            StringAlignment lineAlignment = format.LineAlignment;
            format.Alignment = positiveAngle ? this.GetOppositeAlignment(lineAlignment) : lineAlignment;
            return format;
        }

        public int GetTextWidth(int startIndex, int length)
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
            return (num + this.GetCharABCWidths((int) ((startIndex + length) - 1)));
        }

        private Rectangle PrepareRotatedBounds(Rectangle drawBounds, int textRotation)
        {
            if (!RotatedTextUtils.IsRotated(textRotation))
            {
                return drawBounds;
            }
            Rectangle rectangle = drawBounds;
            rectangle.Width = RotatedTextUtils.GetMaxRotatedWidth(this.FontHeight, drawBounds.Height, textRotation);
            return rectangle;
        }

        private string ReplaceTabsWithSpaces(string st) => 
            st.Replace(DevExpress.Office.Drawing.FontInfo.TabStopChar, DevExpress.Office.Drawing.FontInfo.SpaceChar);

        private int SetTextAlign(IntPtr hdc, StringFormat stringFormat) => 
            this.SetTextAlign(hdc, (int) (this.CalculateHorizontalTextAlign(stringFormat) | this.CalculateVerticalTextAlign(stringFormat)));

        private int SetTextAlign(IntPtr hdc, int value) => 
            Win32.SetTextAlign(hdc, value);

        internal DevExpress.Office.Drawing.TextLines TextLines =>
            this.lines;

        public string Text =>
            this.text;

        public StringAlignment FormatAlignment =>
            this.formatAlignment;

        public StringFormat Format =>
            this.format;

        public StringFormatFlags FormatFlags =>
            this.formatFlags;

        public Rectangle DrawBounds =>
            this.drawBounds;

        public Rectangle ClipedBounds =>
            this.clipedBounds;

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

        public GdiPlusFontInfo FontInfo =>
            this.fontInfo;

        public virtual int FontHeight =>
            this.FontInfo.FontHeightMetrics.Height;

        public virtual int LineSpacing =>
            this.FontHeight;

        public virtual int LineOffset =>
            0;

        public int LineCount =>
            (this.lines != null) ? this.lines.Count : 1;

        public bool IsCropped { get; private set; }

        private bool IsNoWrap =>
            this.isNoWrap;

        protected virtual bool WholeLinesOnly =>
            true;

        private bool IsTrimmingElipsis =>
            this.trimming && ((this.Format.Trimming == StringTrimming.EllipsisCharacter) || ((this.Format.Trimming == StringTrimming.EllipsisPath) || (this.Format.Trimming == StringTrimming.EllipsisWord)));

        public int MaxDrawWidth
        {
            get
            {
                int linesWidth = this.LinesWidth;
                return ((linesWidth < this.DrawBounds.Width) ? linesWidth : this.DrawBounds.Width);
            }
        }

        public int LinesWidth
        {
            get
            {
                int num = 0;
                for (int i = 0; i < this.TextLines.Count; i++)
                {
                    int lineWidth = this.GetLineWidth(this.TextLines[i]);
                    if (num < lineWidth)
                    {
                        num = lineWidth;
                    }
                }
                return num;
            }
        }

        protected virtual bool IsFontUnderline =>
            (this.FontInfo != null) ? this.FontInfo.Underline : false;
    }
}

