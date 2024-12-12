namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;

    public class DrawingBulletAutoNumbered : IDrawingBullet
    {
        public const short DefaultStartAtValue = 1;
        private readonly DrawingTextAutoNumberSchemeType schemeType;
        private readonly short startAt;

        public DrawingBulletAutoNumbered(DrawingTextAutoNumberSchemeType schemeType, short startAt)
        {
            DrawingValueChecker.CheckTextBulletStartAtNumValue(startAt);
            this.schemeType = schemeType;
            this.startAt = startAt;
        }

        public IDrawingBullet CloneTo(IDocumentModel documentModel) => 
            new DrawingBulletAutoNumbered(this.schemeType, this.startAt);

        public override bool Equals(object obj)
        {
            DrawingBulletAutoNumbered numbered = obj as DrawingBulletAutoNumbered;
            return ((numbered != null) && ((this.schemeType == numbered.schemeType) && (this.startAt == numbered.startAt)));
        }

        public override int GetHashCode() => 
            (int) ((base.GetType().GetHashCode() ^ this.schemeType) ^ ((DrawingTextAutoNumberSchemeType) this.startAt));

        public void Visit(IDrawingBulletVisitor visitor)
        {
            visitor.Visit(this);
        }

        public DrawingTextAutoNumberSchemeType SchemeType =>
            this.schemeType;

        public DrawingBulletType Type =>
            DrawingBulletType.Common;

        public short StartAt =>
            this.startAt;
    }
}

