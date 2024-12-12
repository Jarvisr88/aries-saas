namespace DevExpress.XtraPrinting.NativeBricks
{
    using DevExpress.Utils;
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BrickExporters;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    [BrickExporter(typeof(CheckBoxTextBrickExporter))]
    public class CheckBoxTextBrick : ContainerBrickBase, ICheckBoxBrick
    {
        private HorzAlignment checkBoxAlignment;
        private DevExpress.XtraPrinting.TextBrick textBrick;
        private DevExpress.XtraPrinting.CheckBoxBrick checkBoxBrick;

        public CheckBoxTextBrick() : this(NullBrickOwner.Instance)
        {
        }

        public CheckBoxTextBrick(BrickStyle style) : base(style)
        {
            this.checkBoxAlignment = HorzAlignment.Near;
            this.textBrick = new DevExpress.XtraPrinting.TextBrick();
            DevExpress.XtraPrinting.CheckBoxBrick brick1 = new DevExpress.XtraPrinting.CheckBoxBrick();
            brick1.ParentBrick = this;
            this.checkBoxBrick = brick1;
        }

        public CheckBoxTextBrick(IBrickOwner brickOwner) : base(brickOwner)
        {
            this.checkBoxAlignment = HorzAlignment.Near;
            this.textBrick = new DevExpress.XtraPrinting.TextBrick();
            DevExpress.XtraPrinting.CheckBoxBrick brick1 = new DevExpress.XtraPrinting.CheckBoxBrick();
            brick1.ParentBrick = this;
            this.checkBoxBrick = brick1;
        }

        private void AddBricks()
        {
            this.Bricks.Add(this.checkBoxBrick);
            if (!this.IsTextBrickEmpty)
            {
                this.Bricks.Add(this.textBrick);
            }
        }

        protected override object CreateCollectionItemCore(string propertyName, XtraItemEventArgs e) => 
            (propertyName != "CustomGlyphs") ? base.CreateCollectionItemCore(propertyName, e) : new CheckBoxGlyph();

        internal void EnsureTextBrick()
        {
            if (!this.Bricks.Contains(this.TextBrick))
            {
                this.InitTextBrick(this.GetClientRect(), this.CheckBoxBrick.InitialRect.Size, this.GetActualCheckBoxAlignment());
                this.Bricks.Add(this.TextBrick);
            }
        }

        internal BrickAlignment GetActualCheckBoxAlignment()
        {
            BrickAlignment alignment = (this.CheckBoxAlignment == HorzAlignment.Near) ? BrickAlignment.Near : BrickAlignment.Far;
            if (base.Style.StringFormat.RightToLeft && !this.RightToLeftLayout)
            {
                alignment = (alignment == BrickAlignment.Near) ? BrickAlignment.Far : BrickAlignment.Near;
            }
            return alignment;
        }

        private RectangleF GetClientRect()
        {
            RectangleF rect = new RectangleF(PointF.Empty, this.Rect.Size);
            return base.Padding.Deflate(this.GetClientRectangle(rect, 300f), 300f);
        }

        private void InitializeInnerBricksStyles()
        {
            BrickStyle style1 = new BrickStyle(base.Style);
            style1.Sides = BorderSide.None;
            style1.Padding = PaddingInfo.Empty;
            style1.BackColor = Color.Transparent;
            BrickStyle src = style1;
            this.TextBrick.Style = new BrickStyle(src);
            this.CheckBoxBrick.Style = src;
        }

        private unsafe void InitTextBrick(RectangleF clientRect, SizeF checkBoxSize, BrickAlignment checkBoxAlignment)
        {
            RectangleF ef = clientRect;
            float num = checkBoxSize.Width + GraphicsUnitConverter.Convert((float) 2f, (float) 96f, (float) 300f);
            if (checkBoxAlignment == BrickAlignment.Near)
            {
                RectangleF* efPtr1 = &ef;
                efPtr1.X += num;
            }
            RectangleF* efPtr2 = &ef;
            efPtr2.Width -= num;
            this.TextBrick.InitialRect = ef;
        }

        protected override void OnSetPrintingSystem(bool cacheStyle)
        {
            this.Bricks.Clear();
            this.AddBricks();
            base.OnSetPrintingSystem(cacheStyle);
            this.InitializeInnerBricksStyles();
            RectangleF clientRect = this.GetClientRect();
            SizeF size = GraphicsUnitConverter.Convert(this.GlyphSize, (float) 96f, (float) 300f);
            BrickAlignment actualCheckBoxAlignment = this.GetActualCheckBoxAlignment();
            this.CheckBoxBrick.InitialRect = RectF.Align(new RectangleF(PointF.Empty, size), clientRect, actualCheckBoxAlignment, BrickAlignment.Center);
            if (!this.IsTextBrickEmpty)
            {
                this.InitTextBrick(clientRect, size, actualCheckBoxAlignment);
            }
        }

        protected override void SetIndexCollectionItemCore(string propertyName, XtraSetItemIndexEventArgs e)
        {
            if (propertyName == "CustomGlyphs")
            {
                this.CustomGlyphs.Add((CheckBoxGlyph) e.Item.Value);
            }
            base.SetIndexCollectionItemCore(propertyName, e);
        }

        protected override bool ShouldSerializeCore(string propertyName) => 
            (propertyName != "GlyphSize") ? base.ShouldSerializeCore(propertyName) : (this.GlyphSize != DevExpress.XtraPrinting.CheckBoxBrick.GetDefaultGlyphSize(this.GlyphStyle));

        internal DevExpress.XtraPrinting.CheckBoxBrick CheckBoxBrick =>
            this.checkBoxBrick;

        internal DevExpress.XtraPrinting.TextBrick TextBrick =>
            this.textBrick;

        private bool IsTextBrickEmpty =>
            string.IsNullOrEmpty(this.textBrick.Text);

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
                this.CheckBoxBrick.CheckText;
            set => 
                this.CheckBoxBrick.CheckText = value;
        }

        internal string CheckStateText =>
            this.CheckBoxBrick.CheckStateText;

        [XtraSerializableProperty, DefaultValue(0)]
        public System.Windows.Forms.CheckState CheckState
        {
            get => 
                this.CheckBoxBrick.CheckState;
            set => 
                this.CheckBoxBrick.CheckState = value;
        }

        [XtraSerializableProperty, DefaultValue("")]
        public override string Text
        {
            get => 
                this.TextBrick.Text;
            set => 
                this.TextBrick.Text = value;
        }

        public override string BrickType =>
            "CheckBoxText";

        [XtraSerializableProperty, DefaultValue((string) null)]
        public override object TextValue
        {
            get => 
                this.TextBrick.TextValue;
            set => 
                this.TextBrick.TextValue = value;
        }

        [XtraSerializableProperty, DefaultValue((string) null)]
        public override string TextValueFormatString
        {
            get => 
                this.TextBrick.TextValueFormatString;
            set => 
                this.TextBrick.TextValueFormatString = value;
        }

        [XtraSerializableProperty, DefaultValue((string) null)]
        public override string XlsxFormatString
        {
            get => 
                this.TextBrick.XlsxFormatString;
            set => 
                this.TextBrick.XlsxFormatString = value;
        }

        [XtraSerializableProperty, DefaultValue(1)]
        public HorzAlignment CheckBoxAlignment
        {
            get => 
                this.checkBoxAlignment;
            set
            {
                if ((value != HorzAlignment.Near) && (value != HorzAlignment.Far))
                {
                    throw new NotSupportedException();
                }
                this.checkBoxAlignment = value;
            }
        }

        [XtraSerializableProperty, DefaultValue(0)]
        public DevExpress.XtraPrinting.GlyphStyle GlyphStyle
        {
            get => 
                this.CheckBoxBrick.GlyphStyle;
            set => 
                this.CheckBoxBrick.GlyphStyle = value;
        }

        [XtraSerializableProperty]
        public SizeF GlyphSize
        {
            get => 
                this.CheckBoxBrick.GlyphSize;
            set => 
                this.CheckBoxBrick.GlyphSize = value;
        }

        [XtraSerializableProperty(XtraSerializationVisibility.Collection, true, false, false, 0, XtraSerializationFlags.DeserializeCollectionItemBeforeCallSetIndex)]
        public CheckBoxGlyphCollection CustomGlyphs =>
            this.CheckBoxBrick.CustomGlyphs;
    }
}

