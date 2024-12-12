namespace DevExpress.XtraPrinting
{
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting.BrickExporters;
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraPrinting.NativeBricks;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;

    [BrickExporter(typeof(PanelBrickExporter))]
    public class PanelBrick : VisualBrick, IPanelBrick, IVisualBrick, IBaseBrick, IBrick
    {
        private BrickCollectionBase bricks;

        public PanelBrick() : this(NullBrickOwner.Instance)
        {
        }

        public PanelBrick(BrickStyle style) : base(style)
        {
            this.bricks = new BrickCollectionBase(this);
        }

        public PanelBrick(IBrickOwner brickOwner) : base(brickOwner)
        {
            this.bricks = new BrickCollectionBase(this);
        }

        internal PanelBrick(PanelBrick panelBrick) : base(panelBrick)
        {
            this.bricks = new BrickCollectionBase(this);
        }

        private void AddAdditionalBricks()
        {
            if (this.Bricks.Count != 0)
            {
                SpanPanelBrick item = new SpanPanelBrick();
                SpanPanelBrick brick2 = new SpanPanelBrick {
                    BackColor = item.BackColor = base.BackColor,
                    BorderStyle = item.BorderStyle = base.BorderStyle,
                    BorderWidth = item.BorderWidth = base.BorderWidth,
                    BorderColor = item.BorderColor = base.BorderColor
                };
                RectangleF childBricksRect = this.GetChildBricksRect();
                item.Rect = new RectangleF(new PointF(0f, 0f), new SizeF(this.Rect.Width, childBricksRect.Top));
                brick2.Rect = new RectangleF(0f, childBricksRect.Bottom, this.Rect.Width, this.Rect.Height - childBricksRect.Bottom);
                item.Sides = BorderSide.Right | BorderSide.Top | BorderSide.Left;
                brick2.Sides = BorderSide.Bottom | BorderSide.Right | BorderSide.Left;
                this.Bricks.Add(item);
                this.Bricks.Add(brick2);
            }
        }

        public void CenterChildControls()
        {
            this.RemoveSpanPanels();
            PointF diffPoint = this.GetDiffPoint(this.Rect);
            foreach (Brick brick in this.bricks)
            {
                RectangleF initialRect = brick.InitialRect;
                brick.InitialRect = new RectangleF(new PointF(brick.InitialRect.Left + diffPoint.X, brick.InitialRect.Top + diffPoint.Y), initialRect.Size);
            }
            this.AddAdditionalBricks();
        }

        public override object Clone()
        {
            PanelBrick brick = new PanelBrick(this);
            foreach (Brick brick2 in this.bricks)
            {
                brick.Bricks.Add((Brick) brick2.Clone());
            }
            return brick;
        }

        protected override object CreateCollectionItemCore(string propertyName, XtraItemEventArgs e) => 
            (propertyName != "Bricks") ? base.CreateCollectionItemCore(propertyName, e) : BrickFactory.CreateBrick(e);

        public override void Dispose()
        {
            base.Dispose();
            foreach (Brick brick in this.bricks)
            {
                brick.Dispose();
            }
            this.bricks.Clear();
        }

        private PointF GetCenterOfBricksRect()
        {
            RectangleF childBricksRect = this.GetChildBricksRect();
            return new PointF(childBricksRect.Left + (childBricksRect.Width / 2f), childBricksRect.Top + (childBricksRect.Height / 2f));
        }

        private RectangleF GetChildBricksRect()
        {
            RectangleF[] rects = (RectangleF[]) Array.CreateInstance(typeof(RectangleF), this.bricks.Count);
            for (int i = 0; i < this.bricks.Count; i++)
            {
                rects[i] = this.bricks[i].GetViewRectangle();
            }
            return UnionRects(rects);
        }

        private PointF GetDiffPoint(RectangleF clientRect)
        {
            PointF centerOfBricksRect = this.GetCenterOfBricksRect();
            PointF tf2 = new PointF(clientRect.Width / 2f, clientRect.Height / 2f);
            return new PointF(tf2.X - centerOfBricksRect.X, tf2.Y - centerOfBricksRect.Y);
        }

        protected internal virtual ICollection GetInnerBricks() => 
            this.Bricks;

        internal void InitializeBrick(Brick brick, bool cacheStyle)
        {
            brick.Modifier = base.Modifier;
            if (!brick.IsInitialized && (this.PrintingSystem != null))
            {
                this.PrintingSystem.Graph.InitializeBrick(brick, brick.Rect, cacheStyle);
            }
        }

        protected override void OnSetPrintingSystem(bool cacheStyle)
        {
            base.OnSetPrintingSystem(cacheStyle);
            foreach (Brick brick in this.bricks)
            {
                this.InitializeBrick(brick, cacheStyle);
            }
        }

        protected virtual void PerformInnerBricksLayout(IPrintingSystemContext context)
        {
            foreach (Brick brick in this.Bricks)
            {
                brick.PerformLayout(context);
            }
        }

        protected internal override void PerformLayout(IPrintingSystemContext context)
        {
            this.PerformInnerBricksLayout(context);
            base.PerformLayout(context);
        }

        private void RemoveSpanPanels()
        {
            for (int i = this.Bricks.Count - 1; i >= 0; i--)
            {
                if (this.Bricks[i] is SpanPanelBrick)
                {
                    this.Bricks.RemoveAt(i);
                }
            }
        }

        protected internal override void Scale(double scaleFactor)
        {
            base.Scale(scaleFactor);
            foreach (Brick brick in this.bricks)
            {
                brick.Scale(scaleFactor);
            }
        }

        protected override void SetIndexCollectionItemCore(string propertyName, XtraSetItemIndexEventArgs e)
        {
            if (propertyName == "Bricks")
            {
                this.Bricks.Add((Brick) e.Item.Value);
            }
            base.SetIndexCollectionItemCore(propertyName, e);
        }

        protected override bool ShouldSerializeCore(string propertyName) => 
            (propertyName != "Bricks") ? base.ShouldSerializeCore(propertyName) : (this.Bricks.Count > 0);

        private static RectangleF UnionRects(RectangleF[] rects)
        {
            RectangleF empty = RectangleF.Empty;
            foreach (RectangleF ef2 in rects)
            {
                empty = (empty == RectangleF.Empty) ? ef2 : RectangleF.Union(empty, ef2);
            }
            return empty;
        }

        public override float ValidatePageBottom(RectangleF pageBounds, bool enforceSplitNonSeparable, RectangleF rect, IPrintingSystemContext context) => 
            (this.SeparableVert || ((pageBounds.Height < rect.Height) & enforceSplitNonSeparable)) ? new PageValidator(this.Bricks).ValidateBottom(pageBounds, enforceSplitNonSeparable, rect, context) : rect.Top;

        protected internal override float ValidatePageBottomInternal(float pageBottom, RectangleF rect, IPrintingSystemContext context) => 
            new PageValidator(this.Bricks).ValidateBottom(pageBottom, rect, context);

        protected override float ValidatePageRightInternal(float pageRight, RectangleF rect) => 
            new PageValidator(this.Bricks).ValidateRight(pageRight, rect);

        IList IPanelBrick.Bricks =>
            this.bricks;

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

        [Description("Gets a collection of bricks which are contained in this PanelBrick."), XtraSerializableProperty(XtraSerializationVisibility.Collection, true, false, false, 1, XtraSerializationFlags.Cached), EditorBrowsable(EditorBrowsableState.Always)]
        public override BrickCollectionBase Bricks =>
            this.bricks;

        [Description("Gets the text string, containing the brick type information.")]
        public override string BrickType =>
            "Panel";

        [Description("Gets or sets a value indicating whether child bricks that are contained within the current panel brick should be merged into a single Brick object.")]
        public bool Merged
        {
            get => 
                base.flags[BrickBase.bitMerged];
            set => 
                base.flags[BrickBase.bitMerged] = value;
        }
    }
}

