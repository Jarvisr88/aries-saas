namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;

    public sealed class DrawingBullet : IDrawingBullet
    {
        public static DrawingBullet Automatic = new DrawingBullet(DrawingBulletType.Automatic);
        public static DrawingBullet NoBullets = new DrawingBullet(DrawingBulletType.Common);
        public static DrawingBullet TypefaceFollowText = new DrawingBullet(DrawingBulletType.Typeface);
        public static DrawingBullet ColorFollowText = new DrawingBullet(DrawingBulletType.Color);
        public static DrawingBullet SizeFollowText = new DrawingBullet(DrawingBulletType.Size);
        private DrawingBulletType type;

        private DrawingBullet(DrawingBulletType type)
        {
            this.type = type;
        }

        public IDrawingBullet CloneTo(IDocumentModel documentModel) => 
            (this.type != DrawingBulletType.Common) ? ((this.type != DrawingBulletType.Typeface) ? ((this.type != DrawingBulletType.Color) ? ((this.type != DrawingBulletType.Size) ? Automatic : SizeFollowText) : ColorFollowText) : TypefaceFollowText) : NoBullets;

        public override bool Equals(object obj)
        {
            DrawingBullet bullet = obj as DrawingBullet;
            return ((bullet != null) && (this.type == bullet.type));
        }

        public override int GetHashCode() => 
            (int) this.type;

        public void Visit(IDrawingBulletVisitor visitor)
        {
            if (this.type == DrawingBulletType.Common)
            {
                visitor.VisitNoBullets();
            }
            if (this.type == DrawingBulletType.Typeface)
            {
                visitor.VisitBulletTypefaceFollowText();
            }
            if (this.type == DrawingBulletType.Color)
            {
                visitor.VisitBulletColorFollowText();
            }
            if (this.type == DrawingBulletType.Size)
            {
                visitor.VisitBulletSizeFollowText();
            }
        }

        public DrawingBulletType Type =>
            this.type;
    }
}

