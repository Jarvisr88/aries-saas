namespace DevExpress.XtraPrinting.BarCode
{
    using DevExpress.Printing;
    using DevExpress.Utils.Design;
    using System;
    using System.ComponentModel;

    [TypeConverter(typeof(EnumTypeConverter)), ResourceFinder(typeof(ResFinder))]
    public enum ErrorCorrectionLevel
    {
        public const ErrorCorrectionLevel Level0 = ErrorCorrectionLevel.Level0;,
        public const ErrorCorrectionLevel Level1 = ErrorCorrectionLevel.Level1;,
        public const ErrorCorrectionLevel Level2 = ErrorCorrectionLevel.Level2;,
        public const ErrorCorrectionLevel Level3 = ErrorCorrectionLevel.Level3;,
        public const ErrorCorrectionLevel Level4 = ErrorCorrectionLevel.Level4;,
        public const ErrorCorrectionLevel Level5 = ErrorCorrectionLevel.Level5;,
        public const ErrorCorrectionLevel Level6 = ErrorCorrectionLevel.Level6;,
        public const ErrorCorrectionLevel Level7 = ErrorCorrectionLevel.Level7;,
        public const ErrorCorrectionLevel Level8 = ErrorCorrectionLevel.Level8;
    }
}

