namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.Layout;
    using System;
    using System.Runtime.CompilerServices;

    public class GdiFontCacheManager : FontCacheManager
    {
        public GdiFontCacheManager(DocumentLayoutUnitConverter unitConverter, bool allowCjkCorrection, bool useSystemFontQuality) : base(unitConverter, allowCjkCorrection, useSystemFontQuality)
        {
        }

        public override FontCacheManager Clone(DocumentLayoutUnitConverter unitConverter, bool allowCjkCorrection, bool useSystemFontQuality)
        {
            GdiFontCacheManager manager1 = new GdiFontCacheManager(unitConverter, allowCjkCorrection, useSystemFontQuality);
            manager1.UseGdiFontMetrics = this.UseGdiFontMetrics;
            return manager1;
        }

        public override FontCache CreateFontCache()
        {
            GdiFontCache cache1 = new GdiFontCache(base.UnitConverter, base.AllowCjkCorrection, base.UseSystemFontQuality);
            cache1.UseGdiFontMetrics = this.UseGdiFontMetrics;
            return cache1;
        }

        public bool UseGdiFontMetrics { get; set; }
    }
}

