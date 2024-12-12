namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.Layout;
    using DevExpress.Utils;
    using System;

    public abstract class FontCacheManager
    {
        private readonly DocumentLayoutUnitConverter unitConverter;
        private readonly bool allowCjkCorrection;
        private readonly bool useSystemFontQuality;

        protected FontCacheManager(DocumentLayoutUnitConverter unitConverter, bool allowCjkCorrection, bool useSystemFontQuality)
        {
            Guard.ArgumentNotNull(unitConverter, "unitConverter");
            this.unitConverter = unitConverter;
            this.allowCjkCorrection = allowCjkCorrection;
            this.useSystemFontQuality = useSystemFontQuality;
        }

        public abstract FontCacheManager Clone(DocumentLayoutUnitConverter unitConverter, bool allowCjkCorrection, bool useSystemFontQuality);
        public static FontCacheManager CreateDefault(DocumentLayoutUnitConverter unitConverter, bool allowCjkCorrection, bool useSystemFontQuality) => 
            !PrecalculatedMetricsFontCacheManager.ShouldUse() ? ((FontCacheManager) new GdiPlusFontCacheManager(unitConverter, allowCjkCorrection, useSystemFontQuality)) : ((FontCacheManager) new PrecalculatedMetricsFontCacheManager(unitConverter, allowCjkCorrection, useSystemFontQuality));

        public abstract FontCache CreateFontCache();
        public virtual void ReleaseFontCache(FontCache cache)
        {
            cache.Dispose();
        }

        public DocumentLayoutUnitConverter UnitConverter =>
            this.unitConverter;

        public bool AllowCjkCorrection =>
            this.allowCjkCorrection;

        public bool UseSystemFontQuality =>
            this.useSystemFontQuality;
    }
}

