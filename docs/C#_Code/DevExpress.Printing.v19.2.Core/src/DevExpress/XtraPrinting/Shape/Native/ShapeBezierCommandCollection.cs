namespace DevExpress.XtraPrinting.Shape.Native
{
    using System;

    public class ShapeBezierCommandCollection : ShapeCommandCollection
    {
        public ShapeBezierCommandCollection() : base(typeArray1)
        {
            Type[] typeArray1 = new Type[] { typeof(ShapeBezierCommand) };
        }
    }
}

