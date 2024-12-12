namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;

    public class DrawingTextLineBreak : DrawingTextRunBase, IDrawingTextRun, ISupportsCopyFrom<DrawingTextLineBreak>
    {
        public DrawingTextLineBreak(IDocumentModel documentModel) : base(documentModel)
        {
        }

        public IDrawingTextRun CloneTo(IDocumentModel documentModel)
        {
            DrawingTextLineBreak @break = new DrawingTextLineBreak(documentModel);
            @break.CopyFrom(this);
            return @break;
        }

        public void CopyFrom(DrawingTextLineBreak value)
        {
            base.CopyFrom(value);
        }

        public void Visit(IDrawingTextRunVisitor visitor)
        {
            visitor.Visit(this);
        }

        public string Text =>
            Environment.NewLine;
    }
}

