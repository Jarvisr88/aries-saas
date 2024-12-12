namespace DevExpress.Internal
{
    using DevExpress.Utils;
    using System;
    using System.Text;

    public class GB18030EncodingDetector : EastAsianEncodingDetector
    {
        public GB18030EncodingDetector(bool preferredLanguage) : base(preferredLanguage)
        {
        }

        protected internal override CharacterDistributionAnalyzer CreateDistributionAnalyzer() => 
            new GB18030CharDistributionAnalyzer();

        protected internal override EncodingAnalyzer CreateEncodingAnalyzer() => 
            new GB18030EncodingAnalyzer();

        public override System.Text.Encoding Encoding =>
            DXEncoding.GetEncoding(0xd698);
    }
}

