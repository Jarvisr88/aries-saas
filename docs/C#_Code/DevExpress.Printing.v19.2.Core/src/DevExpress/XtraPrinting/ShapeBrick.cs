namespace DevExpress.XtraPrinting
{
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting.BrickExporters;
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraPrinting.Shape;
    using DevExpress.XtraPrinting.Shape.Native;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    [BrickExporter(typeof(ShapeBrickExporter))]
    public class ShapeBrick : VisualBrick, IShapeDrawingInfo
    {
        private ShapeBase shape;
        private float lineWidth;
        private DashStyle lineStyle;
        private int angle;
        private Color fillColor;
        private bool stretch;

        public ShapeBrick() : this(NullBrickOwner.Instance)
        {
        }

        public ShapeBrick(IBrickOwner brickOwner) : base(brickOwner)
        {
            this.lineWidth = 1f;
            this.fillColor = Color.Transparent;
            this.shape = ShapeFactory.DefaultFactory.CreateShape().Shape;
        }

        protected override object CreateContentPropertyValue(XtraItemEventArgs e)
        {
            if (e.Item.Name != "Shape")
            {
                return base.CreateContentPropertyValue(e);
            }
            string stringProperty = BrickFactory.GetStringProperty(e, "ShapeName");
            return ShapeFactory.CreateByType((ShapeId) Enum.Parse(typeof(ShapeId), stringProperty));
        }

        protected internal override void Scale(double scaleFactor)
        {
            base.Scale(scaleFactor);
            this.lineWidth = MathMethods.Scale(this.lineWidth, scaleFactor);
        }

        protected override bool ShouldSerializeCore(string propertyName) => 
            (propertyName != "FillColor") ? base.ShouldSerializeCore(propertyName) : (this.FillColor != Color.Transparent);

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string Text
        {
            get => 
                base.Text;
            set => 
                base.Text = value;
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

        [Description("Gets or sets an object which determines a particular shape type along with its settings."), XtraSerializableProperty(XtraSerializationVisibility.Content, true)]
        public ShapeBase Shape
        {
            get => 
                this.shape;
            set => 
                this.shape = value;
        }

        [Description("Gets or sets the width of the line which is used to draw the shape image."), XtraSerializableProperty, DefaultValue((float) 1f)]
        public float LineWidth
        {
            get => 
                this.lineWidth;
            set => 
                this.lineWidth = ShapeHelper.ValidateRestrictedValue(value, 0f, 2.147484E+09f, "LineWidth");
        }

        [Description("Specifies the ShapeBrick's line style."), XtraSerializableProperty, DefaultValue(0)]
        public DashStyle LineStyle
        {
            get => 
                this.lineStyle;
            set => 
                this.lineStyle = value;
        }

        [Description("Gets or sets the angle (in degrees) by which the shape's image is rotated."), XtraSerializableProperty, DefaultValue(0)]
        public int Angle
        {
            get => 
                this.angle;
            set => 
                this.angle = ShapeHelper.ValidateAngle(value);
        }

        [Description("Gets or sets a value indicating whether or not to stretch a shape when it's rotated."), XtraSerializableProperty, DefaultValue(false)]
        public bool Stretch
        {
            get => 
                this.stretch;
            set => 
                this.stretch = value;
        }

        [Description("Gets or sets the color to fill the shape's contents."), XtraSerializableProperty]
        public Color FillColor
        {
            get => 
                this.fillColor;
            set => 
                this.fillColor = value;
        }

        [Description("Gets or sets the color of the shape displayed in the current brick.")]
        public Color ForeColor
        {
            get => 
                base.Style.ForeColor;
            set => 
                base.Style = BrickStyleHelper.Instance.ChangeForeColor(base.Style, value);
        }

        [Description("Gets the text string, containing the brick type information.")]
        public override string BrickType =>
            "Shape";
    }
}

