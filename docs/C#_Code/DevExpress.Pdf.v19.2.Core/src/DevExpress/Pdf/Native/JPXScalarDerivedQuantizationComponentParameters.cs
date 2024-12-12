namespace DevExpress.Pdf.Native
{
    using System;

    public class JPXScalarDerivedQuantizationComponentParameters : JPXIrreversibleTransformationQuantizationComponentParameters
    {
        private readonly JPXQuantizationStepSize stepSize;

        public JPXScalarDerivedQuantizationComponentParameters(int guardBitCount, PdfBigEndianStreamReader reader) : base(guardBitCount)
        {
            this.stepSize = JPXQuantizationStepSize.Create(reader.ReadByte(), reader.ReadByte());
        }

        protected override JPXQuantizationStepSize GetStepSize(int subBandIndex)
        {
            int num = subBandIndex / 4;
            return new JPXQuantizationStepSize((byte) (this.stepSize.Epsilon + ((num > 0) ? (1 - num) : 0)), this.stepSize.Mu);
        }
    }
}

