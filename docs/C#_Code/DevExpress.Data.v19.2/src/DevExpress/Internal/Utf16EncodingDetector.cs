namespace DevExpress.Internal
{
    using System;

    public abstract class Utf16EncodingDetector : EncodingDetectorBase
    {
        protected const int MinimumDataThreshold = 20;
        protected const int EnoughDataThreshold = 0x100;
        private int index;
        private int oddLowerByteCount;
        private int evenLowerByteCount;

        protected Utf16EncodingDetector()
        {
        }

        protected internal override DetectionResult ForceAnalyseData(byte[] buffer, int from, int length)
        {
            int num = from + length;
            int index = from;
            while (index < num)
            {
                byte num3 = buffer[index];
                if ((this.index % 2) == 0)
                {
                    if (num3 <= 5)
                    {
                        this.evenLowerByteCount++;
                    }
                }
                else if (num3 <= 5)
                {
                    this.oddLowerByteCount++;
                }
                index++;
                this.index++;
            }
            return (((base.CurrentResult != DetectionResult.Detecting) || (this.GetConfidence() <= 0.95f)) ? base.CurrentResult : DetectionResult.Positive);
        }

        public override float GetConfidence()
        {
            int num = this.index + 1;
            if ((num % 2) != 0)
            {
                num++;
            }
            if (num < 20)
            {
                return 0.01f;
            }
            float halfByteCount = ((float) num) / 2f;
            return this.GetConfidenceCore(this.oddLowerByteCount, this.evenLowerByteCount, halfByteCount);
        }

        protected internal abstract float GetConfidenceCore(int oddLowerByteCount, int evenLowerByteCount, float halfByteCount);
    }
}

