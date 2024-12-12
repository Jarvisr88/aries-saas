namespace DevExpress.Internal
{
    using DevExpress.Utils;
    using System;
    using System.Text;

    public class Big5EncodingDetector : EastAsianEncodingDetector
    {
        public Big5EncodingDetector(bool preferredLanguage) : base(preferredLanguage)
        {
        }

        protected internal override CharacterDistributionAnalyzer CreateDistributionAnalyzer() => 
            new Big5CharDistributionAnalyzer();

        protected internal override EncodingAnalyzer CreateEncodingAnalyzer() => 
            new Big5EncodingAnalyzer();

        public override System.Text.Encoding Encoding =>
            DXEncoding.GetEncoding(950);
    }
}

