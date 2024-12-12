namespace DevExpress.XtraPrinting.NativeBricks
{
    using DevExpress.Utils;
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BrickExporters;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;

    [BrickExporter(typeof(ToggleSwitchTextBrickExporter))]
    public class ToggleSwitchTextBrick : ContainerBrickBase
    {
        private HorzAlignment checkBoxAlignment;

        public ToggleSwitchTextBrick() : this(NullBrickOwner.Instance)
        {
        }

        public ToggleSwitchTextBrick(BrickStyle style) : base(style)
        {
            this.checkBoxAlignment = HorzAlignment.Near;
            this.Bricks.Add(new DevExpress.XtraPrinting.ToggleSwitchBrick());
            this.Bricks.Add(new DevExpress.XtraPrinting.TextBrick());
        }

        public ToggleSwitchTextBrick(IBrickOwner brickOwner) : base(brickOwner)
        {
            this.checkBoxAlignment = HorzAlignment.Near;
            this.Bricks.Add(new DevExpress.XtraPrinting.ToggleSwitchBrick());
            this.Bricks.Add(new DevExpress.XtraPrinting.TextBrick());
        }

        private void InitializeInnerBricksStyles()
        {
            BrickStyle style = new BrickStyle(base.Style) {
                Sides = BorderSide.None,
                Padding = PaddingInfo.Empty
            };
            this.TextBrick.Style = style;
            this.ToggleSwitchBrick.Style = this.TextBrick.Style;
        }

        protected override unsafe void OnSetPrintingSystem(bool cacheStyle)
        {
            base.OnSetPrintingSystem(cacheStyle);
            this.InitializeInnerBricksStyles();
            RectangleF rect = new RectangleF(PointF.Empty, this.Rect.Size);
            RectangleF baseRect = base.Padding.Deflate(this.GetClientRectangle(rect, 300f), 300f);
            SizeF checkSize = this.ToggleSwitchBrick.CheckSize;
            this.ToggleSwitchBrick.InitialRect = RectF.Align(new RectangleF(PointF.Empty, checkSize), baseRect, (this.CheckBoxAlignment == HorzAlignment.Near) ? BrickAlignment.Near : BrickAlignment.Far, BrickAlignment.Center);
            RectangleF ef4 = baseRect;
            float num = checkSize.Width + GraphicsUnitConverter.Convert((float) 2f, (float) 96f, (float) 300f);
            if (this.CheckBoxAlignment == HorzAlignment.Near)
            {
                RectangleF* efPtr1 = &ef4;
                efPtr1.X += num;
            }
            RectangleF* efPtr2 = &ef4;
            efPtr2.Width -= num;
            this.TextBrick.InitialRect = ef4;
        }

        internal DevExpress.XtraPrinting.ToggleSwitchBrick ToggleSwitchBrick =>
            (DevExpress.XtraPrinting.ToggleSwitchBrick) this.Bricks[0];

        internal DevExpress.XtraPrinting.TextBrick TextBrick =>
            (DevExpress.XtraPrinting.TextBrick) this.Bricks[1];

        [XtraSerializableProperty, DefaultValue((string) null)]
        public string CheckText
        {
            get => 
                this.ToggleSwitchBrick.CheckText;
            set => 
                this.ToggleSwitchBrick.CheckText = value;
        }

        internal string CheckStateText =>
            this.ToggleSwitchBrick.CheckStateText;

        [XtraSerializableProperty, DefaultValue(false)]
        public bool IsOn
        {
            get => 
                this.ToggleSwitchBrick.IsOn;
            set => 
                this.ToggleSwitchBrick.IsOn = value;
        }

        public ArrayList ImageList
        {
            get => 
                this.ToggleSwitchBrick.ImageList;
            set => 
                this.ToggleSwitchBrick.ImageList = value;
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
            "ToggleSwitchText";

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
    }
}

