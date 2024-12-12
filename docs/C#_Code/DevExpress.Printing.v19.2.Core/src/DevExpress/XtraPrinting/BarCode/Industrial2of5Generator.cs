namespace DevExpress.XtraPrinting.BarCode
{
    using DevExpress.Printing;
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting.BarCode.Native;
    using System;
    using System.Collections;
    using System.ComponentModel;

    public class Industrial2of5Generator : BarCodeGeneratorBase
    {
        protected const float defaultWideNarrowRatio = 2.5f;
        private static string validCharSet;
        private static Hashtable charPattern;
        private float wideNarrowRatio;

        static Industrial2of5Generator();
        public Industrial2of5Generator();
        protected Industrial2of5Generator(Industrial2of5Generator source);
        protected internal static char CalcCheckDigit(string text);
        protected override BarCodeGeneratorBase CloneGenerator();
        protected override string FormatText(string text);
        protected override Hashtable GetPatternTable();
        protected override float GetPatternWidth(char pattern);
        protected override string GetValidCharSet();
        protected override char[] PrepareText(string text);

        [Description("Gets or sets the density of a bar code's bars."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.BarCode.Industrial2of5Generator.WideNarrowRatio"), DefaultValue((float) 2.5f), NotifyParentProperty(true), XtraSerializableProperty]
        public float WideNarrowRatio { get; set; }

        [Description("For internal use. Gets the bar code symbology for the current generator object.")]
        public override BarCodeSymbology SymbologyCode { get; }
    }
}

