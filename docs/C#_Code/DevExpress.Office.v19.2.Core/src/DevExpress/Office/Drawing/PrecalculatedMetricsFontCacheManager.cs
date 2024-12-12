namespace DevExpress.Office.Drawing
{
    using DevExpress.Data.Helpers;
    using DevExpress.Office.Layout;
    using DevExpress.Office.Utils;
    using DevExpress.Utils;
    using System;

    public class PrecalculatedMetricsFontCacheManager : FontCacheManager
    {
        private readonly bool roundMaxDigitWidth;

        public PrecalculatedMetricsFontCacheManager(DocumentLayoutUnitConverter unitConverter, bool allowCjkCorrection, bool useSystemFontQuality) : this(unitConverter, allowCjkCorrection, useSystemFontQuality, false)
        {
        }

        public PrecalculatedMetricsFontCacheManager(DocumentLayoutUnitConverter unitConverter, bool allowCjkCorrection, bool useSystemFontQuality, bool roundMaxDigitWidth) : base(unitConverter, allowCjkCorrection, useSystemFontQuality)
        {
            this.roundMaxDigitWidth = roundMaxDigitWidth;
        }

        public override FontCacheManager Clone(DocumentLayoutUnitConverter unitConverter, bool allowCjkCorrection, bool useSystemFontQuality) => 
            new PrecalculatedMetricsFontCacheManager(unitConverter, allowCjkCorrection, useSystemFontQuality, this.roundMaxDigitWidth);

        public override FontCache CreateFontCache() => 
            new PrecalculatedMetricsFontCache(base.UnitConverter, base.AllowCjkCorrection, this.roundMaxDigitWidth);

        public static bool ShouldUse() => 
            OSHelper.IsWindows ? (AzureCompatibility.Enable || (!SecurityHelper.IsUnmanagedCodeGrantedAndHasZeroHwnd || !SecurityHelper.IsUnmanagedCodeGrantedAndCanUseGetHdc)) : false;

        protected bool RoundMaxDigitWidth =>
            this.roundMaxDigitWidth;
    }
}

