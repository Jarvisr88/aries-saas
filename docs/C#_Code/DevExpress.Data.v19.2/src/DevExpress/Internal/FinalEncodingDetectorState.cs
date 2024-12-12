namespace DevExpress.Internal
{
    using System;
    using System.Text;

    public class FinalEncodingDetectorState : EncodingDetectorState
    {
        private readonly Encoding result;

        public FinalEncodingDetectorState(InternalEncodingDetector detector, Encoding result) : base(detector)
        {
            this.result = result;
        }

        public override bool AnalyseData(byte[] buffer, int from, int length) => 
            true;

        public override Encoding CalculateResult() => 
            this.result;
    }
}

