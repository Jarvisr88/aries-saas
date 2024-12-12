namespace DevExpress.XtraPrinting
{
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting.BrickExporters;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    [BrickExporter(typeof(CheckBoxBrickExporter))]
    public class CheckBoxBrick : VisualBrick, ICheckBoxBrick
    {
        private static SizeF DefaultGlyphSize = new SizeF(13f, 13f);
        private static SizeF DefaultSVGGlyphSize = new SizeF(16f, 16f);
        private string checkText;
        private bool isDefaultGlyphSize;
        private System.Windows.Forms.CheckState checkState;
        private DevExpress.XtraPrinting.GlyphStyle glyphStyle;
        private SizeF glyphSize;
        private CheckBoxGlyphCollection customGlyphs;

        public CheckBoxBrick() : this(NullBrickOwner.Instance)
        {
        }

        public CheckBoxBrick(BrickStyle style) : base(style)
        {
            this.isDefaultGlyphSize = true;
            this.glyphSize = DefaultGlyphSize;
            this.customGlyphs = new CheckBoxGlyphCollection();
        }

        internal CheckBoxBrick(CheckBoxBrick brick) : base(brick)
        {
            this.isDefaultGlyphSize = true;
            this.glyphSize = DefaultGlyphSize;
            this.customGlyphs = new CheckBoxGlyphCollection();
            this.checkState = brick.checkState;
            this.checkText = brick.checkText;
            this.glyphStyle = brick.glyphStyle;
            this.glyphSize = brick.glyphSize;
            this.customGlyphs = new CheckBoxGlyphCollection(brick.customGlyphs);
        }

        public CheckBoxBrick(IBrickOwner brickOwner) : base(brickOwner)
        {
            this.isDefaultGlyphSize = true;
            this.glyphSize = DefaultGlyphSize;
            this.customGlyphs = new CheckBoxGlyphCollection();
        }

        public CheckBoxBrick(BorderSide sides, float borderWidth, Color borderColor, Color backColor) : base(sides, borderWidth, borderColor, backColor)
        {
            this.isDefaultGlyphSize = true;
            this.glyphSize = DefaultGlyphSize;
            this.customGlyphs = new CheckBoxGlyphCollection();
        }

        public CheckBoxBrick(BorderSide sides, float borderWidth, Color borderColor, Color backColor, Color foreColor) : this(sides, borderWidth, borderColor, backColor)
        {
            base.Style.ForeColor = foreColor;
        }

        public override object Clone() => 
            new CheckBoxBrick(this);

        protected override object CreateCollectionItemCore(string propertyName, XtraItemEventArgs e) => 
            (propertyName != "CustomGlyphs") ? base.CreateCollectionItemCore(propertyName, e) : new CheckBoxGlyph();

        internal RectangleF GetCheckRect(IPrintingSystemContext context, RectangleF rect, float dpi)
        {
            RectangleF ef = new RectangleF(PointF.Empty, this.GetScaledGlyphSize(context, dpi));
            return (this.ShouldAlignToBottom ? RectF.Align(ef, rect, BrickAlignment.Center, BrickAlignment.Far) : RectFBase.Center(ef, rect));
        }

        public bool? GetCheckValue()
        {
            System.Windows.Forms.CheckState checkState = this.CheckState;
            if (checkState == System.Windows.Forms.CheckState.Unchecked)
            {
                return false;
            }
            return (checkState == System.Windows.Forms.CheckState.Checked);
        }

        internal static SizeF GetDefaultGlyphSize(DevExpress.XtraPrinting.GlyphStyle glyphStyle) => 
            (glyphStyle != DevExpress.XtraPrinting.GlyphStyle.StandardBox1) ? DefaultSVGGlyphSize : DefaultGlyphSize;

        internal SizeF GetScaledGlyphSize(IPrintingSystemContext context, float dpi) => 
            GraphicsUnitConverter.Convert(MathMethods.Scale(this.GlyphSize, base.GetScaleFactor(context)), (float) 96f, dpi);

        protected override void SetIndexCollectionItemCore(string propertyName, XtraSetItemIndexEventArgs e)
        {
            if (propertyName == "CustomGlyphs")
            {
                this.CustomGlyphs.Add((CheckBoxGlyph) e.Item.Value);
            }
            base.SetIndexCollectionItemCore(propertyName, e);
        }

        protected override bool ShouldSerializeCore(string propertyName) => 
            (propertyName != "GlyphSize") ? base.ShouldSerializeCore(propertyName) : (this.GlyphSize != GetDefaultGlyphSize(this.GlyphStyle));

        internal VisualBrick ParentBrick { get; set; }

        internal override object EditValue
        {
            get => 
                this.CheckState;
            set => 
                this.CheckState = (System.Windows.Forms.CheckState) value;
        }

        [XtraSerializableProperty, DefaultValue((string) null)]
        public string CheckText
        {
            get => 
                this.checkText;
            set => 
                this.checkText = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected internal bool ShouldAlignToBottom { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never), Obsolete("This member has become obsolete.")]
        protected internal float ToDpi
        {
            get => 
                300f;
            set
            {
            }
        }

        internal string CheckStateText =>
            (this.CheckText == null) ? $"[{((this.CheckState == System.Windows.Forms.CheckState.Checked) ? "+" : ((this.CheckState == System.Windows.Forms.CheckState.Unchecked) ? "-" : "?"))}]" : this.CheckText;

        [Description("Gets or sets a value indicating whether the check box can be set to the CheckState.Indeterminate value.")]
        public bool Checked
        {
            get => 
                this.checkState != System.Windows.Forms.CheckState.Unchecked;
            set
            {
                if (value != this.Checked)
                {
                    this.CheckState = value ? System.Windows.Forms.CheckState.Checked : System.Windows.Forms.CheckState.Unchecked;
                }
            }
        }

        [Description("Gets or sets the current state of the CheckBoxBrick object."), XtraSerializableProperty, DefaultValue(0)]
        public System.Windows.Forms.CheckState CheckState
        {
            get => 
                this.checkState;
            set => 
                this.checkState = value;
        }

        [Description("Gets or sets the checkbox glyph style."), XtraSerializableProperty, DefaultValue(0)]
        public DevExpress.XtraPrinting.GlyphStyle GlyphStyle
        {
            get => 
                this.glyphStyle;
            set => 
                this.glyphStyle = value;
        }

        [Description("Gets or sets the checkbox glyph pixel size."), XtraSerializableProperty]
        public SizeF GlyphSize
        {
            get => 
                this.isDefaultGlyphSize ? GetDefaultGlyphSize(this.GlyphStyle) : this.glyphSize;
            set
            {
                this.isDefaultGlyphSize = false;
                this.glyphSize = value;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public SizeF CheckSize =>
            this.GlyphSize;

        [Description("Stores a custom glyph image for each checkbox state (Checked/Unchecked/Indeterminate)."), XtraSerializableProperty(XtraSerializationVisibility.Collection, true, false, false, 0, XtraSerializationFlags.DeserializeCollectionItemBeforeCallSetIndex)]
        public CheckBoxGlyphCollection CustomGlyphs =>
            this.customGlyphs;

        [Description("Gets the text string, containing the brick type information.")]
        public override string BrickType =>
            "CheckBox";
    }
}

