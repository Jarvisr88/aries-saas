namespace DevExpress.XtraPrinting
{
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting.BarCode;
    using DevExpress.XtraPrinting.BarCode.Native;
    using DevExpress.XtraPrinting.BrickExporters;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.ComponentModel;

    [BrickExporter(typeof(BarCodeBrickExporter))]
    public class BarCodeBrick : TextBrickBase, IBarCodeData
    {
        public const float DefaultModule = 2f;
        public const bool DefaultAutoModule = false;
        public const bool DefaultShowText = true;
        public const TextAlignment DefaultAlignment = TextAlignment.TopLeft;
        public const BarCodeOrientation DefaultOrientation = BarCodeOrientation.Normal;
        [EditorBrowsable(EditorBrowsableState.Never)]
        public const string DefaultBinaryDataBase64 = "";
        private double module;
        private bool autoModule;
        private bool showText;
        private BarCodeOrientation orientation;
        private BarCodeGeneratorBase generator;
        private TextAlignment alignment;
        private byte[] binaryData;
        private float fromDpi;
        private float toDpi;

        public BarCodeBrick() : this(NullBrickOwner.Instance)
        {
        }

        internal BarCodeBrick(BarCodeBrick barCodeBrick) : base(barCodeBrick)
        {
            this.module = 2.0;
            this.showText = true;
            this.generator = new Code128Generator();
            this.alignment = TextAlignment.TopLeft;
            this.binaryData = new byte[0];
            this.fromDpi = 300f;
            this.toDpi = 300f;
            this.alignment = barCodeBrick.Alignment;
            this.autoModule = barCodeBrick.AutoModule;
            this.binaryData = barCodeBrick.BinaryData;
            this.generator = barCodeBrick.Generator;
            this.module = barCodeBrick.Module;
            this.orientation = barCodeBrick.Orientation;
            this.showText = barCodeBrick.ShowText;
            this.toDpi = barCodeBrick.ToDpi;
            this.fromDpi = barCodeBrick.FromDpi;
        }

        public BarCodeBrick(IBrickOwner brickOwner) : base(brickOwner)
        {
            this.module = 2.0;
            this.showText = true;
            this.generator = new Code128Generator();
            this.alignment = TextAlignment.TopLeft;
            this.binaryData = new byte[0];
            this.fromDpi = 300f;
            this.toDpi = 300f;
            this.Text = string.Empty;
        }

        public override object Clone() => 
            new BarCodeBrick(this);

        protected override object CreateContentPropertyValue(XtraItemEventArgs e)
        {
            BarCodeSymbology codabar;
            if (e.Item.Name != "Generator")
            {
                return base.CreateContentPropertyValue(e);
            }
            try
            {
                codabar = (BarCodeSymbology) Enum.Parse(typeof(BarCodeSymbology), BrickFactory.GetStringProperty(e, "Name"));
            }
            catch
            {
                codabar = BarCodeSymbology.Codabar;
            }
            return BarCodeGeneratorFactory.Create(codabar);
        }

        protected internal override void Scale(double scaleFactor)
        {
            base.Scale(scaleFactor);
            this.module = MathMethods.Scale(this.module, scaleFactor);
            this.generator.Scale(scaleFactor);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override object TextValue
        {
            get => 
                base.TextValue;
            set => 
                base.TextValue = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string TextValueFormatString
        {
            get => 
                base.TextValueFormatString;
            set => 
                base.TextValueFormatString = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string XlsxFormatString
        {
            get => 
                base.XlsxFormatString;
            set => 
                base.XlsxFormatString = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected internal float ToDpi
        {
            get => 
                this.toDpi;
            set => 
                this.toDpi = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected internal float FromDpi
        {
            get => 
                this.fromDpi;
            set => 
                this.fromDpi = value;
        }

        [Description("Gets or sets the width of the narrowest bar or space in the current BarCodeBrick instance."), XtraSerializableProperty, DefaultValue((double) 2.0)]
        public double Module
        {
            get => 
                (this.FromDpi == this.ToDpi) ? this.module : ((double) GraphicsUnitConverter.Convert((float) this.module, this.FromDpi, this.ToDpi));
            set => 
                this.module = value;
        }

        [Description("Gets or sets a value that specifies whether the BarCodeBrick.Module property value should be calculated automatically based upon the bar code's size."), XtraSerializableProperty, DefaultValue(false)]
        public bool AutoModule
        {
            get => 
                this.autoModule;
            set => 
                this.autoModule = value;
        }

        [Description("Gets or sets a value indicating whether the text is displayed in this BarCodeBrick."), XtraSerializableProperty, DefaultValue(true)]
        public bool ShowText
        {
            get => 
                this.showText;
            set => 
                this.showText = value;
        }

        [Description("Gets or sets how a barcode should be rotated in a report."), XtraSerializableProperty, DefaultValue(0)]
        public BarCodeOrientation Orientation
        {
            get => 
                this.orientation;
            set => 
                this.orientation = value;
        }

        [Description("Gets or sets the alignment of the barcode in the brick rectangle."), XtraSerializableProperty, DefaultValue(1)]
        public TextAlignment Alignment
        {
            get => 
                this.alignment;
            set => 
                this.alignment = value;
        }

        [Description("Gets or sets the symbology (code type) for the barcode and the text displayed in the BarCodeBrick."), XtraSerializableProperty(XtraSerializationVisibility.Content, true)]
        public BarCodeGeneratorBase Generator
        {
            get => 
                this.generator;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }
                this.generator = value;
            }
        }

        [Description("Gets the text string, containing the brick type information.")]
        public override string BrickType =>
            "BarCode";

        [Description("Gets or sets the byte array to be coded into the PDF417 bar code.")]
        public byte[] BinaryData
        {
            get => 
                this.binaryData;
            set
            {
                if (value != null)
                {
                    this.binaryData = value;
                }
            }
        }

        [Description("For internal use only."), XtraSerializableProperty, DefaultValue(""), EditorBrowsable(EditorBrowsableState.Never)]
        public string BinaryDataBase64
        {
            get => 
                Convert.ToBase64String(this.BinaryData);
            set => 
                this.BinaryData = Convert.FromBase64String(value);
        }

        public override string Text
        {
            get => 
                base.Text;
            set => 
                base.Text = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public string DisplayText =>
            this.generator.GetDisplayText(this);
    }
}

