namespace DevExpress.XtraPrinting.BarCode
{
    using DevExpress.Printing;
    using DevExpress.Utils.Design;
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting.BarCode.Native;
    using System;
    using System.Collections;
    using System.ComponentModel;

    public class PDF417Generator : BarCode2DGenerator
    {
        private const string validCharset = "\r\n\t abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789;<>@[\\]_'‘~&!,:#-.$/+“%|*=(^)?{\"}";
        private float yToXRatio;
        private PDF417PatternProcessor pDF417PatternProcessor;
        private PDF417CompactionMode mode;

        public PDF417Generator();
        public PDF417Generator(PDF417Generator source);
        protected override bool BinaryCompactionMode();
        protected override BarCodeGeneratorBase CloneGenerator();
        protected override int GetMaxDataLength();
        protected override string GetValidCharSet();
        protected void Init(PDF417Generator source);
        protected override bool IsValidPattern(ArrayList pattern);
        protected override bool IsValidText(string text);
        protected override bool IsValidTextFormat(string text);
        protected override bool TextCompactionMode();

        [DefaultValue(1), Description("Gets or sets the number of bar code columns, which allows control of the logic width of the bar code."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.BarCode.PDF417Generator.Columns"), XtraSerializableProperty, NotifyParentProperty(true), RefreshProperties(RefreshProperties.All)]
        public int Columns { get; set; }

        [DefaultValue(3), Description("Gets or sets the number of bar code rows, which allows control of the logic height of the bar code."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.BarCode.PDF417Generator.Rows"), XtraSerializableProperty, NotifyParentProperty(true), RefreshProperties(RefreshProperties.All)]
        public int Rows { get; set; }

        [DefaultValue(2), Description("Gets or sets the amount of redundancy built into the bar code's coding, to compensate for calculation errors."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.BarCode.PDF417Generator.ErrorCorrectionLevel"), XtraSerializableProperty, NotifyParentProperty(true), RefreshProperties(RefreshProperties.All)]
        public DevExpress.XtraPrinting.BarCode.ErrorCorrectionLevel ErrorCorrectionLevel { get; set; }

        [DefaultValue(false), TypeConverter(typeof(BooleanTypeConverter)), Description("Gets or sets whether the special end-symbol should be appended to the bar code."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.BarCode.PDF417Generator.TruncateSymbol"), XtraSerializableProperty, NotifyParentProperty(true)]
        public bool TruncateSymbol { get; set; }

        [DefaultValue((float) 3f), Description("Gets or sets the height-to-width ratio of a logical unit's graphic representation."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.BarCode.PDF417Generator.YToXRatio"), XtraSerializableProperty, NotifyParentProperty(true)]
        public virtual float YToXRatio { get; set; }

        [DefaultValue(1), Description("Gets or sets whether textual information or a byte array should be used as the bar code's data."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.BarCode.PDF417Generator.CompactionMode"), XtraSerializableProperty, NotifyParentProperty(true)]
        public PDF417CompactionMode CompactionMode { get; set; }

        [Description("For internal use. Gets the bar code symbology for the current generator object.")]
        public override BarCodeSymbology SymbologyCode { get; }

        protected override float YRatio { get; }

        protected override IPatternProcessor PatternProcessor { get; }

        protected override bool IsSquareBarcode { get; }
    }
}

