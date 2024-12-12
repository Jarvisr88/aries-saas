namespace DevExpress.XtraPrinting
{
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting.BrickExporters;
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraReports.UI;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    [BrickExporter(typeof(LineBrickExporter))]
    public class LineBrick : VisualBrick, ILineBrick
    {
        private DashStyle lineStyle;
        private float width;
        private DevExpress.XtraPrinting.HtmlLineDirection htmlLineDirection;
        private DevExpress.XtraReports.UI.LineDirection lineDirection;

        public LineBrick() : this(NullBrickOwner.Instance)
        {
        }

        public LineBrick(IBrickOwner brickOwner) : base(brickOwner)
        {
            this.width = GraphicsUnitConverter.DipToDoc((float) 1f);
            this.lineDirection = DevExpress.XtraReports.UI.LineDirection.Horizontal;
        }

        private LineBrick(LineBrick lineBrick) : base(lineBrick)
        {
            this.width = GraphicsUnitConverter.DipToDoc((float) 1f);
            this.lineDirection = DevExpress.XtraReports.UI.LineDirection.Horizontal;
            this.LineStyle = lineBrick.LineStyle;
            this.LineWidth = lineBrick.LineWidth;
            this.LineDirection = lineBrick.LineDirection;
            this.HtmlLineDirection = lineBrick.HtmlLineDirection;
        }

        internal LineBrick(PrintingSystemBase ps, PointF pt1, PointF pt2, float width)
        {
            this.width = GraphicsUnitConverter.DipToDoc((float) 1f);
            this.lineDirection = DevExpress.XtraReports.UI.LineDirection.Horizontal;
            this.PrintingSystem = ps;
            this.width = width;
            this.InitialRect = RectFBase.FromPoints(pt1, pt2);
            if (base.Width > base.Height)
            {
                base.Height = Math.Max(base.Height, width);
            }
            else
            {
                base.Width = Math.Max(base.Width, width);
            }
            this.lineDirection = GetDirection(pt1, pt2);
        }

        public override object Clone() => 
            new LineBrick(this);

        void ILineBrick.CalculateDirection(PointF pt1, PointF pt2)
        {
            this.LineDirection = GetDirection(pt1, pt2);
        }

        internal static DevExpress.XtraReports.UI.LineDirection GetDirection(PointF pt1, PointF pt2)
        {
            DevExpress.XtraReports.UI.LineDirection slant = DevExpress.XtraReports.UI.LineDirection.Slant;
            if (pt1.X == pt2.X)
            {
                slant = DevExpress.XtraReports.UI.LineDirection.Vertical;
            }
            else if (pt1.Y == pt2.Y)
            {
                slant = DevExpress.XtraReports.UI.LineDirection.Horizontal;
            }
            else if (((pt1.Y - pt2.Y) * (pt1.X - pt2.X)) > 0f)
            {
                slant = DevExpress.XtraReports.UI.LineDirection.BackSlant;
            }
            return slant;
        }

        internal PointF GetPoint1(RectangleF rect) => 
            (this.lineDirection != DevExpress.XtraReports.UI.LineDirection.Horizontal) ? ((this.lineDirection != DevExpress.XtraReports.UI.LineDirection.Vertical) ? ((this.lineDirection != DevExpress.XtraReports.UI.LineDirection.Slant) ? new PointF(rect.Left, rect.Top) : new PointF(rect.Left, rect.Bottom)) : new PointF(rect.Left + (rect.Width / 2f), rect.Top)) : new PointF(rect.Left, rect.Top + (rect.Height / 2f));

        internal PointF GetPoint2(RectangleF rect) => 
            (this.lineDirection != DevExpress.XtraReports.UI.LineDirection.Horizontal) ? ((this.lineDirection != DevExpress.XtraReports.UI.LineDirection.Vertical) ? ((this.lineDirection != DevExpress.XtraReports.UI.LineDirection.Slant) ? new PointF(rect.Right, rect.Bottom) : new PointF(rect.Right, rect.Top)) : new PointF(rect.Left + (rect.Width / 2f), rect.Bottom)) : new PointF(rect.Right, rect.Top + (rect.Height / 2f));

        protected internal override void Scale(double scaleFactor)
        {
            base.Scale(scaleFactor);
            this.width = MathMethods.Scale(this.width, scaleFactor);
        }

        protected internal override float ValidatePageBottomInternal(float pageBottom, RectangleF rect, IPrintingSystemContext context) => 
            pageBottom;

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

        [Description("Gets or sets the line direction."), XtraSerializableProperty, DefaultValue(2)]
        public DevExpress.XtraReports.UI.LineDirection LineDirection
        {
            get => 
                this.lineDirection;
            set => 
                this.lineDirection = value;
        }

        [Description("Gets or sets the color of the line displayed in the current line brick.")]
        public Color ForeColor
        {
            get => 
                base.Style.ForeColor;
            set => 
                base.Style = BrickStyleHelper.Instance.ChangeForeColor(base.Style, value);
        }

        [Description("Gets or sets the style used for a dashed line."), XtraSerializableProperty, DefaultValue(0)]
        public DashStyle LineStyle
        {
            get => 
                this.lineStyle;
            set => 
                this.lineStyle = value;
        }

        [Description("Gets or sets the width of the line displayed in the line brick."), XtraSerializableProperty]
        public float LineWidth
        {
            get => 
                GraphicsUnitConverter.DocToDip(this.width);
            set => 
                this.width = GraphicsUnitConverter.DipToDoc(value);
        }

        internal float DocWidth =>
            this.width;

        [Description("Gets or sets the line direction in HTML output."), XtraSerializableProperty, DefaultValue(0)]
        public DevExpress.XtraPrinting.HtmlLineDirection HtmlLineDirection
        {
            get => 
                this.htmlLineDirection;
            set => 
                this.htmlLineDirection = value;
        }

        [Description("Gets the text string, containing the brick type information.")]
        public override string BrickType =>
            "Line";

        [Description("Overrides the BrickBase.NoClip property to change its return value.")]
        public override bool NoClip =>
            true;

        internal bool ShouldExportAsImage =>
            (this.LineStyle != DashStyle.Solid) || ((this.LineDirection == DevExpress.XtraReports.UI.LineDirection.Slant) || (this.LineDirection == DevExpress.XtraReports.UI.LineDirection.BackSlant));
    }
}

