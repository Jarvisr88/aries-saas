namespace DevExpress.Internal
{
    using DevExpress.Utils;
    using System;
    using System.Text;

    public class EucKrEncodingDetector : EastAsianEncodingDetector
    {
        public EucKrEncodingDetector(bool preferredLanguage) : base(preferredLanguage)
        {
        }

        protected internal override CharacterDistributionAnalyzer CreateDistributionAnalyzer() => 
            new EucKrCharDistributionAnalyzer();

        protected internal override EncodingAnalyzer CreateEncodingAnalyzer() => 
            new EucKrEncodingAnalyzer();

        public override System.Text.Encoding Encoding =>
            DXEncoding.GetEncoding(0xcaed);
    }
}

