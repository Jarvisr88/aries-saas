namespace DevExpress.XtraPrinting.Shape.Native
{
    using System;

    public abstract class ShapeCommandBase
    {
        protected ShapeCommandBase()
        {
        }

        public abstract void Accept(IShapeCommandVisitor visitor);
    }
}

