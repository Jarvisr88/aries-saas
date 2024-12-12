namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;

    public class DrawingBulletSizePoints : DrawingBulletSizeBase
    {
        public DrawingBulletSizePoints(int value) : base(value)
        {
        }

        public override IDrawingBullet CloneTo(IDocumentModel documentModel) => 
            new DrawingBulletSizePoints(base.Value);

        protected override DrawingBulletSizeBase GetDrawingBulletSize(object obj) => 
            obj as DrawingBulletSizePoints;

        public override void Visit(IDrawingBulletVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}

