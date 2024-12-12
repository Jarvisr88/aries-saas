namespace DevExpress.XtraPrinting.BarCode
{
    using DevExpress.Printing;
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting.BarCode.Native;
    using System;
    using System.Collections;
    using System.ComponentModel;

    public class Code39Generator : BarCodeGeneratorBase
    {
        protected const int defaultWideNarrowRatio = 3;
        private static string charIndexes;
        private static Hashtable charPattern;
        private float wideNarrowRatio;

        static Code39Generator();
        public Code39Generator();
        protected Code39Generator(Code39Generator source);
        protected static char CalcCheckDigit(string text);
        protected override BarCodeGeneratorBase CloneGenerator();
        protected override string FormatText(string text);
        protected override Hashtable GetPatternTable();
        protected override float GetPatternWidth(char pattern);
        protected override string GetValidCharSet();
        protected override char[] PrepareText(string text);

        [Description("Gets or sets the density of a bar code's bars."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.BarCode.Code39Generator.WideNarrowRatio"), DefaultValue(3), NotifyParentProperty(true), XtraSerializableProperty]
        public float WideNarrowRatio { get; set; }

        [Description("For internal use. Gets the bar code symbology for the current generator object.")]
        public override BarCodeSymbology SymbologyCode { get; }
    }
}

