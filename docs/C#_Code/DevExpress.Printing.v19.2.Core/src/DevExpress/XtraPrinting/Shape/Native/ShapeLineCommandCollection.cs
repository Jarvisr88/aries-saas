namespace DevExpress.XtraPrinting.Shape.Native
{
    using System;

    public class ShapeLineCommandCollection : ShapeCommandCollection
    {
        public ShapeLineCommandCollection() : base(typeArray1)
        {
            Type[] typeArray1 = new Type[] { typeof(ShapeLineCommand) };
        }
    }
}

