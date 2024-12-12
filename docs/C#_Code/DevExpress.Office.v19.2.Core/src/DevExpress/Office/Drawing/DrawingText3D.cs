namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;

    public sealed class DrawingText3D : IDrawingText3D
    {
        public static DrawingText3D Automatic = new DrawingText3D();

        private DrawingText3D()
        {
        }

        public IDrawingText3D CloneTo(IDocumentModel documentModel) => 
            Automatic;

        public override bool Equals(object obj) => 
            obj is DrawingText3D;

        public override int GetHashCode() => 
            0;

        public void Visit(IDrawingText3DVisitor visitor)
        {
        }

        public DrawingText3DType Type =>
            DrawingText3DType.Automatic;
    }
}

