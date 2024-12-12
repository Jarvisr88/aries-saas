namespace DevExpress.XtraPrinting
{
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting.BrickExporters;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections;
    using System.Drawing;

    [BrickExporter(typeof(BrickContainerBaseExporter))]
    public class BrickContainerBase : DevExpress.XtraPrinting.Brick
    {
        private DevExpress.XtraPrinting.Brick baseBrick;

        public BrickContainerBase()
        {
        }

        public BrickContainerBase(DevExpress.XtraPrinting.Brick baseBrick)
        {
            if (baseBrick == null)
            {
                throw new ArgumentNullException("baseBrick");
            }
            this.baseBrick = baseBrick;
        }

        internal BrickContainerBase(BrickContainerBase brickContainerBase, DevExpress.XtraPrinting.Brick innerBrick) : base(brickContainerBase)
        {
            this.baseBrick = innerBrick;
        }

        protected internal virtual PointF AdjustLocation(PointF pt) => 
            pt;

        public override object Clone() => 
            new BrickContainerBase(this, this.Brick);

        protected override object CreateContentPropertyValue(XtraItemEventArgs e) => 
            (e.Item.Name != "Brick") ? base.CreateContentPropertyValue(e) : BrickFactory.CreateBrick(e);

        internal RectangleF GetBrickRect(PointF location) => 
            new RectangleF(this.AdjustLocation(location), this.baseBrick.Size);

        public override IEnumerator GetEnumerator()
        {
            object[] objArray1 = new object[] { this.baseBrick };
            return objArray1.GetEnumerator();
        }

        internal override DevExpress.XtraPrinting.Brick GetRealBrick() => 
            this.baseBrick.GetRealBrick();

        public override float ValidatePageBottom(RectangleF pageBounds, bool enforceSplitNonSeparable, RectangleF rect, IPrintingSystemContext context) => 
            this.baseBrick.ValidatePageBottom(pageBounds, enforceSplitNonSeparable, this.GetBrickRect(rect.Location), context);

        protected internal override float ValidatePageBottomInternal(float pageBottom, RectangleF rect, IPrintingSystemContext context) => 
            this.baseBrick.ValidatePageBottomInternal(pageBottom, rect, context);

        public override float ValidatePageRight(float pageRight, RectangleF rect) => 
            this.baseBrick.ValidatePageRight(pageRight, this.GetBrickRect(rect.Location));

        internal override bool IsServiceBrick =>
            this.baseBrick.IsServiceBrick;

        internal override IList InnerBrickList =>
            new BrickBase[] { this.baseBrick };

        internal override PointF InnerBrickListOffset =>
            new PointF(-this.Brick.X, -this.Brick.Y);

        protected internal override RectangleF InitialRect
        {
            get
            {
                PointF location = this.baseBrick.InitialRect.Location;
                return new RectangleF(this.AdjustLocation(location), base.Size);
            }
        }

        protected RectangleF InitialRectCore =>
            base.InitialRect;

        public override BrickCollectionBase Bricks =>
            this.baseBrick.Bricks;

        public override bool SeparableHorz =>
            this.baseBrick.SeparableHorz;

        public override bool SeparableVert =>
            this.baseBrick.SeparableVert;

        public override bool RepeatForVerticallySplitContent =>
            this.baseBrick.RepeatForVerticallySplitContent;

        [XtraSerializableProperty(XtraSerializationVisibility.Content, true, false, false, 0, XtraSerializationFlags.Cached)]
        public DevExpress.XtraPrinting.Brick Brick
        {
            get => 
                this.baseBrick;
            set => 
                this.baseBrick = value;
        }

        public override string BrickType =>
            "ContainerBase";
    }
}

