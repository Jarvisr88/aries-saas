namespace DevExpress.XtraPrinting
{
    using System;
    using System.Drawing;

    public class BrickWrapper : BrickContainerBase
    {
        public BrickWrapper(Brick baseBrick) : base(baseBrick)
        {
        }

        internal override PointF InnerBrickListOffset =>
            PointF.Empty;

        internal override RectangleF DocumentBandRect =>
            this.InitialRect;

        protected internal override RectangleF InitialRect =>
            base.InitialRectCore;
    }
}

