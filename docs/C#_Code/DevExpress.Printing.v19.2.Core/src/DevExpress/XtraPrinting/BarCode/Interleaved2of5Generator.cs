namespace DevExpress.XtraPrinting.BarCode
{
    using DevExpress.Printing;
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting.BarCode.Native;
    using System;
    using System.Collections;
    using System.ComponentModel;

    public class Interleaved2of5Generator : BarCodeGeneratorBase
    {
        protected const int defaultWideNarrowRatio = 3;
        private static string validCharSet;
        private static Hashtable charPattern;
        private float wideNarrowRatio;

        static Interleaved2of5Generator();
        public Interleaved2of5Generator();
        protected Interleaved2of5Generator(Interleaved2of5Generator source);
        protected override BarCodeGeneratorBase CloneGenerator();
        protected override string FormatText(string text);
        protected override Hashtable GetPatternTable();
        protected override float GetPatternWidth(char pattern);
        protected override string GetValidCharSet();
        protected override ArrayList MakeBarCodePattern(string text);
        private static string MergePatterns(char[] first, char[] second);
        protected override char[] PrepareText(string text);

        [Description("Gets or sets the density of a bar code's bars."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.BarCode.Interleaved2of5Generator.WideNarrowRatio"), DefaultValue(3), NotifyParentProperty(true), XtraSerializableProperty]
        public virtual float WideNarrowRatio { get; set; }

        [Description("For internal use. Gets the bar code symbology for the current generator object.")]
        public override BarCodeSymbology SymbologyCode { get; }
    }
}

