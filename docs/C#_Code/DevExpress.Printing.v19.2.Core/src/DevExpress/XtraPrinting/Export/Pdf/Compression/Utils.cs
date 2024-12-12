namespace DevExpress.XtraPrinting.Export.Pdf.Compression
{
    using System;

    public class Utils
    {
        private static readonly byte[] bit4Reverse = new byte[] { 0, 8, 4, 12, 2, 10, 6, 14, 1, 9, 5, 13, 3, 11, 7, 15 };

        public static short BitReverse(int value, int length)
        {
            value = value << ((0x10 - length) & 0x1f);
            return (short) ((((bit4Reverse[value & 15] << 12) | (bit4Reverse[(value >> 4) & 15] << 8)) | (bit4Reverse[(value >> 8) & 15] << 4)) | bit4Reverse[value >> 12]);
        }
    }
}

