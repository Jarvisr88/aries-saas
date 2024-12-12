namespace DevExpress.Office.Drawing
{
    using DevExpress.Export.Xl;
    using DevExpress.Office;
    using DevExpress.Office.PInvoke;
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security;

    [SecuritySafeCritical]
    public static class GDIHeightCalculator
    {
        [ThreadStatic]
        private static Graphics graphics;

        public static int CalcLineOffset(FontHeightMetrics fontHeightMetrics)
        {
            int num2 = Math.Max((int) (Math.Max((fontHeightMetrics.ExternalLeading - (fontHeightMetrics.ExternalLeading / 2)) - 1, 0) + fontHeightMetrics.Descent), (int) ((fontHeightMetrics.ExternalLeading / 2) + fontHeightMetrics.InternalLeading));
            int num3 = (fontHeightMetrics.Height >= 2) ? 2 : 1;
            return Math.Max((num2 + 1) / 2, num3);
        }

        private static int CalcMetricsFromScriptModeCore(FontHeightMetrics fontHeightMetrics, bool isSuperScript, PInvokeSafeNativeMethods.LOGFONT logfont)
        {
            int number = fontHeightMetrics.Height - fontHeightMetrics.InternalLeading;
            int num2 = Math.Abs(isSuperScript ? (number / 2) : (number / 5));
            logfont.lfHeight = Math.Max(MulDiv(number, 2, 3), 1);
            return num2;
        }

        public static RowHeightInfo CalcRowHeight(FontHeightMetrics fontHeightMetrics)
        {
            int num2 = Math.Max((int) (Math.Max((fontHeightMetrics.ExternalLeading - (fontHeightMetrics.ExternalLeading / 2)) - 1, 0) + fontHeightMetrics.Descent), (int) ((fontHeightMetrics.ExternalLeading / 2) + fontHeightMetrics.InternalLeading));
            int num4 = Math.Max(num2, (fontHeightMetrics.Height >= 2) ? 2 : 1);
            int emHeight = (fontHeightMetrics.Ascent - fontHeightMetrics.InternalLeading) + num4;
            return new RowHeightInfo(((fontHeightMetrics.UnderlineDelta + emHeight) + num4) + 1, emHeight);
        }

        private static void CalMetricsFromScriptMode(string fontName, XlScriptType scriptType, FontHeightMetrics fontHeightMetrics, IntPtr hDC, PInvokeSafeNativeMethods.LOGFONT logFont)
        {
            int num = CalcMetricsFromScriptModeCore(fontHeightMetrics, scriptType == XlScriptType.Superscript, logFont);
            logFont.lfHeight = -logFont.lfHeight;
            IntPtr hGdiObj = PInvokeSafeNativeMethods.CreateFont(logFont.lfHeight, logFont.lfWidth, logFont.lfEscapement, logFont.lfOrientation, logFont.lfWeight, logFont.lfItalic, logFont.lfUnderline, logFont.lfStrikeOut, (int) logFont.lfCharSet, logFont.lfOutPrecision, logFont.lfClipPrecision, logFont.lfQuality, logFont.lfPitchAndFamily, logFont.lfFaceName);
            if (hGdiObj != IntPtr.Zero)
            {
                IntPtr ptr2 = PInvokeSafeNativeMethods.SelectObject(hDC, hGdiObj);
                if (ptr2 != IntPtr.Zero)
                {
                    FontHeightMetrics metrics = GdiPlusFontInfo.GetFontHeightMetrics(hDC, fontName);
                    if (metrics == null)
                    {
                        PInvokeSafeNativeMethods.SelectObject(hDC, ptr2);
                    }
                    else
                    {
                        if (scriptType == XlScriptType.Superscript)
                        {
                            if (fontHeightMetrics.Ascent < (metrics.Ascent + num))
                            {
                                fontHeightMetrics.Ascent = metrics.Ascent + num;
                            }
                        }
                        else if (fontHeightMetrics.Descent < (metrics.Descent + num))
                        {
                            fontHeightMetrics.Descent = metrics.Descent + num;
                        }
                        fontHeightMetrics.Height = fontHeightMetrics.Ascent + fontHeightMetrics.Descent;
                        PInvokeSafeNativeMethods.DeleteObject(hGdiObj);
                    }
                }
            }
        }

        private static PInvokeSafeNativeMethods.LOGFONT GetLogFont(ShortFontInfo shortFontInfo)
        {
            PInvokeSafeNativeMethods.LOGFONT logfont = new PInvokeSafeNativeMethods.LOGFONT {
                lfFaceName = shortFontInfo.FontName,
                lfPitchAndFamily = (byte) (((byte) shortFontInfo.FontFamily) << 4),
                lfCharSet = shortFontInfo.CharSet,
                lfHeight = -MulDiv(shortFontInfo.FontSizeTwips, (int) DocumentModelDpi.DpiY, 0x5a0)
            };
            if ((shortFontInfo.CharSet == PInvokeSafeNativeMethods.LogFontCharSet.ANSI) && ((shortFontInfo.FontSizeTwips < 160) && (shortFontInfo.FontSizeTwips >= 0)))
            {
                logfont.lfFaceName = "Small Fonts";
                logfont.lfQuality = 2;
            }
            if ((shortFontInfo.FontSizeTwips >= 0) && (logfont.lfHeight > -1))
            {
                logfont.lfHeight = -1;
            }
            logfont.lfWeight = (int) shortFontInfo.FontWeight;
            logfont.lfItalic = Convert.ToByte(shortFontInfo.Italic);
            logfont.lfStrikeOut = Convert.ToByte(shortFontInfo.StrikeThrough);
            int lCID = CultureInfo.CurrentCulture.LCID;
            if ((((lCID == 0x412) || ((lCID == 0x404) || (lCID == 0x804))) && (shortFontInfo.CharSet != PInvokeSafeNativeMethods.LogFontCharSet.SHIFTJIS)) && ((shortFontInfo.CharSet != PInvokeSafeNativeMethods.LogFontCharSet.HANGEUL) && ((shortFontInfo.CharSet != PInvokeSafeNativeMethods.LogFontCharSet.CHINESEBIG5) && (shortFontInfo.CharSet != PInvokeSafeNativeMethods.LogFontCharSet.GB2312))))
            {
                logfont.lfClipPrecision = (byte) (logfont.lfClipPrecision | 40);
            }
            logfont.lfQuality = 5;
            return logfont;
        }

        public static int GetRowHeightByFont(string fontName, double fontSize, bool italic, bool bold, bool strikeThrough, XlUnderlineType underlineMode, XlScriptType scriptMode)
        {
            int rowHeight = GetRowHeightPixels(ShortFontInfo.FromParameters(fontName, fontSize, italic, scriptMode, bold, underlineMode, strikeThrough, false, false)).RowHeight;
            if (rowHeight > 0x7ff)
            {
                rowHeight = 0x7ff;
            }
            return rowHeight;
        }

        public static RowHeightInfo GetRowHeightByFontEx(string fontName, double fontSize, bool italic, bool bold, bool strikeThrough, XlUnderlineType underlineMode, XlScriptType scriptMode)
        {
            RowHeightInfo rowHeightPixels = GetRowHeightPixels(ShortFontInfo.FromParameters(fontName, fontSize, italic, scriptMode, bold, underlineMode, strikeThrough, false, false));
            if ((rowHeightPixels.RowHeight > 0x7ff) || (rowHeightPixels.EmHeight > 0x7ff))
            {
                rowHeightPixels = new RowHeightInfo(Math.Min(rowHeightPixels.RowHeight, 0x7ff), Math.Min(rowHeightPixels.EmHeight, 0x7ff));
            }
            return rowHeightPixels;
        }

        private static RowHeightInfo GetRowHeightPixels(ShortFontInfo shortFontInfo)
        {
            RowHeightInfo info = new RowHeightInfo(20);
            graphics ??= Graphics.FromHwnd(IntPtr.Zero);
            RowHeightInfo info2 = info;
            IntPtr hdc = graphics.GetHdc();
            try
            {
                PInvokeSafeNativeMethods.LOGFONT logFont = GetLogFont(shortFontInfo);
                IntPtr logFontPtr = PInvokeSafeNativeMethods.CreateFont(logFont.lfHeight, logFont.lfWidth, logFont.lfEscapement, logFont.lfOrientation, logFont.lfWeight, logFont.lfItalic, logFont.lfUnderline, logFont.lfStrikeOut, (int) logFont.lfCharSet, logFont.lfOutPrecision, logFont.lfClipPrecision, logFont.lfQuality, logFont.lfPitchAndFamily, logFont.lfFaceName);
                if (logFontPtr != IntPtr.Zero)
                {
                    FontHeightMetrics fontHeightMetrics = GetTextMetrics(hdc, logFontPtr, shortFontInfo.FontName, shortFontInfo.UnderlineMode);
                    if (fontHeightMetrics != null)
                    {
                        if (shortFontInfo.ScriptMode != XlScriptType.Baseline)
                        {
                            CalMetricsFromScriptMode(shortFontInfo.FontName, shortFontInfo.ScriptMode, fontHeightMetrics, hdc, logFont);
                        }
                        info2 = CalcRowHeight(fontHeightMetrics);
                    }
                    PInvokeSafeNativeMethods.DeleteObject(logFontPtr);
                }
            }
            finally
            {
                graphics.ReleaseHdc(hdc);
            }
            return info2;
        }

        private static FontHeightMetrics GetTextMetrics(IntPtr hdc, IntPtr logFontPtr, string fontName, XlUnderlineType underlineType)
        {
            IntPtr hGdiObj = PInvokeSafeNativeMethods.SelectObject(hdc, logFontPtr);
            if (hGdiObj == IntPtr.Zero)
            {
                return null;
            }
            FontHeightMetrics fontHeightMetrics = GdiPlusFontInfo.GetFontHeightMetrics(hdc, fontName);
            if (fontHeightMetrics == null)
            {
                return null;
            }
            if ((underlineType & ((XlUnderlineType) 0x20)) != XlUnderlineType.None)
            {
                int num = UnderLineAdvancedInfo.CalcMetricsFromUnderlineMode(hdc, fontHeightMetrics, underlineType);
                fontHeightMetrics.Descent -= num;
                fontHeightMetrics.Height -= num;
            }
            PInvokeSafeNativeMethods.SelectObject(hdc, hGdiObj);
            return fontHeightMetrics;
        }

        private static int MulDiv(int number, int numerator, int denominator) => 
            (int) (((number * numerator) + (denominator >> 1)) / ((long) denominator));

        private static class UnderLineAdvancedInfo
        {
            public static int CalcMetricsFromUnderlineMode(IntPtr hdc, FontHeightMetrics fontHeightMetrics, XlUnderlineType underlineMode)
            {
                bool flag = true;
                if ((fontHeightMetrics.PitchAndFamily & (FontPitchAndFamily.DefaultPitch | FontPitchAndFamily.Swiss)) != FontPitchAndFamily.DefaultPitch)
                {
                    flag = !RecalculateFromWinAPI(hdc);
                }
                if (flag)
                {
                    RecalculateManual(fontHeightMetrics.Descent);
                }
                int num = (int) (underlineMode & ((XlUnderlineType) 15));
                int num2 = 0;
                int number = 0;
                while (num2 < 10)
                {
                    num2++;
                    if (Width1Line < 1)
                    {
                        Width1Line = 1;
                    }
                    if (Pos1Line < 0)
                    {
                        Pos1Line = 0;
                    }
                    if (offset < 0)
                    {
                        number = 0;
                    }
                    if (Width2Lines < 1)
                    {
                        Width2Lines = 1;
                    }
                    if ((Pos1Line > 0) && ((Pos1Line + Width1Line) >= number))
                    {
                        Pos1Line /= 2;
                    }
                    int denominator = (num == 2) ? (Width2Lines + number) : (Width1Line + Pos1Line);
                    if (denominator <= fontHeightMetrics.Descent)
                    {
                        break;
                    }
                    if (!flag)
                    {
                        flag = true;
                        RecalculateManual(fontHeightMetrics.Descent);
                    }
                    else
                    {
                        if ((fontHeightMetrics.Descent >= 0) && (fontHeightMetrics.Descent <= 2))
                        {
                            Pos1Line = fontHeightMetrics.Descent - 3;
                            number = (fontHeightMetrics.Descent - 3) + 2;
                            Width2Lines = 1;
                            break;
                        }
                        Pos1Line = GDIHeightCalculator.MulDiv(Pos1Line, fontHeightMetrics.Descent, denominator);
                        Width1Line = GDIHeightCalculator.MulDiv(Width1Line, fontHeightMetrics.Descent, denominator);
                        offset = GDIHeightCalculator.MulDiv(number, fontHeightMetrics.Descent, denominator);
                        Width2Lines = GDIHeightCalculator.MulDiv(Width2Lines, fontHeightMetrics.Descent, denominator);
                    }
                }
                int num4 = (Width2Lines + number) - Pos1Line;
                fontHeightMetrics.Descent += num4;
                fontHeightMetrics.Height = fontHeightMetrics.Descent + fontHeightMetrics.Ascent;
                return num4;
            }

            private static bool RecalculateFromWinAPI(IntPtr hdc)
            {
                uint cbData = PInvokeSafeNativeMethods.GetOutlineTextMetrics(hdc, 0, IntPtr.Zero);
                if (cbData == 0)
                {
                    return false;
                }
                IntPtr ptrZero = Marshal.AllocHGlobal((int) cbData);
                if (PInvokeSafeNativeMethods.GetOutlineTextMetrics(hdc, cbData, ptrZero) == 0)
                {
                    Marshal.FreeHGlobal(ptrZero);
                    return false;
                }
                PInvokeSafeNativeMethods.OUTLINETEXTMETRIC outlinetextmetric = (PInvokeSafeNativeMethods.OUTLINETEXTMETRIC) Marshal.PtrToStructure(ptrZero, typeof(PInvokeSafeNativeMethods.OUTLINETEXTMETRIC));
                Pos1Line = Math.Abs(outlinetextmetric.otmsUnderscorePosition);
                Width2Lines = Math.Max(outlinetextmetric.otmsUnderscoreSize, 1);
                Width1Line = Width2Lines;
                offset = Pos1Line + (2 * Width2Lines);
                if ((3 * Width2Lines) < Pos1Line)
                {
                    offset = Pos1Line;
                    Pos1Line = (Pos1Line - Width2Lines) / 2;
                }
                Marshal.FreeHGlobal(ptrZero);
                return true;
            }

            private static void RecalculateManual(int descent)
            {
                Width2Lines = 1;
                if ((descent / 4) >= 1)
                {
                    Width2Lines = descent / 4;
                }
                Width1Line = Width2Lines;
                Pos1Line = ((descent - (3 * Width2Lines)) + 1) / 2;
                offset = Pos1Line + (2 * Width2Lines);
            }

            private static int offset { get; set; }

            private static int Width1Line { get; set; }

            private static int Pos1Line { get; set; }

            private static int Width2Lines { get; set; }
        }
    }
}

