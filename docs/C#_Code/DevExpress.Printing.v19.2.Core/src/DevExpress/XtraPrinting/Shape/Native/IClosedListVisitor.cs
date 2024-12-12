namespace DevExpress.XtraPrinting.Shape.Native
{
    using System;

    public interface IClosedListVisitor
    {
        void VisitElement(object previous, object current, int currentObjectIndex);
    }
}

