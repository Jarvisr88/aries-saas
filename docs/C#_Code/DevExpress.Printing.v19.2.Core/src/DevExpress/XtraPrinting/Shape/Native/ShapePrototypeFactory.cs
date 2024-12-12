namespace DevExpress.XtraPrinting.Shape.Native
{
    using DevExpress.XtraPrinting.Shape;
    using System;

    public class ShapePrototypeFactory : IShapeFactory
    {
        protected ShapeBase prototype;
        protected int? angle;

        public ShapePrototypeFactory(ShapeBase prototype)
        {
            this.prototype = ShapeFactory.CloneShape(prototype);
        }

        RotatedShape IShapeFactory.CreateShape() => 
            new RotatedShape(ShapeFactory.CloneShape(this.prototype), this.angle);

        Type IShapeFactory.ShapeType =>
            this.prototype.GetType();
    }
}

