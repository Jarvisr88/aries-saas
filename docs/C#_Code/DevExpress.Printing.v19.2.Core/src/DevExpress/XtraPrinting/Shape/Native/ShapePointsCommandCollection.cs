namespace DevExpress.XtraPrinting.Shape.Native
{
    using System;

    public class ShapePointsCommandCollection : ShapeCommandCollection
    {
        public ShapePointsCommandCollection() : base(typeArray1)
        {
            Type[] typeArray1 = new Type[] { typeof(ShapeLineCommand), typeof(ShapeBezierCommand) };
        }
    }
}

