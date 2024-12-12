namespace DevExpress.Pdf.Native
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct PdfFixedPointNumber
    {
        private const int fractionPartSize = 0x16;
        private const int floatToFixedFactor = 0x400000;
        private const int half = 0x200000;
        private readonly int value;
        public static PdfFixedPointNumber operator +(PdfFixedPointNumber first, PdfFixedPointNumber second) => 
            new PdfFixedPointNumber(first.value + second.value);

        public static PdfFixedPointNumber operator *(int first, PdfFixedPointNumber second) => 
            new PdfFixedPointNumber(first * second.value);

        private PdfFixedPointNumber(int value)
        {
            this.value = value;
        }

        public PdfFixedPointNumber(float value)
        {
            this.value = (int) (value * 4194304f);
        }

        public byte RoundToByte()
        {
            int num = (this.value + 0x200000) >> 0x16;
            return ((num <= 0xff) ? ((byte) num) : 0xff);
        }
    }
}

