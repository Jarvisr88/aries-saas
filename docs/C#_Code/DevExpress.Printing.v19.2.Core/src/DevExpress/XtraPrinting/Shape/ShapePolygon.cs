namespace DevExpress.XtraPrinting.Shape
{
    using DevExpress.Printing;
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting.Shape.Native;
    using System;
    using System.ComponentModel;
    using System.Drawing;

    public class ShapePolygon : ShapePolygonBase
    {
        public ShapePolygon()
        {
        }

        private ShapePolygon(ShapePolygon source) : base(source)
        {
        }

        protected override ShapeBase CloneShape() => 
            new ShapePolygon(this);

        protected internal override PointF[] CreatePoints(RectangleF bounds, int angle) => 
            base.CreatePointsCore(bounds, 0.0, 1f);

        internal override DevExpress.XtraPrinting.Shape.Native.ShapeId ShapeId =>
            DevExpress.XtraPrinting.Shape.Native.ShapeId.Polygon;

        [Description("Gets or sets the number of polygon sides."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.Shape.ShapePolygon.NumberOfSides"), DefaultValue(3), XtraSerializableProperty]
        public int NumberOfSides
        {
            get => 
                base.NumberOfSides;
            set => 
                base.NumberOfSides = value;
        }
    }
}

