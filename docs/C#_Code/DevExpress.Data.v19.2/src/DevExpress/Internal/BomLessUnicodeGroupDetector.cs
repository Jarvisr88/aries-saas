namespace DevExpress.Internal
{
    using System;

    public class BomLessUnicodeGroupDetector : GroupDetector
    {
        public BomLessUnicodeGroupDetector()
        {
            this.CreateDetectors();
        }

        protected internal override DetectionResult ForceAnalyseData(byte[] buffer, int from, int length) => 
            this.AnalyseDataCore(buffer, from, length);

        protected internal override void PopulateDetectors()
        {
            base.Detectors.Add(new Utf16LittleEndianEncodingDetector());
            base.Detectors.Add(new Utf16BigEndianEncodingDetector());
        }
    }
}

