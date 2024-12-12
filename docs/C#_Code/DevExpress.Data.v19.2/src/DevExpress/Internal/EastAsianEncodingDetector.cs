namespace DevExpress.Internal
{
    using System;

    public abstract class EastAsianEncodingDetector : EncodingDetectorBase
    {
        private readonly EncodingAnalyzer encodingAnalyzer;
        private readonly CharacterDistributionAnalyzer distributionAnalyzer;
        private readonly bool isPreferredLanguage;
        private readonly byte[] lastCharBuffer;

        protected EastAsianEncodingDetector(bool preferredLanguage)
        {
            this.isPreferredLanguage = preferredLanguage;
            this.lastCharBuffer = new byte[2];
            this.encodingAnalyzer = this.CreateEncodingAnalyzer();
            this.distributionAnalyzer = this.CreateDistributionAnalyzer();
        }

        protected internal virtual void ContinueContextAnalysis(byte[] buffer, int from, int length)
        {
        }

        protected internal abstract CharacterDistributionAnalyzer CreateDistributionAnalyzer();
        protected internal abstract EncodingAnalyzer CreateEncodingAnalyzer();
        protected internal override DetectionResult ForceAnalyseData(byte[] buffer, int from, int length)
        {
            int num = from + length;
            for (int i = from; i < num; i++)
            {
                EAState state = this.encodingAnalyzer.NextState(buffer[i]);
                if (state == EAState.ItsMe)
                {
                    return DetectionResult.Positive;
                }
                if (state == EAState.Start)
                {
                    int currentCharLength = this.encodingAnalyzer.CurrentCharLength;
                    if (i != 0)
                    {
                        this.ContinueContextAnalysis(buffer, i - 1, currentCharLength);
                        this.distributionAnalyzer.AnalyzeCharacter(buffer, i - 1, currentCharLength);
                    }
                    else
                    {
                        this.lastCharBuffer[1] = buffer[0];
                        this.ContinueContextAnalysis(this.lastCharBuffer, 0, currentCharLength);
                        this.distributionAnalyzer.AnalyzeCharacter(this.lastCharBuffer, 0, currentCharLength);
                    }
                }
            }
            this.lastCharBuffer[0] = buffer[num - 1];
            return ((!this.GotEnoughData() || (this.GetConfidence() <= 0.95f)) ? DetectionResult.Detecting : DetectionResult.Positive);
        }

        public override float GetConfidence() => 
            Math.Max(this.GetContextAnalyzerConfidence(this.isPreferredLanguage), this.distributionAnalyzer.GetConfidence(this.isPreferredLanguage));

        protected internal virtual float GetContextAnalyzerConfidence(bool isPreferredLanguage) => 
            0f;

        protected internal virtual bool GotEnoughData() => 
            this.distributionAnalyzer.GotEnoughData();
    }
}

