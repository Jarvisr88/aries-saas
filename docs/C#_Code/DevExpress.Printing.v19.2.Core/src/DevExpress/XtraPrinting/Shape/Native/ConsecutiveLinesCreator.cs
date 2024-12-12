namespace DevExpress.XtraPrinting.Shape.Native
{
    using System;
    using System.Drawing;

    public class ConsecutiveLinesCreator : IClosedListVisitor
    {
        private ShapeLineCommandCollection lines = new ShapeLineCommandCollection();

        void IClosedListVisitor.VisitElement(object previous, object current, int currentObjectIndex)
        {
            this.lines.AddLine((PointF) previous, (PointF) current);
        }

        public ShapeLineCommandCollection Lines =>
            this.lines;
    }
}

