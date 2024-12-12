namespace DevExpress.XtraPrinting
{
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting.BrickExporters;
    using System;
    using System.ComponentModel;
    using System.Drawing;

    [BrickExporter(typeof(BrickContainerExporter))]
    public class BrickContainer : BrickContainerBase
    {
        private float brickOffsetX;
        private float brickOffsetY;

        public BrickContainer()
        {
        }

        public BrickContainer(Brick baseBrick) : base(baseBrick)
        {
        }

        internal BrickContainer(BrickContainer brickContainerBase, Brick innerBrick) : base(brickContainerBase, innerBrick)
        {
            this.brickOffsetX = brickContainerBase.brickOffsetX;
            this.brickOffsetY = brickContainerBase.brickOffsetY;
        }

        protected internal override PointF AdjustLocation(PointF pt) => 
            new PointF(pt.X + this.brickOffsetX, pt.Y + this.brickOffsetY);

        public override object Clone() => 
            new BrickContainer(this, base.Brick);

        internal override PointF InnerBrickListOffset =>
            new PointF(this.brickOffsetX - base.Brick.X, this.brickOffsetY - base.Brick.Y);

        [XtraSerializableProperty, DefaultValue((float) 0f)]
        public float BrickOffsetX
        {
            get => 
                this.brickOffsetX;
            set => 
                this.brickOffsetX = value;
        }

        [XtraSerializableProperty, DefaultValue((float) 0f)]
        public float BrickOffsetY
        {
            get => 
                this.brickOffsetY;
            set => 
                this.brickOffsetY = value;
        }

        public override string BrickType =>
            "Container";

        internal override RectangleF DocumentBandRect
        {
            get
            {
                PointF location = base.Brick.InitialRect.Location;
                PointF* tfPtr1 = &location;
                tfPtr1.X -= this.brickOffsetX;
                PointF* tfPtr2 = &location;
                tfPtr2.Y -= this.brickOffsetY;
                return new RectangleF(location, base.Size);
            }
        }
    }
}

