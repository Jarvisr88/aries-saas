namespace DevExpress.XtraPrinting.Shape.Native
{
    using System;

    public interface IShapeFactory
    {
        RotatedShape CreateShape();

        Type ShapeType { get; }
    }
}

