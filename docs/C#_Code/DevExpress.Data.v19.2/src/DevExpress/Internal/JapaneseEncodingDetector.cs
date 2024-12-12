namespace DevExpress.Internal
{
    using System;

    public abstract class JapaneseEncodingDetector : EastAsianEncodingDetector
    {
        private readonly JapaneseContextAnalyzer contextAnalyzer;

        protected JapaneseEncodingDetector(bool preferredLanguage) : base(preferredLanguage)
        {
            this.contextAnalyzer = this.CreateContextAnalyzer();
        }

        protected internal override void ContinueContextAnalysis(byte[] buffer, int from, int length)
        {
            this.contextAnalyzer.AnalyzeCharacter(buffer, from, length);
        }

        protected internal abstract JapaneseContextAnalyzer CreateContextAnalyzer();
        protected internal override float GetContextAnalyzerConfidence(bool isPreferredLanguage) => 
            this.contextAnalyzer.GetConfidence(isPreferredLanguage);

        protected internal override bool GotEnoughData() => 
            this.contextAnalyzer.GotEnoughData();
    }
}

