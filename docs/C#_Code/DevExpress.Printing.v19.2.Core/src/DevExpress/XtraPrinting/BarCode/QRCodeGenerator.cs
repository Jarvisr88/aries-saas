namespace DevExpress.XtraPrinting.BarCode
{
    using DevExpress.Printing;
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BarCode.Native;
    using DevExpress.XtraPrinting.Drawing;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;

    public class QRCodeGenerator : BarCode2DGenerator
    {
        private QRCodeCompactionMode mode;
        private ImageSource logo;
        private float scaleFactor;
        private DevExpress.XtraPrinting.BarCode.Native.QRCodePatternProcessor patternProcessor;

        public QRCodeGenerator();
        public QRCodeGenerator(QRCodeGenerator source);
        protected override bool BinaryCompactionMode();
        protected override BarCodeGeneratorBase CloneGenerator();
        protected override void DrawBarCode(IGraphicsBase gr, RectangleF barBounds, RectangleF textBounds, IBarCodeData data, float xModule, float yModule);
        private void DrawLogo(IGraphicsBase gr, float xModule, float yModule, PointF barLocation);
        private int GetLogoModuleCount();
        protected override int GetMaxDataLength();
        protected override string GetValidCharSet();
        private void Init(QRCodeGenerator source);
        protected override bool IsCorrectSettings();
        protected override bool IsValidPattern(ArrayList pattern);
        protected override bool IsValidText(string text);
        protected override void RefreshPatternProcessor();
        internal override void Scale(double scaleFactor);
        protected override bool TextCompactionMode();

        private DevExpress.XtraPrinting.BarCode.Native.QRCodePatternProcessor QRCodePatternProcessor { get; }

        [DefaultValue(1), Description("Gets or sets whether numeric, alpha-numeric or byte information should be used as the bar code's data."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.BarCode.QRCodeGenerator.CompactionMode"), XtraSerializableProperty, NotifyParentProperty(true), RefreshProperties(RefreshProperties.All)]
        public QRCodeCompactionMode CompactionMode { get; set; }

        [DefaultValue(1), Description("Gets or sets the amount of redundancy built into the bar code's coding, to compensate for calculation errors."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.BarCode.QRCodeGenerator.ErrorCorrectionLevel"), XtraSerializableProperty, NotifyParentProperty(true), RefreshProperties(RefreshProperties.All)]
        public QRCodeErrorCorrectionLevel ErrorCorrectionLevel { get; set; }

        [DefaultValue(0), Description("Gets or sets the bar code's size."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.BarCode.QRCodeGenerator.Version"), XtraSerializableProperty, NotifyParentProperty(true), RefreshProperties(RefreshProperties.All)]
        public QRCodeVersion Version { get; set; }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DefaultValue((float) 1f), XtraSerializableProperty]
        public float ScaleFactor { get; set; }

        [DefaultValue((string) null), Description("Specifies the image that overlays the QR code."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.BarCode.QRCodeGenerator.Logo"), XtraSerializableProperty, NotifyParentProperty(true), RefreshProperties(RefreshProperties.All)]
        public ImageSource Logo { get; set; }

        private bool HasLogo { get; }

        internal bool HasModuleValue { get; }

        private SizeF LogoSize { get; }

        protected override IPatternProcessor PatternProcessor { get; }

        protected override bool IsSquareBarcode { get; }

        public override BarCodeSymbology SymbologyCode { get; }
    }
}

