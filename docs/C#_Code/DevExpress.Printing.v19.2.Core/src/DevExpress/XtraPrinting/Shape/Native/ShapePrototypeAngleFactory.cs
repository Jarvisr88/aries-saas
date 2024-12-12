namespace DevExpress.XtraPrinting.Shape.Native
{
    using DevExpress.XtraPrinting.Shape;
    using System;

    public class ShapePrototypeAngleFactory : ShapePrototypeFactory
    {
        public ShapePrototypeAngleFactory(ShapeBase prototype, int angle) : base(prototype)
        {
            base.angle = new int?(angle);
        }
    }
}

