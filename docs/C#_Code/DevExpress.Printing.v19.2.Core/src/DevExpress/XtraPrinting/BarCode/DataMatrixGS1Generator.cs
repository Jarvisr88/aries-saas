namespace DevExpress.XtraPrinting.BarCode
{
    using DevExpress.Printing;
    using DevExpress.Utils.Design;
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting.BarCode.Native;
    using System;
    using System.ComponentModel;

    public class DataMatrixGS1Generator : DataMatrixGenerator
    {
        internal const char fnc1Char = '\x00e8';
        private const string defaultFNC1Subst = "#";
        private string fnc1Subst;
        private bool decodeText;

        public DataMatrixGS1Generator();
        public DataMatrixGS1Generator(DataMatrixGS1Generator source);
        protected override BarCodeGeneratorBase CloneGenerator();
        protected override string MakeDisplayText(string text);
        protected override string ProcessText(string text);

        [Description("Specifies the symbol (or set of symbols) in the bar code's text that will be replaced with the FNC1 functional character when the bar code's bars are drawn."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.BarCode.DataMatrixGS1Generator.FNC1Substitute"), DefaultValue("#"), NotifyParentProperty(true), XtraSerializableProperty]
        public string FNC1Substitute { get; set; }

        [Description("Specifies whether or not parentheses should be included in the bar code's text."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.BarCode.DataMatrixGS1Generator.HumanReadableText"), TypeConverter(typeof(BooleanTypeConverter)), DefaultValue(true), NotifyParentProperty(true), XtraSerializableProperty]
        public bool HumanReadableText { get; set; }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override DataMatrixCompactionMode CompactionMode { get; set; }

        public override BarCodeSymbology SymbologyCode { get; }
    }
}

