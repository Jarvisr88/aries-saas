namespace DevExpress.XtraPrinting.Shape
{
    using DevExpress.Printing;
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting.Shape.Native;
    using System;
    using System.ComponentModel;
    using System.Drawing;

    public class ShapeStar : ShapePolygonBase
    {
        private float concavity;

        public ShapeStar()
        {
            this.concavity = 50f;
        }

        private ShapeStar(ShapeStar source) : base(source)
        {
            this.concavity = 50f;
            this.concavity = source.Concavity;
        }

        protected override ShapeBase CloneShape() => 
            new ShapeStar(this);

        protected internal override PointF[] CreatePoints(RectangleF bounds, int angle)
        {
            PointF[] tfArray = base.CreatePointsCore(bounds, 0.0, 1f);
            float num = (float) Math.Cos(base.AngleStep / 2.0);
            PointF[] tfArray2 = base.CreatePointsCore(bounds, base.AngleStep / 2.0, (1f - (this.concavity / 100f)) * num);
            PointF[] tfArray3 = new PointF[base.NumberOfSides * 2];
            int numberOfSides = base.NumberOfSides;
            for (int i = 0; i < numberOfSides; i++)
            {
                tfArray3[2 * i] = tfArray[i];
                tfArray3[(2 * i) + 1] = tfArray2[i];
            }
            return tfArray3;
        }

        internal override DevExpress.XtraPrinting.Shape.Native.ShapeId ShapeId =>
            DevExpress.XtraPrinting.Shape.Native.ShapeId.Star;

        [Description("Gets or sets the number of points for the star shape."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.Shape.ShapeStar.StarPointCount"), DefaultValue(3), XtraSerializableProperty]
        public int StarPointCount
        {
            get => 
                base.NumberOfSides;
            set => 
                base.NumberOfSides = value;
        }

        [Description("Specifies the star's concavity value."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.Shape.ShapeStar.Concavity"), DefaultValue((float) 50f), XtraSerializableProperty]
        public float Concavity
        {
            get => 
                this.concavity;
            set => 
                this.concavity = ShapeHelper.ValidatePercentageValue(value, "Concavity");
        }
    }
}

