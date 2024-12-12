namespace DevExpress.Pdf.Native
{
    using System;

    public abstract class JPXIrreversibleTransformationQuantizationComponentParameters : JPXQuantizationComponentParameters
    {
        protected JPXIrreversibleTransformationQuantizationComponentParameters(int guardBitCount) : base(guardBitCount)
        {
        }

        public override JPXQuantizationHelper CreateHelper(int subBandGainLog, int ri, int subBandIndex) => 
            new JPXIrreversibleTransformationQuantizationHelper(subBandGainLog, ri, this.GetStepSize(subBandIndex), base.GuardBitCount);

        protected abstract JPXQuantizationStepSize GetStepSize(int subBandIndex);
    }
}

