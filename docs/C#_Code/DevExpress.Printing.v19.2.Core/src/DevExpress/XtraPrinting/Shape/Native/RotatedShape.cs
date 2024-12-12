namespace DevExpress.XtraPrinting.Shape.Native
{
    using DevExpress.XtraPrinting.Shape;
    using System;
    using System.ComponentModel;
    using System.Drawing.Design;

    [TypeConverter(typeof(RotatedShapeTypeConverter)), Editor("DevExpress.XtraReports.Design.RotatedShapeEditor, DevExpress.XtraReports.v19.2.Extensions, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a", typeof(UITypeEditor))]
    public class RotatedShape
    {
        public ShapeBase Shape;
        public int? Angle;

        public RotatedShape(ShapeBase shape) : this(shape, nullable)
        {
        }

        public RotatedShape(ShapeBase shape, int? angle)
        {
            this.Shape = shape;
            this.Angle = angle;
        }

        public override string ToString() => 
            this.Shape.ShapeName;

        public bool HasAngle =>
            this.Angle != null;
    }
}

