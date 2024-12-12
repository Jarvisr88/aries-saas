namespace DevExpress.Pdf.Native
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct JPXQuantizationStepSize
    {
        private readonly byte epsilon;
        private readonly short mu;
        public static JPXQuantizationStepSize Create(byte v1, byte v2) => 
            new JPXQuantizationStepSize((byte) (v1 >> 3), (short) (((v1 & 7) << 8) | v2));

        public byte Epsilon =>
            this.epsilon;
        public short Mu =>
            this.mu;
        public JPXQuantizationStepSize(byte epsilon, short mu)
        {
            this.epsilon = epsilon;
            this.mu = mu;
        }
    }
}

