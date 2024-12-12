namespace DevExpress.XtraPrinting.BarCode
{
    using DevExpress.Printing;
    using DevExpress.Utils.Design;
    using System;
    using System.ComponentModel;

    [TypeConverter(typeof(EnumTypeConverter)), ResourceFinder(typeof(ResFinder))]
    public enum DataMatrixSize
    {
        public const DataMatrixSize MatrixAuto = DataMatrixSize.MatrixAuto;,
        public const DataMatrixSize Matrix10x10 = DataMatrixSize.Matrix10x10;,
        public const DataMatrixSize Matrix12x12 = DataMatrixSize.Matrix12x12;,
        public const DataMatrixSize Matrix14x14 = DataMatrixSize.Matrix14x14;,
        public const DataMatrixSize Matrix16x16 = DataMatrixSize.Matrix16x16;,
        public const DataMatrixSize Matrix18x18 = DataMatrixSize.Matrix18x18;,
        public const DataMatrixSize Matrix20x20 = DataMatrixSize.Matrix20x20;,
        public const DataMatrixSize Matrix22x22 = DataMatrixSize.Matrix22x22;,
        public const DataMatrixSize Matrix24x24 = DataMatrixSize.Matrix24x24;,
        public const DataMatrixSize Matrix26x26 = DataMatrixSize.Matrix26x26;,
        public const DataMatrixSize Matrix32x32 = DataMatrixSize.Matrix32x32;,
        public const DataMatrixSize Matrix36x36 = DataMatrixSize.Matrix36x36;,
        public const DataMatrixSize Matrix40x40 = DataMatrixSize.Matrix40x40;,
        public const DataMatrixSize Matrix44x44 = DataMatrixSize.Matrix44x44;,
        public const DataMatrixSize Matrix48x48 = DataMatrixSize.Matrix48x48;,
        public const DataMatrixSize Matrix52x52 = DataMatrixSize.Matrix52x52;,
        public const DataMatrixSize Matrix64x64 = DataMatrixSize.Matrix64x64;,
        public const DataMatrixSize Matrix72x72 = DataMatrixSize.Matrix72x72;,
        public const DataMatrixSize Matrix80x80 = DataMatrixSize.Matrix80x80;,
        public const DataMatrixSize Matrix88x88 = DataMatrixSize.Matrix88x88;,
        public const DataMatrixSize Matrix96x96 = DataMatrixSize.Matrix96x96;,
        public const DataMatrixSize Matrix104x104 = DataMatrixSize.Matrix104x104;,
        public const DataMatrixSize Matrix120x120 = DataMatrixSize.Matrix120x120;,
        public const DataMatrixSize Matrix132x132 = DataMatrixSize.Matrix132x132;,
        public const DataMatrixSize Matrix144x144 = DataMatrixSize.Matrix144x144;,
        public const DataMatrixSize Matrix8x18 = DataMatrixSize.Matrix8x18;,
        public const DataMatrixSize Matrix8x32 = DataMatrixSize.Matrix8x32;,
        public const DataMatrixSize Matrix12x26 = DataMatrixSize.Matrix12x26;,
        public const DataMatrixSize Matrix12x36 = DataMatrixSize.Matrix12x36;,
        public const DataMatrixSize Matrix16x36 = DataMatrixSize.Matrix16x36;,
        public const DataMatrixSize Matrix16x48 = DataMatrixSize.Matrix16x48;
    }
}

