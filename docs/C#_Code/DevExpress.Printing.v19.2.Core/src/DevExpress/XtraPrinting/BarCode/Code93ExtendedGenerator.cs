namespace DevExpress.XtraPrinting.BarCode
{
    using DevExpress.XtraPrinting.BarCode.Native;
    using System;
    using System.Collections;
    using System.ComponentModel;

    public class Code93ExtendedGenerator : Code93Generator
    {
        private static string validCharSet;
        private static Hashtable substituteTable;

        static Code93ExtendedGenerator();
        public Code93ExtendedGenerator();
        private Code93ExtendedGenerator(Code93ExtendedGenerator source);
        protected override BarCodeGeneratorBase CloneGenerator();
        protected override string GetValidCharSet();
        private static string PerformSubstitutions(string text);
        protected override char[] PrepareText(string text);

        [Description("For internal use. Gets the bar code symbology for the current generator object.")]
        public override BarCodeSymbology SymbologyCode { get; }
    }
}

