namespace DevExpress.XtraPrinting.Export.Imaging
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;

    internal class BrickWithOffset
    {
        private DevExpress.XtraPrinting.Brick brick;
        private float horizontalOffset;
        private float verticalOffset;

        public BrickWithOffset(DevExpress.XtraPrinting.Brick brick, float horizontalOffset, float verticalOffset)
        {
            this.brick = brick;
            this.horizontalOffset = horizontalOffset;
            this.verticalOffset = verticalOffset;
        }

        public override string ToString() => 
            $"{this.Rect} {this.Brick}";

        public DevExpress.XtraPrinting.Brick Brick =>
            this.brick;

        public RectangleF Rect
        {
            get
            {
                RectangleF initialRect = this.brick.InitialRect;
                initialRect.Location = new PointF(this.horizontalOffset, this.verticalOffset);
                return initialRect;
            }
        }
    }
}

