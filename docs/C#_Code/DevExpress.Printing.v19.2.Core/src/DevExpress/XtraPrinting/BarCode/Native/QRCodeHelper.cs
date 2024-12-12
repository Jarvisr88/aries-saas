namespace DevExpress.XtraPrinting.BarCode.Native
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;

    [CLSCompliant(false)]
    public class QRCodeHelper
    {
        private int errorCorrectionLevel;
        private int sizeVersion;

        public QRCodeHelper(int errorCorrectionLevel, int sizeVersion);
        private sbyte[] CalculateRSECC(int[] data, sbyte[] bits, int maxCodewords, int maxDataBits);
        public sbyte[][] CreateMatrixContent(int[] dataValue, sbyte[] dataBits, int maxDataBits, int sideModules, sbyte[] frameData);
        private sbyte[] CreateRsBlockOrder();
        private static sbyte[] DivideDataBy8Bits(int[] data, sbyte[] bits, int maxDataCodewords);
        private Stream GetResourceStream(string resourceName);
        public static int GetTotalDataBits(int dataCounter, sbyte[] dataBits);
        private Stream GetUnpackedDataStream(string resourceName, out Stream sourceStream);
        public void MaskApply(sbyte[][] matrixContent, sbyte maskNumber);
        private static int ReadArray(Stream sourceStream, sbyte[] target, int start, int count);
        public sbyte SelectMask(sbyte[][] matrixContent);
        private static int URShift(int number, int bits);
        public static void ValidateDataBits(int dataCounter, int[] dataValue, sbyte[] dataBits, int totalDataBits, int maxDataBits);
        private static sbyte[] XorByteArrays(sbyte[] array1, sbyte[] array2);
    }
}

