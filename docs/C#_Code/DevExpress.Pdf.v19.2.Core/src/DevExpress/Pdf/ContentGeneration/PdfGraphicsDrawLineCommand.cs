namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using System;
    using System.Drawing;

    public class PdfGraphicsDrawLineCommand : PdfGraphicsStrokingCommand
    {
        private float x1;
        private float x2;
        private float y1;
        private float y2;

        public PdfGraphicsDrawLineCommand(Pen pen, float x1, float y1, float x2, float y2) : base(pen)
        {
            this.x1 = x1;
            this.y1 = y1;
            this.x2 = x2;
            this.y2 = y2;
        }

        public override void Execute(PdfGraphicsCommandConstructor constructor, PdfPage page)
        {
            base.Execute(constructor, page);
            constructor.DrawLine(this.x1, this.y1, this.x2, this.y2);
        }
    }
}

