namespace DevExpress.XtraPrinting.BarCode.Native
{
    using System;
    using System.Collections.Generic;

    [CLSCompliant(false)]
    public static class QRCodeConstants
    {
        public static readonly Dictionary<sbyte, int> RSEccCodewordsToOffset;
        public static readonly int[] NumericCodeWordNumPlus;
        public static readonly int[] ByteCodeWordNumPlus;
        public static readonly int[] MaxCodewordsValues;
        public static readonly int[] MatrixRemainBitValues;
        public static readonly sbyte[] FormatInformationX1;
        public static readonly sbyte[] FormatInformationY1;
        public static readonly sbyte[,] FormatInformationValues;
        public static readonly int[][] MaxDataBitValues;
        public static readonly int[] MaxMatrixModuleCount;
        public static readonly float LogoErrorModifier;
        public static readonly float[] ErrorCorrectionCapability;
        public static readonly sbyte[,] RSEccCodewordsValues;
        public static readonly sbyte[][] FormatInformationY2Values;
        public static readonly sbyte[][] FormatInformationX2Values;
        public static readonly sbyte[,][] RSBlockOrderTempValues;

        static QRCodeConstants();
    }
}

