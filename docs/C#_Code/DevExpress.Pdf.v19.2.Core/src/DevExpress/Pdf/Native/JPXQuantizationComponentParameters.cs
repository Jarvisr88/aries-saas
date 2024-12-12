namespace DevExpress.Pdf.Native
{
    using System;

    public abstract class JPXQuantizationComponentParameters
    {
        private readonly int guardBitCount;

        protected JPXQuantizationComponentParameters(int guardBitCount)
        {
            this.guardBitCount = guardBitCount;
        }

        public static JPXQuantizationComponentParameters Create(PdfBigEndianStreamReader reader, int byteCount)
        {
            int num = reader.ReadByte();
            int guardBitCount = num >> 5;
            switch ((num & 0x1f))
            {
                case 0:
                    return new JPXUnitaryStepSizeQuantizationComponentParameters(guardBitCount, reader, byteCount - 1);

                case 1:
                    return new JPXScalarDerivedQuantizationComponentParameters(guardBitCount, reader);

                case 2:
                    return new JPXScalarExpoundedQuantizationComponentParameters(guardBitCount, reader, byteCount - 1);
            }
            PdfDocumentStructureReader.ThrowIncorrectDataException();
            return null;
        }

        public abstract JPXQuantizationHelper CreateHelper(int subBandGainLog, int ri, int subBandIndex);

        public int GuardBitCount =>
            this.guardBitCount;
    }
}

