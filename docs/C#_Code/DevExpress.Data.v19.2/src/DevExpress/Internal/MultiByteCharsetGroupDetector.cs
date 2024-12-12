namespace DevExpress.Internal
{
    using System;

    public class MultiByteCharsetGroupDetector : GroupDetector
    {
        private readonly EncodingDetectorLanguageFilter languageFilter;
        private int _keepNext;

        public MultiByteCharsetGroupDetector(EncodingDetectorLanguageFilter languageFilter)
        {
            this.languageFilter = languageFilter;
            this.CreateDetectors();
        }

        protected internal override DetectionResult ForceAnalyseData(byte[] buffer, int from, int length)
        {
            int num = from;
            int num2 = this._keepNext;
            int num3 = from + length;
            for (int i = from; i < num3; i++)
            {
                if (base.IsUpperAsciiByte(buffer[i]))
                {
                    if (num2 == 0)
                    {
                        num = i;
                    }
                    num2 = 2;
                }
                else if ((num2 != 0) && (--num2 == 0))
                {
                    DetectionResult result = this.AnalyseDataCore(buffer, num, (i + 1) - num);
                    if (result != DetectionResult.Detecting)
                    {
                        return result;
                    }
                }
            }
            if (num2 != 0)
            {
                DetectionResult result2 = this.AnalyseDataCore(buffer, num, length - num);
                if (result2 != DetectionResult.Detecting)
                {
                    return result2;
                }
            }
            this._keepNext = num2;
            return base.CurrentResult;
        }

        protected internal override void PopulateDetectors()
        {
            base.Detectors.Add(new Utf8EncodingDetector());
            if ((this.languageFilter & EncodingDetectorLanguageFilter.Japanese) != 0)
            {
                bool preferredLanguage = this.languageFilter == EncodingDetectorLanguageFilter.Japanese;
                base.Detectors.Add(new ShiftedJisEncodingDetector(preferredLanguage));
                base.Detectors.Add(new EucJpEncodingDetector(preferredLanguage));
            }
            if ((this.languageFilter & EncodingDetectorLanguageFilter.ChineseSimplified) != 0)
            {
                base.Detectors.Add(new GB18030EncodingDetector(this.languageFilter == EncodingDetectorLanguageFilter.ChineseSimplified));
            }
            if ((this.languageFilter & EncodingDetectorLanguageFilter.Korean) != 0)
            {
                base.Detectors.Add(new EucKrEncodingDetector(this.languageFilter == EncodingDetectorLanguageFilter.Korean));
            }
            if ((this.languageFilter & EncodingDetectorLanguageFilter.ChineseTraditional) != 0)
            {
                base.Detectors.Add(new Big5EncodingDetector(this.languageFilter == EncodingDetectorLanguageFilter.ChineseTraditional));
            }
        }
    }
}

