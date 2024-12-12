namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.Layout;
    using System;

    public class GdiPlusFontCacheManager : FontCacheManager
    {
        public GdiPlusFontCacheManager(DocumentLayoutUnitConverter unitConverter, bool allowCjkCorrection, bool useSystemFontQuality) : base(unitConverter, allowCjkCorrection, useSystemFontQuality)
        {
        }

        public override FontCacheManager Clone(DocumentLayoutUnitConverter unitConverter, bool allowCjkCorrection, bool useSystemFontQuality) => 
            new GdiPlusFontCacheManager(unitConverter, allowCjkCorrection, useSystemFontQuality);

        public override FontCache CreateFontCache() => 
            new GdiPlusFontCache(base.UnitConverter, base.AllowCjkCorrection, base.UseSystemFontQuality);
    }
}

