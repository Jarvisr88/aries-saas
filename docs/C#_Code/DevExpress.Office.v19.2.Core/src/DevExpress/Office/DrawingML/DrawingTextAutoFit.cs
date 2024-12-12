namespace DevExpress.Office.DrawingML
{
    using DevExpress.Office;
    using System;

    public class DrawingTextAutoFit : IDrawingTextAutoFit
    {
        public static DrawingTextAutoFit None = new DrawingTextAutoFit(DrawingTextAutoFitType.None);
        public static DrawingTextAutoFit Shape = new DrawingTextAutoFit(DrawingTextAutoFitType.Shape);
        public static DrawingTextAutoFit Automatic = new DrawingTextAutoFit(DrawingTextAutoFitType.Automatic);
        private DrawingTextAutoFitType fitType;

        public DrawingTextAutoFit(DrawingTextAutoFitType fitType)
        {
            this.fitType = fitType;
        }

        public IDrawingTextAutoFit CloneTo(IDocumentModel documentModel) => 
            (this.Type != DrawingTextAutoFitType.Shape) ? ((this.Type != DrawingTextAutoFitType.None) ? Automatic : None) : Shape;

        public bool Equals(IDrawingTextAutoFit other) => 
            (other != null) ? (this.Type == other.Type) : false;

        public void Visit(IDrawingTextAutoFitVisitor visitor)
        {
            if (this.Type == DrawingTextAutoFitType.None)
            {
                visitor.VisitAutoFitNone();
            }
            if (this.Type == DrawingTextAutoFitType.Shape)
            {
                visitor.VisitAutoFitShape();
            }
        }

        public DrawingTextAutoFitType Type =>
            this.fitType;
    }
}

