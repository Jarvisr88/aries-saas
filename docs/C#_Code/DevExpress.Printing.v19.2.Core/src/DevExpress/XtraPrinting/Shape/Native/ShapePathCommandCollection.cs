namespace DevExpress.XtraPrinting.Shape.Native
{
    using System;

    public class ShapePathCommandCollection : ShapeCommandCollection
    {
        public ShapePathCommandCollection() : base(typeArray1)
        {
            Type[] typeArray1 = new Type[] { typeof(ShapeFillPathCommand), typeof(ShapeDrawPathCommand) };
        }
    }
}

