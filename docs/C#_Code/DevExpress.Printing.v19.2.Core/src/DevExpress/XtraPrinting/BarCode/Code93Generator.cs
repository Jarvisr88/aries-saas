namespace DevExpress.XtraPrinting.BarCode
{
    using DevExpress.XtraPrinting.BarCode.Native;
    using System;
    using System.Collections;
    using System.ComponentModel;

    public class Code93Generator : BarCodeGeneratorBase
    {
        private static string validCharSet;
        private static string charIndexes;
        private static Hashtable charPattern;

        static Code93Generator();
        public Code93Generator();
        protected Code93Generator(Code93Generator source);
        private static char CalcCCheckDigit(string text);
        protected internal static char CalcCheckDigit(string text, string charIndexes, int weighting, int modulo);
        private static char CalcKCheckDigit(string text);
        protected override BarCodeGeneratorBase CloneGenerator();
        protected override Hashtable GetPatternTable();
        protected override string GetValidCharSet();
        protected override char[] PrepareText(string text);

        [Description("For internal use. Gets the bar code symbology for the current generator object.")]
        public override BarCodeSymbology SymbologyCode { get; }
    }
}

