namespace DevExpress.XtraPrinting.BarCode
{
    using DevExpress.XtraPrinting.BarCode.Native;
    using System;
    using System.Collections;
    using System.ComponentModel;

    public class UPCE1Generator : UPCEGeneratorBase
    {
        private static Hashtable parityTable;

        static UPCE1Generator();
        public UPCE1Generator();
        protected UPCE1Generator(UPCE1Generator source);
        protected override BarCodeGeneratorBase CloneGenerator();
        protected override char GetNumberSystemDigit();
        protected override string GetParityString(char checkDigit);

        [Description("For internal use. Gets the bar code symbology for the current generator object.")]
        public override BarCodeSymbology SymbologyCode { get; }
    }
}

