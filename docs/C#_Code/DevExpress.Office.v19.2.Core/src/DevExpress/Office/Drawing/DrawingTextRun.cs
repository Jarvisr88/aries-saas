namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;

    public class DrawingTextRun : DrawingTextRunStringBase, IDrawingTextRun, ISupportsCopyFrom<DrawingTextRun>
    {
        public DrawingTextRun(IDocumentModel documentModel) : base(documentModel)
        {
        }

        public DrawingTextRun(IDocumentModel documentModel, string text) : base(documentModel)
        {
            if (!string.IsNullOrEmpty(text))
            {
                base.SetTextCore(text);
            }
        }

        public IDrawingTextRun CloneTo(IDocumentModel documentModel)
        {
            DrawingTextRun run = new DrawingTextRun(documentModel);
            run.CopyFrom(this);
            return run;
        }

        public void CopyFrom(DrawingTextRun value)
        {
            base.CopyFrom(value);
        }

        public void Visit(IDrawingTextRunVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}

