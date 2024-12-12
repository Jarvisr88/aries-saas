namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;

    public sealed class DrawingStrokeUnderline : IStrokeUnderline
    {
        public static DrawingStrokeUnderline Automatic = new DrawingStrokeUnderline(DrawingStrokeUnderlineType.Automatic);
        public static DrawingStrokeUnderline FollowsText = new DrawingStrokeUnderline(DrawingStrokeUnderlineType.FollowsText);
        private DrawingStrokeUnderlineType type;

        private DrawingStrokeUnderline(DrawingStrokeUnderlineType type)
        {
            this.type = type;
        }

        public IStrokeUnderline CloneTo(IDocumentModel documentModel) => 
            (this.type != DrawingStrokeUnderlineType.FollowsText) ? Automatic : FollowsText;

        public override bool Equals(object obj)
        {
            DrawingStrokeUnderline underline = obj as DrawingStrokeUnderline;
            return ((underline != null) && (this.type == underline.Type));
        }

        public override int GetHashCode() => 
            (int) this.type;

        public DrawingStrokeUnderlineType Type =>
            this.type;
    }
}

