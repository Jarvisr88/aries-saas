namespace DevExpress.XtraPrinting.BarCode
{
    using DevExpress.Printing;
    using DevExpress.Utils.Design;
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting.BarCode.Native;
    using System;
    using System.Collections;
    using System.ComponentModel;

    public class EAN128Generator : Code128Generator
    {
        protected const string defaultFNC1Subst = "#";
        private string fnc1Subst;
        private bool decodeText;

        public EAN128Generator();
        protected EAN128Generator(EAN128Generator source);
        protected override BarCodeGeneratorBase CloneGenerator();
        protected override string GetValidCharSet();
        protected override void InsertControlCharsIndexes(ArrayList text);
        protected override bool IsValidTextFormat(string text);
        protected override string MakeDisplayText(string text);
        protected override char[] PrepareText(string text);

        [Description("Gets or sets the symbol (or set of symbols) in the bar code's text that will be replaced with the FNC1 functional character when the bar code's bars are drawn."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.BarCode.EAN128Generator.FNC1Substitute"), DefaultValue("#"), NotifyParentProperty(true), XtraSerializableProperty]
        public string FNC1Substitute { get; set; }

        [Description("Specifies whether or not parentheses should be included in the bar code's text."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.BarCode.EAN128Generator.HumanReadableText"), TypeConverter(typeof(BooleanTypeConverter)), DefaultValue(true), NotifyParentProperty(true), XtraSerializableProperty]
        public virtual bool HumanReadableText { get; set; }

        [Description("For internal use. Gets the bar code symbology for the current generator object.")]
        public override BarCodeSymbology SymbologyCode { get; }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), XtraSerializableProperty(XtraSerializationVisibility.Hidden)]
        public override bool AddLeadingZero { get; set; }
    }
}

