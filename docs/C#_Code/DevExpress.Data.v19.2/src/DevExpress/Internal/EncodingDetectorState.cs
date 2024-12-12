namespace DevExpress.Internal
{
    using System;
    using System.Text;

    public abstract class EncodingDetectorState
    {
        private readonly InternalEncodingDetector detector;

        protected EncodingDetectorState(InternalEncodingDetector detector)
        {
            this.detector = detector;
        }

        public abstract bool AnalyseData(byte[] buffer, int from, int length);
        public abstract Encoding CalculateResult();

        protected internal InternalEncodingDetector Detector =>
            this.detector;
    }
}

