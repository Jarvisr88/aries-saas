namespace DevExpress.XtraPrinting.BarCode
{
    using DevExpress.XtraPrinting.BarCode.Native;
    using System;
    using System.ComponentModel;

    public class UPCSupplemental2Generator : UPCSupplementalNGeneratorBase
    {
        private static string[] parityString;

        static UPCSupplemental2Generator();
        public UPCSupplemental2Generator();
        private UPCSupplemental2Generator(UPCSupplemental2Generator source);
        protected override int CalcCheckDigit(string text);
        protected override BarCodeGeneratorBase CloneGenerator();
        protected override int GetMaxCharsCount();
        protected override string GetParityString(int checkDigit);

        [Description("For internal use. Gets the bar code symbology for the current generator object.")]
        public override BarCodeSymbology SymbologyCode { get; }
    }
}

