namespace DevExpress.Internal
{
    using DevExpress.Utils;
    using System;
    using System.Text;

    public class ShiftedJisEncodingDetector : JapaneseEncodingDetector
    {
        public ShiftedJisEncodingDetector(bool preferredLanguage) : base(preferredLanguage)
        {
        }

        protected internal override void ContinueContextAnalysis(byte[] buffer, int from, int length)
        {
            base.ContinueContextAnalysis(buffer, (from + 2) - length, length);
        }

        protected internal override JapaneseContextAnalyzer CreateContextAnalyzer() => 
            new ShiftedJisContextAnalyzer();

        protected internal override CharacterDistributionAnalyzer CreateDistributionAnalyzer() => 
            new ShiftedJisCharDistributionAnalyzer();

        protected internal override EncodingAnalyzer CreateEncodingAnalyzer() => 
            new ShiftedJisEncodingAnalyzer();

        public override System.Text.Encoding Encoding =>
            DXEncoding.GetEncoding(0x3a4);
    }
}

