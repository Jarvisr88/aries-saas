namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Utils;
    using System;

    public class DrawingBulletCharacter : IDrawingBullet
    {
        private readonly string character;

        public DrawingBulletCharacter(string character)
        {
            Guard.ArgumentNotNull(character, "character");
            this.character = character;
        }

        public IDrawingBullet CloneTo(IDocumentModel documentModel) => 
            new DrawingBulletCharacter(this.character);

        public override bool Equals(object obj)
        {
            DrawingBulletCharacter character = obj as DrawingBulletCharacter;
            return ((character != null) && this.character.Equals(character.character));
        }

        public override int GetHashCode() => 
            base.GetType().GetHashCode() ^ this.character.GetHashCode();

        public void Visit(IDrawingBulletVisitor visitor)
        {
            visitor.Visit(this);
        }

        public string Character =>
            this.character;

        public DrawingBulletType Type =>
            DrawingBulletType.Common;
    }
}

