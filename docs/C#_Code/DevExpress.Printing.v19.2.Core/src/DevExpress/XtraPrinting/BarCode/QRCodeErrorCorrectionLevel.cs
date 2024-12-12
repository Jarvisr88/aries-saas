namespace DevExpress.XtraPrinting.BarCode
{
    using DevExpress.Printing;
    using DevExpress.Utils.Design;
    using System;
    using System.ComponentModel;

    [TypeConverter(typeof(EnumTypeConverter)), ResourceFinder(typeof(ResFinder))]
    public enum QRCodeErrorCorrectionLevel
    {
        public const QRCodeErrorCorrectionLevel M = QRCodeErrorCorrectionLevel.M;,
        public const QRCodeErrorCorrectionLevel L = QRCodeErrorCorrectionLevel.L;,
        public const QRCodeErrorCorrectionLevel H = QRCodeErrorCorrectionLevel.H;,
        public const QRCodeErrorCorrectionLevel Q = QRCodeErrorCorrectionLevel.Q;
    }
}

