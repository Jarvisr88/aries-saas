namespace DevExpress.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class NormalEncodingDetectorState : EncodingDetectorState
    {
        public NormalEncodingDetectorState(InternalEncodingDetector detector) : base(detector)
        {
        }

        public override bool AnalyseData(byte[] buffer, int from, int length)
        {
            List<EncodingDetectorBase> detectors = base.Detector.Detectors;
            int count = detectors.Count;
            for (int i = 0; i < count; i++)
            {
                DetectionResult result = detectors[i].AnalyseData(buffer, from, length);
                if (result == DetectionResult.Positive)
                {
                    base.Detector.DetectorState = new FinalEncodingDetectorState(base.Detector, detectors[i].Encoding);
                    return true;
                }
            }
            return false;
        }

        public override Encoding CalculateResult()
        {
            List<EncodingDetectorBase> detectors = base.Detector.Detectors;
            float num = 0f;
            int num2 = 0;
            int count = detectors.Count;
            for (int i = 0; i < count; i++)
            {
                float confidence = detectors[i].GetConfidence();
                if (confidence > num)
                {
                    num = confidence;
                    num2 = i;
                }
            }
            base.Detector.DetectorState = (num <= 0.2f) ? new FinalEncodingDetectorState(base.Detector, null) : new FinalEncodingDetectorState(base.Detector, detectors[num2].Encoding);
            return base.Detector.DetectorState.CalculateResult();
        }
    }
}

