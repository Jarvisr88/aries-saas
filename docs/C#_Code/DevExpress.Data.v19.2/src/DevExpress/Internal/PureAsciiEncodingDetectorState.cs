namespace DevExpress.Internal
{
    using System;
    using System.Text;

    public class PureAsciiEncodingDetectorState : EncodingDetectorState
    {
        public PureAsciiEncodingDetectorState(InternalEncodingDetector detector) : base(detector)
        {
        }

        public override bool AnalyseData(byte[] buffer, int from, int length)
        {
            int num = from + length;
            for (int i = from; i < num; i++)
            {
                if (((buffer[i] & 0x80) != 0) && (buffer[i] != 160))
                {
                    base.Detector.DetectorState = new NormalEncodingDetectorState(base.Detector);
                    return base.Detector.AnalyseData(buffer, from, length);
                }
            }
            if (base.Detector.UnicodeDetector.AnalyseData(buffer, from, length) != DetectionResult.Positive)
            {
                return false;
            }
            base.Detector.DetectorState = new FinalEncodingDetectorState(base.Detector, base.Detector.UnicodeDetector.Encoding);
            return true;
        }

        public override Encoding CalculateResult() => 
            null;
    }
}

