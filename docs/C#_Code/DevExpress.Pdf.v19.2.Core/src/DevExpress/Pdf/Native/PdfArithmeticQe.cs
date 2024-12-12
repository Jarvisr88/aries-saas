namespace DevExpress.Pdf.Native
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct PdfArithmeticQe
    {
        private static readonly PdfArithmeticQe[] values;
        private uint qe;
        private byte mpsXor;
        private byte lpsXor;
        private byte sw;
        public static PdfArithmeticQe[] Values =>
            values;
        internal uint Qe =>
            this.qe;
        internal byte MpsXor =>
            this.mpsXor;
        internal byte LpsXor =>
            this.lpsXor;
        internal byte Switch =>
            this.sw;
        private PdfArithmeticQe(uint qe, byte mpsXor, byte lpsXor, byte sw)
        {
            this.qe = qe;
            this.mpsXor = mpsXor;
            this.lpsXor = lpsXor;
            this.sw = sw;
        }

        static PdfArithmeticQe()
        {
            PdfArithmeticQe[] qeArray1 = new PdfArithmeticQe[0x2f];
            qeArray1[0] = new PdfArithmeticQe(0x56010000, 2, 2, 1);
            qeArray1[1] = new PdfArithmeticQe(0x34010000, 4, 12, 0);
            qeArray1[2] = new PdfArithmeticQe(0x18010000, 6, 0x12, 0);
            qeArray1[3] = new PdfArithmeticQe(0xac10000, 8, 0x18, 0);
            qeArray1[4] = new PdfArithmeticQe(0x5210000, 10, 0x3a, 0);
            qeArray1[5] = new PdfArithmeticQe(0x2210000, 0x4c, 0x42, 0);
            qeArray1[6] = new PdfArithmeticQe(0x56010000, 14, 12, 1);
            qeArray1[7] = new PdfArithmeticQe(0x54010000, 0x10, 0x1c, 0);
            qeArray1[8] = new PdfArithmeticQe(0x48010000, 0x12, 0x1c, 0);
            qeArray1[9] = new PdfArithmeticQe(0x38010000, 20, 0x1c, 0);
            qeArray1[10] = new PdfArithmeticQe(0x30010000, 0x16, 0x22, 0);
            qeArray1[11] = new PdfArithmeticQe(0x24010000, 0x18, 0x24, 0);
            qeArray1[12] = new PdfArithmeticQe(0x1c010000, 0x1a, 40, 0);
            qeArray1[13] = new PdfArithmeticQe(0x16010000, 0x3a, 0x2a, 0);
            qeArray1[14] = new PdfArithmeticQe(0x56010000, 30, 0x1c, 1);
            qeArray1[15] = new PdfArithmeticQe(0x54010000, 0x20, 0x1c, 0);
            qeArray1[0x10] = new PdfArithmeticQe(0x51010000, 0x22, 30, 0);
            qeArray1[0x11] = new PdfArithmeticQe(0x48010000, 0x24, 0x20, 0);
            qeArray1[0x12] = new PdfArithmeticQe(0x38010000, 0x26, 0x22, 0);
            qeArray1[0x13] = new PdfArithmeticQe(0x34010000, 40, 0x24, 0);
            qeArray1[20] = new PdfArithmeticQe(0x30010000, 0x2a, 0x26, 0);
            qeArray1[0x15] = new PdfArithmeticQe(0x28010000, 0x2c, 0x26, 0);
            qeArray1[0x16] = new PdfArithmeticQe(0x24010000, 0x2e, 40, 0);
            qeArray1[0x17] = new PdfArithmeticQe(0x22010000, 0x30, 0x2a, 0);
            qeArray1[0x18] = new PdfArithmeticQe(0x1c010000, 50, 0x2c, 0);
            qeArray1[0x19] = new PdfArithmeticQe(0x18010000, 0x34, 0x2e, 0);
            qeArray1[0x1a] = new PdfArithmeticQe(0x16010000, 0x36, 0x30, 0);
            qeArray1[0x1b] = new PdfArithmeticQe(0x14010000, 0x38, 50, 0);
            qeArray1[0x1c] = new PdfArithmeticQe(0x12010000, 0x3a, 0x34, 0);
            qeArray1[0x1d] = new PdfArithmeticQe(0x11010000, 60, 0x36, 0);
            qeArray1[30] = new PdfArithmeticQe(0xac10000, 0x3e, 0x38, 0);
            qeArray1[0x1f] = new PdfArithmeticQe(0x9c10000, 0x40, 0x3a, 0);
            qeArray1[0x20] = new PdfArithmeticQe(0x8a10000, 0x42, 60, 0);
            qeArray1[0x21] = new PdfArithmeticQe(0x5210000, 0x44, 0x3e, 0);
            qeArray1[0x22] = new PdfArithmeticQe(0x4410000, 70, 0x40, 0);
            qeArray1[0x23] = new PdfArithmeticQe(0x2a10000, 0x48, 0x42, 0);
            qeArray1[0x24] = new PdfArithmeticQe(0x2210000, 0x4a, 0x44, 0);
            qeArray1[0x25] = new PdfArithmeticQe(0x1410000, 0x4c, 70, 0);
            qeArray1[0x26] = new PdfArithmeticQe(0x1110000, 0x4e, 0x48, 0);
            qeArray1[0x27] = new PdfArithmeticQe(0x850000, 80, 0x4a, 0);
            qeArray1[40] = new PdfArithmeticQe(0x490000, 0x52, 0x4c, 0);
            qeArray1[0x29] = new PdfArithmeticQe(0x250000, 0x54, 0x4e, 0);
            qeArray1[0x2a] = new PdfArithmeticQe(0x150000, 0x56, 80, 0);
            qeArray1[0x2b] = new PdfArithmeticQe(0x90000, 0x58, 0x52, 0);
            qeArray1[0x2c] = new PdfArithmeticQe(0x50000, 90, 0x54, 0);
            qeArray1[0x2d] = new PdfArithmeticQe(0x10000, 90, 0x56, 0);
            qeArray1[0x2e] = new PdfArithmeticQe(0x56010000, 0x5c, 0x5c, 0);
            values = qeArray1;
        }
    }
}

