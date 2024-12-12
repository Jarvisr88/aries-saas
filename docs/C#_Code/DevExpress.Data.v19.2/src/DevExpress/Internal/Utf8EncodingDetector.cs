namespace DevExpress.Internal
{
    using System;
    using System.Text;

    public class Utf8EncodingDetector : EncodingDetectorBase
    {
        private int multiByteCharCount;
        private readonly EncodingAnalyzer encodingAnalyzer = new Utf8EncodingAnalyzer();

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
                if ((state == EAState.Start) && (this.encodingAnalyzer.CurrentCharLength >= 2))
                {
                    this.multiByteCharCount++;
                }
            }
            return (((base.CurrentResult != DetectionResult.Detecting) || (this.GetConfidence() <= 0.95f)) ? base.CurrentResult : DetectionResult.Positive);
        }

        public override float GetConfidence()
        {
            float num = 0.99f;
            if (this.multiByteCharCount >= 6)
            {
                return 0.99f;
            }
            for (int i = 0; i < this.multiByteCharCount; i++)
            {
                num *= 0.5f;
            }
            return (1f - num);
        }

        public override System.Text.Encoding Encoding =>
            System.Text.Encoding.UTF8;
    }
}

