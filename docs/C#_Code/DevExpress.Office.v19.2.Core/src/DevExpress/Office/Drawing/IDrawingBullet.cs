namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;

    public interface IDrawingBullet
    {
        IDrawingBullet CloneTo(IDocumentModel documentModel);
        void Visit(IDrawingBulletVisitor visitor);

        DrawingBulletType Type { get; }
    }
}

