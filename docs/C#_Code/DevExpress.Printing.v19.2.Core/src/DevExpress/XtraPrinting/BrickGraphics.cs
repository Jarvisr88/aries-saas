namespace DevExpress.XtraPrinting
{
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraPrinting.NativeBricks;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class BrickGraphics : IDisposable, IBrickGraphics
    {
        [ThreadStatic]
        private static BrickStyle internalBrickStyle;
        [ThreadStatic]
        private static BrickStyle internalBrickStyleDefault;
        [ThreadStatic]
        private static Stack<BrickStyle> brickStyleStack;
        internal static Color DefaultPageBackColor = SystemColors.Window;
        private int stackLevel;
        private float dpi = GraphicsDpi.Pixel;
        private Color pageBackColor;
        private BrickModifier modifier;
        private PrintingSystemBase ps;
        private GraphicsUnit pageUnit = GraphicsUnit.Pixel;
        private bool deviceIndependentPixel;
        private PanelBrick unionBrick;
        private UnionCollection unionBricks = new UnionCollection();
        private Action<Brick> brickCollector;

        internal event EventHandler ModifierChanged;

        public BrickGraphics(PrintingSystemBase ps)
        {
            this.ps = ps;
            this.pageBackColor = DefaultPageBackColor;
            this.modifier = BrickModifier.None;
        }

        protected virtual Brick AddBrick(Brick brick)
        {
            if (brick != null)
            {
                brick.Modifier = this.modifier;
                if (this.unionBrick != null)
                {
                    this.unionBricks.Add(brick);
                }
                else
                {
                    this.BrickCollector(brick);
                }
            }
            return brick;
        }

        [Obsolete("This member has become obsolete.")]
        public void BeginCalculateRectangle()
        {
        }

        public void BeginUnionRect()
        {
            this.unionBrick = this.CreatePanelUnionBrick();
            this.unionBrick.Modifier = this.Modifier;
            this.unionBrick.NoClip = true;
        }

        internal void Clear()
        {
            this.ClearInternalBrickStyle();
            if (internalBrickStyleDefault != null)
            {
                internalBrickStyleDefault.Dispose();
                internalBrickStyleDefault = null;
            }
        }

        private void ClearInternalBrickStyle()
        {
            BrickStyle objA = null;
            while (true)
            {
                if (BrickStyleStack.Count >= this.stackLevel)
                {
                    objA = (BrickStyleStack.Count > 0) ? BrickStyleStack.Pop() : null;
                    if (!ReferenceEquals(objA, internalBrickStyle) && (internalBrickStyle != null))
                    {
                        internalBrickStyle.Dispose();
                    }
                    internalBrickStyle = objA;
                    if (BrickStyleStack.Count != 0)
                    {
                        continue;
                    }
                }
                return;
            }
        }

        private SizeF ConverFromMeasuringUnit(SizeF result) => 
            this.IsUsingDip ? GraphicsUnitConverter.Convert(result, (float) 300f, this.dpi) : result;

        private EmptyBrick CreateEmptyBrick(RectangleF rect)
        {
            EmptyBrick brick = new EmptyBrick();
            this.InitializeBrick(brick, rect);
            return brick;
        }

        private PanelBrick CreatePanelUnionBrick()
        {
            PanelBrick brick = new PanelBrick {
                BackColor = Color.Transparent,
                BorderWidth = 0f
            };
            this.InitializeBrick(brick, RectangleF.Empty);
            return brick;
        }

        private Brick CreateUserBrick(IBrick userBrick) => 
            ((this.modifier & (BrickModifier.MarginalFooter | BrickModifier.MarginalHeader)) <= BrickModifier.None) ? new UserVisualBrick(userBrick) : new UserPageBrick(userBrick);

        IBrick IBrickGraphics.DrawBrick(IBrick userBrick, RectangleF rect)
        {
            if (userBrick is Brick)
            {
                return this.DrawBrick((Brick) userBrick, rect);
            }
            Brick brick = this.CreateUserBrick(userBrick);
            return this.DrawBrick(brick, rect);
        }

        void IBrickGraphics.RaiseModifierChanged()
        {
            this.RaiseModifierChangedInternal();
        }

        public float DocumValueOf(float val) => 
            GraphicsUnitConverter.Convert(val, this.dpi, (float) 300f);

        public Brick DrawBrick(Brick brick)
        {
            this.InitializeBrick(brick, brick.Rect);
            return this.AddBrick(brick);
        }

        public Brick DrawBrick(Brick brick, RectangleF rect)
        {
            this.InitializeBrick(brick, rect);
            return this.AddBrick(brick);
        }

        public CheckBoxBrick DrawCheckBox(RectangleF rect, bool check)
        {
            CheckBoxBrick brick = new CheckBoxBrick();
            this.InitializeBrick(brick, rect);
            brick.Style = new BrickStyle(this.DefaultBrickStyle);
            brick.Checked = check;
            return (CheckBoxBrick) this.AddBrick(brick);
        }

        public CheckBoxBrick DrawCheckBox(RectangleF rect, BorderSide sides, Color backColor, bool check)
        {
            CheckBoxBrick brick = new CheckBoxBrick();
            this.InitializeBrick(brick, rect);
            brick.Style = new BrickStyle(sides, this.BorderWidth, this.BorderColor, backColor.IsEmpty ? this.BackColor : backColor, this.ForeColor, this.Font, this.StringFormat);
            brick.Checked = check;
            return (CheckBoxBrick) this.AddBrick(brick);
        }

        public EmptyBrick DrawEmptyBrick(RectangleF rect) => 
            (EmptyBrick) this.AddBrick(this.CreateEmptyBrick(rect));

        public ImageBrick DrawImage(System.Drawing.Image image, RectangleF rect)
        {
            ImageBrick brick = new ImageBrick();
            this.InitializeBrick(brick, rect);
            brick.Style = new BrickStyle(this.DefaultBrickStyle);
            brick.Image = image;
            return (ImageBrick) this.AddBrick(brick);
        }

        public ImageBrick DrawImage(System.Drawing.Image image, RectangleF rect, BorderSide sides, Color backColor)
        {
            ImageBrick brick = new ImageBrick();
            this.InitializeBrick(brick, rect);
            brick.Style = new BrickStyle(sides, this.BorderWidth, this.BorderColor, backColor.IsEmpty ? this.BackColor : backColor, this.ForeColor, this.Font, this.StringFormat);
            brick.Image = image;
            return (ImageBrick) this.AddBrick(brick);
        }

        public LineBrick DrawLine(PointF pt1, PointF pt2, Color foreColor, float width)
        {
            LineBrick brick = new LineBrick(this.ps, GraphicsUnitConverter.Convert(pt1, this.dpi, (float) 300f), GraphicsUnitConverter.Convert(pt2, this.dpi, (float) 300f), GraphicsUnitConverter.PixelToDoc(width)) {
                ForeColor = foreColor
            };
            return (LineBrick) this.AddBrick(brick);
        }

        public PageImageBrick DrawPageImage(System.Drawing.Image image, RectangleF rect, BorderSide sides, Color backColor)
        {
            PageImageBrick brick = new PageImageBrick();
            this.InitializeBrick(brick, rect);
            brick.Style = new BrickStyle(sides, this.BorderWidth, this.BorderColor, backColor.IsEmpty ? this.BackColor : backColor, this.ForeColor, this.Font, this.StringFormat);
            brick.Image = image;
            return (PageImageBrick) this.AddBrick(brick);
        }

        public PageInfoBrick DrawPageInfo(PageInfo pageInfo, string format, Color foreColor, RectangleF rect, BorderSide sides)
        {
            PageInfoBrick brick = new PageInfoBrick();
            this.InitializeBrick(brick, rect);
            brick.Style = new BrickStyle(sides, this.BorderWidth, this.BorderColor, this.BackColor, foreColor.IsEmpty ? this.ForeColor : foreColor, this.Font, this.StringFormat);
            brick.Format = format;
            brick.PageInfo = pageInfo;
            return (PageInfoBrick) this.AddBrick(brick);
        }

        public VisualBrick DrawRect(RectangleF rect, BorderSide sides, Color backColor, Color borderColor)
        {
            SeparableBrick brick = new SeparableBrick();
            this.InitializeBrick(brick, rect);
            brick.Style = new BrickStyle(sides, this.BorderWidth, borderColor.IsEmpty ? this.BorderColor : borderColor, backColor.IsEmpty ? this.BackColor : backColor, this.ForeColor, this.Font, this.StringFormat);
            return (VisualBrick) this.AddBrick(brick);
        }

        public TextBrick DrawString(string text, RectangleF rect)
        {
            TextBrick brick = new TextBrick();
            this.InitializeBrick(brick, rect);
            brick.Style = new BrickStyle(this.DefaultBrickStyle);
            brick.Text = text;
            return (TextBrick) this.AddBrick(brick);
        }

        public TextBrick DrawString(string text, Color foreColor, RectangleF rect, BorderSide sides)
        {
            TextBrick brick = new TextBrick();
            this.InitializeBrick(brick, rect);
            brick.Style = new BrickStyle(sides, this.BorderWidth, this.BorderColor, this.BackColor, foreColor.IsEmpty ? this.ForeColor : foreColor, this.Font, this.StringFormat);
            brick.Text = text;
            return (TextBrick) this.AddBrick(brick);
        }

        [Obsolete("This member has become obsolete.")]
        public RectangleF EndCalculateRectangle() => 
            RectangleF.Empty;

        public void EndUnionRect()
        {
            if (this.unionBrick != null)
            {
                this.unionBricks.Unite(this.unionBrick);
                PanelBrick unionBrick = this.unionBrick;
                this.unionBrick = null;
                this.AddBrick(unionBrick);
            }
        }

        private GraphicsUnit GetMeasuringPageUnit() => 
            this.IsUsingDip ? GraphicsUnit.Document : this.pageUnit;

        internal void Init()
        {
            BrickStyleStack.Push(internalBrickStyle);
            this.stackLevel = BrickStyleStack.Count;
        }

        internal void InitializeBrick(Brick brick, RectangleF rect)
        {
            this.InitializeBrick(brick, rect, true);
        }

        internal void InitializeBrick(Brick brick, RectangleF rect, bool cacheStyle)
        {
            brick.Initialize(this.ps, GraphicsUnitConverter.Convert(rect, this.Dpi, (float) 300f), cacheStyle);
        }

        public SizeF MeasureString(string text)
        {
            SizeF result = this.Measurer.MeasureString(text, this.Font, this.GetMeasuringPageUnit());
            return this.ConverFromMeasuringUnit(result);
        }

        public SizeF MeasureString(string text, System.Drawing.Font font)
        {
            SizeF result = this.Measurer.MeasureString(text, font, this.GetMeasuringPageUnit());
            return this.ConverFromMeasuringUnit(result);
        }

        public SizeF MeasureString(string text, int width)
        {
            SizeF result = this.Measurer.MeasureString(text, this.Font, (float) width, null, this.GetMeasuringPageUnit());
            return this.ConverFromMeasuringUnit(result);
        }

        public SizeF MeasureString(string text, int width, System.Drawing.StringFormat stringFormat)
        {
            SizeF result = this.Measurer.MeasureString(text, this.Font, (float) width, stringFormat, this.GetMeasuringPageUnit());
            return this.ConverFromMeasuringUnit(result);
        }

        public SizeF MeasureString(string text, System.Drawing.Font font, int width, System.Drawing.StringFormat stringFormat)
        {
            SizeF result = this.Measurer.MeasureString(text, font, (float) width, stringFormat, this.GetMeasuringPageUnit());
            return this.ConverFromMeasuringUnit(result);
        }

        public static SizeF MeasureString(string text, System.Drawing.Font font, int width, System.Drawing.StringFormat stringFormat, GraphicsUnit pageUnit) => 
            Measurement.MeasureString(text, font, (float) width, stringFormat, pageUnit);

        private void RaiseModifierChangedInternal()
        {
            if (this.ModifierChanged != null)
            {
                this.ModifierChanged(this, EventArgs.Empty);
            }
        }

        void IDisposable.Dispose()
        {
            this.ps = null;
            this.Clear();
        }

        [Obsolete("This member has become obsolete.")]
        public void UnionCalculateRectangle(RectangleF rect)
        {
        }

        public float UnitValueOf(float val) => 
            GraphicsUnitConverter.Convert(val, (float) 300f, this.dpi);

        private void UpdateDpi()
        {
            this.dpi = this.IsUsingDip ? 96f : GraphicsDpi.UnitToDpi(this.pageUnit);
        }

        internal static BrickStyle InternalBrickStyle =>
            (internalBrickStyle != null) ? internalBrickStyle : InternalBrickStyleDefault;

        private static BrickStyle InternalBrickStyleDefault
        {
            get
            {
                internalBrickStyleDefault ??= BrickStyle.CreateDefault();
                return internalBrickStyleDefault;
            }
        }

        private static Stack<BrickStyle> BrickStyleStack
        {
            get
            {
                brickStyleStack ??= new Stack<BrickStyle>();
                return brickStyleStack;
            }
        }

        [Description("Gets or sets the default BrickStyle.")]
        public BrickStyle DefaultBrickStyle
        {
            get => 
                InternalBrickStyle;
            set => 
                internalBrickStyle = value;
        }

        internal float Dpi =>
            this.dpi;

        [Description("Defines the background color for all report pages.")]
        public Color PageBackColor
        {
            get => 
                this.pageBackColor;
            set
            {
                this.pageBackColor = value;
                this.ps.RaisePageBackgrChanged(EventArgs.Empty);
            }
        }

        [Description("Specifies the page area for displaying a specific brick.")]
        public BrickModifier Modifier
        {
            get => 
                this.modifier;
            set
            {
                if (this.modifier != value)
                {
                    this.modifier = value;
                    this.RaiseModifierChangedInternal();
                }
            }
        }

        [Description("Defines graphic measurement units.")]
        public GraphicsUnit PageUnit
        {
            get => 
                this.pageUnit;
            set
            {
                this.pageUnit = value;
                this.UpdateDpi();
            }
        }

        [Description("For internal use.")]
        public bool DeviceIndependentPixel
        {
            get => 
                this.deviceIndependentPixel;
            set
            {
                this.deviceIndependentPixel = value;
                this.UpdateDpi();
            }
        }

        private bool IsUsingDip =>
            (this.pageUnit == GraphicsUnit.Pixel) && this.deviceIndependentPixel;

        internal Action<Brick> BrickCollector
        {
            get
            {
                if (this.brickCollector != null)
                {
                    return this.brickCollector;
                }
                Document document = this.ps.Document;
                return new Action<Brick>(document.AddBrick);
            }
            set => 
                this.brickCollector = value;
        }

        internal DevExpress.XtraPrinting.Native.Measurer Measurer =>
            ((IPrintingSystemContext) this.PrintingSystem).Measurer;

        [Description("Specifies the border width of the current BrickGraphics object.")]
        public float BorderWidth
        {
            get => 
                InternalBrickStyle.BorderWidth;
            set => 
                InternalBrickStyle.BorderWidth = value;
        }

        [Description("Specifies the border color for the current BrickGraphics object.")]
        public Color BorderColor
        {
            get => 
                InternalBrickStyle.BorderColor;
            set => 
                InternalBrickStyle.BorderColor = value;
        }

        [Description("Defines the background color for the current BrickGraphics object.")]
        public Color BackColor
        {
            get => 
                InternalBrickStyle.BackColor;
            set => 
                InternalBrickStyle.BackColor = value;
        }

        [Description("Defines the foreground color of the current BrickGraphics object.")]
        public Color ForeColor
        {
            get => 
                InternalBrickStyle.ForeColor;
            set => 
                InternalBrickStyle.ForeColor = value;
        }

        [Description("Specifies the font of the current BrickGraphics object.")]
        public System.Drawing.Font Font
        {
            get => 
                InternalBrickStyle.Font;
            set => 
                InternalBrickStyle.Font = (System.Drawing.Font) value.Clone();
        }

        [Description("Specifies the default font for a report.")]
        public System.Drawing.Font DefaultFont
        {
            get => 
                BrickStyle.DefaultFont;
            set
            {
            }
        }

        [Description("Gets or sets text layout information (such as alignment, orientation and tab stops) and display manipulations (such as ellipsis insertion and national digit substitution).")]
        public BrickStringFormat StringFormat
        {
            get => 
                InternalBrickStyle.StringFormat;
            set => 
                InternalBrickStyle.StringFormat = (BrickStringFormat) value.Clone();
        }

        [Description("Gets the owner of the current BrickGraphics object.")]
        public PrintingSystemBase PrintingSystem =>
            this.ps;

        [Description("Returns the dimensions of a report page without margins.")]
        public SizeF ClientPageSize =>
            GraphicsUnitConverter.Convert(this.ps.PageSettings.UsefulPageRectF.Size, (float) 300f, this.dpi);

        private class UnionCollection
        {
            private List<Brick> bricks = new List<Brick>();

            internal void Add(Brick brick)
            {
                this.bricks.Add(brick);
            }

            internal void Unite(PanelBrick unionBrick)
            {
                RectangleF empty = RectangleF.Empty;
                foreach (Brick brick in this.bricks)
                {
                    RectangleF rect = brick.Rect;
                    empty = !empty.IsEmpty ? RectangleF.Union(empty, rect) : rect;
                }
                foreach (Brick brick2 in this.bricks)
                {
                    PointF location = empty.Location;
                    brick2.Location = new PointF(Math.Max((float) 0f, (float) (brick2.Location.X - empty.Location.X)), Math.Max((float) 0f, (float) (brick2.Location.Y - location.Y)));
                    unionBrick.Bricks.Add(brick2);
                }
                this.bricks.Clear();
                unionBrick.Location = empty.Location;
                unionBrick.Size = empty.Size;
            }
        }
    }
}

