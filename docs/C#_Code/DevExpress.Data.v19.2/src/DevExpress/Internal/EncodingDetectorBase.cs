namespace DevExpress.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class EncodingDetectorBase
    {
        private DetectionResult currentResult;
        protected const float ShortcutThreshold = 0.95f;

        protected EncodingDetectorBase()
        {
        }

        public DetectionResult AnalyseData(byte[] buffer, int from, int length)
        {
            if (this.CurrentResult == DetectionResult.Detecting)
            {
                if (length <= 0)
                {
                    return this.CurrentResult;
                }
                this.currentResult = this.ForceAnalyseData(buffer, from, length);
            }
            return this.CurrentResult;
        }

        protected int AppendBytes(byte[] source, int from, int to, List<byte> target)
        {
            while (from < to)
            {
                target.Add(source[from]);
                from++;
            }
            return from;
        }

        protected internal abstract DetectionResult ForceAnalyseData(byte[] buffer, int from, int length);
        public abstract float GetConfidence();
        protected bool IsNonEnglishLetterLowerAsciiByte(byte currentByte) => 
            (currentByte < 0x41) || (((currentByte > 90) && (currentByte < 0x61)) || (currentByte > 0x7a));

        protected bool IsUpperAsciiByte(byte currentByte) => 
            (currentByte & 0x80) != 0;

        public abstract System.Text.Encoding Encoding { get; }

        protected DetectionResult CurrentResult =>
            this.currentResult;
    }
}

