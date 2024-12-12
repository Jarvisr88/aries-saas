namespace DevExpress.XtraPrinting.BarCode
{
    using DevExpress.XtraPrinting.BarCode.Native;
    using System;
    using System.Collections;
    using System.ComponentModel;

    public class Code11Generator : BarCodeGeneratorBase
    {
        private static string validCharSet;
        private static string charIndexes;
        private static Hashtable charPattern;

        static Code11Generator();
        public Code11Generator();
        private Code11Generator(Code11Generator source);
        private static char CalcCCheckDigit(string text);
        private static char CalcKCheckDigit(string text);
        protected override BarCodeGeneratorBase CloneGenerator();
        protected override Hashtable GetPatternTable();
        protected override float GetPatternWidth(char pattern);
        protected override string GetValidCharSet();
        protected override char[] PrepareText(string text);

        [DefaultValue(true), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public override bool CalcCheckSum { get; set; }

        [Description("For internal use. Gets the bar code symbology for the current generator object.")]
        public override BarCodeSymbology SymbologyCode { get; }
    }
}

