namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;

    public interface IDrawingEffect
    {
        IDrawingEffect CloneTo(IDocumentModel documentModel);
        void Visit(IDrawingEffectVisitor visitor);
    }
}

