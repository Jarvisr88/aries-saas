namespace DevExpress.XtraPrinting.BarCode
{
    using DevExpress.XtraPrinting.BarCode.Native;
    using System;
    using System.Collections;
    using System.ComponentModel;

    public class Code39ExtendedGenerator : Code39Generator
    {
        private static string validCharSet;
        private static Hashtable substituteTable;

        static Code39ExtendedGenerator();
        public Code39ExtendedGenerator();
        private Code39ExtendedGenerator(Code39ExtendedGenerator source);
        protected override BarCodeGeneratorBase CloneGenerator();
        protected override string FormatText(string text);
        protected override string GetValidCharSet();
        private static string PerformSubstitutions(string text);
        protected override char[] PrepareText(string text);

        [Description("For internal use. Gets the bar code symbology for the current generator object.")]
        public override BarCodeSymbology SymbologyCode { get; }
    }
}

