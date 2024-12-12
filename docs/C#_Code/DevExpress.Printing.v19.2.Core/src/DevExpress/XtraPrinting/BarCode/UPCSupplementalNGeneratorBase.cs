namespace DevExpress.XtraPrinting.BarCode
{
    using System;
    using System.Collections;
    using System.ComponentModel;

    public abstract class UPCSupplementalNGeneratorBase : BarCodeGeneratorBase
    {
        private static string validCharSet;

        static UPCSupplementalNGeneratorBase();
        protected UPCSupplementalNGeneratorBase();
        protected UPCSupplementalNGeneratorBase(UPCSupplementalNGeneratorBase source);
        protected abstract int CalcCheckDigit(string text);
        protected override string FormatText(string text);
        protected abstract int GetMaxCharsCount();
        protected abstract string GetParityString(int checkDigit);
        protected override Hashtable GetPatternTable();
        protected override string GetValidCharSet();
        protected override char[] PrepareText(string text);

        [DefaultValue(true), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public override bool CalcCheckSum { get; set; }
    }
}

