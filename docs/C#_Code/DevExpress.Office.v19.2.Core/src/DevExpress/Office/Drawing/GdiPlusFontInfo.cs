namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.Layout;
    using DevExpress.Office.PInvoke;
    using DevExpress.Office.Utils;
    using DevExpress.Utils.Text;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security;

    public class GdiPlusFontInfo : FontInfo
    {
        private const long CJKCodePages = 0x3e0000L;
        private string fontName;
        private string fontFamilyName;
        private int cjkUnderlineSize;
        private int cjkUnderlinePosition;
        private bool applyCjkUnderline;
        private System.Drawing.Font font;
        private IntPtr gdiFontHandle;
        private Dictionary<char, bool> characterDrawingAbilityTable;
        private static readonly int systemFontQuality = CalculateSystemFontQuality();
        private int[] charsWidth;
        private int[] abcWidths;

        protected GdiPlusFontInfo()
        {
        }

        public GdiPlusFontInfo(FontInfoMeasurer measurer, string fontName, int doubleFontSize, bool fontBold, bool fontItalic, bool fontStrikeout, bool fontUnderline, bool allowCjkCorrection, bool useSystemFontQuality, FontInfo baselineFontInfo) : base(measurer, fontName, doubleFontSize, fontBold, fontItalic, fontStrikeout, fontUnderline, allowCjkCorrection, useSystemFontQuality, baselineFontInfo)
        {
        }

        public GdiPlusFontInfo(FontInfoMeasurer measurer, string fontName, int doubleFontSize, bool fontBold, bool fontItalic, bool fontStrikeout, bool fontUnderline, bool allowCjkCorrection, int textRotation, bool useSystemFontQuality, FontInfo baselineFontInfo) : base(measurer, fontName, doubleFontSize, fontBold, fontItalic, fontStrikeout, fontUnderline, allowCjkCorrection, textRotation, useSystemFontQuality, baselineFontInfo)
        {
        }

        private bool ApplyCjkCorrectionIfPossible(GdiPlusFontInfoMeasurer gdiPlusMeasurer, PInvokeSafeNativeMethods.OUTLINETEXTMETRIC? otm, bool allowCjkCorrection, DevExpress.Office.PInvoke.Win32.FontCharset fontCharset)
        {
            if (allowCjkCorrection && (otm != null))
            {
                long num = this.CalculateSupportedCodePagesBits(gdiPlusMeasurer.MeasureGraphics);
                if (otm != null)
                {
                    bool cjkFont = (fontCharset != DevExpress.Office.PInvoke.Win32.FontCharset.Default) && ((num & 0x3e0000L) != 0L);
                    this.CjkMetrics = this.SetCJKTextMetrics(otm.Value, cjkFont);
                    return this.CjkMetrics;
                }
            }
            return false;
        }

        protected internal static int CalculateActualFontQuality(int quality) => 
            (quality == 0) ? systemFontQuality : 0;

        protected internal virtual bool CalculateCanDrawCharacter(UnicodeRangeInfo unicodeRangeInfo, Graphics gr, char character)
        {
            UnicodeSubrange subrange = unicodeRangeInfo.LookupSubrange(character);
            return ((subrange != null) && this.CalculateSupportedUnicodeSubrangeBits(gr)[subrange.Bit]);
        }

        protected void CalculateCjkUnderlineSize(FontInfoMeasurer measurer)
        {
            this.cjkUnderlineSize = (int) Math.Ceiling((double) (((this.Font.Size / measurer.UnitConverter.FontSizeScale) * 51f) / 1024f));
        }

        protected internal override int CalculateFontCharset(FontInfoMeasurer measurer) => 
            this.CalculateFontCharsetCore(measurer);

        [SecuritySafeCritical]
        private int CalculateFontCharsetCore(FontInfoMeasurer measurer)
        {
            int fontCharset;
            Graphics measureGraphics = ((GdiPlusFontInfoMeasurer) measurer).MeasureGraphics;
            Graphics graphics2 = measureGraphics;
            lock (graphics2)
            {
                IntPtr hdc = measureGraphics.GetHdc();
                try
                {
                    IntPtr hGdiObj = DevExpress.Office.PInvoke.Win32.SelectObject(hdc, this.gdiFontHandle);
                    try
                    {
                        fontCharset = (int) DevExpress.Office.PInvoke.Win32.GetFontCharset(hdc);
                    }
                    finally
                    {
                        DevExpress.Office.PInvoke.Win32.SelectObject(hdc, hGdiObj);
                    }
                }
                finally
                {
                    measureGraphics.ReleaseHdc(hdc);
                }
            }
            return fontCharset;
        }

        protected override void CalculateFontHeightMetrics(FontInfoMeasurer measurer)
        {
            GdiPlusFontInfoMeasurer measurer2 = measurer as GdiPlusFontInfoMeasurer;
            if (measurer2 == null)
            {
                this.CalculateMetricFromFont(null);
            }
            else
            {
                Graphics measureGraphics = measurer2.MeasureGraphics;
                Graphics graphics2 = measureGraphics;
                lock (graphics2)
                {
                    bool flag2;
                    IntPtr hdc = measureGraphics.GetHdc();
                    IntPtr hGdiObj = DevExpress.Office.PInvoke.Win32.SelectObject(hdc, this.GdiFontHandle);
                    try
                    {
                        base.FontHeightMetrics = GetFontHeightMetrics(hdc, this.font.Name);
                        flag2 = (base.FontHeightMetrics != null) && (base.FontHeightMetrics.FirstChar <= base.FontHeightMetrics.LastChar);
                        bool flag1 = flag2;
                    }
                    finally
                    {
                        DevExpress.Office.PInvoke.Win32.SelectObject(hdc, hGdiObj);
                        measureGraphics.ReleaseHdc(hdc);
                    }
                    if (!flag2)
                    {
                        this.CalculateMetricFromFont(measureGraphics);
                    }
                }
            }
        }

        private static float CalculateFontSizeInLayoutUnits(System.Drawing.Font font, DocumentLayoutUnitConverter unitConverter)
        {
            switch (font.Unit)
            {
                case GraphicsUnit.Point:
                    return unitConverter.PointsToFontUnitsF(font.Size);

                case GraphicsUnit.Inch:
                    return unitConverter.InchesToFontUnitsF(font.Size);

                case GraphicsUnit.Document:
                    return unitConverter.DocumentsToFontUnitsF(font.Size);

                case GraphicsUnit.Millimeter:
                    return unitConverter.MillimetersToFontUnitsF(font.Size);
            }
            Exceptions.ThrowInternalException();
            return 0f;
        }

        protected internal override float CalculateFontSizeInPoints() => 
            FontSizeHelper.GetSizeInPoints(this.font);

        protected internal override void CalculateFontVerticalParameters(FontInfoMeasurer measurer, bool allowCjkCorrection)
        {
            DevExpress.Office.PInvoke.Win32.FontCharset charset;
            System.Drawing.Font font = this.Font;
            FontFamily fontFamily = font.FontFamily;
            FontStyle style = font.Style;
            float num3 = (font.Size / measurer.UnitConverter.FontSizeScale) / ((float) fontFamily.GetEmHeight(style));
            this.CalculateCjkUnderlineSize(measurer);
            GdiPlusFontInfoMeasurer gdiPlusMeasurer = (GdiPlusFontInfoMeasurer) measurer;
            int cellAscent = fontFamily.GetCellAscent(style);
            base.Ascent = (int) Math.Ceiling((double) (cellAscent * num3));
            base.Descent = (int) Math.Ceiling((double) (fontFamily.GetCellDescent(style) * num3));
            base.LineSpacing = (int) Math.Ceiling((double) (fontFamily.GetLineSpacing(style) * num3));
            base.CalculatedLineSpacing = base.LineSpacing;
            PInvokeSafeNativeMethods.OUTLINETEXTMETRIC? outlineTextMetrics = this.GetOutlineTextMetrics(gdiPlusMeasurer.MeasureGraphics, out charset);
            if ((outlineTextMetrics != null) && !this.ApplyCjkCorrectionIfPossible(gdiPlusMeasurer, outlineTextMetrics, allowCjkCorrection, charset))
            {
                int num6 = MulDivRound(cellAscent, (int) ((num3 * 65536f) + 0.5), 0x10000);
                this.GdiOffset = outlineTextMetrics.Value.otmTextMetrics.tmAscent - num6;
                if (this.IsFontTablePresent(gdiPlusMeasurer.MeasureGraphics, "VDMX"))
                {
                    this.GdiOffset = Math.Min(this.GdiOffset, 0);
                }
            }
        }

        protected override void CalculateMetricFromFont(Graphics graphics)
        {
            base.CalculateMetricFromFont(graphics);
            this.CreateWidthAndKerningArrays();
        }

        public void CalculateSubscriptOffset(FontInfo baseFontInfo)
        {
            int y = baseFontInfo.SubscriptOffset.Y;
            int num2 = ((baseFontInfo.LineSpacing - base.LineSpacing) + base.AscentAndFree) - baseFontInfo.AscentAndFree;
            if (y > num2)
            {
                y = num2;
            }
            base.SubscriptOffset = new Point(base.SubscriptOffset.X, y);
            base.CalculatedLineSpacing = base.LineSpacing;
        }

        public void CalculateSuperscriptOffset(FontInfo baseFontInfo)
        {
            int y = baseFontInfo.SuperscriptOffset.Y;
            int num2 = (baseFontInfo.AscentAndFree - base.AscentAndFree) + y;
            if (num2 < 0)
            {
                y -= num2;
            }
            base.SuperscriptOffset = new Point(base.SuperscriptOffset.X, y);
            base.CalculatedLineSpacing = base.LineSpacing;
        }

        protected internal virtual long CalculateSupportedCodePagesBits(Graphics gr)
        {
            DevExpress.Office.PInvoke.Win32.FONTSIGNATURE lpSig = new DevExpress.Office.PInvoke.Win32.FONTSIGNATURE();
            Graphics graphics = gr;
            lock (graphics)
            {
                IntPtr hdc = gr.GetHdc();
                try
                {
                    DevExpress.Office.PInvoke.Win32.SelectObject(hdc, this.GdiFontHandle);
                    if (DevExpress.Office.PInvoke.Win32.GetFontCharsetInfo(hdc, ref lpSig) == DevExpress.Office.PInvoke.Win32.FontCharset.Default)
                    {
                        return 0L;
                    }
                }
                finally
                {
                    gr.ReleaseHdc(hdc);
                }
            }
            int[] fsCsb = lpSig.fsCsb;
            return (fsCsb[0] + (fsCsb[1] << 0x20));
        }

        protected internal virtual BitArray CalculateSupportedUnicodeSubrangeBits(Graphics gr)
        {
            DevExpress.Office.PInvoke.Win32.FONTSIGNATURE lpSig = new DevExpress.Office.PInvoke.Win32.FONTSIGNATURE();
            Graphics graphics = gr;
            lock (graphics)
            {
                IntPtr hdc = gr.GetHdc();
                try
                {
                    DevExpress.Office.PInvoke.Win32.SelectObject(hdc, this.GdiFontHandle);
                    if (DevExpress.Office.PInvoke.Win32.GetFontCharsetInfo(hdc, ref lpSig) == DevExpress.Office.PInvoke.Win32.FontCharset.Default)
                    {
                        return new BitArray(0x80, false);
                    }
                }
                finally
                {
                    gr.ReleaseHdc(hdc);
                }
            }
            return new BitArray(lpSig.fsUsb);
        }

        private static int CalculateSystemFontQuality()
        {
            if (!OSHelper.IsWindows)
            {
                return 0;
            }
            try
            {
                int fontSmoothingType = DevExpress.Office.PInvoke.Win32.GetFontSmoothingType();
                return ((fontSmoothingType == 1) ? 4 : ((fontSmoothingType == 2) ? 6 : 0));
            }
            catch (NotSupportedException)
            {
                return 0;
            }
        }

        protected internal override void CalculateUnderlineAndStrikeoutParameters(FontInfoMeasurer measurer)
        {
            DevExpress.Office.PInvoke.Win32.FontCharset charset;
            GdiPlusFontInfoMeasurer measurer2 = (GdiPlusFontInfoMeasurer) measurer;
            PInvokeSafeNativeMethods.OUTLINETEXTMETRIC? outlineTextMetrics = this.GetOutlineTextMetrics(measurer2.MeasureGraphics, out charset);
            if (outlineTextMetrics != null)
            {
                this.CalculateUnderlineAndStrikeoutParametersCore(outlineTextMetrics.Value);
            }
            if (!ReferenceEquals(base.BaselineFontInfo, this))
            {
                this.CalculateSubscriptOffset(base.BaselineFontInfo);
                this.CalculateSuperscriptOffset(base.BaselineFontInfo);
            }
        }

        internal void CalculateUnderlineAndStrikeoutParametersCore(PInvokeSafeNativeMethods.OUTLINETEXTMETRIC otm)
        {
            if (!this.applyCjkUnderline)
            {
                base.UnderlinePosition = -otm.otmsUnderscorePosition;
                base.UnderlineThickness = otm.otmsUnderscoreSize;
            }
            else
            {
                base.UnderlinePosition = this.cjkUnderlinePosition;
                base.UnderlineThickness = this.cjkUnderlineSize;
            }
            base.StrikeoutPosition = otm.otmsStrikeoutPosition;
            base.StrikeoutThickness = (int) otm.otmsStrikeoutSize;
            base.SubscriptSize = (System.Drawing.Size) otm.otmptSubscriptSize;
            base.SubscriptOffset = (Point) otm.otmptSubscriptOffset;
            base.SuperscriptOffset = (Point) otm.otmptSuperscriptOffset;
            Point superscriptOffset = base.SuperscriptOffset;
            superscriptOffset.Y = -superscriptOffset.Y;
            base.SuperscriptOffset = superscriptOffset;
            base.SuperscriptSize = (System.Drawing.Size) otm.otmptSuperscriptSize;
        }

        public virtual bool CanDrawCharacter(UnicodeRangeInfo unicodeRangeInfo, Graphics gr, char character)
        {
            bool flag;
            if (!this.characterDrawingAbilityTable.TryGetValue(character, out flag))
            {
                flag = this.CalculateCanDrawCharacter(unicodeRangeInfo, gr, character);
                this.characterDrawingAbilityTable.Add(character, flag);
            }
            return flag;
        }

        protected internal override void CreateFont(FontInfoMeasurer measurer, string fontName, int doubleFontSize, bool fontBold, bool fontItalic, bool fontStrikeout, bool fontUnderline)
        {
            this.font = ((GdiPlusFontInfoMeasurer) measurer).CreateFont(fontName, ((float) doubleFontSize) / 2f, fontBold, fontItalic, fontStrikeout, fontUnderline);
            this.fontName = this.font.Name;
            this.fontFamilyName = this.font.FontFamily.Name;
        }

        [SecuritySafeCritical]
        public static IntPtr CreateGdiFont(System.Drawing.Font font, DocumentLayoutUnitConverter unitConverter, bool useSystemFontQuality = true)
        {
            PInvokeSafeNativeMethods.LOGFONT logFont = new PInvokeSafeNativeMethods.LOGFONT();
            font.ToLogFont(logFont);
            logFont.lfHeight = (int) -Math.Round((double) (CalculateFontSizeInLayoutUnits(font, unitConverter) / unitConverter.FontSizeScale));
            if (useSystemFontQuality)
            {
                logFont.lfQuality = (byte) CalculateActualFontQuality(logFont.lfQuality);
            }
            return PInvokeSafeNativeMethods.CreateFont(logFont.lfHeight, logFont.lfWidth, logFont.lfEscapement, logFont.lfOrientation, logFont.lfWeight, logFont.lfItalic, logFont.lfUnderline, logFont.lfStrikeOut, 1, logFont.lfOutPrecision, logFont.lfClipPrecision, logFont.lfQuality, logFont.lfPitchAndFamily, logFont.lfFaceName);
        }

        protected virtual IntPtr CreateGdiFontInLayoutUnits(FontInfoMeasurer measurer, bool useSystemFontQuality) => 
            CreateGdiFontInLayoutUnits(this.Font, measurer, base.TextRotation, useSystemFontQuality);

        [SecuritySafeCritical]
        internal static IntPtr CreateGdiFontInLayoutUnits(System.Drawing.Font font, FontInfoMeasurer measurer, int textRotation, bool useSystemFontQuality)
        {
            PInvokeSafeNativeMethods.LOGFONT logFont = new PInvokeSafeNativeMethods.LOGFONT();
            font.ToLogFont(logFont);
            logFont.lfHeight = -((int) Math.Round((double) (font.Size / measurer.UnitConverter.FontSizeScale)));
            if (useSystemFontQuality)
            {
                logFont.lfQuality = (byte) CalculateActualFontQuality(logFont.lfQuality);
            }
            logFont.lfEscapement = textRotation * 10;
            logFont.lfOrientation = textRotation * 10;
            return PInvokeSafeNativeMethods.CreateFont(logFont.lfHeight, logFont.lfWidth, logFont.lfEscapement, logFont.lfOrientation, logFont.lfWeight, logFont.lfItalic, logFont.lfUnderline, logFont.lfStrikeOut, 1, logFont.lfOutPrecision, logFont.lfClipPrecision, logFont.lfQuality, logFont.lfPitchAndFamily, logFont.lfFaceName);
        }

        private void CreateWidthAndKerningArrays()
        {
            this.charsWidth = new int[0];
            this.abcWidths = new int[0];
        }

        private void CreateWidthAndKerningArrays(IntPtr hdc)
        {
            this.CharsWidth = DevExpress.Office.PInvoke.Win32.GetCharWidth(hdc, base.FontHeightMetrics.FirstChar, base.CurrentLastChar);
            this.AbcWidths = DevExpress.Office.PInvoke.Win32.GetCharABCWidths(hdc, base.FontHeightMetrics.FirstChar, base.CurrentLastChar);
        }

        public override void Dispose()
        {
            base.Dispose();
            GC.SuppressFinalize(this);
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && (this.font != null))
                {
                    this.font.Dispose();
                    this.font = null;
                }
                if (this.gdiFontHandle != IntPtr.Zero)
                {
                    DevExpress.Office.PInvoke.Win32.DeleteObject(this.gdiFontHandle);
                    this.gdiFontHandle = IntPtr.Zero;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        ~GdiPlusFontInfo()
        {
            this.Dispose(false);
        }

        public int GetCharABCWidths(char ch)
        {
            if (this.abcWidths == null)
            {
                return 0;
            }
            if (ch < base.FontHeightMetrics.FirstChar)
            {
                return 0;
            }
            int index = ch - base.FontHeightMetrics.FirstChar;
            return ((this.abcWidths.Length > index) ? this.abcWidths[index] : 0);
        }

        public int[] GetCharactersWidth(Graphics graphics, string text, StringFormat stringFormat)
        {
            this.PrepareWidthAndKerningArrays(graphics, text);
            int[] numArray = new int[text.Length];
            int num = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (base.IsFontChar(text[i]))
                {
                    numArray[i] = this.CharsWidth[text[i] - base.FontHeightMetrics.FirstChar];
                }
                else
                {
                    numArray[i] = base.FontHeightMetrics.AverageCharWidth;
                    if (IsTabStop(text[i]))
                    {
                        numArray[i] = this.GetTabWidth(stringFormat, num++);
                    }
                    if (IsNewLine(text[i]) || IsReturn(text[i]))
                    {
                        numArray[i] = 0;
                    }
                }
            }
            return numArray;
        }

        public int[] GetCharactersWidth(IntPtr hdc, string text, StringFormat stringFormat)
        {
            this.PrepareWidthAndKerningArrays(hdc, text);
            int[] numArray = new int[text.Length];
            int num = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (base.IsFontChar(text[i]))
                {
                    numArray[i] = this.CharsWidth[text[i] - base.FontHeightMetrics.FirstChar];
                }
                else
                {
                    numArray[i] = base.FontHeightMetrics.AverageCharWidth;
                    if (IsTabStop(text[i]))
                    {
                        numArray[i] = this.GetTabWidth(stringFormat, num++);
                    }
                    if (IsNewLine(text[i]) || IsReturn(text[i]))
                    {
                        numArray[i] = 0;
                    }
                }
            }
            return numArray;
        }

        [SecuritySafeCritical]
        public static FontHeightMetrics GetFontHeightMetrics(IntPtr hdc, string fontName)
        {
            PInvokeSafeNativeMethods.TEXTMETRICUnicode unicode;
            PInvokeSafeNativeMethods.GetTextMetrics(hdc, out unicode);
            int lCID = CultureInfo.CurrentCulture.LCID;
            if ((((lCID == 0x404) || ((lCID == 0x411) || (lCID <= 0x412))) || (lCID == 0x804)) && ((fontName == "Tahoma") && ((unicode.tmMaxCharWidth / 2) >= unicode.tmAveCharWidth)))
            {
                unicode.tmAveCharWidth = unicode.tmMaxCharWidth / 2;
            }
            return GetFontHeightMetricsCore(unicode);
        }

        private static FontHeightMetrics GetFontHeightMetricsCore(PInvokeSafeNativeMethods.TEXTMETRICUnicode textmetricw) => 
            new FontHeightMetrics { 
                Height = textmetricw.tmHeight,
                Descent = textmetricw.tmDescent,
                InternalLeading = textmetricw.tmInternalLeading,
                ExternalLeading = textmetricw.tmExternalLeading,
                Ascent = textmetricw.tmAscent,
                PitchAndFamily = GetPitchAndFamily((TextMetricsPitchAndFamily) textmetricw.tmPitchAndFamily),
                FirstChar = textmetricw.tmFirstChar,
                LastChar = textmetricw.tmLastChar,
                AverageCharWidth = textmetricw.tmAveCharWidth,
                MaxCharWidth = textmetricw.tmMaxCharWidth
            };

        [SecuritySafeCritical]
        protected internal virtual List<FontCharacterRange> GetFontUnicodeRanges(Graphics gr)
        {
            List<FontCharacterRange> list2;
            Graphics graphics = gr;
            lock (graphics)
            {
                IntPtr hdc = gr.GetHdc();
                try
                {
                    DevExpress.Office.PInvoke.Win32.SelectObject(hdc, this.GdiFontHandle);
                    IntPtr lpgs = Marshal.AllocHGlobal(DevExpress.Office.PInvoke.Win32.GetFontUnicodeRanges(hdc, IntPtr.Zero));
                    try
                    {
                        PInvokeSafeNativeMethods.GetFontUnicodeRanges(hdc, lpgs);
                        List<FontCharacterRange> list = new List<FontCharacterRange>();
                        int num2 = Marshal.ReadInt32(lpgs, 12);
                        int num3 = 0;
                        while (true)
                        {
                            if (num3 >= num2)
                            {
                                list2 = list;
                                break;
                            }
                            int low = (ushort) Marshal.ReadInt16(lpgs, 0x10 + (num3 * 4));
                            int num5 = (ushort) Marshal.ReadInt16(lpgs, 0x12 + (num3 * 4));
                            list.Add(new FontCharacterRange(low, (low + num5) - 1));
                            num3++;
                        }
                    }
                    finally
                    {
                        Marshal.FreeHGlobal(lpgs);
                    }
                }
                finally
                {
                    gr.ReleaseHdc(hdc);
                }
            }
            return list2;
        }

        [SecuritySafeCritical]
        private static PInvokeSafeNativeMethods.OUTLINETEXTMETRIC? GetOutlineTextMetrics(IntPtr hdc)
        {
            uint cbData = PInvokeSafeNativeMethods.GetOutlineTextMetrics(hdc, 0, IntPtr.Zero);
            if (cbData != 0)
            {
                try
                {
                    IntPtr ptrZero = Marshal.AllocHGlobal((int) cbData);
                    try
                    {
                        if (PInvokeSafeNativeMethods.GetOutlineTextMetrics(hdc, cbData, ptrZero) != 0)
                        {
                            return new PInvokeSafeNativeMethods.OUTLINETEXTMETRIC?((PInvokeSafeNativeMethods.OUTLINETEXTMETRIC) Marshal.PtrToStructure(ptrZero, typeof(PInvokeSafeNativeMethods.OUTLINETEXTMETRIC)));
                        }
                    }
                    finally
                    {
                        Marshal.FreeHGlobal(ptrZero);
                    }
                }
                catch
                {
                }
            }
            return null;
        }

        internal PInvokeSafeNativeMethods.OUTLINETEXTMETRIC? GetOutlineTextMetrics(Graphics gr, out DevExpress.Office.PInvoke.Win32.FontCharset fontCharsetInfo)
        {
            PInvokeSafeNativeMethods.OUTLINETEXTMETRIC? outlineTextMetrics;
            Graphics graphics = gr;
            lock (graphics)
            {
                IntPtr hdc = gr.GetHdc();
                try
                {
                    IntPtr hGdiObj = DevExpress.Office.PInvoke.Win32.SelectObject(hdc, this.gdiFontHandle);
                    try
                    {
                        DevExpress.Office.PInvoke.Win32.FONTSIGNATURE lpSig = new DevExpress.Office.PInvoke.Win32.FONTSIGNATURE();
                        fontCharsetInfo = DevExpress.Office.PInvoke.Win32.GetFontCharsetInfo(hdc, ref lpSig);
                        outlineTextMetrics = GetOutlineTextMetrics(hdc);
                    }
                    finally
                    {
                        DevExpress.Office.PInvoke.Win32.SelectObject(hdc, hGdiObj);
                    }
                }
                finally
                {
                    gr.ReleaseHdc(hdc);
                }
            }
            return outlineTextMetrics;
        }

        private static FontPitchAndFamily GetPitchAndFamily(TextMetricsPitchAndFamily pitchAndFamily)
        {
            bool flag = ((int) (pitchAndFamily & TextMetricsPitchAndFamily.Device)) != 0;
            return (FontPitchAndFamily) ((byte) (((((pitchAndFamily & TextMetricsPitchAndFamily.FixedPitch) | ((byte) (2 * Convert.ToInt32((pitchAndFamily & (TextMetricsPitchAndFamily.Device | TextMetricsPitchAndFamily.Vector)) == TextMetricsPitchAndFamily.Vector)))) | ((byte) (2 * Convert.ToInt32(!flag)))) & ((TextMetricsPitchAndFamily) (-49))) | (TextMetricsPitchAndFamily.Device * ((pitchAndFamily & TextMetricsPitchAndFamily.TrueType) | TextMetricsPitchAndFamily.Vector))));
        }

        private int GetTabWidth(StringFormat stringFormat, int tabIndex)
        {
            float firstTabOffset = 0f;
            float[] tabStops = stringFormat.GetTabStops(out firstTabOffset);
            if (tabIndex < tabStops.Length)
            {
                firstTabOffset = tabStops[tabIndex];
            }
            return ((firstTabOffset <= 0f) ? (this.CharsWidth[FontInfo.SpaceChar - base.FontHeightMetrics.FirstChar] * TextUtils.TabStopSpacesCount) : ((int) firstTabOffset));
        }

        protected internal override void Initialize(FontInfoMeasurer measurer, bool useSystemFontQuality)
        {
            this.characterDrawingAbilityTable = new Dictionary<char, bool>();
            this.gdiFontHandle = this.CreateGdiFontInLayoutUnits(measurer, useSystemFontQuality);
        }

        internal bool IsFontTablePresent(Graphics gr, string fontTableName)
        {
            bool flag2;
            Graphics graphics = gr;
            lock (graphics)
            {
                IntPtr hdc = gr.GetHdc();
                try
                {
                    IntPtr hGdiObj = DevExpress.Office.PInvoke.Win32.SelectObject(hdc, this.gdiFontHandle);
                    try
                    {
                        flag2 = DevExpress.Office.PInvoke.Win32.IsFontTablePresent(hdc, fontTableName);
                    }
                    finally
                    {
                        DevExpress.Office.PInvoke.Win32.SelectObject(hdc, hGdiObj);
                    }
                }
                finally
                {
                    gr.ReleaseHdc(hdc);
                }
            }
            return flag2;
        }

        private static int MulDivRound(int value, int numerator, int denominator) => 
            (int) (((value * numerator) + (denominator / 2)) / ((long) denominator));

        protected void PrepareWidthAndKerningArrays(Graphics graphics, string text)
        {
            char currentLastChar = base.CurrentLastChar;
            for (int i = text.Length - 1; i >= 0; i--)
            {
                if (text[i] > currentLastChar)
                {
                    currentLastChar = text[i];
                }
            }
            if (currentLastChar > base.CurrentLastChar)
            {
                this.CurrentLastChar = ((currentLastChar + 'Ѐ') < base.FontHeightMetrics.LastChar) ? ((char) (currentLastChar + 'Ѐ')) : base.FontHeightMetrics.LastChar;
                Graphics graphics2 = graphics;
                lock (graphics2)
                {
                    IntPtr hdc = graphics.GetHdc();
                    try
                    {
                        this.CreateWidthAndKerningArrays(hdc);
                        DevExpress.Office.PInvoke.Win32.SelectObject(hdc, DevExpress.Office.PInvoke.Win32.SelectObject(hdc, this.GdiFontHandle));
                    }
                    finally
                    {
                        graphics.ReleaseHdc(hdc);
                    }
                }
            }
        }

        private void PrepareWidthAndKerningArrays(IntPtr hdc, string text)
        {
            char currentLastChar = base.CurrentLastChar;
            for (int i = text.Length - 1; i >= 0; i--)
            {
                if (text[i] > currentLastChar)
                {
                    currentLastChar = text[i];
                }
            }
            if (currentLastChar > base.CurrentLastChar)
            {
                this.CurrentLastChar = ((currentLastChar + 'Ѐ') < base.FontHeightMetrics.LastChar) ? ((char) (currentLastChar + 'Ѐ')) : base.FontHeightMetrics.LastChar;
                IntPtr hGdiObj = DevExpress.Office.PInvoke.Win32.SelectObject(hdc, this.GdiFontHandle);
                this.CreateWidthAndKerningArrays(hdc);
                DevExpress.Office.PInvoke.Win32.SelectObject(hdc, hGdiObj);
            }
        }

        private bool SetCJKTextMetrics(PInvokeSafeNativeMethods.OUTLINETEXTMETRIC otm, bool cjkFont)
        {
            PInvokeSafeNativeMethods.TEXTMETRIC otmTextMetrics = otm.otmTextMetrics;
            if ((otm.otmfsSelection & 0x80) != 0)
            {
                base.Ascent = otm.otmAscent;
                base.Descent = -otm.otmDescent;
                base.LineSpacing = (base.Ascent + base.Descent) + ((int) otm.otmLineGap);
                base.DrawingOffset = otm.otmTextMetrics.tmAscent - base.Ascent;
                base.UseTypoMetrics = true;
                return true;
            }
            if (!cjkFont)
            {
                return false;
            }
            cjkFont = true;
            this.cjkUnderlinePosition = (int) Math.Ceiling((double) ((base.Ascent * 1.15) + (base.Descent * 0.85)));
            int num = (int) Math.Ceiling((double) (1.3 * (base.Ascent + base.Descent)));
            int num2 = num - (base.Ascent + base.Descent);
            int num3 = num2 / 2;
            int num4 = num2 - num3;
            base.Ascent += num3;
            base.Descent += num4;
            base.LineSpacing = num;
            this.cjkUnderlinePosition -= base.Ascent;
            this.applyCjkUnderline = true;
            int num5 = base.LineSpacing - base.Descent;
            base.DrawingOffset = otm.otmTextMetrics.tmAscent - num5;
            return true;
        }

        public IntPtr GdiFontHandle
        {
            get => 
                this.gdiFontHandle;
            set => 
                this.gdiFontHandle = value;
        }

        public override bool Bold =>
            this.font.Bold;

        public override bool Italic =>
            this.font.Italic;

        public override bool Underline =>
            this.font.Underline;

        public override bool Strikeout =>
            this.font.Strikeout;

        public override float Size =>
            this.font.Size;

        public override string Name =>
            this.fontName;

        public override string FontFamilyName =>
            this.fontFamilyName;

        public override System.Drawing.Font Font =>
            this.font;

        public int GdiOffset { get; set; }

        public bool CjkMetrics { get; set; }

        private int[] CharsWidth
        {
            get => 
                this.charsWidth;
            set => 
                this.charsWidth = value;
        }

        private int[] AbcWidths
        {
            get => 
                this.abcWidths;
            set => 
                this.abcWidths = value;
        }

        private enum CodePageBitField
        {
            Latin1 = 0,
            Latin2 = 1,
            Cyrillic = 2,
            Greek = 3,
            TurkishAnsi = 4,
            Hebrew = 5,
            ArabicAnsi = 6,
            Baltic = 7,
            VietnameseAnsi = 8,
            Thai = 0x10,
            Japanese = 0x11,
            ShiftJIS = 0x11,
            SimplifiedChinese = 0x12,
            Singapore = 0x12,
            KoreanUnifiedHangulCode = 0x13,
            HangulTongHabHyungCode = 0x13,
            TraditionalChinese = 20,
            Taiwan = 20,
            HongKongSAR = 20,
            Korean = 0x15,
            Johab = 0x15,
            Vietnamese2 = 0x2f,
            ModernGreek = 0x30,
            Russian = 0x31,
            Nordic = 50,
            ArabicOem = 0x33,
            CanadianFrench = 0x34,
            Noname = 0x35,
            Icelandic = 0x36,
            Portuguese = 0x37,
            TurkishOem = 0x38,
            CyrillicPrimarilyRussian = 0x39,
            Latin2Oem = 0x3a,
            BalticOem = 0x3b,
            Greek437G = 60,
            ArabicASMO708 = 0x3d,
            MultilingualLatin1 = 0x3e,
            US = 0x3f
        }
    }
}

