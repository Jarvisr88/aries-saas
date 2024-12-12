namespace DevExpress.XtraPrinting.BarCode.Native
{
    using DevExpress.Printing;
    using DevExpress.Utils.Design;
    using System;
    using System.ComponentModel;

    [TypeConverter(typeof(EnumTypeConverter)), ResourceFinder(typeof(ResFinder))]
    public enum BarCodeSymbology
    {
        public const BarCodeSymbology Codabar = BarCodeSymbology.Codabar;,
        public const BarCodeSymbology Industrial2of5 = BarCodeSymbology.Industrial2of5;,
        public const BarCodeSymbology Interleaved2of5 = BarCodeSymbology.Interleaved2of5;,
        public const BarCodeSymbology Code39 = BarCodeSymbology.Code39;,
        public const BarCodeSymbology Code39Extended = BarCodeSymbology.Code39Extended;,
        public const BarCodeSymbology Code93 = BarCodeSymbology.Code93;,
        public const BarCodeSymbology Code93Extended = BarCodeSymbology.Code93Extended;,
        public const BarCodeSymbology Code128 = BarCodeSymbology.Code128;,
        public const BarCodeSymbology Code11 = BarCodeSymbology.Code11;,
        public const BarCodeSymbology CodeMSI = BarCodeSymbology.CodeMSI;,
        public const BarCodeSymbology PostNet = BarCodeSymbology.PostNet;,
        public const BarCodeSymbology EAN13 = BarCodeSymbology.EAN13;,
        public const BarCodeSymbology UPCA = BarCodeSymbology.UPCA;,
        public const BarCodeSymbology EAN8 = BarCodeSymbology.EAN8;,
        public const BarCodeSymbology EAN128 = BarCodeSymbology.EAN128;,
        public const BarCodeSymbology UPCSupplemental2 = BarCodeSymbology.UPCSupplemental2;,
        public const BarCodeSymbology UPCSupplemental5 = BarCodeSymbology.UPCSupplemental5;,
        public const BarCodeSymbology UPCE0 = BarCodeSymbology.UPCE0;,
        public const BarCodeSymbology UPCE1 = BarCodeSymbology.UPCE1;,
        public const BarCodeSymbology Matrix2of5 = BarCodeSymbology.Matrix2of5;,
        public const BarCodeSymbology PDF417 = BarCodeSymbology.PDF417;,
        public const BarCodeSymbology DataMatrix = BarCodeSymbology.DataMatrix;,
        public const BarCodeSymbology QRCode = BarCodeSymbology.QRCode;,
        public const BarCodeSymbology IntelligentMail = BarCodeSymbology.IntelligentMail;,
        public const BarCodeSymbology DataMatrixGS1 = BarCodeSymbology.DataMatrixGS1;,
        public const BarCodeSymbology ITF14 = BarCodeSymbology.ITF14;,
        public const BarCodeSymbology DataBar = BarCodeSymbology.DataBar;,
        public const BarCodeSymbology IntelligentMailPackage = BarCodeSymbology.IntelligentMailPackage;
    }
}

