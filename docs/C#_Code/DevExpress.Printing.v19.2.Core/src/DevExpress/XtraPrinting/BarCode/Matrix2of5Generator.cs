namespace DevExpress.XtraPrinting.BarCode
{
    using DevExpress.XtraPrinting.BarCode.Native;
    using System;
    using System.Collections;
    using System.ComponentModel;

    public class Matrix2of5Generator : Industrial2of5Generator
    {
        private static Hashtable charPattern;

        static Matrix2of5Generator();
        public Matrix2of5Generator();
        private Matrix2of5Generator(Matrix2of5Generator source);
        protected override BarCodeGeneratorBase CloneGenerator();
        protected override string FormatText(string text);
        protected override Hashtable GetPatternTable();
        protected override char[] PrepareText(string text);

        [Description("For internal use. Gets the bar code symbology for the current generator object.")]
        public override BarCodeSymbology SymbologyCode { get; }
    }
}

