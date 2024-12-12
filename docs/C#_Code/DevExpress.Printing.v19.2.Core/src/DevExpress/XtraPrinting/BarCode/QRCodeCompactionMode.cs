namespace DevExpress.XtraPrinting.BarCode
{
    using DevExpress.Printing;
    using DevExpress.Utils.Design;
    using System;
    using System.ComponentModel;

    [TypeConverter(typeof(EnumTypeConverter)), ResourceFinder(typeof(ResFinder))]
    public enum QRCodeCompactionMode
    {
        public const QRCodeCompactionMode Numeric = QRCodeCompactionMode.Numeric;,
        public const QRCodeCompactionMode AlphaNumeric = QRCodeCompactionMode.AlphaNumeric;,
        public const QRCodeCompactionMode Byte = QRCodeCompactionMode.Byte;
    }
}

