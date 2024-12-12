namespace DevExpress.Office.Drawing
{
    using System;

    public interface IDrawingBulletVisitor
    {
        void Visit(DrawingBlip bullet);
        void Visit(DrawingBulletAutoNumbered bullet);
        void Visit(DrawingBulletCharacter bullet);
        void Visit(DrawingBulletSizePercentage bullet);
        void Visit(DrawingBulletSizePoints bullet);
        void Visit(DrawingColor bullet);
        void Visit(DrawingTextFont bullet);
        void VisitBulletColorFollowText();
        void VisitBulletSizeFollowText();
        void VisitBulletTypefaceFollowText();
        void VisitNoBullets();
    }
}

