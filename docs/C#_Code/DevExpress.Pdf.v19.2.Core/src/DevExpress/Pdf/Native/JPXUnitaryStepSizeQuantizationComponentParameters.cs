namespace DevExpress.Pdf.Native
{
    using System;

    public class JPXUnitaryStepSizeQuantizationComponentParameters : JPXQuantizationComponentParameters
    {
        private readonly int[] stepSizeExponents;

        public JPXUnitaryStepSizeQuantizationComponentParameters(int guardBitCount, PdfBigEndianStreamReader reader, int byteCount) : base(guardBitCount)
        {
            this.stepSizeExponents = new int[byteCount];
            for (int i = 0; i < byteCount; i++)
            {
                this.stepSizeExponents[i] = reader.ReadByte() >> 3;
            }
        }

        public override JPXQuantizationHelper CreateHelper(int subBandGainLog, int ri, int subBandIndex) => 
            new JPXUnitaryStepSizeQuantizationHelper(subBandGainLog, this.stepSizeExponents[subBandIndex], base.GuardBitCount);
    }
}

