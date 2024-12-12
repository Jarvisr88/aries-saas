namespace DevExpress.Utils.Text
{
    using DevExpress.Data;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security;

    public class FontCache : IDisposable
    {
        private const int InitialCharCount = 0x400;
        private const string StringToMeasureAverageCharWidth = "ABCDEFGWabcdefgw1234567890";
        private readonly FontStyle fontStyle;
        private readonly IntPtr fontHandle;
        private readonly bool underline;
        private readonly Lazy<IntPtr> fontUnderlineHandleCore;
        private int height;
        private int ascentHeight;
        private int internalLeading;
        private int averageCharWidth;
        private int[] charsWidth;
        private int[] abcWidths;
        private char firstChar;
        private char lastChar;
        private char currentLastChar;
        private Dictionary<uint, int> kerningPairs;
        public static int MaxSingleLineChars = 0x2710;
        public static int MaxMultiLineChars = 0x2710;

        public FontCache(Graphics graphics, Font font)
        {
            if (FontGuard.IsDisposedOrInvalid(font))
            {
                font = new Font(font, font.Style);
            }
            this.fontStyle = font.Style;
            Win32Util.LOGFONT logFont = this.CreateLogFont(font);
            this.fontHandle = this.CreateGdiFont(logFont, false);
            this.underline = font.Underline;
            this.fontUnderlineHandleCore = this.underline ? new Lazy<IntPtr>(() => this.fontHandle) : new Lazy<IntPtr>(() => this.CreateGdiFont(logFont, true));
            if (!this.CalculateMetrics(graphics, logFont))
            {
                this.CalculateMetrics(graphics, font);
            }
            this.IsSymbolFont = logFont.lfCharSet == 2;
        }

        private FontCache(IntPtr hdc, FontFamily family, int height, FontStyle style = 0)
        {
            this.fontStyle = style;
            this.underline = ((style & FontStyle.Underline) == FontStyle.Underline) && family.IsStyleAvailable(FontStyle.Underline);
            Win32Util.LOGFONT lf = new Win32Util.LOGFONT {
                lfHeight = height,
                lfWeight = 400
            };
            if (((style & FontStyle.Bold) == FontStyle.Bold) && family.IsStyleAvailable(FontStyle.Bold))
            {
                lf.lfWeight = 700;
            }
            lf.lfUnderline = this.underline ? ((byte) 1) : ((byte) 0);
            bool flag = ((style & FontStyle.Italic) == FontStyle.Italic) && family.IsStyleAvailable(FontStyle.Italic);
            lf.lfItalic = this.underline ? ((byte) 1) : ((byte) 0);
            bool flag2 = ((style & FontStyle.Strikeout) == FontStyle.Strikeout) && family.IsStyleAvailable(FontStyle.Strikeout);
            lf.lfStrikeOut = this.underline ? ((byte) 1) : ((byte) 0);
            lf.lfCharSet = 1;
            lf.lfFaceName = family.Name;
            this.fontHandle = this.CreateGdiFont(lf, false);
            this.fontUnderlineHandleCore = new Lazy<IntPtr>(() => this.fontHandle);
            this.CalculateMetrics(hdc, lf);
            this.IsSymbolFont = lf.lfCharSet == 2;
        }

        [SecuritySafeCritical]
        private static FontCache BestEmHeight(IntPtr hdc, FontCache fontCache, FontFamily fontFamily, int maxHeight)
        {
            int num = (int) ((maxHeight * 0.7f) + 0.5f);
            int height = fontCache.Height;
            while (fontCache.EmHeight < num)
            {
                fontCache.Dispose();
                height += num;
                fontCache = new FontCache(hdc, fontFamily, height, FontStyle.Regular);
            }
            height = fontCache.Height;
            while ((fontCache.EmHeight > maxHeight) && (fontCache.EmHeight >= 9f))
            {
                fontCache.Dispose();
                height--;
                fontCache = new FontCache(hdc, fontFamily, height, FontStyle.Regular);
            }
            return fontCache;
        }

        private bool CalculateMetrics(Graphics graphics, Win32Util.LOGFONT logFont)
        {
            IntPtr hdc = graphics.GetHdc();
            IntPtr handle = Win32Util.SelectObject(hdc, this.FontHandle);
            DevExpress.Utils.Text.TEXTMETRIC textMetrics = Win32Util.GetTextMetrics(hdc);
            this.firstChar = textMetrics.tmFirstChar;
            this.lastChar = textMetrics.tmLastChar;
            this.internalLeading = textMetrics.tmInternalLeading;
            logFont.lfCharSet = (textMetrics.tmCharSet != 0) ? textMetrics.tmCharSet : ((byte) 1);
            bool flag = this.firstChar <= this.lastChar;
            if (flag)
            {
                this.height = textMetrics.tmHeight;
                this.ascentHeight = textMetrics.tmAscent;
                this.averageCharWidth = textMetrics.tmAveCharWidth;
                this.currentLastChar = ((this.firstChar + 'Ѐ') < this.lastChar) ? ((char) (this.firstChar + 'Ѐ')) : this.lastChar;
                this.CreateWidthAndKerningArrays(hdc);
            }
            Win32Util.SelectObject(hdc, handle);
            graphics.ReleaseHdcInternal(hdc);
            return flag;
        }

        private void CalculateMetrics(Graphics graphics, Font font)
        {
            this.firstChar = '\0';
            this.lastChar = '\0';
            this.height = font.Height;
            int cellAscent = font.FontFamily.GetCellAscent(font.Style);
            this.ascentHeight = (int) ((font.Size * cellAscent) / ((float) font.FontFamily.GetEmHeight(font.Style)));
            this.currentLastChar = ((this.firstChar + 'Ѐ') < this.lastChar) ? ((char) (this.firstChar + 'Ѐ')) : this.lastChar;
            this.averageCharWidth = (int) (graphics.MeasureString("ABCDEFGWabcdefgw1234567890", font).Width / ((float) "ABCDEFGWabcdefgw1234567890".Length));
            this.CreateWidthAndKerningArrays();
        }

        private void CalculateMetrics(IntPtr hdc, Win32Util.LOGFONT logFont)
        {
            IntPtr handle = Win32Util.SelectObject(hdc, this.fontHandle);
            DevExpress.Utils.Text.TEXTMETRIC textMetrics = Win32Util.GetTextMetrics(hdc);
            this.internalLeading = textMetrics.tmInternalLeading;
            logFont.lfCharSet = (textMetrics.tmCharSet != 0) ? textMetrics.tmCharSet : ((byte) 1);
            this.firstChar = '\0';
            this.lastChar = '\0';
            this.currentLastChar = '\0';
            this.height = textMetrics.tmHeight;
            this.ascentHeight = textMetrics.tmAscent;
            this.averageCharWidth = textMetrics.tmAveCharWidth;
            this.CreateWidthAndKerningArrays();
            Win32Util.SelectObject(hdc, handle);
        }

        [SecuritySafeCritical]
        private IntPtr CreateGdiFont(Win32Util.LOGFONT lf, bool forceUnderline = false)
        {
            byte fdwUnderline = forceUnderline ? ((byte) 1) : lf.lfUnderline;
            return Win32Util.Win32API.CreateFont(lf.lfHeight, lf.lfWidth, lf.lfEscapement, lf.lfOrientation, lf.lfWeight, lf.lfItalic, fdwUnderline, lf.lfStrikeOut, lf.lfCharSet, lf.lfOutPrecision, lf.lfClipPrecision, lf.lfQuality, lf.lfPitchAndFamily, lf.lfFaceName);
        }

        private void CreateKernings(IntPtr hdc)
        {
            Win32Util.KerningPair[] kerningPairs = Win32Util.GetKerningPairs(hdc);
            if (kerningPairs == null)
            {
                this.kerningPairs = new Dictionary<uint, int>(0);
            }
            else
            {
                this.kerningPairs = new Dictionary<uint, int>(kerningPairs.Length + 1);
                for (int i = 0; i < kerningPairs.Length; i++)
                {
                    this.kerningPairs[this.GetKerningPairHashCode(kerningPairs[i].wFirst, kerningPairs[i].wSecond)] = kerningPairs[i].iKernAmount;
                }
            }
        }

        [SecuritySafeCritical]
        private Win32Util.LOGFONT CreateLogFont(Font font)
        {
            Win32Util.LOGFONT logFont = new Win32Util.LOGFONT();
            font.ToLogFont(logFont);
            if (font.Unit != GraphicsUnit.Point)
            {
                logFont.lfHeight = (int) -font.Size;
            }
            return logFont;
        }

        private void CreateWidthAndKerningArrays()
        {
            this.charsWidth = new int[0];
            this.abcWidths = new int[0];
        }

        private void CreateWidthAndKerningArrays(IntPtr hdc)
        {
            this.charsWidth = Win32Util.GetCharWidth32(hdc, this.firstChar, this.currentLastChar);
            this.abcWidths = Win32Util.GetCharABCWidths(hdc, this.firstChar, this.currentLastChar);
        }

        public void Dispose()
        {
            this.DisposeCore();
            GC.SuppressFinalize(this);
        }

        [SecuritySafeCritical]
        protected virtual void DisposeCore()
        {
            Win32Util.Win32API.DeleteObject(this.fontHandle);
            if (this.fontUnderlineHandleCore.IsValueCreated)
            {
                Win32Util.Win32API.DeleteObject(this.fontUnderlineHandleCore.Value);
            }
        }

        [SecuritySafeCritical]
        private void DrawPreview(IntPtr hdc, string name, Color foreColor, Rectangle bounds)
        {
            int y = bounds.Y + ((bounds.Height - this.height) / 2);
            name = name.Replace(TabStopChar, SpaceChar);
            IntPtr handle = Win32Util.SelectObject(hdc, this.FontHandle);
            Win32Util.SetTextColor(hdc, foreColor);
            Win32Util.SetBkMode(hdc, 1);
            Win32Util.ExtTextOut(hdc, bounds.X + 1, y, false, Rectangle.Empty, name, name.Length, null);
            Win32Util.SelectObject(hdc, handle);
        }

        [SecuritySafeCritical]
        internal void DrawSingleLineStringSC(Graphics graphics, Color foreColor, string text, Rectangle drawBounds, StringFormat stringFormat)
        {
            TextOutDraw draw = this.PrepareSingleLineTextOut(graphics, foreColor, text, drawBounds, drawBounds, stringFormat);
            IntPtr hdc = graphics.GetHdc();
            Win32Util.SetTextColor(hdc, foreColor);
            Win32Util.SetBkMode(hdc, 1);
            draw.DrawSingleLineString(hdc);
            Win32Util.SelectObject(hdc, Win32Util.SelectObject(hdc, this.FontHandle));
            graphics.ReleaseHdcInternal(hdc);
        }

        public void DrawString(Graphics graphics, Color foreColor, TextOutDraw draw)
        {
            draw.SetGraphics(graphics);
            this.DrawStringSCCore(graphics, foreColor, draw);
        }

        public void DrawString(Graphics graphics, Color foreColor, string text, Rectangle drawBounds, Rectangle clipBounds, StringFormat stringFormat, TextHighLight highLight, IWordBreakProvider wordBreakProvider)
        {
            this.DrawStringSC(graphics, foreColor, text, drawBounds, clipBounds, stringFormat, highLight, wordBreakProvider);
        }

        [SecuritySafeCritical]
        internal void DrawStringSC(Graphics graphics, Color foreColor, string text, Rectangle drawBounds, Rectangle clipBounds, StringFormat stringFormat, TextHighLight highLight, IWordBreakProvider wordBreakProvider)
        {
            TextOutDraw.LastTextOut = this.PrepareTextOut(graphics, foreColor, text, drawBounds, clipBounds, stringFormat, highLight, wordBreakProvider);
            this.DrawStringSCCore(graphics, foreColor, TextOutDraw.LastTextOut);
        }

        [SecuritySafeCritical]
        internal void DrawStringSCCore(Graphics graphics, Color foreColor, TextOutDraw draw)
        {
            IntPtr hdc = graphics.GetHdc();
            Win32Util.SetTextColor(hdc, foreColor);
            Win32Util.SetBkMode(hdc, 1);
            draw.ForeColor = foreColor;
            draw.DrawString(hdc);
            Win32Util.SelectObject(hdc, Win32Util.SelectObject(hdc, this.FontHandle));
            graphics.ReleaseHdcInternal(hdc);
        }

        public int GetCharABCWidths(char ch)
        {
            if (this.abcWidths == null)
            {
                return 0;
            }
            if (ch < this.firstChar)
            {
                return 0;
            }
            int index = ch - this.firstChar;
            return ((this.abcWidths.Length > index) ? this.abcWidths[index] : 0);
        }

        public int[] GetCharactersWidth(Graphics graphics, string text, StringFormat stringFormat)
        {
            int count = 0;
            int[] widths = new int[text.Length];
            return this.GetCharactersWidth(widths, graphics, text, stringFormat, -1, out count);
        }

        public unsafe int[] GetCharactersWidth(int[] widths, Graphics graphics, string text, StringFormat stringFormat, int maxWidth, out int count)
        {
            this.PrepareWidthAndKerningArrays(graphics, text);
            int num = 0;
            int num2 = 0;
            count = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (this.IsFontChar(text[i]))
                {
                    widths[i] = this.charsWidth[text[i] - this.firstChar];
                    if (TextUtils.UseKerning && ((i < (text.Length - 1)) && (this.IsFontChar(text[i]) && this.IsFontChar(text[i + 1]))))
                    {
                        int* numPtr1 = &(widths[i]);
                        numPtr1[0] += this.GetKerningPairAmount(text[i], text[i + 1]);
                    }
                }
                else
                {
                    widths[i] = this.averageCharWidth;
                    if (IsTabStop(text[i]))
                    {
                        widths[i] = this.GetTabWidth(stringFormat, num2++);
                    }
                    if (IsNewLine(text[i]) || IsReturn(text[i]))
                    {
                        widths[i] = 0;
                    }
                }
                num += widths[i];
                if ((maxWidth != -1) && (num > maxWidth))
                {
                    count = i + 1;
                    return widths;
                }
            }
            count = text.Length;
            return widths;
        }

        private int GetKerningPairAmount(char wFirst, char wSecond)
        {
            int num2;
            uint kerningPairHashCode = this.GetKerningPairHashCode(wFirst, wSecond);
            return (this.kerningPairs.TryGetValue(kerningPairHashCode, out num2) ? num2 : 0);
        }

        private uint GetKerningPairHashCode(char wFirst, char wSecond) => 
            (uint) ((wFirst * 0x10000) + wSecond);

        public int[] GetMeasureString(Graphics graphics, string text, StringFormat stringFormat) => 
            this.GetCharactersWidth(graphics, this.ValidateString(text, true, this.IsMultiLine(stringFormat)), stringFormat);

        public int GetStringHeight(Graphics graphics, string text, int width, StringFormat stringFormat) => 
            new TextOutDraw(this, graphics, this.ValidateString(text, this.IsMultiLine(stringFormat)), new Rectangle(0, 0, width, 0x7fffffff), Rectangle.Empty, stringFormat, null, null).LineCount * this.Height;

        public Size GetStringSize(Graphics graphics, string text, StringFormat stringFormat)
        {
            bool flag;
            return this.GetStringSize(graphics, text, stringFormat, 0x7fffffff, 0x7fffffff, null, out flag);
        }

        public Size GetStringSize(Graphics graphics, string text, StringFormat stringFormat, int maxWidth)
        {
            bool flag;
            return this.GetStringSize(graphics, text, stringFormat, maxWidth, 0x7fffffff, null, out flag);
        }

        public Size GetStringSize(Graphics graphics, string text, StringFormat stringFormat, int maxWidth, IWordBreakProvider wordBreakProvider)
        {
            bool flag;
            return this.GetStringSize(graphics, text, stringFormat, maxWidth, 0x7fffffff, wordBreakProvider, out flag);
        }

        public Size GetStringSize(Graphics graphics, string text, StringFormat stringFormat, int maxWidth, int maxHeight)
        {
            bool flag;
            return this.GetStringSize(graphics, text, stringFormat, maxWidth, maxHeight, null, out flag);
        }

        public Size GetStringSize(Graphics graphics, string text, StringFormat stringFormat, int maxWidth, int maxHeight, IWordBreakProvider wordBreakProvider)
        {
            bool flag;
            return this.GetStringSize(graphics, text, stringFormat, maxWidth, maxHeight, wordBreakProvider, out flag);
        }

        public Size GetStringSize(Graphics graphics, string text, StringFormat stringFormat, int maxWidth, int maxHeight, out bool isCropped) => 
            this.GetStringSize(graphics, text, stringFormat, maxWidth, maxHeight, null, out isCropped);

        public Size GetStringSize(Graphics graphics, string text, StringFormat stringFormat, int maxWidth, int maxHeight, IWordBreakProvider wordBreakProvider, out bool isCropped)
        {
            isCropped = false;
            if (maxWidth <= 0)
            {
                return this.GetStringSize(graphics, text, stringFormat);
            }
            TextOutDraw draw = new TextOutDraw(this, graphics, this.ValidateString(text, this.IsMultiLine(stringFormat)), new Rectangle(0, 0, maxWidth, maxHeight), Rectangle.Empty, stringFormat, null, wordBreakProvider);
            isCropped = draw.IsCropped;
            return new Size(draw.MaxDrawWidth, draw.LineCount * this.Height);
        }

        private int GetTabWidth(StringFormat stringFormat, int tabIndex)
        {
            float firstTabOffset = 0f;
            float[] tabStops = stringFormat.GetTabStops(out firstTabOffset);
            if (tabIndex < tabStops.Length)
            {
                firstTabOffset = tabStops[tabIndex];
            }
            return ((firstTabOffset <= 0f) ? (this.charsWidth[SpaceChar - this.firstChar] * TextUtils.TabStopSpacesCount) : ((int) firstTabOffset));
        }

        private bool IsFontChar(char ch) => 
            (ch >= this.firstChar) && (ch <= this.lastChar);

        private bool IsMultiLine(StringFormat stringFormat) => 
            (stringFormat.FormatFlags & StringFormatFlags.NoWrap) == 0;

        public static bool IsNewLine(char ch) => 
            ch == NewLineChar;

        public static bool IsReturn(char ch) => 
            ch == ReturnChar;

        public static bool IsSpace(char ch) => 
            ch == SpaceChar;

        public static bool IsTabStop(char ch) => 
            ch == TabStopChar;

        private TextOutDraw PrepareSingleLineTextOut(Graphics graphics, Color foreColor, string text, Rectangle drawBounds, Rectangle clipBounds, StringFormat stringFormat) => 
            new TextOutDraw(true, this, graphics, text, drawBounds, stringFormat);

        public TextOutDraw PrepareTextOut(Graphics graphics, Color foreColor, string text, Rectangle drawBounds, Rectangle clipBounds, StringFormat stringFormat, TextHighLight highLight, IWordBreakProvider wordBreakProvider) => 
            new TextOutDraw(this, graphics, this.ValidateString(text, highLight, this.IsMultiLine(stringFormat)), drawBounds, clipBounds, stringFormat, highLight, wordBreakProvider);

        private void PrepareWidthAndKerningArrays(Graphics graphics, string text)
        {
            char currentLastChar = this.currentLastChar;
            for (int i = text.Length - 1; i >= 0; i--)
            {
                if (text[i] > currentLastChar)
                {
                    currentLastChar = text[i];
                }
            }
            if (currentLastChar > this.currentLastChar)
            {
                this.currentLastChar = ((currentLastChar + 'Ѐ') < this.lastChar) ? ((char) (currentLastChar + 'Ѐ')) : this.lastChar;
                IntPtr hdc = graphics.GetHdc();
                this.CreateWidthAndKerningArrays(hdc);
                Win32Util.SelectObject(hdc, Win32Util.SelectObject(hdc, this.FontHandle));
                graphics.ReleaseHdc(hdc);
            }
            if (TextUtils.UseKerning && (this.kerningPairs == null))
            {
                IntPtr hdc = graphics.GetHdc();
                this.CreateKernings(hdc);
                Win32Util.SelectObject(hdc, Win32Util.SelectObject(hdc, this.FontHandle));
                graphics.ReleaseHdc(hdc);
            }
        }

        public static void Preview(Graphics graphics, FontFamily family, FontStyle style, Rectangle bounds, Color color, Font defaultFont, bool isRtl)
        {
            int height = Math.Min(defaultFont.Height, bounds.Height);
            IntPtr hdc = graphics.GetHdc();
            string name = family.Name;
            using (FontCache cache = new FontCache(hdc, family, height, style))
            {
                if (cache.IsSymbolFont)
                {
                    using (FontCache cache4 = new FontCache(hdc, defaultFont.FontFamily, height, style))
                    {
                        int width = cache4.PreviewSize(hdc, name + "w").Width;
                        cache4.DrawPreview(hdc, name, color, bounds);
                        bounds.Offset(isRtl ? -width : width, 0);
                    }
                    cache.DrawPreview(hdc, name, color, bounds);
                }
                else
                {
                    try
                    {
                        using (FontCache cache2 = BestEmHeight(hdc, cache, family, height))
                        {
                            cache2.DrawPreview(hdc, name, color, bounds);
                        }
                    }
                    catch
                    {
                        using (FontCache cache3 = new FontCache(graphics, defaultFont))
                        {
                            cache3.DrawPreview(hdc, name, color, bounds);
                        }
                    }
                }
            }
            graphics.ReleaseHdcInternal(hdc);
        }

        [SecuritySafeCritical]
        private Size PreviewSize(IntPtr hdc, string name)
        {
            IntPtr handle = Win32Util.SelectObject(hdc, this.FontHandle);
            Win32Util.SIZE lpSize = new Win32Util.SIZE();
            Win32Util.Win32API.GetTextExtentPoint32(hdc, name, name.Length, out lpSize);
            Win32Util.SelectObject(hdc, handle);
            return new Size(lpSize.cx, lpSize.cy);
        }

        private string ValidateString(string text, bool isMultiLine) => 
            this.ValidateString(text, false, null, isMultiLine);

        private string ValidateString(string text, TextHighLight highLight, bool isMultiLine) => 
            this.ValidateString(text, false, highLight, isMultiLine);

        private string ValidateString(string text, bool removeReturn, bool isMultiLine) => 
            this.ValidateString(text, removeReturn, null, isMultiLine);

        private string ValidateString(string text, bool removeReturn, TextHighLight highLight, bool isMultiLine)
        {
            if (text == null)
            {
                return string.Empty;
            }
            int startIndex = 0;
            if (isMultiLine)
            {
                if (text.Length > MaxMultiLineChars)
                {
                    text = text.Substring(0, MaxMultiLineChars);
                }
            }
            else if (text.Length > MaxSingleLineChars)
            {
                text = text.Substring(0, MaxSingleLineChars);
            }
            if (removeReturn)
            {
                while (startIndex < text.Length)
                {
                    if (!IsNewLine(text[startIndex]))
                    {
                        startIndex++;
                        continue;
                    }
                    text = text.Remove(startIndex, 1);
                    if (highLight != null)
                    {
                        for (int i = 0; i < highLight.Ranges.Length; i++)
                        {
                            DisplayTextHighlightRange range = highLight.Ranges[i];
                            if (startIndex <= (range.Start + range.Length))
                            {
                                if (startIndex <= range.Start)
                                {
                                    highLight.Ranges[i].SetStart(range.Start - 1);
                                }
                                else
                                {
                                    highLight.Ranges[i].SetLength(range.Length - 1);
                                }
                            }
                        }
                    }
                }
            }
            return text;
        }

        public static char TabStopChar =>
            '\t';

        public static char NewLineChar =>
            '\n';

        public static char ReturnChar =>
            '\r';

        public static char SpaceChar =>
            ' ';

        public int Height =>
            this.height;

        public int AscentHeight =>
            this.ascentHeight;

        public int InternalLeading =>
            this.internalLeading;

        internal int EmHeight =>
            this.height - this.internalLeading;

        public bool IsItalic =>
            (this.fontStyle & FontStyle.Italic) == FontStyle.Italic;

        public bool IsSymbolFont { get; private set; }

        internal bool Underline =>
            this.underline;

        internal IntPtr FontHandle =>
            this.fontHandle;

        internal IntPtr FontUnderlineHandle =>
            this.fontUnderlineHandleCore.Value;
    }
}

