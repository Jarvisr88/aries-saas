namespace DevExpress.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class GroupDetector : EncodingDetectorBase
    {
        private int bestMatchDetectorIndex = -1;
        private int activeDetectorCount;
        private List<EncodingDetectorBase> detectors;
        private bool[] isDetectorDisabled;

        protected GroupDetector()
        {
        }

        protected internal virtual DetectionResult AnalyseDataCore(byte[] buffer, int from, int length)
        {
            if (base.CurrentResult != DetectionResult.Negative)
            {
                if (length == 0)
                {
                    return base.CurrentResult;
                }
                for (int i = 0; i < this.Detectors.Count; i++)
                {
                    if (!this.isDetectorDisabled[i])
                    {
                        DetectionResult result = this.Detectors[i].AnalyseData(buffer, from, length);
                        if (result == DetectionResult.Positive)
                        {
                            this.bestMatchDetectorIndex = i;
                            return DetectionResult.Positive;
                        }
                        if (result == DetectionResult.Negative)
                        {
                            this.isDetectorDisabled[i] = true;
                            this.activeDetectorCount--;
                            if (this.activeDetectorCount <= 0)
                            {
                                return DetectionResult.Negative;
                            }
                        }
                    }
                }
            }
            return base.CurrentResult;
        }

        protected internal virtual void CreateDetectors()
        {
            this.detectors = new List<EncodingDetectorBase>();
            this.PopulateDetectors();
            this.isDetectorDisabled = new bool[this.Detectors.Count];
            this.activeDetectorCount = this.Detectors.Count;
        }

        public override float GetConfidence()
        {
            DetectionResult currentResult = base.CurrentResult;
            if (currentResult == DetectionResult.Positive)
            {
                return 0.99f;
            }
            if (currentResult == DetectionResult.Negative)
            {
                return 0.01f;
            }
            float num = 0f;
            for (int i = 0; i < this.Detectors.Count; i++)
            {
                if (!this.isDetectorDisabled[i])
                {
                    float confidence = this.Detectors[i].GetConfidence();
                    if (num < confidence)
                    {
                        num = confidence;
                        this.bestMatchDetectorIndex = i;
                    }
                }
            }
            return num;
        }

        protected internal abstract void PopulateDetectors();

        protected internal List<EncodingDetectorBase> Detectors =>
            this.detectors;

        public override System.Text.Encoding Encoding
        {
            get
            {
                if (this.bestMatchDetectorIndex == -1)
                {
                    this.GetConfidence();
                    if (this.bestMatchDetectorIndex == -1)
                    {
                        return this.Detectors[0].Encoding;
                    }
                }
                return this.Detectors[this.bestMatchDetectorIndex].Encoding;
            }
        }
    }
}

