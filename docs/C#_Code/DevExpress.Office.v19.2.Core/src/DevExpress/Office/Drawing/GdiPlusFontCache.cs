namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.Layout;
    using DevExpress.Office.PInvoke;
    using DevExpress.Office.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;

    public class GdiPlusFontCache : FontCache
    {
        private readonly UnicodeRangeInfo unicodeRangeInfo;
        private readonly bool useSystemFontQuality;

        public GdiPlusFontCache(DocumentLayoutUnitConverter unitConverter, bool allowCjkCorrection, bool useSystemFontQuality) : base(unitConverter, allowCjkCorrection)
        {
            this.unicodeRangeInfo = new UnicodeRangeInfo();
            this.useSystemFontQuality = useSystemFontQuality;
        }

        protected internal virtual FontCharacterSet CreateFontCharacterSet(string fontName)
        {
            FontCharacterSet set;
            using (GdiPlusFontInfo info = (GdiPlusFontInfo) this.CreateFontInfoCore(fontName, 20, false, false, false, false, 0, null))
            {
                byte[] panose = this.GetPanose(info);
                if (panose == null)
                {
                    set = null;
                }
                else
                {
                    List<FontCharacterRange> fontCharacterRanges = this.GetFontCharacterRanges(info);
                    fontCharacterRanges.Add(new FontCharacterRange(0, 0xff));
                    fontCharacterRanges.Add(new FontCharacterRange(0xf000, 0xf0ff));
                    set = new FontCharacterSet(fontCharacterRanges, panose);
                }
            }
            return set;
        }

        protected internal override FontInfo CreateFontInfoCore(string fontName, int doubleFontSize, bool fontBold, bool fontItalic, bool fontStrikeout, bool fontUnderline, int textRotation, FontInfo baselineFontInfo) => 
            new GdiPlusFontInfo(base.Measurer, fontName, doubleFontSize, fontBold, fontItalic, fontStrikeout, fontUnderline, base.AllowCjkCorrection, textRotation, this.useSystemFontQuality, baselineFontInfo);

        protected internal override FontInfoMeasurer CreateFontInfoMeasurer(DocumentLayoutUnitConverter unitConverter) => 
            new GdiPlusFontInfoMeasurer(unitConverter);

        protected override FontCapabilitiesInfo GetFontCapabilitiesCore(string fontName)
        {
            FontCapabilitiesInfo info4;
            try
            {
                FontInfo info = base.CreateFontInfo(fontName, 20, false, false, false, false);
                GdiPlusFontInfo info2 = info as GdiPlusFontInfo;
                if (info2 == null)
                {
                    info4 = null;
                }
                else
                {
                    BitArray array = info2.CalculateSupportedUnicodeSubrangeBits(((GdiPlusFontInfoMeasurer) base.Measurer).MeasureGraphics);
                    bool flag = (info != null) && (string.Compare(info.Name, fontName, StringComparison.InvariantCultureIgnoreCase) != 0);
                    info4 = new FontCapabilitiesInfo {
                        IsSubstituted = string.Compare(info.Name, fontName, StringComparison.InvariantCultureIgnoreCase) != 0,
                        IsComplexScript = array.Get(11) || array.Get(13),
                        IsCjk = (array.Get(0x30) || (array.Get(0x36) || (array.Get(0x37) || (array.Get(0x3b) || array.Get(0x3d))))) || array.Get(0x41)
                    };
                }
            }
            catch
            {
                info4 = null;
            }
            return info4;
        }

        protected override List<FontCharacterRange> GetFontCharacterRanges(FontInfo fontInfo) => 
            ((GdiPlusFontInfo) fontInfo).GetFontUnicodeRanges(((GdiPlusFontInfoMeasurer) base.Measurer).MeasureGraphics);

        public override FontCharacterSet GetFontCharacterSet(string fontName)
        {
            FontCharacterSet set2;
            Dictionary<string, FontCharacterSet> nameToCharacterSetMap = this.NameToCharacterSetMap;
            lock (nameToCharacterSetMap)
            {
                FontCharacterSet set;
                if (this.NameToCharacterSetMap.TryGetValue(fontName, out set))
                {
                    set2 = set;
                }
                else
                {
                    set = this.CreateFontCharacterSet(fontName);
                    if (set != null)
                    {
                        this.NameToCharacterSetMap.Add(fontName, set);
                    }
                    set2 = set;
                }
            }
            return set2;
        }

        protected virtual byte[] GetPanose(GdiPlusFontInfo fontInfo)
        {
            Win32.FontCharset charset;
            GdiPlusFontInfoMeasurer measurer = (GdiPlusFontInfoMeasurer) base.Measurer;
            PInvokeSafeNativeMethods.OUTLINETEXTMETRIC? outlineTextMetrics = fontInfo.GetOutlineTextMetrics(measurer.MeasureGraphics, out charset);
            return ((outlineTextMetrics != null) ? outlineTextMetrics.Value.otmPanoseNumber.ToByteArray() : null);
        }

        protected internal override void PopulateNameToCharacterSetMap()
        {
            GdiPlusFontCache cache = this;
            lock (cache)
            {
                if (!FontCache.nameToCharacterMapPopulated)
                {
                    FontFamily[] families = FontFamily.Families;
                    int length = families.Length;
                    int index = 0;
                    while (true)
                    {
                        if (index >= length)
                        {
                            FontCache.nameToCharacterMapPopulated = true;
                            break;
                        }
                        FontFamily family = families[index];
                        if (family.IsStyleAvailable(FontStyle.Regular))
                        {
                            this.GetFontCharacterSet(family.Name);
                        }
                        index++;
                    }
                }
            }
        }

        public override bool ShouldUseDefaultFontToDrawInvisibleCharacter(FontInfo fontInfo, char character)
        {
            GdiPlusFontInfoMeasurer measurer = (GdiPlusFontInfoMeasurer) base.Measurer;
            return ((GdiPlusFontInfo) fontInfo).CanDrawCharacter(this.unicodeRangeInfo, measurer.MeasureGraphics, character);
        }

        protected bool UseSystemFontQuality =>
            this.useSystemFontQuality;
    }
}

