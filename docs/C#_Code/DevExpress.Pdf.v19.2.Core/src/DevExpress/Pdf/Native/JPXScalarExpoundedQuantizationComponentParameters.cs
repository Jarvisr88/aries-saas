namespace DevExpress.Pdf.Native
{
    using System;

    public class JPXScalarExpoundedQuantizationComponentParameters : JPXIrreversibleTransformationQuantizationComponentParameters
    {
        private readonly JPXQuantizationStepSize[] stepSizes;

        public JPXScalarExpoundedQuantizationComponentParameters(int guardBitCount, PdfBigEndianStreamReader reader, int byteCount) : base(guardBitCount)
        {
            int num = byteCount / 2;
            this.stepSizes = new JPXQuantizationStepSize[num];
            for (int i = 0; i < num; i++)
            {
                this.stepSizes[i] = JPXQuantizationStepSize.Create(reader.ReadByte(), reader.ReadByte());
            }
        }

        protected override JPXQuantizationStepSize GetStepSize(int subBandIndex) => 
            this.stepSizes[subBandIndex];
    }
}

