namespace DevExpress.XtraPrinting.BarCode
{
    using DevExpress.Printing;
    using DevExpress.Utils.Serializing;
    using DevExpress.Utils.Serializing.Helpers;
    using DevExpress.XtraPrinting.BarCode.Native;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class CodabarGenerator : BarCodeGeneratorBase, IXtraSupportShouldSerialize
    {
        private static Hashtable charPattern;
        private static char?[] startStopChars;
        private static string validCharSet;
        protected const int defaultWideNarrowRatio = 2;
        private float wideNarrowRatio;

        static CodabarGenerator();
        public CodabarGenerator();
        private CodabarGenerator(CodabarGenerator source);
        private string AppendStartStopSymbols(string text);
        protected override BarCodeGeneratorBase CloneGenerator();
        bool IXtraSupportShouldSerialize.ShouldSerialize(string propertyName);
        protected override Hashtable GetPatternTable();
        protected override float GetPatternWidth(char pattern);
        protected override string GetValidCharSet();
        protected override char[] PrepareText(string text);

        [DefaultValue(false), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public override bool CalcCheckSum { get; set; }

        [DXHelpExclude(true), Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), XtraSerializableProperty]
        public CodabarStartStopPair StartStopPair { get; set; }

        [Description("Gets or sets the first (start) symbol used to code the bar code's structure."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.BarCode.CodabarGenerator.StartSymbol"), DefaultValue(1), NotifyParentProperty(true), XtraSerializableProperty]
        public CodabarStartStopSymbol StartSymbol { get; set; }

        [Description("Gets or sets the last (stop) symbol used to code the bar code's structure."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.BarCode.CodabarGenerator.StopSymbol"), DefaultValue(1), NotifyParentProperty(true), XtraSerializableProperty]
        public CodabarStartStopSymbol StopSymbol { get; set; }

        [Description("Gets or sets the density of a bar code's bars."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.BarCode.CodabarGenerator.WideNarrowRatio"), DefaultValue(2), XtraSerializableProperty]
        public float WideNarrowRatio { get; set; }

        [Description("For internal use. Gets the bar code symbology for the current generator object.")]
        public override BarCodeSymbology SymbologyCode { get; }
    }
}

