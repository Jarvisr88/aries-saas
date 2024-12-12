namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;

    public class DrawingText3DFlatText : IDrawingText3D
    {
        private readonly long zCoordinate;

        public DrawingText3DFlatText(long zCoordinate)
        {
            this.zCoordinate = zCoordinate;
        }

        public DrawingText3DFlatText Clone() => 
            new DrawingText3DFlatText(this.zCoordinate);

        public IDrawingText3D CloneTo(IDocumentModel documentModel) => 
            new DrawingText3DFlatText(this.zCoordinate);

        public override bool Equals(object obj)
        {
            DrawingText3DFlatText text = obj as DrawingText3DFlatText;
            return ((text != null) && (this.zCoordinate == text.zCoordinate));
        }

        public override int GetHashCode() => 
            this.zCoordinate.GetHashCode();

        public void Visit(IDrawingText3DVisitor visitor)
        {
            visitor.Visit(this);
        }

        public long ZCoordinate =>
            this.zCoordinate;

        public DrawingText3DType Type =>
            DrawingText3DType.FlatText;
    }
}

