namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;

    public class DrawingBulletSizePercentage : DrawingBulletSizeBase
    {
        public DrawingBulletSizePercentage(int value) : base(value)
        {
        }

        public override IDrawingBullet CloneTo(IDocumentModel documentModel) => 
            new DrawingBulletSizePercentage(base.Value);

        protected override DrawingBulletSizeBase GetDrawingBulletSize(object obj) => 
            obj as DrawingBulletSizePercentage;

        public override void Visit(IDrawingBulletVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}

