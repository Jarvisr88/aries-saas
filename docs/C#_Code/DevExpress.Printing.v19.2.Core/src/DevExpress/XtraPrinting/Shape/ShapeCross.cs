namespace DevExpress.XtraPrinting.Shape
{
    using DevExpress.Printing;
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraPrinting.Shape.Native;
    using System;
    using System.ComponentModel;
    using System.Drawing;

    public class ShapeCross : FilletShapeBase
    {
        private int verticalLineWidth;
        private int horizontalLineWidth;

        public ShapeCross()
        {
            this.verticalLineWidth = 50;
            this.horizontalLineWidth = 50;
        }

        private ShapeCross(ShapeCross source) : base(source)
        {
            this.verticalLineWidth = 50;
            this.horizontalLineWidth = 50;
            this.horizontalLineWidth = source.HorizontalLineWidth;
            this.verticalLineWidth = source.VerticalLineWidth;
        }

        protected override ShapeBase CloneShape() => 
            new ShapeCross(this);

        protected internal override PointF[] CreatePoints(RectangleF bounds, int angle)
        {
            PointF tf = RectHelper.CenterOf(bounds);
            float num = (float) Math.Round((double) ((bounds.Width * this.verticalLineWidth) / 200f));
            float num2 = (float) Math.Round((double) ((bounds.Height * this.horizontalLineWidth) / 200f));
            RectangleF ef = new RectangleF(new PointF(tf.X - num, bounds.Y), new SizeF(2f * num, bounds.Height));
            RectangleF ef2 = new RectangleF(new PointF(bounds.X, tf.Y - num2), new SizeF(bounds.Width, 2f * num2));
            RectangleF ef3 = RectangleF.FromLTRB(ef.Left, ef2.Top, ef.Right, ef2.Bottom);
            PointF[] tfArray1 = new PointF[12];
            tfArray1[0] = ef.Location;
            tfArray1[1] = new PointF(ef.Right, ef.Top);
            tfArray1[2] = new PointF(ef3.Right, ef3.Top);
            tfArray1[3] = new PointF(ef2.Right, ef2.Top);
            tfArray1[4] = new PointF(ef2.Right, ef2.Bottom);
            tfArray1[5] = new PointF(ef3.Right, ef3.Bottom);
            tfArray1[6] = new PointF(ef.Right, ef.Bottom);
            tfArray1[7] = new PointF(ef.Left, ef.Bottom);
            tfArray1[8] = new PointF(ef3.Left, ef3.Bottom);
            tfArray1[9] = new PointF(ef2.Left, ef2.Bottom);
            tfArray1[10] = ef2.Location;
            tfArray1[11] = ef3.Location;
            return tfArray1;
        }

        [Description("Gets or sets the vertical line width of a cross (in percents)."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.Shape.ShapeCross.VerticalLineWidth"), DefaultValue(50), XtraSerializableProperty]
        public int VerticalLineWidth
        {
            get => 
                this.verticalLineWidth;
            set => 
                this.verticalLineWidth = ShapeHelper.ValidatePercentageValue(value, "verticalLineWidth");
        }

        [Description("Gets or sets the horizontal line width of a cross (in percents)."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.Shape.ShapeCross.HorizontalLineWidth"), NotifyParentProperty(true), DefaultValue(50), XtraSerializableProperty]
        public int HorizontalLineWidth
        {
            get => 
                this.horizontalLineWidth;
            set => 
                this.horizontalLineWidth = ShapeHelper.ValidatePercentageValue(value, "horizontalLineWidth");
        }

        internal override DevExpress.XtraPrinting.Shape.Native.ShapeId ShapeId =>
            DevExpress.XtraPrinting.Shape.Native.ShapeId.Cross;
    }
}

