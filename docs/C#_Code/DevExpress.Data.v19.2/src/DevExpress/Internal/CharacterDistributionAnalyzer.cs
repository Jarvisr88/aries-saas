namespace DevExpress.Internal
{
    using System;

    public abstract class CharacterDistributionAnalyzer
    {
        private const int EnoughDataThreashold = 0x400;
        private int frequentCharCount;
        private int totalCharCount;

        protected CharacterDistributionAnalyzer()
        {
        }

        public void AnalyzeCharacter(byte[] buffer, int from, int length)
        {
            int index = (length == 2) ? this.GetOrder(buffer, from) : -1;
            if (index >= 0)
            {
                this.totalCharCount++;
                if ((index < this.CharToFreqOrder.Length) && (0x200 > this.CharToFreqOrder[index]))
                {
                    this.frequentCharCount++;
                }
            }
        }

        public float GetConfidence(bool aIsPreferredLanguage)
        {
            if ((this.totalCharCount <= 0) || (!aIsPreferredLanguage && (this.frequentCharCount <= 4)))
            {
                return 0.01f;
            }
            if (this.totalCharCount != this.frequentCharCount)
            {
                float num = ((float) this.frequentCharCount) / ((this.totalCharCount - this.frequentCharCount) * this.TypicalDistributionRatio);
                if (num < 0.99f)
                {
                    return num;
                }
            }
            return 0.99f;
        }

        protected abstract int GetOrder(byte[] str, int offset);
        public bool GotEnoughData() => 
            this.totalCharCount > 0x400;

        protected internal abstract short[] CharToFreqOrder { get; }

        protected internal abstract float TypicalDistributionRatio { get; }
    }
}

