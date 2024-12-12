namespace DevExpress.XtraPrinting.BarCode
{
    using DevExpress.Printing;
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting.BarCode.Native;
    using System;
    using System.Collections;
    using System.ComponentModel;

    public class CodeMSIGenerator : BarCodeGeneratorBase
    {
        private static string validCharSet;
        private static Hashtable charPattern;
        protected const DevExpress.XtraPrinting.BarCode.MSICheckSum defaultCheckSum = DevExpress.XtraPrinting.BarCode.MSICheckSum.Modulo10;
        private DevExpress.XtraPrinting.BarCode.MSICheckSum checkSum;

        static CodeMSIGenerator();
        public CodeMSIGenerator();
        private CodeMSIGenerator(CodeMSIGenerator source);
        private static char CalcCheckDigit(string text);
        private string CalculateCheckSum(string text);
        protected override BarCodeGeneratorBase CloneGenerator();
        protected override Hashtable GetPatternTable();
        protected override float GetPatternWidth(char pattern);
        protected override string GetValidCharSet();
        protected internal static string Mul2(string number);
        protected override char[] PrepareText(string text);

        [DefaultValue(true), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public override bool CalcCheckSum { get; set; }

        [Description("Gets or sets the checksum type for the bar code."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.BarCode.CodeMSIGenerator.MSICheckSum"), DefaultValue(1), NotifyParentProperty(true), XtraSerializableProperty]
        public DevExpress.XtraPrinting.BarCode.MSICheckSum MSICheckSum { get; set; }

        [Description("For internal use. Gets the bar code symbology for the current generator object.")]
        public override BarCodeSymbology SymbologyCode { get; }
    }
}

