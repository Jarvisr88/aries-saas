namespace DevExpress.XtraPrinting.Shape
{
    using DevExpress.Printing;
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraPrinting.Shape.Native;
    using System;
    using System.ComponentModel;
    using System.Drawing;

    public class ShapeArrow : FilletShapeBase
    {
        private int arrowWidth;
        private int arrowHeight;

        public ShapeArrow()
        {
            this.arrowWidth = 50;
            this.arrowHeight = 50;
        }

        private ShapeArrow(ShapeArrow source) : base(source)
        {
            this.arrowWidth = 50;
            this.arrowHeight = 50;
            this.arrowHeight = source.ArrowHeight;
            this.arrowWidth = source.ArrowWidth;
        }

        protected override ShapeBase CloneShape() => 
            new ShapeArrow(this);

        protected internal override PointF[] CreatePoints(RectangleF bounds, int angle)
        {
            PointF tf = RectHelper.CenterOf(bounds);
            double d = (3.1415926535897931 * angle) / 180.0;
            float num2 = (float) ((bounds.Height * Math.Abs(Math.Cos(d))) + (bounds.Width * Math.Abs(Math.Sin(d))));
            float num3 = (float) ((bounds.Width * Math.Abs(Math.Cos(d))) + (bounds.Height * Math.Abs(Math.Sin(d))));
            PointF tf2 = new PointF(bounds.X + (num3 / 2f), bounds.Y + (num2 / 2f));
            bounds.Offset(tf.X - tf2.X, tf.Y - tf2.Y);
            float num4 = num2 * ShapeHelper.PercentsToRatio(this.arrowHeight);
            float y = bounds.Y + num2;
            float num6 = num3 * ShapeHelper.PercentsToRatio(this.arrowWidth);
            float num7 = bounds.Y + num4;
            float x = bounds.X + ((num3 + num6) / 2f);
            float num9 = bounds.X + ((num3 - num6) / 2f);
            return new PointF[] { new PointF(bounds.X, num7), new PointF(bounds.X + (num3 / 2f), bounds.Y), new PointF(bounds.X + num3, num7), new PointF(x, num7), new PointF(x, y), new PointF(num9, y), new PointF(num9, num7) };
        }

        internal override DevExpress.XtraPrinting.Shape.Native.ShapeId ShapeId =>
            DevExpress.XtraPrinting.Shape.Native.ShapeId.Arrow;

        [Description("Gets or sets the width of an arrow (in percent)."), DefaultValue(50), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.Shape.ShapeArrow.ArrowWidth"), XtraSerializableProperty]
        public int ArrowWidth
        {
            get => 
                this.arrowWidth;
            set => 
                this.arrowWidth = ShapeHelper.ValidatePercentageValue(value, "ArrowWidth");
        }

        [Description("Gets or sets the height of an arrow (in percent)."), DefaultValue(50), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.Shape.ShapeArrow.ArrowHeight"), XtraSerializableProperty]
        public int ArrowHeight
        {
            get => 
                this.arrowHeight;
            set => 
                this.arrowHeight = ShapeHelper.ValidatePercentageValue(value, "ArrowHeight");
        }
    }
}

