namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.Layout;
    using DevExpress.Utils.Internal;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class PrecalculatedMetricsFontCache : FontCache
    {
        private readonly bool roundMaxDigitWidth;

        public PrecalculatedMetricsFontCache(DocumentLayoutUnitConverter unitConverter, bool allowCjkCorrection) : this(unitConverter, allowCjkCorrection, false)
        {
        }

        public PrecalculatedMetricsFontCache(DocumentLayoutUnitConverter unitConverter, bool allowCjkCorrection, bool roundMaxDigitWidth) : base(unitConverter, allowCjkCorrection)
        {
            this.roundMaxDigitWidth = roundMaxDigitWidth;
        }

        protected internal virtual FontCharacterSet CreateFontCharacterSet(string fontName)
        {
            FontCharacterSet set;
            using (PrecalculatedMetricsFontInfo info = new PrecalculatedMetricsFontInfo(base.Measurer, fontName, 20, false, false, false, false, false, null))
            {
                TTFontInfo fontInfo = info.FontDescriptor.FontInfo;
                if (fontInfo == null)
                {
                    set = null;
                }
                else
                {
                    List<FontCharacterRange> fontCharacterRanges = this.GetFontCharacterRanges(info);
                    fontCharacterRanges.Add(new FontCharacterRange(0, 0xff));
                    fontCharacterRanges.Add(new FontCharacterRange(0xf000, 0xf0ff));
                    set = new FontCharacterSet(fontCharacterRanges, fontInfo.Panose);
                }
            }
            return set;
        }

        protected internal override FontInfo CreateFontInfoCore(string fontName, int doubleFontSize, bool fontBold, bool fontItalic, bool fontStrikeout, bool fontUnderline, int textRotation, FontInfo baselineFontInfo) => 
            new PrecalculatedMetricsFontInfo(base.Measurer, fontName, doubleFontSize, fontBold, fontItalic, fontStrikeout, fontUnderline, base.AllowCjkCorrection, textRotation, baselineFontInfo);

        protected internal override FontInfoMeasurer CreateFontInfoMeasurer(DocumentLayoutUnitConverter unitConverter) => 
            new PrecalculatedMetricsFontInfoMeasurer(unitConverter, this.roundMaxDigitWidth);

        protected override FontCapabilitiesInfo GetFontCapabilitiesCore(string fontName) => 
            null;

        protected override List<FontCharacterRange> GetFontCharacterRanges(FontInfo fontInfo)
        {
            List<FontCharacterRange> list = new List<FontCharacterRange>();
            TTFontInfo info2 = ((PrecalculatedMetricsFontInfo) fontInfo).FontDescriptor.FontInfo;
            if (info2 == null)
            {
                list.Add(new FontCharacterRange(0, 0x10000));
                return list;
            }
            Size[] charSegments = info2.GetCharSegments();
            int length = charSegments.Length;
            for (int i = 0; i < length; i++)
            {
                list.Add(new FontCharacterRange(charSegments[i].Width, charSegments[i].Height));
            }
            return list;
        }

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

        protected internal override void PopulateNameToCharacterSetMap()
        {
            PrecalculatedMetricsFontCache cache = this;
            lock (cache)
            {
                if (!FontCache.nameToCharacterMapPopulated)
                {
                    foreach (string str in FontManager.GetFontFamilyNames())
                    {
                        this.GetFontCharacterSet(str);
                    }
                    FontCache.nameToCharacterMapPopulated = true;
                }
            }
        }

        protected bool RoundMaxDigitWidth =>
            this.roundMaxDigitWidth;
    }
}

