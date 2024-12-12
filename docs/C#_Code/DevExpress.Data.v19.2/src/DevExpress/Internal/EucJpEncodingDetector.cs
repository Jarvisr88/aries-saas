namespace DevExpress.Internal
{
    using DevExpress.Utils;
    using System;
    using System.Text;

    public class EucJpEncodingDetector : JapaneseEncodingDetector
    {
        public EucJpEncodingDetector(bool preferredLanguage) : base(preferredLanguage)
        {
        }

        protected internal override JapaneseContextAnalyzer CreateContextAnalyzer() => 
            new EucJpContextAnalyzer();

        protected internal override CharacterDistributionAnalyzer CreateDistributionAnalyzer() => 
            new EucJpCharDistributionAnalyzer();

        protected internal override EncodingAnalyzer CreateEncodingAnalyzer() => 
            new EucJpEncodingAnalyzer();

        public override System.Text.Encoding Encoding =>
            DXEncoding.GetEncoding(0xcadc);
    }
}

