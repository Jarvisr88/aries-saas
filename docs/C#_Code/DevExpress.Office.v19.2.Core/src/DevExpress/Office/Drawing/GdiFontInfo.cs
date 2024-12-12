namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.PInvoke;
    using System;

    public class GdiFontInfo : GdiPlusFontInfo
    {
        private const int USE_TYPO_METRICS = 0x80;
        private readonly bool useGdiFontMetrics;

        public GdiFontInfo(FontInfoMeasurer measurer, string fontName, int doubleFontSize, bool fontBold, bool fontItalic, bool fontStrikeout, bool fontUnderline, bool allowCjkCorrection, bool useSystemFontQuality, FontInfo baselineFontInfo) : base(measurer, fontName, doubleFontSize, fontBold, fontItalic, fontStrikeout, fontUnderline, allowCjkCorrection, useSystemFontQuality, baselineFontInfo)
        {
        }

        public GdiFontInfo(FontInfoMeasurer measurer, string fontName, int doubleFontSize, bool fontBold, bool fontItalic, bool fontStrikeout, bool fontUnderline, bool allowCjkCorrection, int textRotation, bool useSystemFontQuality, FontInfo baselineFontInfo) : this(measurer, fontName, doubleFontSize, fontBold, fontItalic, fontStrikeout, fontUnderline, allowCjkCorrection, textRotation, useSystemFontQuality, false, baselineFontInfo)
        {
        }

        public GdiFontInfo(FontInfoMeasurer measurer, string fontName, int doubleFontSize, bool fontBold, bool fontItalic, bool fontStrikeout, bool fontUnderline, bool allowCjkCorrection, int textRotation, bool useSystemFontQuality, bool useGdiFontMetrics, FontInfo baselineFontInfo)
        {
            this.useGdiFontMetrics = useGdiFontMetrics;
            base.Initialize(measurer, fontName, doubleFontSize, fontBold, fontItalic, fontStrikeout, fontUnderline, allowCjkCorrection, textRotation, useSystemFontQuality, baselineFontInfo);
        }

        protected internal override void CalculateFontVerticalParameters(FontInfoMeasurer measurer, bool allowCjkCorrection)
        {
            if (!this.useGdiFontMetrics)
            {
                base.CalculateFontVerticalParameters(measurer, allowCjkCorrection);
            }
            else
            {
                Win32.FontCharset charset;
                GdiPlusFontInfoMeasurer measurer2 = (GdiPlusFontInfoMeasurer) measurer;
                PInvokeSafeNativeMethods.OUTLINETEXTMETRIC? outlineTextMetrics = base.GetOutlineTextMetrics(measurer2.MeasureGraphics, out charset);
                if ((outlineTextMetrics == null) || IsHighFont(outlineTextMetrics.Value))
                {
                    base.CalculateFontVerticalParameters(measurer, allowCjkCorrection);
                }
                else
                {
                    PInvokeSafeNativeMethods.TEXTMETRIC otmTextMetrics = outlineTextMetrics.Value.otmTextMetrics;
                    base.Ascent = otmTextMetrics.tmAscent;
                    base.Descent = otmTextMetrics.tmDescent;
                    base.LineSpacing = otmTextMetrics.tmHeight + otmTextMetrics.tmExternalLeading;
                    base.CalculatedLineSpacing = base.LineSpacing;
                    base.CalculateCjkUnderlineSize(measurer);
                }
            }
        }

        private static bool IsHighFont(PInvokeSafeNativeMethods.OUTLINETEXTMETRIC otm) => 
            ((otm.otmfsSelection & 0x80) != 0) || (otm.otmTextMetrics.tmAscent > (2L * (otm.otmAscent + otm.otmLineGap)));
    }
}

