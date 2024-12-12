namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;

    public abstract class DrawingBulletSizeBase : IDrawingBullet
    {
        private int value;

        protected DrawingBulletSizeBase(int value)
        {
            this.value = value;
        }

        public abstract IDrawingBullet CloneTo(IDocumentModel documentModel);
        public override bool Equals(object obj)
        {
            DrawingBulletSizeBase drawingBulletSize = this.GetDrawingBulletSize(obj);
            return ((drawingBulletSize != null) && (this.value == drawingBulletSize.value));
        }

        protected abstract DrawingBulletSizeBase GetDrawingBulletSize(object obj);
        public override int GetHashCode() => 
            base.GetType().GetHashCode() ^ this.value;

        public abstract void Visit(IDrawingBulletVisitor visitor);

        public DrawingBulletType Type =>
            DrawingBulletType.Size;

        public int Value =>
            this.value;
    }
}

